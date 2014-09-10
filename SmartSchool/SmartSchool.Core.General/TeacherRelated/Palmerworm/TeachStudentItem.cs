using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feature.Teacher;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.ClassRelated;
using SmartSchool.AccessControl;

namespace SmartSchool.TeacherRelated.Palmerworm
{
    [FeatureCode("Content0180")]
    internal partial class TeachStudentItem : PalmerwormItem
    {
        public TeachStudentItem()
        {
            InitializeComponent();
            Title = "¯Z¯Å";
        }

        protected override object OnBackgroundWorkerWorking()
        {
            //return QueryTeacher.GetStudentListBelong(RunningID);
            //return QueryTeacher.GetTeachClass(RunningID);
            List<ClassInfo> list = new List<ClassInfo>();
            foreach (ClassInfo var in Class.Instance.Items)
            {
                if (var.TeacherID == RunningID)
                    list.Add(var);
            }
            return list;
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            listView2.Items.Clear();
            listView2.Groups.Clear();

            //DSResponse dsrsp = result as DSResponse;
            //DSXmlHelper helper = dsrsp.GetContent();
            Dictionary<string, ListViewGroup> groups = new Dictionary<string, ListViewGroup>();

            foreach (ClassInfo var in result as List<ClassInfo>)
            {
                string id = var.ClassID;
                string classname = var.ClassName;
                string studentcount = var.StudentCount;
                string gradeYear = var.GradeYear;

                ListViewGroup group;
                string gKey = gradeYear + "¦~¯Å";
                if (!groups.ContainsKey(gKey))
                {
                    group = new ListViewGroup(gKey, gKey);
                    groups.Add(gKey, group);
                    listView2.Groups.Add(group);
                }
                else
                    group = groups[gKey];

                ListViewItem item = listView2.Items.Add(gradeYear);

                item.SubItems.Add(classname);
                item.SubItems.Add(studentcount);
                item.Group = group;
                item.Tag = id;
            }
            //foreach (XmlElement element in helper.GetElements("Class"))
            //{
            //    string id = element.GetAttribute("ID");
            //    string classname = element.SelectSingleNode("ClassName").InnerText;
            //    string studentcount = element.SelectSingleNode("StudentCount").InnerText;
            //    string gradeYear = element.SelectSingleNode("GradeYear").InnerText;

            //    ListViewGroup group;
            //    string gKey = gradeYear + " ¦~¯Å";
            //    if (!groups.ContainsKey(gKey))
            //    {
            //        group = new ListViewGroup(gKey, gKey);
            //        groups.Add(gKey, group);
            //        listView2.Groups.Add(group);
            //    }
            //    else
            //        group = groups[gKey];

            //    ListViewItem item = listView2.Items.Add(gradeYear);

            //    //ListViewItem i = new ListViewItem(group);
            //    //i.SubItems.Add(new ListViewItem.ListViewSubItem(i, gradeYear));
            //    //i.SubItems.Add(new ListViewItem.ListViewSubItem(i, classname));
            //    //i.SubItems.Add(new ListViewItem.ListViewSubItem(i, studentcount));
            //    //i.Tag = id;
            //    //listView2.Items.Add(i);

            //    item.SubItems.Add(classname);
            //    item.SubItems.Add(studentcount);
            //    item.Group = group;
            //    item.Tag = id;
            //    //string classname = node.SelectSingleNode("ClassName").InnerText;
            //    //ListViewGroup group;
            //    //if (!groups.ContainsKey(classname))
            //    //{
            //    //    group = new ListViewGroup(classname, classname);
            //    //    groups.Add(classname, group);
            //    //    listView.Groups.Add(group);
            //    //}
            //    //else
            //    //    group = groups[classname];
            //    //ListViewItem item = listView.Items.Add(node.SelectSingleNode("SeatNumber").InnerText);
            //    //item.SubItems.Add(node.SelectSingleNode("Name").InnerText);
            //    //item.SubItems.Add(node.SelectSingleNode("StudentNumber").InnerText);
            //    //item.SubItems.Add(node.SelectSingleNode("PermanentPhone").InnerText);
            //    //item.SubItems.Add(node.SelectSingleNode("ContactPhone").InnerText);                
            //    //item.Group = group;
            //}
        }

        public override void Save()
        {
            
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView2.FocusedItem == null) return;
            if (listView2.FocusedItem.Tag == null) return;
            string id = listView2.FocusedItem.Tag.ToString();
            string caption = listView2.FocusedItem.SubItems[1].Text;
            PopupClassForm(id, caption);
        }

        private void PopupClassForm(string classid,string caption)
        {
            Class.Instance.PopupClassForm(classid,caption);
        }
    }
}
