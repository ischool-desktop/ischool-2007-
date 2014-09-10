using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using System.Collections.ObjectModel;

namespace SmartSchool.Others.Configuration.ExamTemplate
{
    class TemplateItem : ButtonItem
    {
        private string _name;
        private string _identity;
        private bool _is_dirty;

        public TemplateItem(string identity, string name)
        {
            TemplateName = name;
            _identity = identity;
            _is_dirty = false;
        }

        public string TemplateName
        {
            get { return _name; }
            set
            {
                _name = value;
                Text = value;
                Refresh();
            }
        }

        public string Identity
        {
            get { return _identity; }
        }

        public bool IsDirty
        {
            get { return _is_dirty; }
            set { _is_dirty = value; }
        }
    }
}
