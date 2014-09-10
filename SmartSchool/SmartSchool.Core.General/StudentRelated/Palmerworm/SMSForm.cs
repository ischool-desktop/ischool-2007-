using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Feature.Basic;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.Palmerworm
{
    internal partial class SMSForm : BaseForm
    {
        private string _number;

        public SMSForm()
        {
            InitializeComponent();
        }

        public void SetNumber(string number)
        {
            _number = number;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            e.Cancel = true;
            this.txtContent.Text = "";
            this.Hide();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("�z�T�w�n�o�e��²�T���y" + _number + "�z?", "�T�{", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Config.SendSMS(_number, this.txtContent.Text);
                this.Close();
            }
        }

        private void SMSForm_Shown(object sender, EventArgs e)
        {
            txtContent.Focus();
        }
    }
}