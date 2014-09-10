using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
using System.Windows.Forms;
using SmartSchool.Common.DateTimeProcess;

namespace SmartSchool.Others.Configuration.ExamTemplate
{
    class ExamItem
    {
        public const string StdTimeFormat = "yyyy/MM/dd HH:mm";

        private DSXmlHelper _exam;
        private DSXmlHelper _clone;
        private Dictionary<string, bool> _dirties;
        private bool _is_dirty;

        private ExamItem()
        {
            _exam = new DSXmlHelper("IncludeExam");

            _exam.SetAttribute(".", "ID", "-1"); //新增資料。
            _exam.AddElement(".", "ExamTemplateID", "-1");
            _exam.AddElement(".", "RefExamID", "-1");
            _exam.AddElement(".", "UseText", "否");
            _exam.AddElement(".", "UseScore", "是");
            _exam.AddElement(".", "OpenTeacherAccess", "否");
            _exam.AddElement("Weight");
            _exam.AddElement("ExamName");
            _exam.AddElement("EndTime");
            _exam.AddElement("StartTime");

            _clone = new DSXmlHelper(_exam.BaseElement.CloneNode(true) as XmlElement);
            _dirties = new Dictionary<string, bool>();
            _is_dirty = false;
        }

        public ExamItem(XmlElement exam)
        {
            _exam = new DSXmlHelper(exam);
            _clone = new DSXmlHelper(_exam.BaseElement.CloneNode(true) as XmlElement);
            _dirties = new Dictionary<string, bool>();
            _is_dirty = false;
        }

        public static ExamItem NewExamItem()
        {
            return new ExamItem();
        }

        public string Identity
        {
            get { return _exam.GetAttribute("@ID"); }
        }

        public string ExamTemplateID
        {
            get { return _exam.GetText("ExamTemplateID"); }
        }

        public string RefExamID
        {
            get { return _exam.GetText("RefExamID"); }
        }

        public void SetRefExamID(string refExamId)
        {
            SetText("RefExamID", refExamId);
        }

        public string ExamName
        {
            get { return _exam.GetText("ExamName"); }
            set { SetText("ExamName", value); }
        }

        public string UseText
        {
            get { return _exam.GetText("UseText"); }
            set { SetText("UseText", value); }
        }

        public bool UseTextBoolean
        {
            get { return UseText == "是" ? true : false; }
        }

        public string UseScore
        {
            get { return _exam.GetText("UseScore"); }
            set { SetText("UseScore", value); }
        }

        public bool UseScoreBoolean
        {
            get { return UseScore == "是" ? true : false; }
        }

        public string OpenTeacherAccess
        {
            get { return _exam.GetText("OpenTeacherAccess"); }
            set { SetText("OpenTeacherAccess", value); }
        }

        public bool OpenTeacherAccessBoolean
        {
            get { return OpenTeacherAccess == "是" ? true : false; }
        }

        public string Weight
        {
            get { return _exam.GetText("Weight"); }
            set { SetText("Weight", value); }
        }

        public string StartTimeStdFormat
        {
            get { return ToStdFormat(_exam.GetText("StartTime")); }
        }

        public string StartTime
        {
            get
            {
                return ToExamFormat(_exam.GetText("StartTime"));
            }
            set
            {
                SetText("StartTime", ToStdFormat(value));
            }
        }

        public string EndTimeStdFormat
        {
            get { return ToStdFormat(_exam.GetText("EndTime")); }
        }

        public string EndTime
        {
            get
            {
                return ToExamFormat(_exam.GetText("EndTime"));
            }
            set
            {
                SetText("EndTime", ToStdFormat(value));
            }
        }

        public bool IsDirty
        {
            get { return _is_dirty; }
        }

        private void SetText(string name, string value)
        {
            string originValue = _clone.GetElement(name).InnerText;

            if (name == "StartTime" || name == "EndTime")
                originValue = ToStdFormat(originValue);

            if (!_dirties.ContainsKey(name))
                _dirties.Add(name, true);

            _dirties[name] = (originValue.Trim() != value.Trim());

            _is_dirty = false;
            foreach (bool each in _dirties.Values)
                _is_dirty |= each;

            _exam.GetElement(name).InnerText = value;
        }

        private static string ToExamFormat(string source)
        {
            if (source == string.Empty) return string.Empty;

            DateTime? dt = DateTimeHelper.ParseGregorian(source, PaddingMethod.First);
            if (dt.HasValue)
                return dt.Value.ToString(StdTimeFormat);
            else
                return string.Empty;
        }

        private static string ToStdFormat(string source)
        {
            if (source == string.Empty) return string.Empty;

            DateTime? dt = DateTimeHelper.ParseGregorian(source, PaddingMethod.First);
            if (dt.HasValue)
                return dt.Value.ToString(DateTimeHelper.StdDateTimeFormat);
            else
                return string.Empty;
        }
    }
}