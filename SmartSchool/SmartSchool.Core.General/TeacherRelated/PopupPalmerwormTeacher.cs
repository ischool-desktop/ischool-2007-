using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.TeacherRelated
{
    public partial class PopupPalmerwormTeacher : SmartSchool.Common.BaseForm
    {
        static private Dictionary<string, PopupPalmerwormTeacher> _items = new Dictionary<string, PopupPalmerwormTeacher>();

        static public void ShowPopupPalmerwormTeacher(string id)
        {
            if (_items.ContainsKey(id))
            {
                if (_items[id].WindowState == FormWindowState.Minimized)
                    _items[id].WindowState = FormWindowState.Normal;
                _items[id].Activate();
            }
            else
            {
                PopupPalmerwormTeacher popup = new PopupPalmerwormTeacher();
                _items.Add(id, popup);
                popup.ID = id;
                popup.Show();
            }
        }

        private PopupPalmerwormTeacher()
        {
            InitializeComponent();
            TeacherRelated.Teacher.Instance.TeacherDeleted += new EventHandler<TeacherDeletedEventArgs>(Instance_TeacherDeleted);
        }

        void Instance_TeacherDeleted(object sender, TeacherDeletedEventArgs e)
        {
            if (e.ID == this.ID)
                this.Close();
        }

        private string ID
        {
            get { return palmerwormTeacher1.TeacherInfo.ID; }
            set
            {
                //Dictionary<string, BriefTeacherData> teacherInfo = new Dictionary<string, BriefTeacherData>();
                //DSResponse resp = Feature.Teacher.QueryTeacher.GetTeacherListWithSupervisedByClassInfo(value);
                //XmlElement[] elements = resp.GetContent().GetElements("Teacher");
                //foreach (XmlElement ele in elements)
                //{
                //    BriefTeacherData newData;
                //    string id = ele.SelectSingleNode("@ID").InnerText;
                //    if (teacherInfo.ContainsKey(id))
                //    {
                //        newData = teacherInfo[id];
                //    }
                //    else
                //    {
                //        newData = new BriefTeacherData(ele);
                //        teacherInfo.Add(id, newData);
                //    }

                //    //if (ele.SelectSingleNode("SupervisedByClassID").InnerText != "")
                //    //{
                //    //    newData.SupervisedByClassInfo.Add(new SupervisedByClassInfo(ele));
                //    //}
                //}
                //if (teacherInfo.Count > 0)
                //{
                //}
                BriefTeacherData teacherInfo;
                if ( TeacherRelated.Teacher.Instance.Items.ContainsKey(value) )
                {
                    teacherInfo = TeacherRelated.Teacher.Instance.Items[value];
                }
                else
                {
                    DSResponse resp = Feature.Teacher.QueryTeacher.GetTeacherDetailTest(value);
                    teacherInfo = new BriefTeacherData(resp.GetContent().GetElement("Teacher"));
                }
                palmerwormTeacher1.TeacherInfo = teacherInfo;
                this.Text = teacherInfo.UniqName + "  老師";
            }
        }

        private void PopupPalmerwormTeacher_FormClosing(object sender, FormClosingEventArgs e)
        {
            TeacherRelated.Teacher.Instance.TeacherDeleted -= new EventHandler<TeacherDeletedEventArgs>(Instance_TeacherDeleted);
            _items.Remove(ID);
        }
    }
}