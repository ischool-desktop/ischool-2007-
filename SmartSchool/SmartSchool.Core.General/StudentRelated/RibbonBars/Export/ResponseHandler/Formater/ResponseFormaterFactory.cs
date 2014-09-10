using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Formater
{
    public class ResponseFormaterFactory
    {
        public static IResponseFormater CreateInstance(ExportType type)
        {
            switch (type)
            {
                case ExportType.ExportStudent:
                    return new ResponseFormater();
                default:
                    return new ResponseFormater();
            }
        }
    }
}
