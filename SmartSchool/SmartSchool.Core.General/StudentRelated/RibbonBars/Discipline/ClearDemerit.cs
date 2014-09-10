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
            this.Text = "【" + _student.Name + "】銷過作業";
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
                    if (cls.Equals("是")) continue;

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
            if (listView.SelectedItems.Count == 0)
            {
                MsgBox.Show("請先選擇欲銷過紀錄");
                return;
            }

            if (!IsValid())
            {
                MsgBox.Show("資料驗證失敗，請修正資料後再行儲存");
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
                element.SetAttribute("Cleared", "是");
                element.SetAttribute("ClearDate", dateTimeTextBox1.DateString);
                element.SetAttribute("ClearReason", textBoxX1.Text);

                helper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
                helper.AddElement("Discipline", "Condition");
                helper.AddElement("Discipline/Condition", "ID", item.Tag.ToString());
            }

            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(helper));

                //懲戒紀錄銷過 log
                StringBuilder clearDesc = new StringBuilder("");
                clearDesc.AppendLine("學生姓名：" + SmartSchool.StudentRelated.Student.Instance.Items[_student.ID].Name + " ");

                foreach (ListViewItem item in listView.SelectedItems)
                {
                    clearDesc.AppendLine(item.SubItems[0].Text + " 事由為「" + item.SubItems[4].Text + "」的懲戒紀錄已銷過 ");
                }

                clearDesc.AppendLine("銷過日期：" + dateTimeTextBox1.Text + " ");
                clearDesc.AppendLine("銷過說明：" + textBoxX1.Text);
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "修改獎懲紀錄", _student.ID, clearDesc.ToString(), "銷過作業", helper.GetRawXml());
            }
            catch (Exception ex)
            {
                MsgBox.Show("銷過件業儲存失敗:" + ex.Message);
                return;
            }

            if (DataSaved != null)
                DataSaved(this, EventArgs.Empty);
            MsgBox.Show("儲存完畢");
            this.Close();
        }
    }
}