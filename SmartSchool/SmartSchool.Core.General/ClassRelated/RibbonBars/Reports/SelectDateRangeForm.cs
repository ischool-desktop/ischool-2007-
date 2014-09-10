using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class SelectDateRangeForm : SmartSchool.Common.BaseForm
    {
        protected DateTime _startDate;
        protected DateTime _endDate;
        protected bool _startTextBoxOK = false;
        protected bool _endTextBoxOK = false;
        protected bool _printable = false;

        public DateTime StartDate
        {
            get { return _startDate; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
        }

        public SelectDateRangeForm(string title) : this()
        {
            Text = title;
        }

        public SelectDateRangeForm()
        {
            InitializeComponent();
            _startDate = DateTime.Today;
            _endDate = DateTime.Today;
            textBoxX1.Text = _startDate.ToShortDateString();
            textBoxX2.Text = _endDate.ToShortDateString();
        }

        protected virtual void buttonX1_Click(object sender, EventArgs e)
        {
            if (_printable == true)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        protected virtual void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (Validate(textBoxX1.Text))
            {
                errorProvider1.Clear();
                _startTextBoxOK = true;
                if (_endTextBoxOK)
                {
                    if (!ValidateRange(textBoxX1.Text, textBoxX2.Text))
                        errorProvider1.SetError(textBoxX1, "日期區間錯誤");
                    else
                    {
                        errorProvider1.Clear();
                        errorProvider2.Clear();
                    }
                }
            }
            else
            {
                errorProvider1.SetError(textBoxX1, "日期格式錯誤");
                _startTextBoxOK = false;
                _printable = false;
            }
        }

        protected virtual void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (Validate(textBoxX2.Text))
            {
                errorProvider2.Clear();
                _endTextBoxOK = true;
                if (_startTextBoxOK)
                {
                    if (!ValidateRange(textBoxX1.Text, textBoxX2.Text))
                        errorProvider2.SetError(textBoxX2, "日期區間錯誤");
                    else
                    {
                        errorProvider1.Clear();
                        errorProvider2.Clear();
                    }
                }
            }
            else
            {
                errorProvider2.SetError(textBoxX2, "日期格式錯誤");
                _endTextBoxOK = false;
                _printable = false;
            }
        }

        private bool ValidateRange(string startDate, string endDate)
        {
            DateTime a, b;
            a = DateTime.Parse(startDate);
            b = DateTime.Parse(endDate);

            if (DateTime.Compare(b, a) < 0)
            {
                _printable = false;
                return false;
            }
            else
            {
                _printable = true;
                _startDate = a;
                _endDate = b;
                return true;
            }
        }

        protected bool Validate(string date)
        {
            DateTime a;
            return DateTime.TryParse(date, out a);
        }
    }
}