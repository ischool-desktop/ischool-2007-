using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using IntelliSchool.DSA30.Util;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0080")]
    internal partial class DemeritPalmerwormItem : PalmerwormItem
    {
        private BriefStudentData _student;

        private FeatureAce _permission;

        public DemeritPalmerwormItem()
        {
            InitializeComponent();
            this.Title = "�g�ٸ��";

            //���o�� Class �w�q�� FeatureCode�C
            FeatureCodeAttribute code = Attribute.GetCustomAttribute(this.GetType(), typeof(FeatureCodeAttribute)) as FeatureCodeAttribute;

            _permission = CurrentUser.Acl[code.FeatureCode];

            btnInsert.Visible = _permission.Editable;
            btnUpdate.Visible = _permission.Editable;
            btnDelete.Visible = _permission.Editable;
            btnClear.Visible = _permission.Editable;
        }

        public override void LoadContent(string id)
        {
            base.LoadContent(id);
            _student = Student.Instance.Items[id];
        }
        protected override object OnBackgroundWorkerWorking()
        {
            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "RefStudentID", RunningID);
            helper.AddElement("Condition", "Or");
            helper.AddElement("Condition/Or", "MeritFlag", "0");
            helper.AddElement("Condition/Or", "MeritFlag", "2");
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "desc");
            return SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper));
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            DSResponse dsrsp = result as DSResponse;
            DSXmlHelper helper = dsrsp.GetContent();

            listView.Items.Clear();
            foreach (XmlElement element in helper.GetElements("Discipline"))
            {
                Discipline d = new Discipline();
                DateTime od;
                if (DateTime.TryParse(element.SelectSingleNode("OccurDate").InnerText, out od))
                    d.OccurDate = od.ToShortDateString();
                d.Reason = element.SelectSingleNode("Reason").InnerText;
                d.Id = element.GetAttribute("ID");
                d.IsAsshole = element.SelectSingleNode("MeritFlag").InnerText == "2";
                if (element.SelectSingleNode("Detail/Discipline/Demerit") != null)
                {
                    XmlElement detail = (XmlElement)element.SelectSingleNode("Detail/Discipline/Demerit");
                    d.A = detail.GetAttribute("A");
                    d.B = detail.GetAttribute("B");
                    d.C = detail.GetAttribute("C");
                    d.ClearDate = detail.GetAttribute("ClearDate");
                    d.ClearReason = detail.GetAttribute("ClearReason");
                    string cls = detail.GetAttribute("Cleared");
                    d.Cleared = cls.Equals("�O");
                }
                d.GradeYear = element.SelectSingleNode("GradeYear").InnerText;
                d.SchoolYear = element.SelectSingleNode("SchoolYear").InnerText;
                d.Semester = element.SelectSingleNode("Semester").InnerText;


                ListViewItem item = listView.Items.Add(d.SchoolYear);
                item.SubItems.Add(d.Semester);
                item.SubItems.Add(d.OccurDate);
                item.SubItems.Add(d.A);
                item.SubItems.Add(d.B);
                item.SubItems.Add(d.C);
                item.SubItems.Add(d.IsAsshole ? "�O" : "�_");
                item.SubItems.Add(d.Reason);
                item.SubItems.Add(d.Cleared ? "�w�P" : "���P");
                item.SubItems.Add(d.ClearDate);
                item.SubItems.Add(d.ClearReason);
                item.Tag = d;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            List<BriefStudentData> students = new List<BriefStudentData>();
            students.Add(_student);
            DemeritEditor editor = new DemeritEditor(students, false);
            editor.DataSaved += new EventHandler(editor_DataSaved);
            editor.ShowDialog();
        }

        void editor_DataSaved(object sender, EventArgs e)
        {
            LoadContent(RunningID);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MsgBox.Show("�Х���ܤ@���z�n�ק諸���");
                return;
            }
            if (listView.SelectedItems.Count > 1)
            {
                MsgBox.Show("��ܸ�Ƶ��ƹL�h�A�@���u��ק�@�����");
                return;
            }

            List<BriefStudentData> students = new List<BriefStudentData>();
            students.Add(_student);

            ListViewItem item = listView.FocusedItem;
            Discipline d = item.Tag as Discipline;

            DemeritEditor editor = new DemeritEditor(students, false, d);
            editor.DataSaved += new EventHandler(editor_DataSaved);
            editor.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MsgBox.Show("�Х���ܱ��R�����");
                return;
            }
            if (MsgBox.Show("�T�w�N�R���ҿ�ܤ��g�٬���?", "�T�{", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            DSXmlHelper helper = new DSXmlHelper("DeleteRequest");
            helper.AddElement("Discipline");
            foreach (ListViewItem item in listView.SelectedItems)
            {
                Discipline d = item.Tag as Discipline;
                helper.AddElement("Discipline", "ID", d.Id);
            }
            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Delete(new DSRequest(helper));

                //�R���g�٬��� log
                StringBuilder deleteDesc = new StringBuilder("");
                deleteDesc.AppendLine("�ǥͩm�W�G" + Student.Instance.Items[RunningID].Name + " ");
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    deleteDesc.AppendLine("�R�� " + item.SubItems[0].Text + " �ƥѬ��u" + item.SubItems[7].Text + "�v���g�٬���");
                }
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "�R�����g����", RunningID, deleteDesc.ToString(), Title, helper.GetRawXml());
            }
            catch (Exception ex)
            {
                MsgBox.Show("�R���g�ٸ�ƥ���:" + ex.Message);
                return;
            }
            Student.Instance.InvokDisciplineChanged(RunningID);
            LoadContent(RunningID);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView.FocusedItem;
            if (item == null) return;

            if (item.SubItems[8].Text == "���P")
            {
                ClearDemerit cd = new ClearDemerit(_student, item);
                cd.DataSaved += new EventHandler(cd_DataSaved);
                cd.ShowDialog();
            }
            else
            {
                DialogResult result = MsgBox.Show("�z�n�N�����P�L������_�����P�L���A��?", "�T�w", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                DSXmlHelper h = new DSXmlHelper("Discipline");
                XmlElement element = h.AddElement("Demerit");
                element.SetAttribute("A", item.SubItems[3].Text);
                element.SetAttribute("B", item.SubItems[4].Text);
                element.SetAttribute("C", item.SubItems[5].Text);
                element.SetAttribute("Cleared", "�_");

                DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
                helper.AddElement("Discipline");
                helper.AddElement("Discipline", "Field");
                helper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
                helper.AddElement("Discipline", "Condition");
                helper.AddElement("Discipline/Condition", "ID", item.Tag.ToString());
                try
                {
                    SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(helper));
                    LoadContent(RunningID);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("�����P�L�@�~����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MsgBox.Show("�����P�L�@�~����!", "���\", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //�����P�L Log
                StringBuilder unclearDesc = new StringBuilder("");
                unclearDesc.AppendLine("�ǥͩm�W�G" + Student.Instance.Items[RunningID].Name + " ");
                unclearDesc.AppendLine(item.SubItems[0].Text + "�Ǧ~�� " + item.SubItems[1].Text + "�Ǵ�" + " �ƥѬ��u" + item.SubItems[6].Text + "�v���g�٬��������P�L ");
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "�ק���g����", RunningID, unclearDesc.ToString(), "�P�L�@�~", helper.GetRawXml());
            }
        }

        void cd_DataSaved(object sender, EventArgs e)
        {
            LoadContent(RunningID);
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count <= 0)
                return;
            //foreach (ListViewItem item in listView.Items)
            //{
            //    foreach (System.Windows.Forms.ListViewItem.ListViewSubItem subitem in item.SubItems)
            //        subitem.BackColor = Color.White;
            //    item.BackColor = Color.White;
            //}
            //if (listView.FocusedItem != null)
            //{
            //ListViewItem item = listView.FocusedItem;
            ListViewItem item = listView.SelectedItems[0];
            //foreach (System.Windows.Forms.ListViewItem.ListViewSubItem subitem in item.SubItems)
            //    subitem.BackColor = Color.LightBlue;
            //item.BackColor = Color.LightBlue;

            btnClear.Enabled = true;
            if (item.SubItems[8].Text == "���P")
                btnClear.Text = "�P�L";
            else
                btnClear.Text = "�����P�L";

            if (item.SubItems[6].Text == "�O")
            {
                btnClear.Enabled = false;
                //btnClear.Text = "�L�k�ϥ�";
            }
            //}
        }
    }
}
