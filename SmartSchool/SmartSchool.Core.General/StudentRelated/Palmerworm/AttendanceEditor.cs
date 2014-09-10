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

            #region ��l�ƾǦ~�׾Ǵ�ComboBox
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
            Text = "�޲z�ǥͯ��m�����i�s�W�Ҧ��j";
            if (_selectedItem != null)
            {
                dateTimeTextBox1.SetDate(_selectedItem.SubItems[2].Text);
                dateTimeTextBox1.ReadOnly = true;
                Text = "�޲z�ǥͯ��m�����i�ק�Ҧ��j";

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
            // ���Ҥ���O�_����
            if (!this.dateTimeTextBox1.IsValid)
            {
                _errorProvider.SetError(dateTimeTextBox1, "����榡���~");
                return false;
            }

            // ���Ҥ���O�_�w������,�Y�O�s�W�~�ˬd , �ק�h���ݭn
            if (_selectedItem == null)
            {
                foreach (ListViewItem item in _listView.Items)
                {
                    DateTime date;
                    if (!DateTime.TryParse(item.SubItems[2].Text, out date)) continue;
                    if (dateTimeTextBox1.GetDate().CompareTo(date) != 0) continue;

                    _errorProvider.SetError(dateTimeTextBox1, "������w�������s�b�A�Ч�έק�Ҧ�");
                    return false;
                }
            }

            // ���ҾǦ~�׾Ǵ�
            int tryValue;
            if (!int.TryParse(comboBoxEx1.Text, out tryValue))
            {
                _errorProvider.SetError(comboBoxEx1, "�Ǧ~�ץ����O�Ʀr");
                return false;
            }
            if (!int.TryParse(comboBoxEx2.Text, out tryValue))
            {
                _errorProvider.SetError(comboBoxEx2, "�Ǵ��榡���~");
                return false;
            }
            else if(tryValue > 2 || tryValue < 1)
            {
                _errorProvider.SetError(comboBoxEx2, "�Ǵ��榡���~");
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
                MsgBox.Show("������Ҧ��~�A�Эץ���A���x�s");
                return;
            }
            try
            {
                StringBuilder updateDesc = new StringBuilder("");
                updateDesc.Append("�ǥͩm�W�G").Append(Student.Instance.Items[_studentid].Name).Append(" \n");
                updateDesc.Append("����G").Append(dateTimeTextBox1.Text).Append(" \n");

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
                    //�u�v
                    updateDesc.AppendLine("�`���u" + var + "�v�ѡu" + _dataValueManager.GetOldValue(var) + "�v�ܧ󬰡u" + _changed[var] + "�v");
                }

                if (_selectedItem == null && !hasContent) //�Y�O�s�W���L�b������
                {
                    MsgBox.Show("�Х��]�w���m����");
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
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, "�s�W���m����", _studentid, updateDesc.ToString(), Text, InsertHelper.GetRawXml());
                    //���m�ܧ�
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
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, "�ק���m����", _studentid, updateDesc.ToString(), Text, updateHelper.GetRawXml());
                    //���m�ܧ�
                    SmartSchool.StudentRelated.Student.Instance.InvokAttendanceChanged(_studentid);
                }

                // �[�J�ƥ�P��������
                if (DataSaved != null) DataSaved.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show("����x�s���ѡG" + ex.Message);
            }
        }

        private void dateTimeTextBox1_Validated(object sender, EventArgs e)
        {
            IsValid();
        }
    }
}