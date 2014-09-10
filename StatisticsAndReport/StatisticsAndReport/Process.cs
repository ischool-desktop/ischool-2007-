using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool;

namespace StatisticsAndReport
{
    public partial class Process : UserControl, IProcess
    {
        public Process()
        {
            InitializeComponent();
        }

        private double _level = 3.1;

        #region IProcess 成員

        public double Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public DevComponents.DotNetBar.RibbonBar ProcessRibbon
        {
            get { return ribbonBar1; }
        }

        public string ProcessTabName
        {
            get { return "學務作業"; }
        }

        #endregion

        private void btnDiscipline_Click(object sender, EventArgs e)
        {
            new DisciplineRelated.DisciplineStatistics();
        }
    }
}
