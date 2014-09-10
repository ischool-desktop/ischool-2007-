using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool.Common.Validate
{
    public partial class ErrorViewer : BaseForm, IErrorViewer
    {
        public ErrorViewer()
        {
            InitializeComponent();
        }

        #region IErrorViewer 成員

        public void SetMessage(string message)
        {
            listBox1.Items.Add(message);
        }

        #endregion

        public void Clear()
        {
            listBox1.Items.Clear();
        }
    }
}