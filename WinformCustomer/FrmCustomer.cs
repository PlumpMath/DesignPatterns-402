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
        private ICustomer cust = null;

        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            DalLayer.Items.Add("ADODal");
            DalLayer.Items.Add("EFDal");

            txtBillingDate.Text = DateTime.Now.ToShortDateString();
            LoadGrid();
        }

        private void LoadGrid()
        {
            IDal<ICustomer> custs = FactoryDAL<IDal<ICustomer>>.Create("ADODal");
            this.dataGridView1.DataSource = custs.Search();
        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = Factory<ICustomer>.Create(cmbCustomerType.Text);
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
            IDal<ICustomer> dal = Factory<IDal<ICustomer>>.Create("ADODal");
            dal.Add(cust); // In memory
            dal.Save(); // Physical commit
        }
    }
}
