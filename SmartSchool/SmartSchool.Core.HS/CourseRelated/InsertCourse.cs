using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.CourseRelated;
using SmartSchool.Feature.Course;
using DevComponents.DotNetBar;

namespace SmartSchool.CourseRelated
{
    public partial class InsertCourse : BaseForm
    {
        private string _newCourseID;

        public string NewCourseID
        {
            get { return _newCourseID; }
        }

        public InsertCourse()
        {
            InitializeComponent();

            foreach (int var in CourseEntity.Instance.Semesters.GroupSchoolYear())
            {
                comboBoxEx1.Items.Add(var);
                if (var == CurrentUser.Instance.SchoolYear)
                    comboBoxEx1.SelectedIndex = comboBoxEx1.Items.Count - 1;
            }

            comboBoxEx2.Items.Add("1");
            comboBoxEx2.Items.Add("2");
            comboBoxEx2.SelectedIndex = 0;
            //comboBoxEx2.SelectedIndex = CurrentUser.Instance.Semester - 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCourseName.Text))
            {
                MsgBox.Show("請輸入課程名稱。");
                return;
            }

            int a;
            if (!int.TryParse(comboBoxEx1.Text, out a))
            {
                MsgBox.Show("學年度請輸入數字。");
                return;
            }

            if (!CourseEntity.Instance.ValidateCourse(txtCourseName.Text, comboBoxEx1.Text, comboBoxEx2.Text))
            {
                MsgBox.Show("課程重複。");
                return;
            }

            _newCourseID = AddCourse.Insert(txtCourseName.Text, comboBoxEx1.Text, comboBoxEx2.Text);
            CurrentUser.Instance.AppLog.Write(SmartSchool.ApplicationLog.EntityType.Course, "新增課程", _newCourseID, "新課程名稱：" + txtCourseName.Text, "課程", _newCourseID);

            if (!string.IsNullOrEmpty(_newCourseID))
            {
                //CourseEntity.Instance.InvokeAfterCourseInsert();
                SmartSchool.Broadcaster.Events.Items["課程/新增"].Invoke();
                this.Close();

                if (chkUpdateOther.Checked)
                    this.DialogResult = DialogResult.Yes;
            }
            else
                MsgBox.Show("新增失敗");

        }
    }
}