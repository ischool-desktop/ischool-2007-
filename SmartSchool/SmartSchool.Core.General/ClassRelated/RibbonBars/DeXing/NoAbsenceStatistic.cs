using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using System.Xml;
using Aspose.Cells;
using System.IO;
using DevComponents.DotNetBar;
using System.Diagnostics;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.RibbonBars.DeXing
{
    public partial class NoAbsenceStatistic : UserControl, IDeXingExport
    {
        private string[] _classidList;
        private string _schoolYear;
        private string _semester;

        public NoAbsenceStatistic(string[] classidList)
        {
            InitializeComponent();
            _classidList = classidList;
        }

        #region IDeXingExport ����

        public Control MainControl
        {
            get { return this.groupPanel1; }
        }

        public void LoadData()
        {
            cboSemester.Items.Add("1");
            cboSemester.Items.Add("2");
            cboSemester.SelectedIndex = SmartSchool.CurrentUser.Instance.Semester - 1;
       
            int schoolYear = SmartSchool.CurrentUser.Instance.SchoolYear;
            for (int i = schoolYear; i > schoolYear - 4; i--)
            {
                cboSchoolYear.Items.Add(i);
            }
            if (cboSchoolYear.Items.Count > 0)
                cboSchoolYear.SelectedIndex = 0;
            
            //_schoolYear = SmartSchool.Common.CurrentUser.Instance.SchoolYear.ToString();
            //_semester = SmartSchool.Common.CurrentUser.Instance.Semester.ToString();
          
            //checkBoxX1.Text = checkBoxX1.Text.Replace("@@", _schoolYear).Replace("!!", _semester);
            checkBoxX1.Checked = true;
        }

        public void Export()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");

            foreach (string id in _classidList)
            {
                helper.AddElement(".", "ClassID", id);
            }
            if (checkBoxX1.Checked)
            {
                helper.AddElement(".", "SchoolYear", cboSchoolYear.SelectedItem.ToString());
                helper.AddElement(".", "Semester", cboSemester.SelectedItem.ToString());
            }

            DSResponse dsrsp = SmartSchool.Feature.Student.QueryAttendance.GetNoAbsenceStatistic(new DSRequest(helper));
            if (!dsrsp.HasContent)
            {
                MsgBox.Show("���o�^�и�ƥ���:" + dsrsp.GetFault().Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DSXmlHelper rsp = dsrsp.GetContent();
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets[0];

            string schoolName = SmartSchool.CurrentUser.Instance.SchoolChineseName;
            Cell A1 = sheet.Cells["A1"];
            A1.Style.Borders.SetColor(Color.Black);
            string A1Name = schoolName + "  ";
            if (checkBoxX1.Checked)
            {
                A1Name += cboSchoolYear.SelectedItem.ToString() + "�Ǧ~�ײ�" + cboSemester.SelectedItem.ToString() + "�Ǵ� ";
            }
            A1Name += "���ԾǥͲM��";
            sheet.Name = A1Name;
            A1.PutValue(A1Name);
            A1.Style.HorizontalAlignment = TextAlignmentType.Center;
            sheet.Cells.Merge(0, 0, 1, 5);

            FormatCell(sheet.Cells["A2"], "�s��");
            FormatCell(sheet.Cells["B2"], "�Z��");
            FormatCell(sheet.Cells["C2"], "�y��");
            FormatCell(sheet.Cells["D2"], "�m�W");
            FormatCell(sheet.Cells["E2"], "�Ǹ�");

            int index = 1;
            foreach (XmlElement e in rsp.GetElements("Student"))
            {
                int rowIndex = index + 2;
                FormatCell(sheet.Cells["A" + rowIndex], index.ToString());
                FormatCell(sheet.Cells["B" + rowIndex], e.GetAttribute("ClassName"));
                FormatCell(sheet.Cells["C" + rowIndex], e.GetAttribute("SeatNo"));
                FormatCell(sheet.Cells["D" + rowIndex], e.GetAttribute("Name"));
                FormatCell(sheet.Cells["E" + rowIndex], e.GetAttribute("StudentNumber"));
                index++;
            }
            string path = Path.Combine(Application.StartupPath, "Reports");
            path = Path.Combine(path, A1Name + ".xls");
            try
            {
                book.Save(path);
            }
            catch (IOException)
            {
                try
                {
                    FileInfo file = new FileInfo(path);
                    string nameTempalte = file.FullName.Replace(file.Extension, "") + "{0}.xls";
                    int count = 1;
                    string fileName = string.Format(nameTempalte, count);
                    while (File.Exists(fileName))
                        fileName = string.Format(nameTempalte, count++);

                    book.Save(fileName);
                    path = fileName;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("�ɮ��x�s����:" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("�ɮ��x�s����:" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MsgBox.Show("�ɮ׶}�ҥ���:" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void FormatCell(Cell cell, string value)
        {
            cell.PutValue(value);
            cell.Style.Borders.SetStyle(CellBorderType.Hair);
            cell.Style.Borders.SetColor(Color.Black);
            cell.Style.Borders.DiagonalStyle = CellBorderType.None;
            cell.Style.HorizontalAlignment = TextAlignmentType.Center;
        }

        #endregion


    }
}
