using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using SmartSchool.TeacherRelated.Search;

namespace SmartSchool.TeacherRelated.SourceProvider
{
    class SupervisedByGradeSourceProvider : DragDropTreeNode,ISourceProvider<BriefTeacherData, Search.ISearchTeacher>
    {
        private List<BriefTeacherData> _Source;

        private SearchTeacherInMetadata _SearchProvider;

        private string _Grade;

        public string Grade
        {
            get { return _Grade; }
            set 
            {
                _Grade = value;
                this.Text = "" + _Grade + "年級(" + (_Source != null ? _Source.Count : 0) + ")";
            }
        }

        #region ISourceProvider<BriefTeacherData,ISearchTeacher> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefTeacherData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "" + _Grade + "年級(" + (_Source != null ? _Source.Count : 0) + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public bool DisplaySource
        {
            get { return true; }
        }

        public ISearchTeacher SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                    _SearchProvider = new Search.SearchTeacherInMetadata();
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public string SearchWatermark
        {
            get 
            {
                return "在\"" + _Grade + "年級\"中搜尋"; 
            }
        }

        #endregion
    }
}
