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
using SmartSchool.StudentRelated.Palmerworm.Attendance;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;

namespace SmartSchool.StudentRelated.Palmerworm
{
    partial class AttendanceEditor : BaseForm
    {
        private ListView _listView;
        private ListViewItem _selectedItem;
        private AbsenceInfo _checkedAbsence;
        private Dictionary<string, AbsenceInfo> _absenceList;
        private Dictionary<string, string> _abbreviationList;
        private int _startIndex;
        private List<PeriodControl> _periodControls;
        private ErrorProvider _errorProvider;
        private string _studentid;
        private ISemester _semesterProvider;

        private DataValueManager _dataValueManager;

        public event EventHandler DataSaved;

        public AttendanceEditor(ListView listView, ListViewItem selectedItem, string studentid)
        {
            _startIndex = 4;
            InitializeComponent();
            _listView = listView;
            _selectedItem = selectedItem;
            _absenceList = new Dictionary<string, AbsenceInfo>();
            _abbreviationList = new Dictionary<string, string>();
            _periodControls = new List<PeriodControl>();
            _errorProvider = new ErrorProvider();
            _studentid = studentid;
            _semesterProvider = SemesterProvider.GetInstance();
            _dataValueManager = new DataValueManager();

            #region 初始化學年度學期ComboBox
            for (int i = 93; i <= CurrentUser.Instance.SchoolYear; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = i.ToString();
                comboBoxEx1.Items.Add(item);
            }
            comboBoxEx1.Text = _semesterProvider.SchoolYear.ToString();
            comboBoxEx2.Text = _semesterProvider.Semester.ToString();

            #endregion

            dateTimeTextBox1.SetDate(DateTime.Today.ToShortDateString());
            Text = "管理學生缺曠紀錄【新增模式】";
            if (_selectedItem != null)
            {
                dateTimeTextBox1.SetDate(_selectedItem.SubItems[2].Text);
                dateTimeTextBox1.ReadOnly = true;
                Text = "管理學生缺曠紀錄【修改模式】";

                comboBoxEx1.Text = _selectedItem.SubItems[0].Text;
                comboBoxEx2.Text = _selectedItem.SubItems[1].Text;
            }
        }

        private void AttendanceEditor_Load(object sender, EventArgs e)
        {
            InitializeRadioButton();
            for (int i = _startIndex; i < _listView.Columns.Count; i++)
            {
                ColumnHeader column = _listView.Columns[i];
                PeriodControl pc = new PeriodControl();
                pc.Label.Text = column.Text;
                pc.Label.Tag = column.Tag;

                pc.TextBox.ReadOnly = true;
                pc.TextBox.TextAlign = HorizontalAlignment.Center;
                pc.TextBox.MouseDoubleClick += new MouseEventHandler(TextBox_MouseDoubleClick);
                pc.TextBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
                pc.Tag = i - _startIndex;

                _periodControls.Add(pc);
                flpPeriod.Controls.Add(pc);
            }

            if (_selectedItem == null)
            {
                foreach (PeriodControl pc in _periodControls)
                {
                    _dataValueManager.AddValue(pc.Label.Text, "");
                }
                return;
            }

            for (int i = _startIndex; i < _selectedItem.SubItems.Count; i++)
            {
                System.Windows.Forms.ListViewItem.ListViewSubItem subitem = _selectedItem.SubItems[i];
                int index = i - _startIndex;
                PeriodControl pc = _periodControls[index];
                pc.TextBox.Text = subitem.Text;
                pc.TextBox.Tag = subitem.Tag;
                _dataValueManager.AddValue(pc.Label.Text, (pc.TextBox.Text == "") ? "" : _abbreviationList[pc.TextBox.Text]);
            }
        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            string key = KeyConverter.GetKeyMapping(e);
            TextBox textBox = sender as TextBox;

            if (!_absenceList.ContainsKey(key))
            {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Delete)
                {
                    textBox.Text = "";
                    textBox.Tag = null;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    int index;
                    if (!int.TryParse(textBox.Parent.Tag.ToString(), out index)) return;
                    index++;
                    if (index == _periodControls.Count) return;
                    _periodControls[index].TextBox.Focus();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    int index;
                    if (!int.TryParse(textBox.Parent.Tag.ToString(), out index)) return;
                    index--;
                    if (index == -1) return;
                    _periodControls[index].TextBox.Focus();
                }
            }
            else
            {
                AbsenceInfo info = _absenceList[key];
                textBox.Text = info.Abbreviation;
                textBox.Tag = info.Clone();
            }
        }

        void TextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == _checkedAbsence.Abbreviation)
            {
                textBox.Text = "";
                textBox.Tag = null;
                return;
            }
            textBox.Text = _checkedAbsence.Abbreviation;
            textBox.Tag = _checkedAbsence.Clone();
        }

        private void InitializeRadioButton()
        {
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetAbsenceList();
            DSXmlHelper helper = dsrsp.GetContent();
            foreach (XmlElement element in helper.GetElements("Absence"))
            {
                AbsenceInfo info = new AbsenceInfo(element);
                _absenceList.Add(info.Hotkey.ToUpper(), info);
                _abbreviationList.Add(info.Abbreviation, info.Name);

                RadioButton rb = new RadioButton();
                rb.Text = info.Name + "(" + info.Hotkey + ")";
                rb.AutoSize = true;
                rb.Font = new Font(FontStyles.GeneralFontFamily, 9.25f);
                rb.Tag = info;
                rb.CheckedChanged += new EventHandler(rb_CheckedChanged);
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
            _checkedAbsence = rb.Tag as AbsenceInfo;
        }

        private bool IsValid()
        {
            _errorProvider.Clear();
            // 驗證日期是否有填
            if (!this.dateTimeTextBox1.IsValid)
            {
                _errorProvider.SetError(dateTimeTextBox1, "日期格式錯誤");
                return false;
            }

            // 驗證日期是否已有紀錄,若是新增才檢查 , 修改則不需要
            if (_selectedItem == null)
            {
                foreach (ListViewItem item in _listView.Items)
                {
                    DateTime date;
                    if (!DateTime.TryParse(item.SubItems[2].Text, out date)) continue;
                    if (dateTimeTextBox1.GetDate().CompareTo(date) != 0) continue;

                    _errorProvider.SetError(dateTimeTextBox1, "此日期已有紀錄存在，請改用修改模式");
                    return false;
                }
            }

            // 驗證學年度學期
            int tryValue;
            if (!int.TryParse(comboBoxEx1.Text, out tryValue))
            {
                _errorProvider.SetError(comboBoxEx1, "學年度必須是數字");
                return false;
            }
            if (!int.TryParse(comboBoxEx2.Text, out tryValue))
            {
                _errorProvider.SetError(comboBoxEx2, "學期格式錯誤");
                return false;
            }
            else if(tryValue > 2 || tryValue < 1)
            {
                _errorProvider.SetError(comboBoxEx2, "學期格式錯誤");
                return false;
            }

            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                MsgBox.Show("資料驗證有誤，請修正後再行儲存");
                return;
            }
            try
            {
                StringBuilder updateDesc = new StringBuilder("");
                updateDesc.Append("學生姓名：").Append(Student.Instance.Items[_studentid].Name).Append(" \n");
                updateDesc.Append("日期：").Append(dateTimeTextBox1.Text).Append(" \n");

                StringBuilder insertDesc = new StringBuilder(updateDesc.ToString());
                StringBuilder deleteDesc = new StringBuilder(updateDesc.ToString());

                _semesterProvider.SetDate(dateTimeTextBox1.GetDate());
                DSXmlHelper h2 = new DSXmlHelper("Attendance");
                bool hasContent = false;
                foreach (PeriodControl pc in _periodControls)
                {
                    if (pc.TextBox.Text == "")
                    {
                        _dataValueManager.SetValue(pc.Label.Text, "");
                        continue;
                    }

                    PeriodInfo pinfo = pc.Label.Tag as PeriodInfo;
                    AbsenceInfo ainfo = pc.TextBox.Tag as AbsenceInfo;
                    XmlElement element = h2.AddElement("Period");
                    element.InnerText = pinfo.Name;
                    element.SetAttribute("AbsenceType", ainfo.Name);
                    element.SetAttribute("AttendanceType", pinfo.Type);
                    hasContent = true;
                }


                foreach (XmlElement var in h2.GetElements("Period"))
                {
                    string type = var.SelectSingleNode("@AbsenceType").InnerText;
                    string period = var.InnerText;
                    _dataValueManager.SetValue(period, type);
                }

                //log
                Dictionary<string, string> _changed = _dataValueManager.GetDirtyItems();
                foreach (string var in _changed.Keys)
                {
                    //「」
                    updateDesc.AppendLine("節次「" + var + "」由「" + _dataValueManager.GetOldValue(var) + "」變更為「" + _changed[var] + "」");
                }

                if (_selectedItem == null && !hasContent) //若是新增但無半筆紀錄
                {
                    MsgBox.Show("請先設定缺曠紀錄");
                    return;
                }
                else if (_selectedItem == null && hasContent)
                {
                    DSXmlHelper InsertHelper = new DSXmlHelper("InsertRequest");
                    InsertHelper.AddElement("Attendance");
                    InsertHelper.AddElement("Attendance", "Field");
                    InsertHelper.AddElement("Attendance/Field", "RefStudentID", _studentid);

                    //InsertHelper.AddElement("Attendance/Field", "SchoolYear", _semesterProvider.SchoolYear.ToString());
                    //InsertHelper.AddElement("Attendance/Field", "Semester", _semesterProvider.Semester.ToString());
                    InsertHelper.AddElement("Attendance/Field", "SchoolYear", comboBoxEx1.Text);
                    InsertHelper.AddElement("Attendance/Field", "Semester", comboBoxEx2.Text);

                    InsertHelper.AddElement("Attendance/Field", "OccurDate", dateTimeTextBox1.DateString);
                    InsertHelper.AddElement("Attendance/Field", "Detail", h2.GetRawXml(), true);
                    SmartSchool.Feature.Student.EditAttendance.Insert(new DSRequest(InsertHelper));

                    //log
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, "新增缺曠紀錄", _studentid, updateDesc.ToString(), Text, InsertHelper.GetRawXml());
                    //缺曠變更
                    SmartSchool.StudentRelated.Student.Instance.InvokAttendanceChanged(_studentid);
                }
                else
                {
                    DSXmlHelper updateHelper = new DSXmlHelper("UpdateRequest");
                    updateHelper.AddElement("Attendance");
                    updateHelper.AddElement("Attendance", "Field");
                    updateHelper.AddElement("Attendance/Field", "RefStudentID", _studentid);

                    //updateHelper.AddElement("Attendance/Field", "SchoolYear", _semesterProvider.SchoolYear.ToString());
                    //updateHelper.AddElement("Attendance/Field", "Semester", _semesterProvider.Semester.ToString());
                    updateHelper.AddElement("Attendance/Field", "SchoolYear", comboBoxEx1.Text);
                    updateHelper.AddElement("Attendance/Field", "Semester", comboBoxEx2.Text);

                    updateHelper.AddElement("Attendance/Field", "OccurDate", dateTimeTextBox1.DateString);
                    updateHelper.AddElement("Attendance/Field", "Detail", h2.GetRawXml(), true);
                    updateHelper.AddElement("Attendance", "Condition");
                    updateHelper.AddElement("Attendance/Condition", "ID", _selectedItem.Tag.ToString());
                    SmartSchool.Feature.Student.EditAttendance.Update(new DSRequest(updateHelper));
                    //log
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, "修改缺曠紀錄", _studentid, updateDesc.ToString(), Text, updateHelper.GetRawXml());
                    //缺曠變更
                    SmartSchool.StudentRelated.Student.Instance.InvokAttendanceChanged(_studentid);
                }

                // 加入事件與關閉視窗
                if (DataSaved != null) DataSaved.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show("資料儲存失敗：" + ex.Message);
            }
        }

        private void dateTimeTextBox1_Validated(object sender, EventArgs e)
        {
            IsValid();
        }
    }
}