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

        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            DalLayer.Items.Add("ADODal");
            DalLayer.Items.Add("EFDal");
            DalLayer.SelectedIndex = 0;

            txtBillingDate.Text = DateTime.Now.ToShortDateString();
            LoadGrid();
        }

        private void LoadGrid()
        {
            IRepository<CustomerBase> custs = FactoryDAL<IRepository<CustomerBase>>.Create(DalLayer.Text);
            this.dataGridView1.DataSource = custs.Search();
        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = Factory<CustomerBase>.Create(cmbCustomerType.Text);
        }

        private void SetCustomer()
        {
            cust.CustomerName = txtCustomerName.Text;
            cust.PhoneNumber = txtPhoneNumber.Text;
            cust.Address = txtAddress.Text;
            cust.BillAmount = Convert.ToDecimal(txtBillingAmount.Text);
            cust.BillDate = Convert.ToDateTime(txtBillingDate.Text);
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            SetCustomer();
            try
            {
                cust.Validate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetCustomer();
            IRepository<CustomerBase> dal = FactoryDAL<IRepository<CustomerBase>>.Create(DalLayer.Text);
            dal.Add(cust); // In memory
            dal.Save(); // Physical commit
        }

        private void DalLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                dal1.Save();

                CustomerBase cust2 = new CustomerBase();
                cust2.CustomerType = "Lead";
                cust2.CustomerName = "Cust2";
                cust2.BillDate = Convert.ToDateTime(txtBillingDate.Text);
                cust2.Address = "111111111111111111111111111111111111111111111111111111111111111";
                IRepository<CustomerBase> dal2 = FactoryDAL<IRepository<CustomerBase>>.Create(DalLayer.Text);
                dal2.SetUnitOfWork(uow);
                dal2.Add(cust2);
                dal2.Save();

                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
