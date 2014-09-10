using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.Placing.Export
{
    public class ExportSheetHandlerFactory
    {
        public static IExportSheetHandler CreateInstance(SheetType type)
        {
            switch (type)
            {
                case SheetType.Nomal:
                    return new NormalExportSheetHandler();
                default:
                    return new NormalExportSheetHandler();
                    
            }
        }
    }
}
