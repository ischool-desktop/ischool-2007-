using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.CourseRelated
{
    public class CourseCollection : List<CourseInformation>
    {
        public CourseCollection()
            : base()
        {
        }

        public CourseCollection(IEnumerable<CourseInformation> collection)
            : base(collection)
        {
        }
    }
}
