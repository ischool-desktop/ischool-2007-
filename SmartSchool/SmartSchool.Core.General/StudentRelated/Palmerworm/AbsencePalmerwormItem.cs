using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.StudentRelated.Palmerworm.Attendance;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0060")]
    partial class AbsencePalmerwormItem : PalmerwormItem
    {
        private Dictionary<string, AbsenceInfo> _absenceList;
        private int _initFinished;
        private DSResponse _record;
        private int _startIndex;

        private FeatureAce _permission;

        public AbsencePalmerwormItem()
        {
            InitializeComponent();
            Title = "���m����";
            Initialize();
        }

        private void Initialize()
        {
            _startIndex = 4;
            _initFinished = 0;
            _absenceList = new Dictionary<string, AbsenceInfo>();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            BackgroundWorker absenceWorker = new BackgroundWorker();
            absenceWorker.DoWork += new DoWorkEventHandler(absenceWorker_DoWork);
            absenceWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(absenceWorker_RunWorkerCompleted);
            absenceWorker.RunWorkerAsync();

            //���o�� Class �w�q�� FeatureCode�C
            FeatureCodeAttribute code = Attribute.GetCustomAttribute(this.GetType(), typeof(FeatureCodeAttribute)) as FeatureCodeAttribute;

            _permission = CurrentUser.Acl[code.FeatureCode];

            btnAdd.Visible = _permission.Editable;
            btnUpdate.Visible = _permission.Editable;
            btnDelete.Visible = _permission.Editable;
        }

        void absenceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DSResponse dsrsp = e.Result as DSResponse;
            DSXmlHelper helper = dsrsp.GetContent();
            foreach (XmlElement element in helper.GetElements("Absence"))
            {
                AbsenceInfo info = new AbsenceInfo(element);
                _absenceList.Add(info.Hotkey.ToUpper(), info);
            }

            _initFinished++;

            if (InitFinished)
                BindData();
        }

        void absenceWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SmartSchool.Feature.Basic.Config.GetAbsenceList();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DSResponse dsrsp = e.Result as DSResponse;
            DSXmlHelper helper = dsrsp.GetContent();
            PeriodCollection collection = new PeriodCollection();
            foreach (XmlElement element in helper.GetElements("Period"))
            {
                PeriodInfo info = new PeriodInfo(element);
                collection.Items.Add(info);
            }
            listView.Columns.Add("colSchoolYear", "�Ǧ~��");
            listView.Columns.Add("colSemester", "�Ǵ�");
            listView.Columns.Add("colDate", "���m���");
            listView.Columns.Add("colDayOfWeek", "�P��");

            foreach (PeriodInfo info in collection.GetSortedList())
            {
                ColumnHeader column = listView.Columns.Add(info.Name, info.Name);
                column.Tag = info;
            }

            _initFinished++;

            if (InitFinished)
                BindData();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SmartSchool.Feature.Basic.Config.GetPeriodList();
        }

        protected override object OnBackgroundWorkerWorking()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "RefStudentID", RunningID);
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "desc");
            return SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper));
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            _record = result as DSResponse;
            _initFinished++;
            if (InitFinished)
                BindData();
        }

        private void BindData()
        {
            listView.Items.Clear();
            DSXmlHelper helper = _record.GetContent();
            foreach (XmlElement element in helper.GetElements("Attendance"))
            {
                // �o�̭n���@�ǨƱ�  �Ҧp���F�a��i�h
                string occurDate = element.SelectSingleNode("OccurDate").InnerText;
                string schoolYear = element.SelectSingleNode("SchoolYear").InnerText;
                string semester = element.SelectSingleNode("Semester").InnerText;
                string id = element.GetAttribute("ID");
                XmlNode dNode = element.SelectSingleNode("Detail").FirstChild;

                DateTime date;
                if (DateTime.TryParse(occurDate, out date))
                    occurDate = date.ToShortDateString();

                ListViewItem item = listView.Items.Add(schoolYear);
                item.Tag = id;
                item.SubItems.Add(semester);
                item.SubItems.Add(occurDate);
                item.SubItems.Add(GetDayOfWeekInChinese(occurDate));

                for (int i = _startIndex; i < listView.Columns.Count; i++)
                {
                    item.SubItems.Add("");
                }

                for (int i = _startIndex; i < listView.Columns.Count; i++)
                {
                    ColumnHeader column = listView.Columns[i];
                    PeriodInfo info = column.Tag as PeriodInfo;

                    foreach (XmlNode node in dNode.SelectNodes("Period"))
                    {
                        if (info == null) continue;
                        if (node.InnerText != info.Name) continue;
                        if (node.SelectSingleNode("@AbsenceType") == null) continue;
                        System.Windows.Forms.ListViewItem.ListViewSubItem subitem = item.SubItems[i];

                        foreach (AbsenceInfo ai in _absenceList.Values)
                        {
                            if (ai.Name != node.SelectSingleNode("@AbsenceType").InnerText) continue;
                            subitem.Tag = ai.Clone();
                            subitem.Text = ai.Abbreviation;
                            break;
                        }
                    }
                }
            }
        }

        private bool InitFinished
        {
            get { return _initFinished >= 3; }
        }

        private string GetDayOfWeekInChinese(string occurDate)
        {
            DateTime date;
            if (DateTime.TryParse(occurDate, out date))
                return GetDayOfWeekInChinese(date.DayOfWeek);
            return "";
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0) return;
            string msg = "�z�T�w�n�R���ҿ�������m������?";
            if (MsgBox.Show(msg, "�T�{", MessageBoxButtons.YesNo) == DialogResult.No) return;

            DSXmlHelper deleteHelper = new DSXmlHelper("DeleteRequest");
            deleteHelper.AddElement("Attendance");

            foreach (ListViewItem item in listView.SelectedItems)
            {
                deleteHelper.AddElement("Attendance", "ID", item.Tag.ToString());
            }
            SmartSchool.Feature.Student.EditAttendance.Delete(new DSRequest(deleteHelper));
            SmartSchool.StudentRelated.Student.Instance.InvokAttendanceChanged(RunningID);
            LoadContent(RunningID);

            //�R�����m���� log
            StringBuilder deleteDesc = new StringBuilder("");

            deleteDesc.AppendLine("�ǥͩm�W�G" + Student.Instance.Items[RunningID].Name);
            foreach (ListViewItem item in listView.SelectedItems)
            {
                deleteDesc.AppendLine("�R�� " + item.SubItems[0].Text + " ���m����");
            }
            CurrentUser.Instance.AppLog.Write(EntityType.Student, "�R�����m����", RunningID, deleteDesc.ToString(), Title, deleteHelper.GetRawXml());
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem == null) return;

            if (Control.ModifierKeys == Keys.Control && e.Item.Selected)
            {
                e.Item.Selected = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AttendanceEditor editor = new AttendanceEditor(listView, null, RunningID);
            editor.DataSaved += new EventHandler(editor_DataSaved);
            editor.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count != 1) return;
            AttendanceEditor editor = new AttendanceEditor(listView, listView.SelectedItems[0], RunningID);
            editor.DataSaved += new EventHandler(editor_DataSaved);
            editor.ShowDialog();
        }

        void editor_DataSaved(object sender, EventArgs e)
        {
            LoadContent(RunningID);
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (Control.ModifierKeys == Keys.Shift)
            //{
            //    try
            //    {
            //        XmlElement attendance = _record.GetContent().GetElement("Attendance[@ID='" + listView.FocusedItem.Tag.ToString() + "']");
            //        XmlBox.ShowXml(attendance);
            //    }
            //    catch { }
            //}
        }
    }
}
