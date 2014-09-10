using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using SmartSchool.StudentRelated.Placing.Rule;
using System.Drawing;

namespace SmartSchool.StudentRelated.Placing.Export
{
    public class NormalExportSheetHandler : IExportSheetHandler
    {

        #region IExportSheetHandler ����

        public void Export(Worksheet sheet, ExcelSheetInfo info)
        {
            sheet.Name = info.Title;
            Cell A1 = sheet.Cells["A1"];
            A1.PutValue(info.Title);
            sheet.Cells.Merge(0, 0, 1, 6);
            A1.Style.HorizontalAlignment = TextAlignmentType.Center;
            A1.Style.Borders.SetColor(Color.Black);

            FormatCell(sheet.Cells["A2"], "�W��");
            FormatCell(sheet.Cells["B2"], "�Z��");
            FormatCell(sheet.Cells["C2"], "�y��");
            FormatCell(sheet.Cells["D2"], "�Ǹ�");
            FormatCell(sheet.Cells["E2"], "�m�W");
            FormatCell(sheet.Cells["F2"], "���Z");

            int rowIndex = 3;
            foreach (PlacingInfo pi in info.PlacingList)
            {
                FormatCell(sheet.Cells["A" + rowIndex], pi.Place.ToString());
                FormatCell(sheet.Cells["B" + rowIndex], pi.Record.ClassName);
                FormatCell(sheet.Cells["C" + rowIndex], pi.Record.SeatNo);
                FormatCell(sheet.Cells["D" + rowIndex], pi.Record.StudentNumber);
                FormatCell(sheet.Cells["E" + rowIndex], pi.Record.StudentName);
                FormatCell(sheet.Cells["F" + rowIndex], GetScore(pi.Score));
                rowIndex++;
            }
        }

        #endregion

        private string GetScore(decimal score)
        {
            score = Math.Round(score, 2, MidpointRounding.ToEven);
            return score.ToString();
        }

        private void FormatCell(Cell cell, string value)
        {
            cell.PutValue(value);
            cell.Style.Borders.SetStyle(CellBorderType.Hair);
            cell.Style.Borders.SetColor(Color.Black);
            cell.Style.Borders.DiagonalStyle = CellBorderType.None;
            cell.Style.HorizontalAlignment = TextAlignmentType.Center;
        }
    }
}
