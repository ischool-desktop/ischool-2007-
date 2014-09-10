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
    public partial class SelectWeekForm : SelectDateRangeForm
    {
        private bool _FixToWeek = true;

        public bool FixToWeek
        {
            get { return _FixToWeek; }
            set
            {
                _FixToWeek = value;
                if (!value)
                    errorProvider1.Clear();
            } 
        }

        public SelectWeekForm()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            textBoxX2.Enabled = false;
            DateTime weekFirstDay = GetWeekFirstDay(DateTime.Today.AddDays(-7));
            _startDate = weekFirstDay;
            _endDate = weekFirstDay.AddDays(4);
            textBoxX1.Text = weekFirstDay.ToShortDateString();
            textBoxX2.Text = weekFirstDay.AddDays(4).ToShortDateString();
            _printable = true;
        }

        private DateTime GetWeekFirstDay(DateTime inputDate)
        {
            DateTime firstDay;
            double day = 0;

            switch (inputDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Tuesday:
                    day = -1;
                    break;
                case DayOfWeek.Wednesday:
                    day = -2;
                    break;
                case DayOfWeek.Thursday :
                    day = -3;
                    break;
                case DayOfWeek.Friday :
                    day = -4;
                    break;
                case DayOfWeek.Saturday :
                    day = -5;
                    break;
                case DayOfWeek.Sunday :
                    day = -6;
                    break;
            }
            inputDate = inputDate.AddDays(day);
            firstDay = new DateTime(inputDate.Year, inputDate.Month, inputDate.Day);
            return firstDay;
        }

        protected override void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode&&_FixToWeek)
            {
                if (Validate(textBoxX1.Text))
                {
                    _startDate = GetWeekFirstDay(DateTime.Parse(textBoxX1.Text));
                    _endDate = _startDate.AddDays(4);
                    _printable = true;
                    textBoxX2.Text = _endDate.ToShortDateString();
                }
                else
                {
                    _printable = false;
                }

                if (!_printable)
                {
                    timer1.Stop();
                    errorProvider1.SetError(textBoxX1, "輸入日期格式錯誤");
                }
                else
                {
                    if (textBoxX1.Text != _startDate.ToShortDateString() && timer1 != null)
                        timer1.Start();
                    errorProvider1.Clear();
                }
            }            
        }

        protected override void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            base.textBoxX2_TextChanged(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_printable)
                textBoxX1.Text = _startDate.ToShortDateString();
            timer1.Stop();
        }
    }
}