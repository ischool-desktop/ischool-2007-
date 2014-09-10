using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SmartSchool.Common;
using SmartSchool.CourseRelated.Navigation.Nodes;
using System.Collections;

namespace SmartSchool.CourseRelated
{
    class NavigateBySubject : NavigateBase
    {
        private ClassifyBySubject _root;

        public NavigateBySubject(CourseDataSource source)
            : base(source)
        {
        }

        protected override void RefreshTree()
        {
            List<CourseInformation> courses = _data_source.CourseList;
            _root = new ClassifyBySubject();

            foreach (CourseInformation course in courses)
                _root.AddCourse(course);

            MoveNoneToLast(_root); //將未分類節點移到最後一個位置。

            _tree_control.Nodes.Clear();
            _tree_control.Nodes.Add(_root);

            //先排序 TreeView，在將「待處理」節點放到最後。
            _tree_control.TreeViewNodeSorter = new NodeSorter();

            _tree_control.Nodes.Add(_temporal_handler.GetTreeNode());
            _root.Expand();

            _need_init = false;
        }

        #region ClassifyBySubject

        public class ClassifyBySubject : NodeBase
        {
            private Dictionary<string, SubjectNode> _subjects;
            private string _name;

            public ClassifyBySubject()
            {
                _subjects = new Dictionary<string, SubjectNode>();

                Text = _name = "所有課程";
                Name = "Subject:All";
            }

            public override void AddCourse(CourseInformation course)
            {
                string subject = course.Subject;
                if (_subjects.ContainsKey(subject))
                    _subjects[subject].AddCourse(course);
                else
                {
                    SubjectNode newNode = new SubjectNode(subject);
                    Nodes.Add(newNode);
                    _subjects.Add(subject, newNode);
                    _subjects[subject].AddCourse(course);
                }

                SyncCourseCount();
            }

            private void SyncCourseCount()
            {
                Text = _name + "(" + CourseCount + ")";
            }

            public int CourseCount
            {
                get
                {
                    int totalCount = 0;

                    foreach (SubjectNode each in _subjects.Values)
                        totalCount += each.Courses.Count;

                    return totalCount;
                }
            }

            protected override CourseCollection SearchSourceGetCourses()
            {
                CourseCollection source = new CourseCollection();
                foreach (SubjectNode each in _subjects.Values)
                {
                    ICourseInfoContainer container = each as ICourseInfoContainer;
                    source.AddRange(container.GetCourses());
                }
                return source;
            }

            protected override string SearchSourceTitle
            {
                get
                {
                    return "搜尋本學期所有課程";
                }
            }
        }

        #endregion

        #region SubjectNode
        public class SubjectNode : NodeBase
        {
            private string _name;
            private CourseCollection _courses;

            public SubjectNode(string name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    _name = "未設定科目";
                    Name = "None"; //這是特殊名字，不可以改。
                    //NodeFont = new Font(FontStyles.General, FontStyle.Bold | FontStyle.Italic);
                }
                else
                {
                    _name = name;
                    Name = "Subject:" + name;
                }

                _courses = new CourseCollection();

                SyncCourseCount();
            }

            public override void AddCourse(CourseInformation course)
            {
                _courses.Add(course);
                SyncCourseCount();
            }

            public CourseCollection Courses
            {
                get { return _courses; }
            }

            public string SubjectName
            {
                get { return _name; }
            }

            private void SyncCourseCount()
            {
                Text = _name + "(" + _courses.Count + ")";
            }

            #region ICourseInfoContainer 成員

            protected override CourseCollection CourseInfoGetCourses()
            {
                return _courses;
            }

            #endregion

            #region ISearchSource 成員

            protected override CourseCollection SearchSourceGetCourses()
            {
                return _courses;
            }

            protected override string SearchSourceTitle
            {
                get { return "在\"" + _name + "\"中搜尋"; }
            }

            #endregion
        }

        #endregion

        #region NodeSorter 排序 SubjectNode 節點。
        private class NodeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x.GetType() == y.GetType()) //兩個 Type 一樣才比大小。
                {
                    if (x.GetType() == typeof(SubjectNode))
                    {
                        SubjectNode nx = x as SubjectNode;
                        SubjectNode ny = y as SubjectNode;

                        return string.Compare(nx.SubjectName, ny.SubjectName);
                    }
                    else
                        return 0;
                }
                else //Type 不同就當作相等。
                    return 0;
            }
        }
        #endregion
    }
}