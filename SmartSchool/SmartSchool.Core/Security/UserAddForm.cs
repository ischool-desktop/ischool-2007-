using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using System.Text.RegularExpressions;
using SmartSchool.Common;

namespace SmartSchool.Security
{
    public partial class UserAddForm : Office2007Form
    {
        private ItemPanel _itemPanel;

        private string _new_user;
        public string NewUser
        {
            get
            {
                if (string.IsNullOrEmpty(_new_user))
                    return "";
                return _new_user;
            }
        }
	

        public UserAddForm(ItemPanel itemPanel)
        {
            InitializeComponent();
            _itemPanel = itemPanel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (ValidateField() == false)
                return;

            //���ͱK�X����
            string passwdHashString = PasswordHash.Compute(textBoxX2.Text);

            try
            {
                Feature.Security.InsertLogin(textBoxX1.Text, passwdHashString);
                _new_user = textBoxX1.Text;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.Show("�s�W�ϥΪ̥��ѡA���~�T���G" + ex.Message);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateField()
        {
            errorProvider1.Clear();

            #region �ˬd�b��

            //���i�ť�
            if (string.IsNullOrEmpty(textBoxX1.Text))
            {
                errorProvider1.SetError(textBoxX1, "���n��줣�i�d��");
                return false;
            }

            Regex re = new Regex(@"^[A-Za-z0-9]((\.)?[A-Za-z0-9]+(\.)?)+[A-Za-z0-9]+$");
            Regex firstChar = new Regex(@"[A-Za-z0-9]");
            Regex doublePoint = new Regex(@"\.\.");

            //�ˬd�榡
            if (!re.Match(textBoxX1.Text).Success)
            {
                if (!firstChar.Match(textBoxX1.Text.Substring(0, 1)).Success)
                    errorProvider1.SetError(textBoxX1, "��p�I�b�����Ĥ@�Ӧr�������O �r��(a-z) �� �Ʀr(0-9)");
                else
                    errorProvider1.SetError(textBoxX1, "��p�I�u�౵�� �r��(a-z)�B�Ʀr(0-9) �M ���I(.)");
                return false;
            }

            //����s����I
            if (doublePoint.Match(textBoxX1.Text).Success)
            {
                errorProvider1.SetError(textBoxX1, "��p�I�b������]�t�s�򪺼��I(.)");
                return false;
            }

            //����b������
            if (textBoxX1.Text.Length < 6 || textBoxX1.Text.Length > 30)
            {
                errorProvider1.SetError(textBoxX1, "��p�I�b���������� 6 �M 30 �����r������");
                return false;
            }

            //�ˬd�b���O�_����
            foreach (User user in _itemPanel.Items)
            {
                if (user.UserName.ToLower() == textBoxX1.Text.ToLower())
                {
                    errorProvider1.SetError(textBoxX1, "��p�I�b�����i�H����");
                    return false;
                }
            }
            #endregion

            #region �ˬd�K�X

            //�ˬd�K�X����
            if (textBoxX2.Text.Length < 4 || textBoxX2.Text.Length > 30)
            {
                errorProvider1.SetError(textBoxX2, "�K�X�������� 4 �M 30 �������r������");
                return false;
            }

            //�ֹ��ӱK�X�r��O�_�ۦP
            if (textBoxX2.Text != textBoxX3.Text)
            {
                errorProvider1.SetError(textBoxX2, "�K�X �P �T�{�K�X ���@�P");
                return false;
            }

            #endregion

            return true;
        }
    }
}