using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool
{
    class CourseRec:SmartSchool.Customization.Data.CourseRecord
    {
        private List<SmartSchool.Customization.Data.CategoryInfo> _CourseCategorys = new List<SmartSchool.Customization.Data.CategoryInfo>();

        private int _CourseID;

        private string _CourseName;

        private int _Credit;

        private string _Entry;

        private Dictionary<string, object> _Fields = new Dictionary<string, object>();

        private bool _NotIncludedInCalc;

        private bool _NotIncludedInCredit;

        private int _SchoolYear;

        private int _Semester;

        private List<SmartSchool.Customization.Data.StudentAttendCourseRecord> _StudentAttendList = new List<SmartSchool.Customization.Data.StudentAttendCourseRecord>();

        private string _Subject;

        private string _SubjectLevel;

        private List<SmartSchool.Customization.Data.ExamScoreInfo> _ExamScoreList = new List<SmartSchool.Customization.Data.ExamScoreInfo>();

        private bool _Required = false;

        private string _RequiredBy = "";

        private List<string> _ExamList = new List<string>();

        public CourseRec(XmlElement element)
        {
            XmlHelper xmlHelper = new XmlHelper(element);
            _CourseID = int.Parse(element.GetAttribute("ID"));
            _CourseName = xmlHelper.GetText("CourseName");
            _Credit = int.Parse(element.GetAttribute("Credit"));
            _Entry = xmlHelper.GetText("ScoreType");
            _NotIncludedInCalc = ( xmlHelper.GetText("NotIncludedInCalc") == "是" );
            _NotIncludedInCredit = ( xmlHelper.GetText("NotIncludedInCredit") == "是" );
            _SchoolYear = int.Parse(element.GetAttribute("SchoolYear"));
            _Semester = int.Parse(element.GetAttribute("Semester"));
            _Subject = xmlHelper.GetText("Subject");
            _SubjectLevel = xmlHelper.GetText("SubjectLevel");
            _Required = xmlHelper.GetText("IsRequired") == "必";
            _RequiredBy = xmlHelper.GetText("RequiredBy");
        }
        #region CourseRecord 成員

        SmartSchool.Customization.Data.CategoryCollection SmartSchool.Customization.Data.CourseRecord.CourseCategorys
        {
            get { return _CourseCategorys; }
        }

        int SmartSchool.Customization.Data.CourseRecord.CourseID
        {
            get { return _CourseID; }
        }

        string SmartSchool.Customization.Data.CourseRecord.CourseName
        {
            get { return _CourseName; }
        }

        int SmartSchool.Customization.Data.CourseRecord.Credit
        {
            get { return _Credit; }
        }

        string SmartSchool.Customization.Data.CourseRecord.Entry
        {
            get { return _Entry; }
        }

        List<string> SmartSchool.Customization.Data.CourseRecord.ExamList
        {
            get { return _ExamList; }
        }

        List<SmartSchool.Customization.Data.ExamScoreInfo> SmartSchool.Customization.Data.CourseRecord.ExamScoreList
        {
            get { return _ExamScoreList; }
        }

        Dictionary<string, object> SmartSchool.Customization.Data.CourseRecord.Fields
        {
            get { return _Fields; }
        }

        bool SmartSchool.Customization.Data.CourseRecord.NotIncludedInCalc
        {
            get { return _NotIncludedInCalc; }
        }

        bool SmartSchool.Customization.Data.CourseRecord.NotIncludedInCredit
        {
            get { return _NotIncludedInCredit; }
        }

        bool SmartSchool.Customization.Data.CourseRecord.Required
        {
            get { return _Required; }
        }

        string SmartSchool.Customization.Data.CourseRecord.RequiredBy
        {
            get { return _RequiredBy; }
        }

        int SmartSchool.Customization.Data.CourseRecord.SchoolYear
        {
            get { return _SchoolYear; }
        }

        int SmartSchool.Customization.Data.CourseRecord.Semester
        {
            get { return _Semester; }
        }

        List<SmartSchool.Customization.Data.StudentAttendCourseRecord> SmartSchool.Customization.Data.CourseRecord.StudentAttendList
        {
            get { return _StudentAttendList; }
        }

        string SmartSchool.Customization.Data.CourseRecord.Subject
        {
            get { return _Subject; }
        }

        string SmartSchool.Customization.Data.CourseRecord.SubjectLevel
        {
            get { return _SubjectLevel; }
        }

        #endregion
    }
}
