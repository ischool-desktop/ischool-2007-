using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;

namespace SmartSchool.API.Provider
{
    internal class TeacherProvider:Customization.Data.TeacherInformationProvider
    {
        private System.Collections.Hashtable _CashePool;

        private AccessHelper _AccessHelper;

        #region TeacherInformationProvider 成員
        public SmartSchool.Customization.Data.AccessHelper AccessHelper
        {
            get
            {
                return _AccessHelper;
            }
            set
            {
                _AccessHelper = value;
            }
        }

        public List<SmartSchool.Customization.Data.TeacherRecord> GetLectureTeacher(SmartSchool.Customization.Data.CourseRecord course)
        {
            //SmartSchool.CourseRelated.CourseInformation cinfo = CouseRelated.CourseEntity.Instance.Items["" + course.CourseID];
            //if ( cinfo != null )
            //{
            //    List<string> teacheridList = new List<string>();
            //    foreach ( CouseRelated.CourseInformation.Teacher t in cinfo.Teachers )
            //    {
            //        teacheridList.Add(""+t.TeacherID);
            //    }
            //    return GetTeacher(teacheridList);
            //}
            //return new List<SmartSchool.Customization.Data.TeacherRecord>();
            return new List<SmartSchool.Customization.Data.TeacherRecord>();
        }

        public List<SmartSchool.Customization.Data.TeacherRecord> GetSelectedTeacher()
        {
            List<SmartSchool.Customization.Data.TeacherRecord> list = new List<SmartSchool.Customization.Data.TeacherRecord>();
            foreach (TeacherRelated.BriefTeacherData tinfo in TeacherRelated.Teacher.Instance.SelectionTeachers)
            {
                lock ( tinfo )
                {
                    if ( CachePool.ContainsKey(tinfo) )
                    {
                        list.Add((TeacherRecord)CachePool[tinfo]);
                    }
                    else
                    {
                        TeacherRecord newitem = new TeacherRecord(tinfo);
                        CachePool.Add(tinfo, newitem);
                        list.Add(newitem);
                    }
                }
            }
            return list;
        }

        public List<SmartSchool.Customization.Data.TeacherRecord> GetTeacher(IEnumerable<string> identities)
        {
            List<SmartSchool.Customization.Data.TeacherRecord> list = new List<SmartSchool.Customization.Data.TeacherRecord>();
            foreach (string id in identities)
            {
                TeacherRelated.BriefTeacherData tinfo = TeacherRelated.Teacher.Instance.Items[id];
                if (tinfo != null)
                {
                    lock ( tinfo )
                    {
                        if ( CachePool.ContainsKey(tinfo) )
                        {
                            list.Add((TeacherRecord)CachePool[tinfo]);
                        }
                        else
                        {
                            TeacherRecord newitem = new TeacherRecord(tinfo);
                            CachePool.Add(tinfo, newitem);
                            list.Add(newitem);
                        }
                    }
                }
            }
            return list;
        }

        public void FillField(string fieldName, IEnumerable<SmartSchool.Customization.Data.TeacherRecord> teachers)
        {
        }

        public System.Collections.Hashtable CachePool
        {
            get
            {
                return _CashePool;
            }
            set
            {
                _CashePool = value;
            }
        }

        public List<SmartSchool.Customization.Data.TeacherRecord> GetAllTeacher()
        {
            List<SmartSchool.Customization.Data.TeacherRecord> list = new List<SmartSchool.Customization.Data.TeacherRecord>();
            foreach ( TeacherRelated.BriefTeacherData tinfo in TeacherRelated.Teacher.Instance.Items )
            {
                lock ( tinfo )
                {
                    if ( CachePool.ContainsKey(tinfo) )
                    {
                        list.Add((TeacherRecord)CachePool[tinfo]);
                    }
                    else
                    {
                        TeacherRecord newitem = new TeacherRecord(tinfo);
                        CachePool.Add(tinfo, newitem);
                        list.Add(newitem);
                    }
                }
            }
            return list;
            }

        public SmartSchool.Customization.Data.TeacherRecord GetTeacherByIdentifiableName(string identifiableName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ICloneable 成員

        public object Clone()
        {
            return new TeacherProvider();
        }

        #endregion

    }
}
