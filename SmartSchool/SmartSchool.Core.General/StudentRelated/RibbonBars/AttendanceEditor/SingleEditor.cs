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

        //log 需要用到的
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
            this.Text = "【" + _student.Name + "】缺曠管理";
            lblInfo.Text = "學生姓名：<b>" + _student.Name + "</b>　學號：<b>" + _student.StudentNumber + "</b>";
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
                toolTip.SetToolTip(picLock, "缺曠登錄日期已鎖定，您可以點選圖示解除鎖定。");
            else
                toolTip.SetToolTip(picLock, "缺曠登錄日期為未鎖定狀態，您可以點選圖示，將特定日期區間鎖定。");
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

            //log 清除 beforeData
            beforeData.Clear();

            foreach (XmlElement element in helper.GetElements("Attendance"))
            {
                // 這裡要做一些事情  例如找到東家塞進去
                string occurDate = element.SelectSingleNode("OccurDate").InnerText;
                string schoolYear = element.SelectSingleNode("SchoolYear").InnerText;
                string semester = element.SelectSingleNode("Semester").InnerText;
                string id = element.GetAttribute("ID");
                XmlNode dNode = element.SelectSingleNode("Detail").FirstChild;

                //log 紀錄修改前的資料 日期部分
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

                            //log 紀錄修改前的資料 缺曠明細部分
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
            dataGridView.Columns.Add("colDate", "日期");
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[0].ReadOnly = true;

            dataGridView.Columns.Add("colWeek", "星期");
            dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[1].ReadOnly = true;

            dataGridView.Columns.Add("colSchoolYear", "學年度");
            dataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[2].ReadOnly = false;

            dataGridView.Columns.Add("colSemester", "學期");
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
                MsgBox.Show("所選取的日期範圍不得超過120日，請調整日期範圍後重新查詢");
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
                    return "一";
                case DayOfWeek.Tuesday:
                    return "二";
                case DayOfWeek.Wednesday:
                    return "三";
                case DayOfWeek.Thursday:
                    return "四";
                case DayOfWeek.Friday:
                    return "五";
                case DayOfWeek.Saturday:
                    return "六";
                default:
                    return "日";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (IsDirty())
            {
                if (MsgBox.Show("資料已變更且尚未儲存，是否放棄已編輯資料?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                MsgBox.Show("資料驗證失敗，請修正後再行儲存", "驗證失敗", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

                //log 紀錄修改後的資料 日期部分
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

                        //log 紀錄修改後的資料 缺曠明細部分
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
                else // 若是原本就有紀錄的
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

                        //log 紀錄修改後的資料 缺曠明細部分
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

                        //log 紀錄被刪除的資料
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

                    //log 寫入log
                    foreach (string date in afterData.Keys)
                    {
                        if (!beforeData.ContainsKey(date) && afterData[date].Count > 0)
                        {
                            StringBuilder desc = new StringBuilder("");
                            desc.AppendLine("學生姓名：" + SmartSchool.StudentRelated.Student.Instance.Items[_student.ID].Name + " ");
                            desc.AppendLine("日期：" + date + " ");
                            foreach (string period in afterData[date].Keys)
                            {
                                desc.AppendLine("節次「" + period + "」為「" + afterData[date][period] + "」 ");
                            }
                            CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Insert, _student.ID, desc.ToString(), this.Text, "");
                        }
                    }
                }
                if (updateHelper.GetElements("Attendance").Length > 0)
                {
                    SmartSchool.Feature.Student.EditAttendance.Update(new DSRequest(updateHelper));

                    //log 寫入log
                    foreach (string date in afterData.Keys)
                    {
                        if (beforeData.ContainsKey(date) && afterData[date].Count > 0)
                        {
                            bool dirty = false;
                            StringBuilder desc = new StringBuilder("");
                            desc.AppendLine("學生姓名：" + SmartSchool.StudentRelated.Student.Instance.Items[_student.ID].Name + " ");
                            desc.AppendLine("日期：" + date + " ");
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
                                        desc.AppendLine("節次「" + period + "」由「" + beforeData[date][period] + "」變更為「" + afterData[date][period] + "」 ");
                                    }
                                }
                                else
                                {
                                    dirty = true;
                                    desc.AppendLine("節次「" + period + "」為「" + afterData[date][period] + "」 ");
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

                    //log 寫入被刪除的資料的log
                    StringBuilder desc = new StringBuilder("");
                    desc.AppendLine("學生姓名：" + SmartSchool.StudentRelated.Student.Instance.Items[_student.ID].Name + " ");
                    foreach (string date in deleteData)
                    {
                        desc.AppendLine("刪除 " + date + " 缺曠紀錄 ");
                    }
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Delete, _student.ID, desc.ToString(), this.Text, "");
                }
                //觸發變更事件
                SmartSchool.StudentRelated.Student.Instance.InvokAttendanceChanged(_student.ID);
                MsgBox.Show("儲存完畢", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show("缺曠紀錄儲存失敗 : " + ex.Message, "儲存失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    errorMessage = "學年度不可為空白";
                else if (!int.TryParse(cell.Value.ToString(), out schoolYear))
                    errorMessage = "學年度必須為整數";

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
                    errorMessage = "學期不可為空白";
                else if (cell.Value.ToString() != "1" && cell.Value.ToString() != "2")
                    errorMessage = "學期必須為整數『1』或『2』";

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
                _errorProvider.SetError(startDate, "日期格式錯誤");
                return;
            }

            if (IsDirty())
            {
                if (MsgBox.Show("資料已變更且尚未儲存，是否放棄已編輯資料?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                toolTip.SetToolTip(picLock, "缺曠登錄日期為未鎖定狀態，您可以點選圖示，將特定日期區間鎖定。");
            }
            else
            {
                picLock.Image = Resources.locked;
                picLock.Tag = true;
                toolTip.SetToolTip(picLock, "缺曠登錄日期已鎖定，您可以點選圖示解除鎖定。");
            }
            SaveDateSetting();
        }

        private void endDate_Validated(object sender, EventArgs e)
        {            
            _errorProvider.SetError(endDate, string.Empty);

            if (!endDate.IsValid)
            {
                _errorProvider.SetError(endDate, "日期格式錯誤");
                return;
            }

            if (IsDirty())
            {
                if (MsgBox.Show("資料已變更且尚未儲存，是否放棄已編輯資料?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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