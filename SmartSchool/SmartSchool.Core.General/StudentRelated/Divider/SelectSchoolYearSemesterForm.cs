using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool.StudentRelated.Divider
{
    public partial class SelectSchoolYearSemesterForm : SmartSchool.Common.BaseForm
    {
        public int SchoolYear
        {
            get
            {
                return  Convert.ToInt32(numSchoolYear.Value);
            }
        }

        public int Semester
        {
            get
            {
               return Convert.ToInt32(numSemester.Value);
            }
        }

        public SelectSchoolYearSemesterForm()
        {
            InitializeComponent();
            if (SmartSchool.CurrentUser.Instance.IsLogined)
            {
                numSchoolYear.Value = SmartSchool.CurrentUser.Instance.SchoolYear;
                numSemester.Value = SmartSchool.CurrentUser.Instance.Semester;
            }
        }

        protected virtual void buttonX1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}