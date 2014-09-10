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

        #region ITeacherDivider ����

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
            #region �إ�Source
            //�Ҧ��Юv
            List<BriefTeacherData> normalStatusTeacherSource = new List<BriefTeacherData>();

            //�Ҧ������Юv
            List<BriefTeacherData> allCategorySource = new List<BriefTeacherData>();

            //�������Юv
            List<BriefTeacherData> nonCategorySource = new List<BriefTeacherData>();

            //�U�����Юv
            Dictionary<string, List<BriefTeacherData>> teacherCategorySource = new Dictionary<string, List<BriefTeacherData>>();

            //�R���Юv
            List<BriefTeacherData> deletedTeacherSource = new List<BriefTeacherData>();
            #endregion

            #region �إ�SourceProvider
            //�Ҧ��Юv
            _NormalStatusTeacherSourceProvider = (_NormalStatusTeacherSourceProvider != null ? _NormalStatusTeacherSourceProvider : new NormalStatusTeacherSourceProvider());

            //�Ҧ������Юv
            _AllCategorySourceProvider = (_AllCategorySourceProvider != null ? _AllCategorySourceProvider : new AllCategorySourceProvider());

            //�������Юv
            _NonCategorySourceProvider = (_NonCategorySourceProvider != null ? _NonCategorySourceProvider : new NonCategorySourceProvider());

            //�U�����Юv
            Dictionary<string, TeacherCategorySourceProvider> oldTeacherCategorySourceProviders = _TeacherCategorySourceProviders;
            _TeacherCategorySourceProviders = new Dictionary<string, TeacherCategorySourceProvider>();

            //�R���Юv
            _DeleteStatusTeacherSourceProvider = (_DeleteStatusTeacherSourceProvider != null ? _DeleteStatusTeacherSourceProvider : new DeleteStatusTeacherSourceProvider());
            #endregion

            #region �N��ƶ�JSource
            foreach (BriefTeacherData teacher in source.Values)
            {
                //���A���@��
                if (teacher.Status == "�@��")
                {
                    normalStatusTeacherSource.Add(teacher);
                    allCategorySource.Add(teacher);

                    //�̷Ӥ�����J�Юv����
                    if (teacher.Category != "")
                    {
                        //�Ӥ������s�b�h�s�W
                        if (!teacherCategorySource.ContainsKey(teacher.Category))
                            teacherCategorySource.Add(teacher.Category, new List<BriefTeacherData>());
                        teacherCategorySource[teacher.Category].Add(teacher);
                    }
                    else
                    {
                        nonCategorySource.Add(teacher);
                    }
                }
                if (teacher.Status == "�R��")
                {
                    deletedTeacherSource.Add(teacher);
                }
            }
            #endregion
            #region �NSource��JSourceProvider
            //�Ҧ��Юv
            _NormalStatusTeacherSourceProvider.Source = normalStatusTeacherSource;
            //�Ҧ������Юv
            _AllCategorySourceProvider.Source = allCategorySource;
            //�������Юv
            _NonCategorySourceProvider.Source = nonCategorySource;
            //�U�����Юv
            foreach (string category in teacherCategorySource.Keys)
            {
                //�p�G�Ӥ����b�¸�Ƥ��s�b�N��L�ӡA�_�h�ؤ@�ӷs��
                if (oldTeacherCategorySourceProviders.ContainsKey(category))
                {
                    _TeacherCategorySourceProviders.Add(category, oldTeacherCategorySourceProviders[category]);
                    //�q�²M�沾���A�S�������̫�|�q�e��������
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
            //�R���Юv
            _DeleteStatusTeacherSourceProvider.Source = deletedTeacherSource;
            #endregion
            #region �NSourceProvider��JTreeView
            ////�p�G�Ҧ��Юv�`�I���s�b�h�s�W
            //if (!_TreeViewTeacher.Nodes.Contains(_NormalStatusTeacherSourceProvider))
            //    _TreeViewTeacher.Nodes.Add(_NormalStatusTeacherSourceProvider);
            //�U�����Юv
            foreach (TeacherCategorySourceProvider categorySourceProvider in _TeacherCategorySourceProviders.Values)
            {
                if (!_AllCategorySourceProvider.Nodes.Contains(categorySourceProvider))
                {
                    _AllCategorySourceProvider.Nodes.Add(categorySourceProvider);
                }
            }
            //�������Юv
            if (!_AllCategorySourceProvider.Nodes.Contains(_NonCategorySourceProvider))
            {
                _AllCategorySourceProvider.Nodes.Add(_NonCategorySourceProvider);
            }
            //�R���w���ݭn���Юv����
            foreach (TeacherCategorySourceProvider node in oldTeacherCategorySourceProviders.Values)
            {
                _AllCategorySourceProvider.Nodes.Remove(node);
            }
            //�i�}�Ҧ����O�`�I
            _AllCategorySourceProvider.ExpandAll();
            if (!_TargetTreeView.Nodes.Contains(_AllCategorySourceProvider))
                _TargetTreeView.Nodes.Add(_AllCategorySourceProvider);
            
            //�ݳB�z�Юv                
            if (!_TargetTreeView.Nodes.Contains(_TempProvider))
                _TargetTreeView.Nodes.Add(_TempProvider);
            //�R���Юv
            if (!_TargetTreeView.Nodes.Contains(_DeleteStatusTeacherSourceProvider))
                _TargetTreeView.Nodes.Add(_DeleteStatusTeacherSourceProvider);
            #endregion

            _TargetTreeView.Tag = this;
            _TargetTreeView.SelectedNode = _SelectedNode;
            
        }

        #endregion

        #region IDenominated ����

        public string Name
        {
            get { return "�˵��U�����Юv"; }
        }

        #endregion
    }
}
