using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feature;
using SmartSchool.Security;

namespace SmartSchool.StudentRelated.Process.StudentIUD
{
    public partial class StudentIDUProcess : UserControl, IProcess, IPalmerwormManager
    {
        FeatureAccessControl addCtrl;
        FeatureAccessControl saveCtrl;
        FeatureAccessControl delCtrl;

        static private StudentIDUProcess _Instance;
        static public StudentIDUProcess Instance
        {
            get
            {
                if (_Instance == null) _Instance = new StudentIDUProcess();
                return _Instance;
            }
        }

        private StudentIDUProcess()
        {
            InitializeComponent();

            superTooltip1.DefaultFont = FontStyles.General;

            //buttonItem14.Enabled = CurrentUser.Instance.HasPermission(typeof(AddStudent));

            //Student.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler += delegate
            {
                btnDeleteStudent.Enabled = (Student.Instance.SelectionStudents.Count == 1);
                delCtrl.Inspect(btnDeleteStudent);
            };
            //權限判斷 - 新增修改刪除 學生
            addCtrl = new FeatureAccessControl("Button0010");
            saveCtrl = new FeatureAccessControl("Button0020");
            delCtrl = new FeatureAccessControl("Button0030");

            addCtrl.Inspect(btnAddStudent);
            saveCtrl.Inspect(btnSaveStudent);
            delCtrl.Inspect(btnDeleteStudent);
        }

        //void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    btnDeleteStudent.Enabled = (Student.Instance.SelectionStudents.Count == 1);

        //    delCtrl.Inspect(btnDeleteStudent);
        //}

        #region IProcess 成員

        public string ProcessTabName
        {
            get { return "學生"; }
        }

        public DevComponents.DotNetBar.RibbonBar ProcessRibbon
        {
            get { return ribbonBar1; }
        }


        private double _Level = 0;
        public double Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        #endregion

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            InsertStudentWizard wizard = new InsertStudentWizard();
            if (wizard.ShowDialog() == DialogResult.Yes)
            {
                PopupPalmerwormStudent.ShowPopupPalmerwormStudent(wizard.NewStudentID);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("是否將 \"" + Student.Instance.SelectionStudents[0].Name + "\" 移至已刪除？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    BriefStudentData stu = Student.Instance.SelectionStudents[0];
                    string stu_id = stu.ID;
                    string stu_student_number = stu.StudentNumber;
                    string stu_name = stu.Name;
                    RemoveStudent.DeleteStudent(stu_id);

                    //Log
                    CurrentUser.Instance.AppLog.Write(
                        SmartSchool.ApplicationLog.EntityType.Student,
                        "刪除學生",
                        stu_id,
                        "學生「" + stu_name + (string.IsNullOrEmpty(stu_student_number) ? "" : " (" + stu_student_number + ")") + "」已變更為刪除。",
                        "學生",
                        string.Format("學生姓名：{0}，學號：{1}", stu_name, stu_student_number));

                    //Student.Instance.InvokBriefDataChanged(Student.Instance.SelectionStudents[0].ID);
                    SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(stu_id);
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MsgBox.Show("刪除學生失敗。\n" + ex.Message);
                }
            }
        }

        #region IPalmerwormManager 成員

        public bool EnableSave
        {
            get
            {
                return btnSaveStudent.Enabled;
            }
            set
            {
                btnSaveStudent.Enabled = value;
            }
        }

        public bool EnableCancel
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        public event EventHandler Save;

        public event EventHandler Cacel;

        public event EventHandler Reflash;

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save != null)
                Save.Invoke(this, new EventArgs());
        }
    }
}
