using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.ClassRelated.Search;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.SourceProvider
{
    class NonGradeClassSourceProvider: DragDropTreeNode, ISourceProvider<ClassInfo, Search.ISearchClass>
    {
        private List<ClassInfo> _Source;

        private SearchClassInMetadata _SearchProvider;

        public NonGradeClassSourceProvider()
        {
            this.Text = "未分年級(" + (_Source==null?0:_Source.Count) + ")";
        }

        #region ISourceProvider<DetailClassInfo,ISearchClass> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<ClassInfo> Source
        {
            get
            {
                return (_Source == null ? new List<ClassInfo>() : _Source);
            }
            set
            {
                _Source = value;
                this.Text = "未分年級(" + (_Source == null ? 0 : _Source.Count) + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public bool DisplaySource
        {
            get { return true; }
        }

        public SmartSchool.ClassRelated.Search.ISearchClass SearchProvider
        {
            get 
            {
                if (_SearchProvider == null)
                    _SearchProvider = new Search.SearchClassInMetadata();
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public string SearchWatermark
        {
            get
            {
                return "在\"未分年級\"中搜尋";
            }
        }

        #endregion
    }
}
