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
using SmartSchool.ClassRelated;
using SmartSchool.StudentRelated.Palmerworm;
using SmartSchool.ApplicationLog;
using System.Collections;
using SmartSchool.Customization.Data;

namespace SmartSchool.ClassRelated.RibbonBars.DeXing
{
    public partial class TeacherBias : BaseForm
    {
        private decimal _tsLimit;
        private Dictionary<string, string> _commentList;

        private bool _initialized = false;

        public TeacherBias()
        {
            InitializeComponent();
            dataGridViewX1.CellValueChanged += new DataGridViewCellEventHandler(dataGridViewX1_CellValueChanged);
        }

        void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            CellInfo ci = cell.Tag as CellInfo;
            if (!_initialized && ci != null)
                ci.SetOldValue(cell);
            else if (ci != null)
                ci.SetValue(cell);
        }

        private void TeacherBias_Load(object sender, EventArgs e)
        {
            _initialized = false;

            // 限制分數
            try
            {
                _tsLimit = SmartSchool.Feature.Basic.Config.GetSupervisedRatingRange();
            }
            catch
            {
                _tsLimit = 0;
            }

            // 評語對照
            DSXmlHelper helper = SmartSchool.Feature.Basic.Config.GetMoralCommentCodeList().GetContent();
            foreach (XmlElement element in helper.GetElements("Morality"))
            {
                string code = element.GetAttribute("Code");
                string comment = element.GetAttribute("Comment");
                if (_commentList == null)
                    _commentList = new Dictionary<string, string>();
                _commentList.Add(code, comment);
            }


            cboItem.Items.Add("導師加減分");
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetMoralDiffItemList();
            helper = dsrsp.GetContent();
            int index = 0;
            foreach (XmlElement element in helper.GetElements("DiffItem"))
            {
                string name = element.GetAttribute("Name");
                dataGridViewX1.Columns.Add("colItem" + index, name);
                cboItem.Items.Add(name);
                index++;
            }
            cboItem.SelectedIndex = 0;

            foreach (DataGridViewColumn column in dataGridViewX1.Columns)
            {
                ButtonItem item = new ButtonItem(column.Name, column.HeaderText);
                item.Checked = true;
                item.AutoCollapseOnClick = false;
                item.Click += new EventHandler(item_Click);
                item.CheckedChanged += new EventHandler(item_CheckedChanged);
                btnShowList.SubItems.Add(item);
            }
            btnShowList.AutoExpandOnClick = true;

            /**
             *  學年度學期
             */
            int sy = SmartSchool.CurrentUser.Instance.SchoolYear;
            int se = SmartSchool.CurrentUser.Instance.Semester;
            for (int i = sy; i > sy - 3; i--)
                cboSchoolYear.Items.Add(i);

            cboSemester.Items.Add(1);
            cboSemester.Items.Add(2);

            cboSchoolYear.SelectedIndex = 0;
            cboSemester.SelectedIndex = se - 1;

            LoadData();

            _initialized = true;
        }

        void item_Click(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            item.Checked = !item.Checked;
        }

        void item_CheckedChanged(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            DataGridViewColumn column = dataGridViewX1.Columns[item.Name];
            column.Visible = item.Checked;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal add = 0;
            if (!decimal.TryParse(txtAdd.Text, out add))
                add = 0;
          
            if (add == 0) return;
            
            string columnText = cboItem.Items[cboItem.SelectedIndex].ToString();
            foreach (DataGridViewColumn column in dataGridViewX1.Columns)
            {
                if (column.HeaderText != columnText) continue;
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    DataGridViewCell cell = row.Cells[column.Name];
                    string cellValue = cell.Value == null ? "0" : cell.Value.ToString();
                    decimal value = 0;
                    if (!decimal.TryParse(cellValue, out value))
                        value = 0;
                    value += add;
                    cell.Value = value.ToString();
                    ValidCell(cell);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            ValidCell(cell);
            //DataGridViewColumn column = dataGridViewX1.Columns[e.ColumnIndex];
            //DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //cell.ErrorText = string.Empty;
            //IValidator validator;
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
            //validator.ValidCell = cell;
            //if (!validator.IsValid())
            //    cell.ErrorText = validator.Message;

            //CellInfo ci = cell.Tag as CellInfo;
            //ci.SetValue(cell);
        }

        private void ValidCell(DataGridViewCell cell)
        {
            DataGridViewColumn column = dataGridViewX1.Columns[cell.ColumnIndex];          
            cell.ErrorText = string.Empty;
            IValidator validator;
            switch (column.HeaderText)
            {
                case "導師加減分":
                    validator = new TeacherBiasValidator();
                    validator.Argument = _tsLimit;
                    break;
                case "導師評語":
                    validator = new CommentValidator();
                    validator.Argument = _commentList;
                    break;
                default:
                    validator = new ScoreValidator();
                    break;
            }
            validator.ValidCell = cell;
            if (!validator.IsValid())
                cell.ErrorText = validator.Message;

            CellInfo ci = cell.Tag as CellInfo;
            ci.SetValue(cell);
        }

        private void dataGridViewX1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space)
            {
                foreach (DataGridViewCell cell in dataGridViewX1.SelectedCells)
                {
                    DataGridViewColumn column = dataGridViewX1.Columns[cell.ColumnIndex];
                    if (!column.ReadOnly)
                    {
                        cell.Value = string.Empty;
                        ValidCell(cell);
                    }
                }
            }
        }

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
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                RowInfo info = row.Tag as RowInfo;
                if (!info.IsDirty) continue;
                if (info.IsNew)
                {
                    string studentName = GetValue(row.Cells[colName.Name]);
                    isb.Append("新增【").Append(studentName).Append("-").Append(cboSchoolYear.Text)
                     .Append("學年度").Append(cboSemester.Text).Append("學期】資料：\n");

                    isb.Append("\t").Append(colTB.HeaderText).Append("：").Append(GetValue(row.Cells[colTB.Name])).Append("\n");
                    isb.Append("\t").Append(colTT.HeaderText).Append("：").Append(GetValue(row.Cells[colTT.Name])).Append("\n");

                    ih.AddElement("SemesterMoralScore");
                    ih.AddElement("SemesterMoralScore", "RefStudentID", info.StudentID);
                    ih.AddElement("SemesterMoralScore", "SchoolYear", cboSchoolYear.Text);
                    ih.AddElement("SemesterMoralScore", "Semester", cboSemester.Text);
                    ih.AddElement("SemesterMoralScore", "SupervisedByDiff", GetValue(row.Cells[colTB.Name]));
                    ih.AddElement("SemesterMoralScore", "SupervisedByComment", GetValue(row.Cells[colTT.Name]));
                    ih.AddElement("SemesterMoralScore", "OtherDiff");
                    bool hasRoot = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {

                        DataGridViewColumn column = dataGridViewX1.Columns[cell.ColumnIndex];
                        if (column.Name.StartsWith("colItem"))
                        {
                            if (!hasRoot)
                            {
                                ih.AddElement("SemesterMoralScore/OtherDiff", "OtherDiffList");
                                hasRoot = true;
                            }
                            XmlElement element = ih.AddElement("SemesterMoralScore/OtherDiff/OtherDiffList", "OtherDiff", GetValue(cell));
                            element.SetAttribute("Name", column.HeaderText);
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
                    uh.AddElement("SemesterMoralScore", "SupervisedByDiff", GetValue(row.Cells[colTB.Name]));
                    uh.AddElement("SemesterMoralScore", "SupervisedByComment", GetValue(row.Cells[colTT.Name]));
                    uh.AddElement("SemesterMoralScore", "OtherDiff");
                    bool hasRoot = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        DataGridViewColumn column = dataGridViewX1.Columns[cell.ColumnIndex];
                        if (column.Name.StartsWith("colItem"))
                        {
                            if (!hasRoot)
                            {
                                uh.AddElement("SemesterMoralScore/OtherDiff", "OtherDiffList");
                                hasRoot = true;
                            }
                            XmlElement element = uh.AddElement("SemesterMoralScore/OtherDiff/OtherDiffList", "OtherDiff", GetValue(cell));
                            element.SetAttribute("Name", column.HeaderText);
                        }
                        CellInfo ci = cell.Tag as CellInfo;
                        if (ci.IsDirty)
                            usb.Append("\t").Append(column.HeaderText).Append("：由「").Append(ci.OldValue).Append("」變更值為「").Append(ci.Value).Append("」\n");
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

        private bool ValidAll()
        {
            if (!ValidSemester())
                return false;
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!string.IsNullOrEmpty(cell.ErrorText))
                        return false;
                }
            }
            return true;
        }

        private string GetValue(DataGridViewCell cell)
        {
            if (cell.Value == null)
                return string.Empty;
            return cell.Value.ToString();
        }

        private void LoadData()
        {
            dataGridViewX1.Rows.Clear();
            /**
            * 取得學生清單資料 
            */
            AccessHelper accessHelper = new AccessHelper();
            List<StudentRecord> students = new List<StudentRecord>();
            foreach (ClassRecord class_rec in accessHelper.ClassHelper.GetSelectedClass())
                students.AddRange(class_rec.Students);

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
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
                int rowIndex = dataGridViewX1.Rows.Add();
                DataGridViewRow row = dataGridViewX1.Rows[rowIndex];

                row.Cells[colName.Name].Value = stu_rec.StudentName;
                row.Cells[colSeatNo.Name].Value = stu_rec.SeatNo;
                row.Cells[colStudentNumber.Name].Value = stu_rec.StudentNumber;

                RowInfo info = new RowInfo(row);
                info.StudentID = stu_rec.StudentID;
                row.Tag = info;

                XmlElement element = helper.GetElement("SemesterMoralScore[RefStudentID='" + stu_rec.StudentID + "']");
                if (element != null)
                {
                    DSXmlHelper moralHelper = new DSXmlHelper(element);

                    row.Cells[colTB.Name].Value = moralHelper.GetText("SupervisedByDiff");
                    row.Cells[colTT.Name].Value = moralHelper.GetText("SupervisedByComment");

                    foreach (XmlNode node in element.SelectNodes("OtherDiffList/OtherDiff"))
                    {
                        DSXmlHelper nodeHelper = new DSXmlHelper(node as XmlElement);
                        string name = nodeHelper.GetText("@Name");
                        foreach (DataGridViewColumn column in dataGridViewX1.Columns)
                        {
                            if (column.HeaderText != name) continue;
                            row.Cells[column.Name].Value = node.InnerText;
                        }
                    }

                    info.ScoreID = moralHelper.GetText("@ID");
                }
                
                
            }
            
            //foreach (XmlElement element in helper.GetElements("Student"))
            //{
            //    int rowIndex = dataGridViewX1.Rows.Add();

            //    DataGridViewRow row = dataGridViewX1.Rows[rowIndex];
            //    row.Cells[colName.Name].Value = element.SelectSingleNode("Name").InnerText;
            //    row.Cells[colSeatNo.Name].Value = element.SelectSingleNode("SeatNo").InnerText;
            //    row.Cells[colStudentNumber.Name].Value = element.SelectSingleNode("StudentNumber").InnerText;
            //    row.Cells[colTB.Name].Value = element.SelectSingleNode("SupervisedByDiff").InnerText;
            //    row.Cells[colTT.Name].Value = element.SelectSingleNode("SupervisedByComment").InnerText;

            //    foreach (XmlNode node in element.SelectNodes("OtherDiffList/OtherDiff"))
            //    {
            //        string name = node.SelectSingleNode("@Name").InnerText;
            //        foreach (DataGridViewColumn column in dataGridViewX1.Columns)
            //        {
            //            if (column.HeaderText != name) continue;
            //            row.Cells[column.Name].Value = node.InnerText;
            //        }
            //    }

            //    RowInfo info = new RowInfo(row);
            //    info.StudentID = element.GetAttribute("ID");
            //    info.ScoreID = element.SelectSingleNode("ScoreID").InnerText;
            //    row.Tag = info;
            //}
        }

        private void cboSemester_TextChanged(object sender, EventArgs e)
        {
            if (_initialized && ValidSemester())
                LoadData();
        }

        private void cboSchoolYear_TextChanged(object sender, EventArgs e)
        {
            if (_initialized && ValidSemester())
                LoadData();
        }

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

        private SortOrder _order = SortOrder.Ascending;
        private void dataGridViewX1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IComparer comparer;
            DataGridViewColumn column = dataGridViewX1.Columns[e.ColumnIndex];
            _order = _order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            switch (column.Name)
            {
                case "colName":
                    comparer = new StringComparer(column.Index, _order);
                    break;
                case "colStudentNumber":
                    comparer = new StringComparer(column.Index, _order);
                    break;
                case "colTT":
                    comparer = new StringComparer(column.Index, _order);
                    break;
                case "colTB":
                    comparer = new DecimalComparer(column.Index, _order);
                    break;
                case "colSeatNo":
                    comparer = new DecimalComparer(column.Index, _order);
                    break;
                default:
                    comparer = new DecimalComparer(column.Index, _order);
                    break;
            }
            dataGridViewX1.Sort(comparer);
        }

        private void dataGridViewX1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //    IComparer comparer;
            //    DataGridViewColumn column = e.Column;
            //    DataGridViewRow row1 = dataGridViewX1.Rows[e.RowIndex1];
            //    DataGridViewRow row2 = dataGridViewX1.Rows[e.RowIndex2];
            //    switch (column.Name)
            //    {
            //        case "colName":
            //            comparer = new StringComparer(column.Index);
            //            break;
            //        case "colStudentNumber":
            //            comparer = new StringComparer(column.Index);
            //            break;
            //        case "colTT":
            //            comparer = new StringComparer(column.Index);
            //            break;
            //        case "colTB":
            //            comparer = new DecimalComparer(column.Index);
            //            break;
            //        case "colSeatNo":
            //            comparer = new DecimalComparer(column.Index);
            //            break;
            //        default:
            //            comparer = new DecimalComparer(column.Index);
            //            break;
            //    }
            //    int asc = 1;
            //    if (dataGridViewX1.SortOrder == SortOrder.Descending)
            //        asc = -1;
            //    e.SortResult = comparer.Compare(row1, row2) * asc;
        }
    }

    class CellInfo
    {
        private string _oldValue;
        private string _newValue;
        public CellInfo(string value)
        {
            _oldValue = value;
            _newValue = value;
        }

        public CellInfo(DataGridViewCell cell)
        {
            string value = cell.Value == null ? string.Empty : cell.Value.ToString();
            _oldValue = value;
            _newValue = value;
        }

        public void SetValue(string value)
        {
            _newValue = value;
        }

        public void SetValue(DataGridViewCell cell)
        {
            string value = cell.Value == null ? string.Empty : cell.Value.ToString();
            _newValue = value;
        }

        public void SetOldValue(DataGridViewCell cell)
        {
            string value = cell.Value == null ? string.Empty : cell.Value.ToString();
            _oldValue = value;
        }

        public string Value
        {
            get { return _newValue; }
        }

        public string OldValue
        {
            get { return _oldValue; }
        }

        public bool IsDirty
        {
            get { return _oldValue != _newValue; }
        }
    }

    class RowInfo
    {
        private string _studentID;

        public string StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }
        private string _scoreID;

        public string ScoreID
        {
            get { return _scoreID; }
            set { _scoreID = value; }
        }

        public bool IsNew
        {
            get { return string.IsNullOrEmpty(_scoreID); }
        }

        private DataGridViewRow _row;
        public RowInfo(DataGridViewRow row)
        {
            _row = row;
            foreach (DataGridViewCell cell in _row.Cells)
            {
                CellInfo ci = new CellInfo(cell);
                cell.Tag = ci;
            }
        }

        public bool IsCellDirty(string columnName)
        {
            CellInfo info = _row.Cells[columnName].Tag as CellInfo;
            return info.IsDirty;
        }

        public CellInfo GetCellInfo(string columnName)
        {
            return _row.Cells[columnName].Tag as CellInfo;
        }

        public bool IsDirty
        {
            get
            {
                foreach (DataGridViewCell cell in _row.Cells)
                {
                    CellInfo ci = cell.Tag as CellInfo;
                    if (ci.IsDirty) return true;
                }
                return false;
            }
        }
    }

    public class DecimalComparer : IComparer
    {
        private int _columnIndex;
        private SortOrder _order;

        public DecimalComparer(int columnIndex, SortOrder order)
        {
            _columnIndex = columnIndex;
            _order = order;
        }

        #region IComparer<DataGridViewCell> 成員

        public int Compare(object x, object y)
        {
            DataGridViewRow xr = x as DataGridViewRow;
            DataGridViewRow yr = y as DataGridViewRow;
            DataGridViewCell xc = xr.Cells[_columnIndex];
            DataGridViewCell yc = yr.Cells[_columnIndex];

            decimal xd, yd;
            if (xc.Value == null) xc.Value = 0;
            if (yc.Value == null) yc.Value = 0;
            if (!decimal.TryParse(xc.Value.ToString(), out xd))
                xd = 0;
            if (!decimal.TryParse(yc.Value.ToString(), out yd))
                yd = 0;

            int order = _order == SortOrder.Ascending ? 1 : -1;
            return xd.CompareTo(yd) * order;
        }

        #endregion
    }

    public class StringComparer : IComparer
    {
        private int _columnIndex;

        private SortOrder _order;

        public StringComparer(int columnIndex, SortOrder order)
        {
            _columnIndex = columnIndex;
            _order = order;
        }
        #region IComparer<DataGridViewCell> 成員

        public int Compare(object x, object y)
        {
            DataGridViewRow xr = x as DataGridViewRow;
            DataGridViewRow yr = y as DataGridViewRow;
            DataGridViewCell xc = xr.Cells[_columnIndex];
            DataGridViewCell yc = yr.Cells[_columnIndex];

            if (xc.Value == null) xc.Value = string.Empty;
            if (yc.Value == null) yc.Value = string.Empty;

            int order = _order == SortOrder.Ascending ? 1 : -1;
            return xc.Value.ToString().CompareTo(yc.Value.ToString()) * order;
        }

        #endregion
    }
}