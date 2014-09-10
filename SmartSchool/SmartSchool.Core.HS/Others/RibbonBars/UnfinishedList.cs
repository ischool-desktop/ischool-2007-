using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using SmartSchool.CourseRelated;
using SmartSchool.CourseRelated.Forms;
using Aspose.Cells;
using System.IO;
using System.Diagnostics;

namespace SmartSchool.Others.RibbonBars
{
    public partial class UnfinishedList : BaseForm
    {
        public UnfinishedList()
        {
            InitializeComponent();

            intSchoolYear.MinValue = CurrentUser.Instance.SchoolYear - 50;
            intSchoolYear.MaxValue = CurrentUser.Instance.SchoolYear + 3;

            intSemester.MinValue = 1;
            intSemester.MaxValue = 2;

            intSchoolYear.Value = CurrentUser.Instance.SchoolYear;
            intSemester.Value = CurrentUser.Instance.Semester;
        }

        private void UnfinishedList_Load(object sender, EventArgs e)
        {
            // 載入試別
            XmlElement element = SmartSchool.Feature.Exam.QueryExam.GetAbstractList();
            foreach (XmlNode node in element.SelectNodes("Exam"))
            {
                string key = node.Attributes["ID"].Value;
                string name = node.SelectSingleNode("ExamName").InnerText;
                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key, name);
                cboExam.Items.Add(kvp);
            }
            cboExam.DisplayMember = "Value";
            cboExam.ValueMember = "Key";

            // 載入P什麼什麼的是不是有勾
            bool ischecked = true;
            XmlElement info = SmartSchool.CurrentUser.Instance.Preference["UnfinishedScoreList_Display"];
            if (info != null)
            {
                if (!bool.TryParse(info.InnerText, out ischecked))
                    ischecked = true;
            }
            chkDisplay.Checked = ischecked;

            if (cboExam.Items.Count > 0)
                cboExam.SelectedIndex = 0;
        }

        private Dictionary<string, ListViewItem> list = new Dictionary<string, ListViewItem>();
        private void cboExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void intSchoolYear_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void intSemester_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void chkDisplay_CheckedChanged(object sender, EventArgs e)
        {
            CheckDisplay();
        }

        private void CheckDisplay()
        {
            listView.SuspendLayout();
            listView.Items.Clear();
            foreach (ListViewItem item in list.Values)
            {
                ScoreDisplay sd1 = item.SubItems[3].Tag as ScoreDisplay;
                ScoreDisplay sd2 = item.SubItems[4].Tag as ScoreDisplay;
                if (!sd1.IsFinished() || !sd2.IsFinished() || !chkDisplay.Checked)
                    listView.Items.Add(item);
            }
            listView.ResumeLayout();
            XmlElement element = SmartSchool.CurrentUser.Instance.Preference["UnfinishedScoreList_Display"];
            if (element == null)
            {
                DSXmlHelper helper = new DSXmlHelper("UnfinishedScoreList_Display");
                element = helper.BaseElement;
            }
            element.InnerText = chkDisplay.Checked.ToString();
            SmartSchool.CurrentUser.Instance.Preference["UnfinishedScoreList_Display"] = element;
        }

        private void LoadData()
        {
            if (cboExam.SelectedItem == null) return;
            KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)cboExam.SelectedItem;
            DSResponse dsrsp = SmartSchool.Feature.Exam.QueryExam.GetCourseBelong(kvp.Key, intSchoolYear.Value, intSemester.Value);
            if (!dsrsp.HasContent)
            {
                MsgBox.Show("查詢資料錯誤:" + dsrsp.GetFault().Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listView.Items.Clear();
            DSXmlHelper helper = dsrsp.GetContent();
            list.Clear();
            listView.SuspendLayout();
            foreach (XmlElement element in helper.GetElements("Course"))
            {
                //if (element.SelectSingleNode("InputRequired").InnerText == "否") continue;

                string courseid = element.SelectSingleNode("CourseID").InnerText;
                ListViewItem item = new ListViewItem(element.SelectSingleNode("CourseName").InnerText);
                //ListViewItem item = listView.Items.Add(element.SelectSingleNode("CourseName").InnerText);
                item.Tag = courseid;
                list.Add(courseid, item);

                string inputRequired = element.SelectSingleNode("InputRequired").InnerText;
                if (inputRequired == "1")
                    item.SubItems.Add("強制");
                else
                    item.SubItems.Add("不強制");

                item.SubItems.Add(element.SelectSingleNode("TeacherName").InnerText);
                    
                string totalScoreCount = element.SelectSingleNode("TotalScoreCount").InnerText;
                string finishedScoreCount = element.SelectSingleNode("FinishedScoreCount").InnerText;
                string totalTextScoreCount = element.SelectSingleNode("TotalTextScoreCount").InnerText;
                string finishedTextScoreCount = element.SelectSingleNode("FinishedTextScoreCount").InnerText;

                int tsc = int.Parse(totalScoreCount);
                int ttc = int.Parse(totalTextScoreCount);
                int fsc = int.Parse(finishedScoreCount);
                int ftc = int.Parse(finishedTextScoreCount);

                System.Windows.Forms.ListViewItem.ListViewSubItem subitem1 = item.SubItems.Add("");
                ScoreDisplay sd1 = new ScoreDisplay(tsc, subitem1);
                subitem1.Tag = sd1;
                sd1.SetScored(fsc);

                if (!sd1.IsFinished())
                    subitem1.ForeColor = Color.Red;

                System.Windows.Forms.ListViewItem.ListViewSubItem subitem2 = item.SubItems.Add("");
                ScoreDisplay sd2 = new ScoreDisplay(ttc, subitem2);
                subitem2.Tag = sd2;
                sd2.SetScored(ftc);

                if (!sd2.IsFinished())
                    subitem2.ForeColor = Color.Red;
            }
            listView.ResumeLayout();
            CheckDisplay();
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView.SelectedItems.Count == 0) return;
            ListViewItem item = listView.FocusedItem;
            if (item.Tag == null) return;

            CourseInformation info = CourseEntity.Instance.FindByCourseID(int.Parse(item.Tag.ToString()));
            if (info == null) return;

            EditCourseScore.DisplayCourseScore(info);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets[0];
            string schoolyear = SmartSchool.CurrentUser.Instance.SchoolYear.ToString();
            string semester = SmartSchool.CurrentUser.Instance.Semester.ToString();
            string schoolName = SmartSchool.CurrentUser.Instance.SchoolChineseName;
            Cell A1 = sheet.Cells["A1"];
            A1.Style.Borders.SetColor(Color.Black);
            A1.PutValue(schoolyear + "學年度第" + semester + "學期  " + schoolName + " " + cboExam.Text + " 成績未完成輸入清單");
            A1.Style.HorizontalAlignment = TextAlignmentType.Center;
            sheet.Cells.Merge(0, 0, 1, 4);


            FormatCell(sheet.Cells["A3"], "課程名稱");
            FormatCell(sheet.Cells["B3"], "授課教師");
            FormatCell(sheet.Cells["C3"], "分數評量輸入狀態");
            FormatCell(sheet.Cells["D3"], "文字評量輸入狀態");

            int index = 4;
            foreach (ListViewItem item in listView.Items)
            {
                Cell A = sheet.Cells["A" + index];
                A.PutValue(item.Text);
                A.Style.HorizontalAlignment = TextAlignmentType.Left;

                Cell B = sheet.Cells["B" + index];
                B.PutValue(item.SubItems[1].Text);
                B.Style.HorizontalAlignment = TextAlignmentType.Left;

                Cell C = sheet.Cells["C" + index];
                C.PutValue(item.SubItems[2].Text);
                C.Style.HorizontalAlignment = TextAlignmentType.Center;

                Cell D = sheet.Cells["D" + index];
                D.PutValue(item.SubItems[3].Text);
                D.Style.HorizontalAlignment = TextAlignmentType.Center;

                index++;
            }
            sheet.AutoFitColumns();

            try
            {
                string path = Path.Combine(Application.StartupPath, "Reports");
                DirectoryInfo dinfo = new DirectoryInfo(path);
                if (!dinfo.Exists)
                    dinfo.Create();
                string fileName = Path.Combine(path, A1.Value.ToString() + ".xls");
                book.Save(fileName, FileFormatType.Excel2000);
                Process.Start(fileName);
            }
            catch (Exception ex)
            {
                MsgBox.Show("儲存檔案失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }

    class ScoreDisplay
    {
        private int _scored;
        private int _total;
        private System.Windows.Forms.ListViewItem.ListViewSubItem _subitem;
        public ScoreDisplay(int total, System.Windows.Forms.ListViewItem.ListViewSubItem subitem)
        {
            _scored = 0;
            _subitem = subitem;
            _total = total;
            _subitem.Text = Display();
        }

        public void SetScored(int scored)
        {
            _scored = scored;
            _subitem.Text = Display();
        }

        public bool IsFinished()
        {
            if (_total == 0 || _total == _scored)
                return true;
            return false;
        }

        public string Display()
        {
            if (_total == 0)
                return "--/--";
            return string.Format("{0}/{1}", _scored, _total);
        }
    }
}