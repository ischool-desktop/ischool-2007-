using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartSchool.TeacherRelated.Search
{
    class SearchTeacherInMetadata : ISearchTeacher
    {
        private List<BriefTeacherData> _Source;
        public List<BriefTeacherData> Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        #region ISearchTeacher 成員

        public List<BriefTeacherData> Search(string searchText)
        {
            Regex rx = new Regex(
                searchText.Replace("*", ".*").Replace(@"\", @"\\").Replace(@"[", @"\[").Replace(@"(", @"\(").Replace(@"]", @"\]").Replace(@")", @"\)").Replace(@"?", @"\?").Replace(@"+", @"\+") 
                );
            List<BriefTeacherData> list = new List<BriefTeacherData>();
            foreach (BriefTeacherData var in _Source)
            {
                if (rx.IsMatch(var.TeacherName))
                {
                    list.Add(var);
                }
            }
            return list;
        }

        #endregion
    }
}
