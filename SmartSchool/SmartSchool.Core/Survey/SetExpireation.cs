using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.Survey
{
    public partial class SetExpireation : BaseForm
    {
        public SetExpireation()
        {
            InitializeComponent();
        }

        public SetExpireation(string dateTimeString)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(dateTimeString))
                return;

            Expireation = dateTimeString;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dt;

            if (string.IsNullOrEmpty(Expireation))
            {
                DialogResult = DialogResult.OK;
                return;
            }

            if (!DateTime.TryParse(txtExpireation.Text, out dt))
            {
                MsgBox.Show("請輸入合法的日期格式。");
                DialogResult = DialogResult.None;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        public string Expireation
        {
            get { return txtExpireation.Text; }
            set
            {
                txtExpireation.Text = value;
            }
        }
    }
}