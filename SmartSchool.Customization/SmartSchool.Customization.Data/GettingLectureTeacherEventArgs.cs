using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class GettingLectureTeacherEventArgs:EventArgs
    {
        private AccessHelper _AccessHelper;
        public AccessHelper AccessHelper
        {
            get
            {
                return _AccessHelper;
            }
        }

        private CourseRecord _Course;
        public CourseRecord Course
        {
            get { return _Course; }
        }

        private List<TeacherRecord> _Teachers=new List<TeacherRecord>();
        public List<TeacherRecord> Teachers
        {
            get { return _Teachers; }
            set { _Teachers = value; }
        }

        public GettingLectureTeacherEventArgs(AccessHelper accessHelper,CourseRecord course)
        {
            _AccessHelper = accessHelper;
            _Course = course;
        }
    }
}
