using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;

namespace SmartSchool.TeacherRelated
{
    internal class TeacherCollection : ReadOnlyCollection<string, BriefTeacherData>
    {
        internal TeacherCollection(Dictionary<string, BriefTeacherData> items) : base(items) { }
    }
}