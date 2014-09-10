using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using SmartSchool.ClassRelated.Search;

namespace SmartSchool.ClassRelated.SourceProvider
{
    class GradeClassSourceProvider : DragDropTreeNode, ISourceProvider<ClassInfo, Search.ISearchClass>
    {
        private List<ClassInfo> _Source;

        private SearchClassInMetadata _SearchProvider;

        private string _Grade;

        public GradeClassSourceProvider()
        {
            this.Text = "" + _Grade + "年級(" + (_Source != null ? _Source.Count : 0) + ")";
        }

        public string Grade
        {
            get { return _Grade; }
            set
            {
                _Grade = value;
                this.Text = "" + _Grade + "年級(" + (_Source != null ? _Source.Count : 0) + ")";
            }
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
                return "在\"" + _Grade + "年級\"中搜尋"; 
            }
        }

        #endregion
    }
}
