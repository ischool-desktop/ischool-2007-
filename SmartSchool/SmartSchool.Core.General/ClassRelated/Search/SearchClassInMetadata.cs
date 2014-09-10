using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartSchool.ClassRelated.Search
{
    class SearchClassInMetadata:ISearchClass
    {
        private List<ClassInfo> _Source;
        public List<ClassInfo> Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        #region ISearchClass 成員

        public List<ClassInfo> Search(string searchText)
        {
            Regex rx = new Regex(
                searchText.Replace("*", ".*").Replace(@"\", @"\\").Replace(@"[", @"\[").Replace(@"(", @"\(").Replace(@"]", @"\]").Replace(@")", @"\)").Replace(@"?", @"\?").Replace(@"+", @"\+") 
                );
            List<ClassInfo> list = new List<ClassInfo>();
            foreach (ClassInfo var in _Source)
            {
                if (rx.IsMatch(var.ClassName))
                {
                    list.Add(var);
                }
            }
            return list;
        }

        #endregion
    }
}
