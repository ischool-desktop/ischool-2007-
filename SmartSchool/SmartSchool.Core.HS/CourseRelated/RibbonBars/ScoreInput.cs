using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.CourseRelated.Forms;
using SmartSchool.CourseRelated;
using SmartSchool.Common;
using SmartSchool.Security;

namespace SmartSchool.CourseRelated.RibbonBars
{
    public partial class ScoreInput : SmartSchool.CourseRelated.RibbonBars.RibbonBarBase
    {
        FeatureAccessControl editScoreCtrl;
        FeatureAccessControl calculateCtrl;

        public ScoreInput()
        {
            InitializeComponent();

            CourseEntity.Instance.SelectionChanged += new EventHandler(Course_SelectionChanged);

            editScoreCtrl = new FeatureAccessControl("Button0550");
            calculateCtrl = new FeatureAccessControl("Button0560");

            editScoreCtrl.Inspect(btnEditScore);
            calculateCtrl.Inspect(btnCalculate);
        }

        private void Course_SelectionChanged(object sender, EventArgs e)
        {
            CourseCollection courses = CourseEntity.Instance.SelectionCourse;

            btnEditScore.Enabled = (courses.Count == 1);
            btnCalculate.Enabled = (courses.Count > 0);

            editScoreCtrl.Inspect(btnEditScore);
            calculateCtrl.Inspect(btnCalculate);
        }

        private void btnEditScore_Click(object sender, EventArgs e)
        {
            CourseCollection courses = CourseEntity.Instance.SelectionCourse;

            if (courses.Count > 0)
                EditCourseScore.DisplayCourseScore(courses[0]);
        }

        private void btnCalcuate_Click(object sender, EventArgs e)
        {

            SmartSchool.CourseRelated.RibbonBars.ScoresCalc.Forms.CalculateWizard wizard = new SmartSchool.CourseRelated.RibbonBars.ScoresCalc.Forms.CalculateWizard();
            wizard.ShowDialog();
        }
    }
}

