using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.Others.Configuration.Setup
{
    public partial class SubjectChineseToEnglishImport : BaseForm
    {
        private bool _overwrite = false;
        public bool Overwrite
        {
            get { return _overwrite; }
        }

        public SubjectChineseToEnglishImport()
        {
            InitializeComponent();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _overwrite = true;
            this.DialogResult = DialogResult.OK;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            //if (MsgBox.Show("如果原有的對照表與匯入的對照表中有重複的科目中文名稱，\n則會以匯入的對照表資料為主。是否繼續匯入動作？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    _overwrite = false;
            //    this.DialogResult = DialogResult.OK;
            //}
            //else
            //    this.DialogResult = DialogResult.Cancel;

            _overwrite = false;
            this.DialogResult = DialogResult.OK;
        }
    }
}