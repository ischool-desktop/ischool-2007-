using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.StudentRelated
{
    internal partial class PopupPalmerwormStudent : SmartSchool.Common.BaseForm
    {
        static private Dictionary<string, PopupPalmerwormStudent> _items = new Dictionary<string, PopupPalmerwormStudent>();

        static public void ShowPopupPalmerwormStudent(string id)
        {
            if ( _items.ContainsKey(id) )
            {
                if ( _items[id].WindowState == FormWindowState.Minimized )
                    _items[id].WindowState = FormWindowState.Normal;
                _items[id].Activate();
            }
            else
            {
                PopupPalmerwormStudent popup = new PopupPalmerwormStudent();
                _items.Add(id, popup);
                popup.ID = id;
                popup.Show();
            }
        }

        private PopupPalmerwormStudent()
        {
            InitializeComponent();
            Student.Instance.StudentDeleted += new EventHandler<StudentDeletedEventArgs>(Student_StudentDeleted);
        }

        private void PopupPalmerwormStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Student.Instance.StudentDeleted -= new EventHandler<StudentDeletedEventArgs>(Student_StudentDeleted);
            _items.Remove(ID);
        }

        private void Student_StudentDeleted(object sender, StudentDeletedEventArgs e)
        {
            if ( e.ID == ID )
                Close();
        }
        private string ID
        {
            get { return palmerwormStudent1.StudentInfo.ID; }
            set
            {
                Dictionary<string, BriefStudentData> studentInfo = new Dictionary<string, BriefStudentData>();
                DSResponse resp = Feature.QueryStudent.GetAbstractInfo(value);
                XmlElement[] elements = resp.GetContent().GetElements("Student");
                foreach ( XmlElement ele in elements )
                {
                    BriefStudentData newData;
                    string id = ele.SelectSingleNode("@ID").InnerText;
                    if ( studentInfo.ContainsKey(id) )
                    {
                        newData = studentInfo[id];
                    }
                    else
                    {
                        newData = new BriefStudentData(ele);
                        studentInfo.Add(id, newData);
                    }
                }
                if ( studentInfo.Count > 0 )
                {
                    palmerwormStudent1.StudentInfo = studentInfo[value];
                    this.Text = ( studentInfo[value].ClassName == "" ? "未分班級" : ( studentInfo[value].ClassName + ( ( studentInfo[value].SeatNo == "" ) ? "" : "(" + studentInfo[value].SeatNo + "號)" ) ) ) + " " + studentInfo[value].Name;
                }
            }
        }
    }
}