using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartSchool.StudentRelated.Search
{
    internal  class SearchStudentInMetadata : ISearchStudent
    {
        bool _SearchInName, _SearchInStudentID, _SearchInSSN;
        List<BriefStudentData> _Source;
        public List<BriefStudentData> Source
        {
            get { return _Source; }
            set { _Source = value; }
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

        List<BriefStudentData> ISearchStudent.Search(string key, int pageSize, int startPage)
        {
            Regex rx = new Regex(
                key.Replace("*", ".*").Replace(@"\", @"\\").Replace(@"[", @"\[").Replace(@"(", @"\(").Replace(@"]", @"\]").Replace(@")", @"\)").Replace(@"?", @"\?").Replace(@"+", @"\+") 
                );
            List<BriefStudentData> list = new List<BriefStudentData>();
            int matchCount = 0;
            int maxCount = pageSize * startPage;
            int minCount = pageSize * (startPage - 1);
            foreach (BriefStudentData var in _Source)
            {
                if (matchCount == maxCount) break;
                if (_SearchInName && rx.IsMatch(var.Name))
                {
                    matchCount++;
                    if (matchCount > minCount)
                        list.Add(var);
                    continue;
                }
                if (_SearchInStudentID && rx.IsMatch(var.StudentNumber))
                {
                    matchCount++;
                    if (matchCount > minCount)
                        list.Add(var);
                    continue;
                }
                if (_SearchInSSN && rx.IsMatch(var.IDNumber))
                {
                    matchCount++;
                    if (matchCount > minCount)
                        list.Add(var);
                    continue;
                }
            }
            return list;
        }

        #endregion
    }
}
