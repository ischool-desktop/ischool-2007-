using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.CourseRelated
{
    internal interface ICourseInfoContainer
    {
        CourseCollection GetCourses();

        void SyncTitleInformation();
    }
}
