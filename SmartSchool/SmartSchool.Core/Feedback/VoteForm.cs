using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feedback.Feature;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Feedback
{
    internal partial class VoteForm : BaseForm
    {
        private Mode _mode = Mode.User;
        public Mode CurrentMode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        private DataGridViewColumn colUrl = null;
        private DataGridViewColumn colCompleted = null;
        private DateTime _serverDate = DateTime.MinValue;
        private DateTime _lastVoteDate = DateTime.MinValue.Date;
        private string _lastVoteFunctionID = "";
        private List<string> _order_list = new List<string>();

        private bool Voted
        {
            get { return (_lastVoteDate.Date >= _serverDate.Date); }
        }

        public VoteForm()
        {
            InitializeComponent();

            if (Control.ModifierKeys == Keys.Shift)
            {
                AuthBox auth = new AuthBox();
                if (auth.ShowDialog() == DialogResult.OK)
                    CurrentMode = Mode.Admin;
            }

            AdjustUI();
        }

        private void AdjustUI()
        {
            if (CurrentMode == Mode.Admin)
            {
                colUrl = new DataGridViewTextBoxColumn();
                colUrl.HeaderText = "詳細說明";
                colUrl.Name = "colUrl";
                colUrl.ReadOnly = false;
                colUrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                colCompleted = new DataGridViewCheckBoxColumn();
                colCompleted.HeaderText = "是否完成";
                colCompleted.Name = "colCompleted";
                colCompleted.ReadOnly = true;
                colCompleted.Width = 70;
                dgFunctionVote.AllowUserToAddRows = true;
                dgFunctionVote.AllowUserToDeleteRows = true;
                dgFunctionVote.Columns[colDesc.Name].ReadOnly = false;
                dgFunctionVote.Columns[colECD.Name].ReadOnly = false;
                colVote.Visible = false;
                if (DSA.Instance.IsDev) this.Text += " (測試用)";
            }
            else
            {
                colUrl = new DataGridViewLinkColumn();
                colUrl.HeaderText = "詳細說明";
                colUrl.Name = "colUrl";
                colUrl.ReadOnly = true;
                colUrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                colUrl.SortMode = DataGridViewColumnSortMode.Automatic;
                //colUrl.Width = 85;
                (colUrl as DataGridViewLinkColumn).ActiveLinkColor = System.Drawing.Color.Blue;
                (colUrl as DataGridViewLinkColumn).LinkColor = System.Drawing.Color.Blue;
                (colUrl as DataGridViewLinkColumn).VisitedLinkColor = System.Drawing.Color.Blue;
                colCompleted = new DataGridViewTextBoxColumn();
                colCompleted.HeaderText = "是否完成";
                colCompleted.Name = "colCompleted";
                colCompleted.ReadOnly = true;
                //colCompleted.Width = 85;
            }

            dgFunctionVote.Columns.Insert(3, colUrl);
            dgFunctionVote.Columns.Insert(6, colCompleted);
        }

        private void VoteForm_Load(object sender, EventArgs e)
        {
            GetServerDateTime();
            LoadPreference();
            LoadFunctions();
        }

        private void GetServerDateTime()
        {
            try
            {
                DSXmlHelper helper = Service.GetDateTimeNow();
                DateTime a;
                if (!DateTime.TryParse(helper.GetText("DateTime"), out a))
                    _serverDate = DateTime.Now;
                _serverDate = a;
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                _serverDate = DateTime.Now;
            }
        }

        private void LoadPreference()
        {
            XmlElement p = CurrentUser.Instance.Preference["Vote"];
            if (p != null)
            {
                DateTime a;
                if (DateTime.TryParse(p.GetAttribute("LastVoteDate"), out a))
                    _lastVoteDate = a.Date;
                _lastVoteFunctionID = p.GetAttribute("LastVoteFunctionID");
            }
        }

        private void SavePreference()
        {
            XmlElement p = CurrentUser.Instance.Preference["Vote"];
            if (p == null)
            {
                p = new XmlDocument().CreateElement("Vote");
            }
            p.SetAttribute("LastVoteDate", "" + _serverDate.Date);
            p.SetAttribute("LastVoteFunctionID", _lastVoteFunctionID);
            CurrentUser.Instance.Preference["Vote"] = p;
        }

        private void KeepOrder()
        {
            _order_list.Clear();
            foreach (DataGridViewRow row in dgFunctionVote.Rows)
            {
                if (row.IsNewRow) continue;
                _order_list.Add(row.Tag as string);
            }
        }

        private void LoadFunctions()
        {
            try
            {
                dgFunctionVote.Rows.Clear();
                dgFunctionVote.SuspendLayout();

                List<DataGridViewRow> rows = new List<DataGridViewRow>();

                bool markLastVote = false;

                #region 如果投過票就不顯示
                if (Voted)
                {
                    markLastVote = true;
                }
                else
                {
                    colVote.ActiveLinkColor = Color.Blue;
                    colVote.LinkColor = Color.Blue;
                    colVote.VisitedLinkColor = Color.Blue;
                }
                #endregion

                DSXmlHelper helper;
                try
                {
                    helper = Service.GetFunction();
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    helper = new DSXmlHelper("BOOM");
                }

                foreach (XmlElement func in helper.GetElements("Function"))
                {
                    DSXmlHelper funcHelper = new DSXmlHelper(func);

                    int number;
                    int.TryParse(funcHelper.GetText("NumberOfVotes"), out number);

                    bool isLastVote = markLastVote && _lastVoteFunctionID == funcHelper.GetText("@ID");

                    bool completed = false;
                    if (funcHelper.GetText("Completed") == "1") completed = true;

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgFunctionVote,
                        funcHelper.GetText("@ID"),
                        (Voted || completed) ? "" : "投票",
                        funcHelper.GetText("Description"),
                        (CurrentMode == Mode.User) ? (string.IsNullOrEmpty(funcHelper.GetText("Url")) ? "" : "檢視") : funcHelper.GetText("Url"),
                        number,
                        funcHelper.GetText("EstimatedCompletionDate"));
                    if(CurrentMode == Mode.User)
                        row.Cells[colCompleted.Index].Value = (completed ? "已完成" : "");
                    else
                        row.Cells[colCompleted.Index].Value = completed;
                    //dgFunctionVote.Rows.Add(row);
                    row.Tag = row.Cells[colVote.Index].Tag = funcHelper.GetText("@ID");
                    row.Cells[colUrl.Index].Tag = funcHelper.GetText("Url");

                    if (CurrentMode == Mode.User && isLastVote)
                    {
                        row.Cells[colVote.Index].Style.BackColor = Color.Yellow;
                        row.Cells[colDesc.Index].Style.BackColor = Color.Yellow;
                        row.Cells[colUrl.Index].Style.BackColor = Color.Yellow;
                        row.Cells[colNumOfVotes.Index].Style.BackColor = Color.Yellow;
                        row.Cells[colECD.Index].Style.BackColor = Color.Yellow;
                        row.Cells[colCompleted.Index].Style.BackColor = Color.Yellow;
                    }

                    if (CurrentMode == Mode.User && completed)
                    {
                        row.Cells[colVote.Index].Style.ForeColor = Color.Gray;
                        row.Cells[colDesc.Index].Style.ForeColor = Color.Gray;
                        row.Cells[colUrl.Index].Style.ForeColor = Color.Gray;
                        row.Cells[colNumOfVotes.Index].Style.ForeColor = Color.Gray;
                        row.Cells[colECD.Index].Style.ForeColor = Color.Gray;
                        row.Cells[colCompleted.Index].Style.ForeColor = Color.Gray;
                    }

                    rows.Add(row);
                }

                if (_order_list.Count > 0)
                    rows.Sort(SortDataGridViewRow);

                foreach (DataGridViewRow row in rows)
                    dgFunctionVote.Rows.Add(row);

                dgFunctionVote.ClearSelection();
                dgFunctionVote.ResumeLayout();
            }
            catch (Exception ex)
            {
                dgFunctionVote.Rows.Clear();
                dgFunctionVote.ResumeLayout();
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgFunctionVote.EndEdit();
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            dgFunctionVote.BeginEdit(false);
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (CurrentMode == Mode.User && dgFunctionVote.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null)
            {
                if (dgFunctionVote.Columns[e.ColumnIndex] == colUrl)
                {
                    string url = dgFunctionVote.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
                    try
                    {
                        System.Diagnostics.Process.Start(url);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
                else if (dgFunctionVote.Columns[e.ColumnIndex] == colVote && !Voted)
                {
                    if (MsgBox.Show("您確定要投「" + ("" + dgFunctionVote.Rows[e.RowIndex].Cells[colDesc.Name].Value) + "」一票嗎？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string user = CurrentUser.Instance.AccessPoint + "/" + CurrentUser.Instance.UserName;
                        string id = dgFunctionVote.Rows[e.RowIndex].Tag as string;

                        if (string.IsNullOrEmpty(id))
                            return;

                        try
                        {
                            Service.InsertVote(user, id);
                            _lastVoteDate = _serverDate.Date;
                            _lastVoteFunctionID = id;
                            SavePreference();
                            KeepOrder();
                            LoadFunctions();
                        }
                        catch (Exception ex)
                        {
                            CurrentUser.ReportError(ex);
                            MsgBox.Show(ex.Message);
                        }
                    }
                }
            }

            if (CurrentMode == Mode.Admin && dgFunctionVote.Columns[e.ColumnIndex] == colCompleted)
            {
                DataGridViewRow row = dgFunctionVote.Rows[e.RowIndex];
                if (!row.IsNewRow)
                {
                    row.Cells[colCompleted.Index].Value = !(bool)row.Cells[colCompleted.Index].Value;
                    bool completed = (bool)row.Cells[colCompleted.Index].Value;
                    string id = "" + row.Tag;

                    DSXmlHelper helper = new DSXmlHelper("Request");
                    helper.AddElement("Function");
                    helper.AddElement("Function", "Completed", completed ? "1" : "0");
                    helper.AddElement("Function", "Condition");
                    helper.AddElement("Function/Condition", "ID", id);

                    try
                    {
                        Service.UpdateFunction(new DSRequest(helper));
                        SaveRemind();
                    }
                    catch (Exception ex)
                    {
                        CurrentUser.ReportError(ex);
                        MsgBox.Show(ex.Message);
                    }
                }
            }
        }

        private void dataGridViewX1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MsgBox.Show("確定要刪除嗎？", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            string id = "" + e.Row.Tag;
            try
            {
                Service.DeleteVote(id);
                Service.DeleteFunction(id);
                SaveRemind();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private void dataGridViewX1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            string desc = "" + dgFunctionVote.CurrentRow.Cells[colDesc.Name].Value;
            string url = "" + dgFunctionVote.CurrentRow.Cells[colUrl.Name].Value;
            string ecd = "" + dgFunctionVote.CurrentRow.Cells[colECD.Name].Value;

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement(".", "Description", desc);
            helper.AddElement(".", "Url", url);
            helper.AddElement(".", "EstimatedCompletionDate", ecd);

            string new_id = "";
            try
            {
                new_id = Service.InsertFunction(new DSRequest(helper));
                dgFunctionVote.CurrentRow.Tag = new_id;
                SaveRemind();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgFunctionVote.Rows[e.RowIndex];
            if (row.IsNewRow)
                return;

            string desc = "" + row.Cells[colDesc.Index].Value;
            string url = "" + row.Cells[colUrl.Index].Value;
            string ecd = "" + row.Cells[colECD.Index].Value;
            string id = "" + row.Tag;

            #region 檢查日期格式
            DateTime temp_date = DateTime.Today;
            if (!string.IsNullOrEmpty(ecd))
            {
                row.Cells[colECD.Name].ErrorText = "";
                if (!DateTime.TryParse(ecd, out temp_date))
                {
                    row.Cells[colECD.Name].ErrorText = "格式錯誤";
                    return;
                }
            }
            #endregion

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Function");
            helper.AddElement("Function", "Description", desc);
            helper.AddElement("Function", "Url", url);
            helper.AddElement("Function", "EstimatedCompletionDate", string.IsNullOrEmpty(ecd) ? "" : temp_date.ToString("yyyy/MM/dd"));
            helper.AddElement("Function", "Condition");
            helper.AddElement("Function/Condition", "ID", id);

            try
            {
                Service.UpdateFunction(new DSRequest(helper));
                SaveRemind();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private void SaveRemind()
        {
            lblRemind.Visible = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            lblRemind.Visible = false;
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (CurrentMode == Mode.Admin && dgFunctionVote.Columns[e.ColumnIndex] == colNumOfVotes)
            {
                try
                {
                    string id = "" + dgFunctionVote.Rows[e.RowIndex].Tag;
                    DSXmlHelper helper = Service.GetVote(id);
                    VoteViewer viewer = new VoteViewer(helper);
                    viewer.ShowDialog();
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            KeepOrder();
            LoadFunctions();
        }

        private void VoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dgFunctionVote.ClearSelection();
        }

        private int SortDataGridViewRow(DataGridViewRow a, DataGridViewRow b)
        {
            int ia = _order_list.IndexOf(a.Tag as string);
            int ib = _order_list.IndexOf(b.Tag as string);

            return ia.CompareTo(ib);
        }
    }
}