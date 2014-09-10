using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.ClassRelated.Divider;
using SmartSchool.Common;
using System.Windows.Forms;
using SmartSchool.StudentRelated.SourceProvider;
using SmartSchool.TagManage;

namespace SmartSchool.StudentRelated.Divider
{
    class CategoryDivider:IStudentDivider
    {
        DragDropTreeView _TargetTreeView;
        private TempStudentSourceProvider _TempProvider;
        private List<TreeNode> _RelatedNodes=new List<TreeNode>();
        private TreeNode _SelectedNode;

        private Dictionary<string, NormalizeStudentSourceProvider> _CategorySourceProviders = new Dictionary<string, NormalizeStudentSourceProvider>();
        private Dictionary<string, NormalizeStudentSourceProvider> _PrefixSourceProviders = new Dictionary<string, NormalizeStudentSourceProvider>();
        private Dictionary<string, Dictionary<string, NormalizeStudentSourceProvider>> _PrefixCategorySourceProviders = new Dictionary<string, Dictionary<string, NormalizeStudentSourceProvider>>();
        NormalizeStudentSourceProvider _NonCategorySourceProviders = new NormalizeStudentSourceProvider();

        private void _TargetTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_RelatedNodes.Contains(_TargetTreeView.SelectedNode))
                _SelectedNode = _TargetTreeView.SelectedNode;
        }
        
        private void ReflshRelatedNodes(TreeNodeCollection treeNodeCollection, bool clearList)
        {
            if (clearList) _RelatedNodes.Clear();
            foreach (TreeNode var in treeNodeCollection)
            {
                _RelatedNodes.Add(var);
                ReflshRelatedNodes(var.Nodes, false);
            }
        }

        #region IStudentDivider 成員

        public TempStudentSourceProvider TempProvider
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

        public CategoryDivider()
        {
            _NonCategorySourceProviders.DisplaySource = true;
            _NonCategorySourceProviders.Text = "未分類別";
        }

        public void Divide(Dictionary<string, BriefStudentData> source)
        {
            Dictionary<string, List<BriefStudentData>> categorySource = new Dictionary<string, List<BriefStudentData>>();
            Dictionary<string, List<BriefStudentData>> prefixSource = new Dictionary<string, List<BriefStudentData>>();
            Dictionary<string, Dictionary<string, List<BriefStudentData>>> prefixCategorySource = new Dictionary<string, Dictionary<string, List<BriefStudentData>>>();
            List<BriefStudentData> nonCategorySource = new List<BriefStudentData>();
            #region 整理資料切割

            foreach ( BriefStudentData var in source.Values )
            {
                if ( var.Tags.Count > 0 )
                {
                    foreach ( TagInfo tag in var.Tags )
                    {
                        if ( tag.Prefix == "" )
                        {
                            #region 新增categorySource內容
                            if ( !categorySource.ContainsKey(tag.Name) )
                                categorySource.Add(tag.Name, new List<BriefStudentData>());
                            #endregion
                            //加入至單獨類別
                            if ( !categorySource[tag.Name].Contains(var) )//避免重複出現
                                categorySource[tag.Name].Add(var);
                        }
                        else
                        {
                            #region 新增prefixSource內容
                            if ( !prefixSource.ContainsKey(tag.Prefix) )
                                prefixSource.Add(tag.Prefix, new List<BriefStudentData>());
                            #endregion
                            //加入至類別群組
                            if ( !prefixSource[tag.Prefix].Contains(var) )//避免重複出現
                                prefixSource[tag.Prefix].Add(var);
                            #region 新增prefixCategorySource內容
                            if ( !prefixCategorySource.ContainsKey(tag.Prefix) )
                                prefixCategorySource.Add(tag.Prefix, new Dictionary<string, List<BriefStudentData>>());
                            if ( !prefixCategorySource[tag.Prefix].ContainsKey(tag.Name) )
                                prefixCategorySource[tag.Prefix].Add(tag.Name, new List<BriefStudentData>());
                            #endregion
                            //加入至類別群組內之類別
                            if ( !prefixCategorySource[tag.Prefix][tag.Name].Contains(var) )//避免重複出現
                                prefixCategorySource[tag.Prefix][tag.Name].Add(var);
                        }
                    }
                }
                else
                    //加入至沒有類別
                    nonCategorySource.Add(var);
            }
            #endregion

            #region 填入畫面
            #region 重新建至暫存資料區
            Dictionary<string, NormalizeStudentSourceProvider> OldCategorySourceProviders = _CategorySourceProviders;
            _CategorySourceProviders = new Dictionary<string, NormalizeStudentSourceProvider>();

            Dictionary<string, NormalizeStudentSourceProvider> OldPrefixSourceProviders = _PrefixSourceProviders;
            _PrefixSourceProviders = new Dictionary<string, NormalizeStudentSourceProvider>();

            Dictionary<string, Dictionary<string, NormalizeStudentSourceProvider>> OldPrefixCategorySourceProviders = _PrefixCategorySourceProviders;
            _PrefixCategorySourceProviders = new Dictionary<string, Dictionary<string, NormalizeStudentSourceProvider>>(); 
            #endregion

            _TargetTreeView.Nodes.Clear();
            #region 群組
            foreach (string tagPrefix in prefixSource.Keys)
            {
                NormalizeStudentSourceProvider prefixNode;
                #region 處理重新填入資料，當節點已產生時不重複產生
                if (OldPrefixSourceProviders.ContainsKey(tagPrefix))
                    prefixNode = OldPrefixSourceProviders[tagPrefix];
                else
                    prefixNode = new NormalizeStudentSourceProvider();
                _PrefixSourceProviders.Add(tagPrefix, prefixNode);
                _PrefixCategorySourceProviders.Add(tagPrefix, new Dictionary<string, NormalizeStudentSourceProvider>());
                #endregion

                //設定群組內的學生
                prefixNode.Source = prefixSource[tagPrefix];
                prefixNode.Nodes.Clear();
                prefixNode.Text = tagPrefix;
                prefixNode.DisplaySource = true;
                _TargetTreeView.Nodes.Add(prefixNode);

                #region 群組內類別
                foreach (string cateName in prefixCategorySource[tagPrefix].Keys)
                {
                    NormalizeStudentSourceProvider categoryNode;
                    #region 處理重新填入資料，當節點已產生時不重複產生
                    if (OldPrefixCategorySourceProviders.ContainsKey(tagPrefix) && OldPrefixCategorySourceProviders[tagPrefix].ContainsKey(cateName))
                        categoryNode = OldPrefixCategorySourceProviders[tagPrefix][cateName];
                    else
                        categoryNode = new NormalizeStudentSourceProvider();
                    _PrefixCategorySourceProviders[tagPrefix].Add(cateName,categoryNode);
                    #endregion
                    categoryNode.Text = cateName;
                    //設定群組內類別的學生
                    categoryNode.Source = prefixCategorySource[tagPrefix][cateName];
                    categoryNode.DisplaySource = true;
                    prefixNode.Nodes.Add(categoryNode);
                }
                #endregion

            }  
            #endregion
            #region 獨立類別
            foreach (string  tagName in categorySource.Keys)
            {
                NormalizeStudentSourceProvider categoryNode;
                #region 處理重新填入資料，當節點已產生時不重複產生
                if (OldCategorySourceProviders.ContainsKey(tagName))
                    categoryNode = OldCategorySourceProviders[tagName];
                else
                    categoryNode = new NormalizeStudentSourceProvider();
                _CategorySourceProviders.Add(tagName, categoryNode);
                #endregion
                categoryNode.Text = tagName;
                categoryNode.DisplaySource = true;
                categoryNode.Source = categorySource[tagName];
                _TargetTreeView.Nodes.Add(categoryNode);
            }
            #endregion
            //暫存區
            //_TargetTreeView.Nodes.Add(_TempProvider);
            //沒有類別的學生
            _NonCategorySourceProviders.Source = nonCategorySource;
            _TargetTreeView.Nodes.Add(_NonCategorySourceProviders);
            #endregion
            _TargetTreeView.SelectedNode = _SelectedNode;
            ReflshRelatedNodes(_TargetTreeView.Nodes,true);
        }
        #endregion

        #region IDenominated 成員

        public string Name
        {
            get { return "依學生分類檢視"; }
        }

        #endregion
    }
}
