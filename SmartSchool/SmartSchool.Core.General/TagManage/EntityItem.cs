using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.TagManage
{
    public class EntityItem
    {
        public EntityItem(int identity, object associate)
        {
            _identity = identity;
            _associate_object = associate;
            _tags = new List<int>();
        }

        private int _identity;
        public int Identity
        {
            get { return _identity; }
        }

        private object _associate_object;
        public object AssociateObject
        {
            get { return _associate_object; }
        }

        private List<int> _tags;
        public List<int> Tags
        {
            get { return _tags; }
        }
    }
}
