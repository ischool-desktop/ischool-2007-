using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator;

namespace SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Formater
{
    public class FieldFormaterFactory
    {
        public static IFieldFormater CreateInstance(ExportType type)
        {
            switch (type)
            {
                case ExportType.ExportStudent:
                    return new BaseFieldFormater();
                default:
                    return new BaseFieldFormater();
            }
        }
    }
}
