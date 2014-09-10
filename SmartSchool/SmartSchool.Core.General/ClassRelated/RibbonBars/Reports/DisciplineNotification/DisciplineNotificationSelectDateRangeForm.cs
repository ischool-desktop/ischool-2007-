using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class DisciplineNotificationSelectDateRangeForm : SelectDateRangeForm
    {
        private MemoryStream _template = null;
        private MemoryStream _defaultTemplate = new MemoryStream(Properties.Resources.獎懲通知單);
        private byte[] _buffer = null;

        private bool _preferenceLoaded = false;

        private string _receiveName = "";
        private string _receiveAddress = "";
        private string _conditionName = "";
        private string _conditionNumber = "0";

        public string ReceiveName { get { return _receiveName; } }
        public string ReceiveAddress { get { return _receiveAddress; } }
        public string ConditionName { get { return _conditionName; } }
        public string ConditionNumber { get { return _conditionNumber; } }

        public MemoryStream Template
        {
            get
            {
                if (_useDefaultTemplate || Aspose.Words.Document.DetectFileFormat(_template) != Aspose.Words.LoadFormat.Doc)
                    return _defaultTemplate;
                else
                    return _template;
            }
        }

        private DateRangeMode _mode = DateRangeMode.Month;

        private bool _useDefaultTemplate = true;

        private bool _printHasRecordOnly = true;
        public bool PrintHasRecordOnly
        {
            get { return _printHasRecordOnly; }
        }

        public DisciplineNotificationSelectDateRangeForm()
        {
            InitializeComponent();
            Text = "獎懲通知單";
            LoadPreference();
            InitialDateRange();
        }

        private void LoadPreference()
        {
            #region 讀取 Preference

            XmlElement config = CurrentUser.Instance.Preference["獎懲通知單"];

            if (config != null)
            {
                _useDefaultTemplate = bool.Parse(config.GetAttribute("Default"));

                XmlElement customize = (XmlElement)config.SelectSingleNode("CustomizeTemplate");
                XmlElement print = (XmlElement)config.SelectSingleNode("PrintHasRecordOnly");
                XmlElement dateRangeMode = (XmlElement)config.SelectSingleNode("DateRangeMode");
                XmlElement receive = (XmlElement)config.SelectSingleNode("Receive");
                XmlElement conditions = (XmlElement)config.SelectSingleNode("Conditions");

                if (customize != null)
                {
                    string templateBase64 = customize.InnerText;
                    _buffer = Convert.FromBase64String(templateBase64);
                    _template = new MemoryStream(_buffer);
                }

                if (print != null)
                {
                    if (print.HasAttribute("Checked"))
                    {
                        _printHasRecordOnly = bool.Parse(print.GetAttribute("Checked"));
                    }
                }

                if (receive != null)
                {
                    _receiveName = receive.GetAttribute("Name");
                    _receiveAddress = receive.GetAttribute("Address");
                }
                else
                {
                    XmlElement newReceive = config.OwnerDocument.CreateElement("Receive");
                    newReceive.SetAttribute("Name", "");
                    newReceive.SetAttribute("Address", "");
                    config.AppendChild(newReceive);
                    CurrentUser.Instance.Preference["獎懲通知單"] = config;
                }

                if (conditions != null)
                {
                    if (conditions.HasAttribute("ConditionName") && conditions.HasAttribute("ConditionNumber"))
                    {
                        _conditionName = conditions.GetAttribute("ConditionName");
                        _conditionNumber = conditions.GetAttribute("ConditionNumber");
                    }
                    else
                    {
                        _conditionName = "大功";
                        _conditionNumber = "0";
                    }
                }
                else
                {
                    XmlElement newConditions = config.OwnerDocument.CreateElement("Conditions");
                    newConditions.SetAttribute("ConditionName", "");
                    newConditions.SetAttribute("ConditionNumber", "0");
                    config.AppendChild(newConditions);
                    CurrentUser.Instance.Preference["獎懲通知單"] = config;
                }

                if (dateRangeMode != null)
                {
                    _mode = (DateRangeMode)int.Parse(dateRangeMode.InnerText);
                    if (_mode != DateRangeMode.Custom)
                        textBoxX2.Enabled = false;
                    else
                        textBoxX2.Enabled = true;
                }
                else
                {
                    XmlElement newDateRangeMode = config.OwnerDocument.CreateElement("DateRangeMode");
                    newDateRangeMode.InnerText = ((int)_mode).ToString();
                    config.AppendChild(newDateRangeMode);
                    CurrentUser.Instance.Preference["獎懲通知單"] = config;
                }
            }
            else
            {
                #region 產生空白設定檔
                config = new XmlDocument().CreateElement("獎懲通知單");
                config.SetAttribute("Default", "true");
                XmlElement printSetup = config.OwnerDocument.CreateElement("PrintHasRecordOnly");
                XmlElement customize = config.OwnerDocument.CreateElement("CustomizeTemplate");
                XmlElement dateRangeMode = config.OwnerDocument.CreateElement("DateRangeMode");
                XmlElement receive = config.OwnerDocument.CreateElement("Receive");
                XmlElement conditions = config.OwnerDocument.CreateElement("Conditions");
                printSetup.SetAttribute("Checked", "true");
                dateRangeMode.InnerText = ((int)_mode).ToString();
                receive.SetAttribute("Name", "");
                receive.SetAttribute("Address", "");
                conditions.SetAttribute("ConditionName", "");
                conditions.SetAttribute("ConditionNumber", "0");
                config.AppendChild(printSetup);
                config.AppendChild(customize);
                config.AppendChild(dateRangeMode);
                config.AppendChild(receive);
                config.AppendChild(conditions);
                CurrentUser.Instance.Preference["獎懲通知單"] = config;

                _useDefaultTemplate = true;
                _printHasRecordOnly = true;

                #endregion
            }

            #endregion

            _preferenceLoaded = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DisciplineNotificationConfigForm configForm = new DisciplineNotificationConfigForm(
                _useDefaultTemplate, _printHasRecordOnly, _mode, _buffer, _receiveName, _receiveAddress, _conditionName, _conditionNumber);

            if (configForm.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
                InitialDateRange();
            }
        }

        private void InitialDateRange()
        {
            switch (_mode)
            {
                case DateRangeMode.Month:
                    {
                        DateTime a = DateTime.Today;
                        a = GetMonthFirstDay(a.AddMonths(-1));
                        textBoxX1.Text = a.ToShortDateString();
                        textBoxX2.Text = a.AddMonths(1).AddDays(-1).ToShortDateString();
                        break;
                    }
                case DateRangeMode.Week:
                    {
                        DateTime b = DateTime.Today;
                        b = GetWeekFirstDay(b.AddDays(-7));
                        textBoxX1.Text = b.ToShortDateString();
                        textBoxX2.Text = b.AddDays(5).ToShortDateString();
                        break;
                    }
                case DateRangeMode.Custom:
                    {
                        textBoxX1.Text = DateTime.Today.ToShortDateString();
                        textBoxX2.Text = textBoxX1.Text;
                        break;
                    }
                default:
                    throw new Exception("Date Range Mode Error.");
            }

            _printable = true;
            _startTextBoxOK = true;
            _endTextBoxOK = true;
        }

        private DateTime GetWeekFirstDay(DateTime inputDate)
        {
            switch (inputDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return inputDate;
                case DayOfWeek.Tuesday:
                    return inputDate.AddDays(-1);
                case DayOfWeek.Wednesday:
                    return inputDate.AddDays(-2);
                case DayOfWeek.Thursday:
                    return inputDate.AddDays(-3);
                case DayOfWeek.Friday:
                    return inputDate.AddDays(-4);
                case DayOfWeek.Saturday:
                    return inputDate.AddDays(-5);
                default:
                    return inputDate.AddDays(-6);
            }
        }

        private DateTime GetMonthFirstDay(DateTime inputDate)
        {
            return DateTime.Parse(inputDate.Year + "/" + inputDate.Month + "/1");
        }

        protected override void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            base.textBoxX1_TextChanged(sender, e);

            if (_startTextBoxOK && _mode != DateRangeMode.Custom)
            {
                switch (_mode)
                {
                    case DateRangeMode.Month:
                        {
                            _startDate = GetMonthFirstDay(DateTime.Parse(textBoxX1.Text));
                            _endDate = _startDate.AddMonths(1).AddDays(-1);
                            textBoxX2.Text = _endDate.ToShortDateString();
                            _printable = true;
                            break;
                        }
                    case DateRangeMode.Week:
                        {
                            _startDate = GetWeekFirstDay(DateTime.Parse(textBoxX1.Text));
                            _endDate = _startDate.AddDays(4);
                            textBoxX2.Text = _endDate.ToShortDateString();
                            _printable = true;
                            break;
                        }
                    case DateRangeMode.Custom:
                        break;
                    default:
                        throw new Exception("Date Range Mode Error");
                }

                if (textBoxX1.Text != _startDate.ToShortDateString() && timer1 != null)
                    timer1.Start();
                errorProvider1.Clear();
            }
        }

        protected override void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (_preferenceLoaded)
            {
                if (_mode == DateRangeMode.Custom)
                {
                    base.textBoxX2_TextChanged(sender, e);
                }
                else
                {
                    _endTextBoxOK = true;
                    errorProvider2.Clear();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_printable)
                textBoxX1.Text = _startDate.ToShortDateString();
            timer1.Stop();
        }

    }

    public enum DateRangeMode { Month, Week, Custom }
}