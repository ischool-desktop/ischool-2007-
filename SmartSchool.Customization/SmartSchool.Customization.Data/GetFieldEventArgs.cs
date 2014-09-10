using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class GetFieldEventArgs : EventArgs
    {
        private string _FieldName;
        internal GetFieldEventArgs(string fieldName)
        {
            _FieldName = fieldName;
        }
        public string FieldName
        {
            get { return _FieldName; }
        }
    }
}
