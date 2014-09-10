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
    public partial class AcademicAffairsRibbonBarSecond : UserControl, IProcess
    {
        public AcademicAffairsRibbonBarSecond()
        {
            InitializeComponent();
        }

        private void btnUpdateRecord_Click(object sender, EventArgs e)
        {
            new UpdateRecord().ShowDialog();
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
            get { return "教務作業"; }
        }

        #endregion
    }
}
