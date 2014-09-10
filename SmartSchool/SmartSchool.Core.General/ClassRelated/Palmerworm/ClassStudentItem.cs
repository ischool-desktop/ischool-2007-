using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Feature.Class;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.StudentRelated;
using SmartSchool.AccessControl;

namespace SmartSchool.ClassRelated.Palmerworm
{
    [FeatureCode("Content0160")]
    internal partial class ClassStudentItem : ClassPalmerwormItem
    {
        public ClassStudentItem()
        {
            InitializeComponent();
            Title = "班級學生資訊";
        }

        protected override object OnBackgroundWorkerWorking()
        {
            //return QueryClass.GetClassStudentList(RunningID);
            return Student.Instance.GetClassStudent(RunningID);
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            //DSResponse dsrsp = result as DSResponse;
            //DSXmlHelper helper = dsrsp.GetContent();
            lvwStudent.Items.Clear();

            foreach (BriefStudentData var in result as List<BriefStudentData>)
            {
                ListViewItem item = lvwStudent.Items.Add(var.SeatNo);
                item.SubItems.Add(var.Name);
                item.SubItems.Add(var.StudentNumber);
                item.SubItems.Add(var.PermanentPhone);
                item.SubItems.Add(var.ContactPhone);
                item.Tag = var.ID;
            }
            //foreach (XmlNode node in helper.GetElements("Student"))
            //{
            //    ListViewItem item = lvwStudent.Items.Add(node.SelectSingleNode("SeatNo").InnerText);
            //    item.SubItems.Add(node.SelectSingleNode("Name").InnerText);
            //    item.SubItems.Add(node.SelectSingleNode("StudentNumber").InnerText);
            //    item.SubItems.Add(node.SelectSingleNode("PermanentPhone").InnerText);
            //    item.SubItems.Add(node.SelectSingleNode("ContactPhone").InnerText);
            //    item.Tag = node.Attributes["ID"].Value;
            //}
        }

        public override bool IsValid()
        {
            return true;
        }

        private void lvwStudent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwStudent.SelectedItems.Count < 1)
                return;
            ListViewItem item = lvwStudent.SelectedItems[0];
            string studentid = item.Tag.ToString();
            Student.Instance.ShowDetail(studentid);
        }
    }
}
