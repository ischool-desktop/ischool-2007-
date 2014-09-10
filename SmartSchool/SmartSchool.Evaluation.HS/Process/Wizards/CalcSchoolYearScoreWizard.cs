using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Evaluation.Process.Rating;

namespace SmartSchool.Evaluation.Process.Wizards
{
    public partial class CalcSchoolYearScoreWizard : BaseForm
    {
        public CalcSchoolYearScoreWizard()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new CalcSchoolYearSubjectScoreWizard(SelectType.GradeYearStudent).ShowDialog();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            new CalcSchoolYearEntryScoreWizard(SelectType.GradeYearStudent).ShowDialog();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            new SchoolYearRatingForm().ShowDialog();
        }
    }
}