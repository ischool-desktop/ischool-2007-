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
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0070")]
    internal partial class MeritPalmerwormItem : PalmerwormItem
    {
        private BriefStudentData _student;

        private FeatureAce _permission;

        public MeritPalmerwormItem()
        {
            InitializeComponent();
            this.Title = "獎勵資料";

            //取得此 Class 定義的 FeatureCode。
            FeatureCodeAttribute code = Attribute.GetCustomAttribute(this.GetType(), typeof(FeatureCodeAttribute)) as FeatureCodeAttribute;

            _permission = CurrentUser.Acl[code.FeatureCode];

            btnInsert.Visible = _permission.Editable;
            btnUpdate.Visible = _permission.Editable;
            btnDelete.Visible = _permission.Editable;
        }

        public override void LoadContent(string id)
        {
            base.LoadContent(id);

            //foreach (BriefStudentData student in Student.Instance.SelectionStudents)
            //{
            //    if (student.ID != id) continue;
            //    _student = student;
            //}

            _student = Student.Instance.Items[id];
        }

        protected override object OnBackgroundWorkerWorking()
        {
            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "RefStudentID", RunningID);
            helper.AddElement("Condition", "MeritFlag", "1");
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

                if (element.SelectSingleNode("Detail/Discipline/Merit") != null)
                {
                    XmlElement detail = (XmlElement)element.SelectSingleNode("Detail/Discipline/Merit");
                    d.A = detail.GetAttribute("A");
                    d.B = detail.GetAttribute("B");
                    d.C = detail.GetAttribute("C");
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
                item.SubItems.Add(d.Reason);              
                item.Tag = d;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            List<BriefStudentData> students = new List<BriefStudentData>();
            students.Add(_student);
            InsertEditor editor = new InsertEditor(students, true);
            editor.DataSaved += new EventHandler(editor_DataSaved);
            editor.ShowDialog();
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

            InsertEditor editor = new InsertEditor(students, true, d);
            editor.DataSaved += new EventHandler(editor_DataSaved);
            editor.ShowDialog();
        }

        void editor_DataSaved(object sender, EventArgs e)
        {
            LoadContent(RunningID);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MsgBox.Show("請先選擇欲刪除資料");
                return;
            }
            if (MsgBox.Show("確定將刪除所選擇之獎勵紀錄?", "確認", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

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

                //刪除獎勵紀錄 log
                StringBuilder deleteDesc = new StringBuilder("");
                deleteDesc.AppendLine("學生姓名：" + Student.Instance.Items[RunningID].Name+" ");
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    deleteDesc.AppendLine("刪除 " + item.SubItems[0].Text + " 事由為「" + item.SubItems[4].Text + "」的獎勵紀錄");
                }
                CurrentUser.Instance.AppLog.Write(EntityType.Student, "刪除獎懲紀錄", RunningID, deleteDesc.ToString(), Title, helper.GetRawXml());
            }
            catch (Exception ex)
            {
                MsgBox.Show("刪除獎勵資料失敗:" + ex.Message);
                return;
            }
            Student.Instance.InvokDisciplineChanged(RunningID);
            LoadContent(RunningID);
        }
    }
}
