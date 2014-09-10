using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using Aspose.Words;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.Customization.Data;

namespace SmartSchool.ArchivalImage
{
    public partial class FrontForm : BaseForm
    {
        private MemoryStream _template1 = null;
        private MemoryStream _template2 = null;
        private MemoryStream _defaultTemplate1 = new MemoryStream(Properties.Resources.學籍表高中);
        private MemoryStream _defaultTemplate2 = new MemoryStream(Properties.Resources.學籍表高職);

        private int _useTemplateNumber = 1;
        private byte[] _buffer1 = null;
        private byte[] _buffer2 = null;

        public int TemplateNumber
        {
            get { return _useTemplateNumber; }
        }

        public MemoryStream Template
        {
            get
            {
                switch (_useTemplateNumber)
                {
                    case 1:
                        return _defaultTemplate1;
                    case 2:
                        if (_template1 != null && Document.DetectFileFormat(_template1) == LoadFormat.Doc)
                            return _template1;
                        else
                            return _defaultTemplate1;
                    case 3:
                        return _defaultTemplate2;
                    case 4:
                        if (_template2 != null && Document.DetectFileFormat(_template2) == LoadFormat.Doc)
                            return _template2;
                        else
                            return _defaultTemplate2;
                    default:
                        return new MemoryStream();
                }
            }
        }

        private bool _error2 = false;

        private string _text1;
        private string _text2;

        public string Text1
        {
            get { return _text1; }
        }

        public string Text2
        {
            get { return _text2; }
        }

        private int _custodian = 0;
        private int _address = 0;
        private int _phone = 0;

        public int Custodian
        {
            get { return _custodian; }
        }

        public int Address
        {
            get { return _address; }
        }

        public int Phone
        {
            get { return _phone; }
        }

        private bool _over100 = false;
        public bool AllowMoralScoreOver100
        {
            get { return _over100; }
        }

        #region 自訂標示

        /// <summary>
        /// 核心科目標示
        /// </summary>
        private string _sign_core_subject = "";
        public string SignCoreSubject
        {
            get { return _sign_core_subject; }
        }

        /// <summary>
        /// 未取得學分標示
        /// </summary>
        private string _sign_failed = "*";
        public string SignFailed
        {
            get { return _sign_failed; }
        }

        /// <summary>
        /// 學年調整成績標示
        /// </summary>
        private string _sign_school_year_adjust = "";
        public string SignSchoolYearAdjust
        {
            get { return _sign_school_year_adjust; }
        }

        /// <summary>
        /// 手動調整成績標示
        /// </summary>
        private string _sign_manual_adjust = "";
        public string SignManualAdjust
        {
            get { return _sign_manual_adjust; }
        }

        /// <summary>
        /// 補考成績標示
        /// </summary>
        private string _sign_resit = "C";
        public string SignResit
        {
            get { return _sign_resit; }
        }

        /// <summary>
        /// 重修成績標示
        /// </summary>
        private string _sign_retake = "#";
        public string SignRetake
        {
            get { return _sign_retake; }
        }

        #endregion

        private Dictionary<string, string> _tagList = new Dictionary<string, string>();
        public Dictionary<string, string> TagList
        {
            get { return _tagList; }
        }

        public FrontForm()
        {
            InitializeComponent();
            LoadPreference();
        }

        private void LoadPreference()
        {
            #region 讀取 Preference

            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"];

            if (config != null)
            {
                _useTemplateNumber = int.Parse(config.GetAttribute("TemplateNumber"));

                XmlElement customize1 = (XmlElement)config.SelectSingleNode("CustomizeTemplate1");
                XmlElement customize2 = (XmlElement)config.SelectSingleNode("CustomizeTemplate2");
                XmlElement serial = (XmlElement)config.SelectSingleNode("Serial");
                XmlElement print = (XmlElement)config.SelectSingleNode("Print");
                XmlElement tags = (XmlElement)config.SelectSingleNode("Tags");

                if (customize1 != null)
                {
                    string templateBase64 = customize1.InnerText;
                    _buffer1 = Convert.FromBase64String(templateBase64);
                    _template1 = new MemoryStream(_buffer1);
                }
                else
                {
                    XmlElement newCustomize1 = config.OwnerDocument.CreateElement("CustomizeTemplate1");
                    config.AppendChild(newCustomize1);
                    SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"] = config;
                }

                if (customize2 != null)
                {
                    string templateBase64 = customize2.InnerText;
                    _buffer2 = Convert.FromBase64String(templateBase64);
                    _template2 = new MemoryStream(_buffer2);
                }
                else
                {
                    XmlElement newCustomize2 = config.OwnerDocument.CreateElement("CustomizeTemplate2");
                    config.AppendChild(newCustomize2);
                    SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"] = config;
                }

                if (serial != null)
                {
                    textBoxX1.Text = serial.GetAttribute("Word");
                    textBoxX2.Text = serial.GetAttribute("Number");
                }
                else
                {
                    XmlElement newSerial = config.OwnerDocument.CreateElement("Serial");
                    newSerial.SetAttribute("Word", "");
                    newSerial.SetAttribute("Number", "");
                    config.AppendChild(newSerial);
                    SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"] = config;
                }

                if (print != null)
                {
                    _custodian = int.Parse(print.GetAttribute("Custodian"));
                    _address = int.Parse(print.GetAttribute("Address"));
                    _phone = int.Parse(print.GetAttribute("Phone"));
                    if (!string.IsNullOrEmpty(print.GetAttribute("AllowMoralScoreOver100")))
                        _over100 = bool.Parse(print.GetAttribute("AllowMoralScoreOver100"));

                    if (print.HasAttribute("CoreSubjectSign"))
                        _sign_core_subject = print.GetAttribute("CoreSubjectSign");
                    if (print.HasAttribute("ResitSign"))
                        _sign_resit = print.GetAttribute("ResitSign");
                    if (print.HasAttribute("RetakeSign"))
                        _sign_retake = print.GetAttribute("RetakeSign");
                    if (print.HasAttribute("FailedSign"))
                        _sign_failed = print.GetAttribute("FailedSign");
                    if (print.HasAttribute("SchoolYearAdjustSign"))
                        _sign_school_year_adjust = print.GetAttribute("SchoolYearAdjustSign");
                    if (print.HasAttribute("ManualAdjustSign"))
                        _sign_manual_adjust = print.GetAttribute("ManualAdjustSign");
                }
                else
                {
                    XmlElement newPrint = config.OwnerDocument.CreateElement("Print");
                    newPrint.SetAttribute("Custodian", "0");
                    newPrint.SetAttribute("Address", "0");
                    newPrint.SetAttribute("Phone", "0");
                    newPrint.SetAttribute("AllowMoralScoreOver100", "False");
                    config.AppendChild(newPrint);
                    SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"] = config;
                }

                if (tags != null)
                {
                    foreach (XmlElement tag in tags.SelectNodes("Tag"))
                    {
                        if (!_tagList.ContainsKey(tag.GetAttribute("ID")))
                            _tagList.Add(tag.GetAttribute("ID"), tag.GetAttribute("FullName"));
                    }
                }
            }
            else
            {
                #region 產生空白設定檔

                config = new XmlDocument().CreateElement("學生學籍表");
                config.SetAttribute("TemplateNumber", "1");
                XmlElement customize1 = config.OwnerDocument.CreateElement("CustomizeTemplate1");
                XmlElement customize2 = config.OwnerDocument.CreateElement("CustomizeTemplate2");
                XmlElement serial = config.OwnerDocument.CreateElement("Serial");
                XmlElement newPrint = config.OwnerDocument.CreateElement("Print");

                serial.SetAttribute("Word", "");
                serial.SetAttribute("Number", "");

                newPrint.SetAttribute("Custodian", "0");
                newPrint.SetAttribute("Address", "0");
                newPrint.SetAttribute("Phone", "0");
                newPrint.SetAttribute("AllowMoralScoreOver100", "False");

                config.AppendChild(customize1);
                config.AppendChild(customize2);
                config.AppendChild(serial);
                config.AppendChild(newPrint);

                SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"] = config;

                _useTemplateNumber = 1;

                #endregion
            }
            #endregion
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (_error2)
                return;

            _text1 = textBoxX1.Text;
            _text2 = textBoxX2.Text;

            #region 儲存Preference

            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"];

            if (config != null)
            {
                XmlElement serial = (XmlElement)config.SelectSingleNode("Serial");

                if (serial != null)
                {
                    serial.SetAttribute("Word", _text1);
                    serial.SetAttribute("Number", _text2);
                }
            }

            SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"] = config;

            #endregion

            this.DialogResult = DialogResult.OK;
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxX2.Text))
            {
                _error2 = false;
                int a = 0;

                foreach (char var in textBoxX2.Text.ToCharArray())
                {
                    if (!int.TryParse(var.ToString(), out a))
                    {
                        _error2 = true;
                        break;
                    }
                }

                if (_error2)
                    errorProvider2.SetError(textBoxX2, "格式為數字");
                else
                    errorProvider2.Clear();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TemplateConfigForm form = new TemplateConfigForm(
                _useTemplateNumber,
                _buffer1,
                _buffer2,
                new int[] { _custodian, _address, _phone },
                _over100,
                _sign_core_subject,
                _sign_resit,
                _sign_retake,
                _sign_school_year_adjust,
                _sign_manual_adjust,
                _sign_failed);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SmartSchool.Customization.Data.SystemInformation.getField("StudentCategories");
            XmlElement categories = SmartSchool.Customization.Data.SystemInformation.Fields["StudentCategories"] as XmlElement;
            TagConfig form = new TagConfig(categories, _tagList);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }
    }
}