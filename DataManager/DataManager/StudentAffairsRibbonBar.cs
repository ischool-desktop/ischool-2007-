using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool;

namespace DataManager
{
    public partial class StudentAffairsRibbonBar : UserControl, IProcess
    {
        public StudentAffairsRibbonBar()
        {
            InitializeComponent();
        }

        private void btnDiscipline_Click(object sender, EventArgs e)
        {
            new Discipline().ShowDialog();
        }

        #region IProcess 成員

        private double _level = 7;
        public double Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public DevComponents.DotNetBar.RibbonBar ProcessRibbon
        {
            get { return this.ribbonBar1; }
        }

        public string ProcessTabName
        {
            get { return "學務作業"; }
        }

        #endregion
    }
}
