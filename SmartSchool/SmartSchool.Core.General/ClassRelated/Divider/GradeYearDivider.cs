using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SmartSchool.ClassRelated.SourceProvider;

namespace SmartSchool.ClassRelated.Divider
{
    class GradeYearDivider : IClassDivider
    {
        TreeView _TargetTreeView;
        private TempClassSourceProvider _TempProvider;
        private AllClassSourceProvider _AllClassSourceProvider;
        private Dictionary<int, GradeClassSourceProvider> _GradeYearSourceProviders;
        private NonGradeClassSourceProvider _NonGradeClassSourceProvider;

        #region IClassDivider 成員

        public SmartSchool.ClassRelated.SourceProvider.TempClassSourceProvider TempProvider
        {
            get { return _TempProvider; }
            set { _TempProvider = value; }
        }

        public System.Windows.Forms.TreeView TargetTreeView
        {
            get { return _TargetTreeView; }
            set { _TargetTreeView = value; }
        }

        public void Divide(Dictionary<string, ClassInfo> source)
        {
            #region 建立Source
            List<ClassInfo> allClassSource = new List<ClassInfo>();
            List<ClassInfo> nonGradeClassSource = new List<ClassInfo>();
            Dictionary<int, List<ClassInfo>> gradeYearSource = new Dictionary<int, List<ClassInfo>>();
            #endregion
            #region 建立SourceProvider
            _AllClassSourceProvider = (_AllClassSourceProvider != null ? _AllClassSourceProvider : new AllClassSourceProvider());
            _NonGradeClassSourceProvider = (_NonGradeClassSourceProvider != null ? _NonGradeClassSourceProvider : new NonGradeClassSourceProvider());
            Dictionary<int, GradeClassSourceProvider> _DeleteGradeYearSourceProviders = (_GradeYearSourceProviders==null?new Dictionary<int, GradeClassSourceProvider>():_GradeYearSourceProviders);
            _GradeYearSourceProviders = new Dictionary<int, GradeClassSourceProvider>();
            #endregion
            #region 將資料填入Source
            foreach (ClassInfo classInfo in source.Values)
            {
                //加入所有班級
                allClassSource.Add(classInfo);
                int gradeYear;
                if (int.TryParse(classInfo.GradeYear, out gradeYear))
                {
                    //加入各年級
                    if (!gradeYearSource.ContainsKey(gradeYear))
                        gradeYearSource.Add(gradeYear, new List<ClassInfo>());
                    gradeYearSource[gradeYear].Add(classInfo);
                }
                else
                {
                    //加入未分年級
                    nonGradeClassSource.Add(classInfo);
                }
            }
            #endregion
            #region 將Source指派給SourceProvider
            _AllClassSourceProvider.Source = allClassSource;
            _NonGradeClassSourceProvider.Source = nonGradeClassSource;

            foreach (int gradeyearKey in gradeYearSource.Keys)
            {
                if (_DeleteGradeYearSourceProviders.ContainsKey(gradeyearKey))
                {
                    _GradeYearSourceProviders.Add(gradeyearKey, _DeleteGradeYearSourceProviders[gradeyearKey]);
                    _DeleteGradeYearSourceProviders.Remove(gradeyearKey);
                }
                else
                {
                    _GradeYearSourceProviders.Add(gradeyearKey, new GradeClassSourceProvider());
                    _GradeYearSourceProviders[gradeyearKey].Grade = "" + gradeyearKey;
                }
                _GradeYearSourceProviders[gradeyearKey].Source = gradeYearSource[gradeyearKey];
            }
            #endregion
            #region 將SourceProvider放到TreeView上

            bool expandAllClassSourceProvider =! _TargetTreeView.Nodes.Contains(_AllClassSourceProvider);
            _AllClassSourceProvider.Nodes.Clear();

            //所有學生
            if (!_TargetTreeView.Nodes.Contains(_AllClassSourceProvider))
            {
                _TargetTreeView.Nodes.Add(_AllClassSourceProvider);
            }
            //各年級學生
            foreach (GradeClassSourceProvider provider in _GradeYearSourceProviders.Values)
            {
                if (!_AllClassSourceProvider.Nodes.Contains(provider))
                    _AllClassSourceProvider.Nodes.Add(provider);
            }
            //未分年級學生
            if (!_AllClassSourceProvider.Nodes.Contains(_NonGradeClassSourceProvider))
                _AllClassSourceProvider.Nodes.Add(_NonGradeClassSourceProvider);
            //待處理班級
            if (!_TargetTreeView.Nodes.Contains(_TempProvider))
                _TargetTreeView.Nodes.Add(_TempProvider);
            if (expandAllClassSourceProvider)
            {
                //展開所有班級
                _AllClassSourceProvider.Expand();
            }
            #endregion
        }

        #endregion

        #region IDenominated 成員

        public string Name
        {
            get { return "依年級檢視"; }
        }

        #endregion
    }
}
