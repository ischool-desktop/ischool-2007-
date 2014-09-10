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
            //if (MsgBox.Show("�p�G�즳����Ӫ�P�פJ����Ӫ������ƪ���ؤ���W�١A\n�h�|�H�פJ����Ӫ��Ƭ��D�C�O�_�~��פJ�ʧ@�H", "ĵ�i", MessageBoxButtons.YesNo) == DialogResult.Yes)
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