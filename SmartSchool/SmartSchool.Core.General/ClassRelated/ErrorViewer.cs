using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated
{
    public partial class ErrorViewer : BaseForm
    {
        public ErrorViewer(string title,List<string> errors)
        {
            InitializeComponent();
            this.Text = title+"(共"+errors.Count+"筆)";
            foreach ( string var in errors )
            {
                richTextBox1.Text += var + "\n";
            }
        }
    }
}