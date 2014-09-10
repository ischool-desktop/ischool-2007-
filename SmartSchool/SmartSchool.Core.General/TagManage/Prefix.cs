using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.TagManage
{
    public class Prefix
    {
        public Prefix(string name)
        {
            _name = name;
            _tags = new TagCollection();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        public virtual string DisplayText
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "<¥¼¤ÀÃþ>";
                }
                else
                    return Name;
            }
        }

        private TagCollection _tags;
        public TagCollection Tags
        {
            get { return _tags; }
        }
    }
}
