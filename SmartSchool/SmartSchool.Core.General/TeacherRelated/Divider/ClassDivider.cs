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
            #region �إ�Source
            //�Ҧ��Юv
            List<BriefTeacherData> normalStatusTeacherSource = new List<BriefTeacherData>();
            //�Ҧ��Z�ɮv
            List<BriefTeacherData> supervisedBySource = new List<BriefTeacherData>();
            //�U�~�ůZ�ɮv
            Dictionary<string, List<BriefTeacherData>> supervisedByGradeSource = new Dictionary<string, List<BriefTeacherData>>();
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
            
            //�Ҧ��Z�ɮv
            _SupervisedBySourceProvider = (_SupervisedBySourceProvider != null ? _SupervisedBySourceProvider : new SupervisedBySourceProvider());
            
            //�U�~�ůZ�ɮv
            Dictionary<string, SupervisedByGradeSourceProvider> oldSupervisedByGradeSourceProviders = _SupervisedByGradeSourceProviders;
            _SupervisedByGradeSourceProviders = new Dictionary<string, SupervisedByGradeSourceProvider>();

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
                    //�O�Z�ɮv��
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
                    //    //�[�J�Z�ɮv��
                    //    supervisedBySource.Add(teacher);
                    //    //�[�J�ܦU�~�ůZ�ɮv��
                    //    foreach (SupervisedByClassInfo classInfo in teacher.SupervisedByClassInfo)
                    //    {
                    //        //�Ӧ~�Ť��s�b�h�s�W
                    //        if (!supervisedByGradeSource.ContainsKey(classInfo.SupervisedByGradeYear))
                    //            supervisedByGradeSource.Add(classInfo.SupervisedByGradeYear, new List<BriefTeacherData>());
                    //        //�P�~�ŦP�Ѯv�u�ݤ@��
                    //        if (!supervisedByGradeSource[classInfo.SupervisedByGradeYear].Contains(teacher))
                    //            supervisedByGradeSource[classInfo.SupervisedByGradeYear].Add(teacher);
                    //    }
                    //}
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
            //�Ҧ��Z�ɮv
            _SupervisedBySourceProvider.Source = supervisedBySource;
            //�U�~�ůZ�ɮv
            foreach (string grade in supervisedByGradeSource.Keys)
            {
                //�p�G�Ӧ~�Ŧb�¸�Ƥ��s�b�N��L�ӡA�_�h�ؤ@�ӷs��
                if (oldSupervisedByGradeSourceProviders.ContainsKey(grade))
                {
                    _SupervisedByGradeSourceProviders.Add(grade, oldSupervisedByGradeSourceProviders[grade]);
                    //�q�²M�沾���A�S�Q�����N�O�n�q�e�����s����
                    oldSupervisedByGradeSourceProviders.Remove(grade);
                }
                else
                {
                    SupervisedByGradeSourceProvider newGradeSourceProvider = new SupervisedByGradeSourceProvider();
                    newGradeSourceProvider.Grade = (grade == "" ? "����" : grade);
                    _SupervisedByGradeSourceProviders.Add(grade, newGradeSourceProvider);
                }
                _SupervisedByGradeSourceProviders[grade].Source = supervisedByGradeSource[grade];
            }

            //�R���Юv
            _DeleteStatusTeacherSourceProvider.Source = deletedTeacherSource;
            #endregion
            #region �NSourceProvider��JTreeView
            ////�p�G�Ҧ��Юv�`�I���s�b�h�s�W
            //if (!_TreeViewTeacher.Nodes.Contains(_NormalStatusTeacherSourceProvider))
            //    _TreeViewTeacher.Nodes.Add(_NormalStatusTeacherSourceProvider);
            //�U�~�ůZ�ɮv�`�I
            foreach (SupervisedByGradeSourceProvider var in _SupervisedByGradeSourceProviders.Values)
            {
                if (!_SupervisedBySourceProvider.Nodes.Contains(var))
                {
                    int gradeYear = 0;
                    if (!int.TryParse(var.Grade, out gradeYear))
                        gradeYear = int.MaxValue;
                    //�Ӧ~�űƧǡA�M�䴡�J�I
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
            //�R���w���ݭn���Z�ɮv�`�I
            foreach (SupervisedByGradeSourceProvider var in oldSupervisedByGradeSourceProviders.Values)
            {
                _SupervisedBySourceProvider.Nodes.Remove(var);
            }

            //�i�}�Ҧ��Z�ɮv�`�I
            _SupervisedBySourceProvider.ExpandAll();
            if (!_TargetTreeView.Nodes.Contains(_SupervisedBySourceProvider))
                _TargetTreeView.Nodes.Add(_SupervisedBySourceProvider);

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


        #region INamed<string> ����

        public string Name
        {
            get { return "�˵��Z�ɮv"; }
        }

        #endregion
    }
}
