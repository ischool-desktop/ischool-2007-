using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ischool
{
    public partial class ErrorMessage : SmartSchool.Common.BaseForm
    {
        public ErrorMessage(string ErrorMessage)
        {
            InitializeComponent();
            textBoxX1.Lines = ErrorMessage.Split('\n');
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}