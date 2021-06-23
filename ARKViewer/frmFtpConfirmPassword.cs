using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmFtpConfirmPassword : Form
    {

        public string ConfirmedPassword { get; internal set; } = "";
        public frmFtpConfirmPassword()
        {
            InitializeComponent();
        }

        private void chkPasswordVisibility_CheckedChanged(object sender, EventArgs e)
        {
            txtFTPPassword.PasswordChar = chkPasswordVisibility.Checked ? '\0' : '●';
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ConfirmedPassword = txtFTPPassword.Text.Trim();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
