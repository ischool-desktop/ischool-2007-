using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Customization.Data;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.ApplicationLog;

namespace SmartSchool.GovernmentalDocument.Process.ProcessWizards
{
    public partial class ChangeDeptProcess : BaseForm
    {
        private StudentRecord _StudentRec;
        private Dictionary<string, string> _DeptIDList = new Dictionary<string, string>();
        private AccessHelper _AccessHelper = new AccessHelper();
        public ChangeDeptProcess(string id)
        {
            InitializeComponent();
            updateRecordInfo1.SetDefaultValue(id);
            updateRecordInfo2.SetDefaultValue(id);
            _StudentRec = _AccessHelper.StudentHelper.GetStudents(id)[0];
            string currentDept = _StudentRec.Department + ( ( "" + _StudentRec.Fields["SubDepartment"] ) == "" ? "" : ":" + _StudentRec.Fields["SubDepartment"] );
            //textBoxX1.Text = _StudentRec.StudentNumber;
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetDepartment();
            foreach ( XmlNode node in dsrsp.GetContent().GetElements("Department") )
            {
                _DeptIDList.Add(node.SelectSingleNode("Name").InnerText.Replace("：", ":"), node.SelectSingleNode("@ID").InnerText);
                if ( node.SelectSingleNode("Name").InnerText.Replace("：", ":") != currentDept )
                    cboDept.Items.Add(node.SelectSingleNode("Name").InnerText.Replace("：", ":"));
            }
            cboClass.DataSource = new List<ClassRecord>(); ;
            cboClass.SelectedItem = null;
            CheckOnTextChanged(null, null);
        }

        private void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDept=cboDept.Text.Contains(":") || cboDept.Text.Contains("：") ? cboDept.Text.Split(':', '：')[0] : cboDept.Text;
            List<ClassRecord> source = new List<ClassRecord>();
            foreach ( ClassRecord classRec in _AccessHelper.ClassHelper.GetAllClass() )//抓同年級該科的班
            {
                if ( classRec.Department == selectedDept&&(_StudentRec.RefClass==null?true:_StudentRec.RefClass.GradeYear==classRec.GradeYear) )
                {
                    source.Add(classRec);
                }
            }
            cboClass.DataSource = source;
        }

        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSeatNo.Items.Clear();
            ClassRecord classRec = (ClassRecord)cboClass.SelectedItem;
            if ( classRec != null )
            {
                List<int> seatList = new List<int>();
                int max=0;
                foreach ( StudentRecord s in classRec.Students )
                {
                    int i = 0;
                    if ( int.TryParse(s.SeatNo, out i) && !seatList.Contains(i) )
                    {
                        seatList.Add(i);
                        if(i>max)
                            max=i;
                    }
                }
                if ( seatList.Count > 0 )
                {
                    for ( int i = 1 ; i <= max+1 ; i++ )
                    {
                        if ( !seatList.Contains(i) )
                            cboSeatNo.Items.Add(i);
                    }
                }
            }
        }

        private void CheckOnTextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            bool pass = true;
            if ( cboSeatNo.Text != "" )
            {
                int i = 0;
                if ( !int.TryParse(cboSeatNo.Text, out i) )
                {
                    errorProvider1.SetError(cboSeatNo, "請輸入數字");
                    pass = false;
                }
            }
            if ( textBoxX1.Text == "" )
            {
                errorProvider1.SetError(textBoxX1, "必需輸入學號");
                pass = false;            
            }
            wizardPage1.NextButtonEnabled = ( cboDept.Text != "" && pass ) ? DevComponents.DotNetBar.eWizardButtonState.True : DevComponents.DotNetBar.eWizardButtonState.False;   
        }

        private void wizardPage2_BackButtonClick(object sender, CancelEventArgs e)
        {
            this.SuspendLayout();
            this.Size = new Size(420, 334);
            this.Location = new Point(this.Location.X +205, this.Location.Y +104);
            this.ResumeLayout();
        }

        private void wizardPage1_NextButtonClick(object sender, CancelEventArgs e)
        {
            this.SuspendLayout();
            this.Size = new Size(930, 543);
            this.Location = new Point(this.Location.X - 205, this.Location.Y - 104);
            this.ResumeLayout();
        }

        private void wizardPage2_AfterPageDisplayed(object sender, DevComponents.DotNetBar.WizardPageChangeEventArgs e)
        {
            updateRecordInfo1.UpdateCode = "301";
            updateRecordInfo1.NewStudentNumberVisible = false;
            updateRecordInfo2.UpdateCode = "211";
            updateRecordInfo2.Department = cboDept.Text.Contains(":") || cboDept.Text.Contains("：") ? cboDept.Text.Split(':', '：')[0] : cboDept.Text;
            updateRecordInfo2.NewStudentNumberVisible = true;
            updateRecordInfo2.NewStudentNumber = textBoxX1.Text;
        }

        private void wizardPage2_FinishButtonClick(object sender, CancelEventArgs e)
        {

            DSXmlHelper helper = new DSXmlHelper("UpdateStudentList");
            helper.AddElement("Student");
            helper.AddElement("Student", "Field");

            StringBuilder desc = new StringBuilder("");
            StudentRecord stu = _StudentRec;
            desc.AppendLine("學生姓名：" + stu.StudentName + " ");
            if ( stu.StudentNumber != textBoxX1 .Text)
            {
                desc.AppendLine("學號由「" + stu.StudentNumber + "」變更為「" + textBoxX1.Text + "」");
                helper.AddElement("Student/Field", "StudentNumber", textBoxX1.Text);
            }
            ClassRecord classRec = (ClassRecord)cboClass.SelectedItem;
            if ( classRec.Department + ( ( "" + classRec.Fields["SubDepartment"] ) == "" ? "" : ":" + classRec.Fields["SubDepartment"] ) != cboDept.Text )
            {
                desc.AppendLine("科別由「" + stu.Department + "」變更為「" + cboDept.Text + "」");
                helper.AddElement("Student/Field", "OverrideDeptID", _DeptIDList[cboDept.Text]);
            }
            else
            {
                desc.AppendLine("科別由「" + stu.Department + "」變更為「" + cboDept.Text + "」");
                helper.AddElement("Student/Field", "OverrideDeptID", "");
            }
            if ( classRec != stu.RefClass )
            {
                desc.AppendLine("班級由「" + ( stu.RefClass == null ? "" : stu.RefClass.ClassName ) + "」變更為「" + classRec.ClassName + "」");
                helper.AddElement("Student/Field", "RefClassID", classRec.ClassID);
            }
            desc.AppendLine("座號由「" + ( stu.SeatNo) + "」變更為「" + cboSeatNo.Text + "」");
            helper.AddElement("Student/Field", "SeatNo", cboSeatNo.Text);

            helper.AddElement("Student", "Condition");
            helper.AddElement("Student/Condition", "ID", stu.StudentID);

            this.Close();
            CurrentUser.Instance.AppLog.Write(EntityType.Student, "學生轉科", stu.StudentID, desc.ToString(), "", "");
            SmartSchool.Feature.EditStudent.Update(new DSRequest(helper));
            this.updateRecordInfo1.Save();
            this.updateRecordInfo2.Save();
            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(stu.StudentID);
        }
    }
}