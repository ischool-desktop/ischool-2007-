using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.Others.Configuration.WordCommentMapping
{
    public partial class OverrideConfirm : BaseForm
    {
        public OverrideConfirm()
        {
            InitializeComponent();
        }

        private bool _overwrite = false;
        public bool Overwrite
        {
            get { return _overwrite; }
        }

        private void btnOverride_Click(object sender, EventArgs e)
        {
            _overwrite = true;
            this.DialogResult = DialogResult.OK;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("如果原有的文字評量代碼表與匯入的文字評量代碼表中有重複的代碼，\n則會以後來匯入的文字評量代碼表為主。是否繼續匯入動作？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _overwrite = false;
                this.DialogResult = DialogResult.OK;
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }
    }
}