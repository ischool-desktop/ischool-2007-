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

namespace SmartSchool.StudentRelated.Palmerworm
{
    public partial class ClearDemerit : BaseForm
    {
        public event EventHandler DataSaved;

        private BriefStudentData _student;
        private ErrorProvider _errorProvider;
        private ListViewItem _item;

        public ClearDemerit(BriefStudentData student, ListViewItem item)
        {
            InitializeComponent();
            _student = student;
            _item = item;
            _errorProvider = new ErrorProvider();
            this.Text = "�i" + _student.Name + "�j�P�L�@�~";
            dateTimeTextBox1.SetDate(DateTime.Today.ToShortDateString());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
            //if (listView.SelectedItems.Count == 0)
            //{
            //    MsgBox.Show("�Х���ܱ��P�L����");
            //    return;
            //}

            if (!IsValid())
            {
                MsgBox.Show("������ҥ��ѡA�Эץ���ƫ�A���x�s");
                return;
            }

            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");

            helper.AddElement("Discipline");

            helper.AddElement("Discipline", "Field");

            DSXmlHelper h = new DSXmlHelper("Discipline");
            XmlElement element = h.AddElement("Demerit");
            element.SetAttribute("A", _item.SubItems[3].Text);
            element.SetAttribute("B", _item.SubItems[4].Text);
            element.SetAttribute("C", _item.SubItems[5].Text);
            element.SetAttribute("Cleared", "�O");
            element.SetAttribute("ClearDate", dateTimeTextBox1.DateString);
            element.SetAttribute("ClearReason", textBoxX1.Text);

            helper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
            helper.AddElement("Discipline", "Condition");
            helper.AddElement("Discipline/Condition", "ID", _item.Tag.ToString());


            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(helper));

                //�g�٬����P�L log
                StringBuilder clearDesc = new StringBuilder("");
                clearDesc.AppendLine("�ǥͩm�W�G" + Student.Instance.Items[_student.ID].Name + " ");
                clearDesc.AppendLine(_item.SubItems[0].Text + "�Ǧ~�� " + _item.SubItems[1].Text + "�Ǵ�" + " �ƥѬ��u" + _item.SubItems[7].Text + "�v���g�٬����w�P�L ");
                clearDesc.AppendLine("�P�L����G" + dateTimeTextBox1.Text + " ");
                clearDesc.AppendLine("�P�L�����G" + textBoxX1.Text);
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "�ק���g����", _student.ID, clearDesc.ToString(), "�P�L�@�~", helper.GetRawXml());
            }
            catch (Exception ex)
            {
                MsgBox.Show("�P�L�@�~�x�s����:" + ex.Message);
                return;
            }

            Student.Instance.InvokDisciplineChanged(_student.ID);
            if (DataSaved != null)
                DataSaved(this, EventArgs.Empty);
            MsgBox.Show("�x�s����");
            this.Close();
        }
    }
}