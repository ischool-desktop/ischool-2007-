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

namespace SmartSchool.StudentRelated.RibbonBars.Discipline
{
    public partial class ClearDemerit : BaseForm
    {
        public event EventHandler DataSaved;

        private BriefStudentData _student;
        private ErrorProvider _errorProvider;

        public ClearDemerit(BriefStudentData student)
        {
            InitializeComponent();
            _student = student;
            _errorProvider = new ErrorProvider();
            this.Text = "�i" + _student.Name + "�j�P�L�@�~";
            dateTimeTextBox1.SetDate(DateTime.Today.ToShortDateString());
        }

        private void ClearDemerit_Load(object sender, EventArgs e)
        {
            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "RefStudentID", _student.ID);
            helper.AddElement("Condition", "Or");
            helper.AddElement("Condition/Or", "MeritFlag", "0");
            helper.AddElement("Condition/Or", "MeritFlag", "2");
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "desc");
            DSResponse dsrsp = SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper));

            helper = dsrsp.GetContent();
            foreach (XmlElement element in helper.GetElements("Discipline"))
            {
                string occurDate = "";
                DateTime od;
                if (DateTime.TryParse(element.SelectSingleNode("OccurDate").InnerText, out od))
                    occurDate = od.ToShortDateString();
                string reason = element.SelectSingleNode("Reason").InnerText;
                string id = element.GetAttribute("ID");

                string a, b, c;
                if (element.SelectSingleNode("Detail/Discipline/Demerit") != null)
                {
                    XmlElement detail = (XmlElement)element.SelectSingleNode("Detail/Discipline/Demerit");
                    a = detail.GetAttribute("A");
                    b = detail.GetAttribute("B");
                    c = detail.GetAttribute("C"); ;
                    string cls = detail.GetAttribute("Cleared");
                    if (cls.Equals("�O")) continue;

                    ListViewItem item = listView.Items.Add(occurDate);
                    item.SubItems.Add(a);
                    item.SubItems.Add(b);
                    item.SubItems.Add(c);
                    item.SubItems.Add(reason);
                    item.Tag = id;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem == null) return;
            if (Control.ModifierKeys == Keys.Control && e.Item.Selected)
                e.Item.Selected = false;
        }

        private void dateTimeTextBox1_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(dateTimeTextBox1, null);
            if (dateTimeTextBox1.Text == "")
                _errorProvider.SetError(dateTimeTextBox1, "�ɶ����i�ť�");
            if (!dateTimeTextBox1.IsValid)
                _errorProvider.SetError(dateTimeTextBox1, "�ɶ��榡����");
        }

        private bool IsValid()
        {
            foreach (Control control in this.Controls)
                if (!string.IsNullOrEmpty(_errorProvider.GetError(control))) return false;
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MsgBox.Show("�Х���ܱ��P�L����");
                return;
            }

            if (!IsValid())
            {
                MsgBox.Show("������ҥ��ѡA�Эץ���ƫ�A���x�s");
                return;
            }

            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            foreach (ListViewItem item in listView.SelectedItems)
            {
                helper.AddElement("Discipline");
                helper.AddElement("Discipline", "Field");

                DSXmlHelper h = new DSXmlHelper("Discipline");
                XmlElement element = h.AddElement("Demerit");
                element.SetAttribute("A", item.SubItems[1].Text);
                element.SetAttribute("B", item.SubItems[2].Text);
                element.SetAttribute("C", item.SubItems[3].Text);
                element.SetAttribute("Cleared", "�O");
                element.SetAttribute("ClearDate", dateTimeTextBox1.DateString);
                element.SetAttribute("ClearReason", textBoxX1.Text);

                helper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
                helper.AddElement("Discipline", "Condition");
                helper.AddElement("Discipline/Condition", "ID", item.Tag.ToString());
            }

            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(helper));

                //�g�٬����P�L log
                StringBuilder clearDesc = new StringBuilder("");
                clearDesc.AppendLine("�ǥͩm�W�G" + SmartSchool.StudentRelated.Student.Instance.Items[_student.ID].Name + " ");

                foreach (ListViewItem item in listView.SelectedItems)
                {
                    clearDesc.AppendLine(item.SubItems[0].Text + " �ƥѬ��u" + item.SubItems[4].Text + "�v���g�٬����w�P�L ");
                }

                clearDesc.AppendLine("�P�L����G" + dateTimeTextBox1.Text + " ");
                clearDesc.AppendLine("�P�L�����G" + textBoxX1.Text);
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "�ק���g����", _student.ID, clearDesc.ToString(), "�P�L�@�~", helper.GetRawXml());
            }
            catch (Exception ex)
            {
                MsgBox.Show("�P�L��~�x�s����:" + ex.Message);
                return;
            }

            if (DataSaved != null)
                DataSaved(this, EventArgs.Empty);
            MsgBox.Show("�x�s����");
            this.Close();
        }
    }
}