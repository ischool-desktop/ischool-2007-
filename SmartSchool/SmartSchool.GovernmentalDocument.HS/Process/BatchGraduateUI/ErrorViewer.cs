using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Customization.Data;

namespace SmartSchool.GovernmentalDocument.Process
{
    public partial class ErrorViewer : BaseForm
    {
        private Dictionary<StudentRecord, string> _list;

        public ErrorViewer(Dictionary<StudentRecord, string> list)
        {
            InitializeComponent();

            _list = list;
        }

        private void ErrorViewer_Load(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();
            dataGridViewX1.SuspendLayout();

            foreach (StudentRecord var in _list.Keys)
            {
                int index = dataGridViewX1.Rows.Add();
                DataGridViewRow row = dataGridViewX1.Rows[index];
                row.Cells[colClass.Name].Value = (var.RefClass != null) ? var.RefClass.ClassName : "(未分班)";
                row.Cells[colStudentNumber.Name].Value = var.StudentNumber;
                row.Cells[colStudentName.Name].Value = var.StudentName;
                row.Cells[colError.Name].Value = _list[var];
            }

            dataGridViewX1.ResumeLayout();
        }


    }
}