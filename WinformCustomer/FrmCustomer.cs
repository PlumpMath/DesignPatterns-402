using System;
using System.Windows.Forms;
using FactoryCustomer;
using InterfaceCustomer;
using InterfaceDal;
using FactoryDAL;

namespace WinformCustomer
{
    public partial class FrmCustomer : Form
    {
        private CustomerBase cust = null;
        private IRepository<CustomerBase> Idal = null;

        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            DalLayer.Items.Add("ADODal");
            DalLayer.Items.Add("EFDal");
            DalLayer.SelectedIndex = 0;

            Idal = FactoryDAL<IRepository<CustomerBase>>.Create(DalLayer.Text);

            ClearCustomer();
            LoadGrid();
        }

        private void LoadGrid()
        {
            this.dtgCustomer.DataSource = null;
            this.dtgCustomer.DataSource = Idal.Search();
        }

        private void LoadGridInMemory()
        {
            this.dtgCustomer.DataSource = null;
            this.dtgCustomer.DataSource = Idal.GetData(); // In memory 
        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cust = Factory<CustomerBase>.Create(cmbCustomerType.Text);
        }

        private void SetCustomer()
        {
            cust.CustomerName = txtCustomerName.Text;
            cust.PhoneNumber = txtPhoneNumber.Text;
            cust.Address = txtAddress.Text;
            cust.BillAmount = Convert.ToDecimal(txtBillingAmount.Text);
            cust.BillDate = Convert.ToDateTime(txtBillingDate.Text);
        }

        private void ClearCustomer()
        {
            txtCustomerName.Text = "";
            txtPhoneNumber.Text = "";
            txtBillingAmount.Text = "0";
            txtAddress.Text = "";
            txtBillingDate.Text = DateTime.Now.ToShortDateString();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                SetCustomer();
                cust.Validate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                cust.Clone();
                SetCustomer();
                Idal.Add(cust); // In memory
                                //Idal.Save(); // Physical commit
                LoadGridInMemory();
                ClearCustomer();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DalLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Idal = FactoryDAL<IRepository<CustomerBase>>.Create(DalLayer.Text);
            LoadGrid();
        }

        private void btnUOW_Click(object sender, EventArgs e)
        {
            IUow uow = FactoryDAL<IUow>.Create("EFUow");
            try
            {
                CustomerBase cust1 = new CustomerBase();
                cust1.CustomerType = "Lead";
                cust1.CustomerName = "Cust1";
                cust1.BillDate = Convert.ToDateTime(txtBillingDate.Text);
                IRepository<CustomerBase> dal1 = FactoryDAL<IRepository<CustomerBase>>.Create(DalLayer.Text);
                dal1.SetUnitOfWork(uow);
                dal1.Add(cust1);

                CustomerBase cust2 = new CustomerBase();
                cust2.CustomerType = "Lead";
                cust2.CustomerName = "Cust2";
                cust2.BillDate = Convert.ToDateTime(txtBillingDate.Text);
                cust2.Address = "111111111111111111111111111111111111111111111111111111111111111";
                IRepository<CustomerBase> dal2 = FactoryDAL<IRepository<CustomerBase>>.Create(DalLayer.Text);
                dal2.SetUnitOfWork(uow);
                dal2.Add(cust2);

                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.RollBack();
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Idal.Save();
            LoadGrid();
            ClearCustomer();
        }

        private void dtgGridCustomer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cust = Idal.GetData(e.RowIndex);
            LoadCustomerUI();
        }

        private void LoadCustomerUI()
        {
            txtCustomerName.Text = cust.CustomerName;
            txtPhoneNumber.Text = cust.PhoneNumber;
            txtAddress.Text = cust.Address;
            txtBillingAmount.Text = cust.BillAmount.ToString();
            txtBillingDate.Text = cust.BillDate.ToShortDateString();
            cmbCustomerType.Text = cust.CustomerType;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cust.Revert();
            ClearCustomer();
            LoadGridInMemory();
        }
    }
}
