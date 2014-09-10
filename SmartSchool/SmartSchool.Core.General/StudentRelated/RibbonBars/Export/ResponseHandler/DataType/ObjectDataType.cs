using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.DataType
{
    public class ObjectDataType:IDataType
    {
        #region IDataType<object> ����
        private string _value;

        public void SetValue(string value)
        {
            _value = value;
        }

        public bool IsValidDataType
        {
            get { return true; }
        }

        public object GetTypeValue()
        {
            return _value;
        }

        #endregion
    }
}
