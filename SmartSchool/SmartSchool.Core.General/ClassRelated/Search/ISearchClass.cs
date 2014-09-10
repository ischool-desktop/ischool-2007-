using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ClassRelated.Search
{
    interface ISearchClass
    {
        List<ClassInfo> Search(string searchText);
    }
}
