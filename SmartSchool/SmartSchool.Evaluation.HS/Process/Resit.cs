using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using IntelliSchool.DSA30.Util;
using System.Xml;
using Aspose.Cells;
using SmartSchool;
using System.IO;
using System.Diagnostics;
using SmartSchool.Evaluation.Reports;

namespace SmartSchool.Evaluation.Process
{
    public partial class Resit : SmartSchool.Evaluation.Process.RibbonBarBase
    {
        public Resit()
        {
            InitializeComponent();
            this.Level = 12;
        }
        public override string ProcessTabName
        {
            get
            {
                return "¦¨ÁZ³B²z";
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            new ResitListByStudent ();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            new ResitListBySubject();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            new ResitScoreImport();
        }
    }
}

