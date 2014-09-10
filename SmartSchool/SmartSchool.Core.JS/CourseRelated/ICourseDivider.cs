using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;

namespace SmartSchool.CourseRelated
{
    interface ICourseDivider : IDenominated
    {
        TempCourseSourceProvider TempProvider { get; set; }
        DragDropTreeView TargetTreeView { get; set; }
        void Divide(Dictionary<string, CourseRec> source);
    }
}
