using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.GovernmentalDocument.Process
{
    public partial class InitialForm : BaseForm
    {
        private DateTime _date;
        public DateTime Date
        {
            get
            {
                if (_date != null)
                    return _date;
                return DateTime.Today;
            }
        }

        public InitialForm()
        {
            InitializeComponent();

            textBoxX1.Text = DateTime.Today.ToShortDateString();
            textBoxX1.Focus();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            errorProvider1.Clear();
            buttonX1.Enabled = true;

            DateTime a;
            if (!DateTime.TryParse(textBoxX1.Text, out a))
            {
                errorProvider1.SetError(textBoxX1, "日期格式不正確");
                buttonX1.Enabled = false;
            }
            else
            {
                _date = a;
            }
        }
    }
}