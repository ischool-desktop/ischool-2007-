using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Drawing;

namespace SmartSchool.CourseRelated
{
    internal class TemporalHandler
    {
        private CourseCollection _temporal_course;

        public TemporalHandler()
        {
            _temporal_course = new CourseCollection();

            CourseEntity.Instance.CourseChanged += new EventHandler<CourseChangeEventArgs>(Instance_CourseChanged);
        }

        private void Instance_CourseChanged(object sender, CourseChangeEventArgs e)
        {
            CourseDictionary courses = CourseEntity.Instance.Items;

            for (int i = 0; i < _temporal_course.Count; i++)
            {
                CourseInformation course = _temporal_course[i];

                if (courses.ContainsKey(course.Identity.ToString()))//如果在課程集合中存在。
                    _temporal_course[i] = courses[course.Identity.ToString()]; //把 Temporal  中的換掉。
            }

            if (ContentChanged != null)
                ContentChanged(this, EventArgs.Empty);
        }

        public void AddCourse(CourseInformation course)
        {
            //課程不有在才新增。
            if (!_temporal_course.Contains(course) && course != null)
            {
                _temporal_course.Add(course);

                if (ContentChanged != null)
                    ContentChanged(this, EventArgs.Empty);
            }
        }

        public void RemoveCourse(CourseInformation course)
        {
            //課程在才移除。
            if (_temporal_course.Contains(course) && course != null)
            {
                bool remoeSuccess = _temporal_course.Remove(course);

                if (ContentChanged != null && remoeSuccess)
                    ContentChanged(this, EventArgs.Empty);
            }
        }

        public TemporalTreeNode GetTreeNode()
        {
            return new TemporalTreeNode(_temporal_course, this);
        }

        public CourseCollection Courses
        {
            get { return _temporal_course; }
        }

        public event EventHandler ContentChanged;
    }

    internal class TemporalTreeNode : TreeNode, ICourseInfoContainer, ISearchSource
    {
        private CourseCollection _temporal_course;
        private TemporalHandler _handler;

        public TemporalTreeNode(CourseCollection courses, TemporalHandler handler)
        {
            Name = "Temporal";
            //NodeFont = new Font(FontStyles.General, FontStyle.Bold | FontStyle.Italic);

            _temporal_course = courses;
            _handler = handler;
            handler.ContentChanged += new EventHandler(Handler_ContentChanged);

            SyncTitle();

        }

        private void SyncTitle()
        {
            Text = "待處理課程(" + _temporal_course.Count.ToString() + ")";
        }

        private void Handler_ContentChanged(object sender, EventArgs e)
        {
            TemporalHandler handler = sender as TemporalHandler;
            _temporal_course = handler.Courses;
            SyncTitle();
        }

        public TemporalHandler Handler
        {
            get { return _handler; }
        }

        #region ISearchSource 成員

        CourseCollection ISearchSource.GetCourses()
        {
            return _temporal_course;
        }

        string ISearchSource.Title
        {
            get { return "搜尋待處理課程"; }
        }

        #endregion

        #region ICourseInfoContainer 成員

        CourseCollection ICourseInfoContainer.GetCourses()
        {
            return _temporal_course;
        }

        public void SyncTitleInformation()
        {
            SyncTitle();
        }

        #endregion
    }
}
