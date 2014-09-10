using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using DevComponents.DotNetBar;

namespace SmartSchool.StudentRelated.RibbonBars
{
    public partial class RibbonBarBase : UserControl, IProcess
    {
        public RibbonBarBase()
        {
            InitializeComponent();
        }

        #region IProcess 成員

        public virtual string ProcessTabName
        {
            get { return "未定"; }
        }

        public virtual RibbonBar ProcessRibbon
        {
            get { return MainRibbonBar; }
        }

        private double _Level = 0;
        public double Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        #endregion
    }
}
