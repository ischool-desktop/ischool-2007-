using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Converter
{
    public interface IConverter
    {
        string Convert(string value);
    }
}
