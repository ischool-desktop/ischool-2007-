using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.TeacherRelated.Search
{
    interface ISearchTeacher
    {
        List<BriefTeacherData> Search(string searchText);
    }
}
