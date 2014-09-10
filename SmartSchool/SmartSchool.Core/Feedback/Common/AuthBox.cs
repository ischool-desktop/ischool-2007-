using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.Feedback
{
    public partial class AuthBox : BaseForm
    {
        private const string password = "%#$$^%&"; //shift + 5344657

        public AuthBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
                return;

            if (txtPassword.Text != password)
            {
                txtPassword.Focus();
                txtPassword.SelectAll();
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }
    }
}