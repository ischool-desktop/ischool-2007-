using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;

namespace SmartSchool.StudentRelated.Placing.Export
{
    public interface IExportSheetHandler
    {
        void Export(Worksheet targetSheet, ExcelSheetInfo info);
    }
}
