using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.API
{
    internal class StudentAttendCourseRecord : Customization.Data.StudentAttendCourseRecord
    {
        private Customization.Data.StudentRecord _StudentRecord;

        private Customization.Data.CourseRecord _CourseRecord;

        private decimal? _FinalScore;

        private bool _Required;

        private string _RequiredBy;

        public StudentAttendCourseRecord(
            Customization.Data.StudentRecord studentRecord,
            Customization.Data.CourseRecord courseRecord,
            decimal? finalScore,
            bool? required,
            string requiredBy)
        {
            _StudentRecord = studentRecord;
            _CourseRecord = courseRecord;
            _FinalScore = finalScore;
            _Required = ( required == null ? courseRecord.Required : (bool)required );
            _RequiredBy = ( string.IsNullOrEmpty(requiredBy) ? courseRecord.RequiredBy : requiredBy );
        }

        #region StudentAttendCourseRecord 成員

        public decimal FinalScore
        {
            get { return (_FinalScore==null?0:(decimal)_FinalScore);}
        }

        public bool Required
        {
            get { return _Required; }
        }

        public string RequiredBy
        {
            get { return _RequiredBy; }
        }

        public bool HasFinalScore
        {
            get { return _FinalScore != null; }
        }
        #endregion

        #region StudentRecord 成員

        public string Department
        {
            get { return _StudentRecord.Department; }
        }

        public string Gender
        {
            get { return _StudentRecord.Gender; }
        }

        public string IDNumber
        {
            get { return _StudentRecord.IDNumber; }
        }

        public bool IsNormalStudent
        {
            get { return _StudentRecord.IsNormalStudent; }
        }

        public SmartSchool.Customization.Data.ClassRecord RefClass
        {
            get { return _StudentRecord.RefClass; }
        }

        public string SeatNo
        {
            get { return _StudentRecord.SeatNo; }
        }

        public string Status
        {
            get { return _StudentRecord.Status; }
        }

        public string StudentID
        {
            get { return _StudentRecord.StudentID; }
        }

        public string StudentName
        {
            get { return _StudentRecord.StudentName; }
        }

        public string StudentNumber
        {
            get { return _StudentRecord.StudentNumber; }
        }

        public SmartSchool.Customization.Data.CategoryCollection StudentCategorys
        {
            get { return _StudentRecord.StudentCategorys; }
        }

        #region 明確實做
        string SmartSchool.Customization.Data.StudentRecord.Department
        {
            get { return _StudentRecord.Department; }
        }

        List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo> SmartSchool.Customization.Data.StudentRecord.UpdateRecordList
        {
            get { return _StudentRecord.UpdateRecordList; }
        }

        Dictionary<string, object> SmartSchool.Customization.Data.StudentRecord.Fields
        {
            get { return _StudentRecord.Fields; }
        }

        List<SmartSchool.Customization.Data.StudentAttendCourseRecord> SmartSchool.Customization.Data.StudentRecord.AttendCourseList
        {
            get { return _StudentRecord.AttendCourseList; }
        }

        List<SmartSchool.Customization.Data.StudentExtension.AttendanceInfo> SmartSchool.Customization.Data.StudentRecord.AttendanceList
        {
            get { return _StudentRecord.AttendanceList; }
        }

        string SmartSchool.Customization.Data.StudentRecord.Birthday
        {
            get { return _StudentRecord.Birthday; }
        }

        SmartSchool.Customization.Data.StudentExtension.ContactInfo SmartSchool.Customization.Data.StudentRecord.ContactInfo
        {
            get
            {
                return _StudentRecord.ContactInfo;
            }
            set
            {
                _StudentRecord.ContactInfo = value;
            }
        }

        List<SmartSchool.Customization.Data.ExamScoreInfo> SmartSchool.Customization.Data.StudentRecord.ExamScoreList
        {
            get { return _StudentRecord.ExamScoreList; }
        }

        SmartSchool.Customization.Data.StudentExtension.ParentInfo SmartSchool.Customization.Data.StudentRecord.ParentInfo
        {
            get
            {
                return _StudentRecord.ParentInfo;
            }
            set
            {
                _StudentRecord.ParentInfo = value;
            }
        }

        List<SmartSchool.Customization.Data.StudentExtension.RewardInfo> SmartSchool.Customization.Data.StudentRecord.RewardList
        {
            get { return _StudentRecord.RewardList; }
        }

        List<SmartSchool.Customization.Data.StudentExtension.SchoolYearEntryScoreInfo> SmartSchool.Customization.Data.StudentRecord.SchoolYearEntryScoreList
        {
            get { return _StudentRecord.SchoolYearEntryScoreList; }
        }

        List<SmartSchool.Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo> SmartSchool.Customization.Data.StudentRecord.SchoolYearSubjectScoreList
        {
            get { return _StudentRecord.SchoolYearSubjectScoreList; }
        }

        List<SmartSchool.Customization.Data.StudentExtension.SemesterEntryScoreInfo> SmartSchool.Customization.Data.StudentRecord.SemesterEntryScoreList
        {
            get { return _StudentRecord.SemesterEntryScoreList; }
        }

        List<SmartSchool.Customization.Data.StudentExtension.SemesterSubjectScoreInfo> SmartSchool.Customization.Data.StudentRecord.SemesterSubjectScoreList
        {
            get { return _StudentRecord.SemesterSubjectScoreList; }
        }

        List<SmartSchool.Customization.Data.StudentExtension.SemesterMoralScoreInfo> SmartSchool.Customization.Data.StudentRecord.SemesterMoralScoreList
        {
            get { return _StudentRecord.SemesterMoralScoreList; }
        }

        List<SmartSchool.Customization.Data.StudentExtension.SemesterHistory> SmartSchool.Customization.Data.StudentRecord.SemesterHistoryList
        {
            get { return _StudentRecord.SemesterHistoryList; }
        }

        SmartSchool.Customization.Data.StudentExtension.ExtensionFieldCollection SmartSchool.Customization.Data.StudentRecord.ExtensionFields
        {
            get { return _StudentRecord.ExtensionFields; }
        }
        #endregion

        #endregion

        #region CourseRecord 成員

        public SmartSchool.Customization.Data.CategoryCollection CourseCategorys
        {
            get { return _CourseRecord.CourseCategorys; }
        }

        public int CourseID
        {
            get { return _CourseRecord.CourseID; }
        }

        public string CourseName
        {
            get { return _CourseRecord.CourseName; }
        }

        public int Credit
        {
            get { return _CourseRecord.Credit; }
        }

        public string Entry
        {
            get { return _CourseRecord.Entry; }
        }

        public bool NotIncludedInCalc
        {
            get { return _CourseRecord.NotIncludedInCalc; }
        }

        public bool NotIncludedInCredit
        {
            get { return _CourseRecord.NotIncludedInCredit; }
        }

        public int SchoolYear
        {
            get { return _CourseRecord.SchoolYear; }
        }

        public int Semester
        {
            get { return _CourseRecord.Semester; }
        }

        public string Subject
        {
            get { return _CourseRecord.Subject; }
        }

        public string SubjectLevel
        {
            get { return _CourseRecord.SubjectLevel; }
        }

        

        #region 明確實做
        Dictionary<string, object> SmartSchool.Customization.Data.CourseRecord.Fields
        {
            get { return _CourseRecord.Fields; }
        }

        List<SmartSchool.Customization.Data.ExamScoreInfo> SmartSchool.Customization.Data.CourseRecord.ExamScoreList
        {
            get { return _CourseRecord.ExamScoreList; }
        }

        List<SmartSchool.Customization.Data.StudentAttendCourseRecord> SmartSchool.Customization.Data.CourseRecord.StudentAttendList
        {
            get { return _CourseRecord.StudentAttendList; }
        }

        List<string> SmartSchool.Customization.Data.CourseRecord.ExamList
        {
            get
            {
                return _CourseRecord.ExamList;
            }
        }
        #endregion
        #endregion
    }
}
