using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.TeacherRelated.SourceProvider;
using SmartSchool.Common;
using System.Windows.Forms;
using SmartSchool.ClassRelated;

namespace SmartSchool.TeacherRelated.Divider
{
    class ClassDivider : ITeacherDivider
    {
        DragDropTreeView _TargetTreeView;
        private TempTeacherSourceProvider _TempProvider;
        private TreeNode _SelectedNode;

        private NormalStatusTeacherSourceProvider _NormalStatusTeacherSourceProvider;
        private SupervisedBySourceProvider _SupervisedBySourceProvider;
        private Dictionary<string, SupervisedByGradeSourceProvider> _SupervisedByGradeSourceProviders = new Dictionary<string, SupervisedByGradeSourceProvider>();
        private AllTeacherSourceProvider _AllTeacherSourceProvider = new AllTeacherSourceProvider();
        private DeleteStatusTeacherSourceProvider _DeleteStatusTeacherSourceProvider;

        #region ITeacherDivider 成員

        public TempTeacherSourceProvider TempProvider
        {
            get { return _TempProvider; }
            set { _TempProvider = value; }
        }

        public DragDropTreeView TargetTreeView
        {
            get { return _TargetTreeView; }
            set
            {
                if(_TargetTreeView!=null)
                    _TargetTreeView.AfterSelect -= new TreeViewEventHandler(_TargetTreeView_AfterSelect);
                _TargetTreeView = value;
                _TargetTreeView.AfterSelect += new TreeViewEventHandler(_TargetTreeView_AfterSelect);
            }
        }

        void _TargetTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_TargetTreeView.Tag.ToString() == this.ToString())
                _SelectedNode = _TargetTreeView.SelectedNode;
        }

        public void Divide(Dictionary<string, BriefTeacherData> source)
        {
            #region 建立Source
            //所有教師
            List<BriefTeacherData> normalStatusTeacherSource = new List<BriefTeacherData>();
            //所有班導師
            List<BriefTeacherData> supervisedBySource = new List<BriefTeacherData>();
            //各年級班導師
            Dictionary<string, List<BriefTeacherData>> supervisedByGradeSource = new Dictionary<string, List<BriefTeacherData>>();
            //所有分類教師
            List<BriefTeacherData> allCategorySource = new List<BriefTeacherData>();
            //未分類教師
            List<BriefTeacherData> nonCategorySource = new List<BriefTeacherData>();
            //各分類教師
            Dictionary<string, List<BriefTeacherData>> teacherCategorySource = new Dictionary<string, List<BriefTeacherData>>();
            //刪除教師
            List<BriefTeacherData> deletedTeacherSource = new List<BriefTeacherData>();
            #endregion

            #region 建立SourceProvider
            //所有教師
            _NormalStatusTeacherSourceProvider = (_NormalStatusTeacherSourceProvider != null ? _NormalStatusTeacherSourceProvider : new NormalStatusTeacherSourceProvider());
            
            //所有班導師
            _SupervisedBySourceProvider = (_SupervisedBySourceProvider != null ? _SupervisedBySourceProvider : new SupervisedBySourceProvider());
            
            //各年級班導師
            Dictionary<string, SupervisedByGradeSourceProvider> oldSupervisedByGradeSourceProviders = _SupervisedByGradeSourceProviders;
            _SupervisedByGradeSourceProviders = new Dictionary<string, SupervisedByGradeSourceProvider>();

            //刪除教師
            _DeleteStatusTeacherSourceProvider = (_DeleteStatusTeacherSourceProvider != null ? _DeleteStatusTeacherSourceProvider : new DeleteStatusTeacherSourceProvider());
            #endregion

            #region 將資料填入Source
            foreach (BriefTeacherData teacher in source.Values)
            {
                //狀態為一般
                if (teacher.Status == "一般")
                {
                    normalStatusTeacherSource.Add(teacher);
                    allCategorySource.Add(teacher);
                    //是班導師時
                    if (Class.Instance.GetSupervise(teacher.ID).Count > 0)
                    {
                        supervisedBySource.Add(teacher);
                        foreach (ClassInfo var in Class.Instance.GetSupervise(teacher.ID))
                        {
                            if (!supervisedByGradeSource.ContainsKey(var.GradeYear))
                                supervisedByGradeSource.Add(var.GradeYear, new List<BriefTeacherData>());
                            if (!supervisedByGradeSource[var.GradeYear].Contains(teacher))
                                supervisedByGradeSource[var.GradeYear].Add(teacher);
                        }
                    }
                    //if (teacher.SupervisedByClassInfo.Count > 0)
                    //{
                    //    //加入班導師中
                    //    supervisedBySource.Add(teacher);
                    //    //加入至各年級班導師中
                    //    foreach (SupervisedByClassInfo classInfo in teacher.SupervisedByClassInfo)
                    //    {
                    //        //該年級不存在則新增
                    //        if (!supervisedByGradeSource.ContainsKey(classInfo.SupervisedByGradeYear))
                    //            supervisedByGradeSource.Add(classInfo.SupervisedByGradeYear, new List<BriefTeacherData>());
                    //        //同年級同老師只需一個
                    //        if (!supervisedByGradeSource[classInfo.SupervisedByGradeYear].Contains(teacher))
                    //            supervisedByGradeSource[classInfo.SupervisedByGradeYear].Add(teacher);
                    //    }
                    //}
                    //依照分類填入教師分類
                    if (teacher.Category != "")
                    {
                        //該分類不存在則新增
                        if (!teacherCategorySource.ContainsKey(teacher.Category))
                            teacherCategorySource.Add(teacher.Category, new List<BriefTeacherData>());
                        teacherCategorySource[teacher.Category].Add(teacher);
                    }
                    else
                    {
                        nonCategorySource.Add(teacher);
                    }
                }
                if (teacher.Status == "刪除")
                {
                    deletedTeacherSource.Add(teacher);
                }
            }
            #endregion
            #region 將Source填入SourceProvider
            //所有教師
            _NormalStatusTeacherSourceProvider.Source = normalStatusTeacherSource;
            //所有班導師
            _SupervisedBySourceProvider.Source = supervisedBySource;
            //各年級班導師
            foreach (string grade in supervisedByGradeSource.Keys)
            {
                //如果該年級在舊資料中存在就抓過來，否則建一個新的
                if (oldSupervisedByGradeSourceProviders.ContainsKey(grade))
                {
                    _SupervisedByGradeSourceProviders.Add(grade, oldSupervisedByGradeSourceProviders[grade]);
                    //從舊清單移除，沒被移除就是要從畫面中山掉的
                    oldSupervisedByGradeSourceProviders.Remove(grade);
                }
                else
                {
                    SupervisedByGradeSourceProvider newGradeSourceProvider = new SupervisedByGradeSourceProvider();
                    newGradeSourceProvider.Grade = (grade == "" ? "未分" : grade);
                    _SupervisedByGradeSourceProviders.Add(grade, newGradeSourceProvider);
                }
                _SupervisedByGradeSourceProviders[grade].Source = supervisedByGradeSource[grade];
            }

            //刪除教師
            _DeleteStatusTeacherSourceProvider.Source = deletedTeacherSource;
            #endregion
            #region 將SourceProvider放入TreeView
            ////如果所有教師節點不存在則新增
            //if (!_TreeViewTeacher.Nodes.Contains(_NormalStatusTeacherSourceProvider))
            //    _TreeViewTeacher.Nodes.Add(_NormalStatusTeacherSourceProvider);
            //各年級班導師節點
            foreach (SupervisedByGradeSourceProvider var in _SupervisedByGradeSourceProviders.Values)
            {
                if (!_SupervisedBySourceProvider.Nodes.Contains(var))
                {
                    int gradeYear = 0;
                    if (!int.TryParse(var.Grade, out gradeYear))
                        gradeYear = int.MaxValue;
                    //照年級排序，尋找插入點
                    int index = 0;
                    for (index = 0; index < _SupervisedBySourceProvider.Nodes.Count; index++)
                    {
                        int gradeYear2 = 0;
                        if (!int.TryParse(((SupervisedByGradeSourceProvider)_SupervisedBySourceProvider.Nodes[index]).Grade, out gradeYear2))
                            gradeYear2 = int.MaxValue;
                        if (gradeYear < gradeYear2)
                            break;
                    }
                    _SupervisedBySourceProvider.Nodes.Insert(index, var);
                }
            }
            //刪除已不需要的班導師節點
            foreach (SupervisedByGradeSourceProvider var in oldSupervisedByGradeSourceProviders.Values)
            {
                _SupervisedBySourceProvider.Nodes.Remove(var);
            }

            //展開所有班導師節點
            _SupervisedBySourceProvider.ExpandAll();
            if (!_TargetTreeView.Nodes.Contains(_SupervisedBySourceProvider))
                _TargetTreeView.Nodes.Add(_SupervisedBySourceProvider);

            //待處理教師                
            if (!_TargetTreeView.Nodes.Contains(_TempProvider))
                _TargetTreeView.Nodes.Add(_TempProvider);
            //刪除教師
            if (!_TargetTreeView.Nodes.Contains(_DeleteStatusTeacherSourceProvider))
                _TargetTreeView.Nodes.Add(_DeleteStatusTeacherSourceProvider);    
            #endregion

            _TargetTreeView.Tag = this;
            _TargetTreeView.SelectedNode = _SelectedNode;
            
        }

        #endregion


        #region INamed<string> 成員

        public string Name
        {
            get { return "檢視班導師"; }
        }

        #endregion
    }
}
