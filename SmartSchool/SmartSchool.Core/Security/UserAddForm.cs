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

            //產生密碼雜湊
            string passwdHashString = PasswordHash.Compute(textBoxX2.Text);

            try
            {
                Feature.Security.InsertLogin(textBoxX1.Text, passwdHashString);
                _new_user = textBoxX1.Text;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.Show("新增使用者失敗，錯誤訊息：" + ex.Message);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateField()
        {
            errorProvider1.Clear();

            #region 檢查帳號

            //不可空白
            if (string.IsNullOrEmpty(textBoxX1.Text))
            {
                errorProvider1.SetError(textBoxX1, "必要欄位不可留白");
                return false;
            }

            Regex re = new Regex(@"^[A-Za-z0-9]((\.)?[A-Za-z0-9]+(\.)?)+[A-Za-z0-9]+$");
            Regex firstChar = new Regex(@"[A-Za-z0-9]");
            Regex doublePoint = new Regex(@"\.\.");

            //檢查格式
            if (!re.Match(textBoxX1.Text).Success)
            {
                if (!firstChar.Match(textBoxX1.Text.Substring(0, 1)).Success)
                    errorProvider1.SetError(textBoxX1, "抱歉！帳號的第一個字元必須是 字母(a-z) 或 數字(0-9)");
                else
                    errorProvider1.SetError(textBoxX1, "抱歉！只能接受 字母(a-z)、數字(0-9) 和 數點(.)");
                return false;
            }

            //不能連續數點
            if (doublePoint.Match(textBoxX1.Text).Success)
            {
                errorProvider1.SetError(textBoxX1, "抱歉！帳號不能包含連續的數點(.)");
                return false;
            }

            //限制帳號長度
            if (textBoxX1.Text.Length < 6 || textBoxX1.Text.Length > 30)
            {
                errorProvider1.SetError(textBoxX1, "抱歉！帳號必須介於 6 和 30 間的字元長度");
                return false;
            }

            //檢查帳號是否重複
            foreach (User user in _itemPanel.Items)
            {
                if (user.UserName.ToLower() == textBoxX1.Text.ToLower())
                {
                    errorProvider1.SetError(textBoxX1, "抱歉！帳號不可以重複");
                    return false;
                }
            }
            #endregion

            #region 檢查密碼

            //檢查密碼長度
            if (textBoxX2.Text.Length < 4 || textBoxX2.Text.Length > 30)
            {
                errorProvider1.SetError(textBoxX2, "密碼必須介於 4 和 30 之間的字元長度");
                return false;
            }

            //核對兩個密碼字串是否相同
            if (textBoxX2.Text != textBoxX3.Text)
            {
                errorProvider1.SetError(textBoxX2, "密碼 與 確認密碼 不一致");
                return false;
            }

            #endregion

            return true;
        }
    }
}