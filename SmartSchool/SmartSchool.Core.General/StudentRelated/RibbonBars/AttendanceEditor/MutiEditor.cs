using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.StudentRelated;
using IntelliSchool.DSA30.Util;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;
using SmartSchool.Properties;
//using SmartReport.Properties;
//using SmartSchool.SmartPlugIn.Properties;

namespace SmartSchool.StudentRelated.RibbonBars.AttendanceEditor
{
    public partial class MutiEditor : BaseForm
    {
        private List<BriefStudentData> _students;
        private ISemester _semesterProvider;
        private Dictionary<string, AbsenceInfo> _absenceList;
        private int _startIndex;
        private AbsenceInfo _checkedAbsence;
        private ErrorProvider _errProvider;
        private DateTime _currentDate;
        List<DataGridViewRow> _hiddenRows;

        System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

        //log �ݭn�Ψ쪺
        private Dictionary<string, Dictionary<string, string>> beforeData = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, Dictionary<string, string>> afterData = new Dictionary<string, Dictionary<string, string>>();
        private List<string> deleteData = new List<string>();
        private DateTime logDate;

        public MutiEditor(List<BriefStudentData> students)
        {
            InitializeComponent();
            _students = students;
            _absenceList = new Dictionary<string, AbsenceInfo>();
            _semesterProvider = SemesterProvider.GetInstance();
            _errProvider = new ErrorProvider();
            _startIndex = 6;

            _hiddenRows = new List<DataGridViewRow>();
        }

        private void MutiEditor_Load(object sender, EventArgs e)
        {
            InitializeRadioButton();
            InitializeDateRange();
            InitializeDataGridView();
            SearchStudentRange();
            buttonX1_Click(null, null);

            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            bool isLock = false;
            if (picLock.Tag != null)
            {
                if (!bool.TryParse(picLock.Tag.ToString(), out isLock))
                    isLock = false;
            }
            if (isLock)
                toolTip.SetToolTip(picLock, "���m�n������w��w�A�z�i�H�I��ϥܸѰ���w�C");
            else
                toolTip.SetToolTip(picLock, "���m�n�����������w���A�A�z�i�H�I��ϥܡA�N�S�w����϶���w�C");
        }

        private void InitializeDateRange()
        {
            XmlElement info = SmartSchool.CurrentUser.Instance.Preference["Attendance_MutiEditor"];
            if (info == null)
            {
                startDate.SetDate(DateTime.Today.ToShortDateString());
                picLock.Image = Resources.unlocked;
                picLock.Tag = false;
            }
            else
            {
                bool isLock = false;
                if (!bool.TryParse(info.SelectSingleNode("Locked").InnerText, out isLock))
                    isLock = false;
                if (isLock)
                {
                    startDate.SetDate(info.SelectSingleNode("Date").InnerText);
                    picLock.Image = Resources.locked;
                    picLock.Tag = true;
                }
                else
                {
                    startDate.SetDate(DateTime.Today.ToShortDateString());
                    picLock.Image = Resources.unlocked;
                    picLock.Tag = false;
                }
            }
            _currentDate = startDate.GetDate();
        }

        private void InitializeRadioButton()
        {
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetAbsenceList();
            DSXmlHelper helper = dsrsp.GetContent();
            foreach (XmlElement element in helper.GetElements("Absence"))
            {
                AbsenceInfo info = new AbsenceInfo(element);
                _absenceList.Add(info.Hotkey.ToUpper(), info);

                RadioButton rb = new RadioButton();
                rb.Text = info.Name + "(" + info.Hotkey + ")";
                rb.AutoSize = true;
                rb.Font = new Font(FontStyles.GeneralFontFamily, 9.25f);
                rb.Tag = info;
                rb.CheckedChanged += new EventHandler(rb_CheckedChanged);
                rb.Click += new EventHandler(rb_CheckedChanged);
                panel.Controls.Add(rb);
                if (_checkedAbsence == null)
                {
                    _checkedAbsence = info;
                    rb.Checked = true;
                }
            }
        }

        void rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                _checkedAbsence = rb.Tag as AbsenceInfo;
                foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                {
                    if (cell.ColumnIndex < _startIndex) continue;
                    cell.Value = _checkedAbsence.Abbreviation;
                    AbsenceCellInfo acInfo = cell.Tag as AbsenceCellInfo;
                    if (acInfo == null)
                    {
                        acInfo = new AbsenceCellInfo();
                    }
                    acInfo.SetValue(_checkedAbsence);
                    cell.Value = acInfo.AbsenceInfo.Abbreviation;
                    cell.Tag = acInfo;
                }
                dataGridView.Focus();
            }
        }

        private void InitializeDataGridView()
        {
            InitializeDataGridViewColumn();
        }

        private void InitializeDataGridViewColumn()
        {
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetPeriodList();
            DSXmlHelper helper = dsrsp.GetContent();
            PeriodCollection collection = new PeriodCollection();
            foreach (XmlElement element in helper.GetElements("Period"))
            {
                PeriodInfo info = new PeriodInfo(element);
                collection.Items.Add(info);
            }
            dataGridView.Columns.Add("colClassName", "�Z��");
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[0].ReadOnly = true;

            dataGridView.Columns.Add("colName", "�m�W");
            dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[1].ReadOnly = true;

            dataGridView.Columns.Add("colSchoolNumber", "�Ǹ�");
            dataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[2].ReadOnly = true;

            dataGridView.Columns.Add("colSeatNo", "�y��");
            dataGridView.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[3].ReadOnly = true;

            dataGridView.Columns.Add("colSchoolYear", "�Ǧ~��");
            dataGridView.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[4].ReadOnly = false;

            dataGridView.Columns.Add("colSemester", "�Ǵ�");
            dataGridView.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[5].ReadOnly = false;
            dataGridView.Columns[5].Frozen = true;

            foreach (PeriodInfo info in collection.GetSortedList())
            {
                int columnIndex = dataGridView.Columns.Add(info.Name, info.Name);
                DataGridViewColumn column = dataGridView.Columns[columnIndex];
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.ReadOnly = true;
                column.Tag = info;
            }
        }

        private void SearchStudentRange()
        {
            dataGridView.Rows.Clear();
            _semesterProvider.SetDate(startDate.GetDate());
            foreach (BriefStudentData student in _students)
            {
                int rowIndex = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[rowIndex];
                row.Cells[0].Value = student.ClassName;
                row.Cells[1].Value = student.Name;
                row.Cells[2].Value = student.StudentNumber;
                row.Cells[3].Value = student.SeatNo;
                row.Cells[4].Value = _semesterProvider.SchoolYear;
                row.Cells[5].Value = _semesterProvider.Semester;
                row.Cells[4].Tag = new SemesterCellInfo(_semesterProvider.SchoolYear.ToString());
                row.Cells[5].Tag = new SemesterCellInfo(_semesterProvider.Semester.ToString());
                StudentRowTag tag = new StudentRowTag();
                tag.Student = student;
                row.Tag = tag;
            }
        }

        private void GetAbsense()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "OccurDate", startDate.DateString);
            foreach (BriefStudentData student in _students)
                helper.AddElement("Condition", "RefStudentID", student.ID);

            DSResponse dsrsp = SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper));
            helper = dsrsp.GetContent();

            //log �M�� beforeData
            beforeData.Clear();

            //log �������
            logDate = DateTime.Parse(startDate.DateString);

            foreach (XmlElement element in helper.GetElements("Attendance"))
            {
                // �o�̭n���@�ǨƱ�  �Ҧp���F�a��i�h
                string occurDate = element.SelectSingleNode("OccurDate").InnerText;
                string schoolYear = element.SelectSingleNode("SchoolYear").InnerText;
                string semester = element.SelectSingleNode("Semester").InnerText;
                string studentid = element.SelectSingleNode("RefStudentID").InnerText;
                string id = element.GetAttribute("ID");
                XmlNode dNode = element.SelectSingleNode("Detail").FirstChild;

                //log �����ק�e����� �����ǥ�ID
                if (!beforeData.ContainsKey(studentid))
                    beforeData.Add(studentid, new Dictionary<string, string>());

                DataGridViewRow row = null;
                foreach (DataGridViewRow r in dataGridView.Rows)
                {
                    DateTime date;
                    StudentRowTag rt = r.Tag as StudentRowTag;
                    if (rt.Student.ID == studentid)
                    {
                        row = r;
                        rt.RowID = id;
                        break;
                    }
                }

                if (row == null) continue;

                row.Cells[4].Value = schoolYear;
                row.Cells[4].Tag = new SemesterCellInfo(schoolYear);

                row.Cells[5].Value = semester;
                row.Cells[5].Tag = new SemesterCellInfo(semester);

                for (int i = _startIndex; i < dataGridView.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView.Columns[i];
                    PeriodInfo info = column.Tag as PeriodInfo;

                    foreach (XmlNode node in dNode.SelectNodes("Period"))
                    {
                        if (node.InnerText != info.Name) continue;
                        if (node.SelectSingleNode("@AbsenceType") == null) continue;

                        DataGridViewCell cell = row.Cells[i];
                        foreach (AbsenceInfo ai in _absenceList.Values)
                        {
                            if (ai.Name != node.SelectSingleNode("@AbsenceType").InnerText) continue;
                            AbsenceInfo ainfo = ai.Clone();
                            cell.Tag = new AbsenceCellInfo(ainfo);
                            cell.Value = ai.Abbreviation;

                            //log �����ק�e����� ���m���ӳ���
                            if (!beforeData[studentid].ContainsKey(info.Name))
                                beforeData[studentid].Add(info.Name, ai.Name);

                            break;
                        }
                    }
                }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                MsgBox.Show("������ҥ��ѡA�Эץ���A���x�s", "���ҥ���", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DSXmlHelper InsertHelper = new DSXmlHelper("InsertRequest");
            DSXmlHelper updateHelper = new DSXmlHelper("UpdateRequest");
            List<string> deleteList = new List<string>();
            //ISemester semester = SemesterProvider.GetInstance();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                StudentRowTag tag = row.Tag as StudentRowTag;
                //semester.SetDate(tag.Date);

                //log �����ק�᪺��� �������
                if (!afterData.ContainsKey(tag.Student.ID))
                    afterData.Add(tag.Student.ID, new Dictionary<string, string>());

                if (tag.RowID == null)
                {
                    DSXmlHelper h2 = new DSXmlHelper("Attendance");
                    bool hasContent = false;
                    for (int i = _startIndex; i < dataGridView.Columns.Count; i++)
                    {
                        DataGridViewCell cell = row.Cells[i];
                        if (cell.Value == null) continue;

                        PeriodInfo pinfo = dataGridView.Columns[i].Tag as PeriodInfo;
                        AbsenceCellInfo acInfo = cell.Tag as AbsenceCellInfo;
                        AbsenceInfo ainfo = acInfo.AbsenceInfo;
                        XmlElement element = h2.AddElement("Period");
                        element.InnerText = pinfo.Name;
                        element.SetAttribute("AbsenceType", ainfo.Name);
                        element.SetAttribute("AttendanceType", pinfo.Type);
                        hasContent = true;

                        //log �����ק�᪺��� ���m���ӳ���
                        if (!afterData[tag.Student.ID].ContainsKey(pinfo.Name))
                            afterData[tag.Student.ID].Add(pinfo.Name, ainfo.Name);
                    }
                    if (hasContent)
                    {
                        InsertHelper.AddElement("Attendance");
                        InsertHelper.AddElement("Attendance", "Field");
                        InsertHelper.AddElement("Attendance/Field", "RefStudentID", tag.Student.ID);
                        InsertHelper.AddElement("Attendance/Field", "SchoolYear", row.Cells[4].Value.ToString());
                        InsertHelper.AddElement("Attendance/Field", "Semester", row.Cells[5].Value.ToString());
                        InsertHelper.AddElement("Attendance/Field", "OccurDate", startDate.DateString);
                        InsertHelper.AddElement("Attendance/Field", "Detail", h2.GetRawXml(), true);
                    }

                }
                else // �Y�O�쥻�N��������
                {
                    DSXmlHelper h2 = new DSXmlHelper("Attendance");
                    bool hasContent = false;
                    for (int i = _startIndex; i < dataGridView.Columns.Count; i++)
                    {
                        DataGridViewCell cell = row.Cells[i];
                        if (cell.Value == null) continue;

                        PeriodInfo pinfo = dataGridView.Columns[i].Tag as PeriodInfo;
                        AbsenceCellInfo acInfo = cell.Tag as AbsenceCellInfo;
                        AbsenceInfo ainfo = acInfo.AbsenceInfo;

                        XmlElement element = h2.AddElement("Period");
                        element.InnerText = pinfo.Name;
                        element.SetAttribute("AbsenceType", ainfo.Name);
                        element.SetAttribute("AttendanceType", pinfo.Type);
                        hasContent = true;

                        //log �����ק�᪺��� ���m���ӳ���
                        if (!afterData[tag.Student.ID].ContainsKey(pinfo.Name))
                            afterData[tag.Student.ID].Add(pinfo.Name, ainfo.Name);
                    }

                    if (hasContent)
                    {
                        updateHelper.AddElement("Attendance");
                        updateHelper.AddElement("Attendance", "Field");
                        updateHelper.AddElement("Attendance/Field", "RefStudentID", tag.Student.ID);
                        updateHelper.AddElement("Attendance/Field", "SchoolYear", row.Cells[4].Value.ToString());
                        updateHelper.AddElement("Attendance/Field", "Semester", row.Cells[5].Value.ToString());
                        updateHelper.AddElement("Attendance/Field", "OccurDate", startDate.DateString);
                        updateHelper.AddElement("Attendance/Field", "Detail", h2.GetRawXml(), true);
                        updateHelper.AddElement("Attendance", "Condition");
                        updateHelper.AddElement("Attendance/Condition", "ID", tag.RowID);
                    }
                    else
                    {
                        deleteList.Add(tag.RowID);

                        //log �����Q�R�������
                        afterData.Remove(tag.Student.ID);
                        deleteData.Add(tag.Student.ID);
                    }
                }
            }
            try
            {
                if (InsertHelper.GetElements("Attendance").Length > 0)
                {
                    SmartSchool.Feature.Student.EditAttendance.Insert(new DSRequest(InsertHelper));

                    //log �g�Jlog
                    foreach (string studentid in afterData.Keys)
                    {
                        if (!beforeData.ContainsKey(studentid) && afterData[studentid].Count > 0)
                        {
                            StringBuilder desc = new StringBuilder("");
                            desc.AppendLine("�ǥͩm�W�G" + SmartSchool.StudentRelated.Student.Instance.Items[studentid].Name + " ");
                            desc.AppendLine("����G" + logDate.ToShortDateString() + " ");
                            foreach (string period in afterData[studentid].Keys)
                            {
                                desc.AppendLine("�`���u" + period + "�v���u" + afterData[studentid][period] + "�v ");
                            }
                            CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Insert, studentid, desc.ToString(), this.Text, "");

                        }
                    }
                }
                if (updateHelper.GetElements("Attendance").Length > 0)
                {
                    SmartSchool.Feature.Student.EditAttendance.Update(new DSRequest(updateHelper));

                    //log �g�Jlog
                    foreach (string studentid in afterData.Keys)
                    {
                        if (beforeData.ContainsKey(studentid) && afterData[studentid].Count > 0)
                        {
                            bool dirty = false;
                            StringBuilder desc = new StringBuilder("");
                            desc.AppendLine("�ǥͩm�W�G" + SmartSchool.StudentRelated.Student.Instance.Items[studentid].Name + " ");
                            desc.AppendLine("����G" + logDate.ToShortDateString() + " ");
                            foreach (string period in beforeData[studentid].Keys)
                            {
                                if (!afterData[studentid].ContainsKey(period))
                                    afterData[studentid].Add(period, "");
                            }
                            foreach (string period in afterData[studentid].Keys)
                            {
                                if (beforeData[studentid].ContainsKey(period))
                                {
                                    if (beforeData[studentid][period] != afterData[studentid][period])
                                    {
                                        dirty = true;
                                        desc.AppendLine("�`���u" + period + "�v�ѡu" + beforeData[studentid][period] + "�v�ܧ󬰡u" + afterData[studentid][period] + "�v ");
                                    }
                                }
                                else
                                {
                                    dirty = true;
                                    desc.AppendLine("�`���u" + period + "�v���u" + afterData[studentid][period] + "�v ");
                                }

                            }
                            if (dirty)
                                CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Update, studentid, desc.ToString(), this.Text, "");
                        }
                    }
                }
                if (deleteList.Count > 0)
                {
                    DSXmlHelper deleteHelper = new DSXmlHelper("DeleteRequest");
                    deleteHelper.AddElement("Attendance");
                    foreach (string key in deleteList)
                    {
                        deleteHelper.AddElement("Attendance", "ID", key);
                    }
                    SmartSchool.Feature.Student.EditAttendance.Delete(new DSRequest(deleteHelper));

                    //log �g�J�Q�R������ƪ�log
                    foreach (string studentid in deleteData)
                    {
                        StringBuilder desc = new StringBuilder("");
                        desc.AppendLine("�ǥͩm�W�G" + SmartSchool.StudentRelated.Student.Instance.Items[studentid].Name + " ");
                        desc.AppendLine("�R�� " + logDate.ToShortDateString() + " ���m���� ");
                        CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Delete, studentid, desc.ToString(), this.Text, "");
                    }
                }
                List<string> studentids = new List<string>();
                foreach (BriefStudentData var in _students)
                {
                    studentids.Add(var.ID);
                }
                if (studentids.Count > 0)
                    SmartSchool.StudentRelated.Student.Instance.InvokAttendanceChanged(studentids.ToArray());
                MsgBox.Show("�x�s����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show("���m�����x�s���� : " + ex.Message, "�x�s����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValid()
        {
            _errProvider.Clear();
            if (!startDate.IsValid)
            {
                _errProvider.SetError(startDate, "����榡���~");
                return false;
            }
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!string.IsNullOrEmpty(cell.ErrorText))
                        return false;
                    //if (cell.Style.BackColor == Color.Red)
                    //    return false;
                }
            }
            return true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _errProvider.Clear();
            if (!startDate.IsValid)
            {
                _errProvider.SetError(startDate, "����榡���~");
                return;
            }
            SearchStudentRange();
            GetAbsense();
            chkHasData_CheckedChanged(null, null);
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {

            string key = KeyConverter.GetKeyMapping(e);

            if (!_absenceList.ContainsKey(key))
            {
                if (e.KeyCode != Keys.Space && e.KeyCode != Keys.Delete) return;
                foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                {
                    if (cell.ColumnIndex < _startIndex) continue;
                    cell.Value = null;
                    AbsenceCellInfo acInfo = cell.Tag as AbsenceCellInfo;
                    if (acInfo != null)
                        acInfo.SetValue(null);
                }
            }
            else
            {
                AbsenceInfo info = _absenceList[key];
                foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                {
                    if (cell.ColumnIndex < _startIndex) continue;
                    AbsenceCellInfo acInfo = cell.Tag as AbsenceCellInfo;

                    if (acInfo == null)
                    {
                        acInfo = new AbsenceCellInfo();
                    }
                    acInfo.SetValue(info);

                    if (acInfo.IsValueChanged)
                        cell.Value = acInfo.AbsenceInfo.Abbreviation;
                    else
                    {
                        cell.Value = string.Empty;
                        acInfo.SetValue(AbsenceInfo.Empty);
                    }
                    cell.Tag = acInfo;
                }
            }
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (e.ColumnIndex < _startIndex) return;
            if (e.RowIndex < 0) return;
            DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

            cell.Value = _checkedAbsence.Abbreviation;
            AbsenceCellInfo acInfo = cell.Tag as AbsenceCellInfo;
            if (acInfo == null)
            {
                acInfo = new AbsenceCellInfo();
            }
            acInfo.SetValue(_checkedAbsence);
            if (acInfo.IsValueChanged)
                cell.Value = acInfo.AbsenceInfo.Abbreviation;
            else
            {
                cell.Value = string.Empty;
                acInfo.SetValue(AbsenceInfo.Empty);
            }
            cell.Tag = acInfo;
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataGridViewColumn column = dataGridView.Columns[e.ColumnIndex];
            if (column.HeaderText == "�Ǧ~��")
            {
                string errorMessage = "";
                int schoolYear;
                if (cell.Value == null)
                    errorMessage = "�Ǧ~�פ��i���ť�";
                else if (!int.TryParse(cell.Value.ToString(), out schoolYear))
                    errorMessage = "�Ǧ~�ץ��������";

                if (errorMessage != "")
                {
                    cell.ErrorText = errorMessage;
                    //cell.Style.BackColor = Color.Red;
                    //cell.ToolTipText = errorMessage;
                }
                else
                {
                    cell.ErrorText = string.Empty;
                    SemesterCellInfo cinfo = cell.Tag as SemesterCellInfo;
                    cinfo.SetValue(cell.Value == null ? string.Empty : cell.Value.ToString());
                    //cell.Style.BackColor = Color.White;
                    //cell.ToolTipText = "";
                }
            }
            else if (column.HeaderText == "�Ǵ�")
            {
                string errorMessage = string.Empty;

                if (cell.Value == null)
                    errorMessage = "�Ǵ����i���ť�";
                else if (cell.Value.ToString() != "1" && cell.Value.ToString() != "2")
                    errorMessage = "�Ǵ���������ơy1�z�Ρy2�z";

                if (errorMessage != string.Empty)
                {
                    cell.ErrorText = errorMessage;
                    //cell.Style.BackColor = Color.Red;
                    //cell.ToolTipText = errorMessage;
                }
                else
                {
                    cell.ErrorText = string.Empty;
                    SemesterCellInfo cinfo = cell.Tag as SemesterCellInfo;
                    cinfo.SetValue(cell.Value == null ? string.Empty : cell.Value.ToString());
                    //cell.Style.BackColor = Color.White;
                    //cell.ToolTipText = "";
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (IsDirty())
            {
                if (MsgBox.Show("��Ƥw�ܧ�B�|���x�s�A�O�_���w�s����?", "�T�{", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
                this.Close();
        }

        private void startDate_Validated(object sender, EventArgs e)
        {
            _errProvider.Clear();
            if (!startDate.IsValid)
            {
                _errProvider.SetError(startDate, "����榡���~");
                return;
            }

            if (IsDirty())
            {
                if (MsgBox.Show("��Ƥw�ܧ�B�|���x�s�A�O�_���w�s����?", "�T�{", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    startDate.SetDate(_currentDate.ToShortDateString());
                    return;
                }
            }

            SaveDateSetting();

            _currentDate = startDate.GetDate();
            SearchStudentRange();
            GetAbsense();
            chkHasData_CheckedChanged(null, null);
        }

        private void SaveDateSetting()
        {
            bool locked = bool.Parse(picLock.Tag.ToString());

            if (locked)
            {
                DSXmlHelper helper = new DSXmlHelper("Attendance_MutiEditor");
                XmlElement element = helper.AddElement("Date");
                element.InnerText = startDate.GetDate().ToShortDateString();

                element = helper.AddElement("Locked");
                element.InnerText = picLock.Tag.ToString();

                SmartSchool.CurrentUser.Instance.Preference["Attendance_MutiEditor"] = helper.BaseElement;
            }
            else
            {
                XmlElement element = SmartSchool.CurrentUser.Instance.Preference["Attendance_MutiEditor"];
                if (element == null)
                {
                    DSXmlHelper helper = new DSXmlHelper("Attendance_MutiEditor");
                    XmlElement e = helper.AddElement("Date");
                    e.InnerText = startDate.GetDate().ToShortDateString();

                    e = helper.AddElement("Locked");
                    e.InnerText = "false";
                    SmartSchool.CurrentUser.Instance.Preference["Attendance_MutiEditor"] = helper.BaseElement;
                }
                else
                {
                    element.SelectSingleNode("Locked").InnerText = "false";
                    SmartSchool.CurrentUser.Instance.Preference["Attendance_MutiEditor"] = element;
                }
            }
        }

        private void picLock_Click(object sender, EventArgs e)
        {
            bool isLock = false;
            if (picLock.Tag != null)
            {
                if (!bool.TryParse(picLock.Tag.ToString(), out isLock))
                    isLock = false;
            }
            if (isLock)
            {
                picLock.Image = Resources.unlocked;
                picLock.Tag = false;
                toolTip.SetToolTip(picLock, "���m�n�����������w���A�A�z�i�H�I��ϥܡA�N�S�w����϶���w�C");
            }
            else
            {
                picLock.Image = Resources.locked;
                picLock.Tag = true;
                toolTip.SetToolTip(picLock, "���m�n������w��w�A�z�i�H�I��ϥܸѰ���w�C");
            }
        }

        private bool IsDirty()
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Tag == null) continue;
                    if (cell.Tag is SemesterCellInfo)
                    {
                        SemesterCellInfo cInfo = cell.Tag as SemesterCellInfo;
                        if (cInfo.IsDirty) return true;
                    }
                    else if (cell.Tag is AbsenceCellInfo)
                    {
                        AbsenceCellInfo cInfo = cell.Tag as AbsenceCellInfo;
                        if (cInfo.IsDirty) return true;
                    }
                }
            }
            return false;
        }

        private void chkHasData_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView.SuspendLayout();

            if (chkHasData.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    bool hasData = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ColumnIndex < _startIndex) continue;
                        if (!string.IsNullOrEmpty("" + cell.Value))
                        {
                            hasData = true;
                            break;
                        }
                    }
                    if (hasData == false)
                    {
                        _hiddenRows.Add(row);
                        row.Visible = false;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in _hiddenRows)
                    row.Visible = true;
            }

            dataGridView.ResumeLayout();
        }
    }

    public class StudentRowTag
    {
        private BriefStudentData _student;

        public BriefStudentData Student
        {
            get { return _student; }
            set { _student = value; }
        }
        private string _RowID;

        public string RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
    }
}