using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.StudentRelated.RibbonBars
{
    public partial class TemporaStudentManager : RibbonBarBase
    {
        public TemporaStudentManager()
        {
            InitializeComponent();
            controlContainerItem1.Control = panel1;
            //設定畫面分隔線樣式
            expandableSplitter1.GripLightColor = expandableSplitter1.GripDarkColor = System.Drawing.Color.Transparent;
            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler += new EventHandler<SmartSchool.Broadcaster.EventArguments>(TemporaStudentManager_Handler);
            Student.Instance.TemporalChanged += new EventHandler(Instance_TemporalChanged);
        }

        void Instance_TemporalChanged(object sender, EventArgs e)
        {
            TemporaStudentManager_Handler(null, null);
        }

        void TemporaStudentManager_Handler(object sender, SmartSchool.Broadcaster.EventArguments e)
        {
            List<BriefStudentData> selectedList = Student.Instance.SelectionStudents;
            List<BriefStudentData> tempList = Student.Instance.TemporaStudent;
            bool canAdd = false;
            bool canRemove = false;
            foreach ( BriefStudentData stu in selectedList )
            {
                if ( tempList.Contains(stu) )
                    canRemove = true;
                else
                    canAdd = true;
            }
            btnAdd.Enabled = canAdd;
            btnRemove.Enabled = canRemove;
        }

        public override string ProcessTabName
        {
            get
            {
                return "學生";
            }
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            bool pass = false,confuse=false;
            pictureBox1.Visible = false;
            textBoxX2.Text = "";
            if ( textBoxX1.Text != "" )
            {
                foreach ( ClassRelated.ClassInfo cifo in ClassRelated.Class.Instance.Items )
                {
                    if ( cifo.ClassName == textBoxX1.Text )
                    {
                        pictureBox1.Visible = true;
                        pass = true;
                    }
                    else if ( cifo.ClassName.StartsWith(textBoxX1.Text) )
                        confuse = true;
                }
                if ( pass & confuse == false )
                    textBoxX2.Focus();
            }
        }

        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyData == Keys.Enter && textBoxX1.Text != "" )
            {
                foreach ( ClassRelated.ClassInfo cifo in ClassRelated.Class.Instance.Items )
                {
                    if ( cifo.ClassName == textBoxX1.Text )
                    {
                        textBoxX2.Focus();
                    }
                }
            }
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            bool pass = false;
            pictureBox2.Visible = false;
            if ( textBoxX2.Text != "" )
            {
                foreach ( ClassRelated.ClassInfo cifo in ClassRelated.Class.Instance.Items )
                {
                    if ( cifo.ClassName == textBoxX1.Text )
                    {
                        foreach ( BriefStudentData stu in cifo.Students )
                        {
                            if ( stu.SeatNo == textBoxX2.Text )
                                pass = true;
                        }
                    }
                }
                if ( pass )
                    pictureBox2.Visible = true;
            }
        }

        private void textBoxX2_Enter(object sender, EventArgs e)
        {
            textBoxX2.SelectAll();
        }

        private void textBoxX1_Enter(object sender, EventArgs e)
        {
            textBoxX1.SelectAll();
        }

        private void textBoxX2_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyData == Keys.Enter && textBoxX2.Text != "" )
            {
                foreach ( ClassRelated.ClassInfo cifo in ClassRelated.Class.Instance.Items )
                {
                    if ( cifo.ClassName == textBoxX1.Text )
                    {
                        foreach ( BriefStudentData stu in cifo.Students )
                        {
                            if ( stu.SeatNo == textBoxX2.Text )
                            {
                                if ( !StudentRelated.Student.Instance.TemporaStudent.Contains(stu) )
                                {
                                    List<BriefStudentData> list=Student.Instance.TemporaStudent;
                                    list.Add(stu);
                                    StudentRelated.Student.Instance.TemporaStudent = list;
                                    Student.Instance.ViewTemporaStudent();
                                    Customization.PlugIn.Global.SetStatusBarMessage("將" + textBoxX1.Text + "(" + textBoxX2.Text + "號)" + stu.Name + "加入待處理。");
                                }
                                else
                                {
                                    Student.Instance.ViewTemporaStudent();
                                    Customization.PlugIn.Global.SetStatusBarMessage("" + textBoxX1.Text + "(" + textBoxX2.Text + "號)" + stu.Name + "已經加入待處理。");
                                }
                                //textBoxX1.Text = textBoxX2.Text = "";
                                textBoxX1.Focus();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void textBoxX3_Enter(object sender, EventArgs e)
        {
            textBoxX3.SelectAll();
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            if ( textBoxX3.Text != "" )
            {
                foreach ( BriefStudentData stu in Student.Instance.Items )
                {
                    if ( stu.StudentNumber == textBoxX3.Text )
                        pictureBox3.Visible = true;
                }
            }
        }

        private void textBoxX3_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyData == Keys.Enter && textBoxX3.Text != "" )
            {
                foreach ( BriefStudentData stu in Student.Instance.Items )
                {
                    if ( stu.StudentNumber == textBoxX3.Text )
                    {
                        if ( !StudentRelated.Student.Instance.TemporaStudent.Contains(stu) )
                        {
                            List<BriefStudentData> list=Student.Instance.TemporaStudent;
                            list.Add(stu);
                            StudentRelated.Student.Instance.TemporaStudent = list;
                            Customization.PlugIn.Global.SetStatusBarMessage("將" + textBoxX3.Text +" "+ stu.Name + "加入待處理。");
                        }
                        else
                        {
                            Customization.PlugIn.Global.SetStatusBarMessage("" + textBoxX3.Text + " " + stu.Name + "已經加入待處理。");
                        }
                        textBoxX3.SelectAll();
                        Student.Instance.ViewTemporaStudent();
                        break;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<BriefStudentData> selectedList = Student.Instance.SelectionStudents;
            List<BriefStudentData> tempList = Student.Instance.TemporaStudent;
            int count = 0;
            foreach ( BriefStudentData stu in selectedList )
            {
                if ( !tempList.Contains(stu) )
                {
                    tempList.Add(stu);
                    count++;
                }
            }
            Student.Instance.TemporaStudent = tempList;
            Customization.PlugIn.Global.SetStatusBarMessage("加入" + count + "名學生至待處理。");
            TemporaStudentManager_Handler(null, null);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<BriefStudentData> selectedList = Student.Instance.SelectionStudents;
            List<BriefStudentData> tempList = Student.Instance.TemporaStudent;
            int count = 0;
            foreach ( BriefStudentData stu in selectedList )
            {
                if ( tempList.Contains(stu) )
                {
                    tempList.Remove(stu);
                    count++;
                }
            }
            Student.Instance.TemporaStudent = tempList;
            Customization.PlugIn.Global.SetStatusBarMessage("從待處理中移出"+count+"名學生。");
            TemporaStudentManager_Handler(null, null);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Student.Instance.ViewTemporaStudent();
        }
    }
}
