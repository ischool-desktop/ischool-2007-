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
            this.Text = "【" + _student.Name + "】銷過作業";
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
                _errorProvider.SetError(dateTimeTextBox1, "時間不可空白");
            if (!dateTimeTextBox1.IsValid)
                _errorProvider.SetError(dateTimeTextBox1, "時間格式不符");
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
            //    MsgBox.Show("請先選擇欲銷過紀錄");
            //    return;
            //}

            if (!IsValid())
            {
                MsgBox.Show("資料驗證失敗，請修正資料後再行儲存");
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
            element.SetAttribute("Cleared", "是");
            element.SetAttribute("ClearDate", dateTimeTextBox1.DateString);
            element.SetAttribute("ClearReason", textBoxX1.Text);

            helper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
            helper.AddElement("Discipline", "Condition");
            helper.AddElement("Discipline/Condition", "ID", _item.Tag.ToString());


            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(helper));

                //懲戒紀錄銷過 log
                StringBuilder clearDesc = new StringBuilder("");
                clearDesc.AppendLine("學生姓名：" + Student.Instance.Items[_student.ID].Name + " ");
                clearDesc.AppendLine(_item.SubItems[0].Text + "學年度 " + _item.SubItems[1].Text + "學期" + " 事由為「" + _item.SubItems[7].Text + "」的懲戒紀錄已銷過 ");
                clearDesc.AppendLine("銷過日期：" + dateTimeTextBox1.Text + " ");
                clearDesc.AppendLine("銷過說明：" + textBoxX1.Text);
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "修改獎懲紀錄", _student.ID, clearDesc.ToString(), "銷過作業", helper.GetRawXml());
            }
            catch (Exception ex)
            {
                MsgBox.Show("銷過作業儲存失敗:" + ex.Message);
                return;
            }

            Student.Instance.InvokDisciplineChanged(_student.ID);
            if (DataSaved != null)
                DataSaved(this, EventArgs.Empty);
            MsgBox.Show("儲存完畢");
            this.Close();
        }
    }
}