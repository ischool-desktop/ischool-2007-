using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.ExceptionHandler;

namespace SmartSchool.CourseRelated.Forms
{
    public partial class EditCourseScore : BaseForm
    {
        private CourseInformation _course;

        public EditCourseScore(CourseInformation course)
        {
            Font = FontStyles.General;

            InitializeComponent();

            score.Font = Font;

            _course = course;
            score.LoadContent(_course.Identity.ToString());
            lblCourseName.Text = course.CourseName + "(" + course.MajorTeacherName + ")";
            Text = lblCourseName.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!score.IsValid)
            {
                MsgBox.Show("輸入成績中有部份資料不符合規範，請修正後再行儲存");
                return;
            }

            try
            {
                score.Save();
                Hide();
            }
            catch (Exception ex)
            {
                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);

                MsgBox.Show(ex.Message);
            }
        }

        private CourseInformation Course
        {
            get { return _course; }
        }

        private static Dictionary<int, EditCourseScore> _editors;

        static EditCourseScore()
        {
            _editors = new Dictionary<int, EditCourseScore>();
        }

        public static void DisplayCourseScore(CourseInformation course)
        {
            if (_editors.ContainsKey(course.Identity))
                _editors[course.Identity].ShowDialog();
            else
            {
                EditCourseScore editor = new EditCourseScore(course);
                editor.FormClosed += new FormClosedEventHandler(Editor_FormClosed);
                _editors.Add(course.Identity, editor);
                editor.ShowDialog();
            }
        }

        private static void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            EditCourseScore editor = sender as EditCourseScore;
            editor.FormClosed -= new FormClosedEventHandler(Editor_FormClosed);

            _editors.Remove(editor.Course.Identity);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}