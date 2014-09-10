using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using SmartSchool.AccessControl;

namespace SmartSchool.UserInfomation
{
    [FeatureCode("System0040")]
    public partial class UserInfoManager : BaseForm
    {
        private ErrorProvider _errorProvider;

        public UserInfoManager()
        {
            InitializeComponent();
            _errorProvider = new ErrorProvider();
            lblUserid.Text = "�i " + CurrentUser.Instance.UserName + " �j";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DoubleCheck();
            bool valid = true;
            foreach (Control control in Controls)
            {
                if (_errorProvider.GetError(control) != string.Empty)
                    valid = false;
            }
            if (!valid)
            {
                MsgBox.Show("�K�X��Ʀ��~�A�Х��ץ���A���x�s�I");
                return;
            }

            try
            {
                //�p��K�X��~!
                SmartSchool.Feature.Personal.ChangePassword(PasswordHash.Compute(txtPassword.Text));
            }
            catch (Exception ex)
            {
                MsgBox.Show("�K�X�ܧ󥢱� :" + ex.Message);
                return;
            }
            string accesspoint = CurrentUser.Instance.AccessPoint;
            string username = CurrentUser.Instance.UserName;
            try
            {
                //CurrentUser.Instance.SetConnection(accesspoint, username, txtPassword.Text);
                //CurrentUser.Instance.SetConnection(accesspoint, username, txtPassword.Text);
            }
            catch (Exception ex)
            {
                MsgBox.Show("���s�إ߳s�u���� : " + ex.Message);
                return;
            }
            MsgBox.Show("�K�X�ܧ󧹦��I");
            this.Close();
        }

        private void txtPassword_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(txtPassword, string.Empty);
            if (txtPassword.Text == string.Empty)
                _errorProvider.SetError(txtPassword, "�K�X���i�ťաI");

            else if (txtPassword.Text.Length < 4)
                _errorProvider.SetError(txtPassword, "�K�X���פ��i�֩�4�X�I");
        }

        private void DoubleCheck()
        {
            _errorProvider.SetError(txtConfirm, string.Empty);
            if (txtConfirm.Text == string.Empty)
                _errorProvider.SetError(txtConfirm, "�п�J�T�{�K�X�I");
            else if (txtConfirm.Text != txtPassword.Text)
                _errorProvider.SetError(txtConfirm, "�T�{�K�X�P�s�K�X���šI");
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}