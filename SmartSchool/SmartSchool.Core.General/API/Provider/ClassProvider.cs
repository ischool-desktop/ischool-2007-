using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.ClassRelated;
using SmartSchool.Customization.Data;

namespace SmartSchool.API.Provider
{
    internal class ClassProvider:Customization.Data.ClassInformationProvider
    {
        private System.Collections.Hashtable _CashePool;

        private AccessHelper _AccessHelper;

        #region ClassInformationProvider 成員

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

        public List<SmartSchool.Customization.Data.ClassRecord> GetClass(IEnumerable<string> identities)
        {
            List<Customization.Data.ClassRecord> list = new List<SmartSchool.Customization.Data.ClassRecord>();
            foreach (string id in identities)
            {
                ClassInfo cinfo = Class.Instance.Items[id];
                if (cinfo != null)
                {
                    lock ( cinfo )
                    {
                        if ( CachePool.ContainsKey(cinfo) )
                        {
                            list.Add((ClassRecord)CachePool[cinfo]);
                        }
                        else
                        {
                            ClassRecord newitem = new ClassRecord(cinfo, CachePool);
                            CachePool.Add(cinfo, newitem);
                            list.Add(newitem);
                        }
                    }
                }
            }
            return list;
        }

        public List<SmartSchool.Customization.Data.ClassRecord> GetSelectedClass()
        {
            List<Customization.Data.ClassRecord> list = new List<SmartSchool.Customization.Data.ClassRecord>();
            foreach (ClassInfo cinfo in Class.Instance.SelectionClasses)
            {
                lock ( cinfo )
                {
                    if ( CachePool.ContainsKey(cinfo) )
                    {
                        list.Add((ClassRecord)CachePool[cinfo]);
                    }
                    else
                    {
                        ClassRecord newitem = new ClassRecord(cinfo, CachePool);
                        CachePool.Add(cinfo, newitem);
                        list.Add(newitem);
                    }
                }
            }
            return list;
        }

        public List<SmartSchool.Customization.Data.ClassRecord> GetAllClass()
        {
            List<Customization.Data.ClassRecord> list = new List<SmartSchool.Customization.Data.ClassRecord>();
            foreach ( ClassInfo cinfo in Class.Instance.Items )
            {
                lock ( cinfo )
                {
                    if ( CachePool.ContainsKey(cinfo) )
                    {
                        list.Add((ClassRecord)CachePool[cinfo]);
                    }
                    else
                    {
                        ClassRecord newitem = new ClassRecord(cinfo, CachePool);
                        CachePool.Add(cinfo, newitem);
                        list.Add(newitem);
                    }
                }
            }
            return list;
        }

        public void FillField(string fieldName, IEnumerable<SmartSchool.Customization.Data.ClassRecord> classes)
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
                _CashePool =value;
            }
        }

        public SmartSchool.Customization.Data.ClassRecord GetClassByClassName(string className)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ICloneable 成員

        public object Clone()
        {
            return new ClassProvider();
        }

        #endregion
    }
}
