using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.ClassRelated;
using System.Collections;

namespace SmartSchool.API
{
    internal class ClassRecord:Customization.Data.ClassRecord
    {
        private Hashtable _CachePool;

        private string _ClassID = "";

        private string _ClassName = "";

        private string _Department = "";

        private string _GradeYear = "";

        private string _RefTeacherID="";

        private Dictionary<string, object> _Fields = new Dictionary<string, object>();

        private List<SmartSchool.Customization.Data.CategoryInfo> _ClassCategorys = new List<SmartSchool.Customization.Data.CategoryInfo>();

        public ClassRecord(ClassInfo classInfo, Hashtable CachePool)
        {
            _ClassID = classInfo.ClassID;
            _ClassName = classInfo.ClassName;
            //_Department = classInfo.Department;
            _Department = classInfo.Department.Contains(":") || classInfo.Department.Contains("：") ? classInfo.Department.Split(':','：')[0] : classInfo.Department;

            this.Fields.Add("SubDepartment",(classInfo.Department.Contains(":") || classInfo.Department.Contains("：") )? classInfo.Department.Split(':', '：')[1] : "");

            _GradeYear = classInfo.GradeYear;
            _RefTeacherID = classInfo.TeacherID;

            _CachePool = CachePool;

        }

        #region ClassRecord 成員

        public List<SmartSchool.Customization.Data.CategoryInfo> ClassCategorys
        {
            get { return _ClassCategorys; }
        }

        public string ClassID
        {
            get { return _ClassID; }
        }

        public string ClassName
        {
            get { return _ClassName; }
        }

        public string Department
        {
            get { return _Department; }
        }

        public string GradeYear
        {
            get { return _GradeYear; }
        }

        public SmartSchool.Customization.Data.TeacherRecord RefTeacher
        {
            get
            {
                TeacherRelated.BriefTeacherData tinfo = TeacherRelated.Teacher.Instance.Items[_RefTeacherID];
                if ( tinfo != null )
                {
                    lock ( tinfo )
                    {
                        if ( _CachePool.ContainsKey(tinfo) )
                        {
                            return ( (TeacherRecord)_CachePool[tinfo] );
                        }
                        else
                        {
                            TeacherRecord newitem = new TeacherRecord(tinfo);
                            _CachePool.Add(tinfo, newitem);
                            return ( newitem );
                        }
                    }
                }
                else
                    return null;
            }
        }

        public List<SmartSchool.Customization.Data.StudentRecord> Students
        {
            get
            {
                List<SmartSchool.Customization.Data.StudentRecord> _Students = new List<SmartSchool.Customization.Data.StudentRecord>();
                foreach ( StudentRelated.BriefStudentData student in StudentRelated.Student.Instance.GetClassStudent(_ClassID) )
                {
                    lock ( student )
                    {
                        if ( _CachePool.ContainsKey(student) )
                        {
                            _Students.Add((StudentRecord)_CachePool[student]);
                        }
                        else
                        {
                            StudentRecord newitem = new StudentRecord(student, _CachePool);
                            _CachePool.Add(student, newitem);
                            _Students.Add(newitem);
                        }
                    }
                }
                return _Students;
            }
        }

        public Dictionary<string, object> Fields
        {
            get { return _Fields; }
        }

        #endregion

        #region ClassRecord 成員

        SmartSchool.Customization.Data.CategoryCollection SmartSchool.Customization.Data.ClassRecord.ClassCategorys
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}
