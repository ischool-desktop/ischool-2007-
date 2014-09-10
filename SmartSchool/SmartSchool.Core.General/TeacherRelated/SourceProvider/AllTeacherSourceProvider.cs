using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using SmartSchool.TeacherRelated.Search;

namespace SmartSchool.TeacherRelated.SourceProvider
{
    class AllTeacherSourceProvider: DragDropTreeNode, ISourceProvider<BriefTeacherData, Search.ISearchTeacher>
    {
        private List<BriefTeacherData> _Source;

        private SearchTeacherInMetadata _SearchProvider;

        #region ISourceProvider<BriefTeacherData,ISearchTeacher> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefTeacherData> Source
        {
            get
            {
                return (_Source == null ? new List<BriefTeacherData>() : _Source);
            }
            set
            {
                _Source = value;
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public bool DisplaySource
        {
            get { return false; }
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
                return "搜尋所有教師";
            }
        }

        #endregion
    }
}
