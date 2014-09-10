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
    class NavigateByClass : NavigateBase
    {
        private AllCourse _root;

        public NavigateByClass(CourseDataSource source)
            : base(source)
        {
        }

        protected override void RefreshTree()
        {
            List<CourseInformation> courses = _data_source.CourseList;
            _root = new AllCourse();

            foreach (CourseInformation course in courses)
                _root.AddCourse(course);

            _tree_control.Nodes.Clear();
            _tree_control.Nodes.Add(_root);

            //先排序 TreeView，在將「待處理」節點放到最後。
            _tree_control.TreeViewNodeSorter = new NodeSorter();

            MoveNoneToLast(_root); //將所有「未分類」類型的節點都移到最後。

            _tree_control.Nodes.Add(_temporal_handler.GetTreeNode());
            _root.Expand();

            _need_init = false;
        }

        #region AllCourse
        public class AllCourse : NodeBase
        {
            private ClassifyByClass _by_class;
            private UnAssignClass _un_class;
            private string _name;

            public AllCourse()
            {
                Text = _name = "所有課程";
                Name = "Class:All";

                _by_class = new ClassifyByClass();
                _by_class.Expand();

                Nodes.Add(_by_class);

                _un_class = new UnAssignClass();
            }

            public override void AddCourse(CourseInformation course)
            {
                if (course.ClassID == -1)
                {
                    _un_class.AddCourse(course);

                    //當有一個節點時就顯示。
                    if (_un_class.CourseCount == 1)
                        Nodes.Add(_un_class);
                }
                else
                {
                    _by_class.AddCourse(course);

                    //if (_by_class.GradeYearCount == 1)
                    //    Nodes.Add(_by_class);
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
                    return _by_class.CourseCount + _un_class.CourseCount;
                }
            }

            #region ISearchSource 成員

            protected override CourseCollection SearchSourceGetCourses()
            {
                CourseCollection source = new CourseCollection();

                ISearchSource search = _by_class as ISearchSource;
                if (search != null)
                    source.AddRange(search.GetCourses());

                search = _un_class as ISearchSource;
                if (search != null)
                    source.AddRange(search.GetCourses());

                return source;
            }

            protected override string SearchSourceTitle
            {
                get
                {
                    return "搜尋<b>此學期所有課程</b>";
                }
            }

            #endregion
        }
        #endregion

        #region ClassifyByClass
        public class ClassifyByClass : NodeBase
        {
            private Dictionary<int, GradeYearNode> _grade_years;
            private string _name;

            public ClassifyByClass()
            {
                Text = _name = "依班級檢視課程";
                _grade_years = new Dictionary<int, GradeYearNode>();

                Name = "Class:CategoryByClass";
            }

            public override void AddCourse(CourseInformation course)
            {
                int grade_year = course.ClassGradeYear;
                if (_grade_years.ContainsKey(grade_year))
                    _grade_years[grade_year].AddCourse(course);
                else
                {
                    GradeYearNode newNode = new GradeYearNode(grade_year);
                    Nodes.Add(newNode);
                    _grade_years.Add(grade_year, newNode);
                    _grade_years[grade_year].AddCourse(course);
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
                    foreach (GradeYearNode each in _grade_years.Values)
                        totalCount += each.CourseCount;

                    return totalCount;
                }
            }

            public int GradeYearCount
            {
                get { return _grade_years.Count; }
            }

            #region ISearchSource 成員

            protected override CourseCollection SearchSourceGetCourses()
            {
                CourseCollection source = new CourseCollection();
                foreach (GradeYearNode each in _grade_years.Values)
                {
                    ISearchSource container = each as ISearchSource;

                    if (container != null)
                        source.AddRange(container.GetCourses());
                }

                return source;

            }

            protected override string SearchSourceTitle
            {
                get { return "在\"依班級檢視課程\"中搜尋"; }
            }

            #endregion
        }
        #endregion

        #region UnAssignClass
        public class UnAssignClass : NodeBase
        {
            private CourseCollection _courses;
            private string _name;

            public UnAssignClass()
            {
                Text = _name = "未設班級課程";
                Name = "None";
                //NodeFont = new Font(FontStyles.General, FontStyle.Bold | FontStyle.Italic);

                _courses = new CourseCollection();

                SyncCourseCount();
            }

            public override void AddCourse(CourseInformation course)
            {
                _courses.Add(course);
                SyncCourseCount();
            }

            public int CourseCount
            {
                get { return _courses.Count; }
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
                get { return "在\"未設班級課程\""; }
            }

            #endregion
        }
        #endregion

        #region GradeYearNode
        public class GradeYearNode : NodeBase
        {
            private string _name;
            private int _grade_year;
            private Dictionary<int, ClassNode> _classes;

            public GradeYearNode(int gradeYear)
            {
                _grade_year = gradeYear;

                //-1 代表未分班級。
                if (gradeYear == -1)
                {
                    _name = "未分年級的班級";
                    Name = "None"; //這是特殊名字，不可以改。
                    //NodeFont = new Font(FontStyles.General, FontStyle.Bold | FontStyle.Italic);
                }
                else
                {
                    _name = GetDisplayName();
                    Name = "GradeYear:" + gradeYear;
                }

                Text = _name;

                _classes = new Dictionary<int, ClassNode>();
            }

            private string GetDisplayName()
            {
                return _grade_year + "年級";
            }

            public override void AddCourse(CourseInformation course)
            {
                int classid = course.ClassID;
                if (_classes.ContainsKey(classid))
                    _classes[classid].AddCourse(course);
                else
                {
                    ClassNode newNode = new ClassNode(classid, course.ClassName);
                    _classes.Add(classid, newNode);
                    newNode.AddCourse(course);
                    Nodes.Add(newNode);
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
                    foreach (ClassNode each in _classes.Values)
                        totalCount += each.Courses.Count;

                    return totalCount;
                }
            }

            #region ISearchSource 成員

            protected override string SearchSourceTitle
            {
                get { return "在\"" + _name + "\"中搜尋"; }
            }

            protected override CourseCollection SearchSourceGetCourses()
            {
                CourseCollection source = new CourseCollection();
                foreach (ClassNode each in _classes.Values)
                {
                    ISearchSource search = each as ISearchSource;
                    if (search != null)
                        source.AddRange(search.GetCourses());
                }

                return source;
            }

            #endregion
        }
        #endregion

        #region ClassNode
        public class ClassNode : NodeBase
        {
            private string _name;
            private CourseCollection _courses;

            public ClassNode(int classid, string name)
            {
                //-1 代表未分班級。
                if (classid == -1)
                {
                    _name = "未設定班級";
                    Name = "None"; //這是特殊名字，不可以改。
                    //NodeFont = new Font(FontStyles.General, FontStyle.Bold | FontStyle.Italic);
                }
                else
                {
                    _name = name;
                    Name = "Class:" + classid;
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
                TreeNode nx = x as TreeNode;
                TreeNode ny = y as TreeNode;

                if (x.GetType() == y.GetType()) //兩個 Type 一樣才比大小。
                {
                    if (nx.Name == "None")
                        return 1;
                    else if (ny.Name == "None")
                        return -1;

                    return string.Compare(nx.Text, ny.Text);
                }
                else
                {
                    if (nx.Text == "None")
                        return 1;
                    else if (ny.Text == "None")
                        return -1;

                    return 0;
                }
            }
        }
        #endregion
    }
}
