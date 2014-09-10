using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Converter
{
    public class BaseConverter : IConverter
    {
        public string Convert(string value)
        {
            if (value == null)
                return string.Empty;
            return value;
        }
    }
}
