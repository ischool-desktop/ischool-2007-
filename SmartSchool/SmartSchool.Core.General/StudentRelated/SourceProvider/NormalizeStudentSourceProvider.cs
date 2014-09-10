using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.SourceProvider
{
    class NormalizeStudentSourceProvider : DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        private bool _DisplaySource;

        private string _Text;

        new public string Text
        {
            get { return _Text; }
            set 
            {
                _Text = value;
                base.Text = Text + "(" + _Source.Count + ")";
            }
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                base.Text = Text+"(" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return _DisplaySource; }
            set { _DisplaySource = value; }
        }

        public string SearchWatermark { get { return "在\""+_Text+"\"中搜尋"; } }
        #endregion
    }
}
