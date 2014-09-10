using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.CourseRelated
{
    public class CourseInsertEventArgs : EventArgs
    {
        private int _newCourseID;

        public CourseInsertEventArgs(int newCourseID)
        {
            _newCourseID = newCourseID;
        }

        public int NewCourseID
        {
            get { return _newCourseID; }
        }
    }

    public class CourseDeleteEventArgs : EventArgs
    {
        private int _course_id;

        public CourseDeleteEventArgs(int course)
        {
            _course_id = course;
        }

        public int CourseID
        {
            get { return _course_id; }
        }
    }

    public class CourseChangeEventArgs : EventArgs
    {
        private List<int> _courses;

        public CourseChangeEventArgs(List<int> courses)
        {
            _courses = courses;
        }

        public List<int> CoursesIdCollection
        {
            get { return _courses; }
        }
    }
}
