using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using SmartSchool.StudentRelated.SourceProvider;
using System.Windows.Forms;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using SmartSchool.API.StudentExtension;

namespace SmartSchool.StudentRelated.Divider
{
    class DisciplineDivider : IStudentDivider
    {
        DragDropTreeView _TargetTreeView;
        private TempStudentSourceProvider _TempProvider;
        private TreeNode _MoreNodes;
        private TreeNode _SetupNodes;
        private List<SemesterDisciplineStudentSourceProvider> _SemesterProviders = new List<SemesterDisciplineStudentSourceProvider>();
        private List<TreeNode> _RelatedNodes = new List<TreeNode>();
        private TreeNode _SelectedNode = null;
        private bool _Dividing = false;
        private SetupDisciplineForm _SetupForm = new SetupDisciplineForm();
        private List<string> _Source = new List<string>();
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

        void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Student.Instance.ReloadData();
            List<SemesterDisciplineStudentSourceProvider> newlist = _SemesterProviders;
            _SemesterProviders = new List<SemesterDisciplineStudentSourceProvider>();
            foreach ( SemesterDisciplineStudentSourceProvider var in newlist )
            {
                _SemesterProviders.Add(new SemesterDisciplineStudentSourceProvider(var.SchoolYear, var.Semester, _RelatedNodes, _SetupForm));
            }
            Divide(_Source);
        }
        private void _TargetTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ( _TargetTreeView.SelectedNode == _MoreNodes )
            {
                #region 增加別的學期
                _TargetTreeView.SelectedNode = _SelectedNode;
                SelectSchoolYearSemesterForm form = new SelectSchoolYearSemesterForm();
                if ( form.ShowDialog() == DialogResult.OK )
                {
                    foreach ( SemesterDisciplineStudentSourceProvider var in _SemesterProviders )
                    {
                        //選取學年度學期已經載入了
                        if ( var.SchoolYear == form.SchoolYear && var.Semester == form.Semester )
                            return;
                    }
                    SemesterDisciplineStudentSourceProvider newsemester = new SemesterDisciplineStudentSourceProvider(form.SchoolYear, form.Semester, _RelatedNodes, _SetupForm);
                    _SemesterProviders.Add(newsemester);
                    _RelatedNodes.Add(newsemester);
                    newsemester.Reflash(_Source);
                    ReflashTreeView();
                }
                return; 
                #endregion
            }
            if ( _TargetTreeView.SelectedNode == _SetupNodes )
            {
                #region 改變檢視設定
                _TargetTreeView.SelectedNode = _SelectedNode;
                if ( _SetupForm.ShowDialog() == DialogResult.OK )
                {
                    Divide(_Source);
                }
                return; 
                #endregion
            }
            if ( !_Dividing &&_RelatedNodes.Contains(_TargetTreeView.SelectedNode) )
                _SelectedNode = _TargetTreeView.SelectedNode;
        }

        public DisciplineDivider()
        {
            SemesterDisciplineStudentSourceProvider currentSemester = new SemesterDisciplineStudentSourceProvider(CurrentUser.Instance.SchoolYear, CurrentUser.Instance.Semester, _RelatedNodes, _SetupForm);
            _SemesterProviders.Add(currentSemester);
            _RelatedNodes.Add(currentSemester);
            _MoreNodes = new TreeNode();
            _MoreNodes.Text = "其它學期";
            _SetupNodes = new TreeNode();
            _SetupNodes.Text = "選項";

            #region toolStripMenuItem1
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(93, 22);
            toolStripMenuItem1.Text = "重新整理";
            toolStripMenuItem1.Click += new EventHandler(toolStripMenuItem1_Click);
            #endregion

            #region contextMenuStrip1
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripMenuItem1});
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.ShowImageMargin = false;
            contextMenuStrip1.Size = new System.Drawing.Size(94, 26);
            #endregion
        }

        private void ReflashTreeView()
        {
            _TargetTreeView.Nodes.Clear();
            _SemesterProviders.Sort();
            _TargetTreeView.Nodes.AddRange(_SemesterProviders.ToArray());
            _TargetTreeView.Nodes.Add(_MoreNodes);
            _TargetTreeView.Nodes.Add(_SetupNodes);
            //_TargetTreeView.Nodes.Add(_TempProvider);
        }
        #region IStudentDivider 成員

        public SmartSchool.StudentRelated.SourceProvider.TempStudentSourceProvider TempProvider
        {
            get { return _TempProvider; }
            set { _TempProvider = value; }
        }

        public SmartSchool.Common.DragDropTreeView TargetTreeView
        {
            get { return _TargetTreeView; }
            set
            {
                if ( _TargetTreeView != null )
                    _TargetTreeView.AfterSelect -= new TreeViewEventHandler(_TargetTreeView_AfterSelect);
                _TargetTreeView = value;
                _TargetTreeView.ContextMenuStrip = this.contextMenuStrip1;
                _TargetTreeView.AfterSelect += new TreeViewEventHandler(_TargetTreeView_AfterSelect);
            }
        }
        public void Divide(List<string> source)
        {
            _Source = source;
            _Dividing = true;
            _TargetTreeView.Nodes.Clear();
            foreach ( SemesterDisciplineStudentSourceProvider var in _SemesterProviders )
            {
                var.Reflash(_Source);
            }
            ReflashTreeView();
            _TargetTreeView.SelectedNode = _SelectedNode;
            _Dividing = false;
        }
        public void Divide(Dictionary<string, BriefStudentData> source)
        {
            Divide( new List<string>(source.Keys));
        }

        #endregion

        #region IDenominated 成員

        public string Name
        {
            get { return "依獎懲紀錄檢視"; }
        }

        #endregion


    }
}
