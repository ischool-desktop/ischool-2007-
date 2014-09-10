using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.TeacherRelated.SourceProvider;
using SmartSchool.Common;
using System.Windows.Forms;

namespace SmartSchool.TeacherRelated.Divider
{
    class CategoryDivider : ITeacherDivider
    {
        DragDropTreeView _TargetTreeView;
        private TempTeacherSourceProvider _TempProvider;
        private TreeNode _SelectedNode;

        private NormalStatusTeacherSourceProvider _NormalStatusTeacherSourceProvider;
        private AllTeacherSourceProvider _AllTeacherSourceProvider = new AllTeacherSourceProvider();
        private NonCategorySourceProvider _NonCategorySourceProvider = new NonCategorySourceProvider();
        private Dictionary<string, TeacherCategorySourceProvider> _TeacherCategorySourceProviders = new Dictionary<string, TeacherCategorySourceProvider>();
        private AllCategorySourceProvider _AllCategorySourceProvider;
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
                if (_TargetTreeView != null)
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

            //所有分類教師
            _AllCategorySourceProvider = (_AllCategorySourceProvider != null ? _AllCategorySourceProvider : new AllCategorySourceProvider());

            //未分類教師
            _NonCategorySourceProvider = (_NonCategorySourceProvider != null ? _NonCategorySourceProvider : new NonCategorySourceProvider());

            //各分類教師
            Dictionary<string, TeacherCategorySourceProvider> oldTeacherCategorySourceProviders = _TeacherCategorySourceProviders;
            _TeacherCategorySourceProviders = new Dictionary<string, TeacherCategorySourceProvider>();

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
            //所有分類教師
            _AllCategorySourceProvider.Source = allCategorySource;
            //未分類教師
            _NonCategorySourceProvider.Source = nonCategorySource;
            //各分類教師
            foreach (string category in teacherCategorySource.Keys)
            {
                //如果該分類在舊資料中存在就抓過來，否則建一個新的
                if (oldTeacherCategorySourceProviders.ContainsKey(category))
                {
                    _TeacherCategorySourceProviders.Add(category, oldTeacherCategorySourceProviders[category]);
                    //從舊清單移除，沒移除的最後會從畫面中移除
                    oldTeacherCategorySourceProviders.Remove(category);
                }
                else
                {
                    TeacherCategorySourceProvider newTeacherCategorySourceProvider = new TeacherCategorySourceProvider();
                    newTeacherCategorySourceProvider.Category = category;
                    _TeacherCategorySourceProviders.Add(category, newTeacherCategorySourceProvider);
                }
                _TeacherCategorySourceProviders[category].Source = teacherCategorySource[category];
            }
            //刪除教師
            _DeleteStatusTeacherSourceProvider.Source = deletedTeacherSource;
            #endregion
            #region 將SourceProvider放入TreeView
            ////如果所有教師節點不存在則新增
            //if (!_TreeViewTeacher.Nodes.Contains(_NormalStatusTeacherSourceProvider))
            //    _TreeViewTeacher.Nodes.Add(_NormalStatusTeacherSourceProvider);
            //各分類教師
            foreach (TeacherCategorySourceProvider categorySourceProvider in _TeacherCategorySourceProviders.Values)
            {
                if (!_AllCategorySourceProvider.Nodes.Contains(categorySourceProvider))
                {
                    _AllCategorySourceProvider.Nodes.Add(categorySourceProvider);
                }
            }
            //未分類教師
            if (!_AllCategorySourceProvider.Nodes.Contains(_NonCategorySourceProvider))
            {
                _AllCategorySourceProvider.Nodes.Add(_NonCategorySourceProvider);
            }
            //刪除已不需要的教師分類
            foreach (TeacherCategorySourceProvider node in oldTeacherCategorySourceProviders.Values)
            {
                _AllCategorySourceProvider.Nodes.Remove(node);
            }
            //展開所有類別節點
            _AllCategorySourceProvider.ExpandAll();
            if (!_TargetTreeView.Nodes.Contains(_AllCategorySourceProvider))
                _TargetTreeView.Nodes.Add(_AllCategorySourceProvider);
            
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

        #region IDenominated 成員

        public string Name
        {
            get { return "檢視各分類教師"; }
        }

        #endregion
    }
}
