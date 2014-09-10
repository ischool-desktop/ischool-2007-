using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated;
using SmartSchool.ClassRelated;
using System.Collections;

namespace SmartSchool.API
{
    internal class StudentRecord : Customization.Data.StudentRecord
    {
        private Hashtable _CachePool;

        private string _Gender = "";

        private bool _IsNormalStudent = false;

        private string _RefClassID;

        private string _SeatNo = "";

        private string _Status = "";

        private string _StudentID = "";

        private string _StudentName = "";

        private string _StudentNumber = "";

        private string _Birthday = "";

        private string _IDNumber = "";

        private string _OverrideDepartment = "";

        private Dictionary<string, object> _Fields = new Dictionary<string, object>();

        private SmartSchool.Customization.Data.CategoryCollection _StudentCategorys = new SmartSchool.Customization.Data.CategoryCollection();

        private SmartSchool.Customization.Data.StudentExtension.ContactInfo _ContactInfo = null;

        private SmartSchool.Customization.Data.StudentExtension.ParentInfo _ParentInfo = null;

        private SmartSchool.Customization.Data.StudentExtension.ExtensionFieldCollection _ExtensionFields = new SmartSchool.Customization.Data.StudentExtension.ExtensionFieldCollection();

        private List<SmartSchool.Customization.Data.StudentExtension.SemesterHistory> _SemesterHistoryList = new List<SmartSchool.Customization.Data.StudentExtension.SemesterHistory>();

        private List<SmartSchool.Customization.Data.StudentAttendCourseRecord> _AttendCourseList = new List<SmartSchool.Customization.Data.StudentAttendCourseRecord>();

        private List<SmartSchool.Customization.Data.StudentExtension.SemesterMoralScoreInfo> _SemsesterMoralScoreList = new List<SmartSchool.Customization.Data.StudentExtension.SemesterMoralScoreInfo>();

        private List<SmartSchool.Customization.Data.StudentExtension.AttendanceInfo> _AttendanceList = new List<SmartSchool.Customization.Data.StudentExtension.AttendanceInfo>();

        private List<SmartSchool.Customization.Data.ExamScoreInfo> _ExamScoreList = new List<SmartSchool.Customization.Data.ExamScoreInfo>();

        private List<SmartSchool.Customization.Data.StudentExtension.RewardInfo> _RewardList = new List<SmartSchool.Customization.Data.StudentExtension.RewardInfo>();

        private List<SmartSchool.Customization.Data.StudentExtension.SchoolYearEntryScoreInfo> _SchoolYearEntryScoreList = new List<SmartSchool.Customization.Data.StudentExtension.SchoolYearEntryScoreInfo>();

        private List<SmartSchool.Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo> _SchoolYearSubjectScoreList = new List<SmartSchool.Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo>();

        private List<SmartSchool.Customization.Data.StudentExtension.SemesterEntryScoreInfo> _SemesterEntryScoreList = new List<SmartSchool.Customization.Data.StudentExtension.SemesterEntryScoreInfo>();

        private List<SmartSchool.Customization.Data.StudentExtension.SemesterSubjectScoreInfo> _SemesterSubjectScoreList = new List<SmartSchool.Customization.Data.StudentExtension.SemesterSubjectScoreInfo>();

        private List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo> _UpdateRecordList = new List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>();

        public StudentRecord(BriefStudentData student, Hashtable CachePool)
        {
            _Gender = student.Gender;

            _IsNormalStudent = student.IsNormal;

            _RefClassID = student.RefClassID;

            _SeatNo = student.SeatNo;

            _Status = student.Status;

            _StudentID = student.ID;

            _StudentName = student.Name;

            _StudentNumber = student.StudentNumber;

            _Birthday = student.Birthday;

            _IDNumber = student.IDNumber;

            string dept = student.IsGraduated ? student.LeaveDepartment : student.Department;

            _OverrideDepartment = dept.Contains(":") || dept.Contains("：") ? dept.Split(':', '：')[0] : dept;

            this.Fields.Add("SubDepartment", dept.Contains(":") || dept.Contains("：") ? dept.Split(':', '：')[1] : "");

            this.Fields.Add("LeaveClassName", student.LeaveClassName);

            this.Fields.Add("LeaveDepartment", student.LeaveDepartment);

            this.Fields.Add("LeaveReason", student.LeaveReason);

            this.Fields.Add("LeaveSchoolYear", student.LeaveSchoolYear);

            _CachePool = CachePool;

            foreach ( TagManage.TagInfo tag in student.Tags )
            {
                _StudentCategorys.Add(new CategoryInfo(tag));
            }
        }

        #region StudentRecord 成員

        public List<SmartSchool.Customization.Data.StudentAttendCourseRecord> AttendCourseList
        {
            get { return _AttendCourseList; }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.AttendanceInfo> AttendanceList
        {
            get { return _AttendanceList; }
        }

        public SmartSchool.Customization.Data.StudentExtension.ContactInfo ContactInfo
        {
            get
            {
                return _ContactInfo;
            }
            set
            {
                _ContactInfo = value;
            }
        }


        public string Department
        {
            get
            {
                if ( !string.IsNullOrEmpty(_OverrideDepartment) )
                    return _OverrideDepartment;

                if ( this.RefClass != null )
                {
                    return RefClass.Department;
                }
                else
                    return "";
            }
        }


        public List<SmartSchool.Customization.Data.ExamScoreInfo> ExamScoreList
        {
            get { return _ExamScoreList; }
        }

        public string Gender
        {
            get { return _Gender; }
        }

        public bool IsNormalStudent
        {
            get { return _IsNormalStudent; }
        }

        public SmartSchool.Customization.Data.StudentExtension.ParentInfo ParentInfo
        {
            get
            {
                return _ParentInfo;
            }
            set
            {
                _ParentInfo = value;
            }
        }

        public SmartSchool.Customization.Data.ClassRecord RefClass
        {
            get
            {
                ClassInfo cinfo = Class.Instance.Items[_RefClassID];
                if ( cinfo != null )
                {
                    lock ( cinfo )
                    {
                        if ( _CachePool.ContainsKey(cinfo) )
                        {
                            return ( (SmartSchool.Customization.Data.ClassRecord)_CachePool[cinfo] );
                        }
                        else
                        {
                            SmartSchool.Customization.Data.ClassRecord newitem = new ClassRecord(cinfo, _CachePool);
                            _CachePool.Add(cinfo, newitem);
                            return newitem;
                        }
                    }
                }
                else
                    return null;
            }
        }

        public string Birthday
        {
            get { return _Birthday; }
        }

        public string IDNumber
        {
            get { return _IDNumber; }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.RewardInfo> RewardList
        {
            get
            {
                return _RewardList;
            }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.SchoolYearEntryScoreInfo> SchoolYearEntryScoreList
        {
            get { return _SchoolYearEntryScoreList; }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo> SchoolYearSubjectScoreList
        {
            get { return _SchoolYearSubjectScoreList; }
        }

        public string SeatNo
        {
            get { return _SeatNo; }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.SemesterEntryScoreInfo> SemesterEntryScoreList
        {
            get { return _SemesterEntryScoreList; }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.SemesterSubjectScoreInfo> SemesterSubjectScoreList
        {
            get { return _SemesterSubjectScoreList; }
        }

        public string Status
        {
            get { return _Status; }
        }

        public SmartSchool.Customization.Data.CategoryCollection StudentCategorys
        {
            get { return _StudentCategorys; }
        }

        public string StudentID
        {
            get { return _StudentID; }
        }

        public string StudentName
        {
            get { return _StudentName; }
        }

        public string StudentNumber
        {
            get { return _StudentNumber; }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo> UpdateRecordList
        {
            get { return _UpdateRecordList; }
        }

        public Dictionary<string, object> Fields
        {
            get { return _Fields; }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.SemesterMoralScoreInfo> SemesterMoralScoreList
        {
            get { return _SemsesterMoralScoreList; }
        }

        public List<SmartSchool.Customization.Data.StudentExtension.SemesterHistory> SemesterHistoryList
        {
            get { return _SemesterHistoryList; }
        }

        #endregion

        #region StudentRecord 成員


        public SmartSchool.Customization.Data.StudentExtension.ExtensionFieldCollection ExtensionFields
        {
            get
            {
                return _ExtensionFields;
            }
        }

        #endregion
    }
}
