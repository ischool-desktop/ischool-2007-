using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool;

namespace DataManager
{
    public partial class Discipline : BaseForm
    {
        private DateTime _startDate;
        private DateTime _endDate;

        private bool Waiting
        {
            set { picWaiting.Visible = value; }
        }

        private BackgroundWorker _loader;

        public Discipline()
        {
            InitializeComponent();
            InitialBackgroundWorker();
            InitialDate();
        }

        private void InitialDate()
        {
            txtEndDate.Text = txtStartDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
        }

        private void InitialBackgroundWorker()
        {
            _loader = new BackgroundWorker();
            _loader.DoWork += new DoWorkEventHandler(_loader_DoWork);
            _loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_loader_RunWorkerCompleted);
        }

        private void _loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result is DSXmlHelper && e.Result != null)
                    FillDataGridView(e.Result as DSXmlHelper);
            }
            Waiting = false;
        }

        private void _loader_DoWork(object sender, DoWorkEventArgs e)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "StartDate", _startDate.ToString("yyyy/MM/dd"));
            helper.AddElement("Condition", "EndDate", _endDate.ToString("yyyy/MM/dd"));
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate");
            helper.AddElement("Order", "RefStudentID");

            try
            {
                DSResponse dsrsp = SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper));
                e.Result = dsrsp.GetContent();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show("取得獎懲失敗。");
            }
        }

        private void FillDataGridView(DSXmlHelper helper)
        {
            dataGridViewX1.SuspendLayout();

            foreach (XmlElement dis in helper.GetElements("Discipline"))
            {
                DSXmlHelper disHelper = new DSXmlHelper(dis);

                string discipline = GetDisciplineString(disHelper);

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1,
                    disHelper.GetText("@ID"),
                    disHelper.GetText("RefStudentID"),
                    disHelper.GetText("OccurDate"),
                    disHelper.GetText("ClassName"),
                    disHelper.GetText("SeatNo"),
                    disHelper.GetText("StudentNumber"),
                    disHelper.GetText("Name"),
                    disHelper.GetText("Gender"),
                    disHelper.GetText("SchoolYear"),
                    disHelper.GetText("Semester"),
                    "",//info.OccurPlace,
                    discipline,
                    disHelper.GetText("MeritFlag"),
                    "",//info.AwardA,
                    "",//info.AwardB,
                    "",//info.AwardC,
                    "",//info.FaultA,
                    "",//info.FaultB,
                    "",//info.FaultC,
                    disHelper.GetText("Reason"),
                    "",//info.Cleared,
                    "",//info.ClearDate,
                    "",//info.ClearReason,
                    ""//info.UltimateAdmonition
                );
                row.Tag = disHelper;
                dataGridViewX1.Rows.Add(row);
            }

            dataGridViewX1.ResumeLayout();
        }

        private string GetDisciplineString(DSXmlHelper dis)
        {
            string result = "";

            if (dis.GetText("MeritFlag") == "1")
            {
                XmlElement merit = dis.GetElement("Detail/Discipline/Merit");
                int a, b, c;
                if (int.TryParse(merit.GetAttribute("A"), out a) && a > 0)
                    result += string.Format("大功:{0}", a);
                if (int.TryParse(merit.GetAttribute("B"), out b) && b > 0)
                {
                    if (!string.IsNullOrEmpty(result)) result += ",";
                    result += string.Format("小功:{0}", b);
                }
                if (int.TryParse(merit.GetAttribute("C"), out c) && c > 0)
                {
                    if (!string.IsNullOrEmpty(result)) result += ",";
                    result += string.Format("嘉獎:{0}", c);
                }
            }
            else if (dis.GetText("MeritFlag") == "0")
            {
                XmlElement demerit = dis.GetElement("Detail/Discipline/Demerit");
                int a, b, c;
                if (int.TryParse(demerit.GetAttribute("A"), out a) && a > 0)
                    result += string.Format("大過:{0}", a);
                if (int.TryParse(demerit.GetAttribute("B"), out b) && b > 0)
                {
                    if (!string.IsNullOrEmpty(result)) result += ",";
                    result += string.Format("小過:{0}", b);
                }
                if (int.TryParse(demerit.GetAttribute("C"), out c) && c > 0)
                {
                    if (!string.IsNullOrEmpty(result)) result += ",";
                    result += string.Format("警告:{0}", c);
                }
            }
            else if (dis.GetText("MeritFlag") == "2")
            {
                result = "留校察看";
            }

            return result;
        }

        private bool IsDateTime(string date)
        {
            DateTime try_value;
            if (DateTime.TryParse(date, out try_value))
                return true;
            return false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bool valid = true;
            errorProvider1.Clear();

            if (!IsDateTime(txtStartDate.Text))
            {
                errorProvider1.SetError(txtStartDate, "日期格式錯誤");
                valid = false;
            }
            else
                _startDate = DateTime.Parse(txtStartDate.Text);

            if (!IsDateTime(txtEndDate.Text))
            {
                errorProvider1.SetError(txtEndDate, "日期格式錯誤");
                valid = false;
            }
            else
                _endDate = DateTime.Parse(txtEndDate.Text);

            if (valid && !_loader.IsBusy)
            {
                dataGridViewX1.Rows.Clear();
                //Waiting = true;
                _loader.RunWorkerAsync();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count <= 0) return;

            #region 檢查選取獎懲的類型是否一致 (MeritFlag一致)

            DataGridViewRow firstRow = dataGridViewX1.SelectedRows[dataGridViewX1.SelectedRows.Count - 1];
            string firstFlag = "" + firstRow.Cells[colMeritFlag.Index].Value;
            foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
            {
                if (row == firstRow) continue;
                if ("" + row.Cells[colMeritFlag.Index].Value != firstFlag)
                {
                    MsgBox.Show("請選取相同獎懲類別之獎懲記錄。", "錯誤");
                    return;
                }
            }

            #endregion

            #region 檢查選取的獎懲

            bool warning = false;

            DSXmlHelper helper = firstRow.Tag as DSXmlHelper;

            string firstReason = "" + firstRow.Cells[colReason.Index].Value;
            string firstDiscipline = GetDisciplineString(helper);
            List<string> list = new List<string>();

            foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
            {
                list.Add("" + row.Cells[colID.Index].Value);
                if (row == firstRow) continue;
                if ("" + row.Cells[colReason.Index].Value != firstReason || "" + row.Cells[colDisciplineCount.Index].Value != firstDiscipline)
                    warning = true;
            }

            if (warning)
                MsgBox.Show("您所選擇的獎懲記錄中包含不同獎懲次數或事由，\n系統將會以您第一筆點選的獎懲記錄為主。", "警告");

            #endregion

            ModifyForm form = new ModifyForm(helper, list);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dataGridViewX1.SuspendLayout();
                string reason = form.NewReason;
                string discipline = GetDisciplineString(form.Helper);
                foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
                {
                    row.Cells[colReason.Index].Value = reason;
                    row.Cells[colDisciplineCount.Index].Value = discipline;
                }
                dataGridViewX1.ResumeLayout();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            DataGridViewExport export = new DataGridViewExport(dataGridViewX1);
            export.Save(saveFileDialog1.FileName);

            if (new CompleteForm().ShowDialog() == DialogResult.Yes)
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
        }
    }
}
