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
    class NavigateByTeacher : NavigateBase
    {
        private AllCourse _root;

        public NavigateByTeacher(CourseDataSource source)
            : base(source)
        {
        }

        protected override void RefreshTree()
        {
            CourseCollection courses = _data_source.CourseList;
            _root = new AllCourse();

            foreach (CourseInformation course in courses)
                _root.AddCourse(course);

            _root.SyncTitleInformation();

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
            private ClassifyByTeacher _by_teacher;
            private TeacherUnAssign _un_assign;
            private string _name;

            public AllCourse()
            {
                Text = _name = "所有課程";
                Name = "Teacher:All";

                _by_teacher = new ClassifyByTeacher();
                _by_teacher.Expand();

                _un_assign = new TeacherUnAssign();
                Nodes.Add(_by_teacher);
            }

            public override void AddCourse(CourseInformation course)
            {
                if (course.MajorTeacherID == -1 && course.Teachers.Count <= 0)
                {
                    _un_assign.AddCourse(course);

                    if (_un_assign.CourseCount == 1) //當有一個節點時就顯示，沒有任可節點時不顯示。
                        Nodes.Add(_un_assign);
                }
                else
                {
                    _by_teacher.AddCourse(course);

                    //if (_by_teacher.CategoryCount == 1) //當有一個節點時就顯示，沒有任可節點時不顯示。
                    //    Nodes.Add(_by_teacher);
                }

                //SyncCourseCount();
            }

            public override void SyncTitleInformation()
            {
                _by_teacher.SyncTitleInformation();
                _un_assign.SyncTitleInformation();

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
                    return _un_assign.CourseCount + _by_teacher.CourseCount;
                }
            }

            #region ISearchSource 成員

            protected override CourseCollection SearchSourceGetCourses()
            {
                CourseCollection source = new CourseCollection();

                ISearchSource search = _by_teacher as ISearchSource;
                if (search != null)
                    source.AddRange(search.GetCourses());

                search = _un_assign as ISearchSource;
                if (search != null)
                    source.AddRange(search.GetCourses());

                return source;
            }

            protected override string SearchSourceTitle
            {
                get
                {
                    return "搜尋\"此學期所有課程\"";
                }
            }

            #endregion
        }
        #endregion

        #region TeacherUnAssign
        public class TeacherUnAssign : NodeBase
        {
            private CourseCollection _courses;
            private string _name;

            public TeacherUnAssign()
            {
                _name = Text = "未設教師課程";
                Name = "None"; //特殊名稱，不可改。
                //NodeFont = new Font(FontStyles.General, FontStyle.Bold | FontStyle.Italic);
                _courses = new CourseCollection();
            }

            public override void AddCourse(CourseInformation course)
            {
                _courses.Add(course);
                //SyncCourseCount();
            }

            protected override CourseCollection CourseInfoGetCourses()
            {
                return _courses;
            }

            protected override CourseCollection SearchSourceGetCourses()
            {
                return _courses;
            }

            protected override string SearchSourceTitle
            {
                get
                {
                    return "在\"未設教師課程\"中搜尋";
                }
            }

            public int CourseCount
            {
                get { return _courses.Count; }
            }

            public override void SyncTitleInformation()
            {
                SyncCourseCount();
            }

            private void SyncCourseCount()
            {
                Text = _name + "(" + _courses.Count + ")";
            }
        }

        #endregion

        #region ClassifyByTeacher
        public class ClassifyByTeacher : NodeBase
        {
            private Dictionary<string, TeacherCategoryNode> _category;
            private string _name;

            public ClassifyByTeacher()
            {
                Text = _name = "依教師檢視課程";
                _category = new Dictionary<string, TeacherCategoryNode>();

                Name = "Teacher:CategoryByTeacher";
            }

            public override void AddCourse(CourseInformation course)
            {
                foreach (CourseInformation.Teacher each in course.Teachers)
                {
                    string category = each.TeacherCategory;

                    if (_category.ContainsKey(category))
                    {
                        TeacherCategoryNode tc = _category[category];
                        tc.SetCurrentSequence(each.Sequence);
                        tc.AddCourse(course);
                    }
                    else
                    {
                        TeacherCategoryNode newNode = new TeacherCategoryNode(category);
                        _category.Add(category, newNode); //記錄到全域 Collection 中。
                        newNode.SetCurrentSequence(each.Sequence);
                        newNode.AddCourse(course); //將新課程加入到新子節點中。
                        Nodes.Add(newNode); //Attach Tree 節點。
                    }
                }

                //SyncCourseCount();
            }

            public override void SyncTitleInformation()
            {
                foreach (TeacherCategoryNode each in _category.Values)
                    each.SyncTitleInformation();

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
                    CourseCollection total = new CourseCollection();
                    foreach (TeacherCategoryNode each in _category.Values)
                    {
                        foreach (TreeNode each2 in each.Nodes)
                        {
                            TeacherNode tnode = each2 as TeacherNode;

                            if (tnode == null) continue;

                            foreach (CourseInformation course in tnode.Courses)
                            {
                                if (total.Contains(course)) continue;

                                total.Add(course);
                            }
                        }
                    }

                    return total.Count;
                }
            }

            public int CategoryCount
            {
                get { return _category.Count; }
            }

            #region ISearchSource 成員

            protected override CourseCollection SearchSourceGetCourses()
            {
                CourseCollection source = new CourseCollection();
                foreach (TeacherCategoryNode each in _category.Values)
                {
                    ISearchSource container = each as ISearchSource;

                    if (container != null)
                        source.AddRange(container.GetCourses());
                }

                return source;
            }

            protected override string SearchSourceTitle
            {
                get
                {
                    return "在\"依教師檢視課程\"中搜尋";
                }
            }

            #endregion
        }
        #endregion

        #region TeacherCategoryNode

        public class TeacherCategoryNode : NodeBase
        {
            private Dictionary<int, TeacherNode> _teacher;
            private string _name;

            public TeacherCategoryNode(string name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    _name = "未設類別教師";
                    Name = "None";
                    //NodeFont = new Font(FontStyles.General, FontStyle.Bold | FontStyle.Italic);
                }
                else
                {
                    _name = name;
                    Name = "Teacher:" + name;
                }

                Text = _name;

                _teacher = new Dictionary<int, TeacherNode>();
            }

            private int _current_sequence = 1;

            /// <summary>
            /// 設定新增課程時，要取得課程中的哪位教師處理。
            /// </summary>
            internal void SetCurrentSequence(int sequence)
            {
                _current_sequence = sequence;
            }

            public override void AddCourse(CourseInformation course)
            {
                CourseInformation.Teacher current = null;
                foreach (CourseInformation.Teacher each in course.Teachers)
                {
                    if (each.Sequence == _current_sequence)
                        current = each;
                }

                int teacherid = current.TeacherID;
                if (_teacher.ContainsKey(teacherid))
                    _teacher[teacherid].AddCourse(course);
                else
                {
                    TeacherNode newNode = new TeacherNode(teacherid, current.TeacherName);
                    Nodes.Add(newNode);
                    _teacher.Add(teacherid, newNode);
                    _teacher[teacherid].AddCourse(course);
                }

                //SyncCourseCount();
            }

            public override void SyncTitleInformation()
            {
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
                    int total = 0;
                    foreach (TeacherNode each in _teacher.Values)
                        total += each.Courses.Count;

                    return total;
                }
            }

            #region ISearchSource 成員

            protected override CourseCollection SearchSourceGetCourses()
            {
                CourseCollection source = new CourseCollection();
                foreach (TeacherNode each in _teacher.Values)
                {
                    ISearchSource container = each as ISearchSource;

                    if (container != null)
                        source.AddRange(container.GetCourses());
                }

                return source;
            }

            protected override string SearchSourceTitle
            {
                get { return "在\"" + _name + "\"中搜尋"; }
            }

            #endregion
        }

        #endregion

        #region TeacherNode
        public class TeacherNode : NodeBase
        {
            private string _name;
            private CourseCollection _courses;

            public TeacherNode(int teacherid, string name)
            {
                //-1 代表未分班級。
                if (teacherid == -1)
                {
                    _name = "未設定教師";
                    Name = "None"; //特殊名稱，不可改。
                    //NodeFont = new Font(FontStyles.General, FontStyle.Bold | FontStyle.Italic);
                }
                else
                {
                    _name = name;
                    Name = "Teacher:" + teacherid;
                }

                _courses = new CourseCollection();

                //SyncCourseCount();
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

            public override void SyncTitleInformation()
            {
                SyncCourseCount();
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
                get
                {
                    return "在\"" + _name + "\"中搜尋";
                }
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

