using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.CourseRelated
{
    internal class NavigateBase : INavigatePerspective
    {
        protected CourseDataSource _data_source;
        protected TemporalHandler _temporal_handler;
        protected TreeView _tree_control;
        protected bool _need_init;

        public NavigateBase(CourseDataSource source)
        {
            _need_init = true;
            _data_source = source;

            _data_source.SourceRefresh += new EventHandler(DataSource_SourceRefresh);
        }

        private void DataSource_SourceRefresh(object sender, EventArgs e)
        {
            _need_init = true;
        }

        public virtual void RefreshTreeView()
        {
            if (!_tree_control.Visible) return;

            TreeNode current_selected = _tree_control.SelectedNode;

            RefreshTree();

            if (current_selected == null)
            {
                CourseEntity.Instance.DisplayNodeContent(null); //顯示 Null 代表 ListPane 不顯示任何資料。

                return;
            }

            TreeNode[] nodes = _tree_control.Nodes.Find(current_selected.Name, true);

            if (nodes.Length <= 0)
            {
                CourseEntity.Instance.DisplayNodeContent(null); //顯示 Null 代表 ListPane 不顯示任何資料。
                return;
            }

            //Node 的 Key(Name) 要一樣外，型別也要一樣才行。
            TreeNode finalNode = null;
            foreach (TreeNode each in nodes)
            {
                if (each.GetType() == current_selected.GetType())
                {
                    finalNode = each;
                    break;
                }
            }

            if ((_tree_control == CourseEntity.Instance.CurrentPerspective.CurrentView) && current_selected != null)
            {
                _tree_control.SelectedNode = finalNode;
                CourseEntity.Instance.DisplayNodeContent(finalNode);
            }
        }

        #region INavigatePerspective 成員

        public virtual void BindView(TreeView view)
        {
            _tree_control = view;
        }

        public TreeView CurrentView
        {
            get { return _tree_control; }
        }

        public void SetTemporalHandler(TemporalHandler handler)
        {
            _temporal_handler = handler;
        }

        public void Show()
        {
            if (_need_init)
                RefreshTree();

            ShowTreeView();
        }

        protected virtual void RefreshTree()
        {
        }

        /// <summary>
        /// 處理各種節點中的「未分類」節點。
        /// 將其移到最後一個位置。
        /// </summary>
        protected void MoveNoneToLast(TreeNode parentNode)
        {
            TreeNode[] nodes = parentNode.Nodes.Find("None", true);

            foreach (TreeNode each in nodes)
            {
                TreeNode parent = each.Parent;
                if (parent != null)
                {
                    each.Remove();
                    parent.Nodes.Insert(parent.Nodes.Count - 1, each);
                }
            }
        }

        private void ShowTreeView()
        {
            _tree_control.Show();
        }

        public virtual void Hide()
        {
            _tree_control.Hide();
        }

        #endregion

    }
}
