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
    public partial class ImportConfirm : BaseForm
    {
        private bool _overwrite = false;
        public bool Overwrite
        {
            get { return _overwrite; }
        }

        public ImportConfirm()
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
            if (MsgBox.Show("�p�G�즳����r���q�N�X��P�פJ����r���q�N�X�������ƪ��N�X�A\n�h�|�H��ӶפJ����r���q�N�X���D�C�O�_�~��פJ�ʧ@�H", "ĵ�i", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _overwrite = false;
                this.DialogResult = DialogResult.OK;
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }
    }
}