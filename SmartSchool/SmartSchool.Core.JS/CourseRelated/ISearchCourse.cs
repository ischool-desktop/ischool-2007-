using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.CourseRelated
{
    interface ISearchCourse
    {
        //bool SearchInCourseName
        //{
        //    get;
        //    set;
        //}

        //bool SearchInTeacher
        //{
        //    get;
        //    set;
        //}

        //bool SearchInRefClass
        //{
        //    get;
        //    set;
        //}

        List<CourseRec> Search(string key);
    }
}
