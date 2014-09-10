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
using SmartSchool.StudentRelated;
using SmartSchool.ApplicationLog;
using SmartSchool.Properties;

namespace SmartSchool.StudentRelated.RibbonBars.AttendanceEditor
{
    public partial class SingleEditor : BaseForm
    {
        private AbsenceInfo _checkedAbsence;
        private Dictionary<string, AbsenceInfo> _absenceList;
        private BriefStudentData _student;
        private ISemester _semesterProvider;
        private int _startIndex;
        private ErrorProvider _errorProvider;
        private DateTime _currentStartDate;
        private DateTime _currentEndDate;

        List<DataGridViewRow> _hiddenRows;

        System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

        //log �ݭn�Ψ쪺
        private Dictionary<string, Dictionary<string, string>> beforeData = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, Dictionary<string, string>> afterData = new Dictionary<string, Dictionary<string, string>>();
        private List<string> deleteData = new List<string>();

        public SingleEditor(BriefStudentData student)
        {
            _errorProvider = new ErrorProvider();
            _student = student;
            InitializeComponent();
            _absenceList = new Dictionary<string, AbsenceInfo>();
            _semesterProvider = SemesterProvider.GetInstance(); 
            _startIndex = 4;

            _hiddenRows = new List<DataGridViewRow>();
        }

        private void SingleEditor_Load(object sender, EventArgs e)
        {
            this.Text = "�i" + _student.Name + "�j���m�޲z";
            lblInfo.Text = "�ǥͩm�W�G<b>" + _student.Name + "</b>�@�Ǹ��G<b>" + _student.StudentNumber + "</b>";
            InitializeRadioButton();
            InitializeDateRange();
            InitializeDataGridView();
            SearchDateRange();
            GetAbsense();
            LoadAbsense();

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

        private void GetAbsense()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "RefStudentID", _student.ID);
            helper.AddElement("Condition", "StartDate", startDate.DateString);
            helper.AddElement("Condition", "EndDate", endDate.DateString);
            DSResponse dsrsp = SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper));
            helper = dsrsp.GetContent();

            //log �M�� beforeData
            beforeData.Clear();

            foreach (XmlElement element in helper.GetElements("Attendance"))
            {
                // �o�̭n���@�ǨƱ�  �Ҧp���F�a��i�h
                string occurDate = element.SelectSingleNode("OccurDate").InnerText;
                string schoolYear = element.SelectSingleNode("SchoolYear").InnerText;
                string semester = element.SelectSingleNode("Semester").InnerText;
                string id = element.GetAttribute("ID");
                XmlNode dNode = element.SelectSingleNode("Detail").FirstChild;

                //log �����ק�e����� �������
                DateTime logDate;
                if (DateTime.TryParse(occurDate, out logDate))
                {
                    if (!beforeData.ContainsKey(logDate.ToShortDateString()))
                        beforeData.Add(logDate.ToShortDateString(), new Dictionary<string, string>());
                }

                DataGridViewRow row = null;
                foreach (DataGridViewRow r in dataGridView.Rows)
                {
                    DateTime date;
                    RowTag rt = r.Tag as RowTag;

                    if (!DateTime.TryParse(occurDate, out date)) continue;
                    if (date.CompareTo(rt.Date) != 0) continue;
                    row = r;
                }

                if (row == null) continue;
                RowTag rowTag = row.Tag as RowTag;
                rowTag.IsNew = false;
                rowTag.Key = id;

                row.Cells[2].Value = schoolYear;
                row.Cells[2].Tag = new SemesterCellInfo(schoolYear);

                row.Cells[3].Value = semester;
                row.Cells[3].Tag = new SemesterCellInfo(semester);

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
                            if (!beforeData[logDate.ToShortDateString()].ContainsKey(info.Name))
                                beforeData[logDate.ToShortDateString()].Add(info.Name, ai.Name);

                            break;
                        }
                    }
                }
            }
        }

        private void InitializeDateRange()
        {
            XmlElement info = SmartSchool.CurrentUser.Instance.Preference["Attendance_SingleEditor"];
            if (info == null)
            {
                InitializeStartDate();
                endDate.SetDate(DateTime.Today.ToShortDateString());
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
                    startDate.SetDate(info.SelectSingleNode("StartDate").InnerText);
                    endDate.SetDate(info.SelectSingleNode("EndDate").InnerText);
                    picLock.Image = Resources.locked;
                    picLock.Tag = true;
                }
                else
                {
                    InitializeStartDate();
                    endDate.SetDate(DateTime.Today.ToShortDateString());
                    picLock.Image = Resources.unlocked;
                    picLock.Tag = false;
                }
            }
            _currentStartDate = startDate.GetDate();
            _currentEndDate = endDate.GetDate();
        }

        private void InitializeStartDate()
        {
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            DateTime date = DateTime.Today;
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.Subtract(ts);
            }
            startDate.SetDate(date.ToShortDateString());
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
            dataGridView.Columns.Add("colDate", "���");
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[0].ReadOnly = true;

            dataGridView.Columns.Add("colWeek", "�P��");
            dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[1].ReadOnly = true;

            dataGridView.Columns.Add("colSchoolYear", "�Ǧ~��");
            dataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[2].ReadOnly = false;

            dataGridView.Columns.Add("colSemester", "�Ǵ�");
            dataGridView.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[3].ReadOnly = false;
            dataGridView.Columns[3].Frozen=true ;

            foreach (PeriodInfo info in collection.GetSortedList())
            {
                int columnIndex = dataGridView.Columns.Add(info.Name, info.Name);
                DataGridViewColumn column = dataGridView.Columns[columnIndex];
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.ReadOnly = true;
                column.Tag = info;
            }
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
            if ( rb.Checked )
            {
                _checkedAbsence = rb.Tag as AbsenceInfo;
                foreach ( DataGridViewCell cell in dataGridView.SelectedCells )
                {
                    if ( cell.ColumnIndex < _startIndex ) continue;
                    cell.Value = _checkedAbsence.Abbreviation;
                    AbsenceCellInfo acInfo = cell.Tag as AbsenceCellInfo;
                    if ( acInfo == null )
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAbsense();
        }

        private void LoadAbsense()
        {
            if (startDate.IsValid && endDate.IsValid)
            {
                SearchDateRange();
                GetAbsense();
                chkHasData_CheckedChanged(null, null);
                SaveDateSetting();
            }
        }

        private void SaveDateSetting()
        {            
            bool locked = bool.Parse(picLock.Tag.ToString());

            if (locked)
            {
                DSXmlHelper helper = new DSXmlHelper("Attendance_SingleEditor");
                XmlElement element = helper.AddElement("StartDate");
                element.InnerText = startDate.GetDate().ToShortDateString();

                element = helper.AddElement("EndDate");
                element.InnerText = endDate.GetDate().ToShortDateString();

                element = helper.AddElement("Locked");
                element.InnerText = picLock.Tag.ToString();
                SmartSchool.CurrentUser.Instance.Preference["Attendance_SingleEditor"] = helper.BaseElement;
            }
            else
            {
                XmlElement element = SmartSchool.CurrentUser.Instance.Preference["Attendance_SingleEditor"];
                if (element == null)
                {
                    DSXmlHelper helper = new DSXmlHelper("Attendance_SingleEditor");
                    XmlElement e = helper.AddElement("StartDate");
                    e.InnerText = startDate.GetDate().ToShortDateString();

                    e = helper.AddElement("EndDate");
                    e.InnerText = endDate.GetDate().ToShortDateString();

                    e = helper.AddElement("Locked");
                    e.InnerText = "false";
                    SmartSchool.CurrentUser.Instance.Preference["Attendance_SingleEditor"] = helper.BaseElement;
                }
                else
                {
                    element.SelectSingleNode("Locked").InnerText = "false";
                    SmartSchool.CurrentUser.Instance.Preference["Attendance_SingleEditor"] = element;
                }                
            }            
        }

        private void SearchDateRange()
        {
            DateTime start = startDate.GetDate();
            DateTime end = endDate.GetDate();
            if (start.AddDays(120).CompareTo(end) < 1)
            {
                MsgBox.Show("�ҿ��������d�򤣱o�W�L120��A�нվ����d��᭫�s�d��");
                return;
            }

            DateTime date = start;
            dataGridView.Rows.Clear();
            while (date.CompareTo(end) <= 0)
            {
                if (!chkSunday.Checked && date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(1);
                    continue;
                }
                string dateValue = date.ToShortDateString();
                int rowIndex = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[rowIndex];
                RowTag tag = new RowTag();
                tag.Date = date;
                tag.IsNew = true;
                row.Tag = tag;
                row.Cells[0].Value = dateValue;
                row.Cells[1].Value = GetDayOfWeekInChinese(date.DayOfWeek);
                _semesterProvider.SetDate(date);
                row.Cells[2].Value = _semesterProvider.SchoolYear;
                row.Cells[3].Value = _semesterProvider.Semester;
                date = date.AddDays(1);
            }
        }

        private string GetDayOfWeekInChinese(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "�@";
                case DayOfWeek.Tuesday:
                    return "�G";
                case DayOfWeek.Wednesday:
                    return "�T";
                case DayOfWeek.Thursday:
                    return "�|";
                case DayOfWeek.Friday:
                    return "��";
                case DayOfWeek.Saturday:
                    return "��";
                default:
                    return "��";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        private void chkSunday_CheckedChanged(object sender, EventArgs e)
        {
            SearchDateRange();
            GetAbsense();
            chkHasData_CheckedChanged(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                MsgBox.Show("������ҥ��ѡA�Эץ���A���x�s", "���ҥ���", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DSXmlHelper InsertHelper = new DSXmlHelper("InsertRequest");
            DSXmlHelper updateHelper = new DSXmlHelper("UpdateRequest");
            List<string> deleteList = new List<string>();

            ISemester semester = SemesterProvider.GetInstance();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                RowTag tag = row.Tag as RowTag;
                semester.SetDate(tag.Date);

                //log �����ק�᪺��� �������
                if (!afterData.ContainsKey(tag.Date.ToShortDateString()))
                    afterData.Add(tag.Date.ToShortDateString(), new Dictionary<string, string>());

                if (tag.IsNew)
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
                        if (!afterData[tag.Date.ToShortDateString()].ContainsKey(pinfo.Name))
                            afterData[tag.Date.ToShortDateString()].Add(pinfo.Name, ainfo.Name);

                    }
                    if (hasContent)
                    {
                        InsertHelper.AddElement("Attendance");
                        InsertHelper.AddElement("Attendance", "Field");
                        InsertHelper.AddElement("Attendance/Field", "RefStudentID", _student.ID);
                        InsertHelper.AddElement("Attendance/Field", "SchoolYear", row.Cells[2].Value.ToString());
                        InsertHelper.AddElement("Attendance/Field", "Semester", row.Cells[3].Value.ToString());
                        InsertHelper.AddElement("Attendance/Field", "OccurDate", tag.Date.ToShortDateString());
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
                        if (!afterData[tag.Date.ToShortDateString()].ContainsKey(pinfo.Name))
                            afterData[tag.Date.ToShortDateString()].Add(pinfo.Name, ainfo.Name);
                    }

                    if (hasContent)
                    {
                        updateHelper.AddElement("Attendance");
                        updateHelper.AddElement("Attendance", "Field");
                        updateHelper.AddElement("Attendance/Field", "RefStudentID", _student.ID);
                        updateHelper.AddElement("Attendance/Field", "SchoolYear", row.Cells[2].Value.ToString());
                        updateHelper.AddElement("Attendance/Field", "Semester", row.Cells[3].Value.ToString());
                        updateHelper.AddElement("Attendance/Field", "OccurDate", tag.Date.ToShortDateString());
                        updateHelper.AddElement("Attendance/Field", "Detail", h2.GetRawXml(), true);
                        updateHelper.AddElement("Attendance", "Condition");
                        updateHelper.AddElement("Attendance/Condition", "ID", tag.Key);
                    }
                    else
                    {
                        deleteList.Add(tag.Key);

                        //log �����Q�R�������
                        afterData.Remove(tag.Date.ToShortDateString());
                        deleteData.Add(tag.Date.ToShortDateString());
                    }
                }
            }
            try
            {
                if (InsertHelper.GetElements("Attendance").Length > 0)
                {
                    SmartSchool.Feature.Student.EditAttendance.Insert(new DSRequest(InsertHelper));

                    //log �g�Jlog
                    foreach (string date in afterData.Keys)
                    {
                        if (!beforeData.ContainsKey(date) && afterData[date].Count > 0)
                        {
                            StringBuilder desc = new StringBuilder("");
                            desc.AppendLine("�ǥͩm�W�G" + SmartSchool.StudentRelated.Student.Instance.Items[_student.ID].Name + " ");
                            desc.AppendLine("����G" + date + " ");
                            foreach (string period in afterData[date].Keys)
                            {
                                desc.AppendLine("�`���u" + period + "�v���u" + afterData[date][period] + "�v ");
                            }
                            CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Insert, _student.ID, desc.ToString(), this.Text, "");
                        }
                    }
                }
                if (updateHelper.GetElements("Attendance").Length > 0)
                {
                    SmartSchool.Feature.Student.EditAttendance.Update(new DSRequest(updateHelper));

                    //log �g�Jlog
                    foreach (string date in afterData.Keys)
                    {
                        if (beforeData.ContainsKey(date) && afterData[date].Count > 0)
                        {
                            bool dirty = false;
                            StringBuilder desc = new StringBuilder("");
                            desc.AppendLine("�ǥͩm�W�G" + SmartSchool.StudentRelated.Student.Instance.Items[_student.ID].Name + " ");
                            desc.AppendLine("����G" + date + " ");
                            foreach (string period in beforeData[date].Keys)
                            {
                                if (!afterData[date].ContainsKey(period))
                                    afterData[date].Add(period, "");
                            }
                            foreach (string period in afterData[date].Keys)
                            {
                                if (beforeData[date].ContainsKey(period))
                                {
                                    if (beforeData[date][period] != afterData[date][period])
                                    {
                                        dirty = true;
                                        desc.AppendLine("�`���u" + period + "�v�ѡu" + beforeData[date][period] + "�v�ܧ󬰡u" + afterData[date][period] + "�v ");
                                    }
                                }
                                else
                                {
                                    dirty = true;
                                    desc.AppendLine("�`���u" + period + "�v���u" + afterData[date][period] + "�v ");
                                }

                            }
                            if (dirty)
                                CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Update, _student.ID, desc.ToString(), this.Text, "");
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
                    StringBuilder desc = new StringBuilder("");
                    desc.AppendLine("�ǥͩm�W�G" + SmartSchool.StudentRelated.Student.Instance.Items[_student.ID].Name + " ");
                    foreach (string date in deleteData)
                    {
                        desc.AppendLine("�R�� " + date + " ���m���� ");
                    }
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Delete, _student.ID, desc.ToString(), this.Text, "");
                }
                //Ĳ�o�ܧ�ƥ�
                SmartSchool.StudentRelated.Student.Instance.InvokAttendanceChanged(_student.ID);
                MsgBox.Show("�x�s����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show("���m�����x�s���� : " + ex.Message, "�x�s����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SaveDateSetting();
        }

        private bool IsValid()
        {
            if (_errorProvider.GetError(startDate) != string.Empty)
                return false;
            if (_errorProvider.GetError(endDate) != string.Empty)
                return false;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ErrorText != string.Empty)
                        return false;
                }
            }
            return true;
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (e.ColumnIndex == 2)
            {
                string errorMessage = "";
                int schoolYear;
                if (cell.Value == null)
                    errorMessage = "�Ǧ~�פ��i���ť�";
                else if (!int.TryParse(cell.Value.ToString(), out schoolYear))
                    errorMessage = "�Ǧ~�ץ��������";

                if (errorMessage != "")
                {
                    cell.Style.BackColor = Color.Red;
                    cell.ToolTipText = errorMessage;
                }
                else
                {
                    cell.Style.BackColor = Color.White;
                    cell.ToolTipText = "";
                }
            }
            else if (e.ColumnIndex == 3)
            {
                string errorMessage = "";

                if (cell.Value == null)
                    errorMessage = "�Ǵ����i���ť�";
                else if (cell.Value.ToString() != "1" && cell.Value.ToString() != "2")
                    errorMessage = "�Ǵ���������ơy1�z�Ρy2�z";

                if (errorMessage != "")
                {
                    cell.ErrorText = errorMessage;
                }
                else
                {
                    cell.ErrorText = string.Empty;
                }
            }
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

        private void startDate_Validated(object sender, EventArgs e)
        {            
            _errorProvider.SetError(startDate, string.Empty);

            if (!startDate.IsValid)
            {
                _errorProvider.SetError(startDate, "����榡���~");
                return;
            }

            if (IsDirty())
            {
                if (MsgBox.Show("��Ƥw�ܧ�B�|���x�s�A�O�_���w�s����?", "�T�{", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    startDate.SetDate(_currentStartDate.ToShortDateString());
                    return;
                }
            }
            _currentStartDate = startDate.GetDate();
            dataGridView.Rows.Clear();
            LoadAbsense();
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
            SaveDateSetting();
        }

        private void endDate_Validated(object sender, EventArgs e)
        {            
            _errorProvider.SetError(endDate, string.Empty);

            if (!endDate.IsValid)
            {
                _errorProvider.SetError(endDate, "����榡���~");
                return;
            }

            if (IsDirty())
            {
                if (MsgBox.Show("��Ƥw�ܧ�B�|���x�s�A�O�_���w�s����?", "�T�{", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    endDate.SetDate(_currentEndDate.ToShortDateString());
                    return;
                }
            }
            _currentEndDate = endDate.GetDate();
            dataGridView.Rows.Clear();
            LoadAbsense();           
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

        private void SingleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveDateSetting();
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

    class RowTag
    {
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        private bool _isNew;

        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
    }
}