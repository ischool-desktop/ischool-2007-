using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace StatisticsAndReport.DisciplineRelated
{
    public partial class SelectSemesterForm : BaseForm
    {
        private int _school_year = 50;
        public int SchoolYear
        {
            get { return _school_year; }
        }

        private int _semester = 1;
        public int Semester
        {
            get { return _semester; }
        }

        public SelectSemesterForm()
        {
            InitializeComponent();
        }

        private void SelectSemesterForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = SmartSchool.Customization.Data.SystemInformation.SchoolYear;
            numericUpDown2.Value = SmartSchool.Customization.Data.SystemInformation.Semester;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _school_year = (int)numericUpDown1.Value;
            _semester = (int)numericUpDown2.Value;

            this.DialogResult = DialogResult.OK;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}