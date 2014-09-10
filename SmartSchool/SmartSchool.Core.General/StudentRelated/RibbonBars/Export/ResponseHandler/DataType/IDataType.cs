using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.DataType
{
    public interface IDataType
    {
        void SetValue(string value);
        bool IsValidDataType { get;}  
        object GetTypeValue();
    }
}
