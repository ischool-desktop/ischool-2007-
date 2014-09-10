using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feature.Basic;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common.DateTimeProcess;
using DevComponents.DotNetBar.Controls;

namespace SmartSchool.Others.RibbonBars
{
    internal partial class TeacherDiffOpenConfig : BaseForm
    {
        private const string TimeDisplayFormat = "yyyy/MM/dd HH:mm";

        enum UpdateMode
        {
            Setup,
            Reset
        }

        private string _current_year, _current_sems;
        private UploadConfig _origin_config;
        private UpdateMode _current_mode;
        private EnhancedErrorProvider _errors;

        internal TeacherDiffOpenConfig()
        {
            InitializeComponent();
        }

        internal static void Display()
        {
            TeacherDiffOpenConfig config = new TeacherDiffOpenConfig();
            config.ShowDialog();
        }

        private void TeacherDiffOpenConfig_Load(object sender, EventArgs e)
        {
            _current_year = CurrentUser.Instance.SchoolYear.ToString();
            _current_sems = CurrentUser.Instance.Semester.ToString();

            lblSchoolYear.Text = CurrentUser.Instance.SchoolYear.ToString();
            lblSemester.Text = CurrentUser.Instance.Semester.ToString();

            panel1.Location = new Point(12, 42);
            panel2.Location = new Point(12, 42);

            //cboSchoolYear.Items.Add(_current_year);
            //cboSchoolYear.Items.Add((CurrentUser.Instance.SchoolYear + 1) + "");

            _errors = new EnhancedErrorProvider();

            try
            {
                _origin_config = new UploadConfig(Config.GetMoralUploadConfig());

                if (string.IsNullOrEmpty(_origin_config.StartTime))
                    ChangeSize(UpdateMode.Setup);
                else
                    ChangeSize(UpdateMode.Reset);

                //SyncSizeMode();

                EndTime = DateToDisplayFormat(_origin_config.EndTime);
                lblPreviousSetup.Text = lblPreviousSetup.Text.Replace("<%StartTime%>", DateToDisplayFormat(_origin_config.StartTime));
                lblPreviousSetup.Text = lblPreviousSetup.Text.Replace("<%EndTime%>", DateToDisplayFormat(_origin_config.EndTime));
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                CurrentUser.ReportError(ex);
                btnSetup.Enabled = false;
            }
        }

        private static string DateToDisplayFormat(string source)
        {
            if (source == null || source == string.Empty) return string.Empty;

            DateTime? dt = DateTimeHelper.ParseGregorian(source, PaddingMethod.First);
            if (dt.HasValue)
                return dt.Value.ToString(TimeDisplayFormat);
            else
                return string.Empty;
        }

        private static string DateToSaveFormat(string source)
        {
            if (source == null || source == string.Empty) return string.Empty;

            DateTime? dt = DateTimeHelper.ParseGregorian(source, PaddingMethod.First);
            if (dt.HasValue)
                return dt.Value.ToString(DateTimeHelper.StdDateTimeFormat);
            else
                return string.Empty;
        }

        //private void SyncSizeMode()
        //{
        //    if (IsOriginSemester())
        //        ChangeSize(UpdateMode.Reset);
        //    else
        //        ChangeSize(UpdateMode.Setup);
        //}

        //private bool IsOriginSemester()
        //{
        //    return _origin_config.SchoolYear == cboSchoolYear.Text && _origin_config.Semester == cboSemester.Text;
        //}

        private void ChangeSize(UpdateMode mode)
        {
            _current_mode = mode;

            panel1.Visible = (mode == UpdateMode.Setup);
            panel2.Visible = (mode == UpdateMode.Reset);

            if (mode == UpdateMode.Reset)
                Size = new Size(310, 290);
            else
                Size = new Size(310, 290 - 30);
        }

        private UpdateMode CurrentMode
        {
            get { return _current_mode; }
            set { _current_mode = value; }
        }

        private string EndTime
        {
            get
            {
                if (CurrentMode == UpdateMode.Setup)
                    return txtTerm1.Text;
                else
                    return txtTerm2.Text;
            }
            set
            {
                if (CurrentMode == UpdateMode.Setup)
                    txtTerm1.Text = value;
                else
                    txtTerm2.Text = value;
            }
        }

        private TextBoxX EndTimeControl
        {
            get
            {
                if (CurrentMode == UpdateMode.Setup)
                    return txtTerm1;
                else
                    return txtTerm2;
            }
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateControls())
                {
                    MsgBox.Show("資料有錯誤，請修正後再儲存。", Application.ProductName);
                    return;
                }

                DSXmlHelper obj = new DSXmlHelper("Request");
                obj.AddElement("Content");
                //obj.AddElement("Content", "SchoolYear", cboSchoolYear.Text);
                //obj.AddElement("Content", "Semester", cboSemester.Text);
                obj.AddElement("Content", "StartTime", DateTime.Now.ToString(DateTimeHelper.StdDateTimeFormat));

                //如果沒有輸入結束日期，就儲存空白。
                if (EndTime == string.Empty)
                    obj.AddElement("Content", "EndTime");
                else
                {
                    DateTime? dt = DateTimeHelper.ParseGregorian(EndTime, PaddingMethod.First);
                    obj.AddElement("Content", "EndTime", dt.Value.ToString(DateTimeHelper.StdDateTimeFormat));
                }

                Config.SetMoralUploadConfig(obj.BaseElement);

                Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                CurrentUser.ReportError(ex);
            }
        }

        private void cboSchoolYear_Validated(object sender, EventArgs e)
        {
            //SyncSizeMode();
        }

        private void cboSemester_Validated(object sender, EventArgs e)
        {
            //SyncSizeMode();
        }

        private void txtTerm1_Validating(object sender, CancelEventArgs e)
        {
            if (txtTerm1.Text == string.Empty)
                return;

            DateTime? dt = DateTimeHelper.ParseGregorian(txtTerm1.Text, PaddingMethod.First);
            if (dt.HasValue)
                txtTerm1.Text = dt.Value.ToString(TimeDisplayFormat);
        }

        private void txtTerm2_Validating(object sender, CancelEventArgs e)
        {
            if (txtTerm2.Text == string.Empty)
                return;

            DateTime? dt = DateTimeHelper.ParseGregorian(txtTerm2.Text, PaddingMethod.First);
            if (dt.HasValue)
                txtTerm2.Text = dt.Value.ToString(TimeDisplayFormat);
        }

        private bool ValidateControls()
        {
            _errors.Clear();

            //int number;
            //if (!int.TryParse(cboSchoolYear.Text, out number))
            //    _errors.SetError(cboSchoolYear, "請輸入整數。");

            //if (!int.TryParse(cboSemester.Text, out number))
            //    _errors.SetError(cboSemester, "請輸入整數。");

            if (EndTimeControl.Text != string.Empty)
            {
                DateTime? dt = DateTimeHelper.ParseGregorian(EndTimeControl.Text, PaddingMethod.First);
                if (!dt.HasValue)
                    _errors.SetError(EndTimeControl, "請輸入合法的日期格式。");
                else
                {
                    if (CurrentMode == UpdateMode.Reset && _origin_config.StartTime != string.Empty)
                    {
                        if (dt.Value < DateTime.Now)
                            _errors.SetError(EndTimeControl, "結束時間必須在現在時間之後。");
                    }
                }
            }

            return !_errors.HasError;
        }

        private class UploadConfig
        {
            private string _schoolyear, _semester;
            private string _start_time, _end_time;

            public UploadConfig(XmlElement config)
            {
                XmlNode n = config.SelectSingleNode("SchoolYear");
                if (n != null) SchoolYear = n.InnerText;

                n = config.SelectSingleNode("Semester");
                if (n != null) Semester = n.InnerText;

                n = config.SelectSingleNode("StartTime");
                if (n != null) StartTime = n.InnerText;

                n = config.SelectSingleNode("EndTime");
                if (n != null) EndTime = n.InnerText;
            }

            public UploadConfig()
            {
            }

            public string SchoolYear
            {
                get { return _schoolyear; }
                set { _schoolyear = value; }
            }

            public string Semester
            {
                get { return _semester; }
                set { _semester = value; }
            }

            public string StartTime
            {
                get { return _start_time; }
                set { _start_time = value; }
            }

            public string EndTime
            {
                get { return _end_time; }
                set { _end_time = value; }
            }
        }

    }
}