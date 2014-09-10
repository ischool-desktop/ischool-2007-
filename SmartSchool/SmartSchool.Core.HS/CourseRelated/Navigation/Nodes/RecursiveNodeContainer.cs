using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.CourseRelated.Navigation.Nodes;
using SmartSchool.CourseRelated;
using System.Windows.Forms;

namespace SmartSchool.CourseRelated.Navigation.Nodes
{
    internal class RecursiveNodeContainer : NodeBase
    {
        private CourseCollection _courses;
        private string _search_title;

        public RecursiveNodeContainer(ICourseInfoContainer container)
        {
            _courses = new CourseCollection();
            AddCourses(container);

            if (container is ISearchSource)
                _search_title = (container as ISearchSource).Title;
        }

        protected override string SearchSourceTitle
        {
            get
            {
                return _search_title;
            }
        }

        public override void AddCourse(SmartSchool.CourseRelated.CourseInformation course)
        {
        }

        protected override CourseCollection SearchSourceGetCourses()
        {
            return _courses;
        }

        protected override CourseCollection CourseInfoGetCourses()
        {
            return _courses;
        }

        private void AddCourses(ICourseInfoContainer container)
        {
            if (container != null)
                _courses.AddRange(container.GetCourses());

            TreeNode node = container as TreeNode;
            if (node != null && node.Nodes.Count > 0)
            {
                foreach (TreeNode each in node.Nodes)
                {
                    ICourseInfoContainer childNode = each as ICourseInfoContainer;
                    if (childNode != null)
                        AddCourses(childNode);
                }
            }
        }
    }
}
