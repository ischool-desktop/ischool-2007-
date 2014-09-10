using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.CourseRelated.Navigation.Nodes
{
    internal abstract class NodeBase : TreeNode, ICourseInfoContainer, ISearchSource
    {
        public abstract void AddCourse(CourseInformation course);

        #region ICourseInfoContainer 成員

        CourseCollection ICourseInfoContainer.GetCourses()
        {
            return CourseInfoGetCourses();
        }
        
        public virtual void SyncTitleInformation()
        {
        }

        protected virtual CourseCollection CourseInfoGetCourses()
        {
            return new CourseCollection();
        }

        #endregion

        #region ISearchSource 成員

        CourseCollection ISearchSource.GetCourses()
        {
            return SearchSourceGetCourses();
        }

        protected virtual CourseCollection SearchSourceGetCourses()
        {
            return new CourseCollection();
        }

        string ISearchSource.Title
        {
            get { return SearchSourceTitle; }
        }

        protected virtual string SearchSourceTitle
        {
            get { return string.Empty; }
        }
        #endregion
    }
}
