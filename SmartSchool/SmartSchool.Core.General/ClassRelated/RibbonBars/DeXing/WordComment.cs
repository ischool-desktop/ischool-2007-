using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.Customization.Data;
using System.Collections;
using SmartSchool.StudentRelated.Palmerworm;
using SmartSchool.ApplicationLog;

namespace SmartSchool.ClassRelated.RibbonBars.DeXing
{
    public partial class WordComment : BaseForm
    {
        private CommentList _commentList;
        private ReserveFaces _reserveFaces;
        private bool _initialized = false;
        private SortOrder _order = SortOrder.Ascending;

        public WordComment()
        {
            InitializeComponent();
            this.dgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellValueChanged);
        }

        #region Main Form Event
        private void WordComment_Load(object sender, EventArgs e)
        {
            _initialized = false;

            _commentList = new CommentList();
            _reserveFaces = new ReserveFaces();

            List<string> faceList = new List<string>();

            //抓文字評量對照表
            DSXmlHelper helper = SmartSchool.Feature.Basic.Config.GetWordCommentList().GetContent();
            foreach (XmlElement morality in helper.GetElements("Content/Morality"))
            {
                DSXmlHelper moralityHelper = new DSXmlHelper(morality);
                string face = moralityHelper.GetText("@Face");
                if (!faceList.Contains(face)) faceList.Add(face);
                foreach (XmlElement item in moralityHelper.GetElements("Item"))
                {
                    string code = item.GetAttribute("Code");
                    string comment = item.GetAttribute("Comment");
                    if (_commentList.Add(face, code, comment) == false)
                        MsgBox.Show("文字評量代碼表有誤。");
                }
                if (!_commentList.ContainsFace(face)) _commentList.Add(face);
            }

            //文字評量 Face Column
            int index = 0;
            foreach (string face in faceList)
            {
                dgvList.Columns.Add("colItem" + index, face);
                index++;
            }

            //顯示項目
            foreach (DataGridViewColumn column in dgvList.Columns)
            {
                ButtonItem item = new ButtonItem(column.Name, column.HeaderText);
                item.Checked = true;
                item.AutoCollapseOnClick = false;
                item.Click += new EventHandler(item_Click);
                item.CheckedChanged += new EventHandler(item_CheckedChanged);
                btnShowList.SubItems.Add(item);
            }
            btnShowList.AutoExpandOnClick = true;

            //學年度學期
            int sy = SmartSchool.CurrentUser.Instance.SchoolYear;
            int se = SmartSchool.CurrentUser.Instance.Semester;
            for (int i = sy; i > sy - 3; i--)
                cboSchoolYear.Items.Add(i);

            cboSemester.Items.Add(1);
            cboSemester.Items.Add(2);

            cboSchoolYear.SelectedIndex = 0;
            cboSemester.SelectedIndex = se - 1;

            //Load Data
            LoadData();

            _initialized = true;
        }
        #endregion

        private void LoadData()
        {
            dgvList.SuspendLayout();
            dgvList.Rows.Clear();

            //取得學生清單資料 
            AccessHelper accessHelper = new AccessHelper();
            List<StudentRecord> students = new List<StudentRecord>();
            foreach (ClassRecord class_rec in accessHelper.ClassHelper.GetSelectedClass())
                students.AddRange(class_rec.Students);

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "ID");
            helper.AddElement("Field", "TextScore");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "SchoolYear", cboSchoolYear.Text);
            helper.AddElement("Condition", "Semester", cboSemester.Text);
            helper.AddElement("Condition", "StudentIDList");
            foreach (StudentRecord stu_rec in students)
                helper.AddElement("Condition/StudentIDList", "ID", stu_rec.StudentID);

            DSResponse dsrsp = SmartSchool.Feature.Score.QueryScore.GetSemesterMoralScore(new DSRequest(helper));
            if (!dsrsp.HasContent)
            {
                MsgBox.Show("取得回覆文件錯誤:" + dsrsp.GetFault().Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            helper = dsrsp.GetContent();

            foreach (StudentRecord stu_rec in students)
            {
                int rowIndex = dgvList.Rows.Add();
                DataGridViewRow row = dgvList.Rows[rowIndex];

                row.Cells[colName.Name].Value = stu_rec.StudentName;
                row.Cells[colSeatNo.Name].Value = stu_rec.SeatNo;
                row.Cells[colStudentNumber.Name].Value = stu_rec.StudentNumber;

                string score_id = "";
                XmlElement element = helper.GetElement("SemesterMoralScore[RefStudentID='" + stu_rec.StudentID + "']");
                if (element != null)
                {
                    DSXmlHelper moralHelper = new DSXmlHelper(element);

                    score_id = moralHelper.GetText("@ID");

                    foreach (XmlElement node in element.SelectNodes("TextScore/Morality"))
                    {
                        DSXmlHelper nodeHelper = new DSXmlHelper(node);
                        string face = nodeHelper.GetText("@Face");

                        //如果 face 不存在對照表中，偷偷保留下來
                        if (!_commentList.ContainsFace(face)) _reserveFaces.Add(score_id, face, node.InnerText);

                        foreach (DataGridViewColumn column in dgvList.Columns)
                        {
                            if (column.Name.StartsWith("colItem"))
                            {
                                if (column.HeaderText != face) continue;
                                row.Cells[column.Index].Value = node.InnerText;
                            }
                        }
                    }
                }

                RowInfo info = new RowInfo(row);
                info.StudentID = stu_rec.StudentID;
                info.ScoreID = score_id;
                row.Tag = info;
            }
        }

        #region Show Item List Event
        private void item_Click(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            item.Checked = !item.Checked;
        }

        private void item_CheckedChanged(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            DataGridViewColumn column = dgvList.Columns[item.Name];
            column.Visible = item.Checked;
        }
        #endregion

        #region DataGridView Event
        private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex];
            ValidCell(cell);
        }

        private void dgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex];
            CellInfo ci = cell.Tag as CellInfo;
            if (!_initialized && ci != null)
                ci.SetOldValue(cell);
            else if (ci != null)
                ci.SetValue(cell);
        }

        private void dgvList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IComparer comparer;
            DataGridViewColumn column = dgvList.Columns[e.ColumnIndex];
            _order = _order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            switch (column.Name)
            {
                case "colName":
                    comparer = new StringComparer(column.Index, _order);
                    break;
                case "colStudentNumber":
                    comparer = new StringComparer(column.Index, _order);
                    break;
                case "colSeatNo":
                    comparer = new DecimalComparer(column.Index, _order);
                    break;
                default:
                    comparer = new StringComparer(column.Index, _order);
                    break;
            }
            dgvList.Sort(comparer);
        }

        private void dgvList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space)
            {
                foreach (DataGridViewCell cell in dgvList.SelectedCells)
                {
                    DataGridViewColumn column = dgvList.Columns[cell.ColumnIndex];
                    if (!column.ReadOnly)
                    {
                        cell.Value = string.Empty;
                        ValidCell(cell);
                    }
                }
            }
        }
        #endregion

        #region SchoolYear and Semester Combox Event
        private void cboSchoolYear_TextChanged(object sender, EventArgs e)
        {
            if (_initialized && ValidSemester())
                LoadData();
        }

        private void cboSemester_TextChanged(object sender, EventArgs e)
        {
            if (_initialized && ValidSemester())
                LoadData();
        }
        #endregion

        #region Valid Function
        private bool ValidSemester()
        {
            errorProvider1.Clear();
            bool valid = true;
            int sy;
            if (!int.TryParse(cboSchoolYear.Text, out sy))
            {
                errorProvider1.SetError(lblSchoolYear, "不合法的學年度");
                valid = false;
            }
            if (sy > SmartSchool.CurrentUser.Instance.SchoolYear || sy < 80)
            {
                errorProvider1.SetError(lblSchoolYear, "超出範圍");
                valid = false;
            }
            if (cboSemester.Text != "1" && cboSemester.Text != "2")
            {
                errorProvider1.SetError(lblSemester, "不合法的學期");
                valid = false;
            }
            return valid;
        }

        private void ValidCell(DataGridViewCell cell)
        {
            DataGridViewColumn column = cell.OwningColumn;
            cell.ErrorText = string.Empty;
            IValidator validator = new WordCommentValidator();
            //switch (column.HeaderText)
            //{
            //    case "導師加減分":
            //        validator = new TeacherBiasValidator();
            //        validator.Argument = _tsLimit;
            //        break;
            //    case "導師評語":
            //        validator = new CommentValidator();
            //        validator.Argument = _commentList;
            //        break;
            //    default:
            //        validator = new ScoreValidator();
            //        break;
            //}
            validator.ValidCell = cell;
            validator.Argument = _commentList;
            if (!validator.IsValid())
                cell.ErrorText = validator.Message;

            CellInfo ci = cell.Tag as CellInfo;
            ci.SetValue(cell);
        }

        private bool ValidAll()
        {
            if (!ValidSemester())
                return false;
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!string.IsNullOrEmpty(cell.ErrorText))
                        return false;
                }
            }
            return true;
        }
        #endregion

        private string GetValue(DataGridViewCell cell)
        {
            if (cell.Value == null)
                return string.Empty;
            return cell.Value.ToString();
        }

        #region Button Event
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidAll())
            {
                MsgBox.Show("資料內容有誤, 請先修正後再行儲存!", "內容錯誤", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            StringBuilder isb = new StringBuilder("");
            StringBuilder usb = new StringBuilder("");

            DSXmlHelper ih = new DSXmlHelper("Request");
            DSXmlHelper uh = new DSXmlHelper("Request");
            int icount = 0, ucount = 0;
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                RowInfo info = row.Tag as RowInfo;
                if (!info.IsDirty) continue;
                if (info.IsNew)
                {
                    string studentName = GetValue(row.Cells[colName.Name]);
                    isb.Append("新增【").Append(studentName).Append("-").Append(cboSchoolYear.Text)
                     .Append("學年度").Append(cboSemester.Text).Append("學期】資料：\n");

                    ih.AddElement("SemesterMoralScore");
                    ih.AddElement("SemesterMoralScore", "RefStudentID", info.StudentID);
                    ih.AddElement("SemesterMoralScore", "SchoolYear", cboSchoolYear.Text);
                    ih.AddElement("SemesterMoralScore", "Semester", cboSemester.Text);
                    ih.AddElement("SemesterMoralScore", "TextScore");
                    bool hasRoot = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        DataGridViewColumn column = cell.OwningColumn;
                        if (column.Name.StartsWith("colItem"))
                        {
                            if (!hasRoot)
                            {
                                ih.AddElement("SemesterMoralScore/TextScore", "Content");
                                hasRoot = true;
                            }
                            XmlElement element = ih.AddElement("SemesterMoralScore/TextScore/Content", "Morality", GetValue(cell));
                            element.SetAttribute("Face", column.HeaderText);
                            isb.Append("\t").Append(column.HeaderText).Append("：").Append(GetValue(row.Cells[column.Name])).Append("\n");
                        }
                    }
                    icount++;
                }
                else
                {
                    string studentName = GetValue(row.Cells[colName.Name]);
                    string studentNumber = GetValue(row.Cells[colStudentNumber.Name]);
                    usb.Append("學生【" + studentName + "-" + studentNumber + "】");
                    usb.Append(cboSchoolYear.Text).Append("學年度").Append(cboSemester.Text).Append("學期");
                    usb.Append("資料修改：\n");

                    uh.AddElement("SemesterMoralScore");
                    uh.AddElement("SemesterMoralScore", "Condition");
                    uh.AddElement("SemesterMoralScore/Condition", "ID", info.ScoreID);
                    uh.AddElement("SemesterMoralScore", "TextScore");
                    bool hasRoot = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        DataGridViewColumn column = cell.OwningColumn;
                        if (column.Name.StartsWith("colItem"))
                        {
                            if (!hasRoot)
                            {
                                uh.AddElement("SemesterMoralScore/TextScore", "Content");
                                hasRoot = true;
                            }
                            XmlElement element = uh.AddElement("SemesterMoralScore/TextScore/Content", "Morality", GetValue(cell));
                            element.SetAttribute("Face", column.HeaderText);
                        }
                        CellInfo ci = cell.Tag as CellInfo;
                        if (ci.IsDirty)
                            usb.Append("\t").Append(column.HeaderText).Append("：由「").Append(ci.OldValue).Append("」變更值為「").Append(ci.Value).Append("」\n");
                    }
                    foreach (string other_face in _reserveFaces.GetFacesByID(info.ScoreID))
                    {
                        if (!hasRoot)
                        {
                            uh.AddElement("SemesterMoralScore/TextScore", "Content");
                            hasRoot = true;
                        }
                        XmlElement element = uh.AddElement("SemesterMoralScore/TextScore/Content", "Morality", _reserveFaces.GetComment(info.ScoreID, other_face));
                        element.SetAttribute("Face", other_face);
                    }
                    ucount++;
                }
            }
            try
            {
                string classID = SmartSchool.ClassRelated.Class.Instance.SelectionClasses[0].ClassID;
                if (icount > 0)
                {
                    SmartSchool.Feature.Score.AddScore.InsertSemesterMoralScore(new DSRequest(ih));
                    CurrentUser.Instance.AppLog.Write(EntityType.Class, EntityAction.Insert, classID, isb.ToString(), this.Text, ih.GetRawXml());
                }
                if (ucount > 0)
                {
                    SmartSchool.Feature.Score.EditScore.UpdateSemesterMoralScore(new DSRequest(uh));
                    CurrentUser.Instance.AppLog.Write(EntityType.Class, EntityAction.Update, classID, usb.ToString(), this.Text, uh.GetRawXml());
                }
                MsgBox.Show("儲存完畢", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show("儲存資料失敗:" + ex.Message, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}