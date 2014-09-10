using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.CourseRelated.Forms
{
    public partial class CourseDetailForm : BaseForm
    {
        private CourseInformation _course;

        public CourseDetailForm(CourseInformation course)
        {
            Font = FontStyles.General;

            InitializeComponent();

            courseDetailPane1.Font = Font;

            _course = course;
            Text = "課程資訊";
        }

        private void CourseDetailForm_Load(object sender, EventArgs e)
        {
            courseDetailPane1.DisplayDetail(Course);
        }

        private CourseInformation Course
        {
            get { return _course; }
        }

        private static Dictionary<int, CourseDetailForm> _detail_forms;

        static CourseDetailForm()
        {
            _detail_forms = new Dictionary<int, CourseDetailForm>();
        }

        public static void PopupDetail(CourseInformation course)
        {
            if (_detail_forms.ContainsKey(course.Identity))
                _detail_forms[course.Identity].Activate();
            else
            {
                CourseDetailForm detail = new CourseDetailForm(course);
                detail.FormClosed += new FormClosedEventHandler(Editor_FormClosed);
                _detail_forms.Add(course.Identity, detail);
                detail.Show();
            }
        }

        private static void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            CourseDetailForm detail = sender as CourseDetailForm;
            detail.FormClosed -= new FormClosedEventHandler(Editor_FormClosed);

            _detail_forms.Remove(detail.Course.Identity);
        }
    }
}