using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API
{
    internal class CourseRecord : Customization.Data.CourseRecord
    {
        private List<SmartSchool.Customization.Data.CategoryInfo> _CourseCategorys=new List<SmartSchool.Customization.Data.CategoryInfo>();

        private int _CourseID;

        private string _CourseName;

        private int _Credit;

        private string _Entry;

        private Dictionary<string, object> _Fields=new Dictionary<string,object>();

        private bool _NotIncludedInCalc;

        private bool _NotIncludedInCredit;

        private int _SchoolYear;

        private int _Semester;

        private List<SmartSchool.Customization.Data.StudentAttendCourseRecord> _StudentAttendList=new List<SmartSchool.Customization.Data.StudentAttendCourseRecord>();

        private string _Subject;

        private string _SubjectLevel;

        private List<SmartSchool.Customization.Data.ExamScoreInfo> _ExamScoreList = new List<SmartSchool.Customization.Data.ExamScoreInfo>();

        private bool _Required=false; 

        private string _RequiredBy="";

        private List<string> _ExamList = new List<string>();

        public CourseRecord(CourseRelated.CourseInformation course)
        {
            _CourseID = course.Identity;
            _CourseName = course.CourseName;
            _Credit = course.Credit;
            _Entry=course.Entry;
            _NotIncludedInCalc = course.NotIncludedInCalc;
            _NotIncludedInCredit = course.NotIncludedInCredit;
            _SchoolYear = course.SchoolYear;
            _Semester = course.Semester;
            _Subject = course.Subject;
            _SubjectLevel = course.SubjectLevel;
            _Required = course.Required == "必";
            _RequiredBy = course.RequiredBy;
        }

        #region CourseRecord 成員

        public List<SmartSchool.Customization.Data.CategoryInfo> CourseCategorys
        {
            get { return _CourseCategorys; }
        }

        public int CourseID
        {
            get { return _CourseID; }
        }

        public string CourseName
        {
            get { return _CourseName; }
        }

        public int Credit
        {
            get { return _Credit; }
        }

        public string Entry
        {
            get { return _Entry; }
        }

        public Dictionary<string, object> Fields
        {
            get { return _Fields; }
        }

        public bool NotIncludedInCalc
        {
            get { return _NotIncludedInCalc; }
        }

        public bool NotIncludedInCredit
        {
            get { return _NotIncludedInCredit; }
        }

        public int SchoolYear
        {
            get { return _SchoolYear; }
        }

        public int Semester
        {
            get { return _Semester; }
        }

        public List<SmartSchool.Customization.Data.StudentAttendCourseRecord> StudentAttendList
        {
            get { return _StudentAttendList; }
        }

        public string Subject
        {
            get { return _Subject; }
        }

        public string SubjectLevel
        {
            get { return _SubjectLevel; }
        }
        
        public List<SmartSchool.Customization.Data.ExamScoreInfo> ExamScoreList
        {
            get { return _ExamScoreList; }
        }
        
        public bool Required
        {
            get { return _Required; }
        }
       
        public string RequiredBy
        {
            get { return _RequiredBy; }
        }

        public List<string> ExamList
        {
            get
            {
                return _ExamList;
            }
        }

        #endregion

        #region CourseRecord 成員

        SmartSchool.Customization.Data.CategoryCollection SmartSchool.Customization.Data.CourseRecord.CourseCategorys
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}
