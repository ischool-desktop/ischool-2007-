using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using SmartSchool.StudentRelated;
using SmartSchool.Evaluation.Process.Wizards;
using SmartSchool.Evaluation.Process.Rating;
using SmartSchool.AccessControl;

namespace SmartSchool.Evaluation.Process
{
    [FeatureCode("Button0705")]
    public partial class CalculationBatch : SmartSchool.Evaluation.Process.RibbonBarBase
    {
        public CalculationBatch()
        {
            InitializeComponent();
            this.Level = 10.5;
        }
        public override string ProcessTabName
        {
            get
            {
                return "成績處理";
            }
        }
        /// <summary>
        /// 計算學期分項成績
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            new CalcSemesterEntryScoreWizard(SelectType.GradeYearStudent).ShowDialog();

        }
        /// <summary>
        /// 計算學年科目成績
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            new CalcSemesterSubjectScoreWizard(SelectType.GradeYearStudent).ShowDialog();
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            new CalcSemesterMoralScoreWizard(SelectType.GradeYearStudent).ShowDialog();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            new CalcSchoolYearEntryScoreWizard(SelectType.GradeYearStudent).ShowDialog();
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            new CalcSchoolYearMoralScoreWizard(SelectType.GradeYearStudent).ShowDialog();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            new CalcSchoolYearSubjectScoreWizard(SelectType.GradeYearStudent).ShowDialog();
        }

        private void btnSemesterRank_Click(object sender, EventArgs e)
        {
            new SemesterRatingForm().ShowDialog();
        }

        private void btnSchoolYearRank_Click(object sender, EventArgs e)
        {
            new SchoolYearRatingForm().ShowDialog();
        }

        private void buttonItem6_Click_1(object sender, EventArgs e)
        {
            new CalsSemesterScoreWizard().ShowDialog();
        }

        private void buttonItem8_Click_1(object sender, EventArgs e)
        {
            new CalcSchoolYearScoreWizard().ShowDialog();
        }
    }
}

