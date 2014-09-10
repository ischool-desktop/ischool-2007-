using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature;

namespace SmartSchool.StudentRelated.Search
{
    internal class SearchStudentInDatabase:ISearchStudent
    {
        bool _SearchInName, _SearchInStudentID, _SearchInSSN;
        List<string> _SearchInStatus=new List<string>();

        public List<string> SearchInStatus
        {
            get { return _SearchInStatus; }
        }

        #region ISearchStudent 成員

        public bool SearchInName
        {
            get
            {
                return _SearchInName;
            }
            set
            {
                _SearchInName = value;
            }
        }

        public bool SearchInStudentID
        {
            get
            {
                return _SearchInStudentID;
            }
            set
            {
                _SearchInStudentID = value;
            }
        }

        public bool SearchInSSN
        {
            get
            {
                return _SearchInSSN;
            }
            set
            {
                _SearchInSSN = value;
            }
        }

        public List<BriefStudentData> Search(string key, int pageSize, int startPage)
        {
            
            DSResponse dsresp = QueryStudent.SearchStudent(key,pageSize,startPage,_SearchInStudentID, _SearchInSSN, _SearchInName,_SearchInStatus.ToArray());

            //DSResponse dsresp = Feature.Basic.Student.SearchStudent(new IntelliSchool.DSA30.Util.DSRequest(req));
            List<BriefStudentData> list = new List<BriefStudentData>();
            if (_SearchInStudentID || _SearchInSSN || _SearchInName)
            {
                foreach (XmlNode var in dsresp.GetContent().BaseElement.SelectNodes("Student"))
                {
                    list.Add(new BriefStudentData((XmlElement)var));
                }
            }
            return list;
        }

        #endregion
    }
}
