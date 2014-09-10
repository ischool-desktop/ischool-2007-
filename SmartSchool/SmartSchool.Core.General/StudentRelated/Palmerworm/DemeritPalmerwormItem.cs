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
            this.Title = "懲戒資料";

            //取得此 Class 定義的 FeatureCode。
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
                    d.Cleared = cls.Equals("是");
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
                item.SubItems.Add(d.IsAsshole ? "是" : "否");
                item.SubItems.Add(d.Reason);
                item.SubItems.Add(d.Cleared ? "已銷" : "未銷");
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
                MsgBox.Show("請先選擇一筆您要修改的資料");
                return;
            }
            if (listView.SelectedItems.Count > 1)
            {
                MsgBox.Show("選擇資料筆數過多，一次只能修改一筆資料");
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
                MsgBox.Show("請先選擇欲刪除資料");
                return;
            }
            if (MsgBox.Show("確定將刪除所選擇之懲戒紀錄?", "確認", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

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

                //刪除懲戒紀錄 log
                StringBuilder deleteDesc = new StringBuilder("");
                deleteDesc.AppendLine("學生姓名：" + Student.Instance.Items[RunningID].Name + " ");
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    deleteDesc.AppendLine("刪除 " + item.SubItems[0].Text + " 事由為「" + item.SubItems[7].Text + "」的懲戒紀錄");
                }
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "刪除獎懲紀錄", RunningID, deleteDesc.ToString(), Title, helper.GetRawXml());
            }
            catch (Exception ex)
            {
                MsgBox.Show("刪除懲戒資料失敗:" + ex.Message);
                return;
            }
            Student.Instance.InvokDisciplineChanged(RunningID);
            LoadContent(RunningID);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView.FocusedItem;
            if (item == null) return;

            if (item.SubItems[8].Text == "未銷")
            {
                ClearDemerit cd = new ClearDemerit(_student, item);
                cd.DataSaved += new EventHandler(cd_DataSaved);
                cd.ShowDialog();
            }
            else
            {
                DialogResult result = MsgBox.Show("您要將此筆銷過紀錄恢復成未銷過狀態嗎?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                DSXmlHelper h = new DSXmlHelper("Discipline");
                XmlElement element = h.AddElement("Demerit");
                element.SetAttribute("A", item.SubItems[3].Text);
                element.SetAttribute("B", item.SubItems[4].Text);
                element.SetAttribute("C", item.SubItems[5].Text);
                element.SetAttribute("Cleared", "否");

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
                    MsgBox.Show("取消銷過作業失敗!", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MsgBox.Show("取消銷過作業完成!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //取消銷過 Log
                StringBuilder unclearDesc = new StringBuilder("");
                unclearDesc.AppendLine("學生姓名：" + Student.Instance.Items[RunningID].Name + " ");
                unclearDesc.AppendLine(item.SubItems[0].Text + "學年度 " + item.SubItems[1].Text + "學期" + " 事由為「" + item.SubItems[6].Text + "」的懲戒紀錄取消銷過 ");
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "修改獎懲紀錄", RunningID, unclearDesc.ToString(), "銷過作業", helper.GetRawXml());
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
            if (item.SubItems[8].Text == "未銷")
                btnClear.Text = "銷過";
            else
                btnClear.Text = "取消銷過";

            if (item.SubItems[6].Text == "是")
            {
                btnClear.Enabled = false;
                //btnClear.Text = "無法使用";
            }
            //}
        }
    }
}
