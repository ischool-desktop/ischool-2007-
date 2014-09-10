using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using DevComponents.DotNetBar;
using Aspose.Cells;
using System.Xml;
using System.IO;
using System.Diagnostics;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.RibbonBars.DeXing
{
    public partial class DemeritStatistic : UserControl, IDeXingExport
    {
        private string[] _classList;
        public DemeritStatistic(string[] classList)
        {
            _classList = classList;
            InitializeComponent();
        }

        #region IDeXingExport 成員
        public Control MainControl
        {
            get { return this.groupPanel1; }
        }

        public void LoadData()
        {
            cboSemester.Items.Add("1");
            cboSemester.Items.Add("2");
            cboSemester.SelectedIndex = 0;

            int schoolYear = SmartSchool.CurrentUser.Instance.SchoolYear;
            for (int i = schoolYear; i > schoolYear - 4; i--)
            {
                cboSchoolYear.Items.Add(i);
            }
            if (cboSchoolYear.Items.Count > 0)
                cboSchoolYear.SelectedIndex = 0;

            rbType1.Checked = true;
        }

        public void Export()
        {
            if (!IsValid()) return;

            // 取得換算原則
            DSResponse d = SmartSchool.Feature.Basic.Config.GetMDReduce();
            if (!d.HasContent)
            {
                MsgBox.Show("取得獎懲換算規則失敗:" + d.GetFault().Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DSXmlHelper h = d.GetContent();
            int ab = int.Parse(h.GetText("Demerit/AB"));
            int bc = int.Parse(h.GetText("Demerit/BC"));
            int wa = int.Parse(txtA.Tag.ToString());
            int wb = int.Parse(txtB.Tag.ToString());
            int wc = int.Parse(txtC.Tag.ToString());
            int want = (wa * ab * bc) + (wb * bc) + wc;

            List<string> _studentIDList = new List<string>();
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets[0];
            string schoolName = SmartSchool.CurrentUser.Instance.SchoolChineseName;
            string A1Name = "";

            string wantString = wa + "大過 " + wb + " 小過 " + wc + " 警告";
            if (rbType1.Checked)
            {
                DSXmlHelper helper = new DSXmlHelper("Request");
                helper.AddElement("Condition");
                foreach (string classid in _classList)
                    helper.AddElement("Condition", "ClassID", classid);

                helper.AddElement("Condition", "SchoolYear", cboSchoolYear.SelectedItem.ToString());
                helper.AddElement("Condition", "Semester", cboSemester.SelectedItem.ToString());
                helper.AddElement("Order");
                helper.AddElement("Order", "GradeYear", "ASC");
                helper.AddElement("Order", "DisplayOrder", "ASC");
                helper.AddElement("Order", "ClassName", "ASC");
                helper.AddElement("Order", "SeatNo", "ASC");
                helper.AddElement("Order", "Name", "ASC");

                DSResponse dsrsp = SmartSchool.Feature.Student.QueryDiscipline.GetDemeritStatistic(new DSRequest(helper));
                if (!dsrsp.HasContent)
                {
                    MsgBox.Show("查詢結果失敗:" + dsrsp.GetFault().Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                helper = dsrsp.GetContent();

                Cell A1 = sheet.Cells["A1"];
                A1.Style.Borders.SetColor(Color.Black);

                A1Name = schoolName + "  " + cboSchoolYear.SelectedItem.ToString() +
                    "學年度第" + cboSemester.SelectedItem.ToString() + "學期 表現特殊學生清單";

                sheet.Name = A1Name;
                A1.PutValue(A1Name);
                A1.Style.HorizontalAlignment = TextAlignmentType.Center;
                sheet.Cells.Merge(0, 0, 1, 7);

                FormatCell(sheet.Cells["A2"], "班級");
                FormatCell(sheet.Cells["B2"], "座號");
                FormatCell(sheet.Cells["C2"], "姓名");
                FormatCell(sheet.Cells["D2"], "學號");
                FormatCell(sheet.Cells["E2"], "大過");
                FormatCell(sheet.Cells["F2"], "小過");
                FormatCell(sheet.Cells["G2"], "警告");
                int index = 1;
                foreach (XmlElement e in helper.GetElements("Student"))
                {
                    string da = e.SelectSingleNode("DemeritA").InnerText;
                    string db = e.SelectSingleNode("DemeritB").InnerText;
                    string dc = e.SelectSingleNode("DemeritC").InnerText;

                    int a, b, c, total;
                    if (!int.TryParse(da, out a)) a = 0;
                    if (!int.TryParse(db, out b)) b = 0;
                    if (!int.TryParse(dc, out c)) c = 0;
                    total = (a * ab * bc) + (b * bc) + c;
                    if (total < want) continue;

                    _studentIDList.Add(e.GetAttribute("StudentID"));

                    int rowIndex = index + 2;
                    FormatCell(sheet.Cells["A" + rowIndex], e.SelectSingleNode("ClassName").InnerText);
                    FormatCell(sheet.Cells["B" + rowIndex], e.SelectSingleNode("SeatNo").InnerText);
                    FormatCell(sheet.Cells["C" + rowIndex], e.SelectSingleNode("Name").InnerText);
                    FormatCell(sheet.Cells["D" + rowIndex], e.SelectSingleNode("StudentNumber").InnerText);
                    FormatCell(sheet.Cells["E" + rowIndex], e.SelectSingleNode("DemeritA").InnerText);
                    FormatCell(sheet.Cells["F" + rowIndex], e.SelectSingleNode("DemeritB").InnerText);
                    FormatCell(sheet.Cells["G" + rowIndex], e.SelectSingleNode("DemeritC").InnerText);
                    index++;
                }

            }
            else // 若統計累計時的處理
            {
                DSXmlHelper helper = new DSXmlHelper("Request");
                helper.AddElement("Condition");
                foreach (string classid in _classList)
                    helper.AddElement("Condition", "ClassID", classid);
                helper.AddElement("Order");
                helper.AddElement("Order", "GradeYear", "ASC");
                helper.AddElement("Order", "DisplayOrder", "ASC");
                helper.AddElement("Order", "ClassName", "ASC");
                helper.AddElement("Order", "SeatNo", "ASC");
                helper.AddElement("Order", "Name", "ASC");
                DSResponse dsrsp = SmartSchool.Feature.Student.QueryDiscipline.GetDemeritStatistic(new DSRequest(helper));
                if (!dsrsp.HasContent)
                {
                    MsgBox.Show("查詢結果失敗:" + dsrsp.GetFault().Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                helper = dsrsp.GetContent();

                Cell A1 = sheet.Cells["A1"];
                A1.Style.Borders.SetColor(Color.Black);

                A1Name = schoolName + "  懲戒累計清單";

                sheet.Name = A1Name;
                A1.PutValue(A1Name);
                A1.Style.HorizontalAlignment = TextAlignmentType.Center;
                sheet.Cells.Merge(0, 0, 1, 7);

                FormatCell(sheet.Cells["A2"], "班級");
                FormatCell(sheet.Cells["B2"], "座號");
                FormatCell(sheet.Cells["C2"], "姓名");
                FormatCell(sheet.Cells["D2"], "學號");
                FormatCell(sheet.Cells["E2"], "大過");
                FormatCell(sheet.Cells["F2"], "小過");
                FormatCell(sheet.Cells["G2"], "警告");

                int index = 3;
                foreach (XmlElement e in helper.GetElements("Student"))
                {
                    _studentIDList.Add(e.GetAttribute("StudentID"));
                    string da = e.SelectSingleNode("DemeritA").InnerText;
                    string db = e.SelectSingleNode("DemeritB").InnerText;
                    string dc = e.SelectSingleNode("DemeritC").InnerText;

                    int a, b, c, total;
                    if (!int.TryParse(da, out a)) a = 0;
                    if (!int.TryParse(db, out b)) b = 0;
                    if (!int.TryParse(dc, out c)) c = 0;
                    total = (a * ab * bc) + (b * bc) + c;
                    if (total < want) continue;

                    FormatCell(sheet.Cells["A" + index], e.SelectSingleNode("ClassName").InnerText);
                    FormatCell(sheet.Cells["B" + index], e.SelectSingleNode("SeatNo").InnerText);
                    FormatCell(sheet.Cells["C" + index], e.SelectSingleNode("Name").InnerText);
                    FormatCell(sheet.Cells["D" + index], e.SelectSingleNode("StudentNumber").InnerText);
                    FormatCell(sheet.Cells["E" + index], e.SelectSingleNode("DemeritA").InnerText);
                    FormatCell(sheet.Cells["F" + index], e.SelectSingleNode("DemeritB").InnerText);
                    FormatCell(sheet.Cells["G" + index], e.SelectSingleNode("DemeritC").InnerText);
                    index++;
                }
            }

            h = new DSXmlHelper("Request");
            h.AddElement("Field");
            h.AddElement("Field", "All");
            h.AddElement("Condition");
            h.AddElement("Condition", "Or");
            h.AddElement("Condition/Or", "MeritFlag", "0");
            h.AddElement("Condition/Or", "MeritFlag", "2");
            
            h.AddElement("Condition", "RefStudentID", "-1"); //這真是絕招!!
            foreach (string sid in _studentIDList)
                h.AddElement("Condition", "RefStudentID", sid);
            if (rbType1.Checked)
            {
                h.AddElement("Condition", "SchoolYear", cboSchoolYear.Text);
                h.AddElement("Condition", "Semester", cboSemester.Text);
            }
            h.AddElement("Order");
            h.AddElement("Order", "GradeYear", "ASC");
            h.AddElement("Order", "ClassDisplayOrder", "ASC");
            h.AddElement("Order", "ClassName", "ASC");
            h.AddElement("Order", "SeatNo", "ASC");
            h.AddElement("Order", "RefStudentID", "ASC");
            h.AddElement("Order", "OccurDate", "ASC");

            d = SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(h));
            if (!d.HasContent)
            {
                MsgBox.Show("取得明細資料錯誤:" + d.GetFault().Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            h = d.GetContent();
            book.Worksheets.Add();
            sheet = book.Worksheets[book.Worksheets.Count - 1];
            sheet.Name = schoolName + "懲戒累計明細";
            Cell titleCell = sheet.Cells["A1"];
            titleCell.Style.Borders.SetColor(Color.Black);

            titleCell.PutValue(sheet.Name);
            titleCell.Style.HorizontalAlignment = TextAlignmentType.Center;
            sheet.Cells.Merge(0, 0, 1, 7);

            FormatCell(sheet.Cells["A2"], "班級");
            FormatCell(sheet.Cells["B2"], "座號");
            FormatCell(sheet.Cells["C2"], "姓名");
            FormatCell(sheet.Cells["D2"], "學號");
            FormatCell(sheet.Cells["E2"], "學年度");
            FormatCell(sheet.Cells["F2"], "學期");
            FormatCell(sheet.Cells["G2"], "日期");
            FormatCell(sheet.Cells["H2"], "大過");
            FormatCell(sheet.Cells["I2"], "小過");
            FormatCell(sheet.Cells["J2"], "警告");
            FormatCell(sheet.Cells["K2"], "留察");
            FormatCell(sheet.Cells["L2"], "事由");

            int ri = 3;
            foreach (XmlElement e in h.GetElements("Discipline"))
            {
                FormatCell(sheet.Cells["A" + ri], e.SelectSingleNode("ClassName").InnerText);
                FormatCell(sheet.Cells["B" + ri], e.SelectSingleNode("SeatNo").InnerText);
                FormatCell(sheet.Cells["C" + ri], e.SelectSingleNode("Name").InnerText);
                FormatCell(sheet.Cells["D" + ri], e.SelectSingleNode("StudentNumber").InnerText);
                FormatCell(sheet.Cells["E" + ri], e.SelectSingleNode("SchoolYear").InnerText);
                FormatCell(sheet.Cells["F" + ri], e.SelectSingleNode("Semester").InnerText);
                FormatCell(sheet.Cells["G" + ri], e.SelectSingleNode("OccurDate").InnerText);
                FormatCell(sheet.Cells["H" + ri], e.SelectSingleNode("Detail/Discipline/Demerit/@A").InnerText);
                FormatCell(sheet.Cells["I" + ri], e.SelectSingleNode("Detail/Discipline/Demerit/@B").InnerText);
                FormatCell(sheet.Cells["J" + ri], e.SelectSingleNode("Detail/Discipline/Demerit/@C").InnerText);
                FormatCell(sheet.Cells["K" + ri], e.SelectSingleNode("MeritFlag").InnerText == "2" ? "是" : "否");
                FormatCell(sheet.Cells["L" + ri], e.SelectSingleNode("Reason").InnerText);
                ri++;
            }


            string path = Path.Combine(Application.StartupPath, "Reports");

            //如果目錄不存在則建立。
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            path = Path.Combine(path, ConvertToValidName(A1Name) + ".xls");
            try
            {
                book.Save(path);
            }
            catch (Exception ex)
            {
                MsgBox.Show("檔案儲存失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MsgBox.Show("檔案開啟失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        #endregion

        private bool IsValid()
        {
            error.Clear();
            error.Tag = true;
            ValidInt(txtA, txtA, "必須填入數字");
            ValidInt(txtB, txtB, "必須填入數字");
            ValidInt(txtC, txtC, "必須填入數字");

            return bool.Parse(error.Tag.ToString());
        }

        private void ValidInt(Control intControl, Control showErrorControl, string message)
        {
            int i = 0;
            intControl.Tag = "0";
            if (intControl.Text == string.Empty) return;
            if (!int.TryParse(intControl.Text, out i))
            {
                error.SetError(showErrorControl, message);
                error.Tag = false;
            }
            intControl.Tag = intControl.Text;
        }

        private void FormatCell(Cell cell, string value)
        {
            cell.PutValue(value);
            cell.Style.Borders.SetStyle(CellBorderType.Hair);
            cell.Style.Borders.SetColor(Color.Black);
            cell.Style.Borders.DiagonalStyle = CellBorderType.None;
            cell.Style.HorizontalAlignment = TextAlignmentType.Center;
        }

        private void FormatCellWithStandard(Cell cell, string value, string standard)
        {
            int v = 0, s = 0;
            if (!int.TryParse(value, out v)) v = 0;
            if (!int.TryParse(standard, out s)) s = -1;
            if (v >= s && s != -1)
                cell.Style.Font.Color = Color.Red;
            cell.PutValue(value);
            cell.Style.Borders.SetStyle(CellBorderType.Hair);
            cell.Style.Borders.SetColor(Color.Black);
            cell.Style.Borders.DiagonalStyle = CellBorderType.None;
            cell.Style.HorizontalAlignment = TextAlignmentType.Center;
        }

        private string ConvertToValidName(string A1Name)
        {
            char[] invalids = Path.GetInvalidFileNameChars();

            string result = A1Name;
            foreach (char each in invalids)
                result = result.Replace(each, '_');

            return result;
        }
    }
}
