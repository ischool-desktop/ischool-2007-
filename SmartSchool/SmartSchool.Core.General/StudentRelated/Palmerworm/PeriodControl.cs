using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;
using Aspose.Cells;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.Palmerworm
{
    public partial class PeriodControl : UserControl
    {
        public PeriodControl()
        {
            InitializeComponent();
            this.Font = FontStyles.General;            
            this.Width = 48;           
        }

        public LabelX Label
        {
            get { return label; }
        }

        public TextBoxX TextBox
        {
            get { return textBox; }
        }
    }
}
