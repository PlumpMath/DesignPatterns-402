using System;
using System.Windows.Forms;
using FactoryCustomer;
using MiddleLayer;

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
            txtBillingDate.Text = DateTime.Now.ToShortDateString();
        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = Factory.Create(cmbCustomerType.Text);
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
    }
}
