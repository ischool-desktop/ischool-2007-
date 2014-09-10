using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace StatisticsAndReport.DisciplineRelated.Model
{
    internal class RowStudent : DataGridViewRow
    {
        private Student _student;
        public Student Student
        {
            get { return _student; }
        }
    }
}