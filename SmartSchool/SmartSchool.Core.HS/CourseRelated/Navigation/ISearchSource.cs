using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.CourseRelated
{
    internal interface ISearchSource
    {
        CourseCollection GetCourses();

        string Title { get;}
    }
}
