using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinformCustomer
{
    public partial class FrmCustomer : Form
    {
        
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            txtBillingDate.Text = DateTime.Now.ToShortDateString();
        }        
    }
}
