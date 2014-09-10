using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.StudentRelated.Search
{
    internal interface  ISearchStudent
    {

        bool SearchInName
        {
            get;
            set;
        }

        bool SearchInStudentID
        {
            get;
            set;
        }

        bool SearchInSSN
        {
            get;
            set;
        }

        List<BriefStudentData> Search(string key, int pageSize, int startPage);
    }
}
