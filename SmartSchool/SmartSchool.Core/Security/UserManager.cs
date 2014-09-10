using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Common;
using SmartSchool.AccessControl;
using SmartSchool.ApplicationLog;

namespace SmartSchool.Security
{
    [FeatureCode("System0050")]
    public partial class UserManager : Office2007Form
    {
        private Dictionary<string, ListViewItem> _rolesDict;
        private User _currentUser;
        private string _previousUserName;
        private bool _isRegisterItemChecked;
        private RoleValueManager _valueManager;

        public UserManager()
        {
            InitializeComponent();
            _rolesDict = new Dictionary<string, ListViewItem>();
            _currentUser = null;
            _previousUserName = "";
            _isRegisterItemChecked = false;
            _valueManager = new RoleValueManager();
        }

        private void UserManager_Load(object sender, EventArgs e)
        {
            LoadUsersAndRoles();
        }

        private void LoadUsersAndRoles()
        {
            _rolesDict.Clear();
            itemPanel1.Items.Clear();
            listViewEx1.Items.Clear();
            lblAccount.Text = "";
            _currentUser = null;
            _valueManager.Clear();

            //取得所有角色
            DSResponse dsrsp = Feature.Security.GetRoleDetailList();
            foreach (XmlElement roleElement in dsrsp.GetContent().GetElements("Role"))
            {
                string role_id = roleElement.GetAttribute("ID");
                string role_name = roleElement.SelectSingleNode("RoleName").InnerText;
                ListViewItem item = new ListViewItem();
                item.Text = role_name;
                item.Tag = role_id;

                listViewEx1.Items.Add(item);
                _rolesDict.Add(role_name, item);
            }

            //取得使用者清單及所屬角色
            dsrsp = Feature.Security.GetLoginDetailList();
            foreach (XmlElement loginElement in dsrsp.GetContent().GetElements("Login"))
            {
                string id = loginElement.GetAttribute("ID");
                string login_name = loginElement.SelectSingleNode("LoginName").InnerText;
                List<string> roles = new List<string>();
                foreach (XmlElement roleElement in loginElement.SelectNodes("Roles/Role"))
                {
                    string role_name = roleElement.GetAttribute("Name");
                    roles.Add(role_name);
                }

                User user = new User(id, login_name, roles);
                user.OptionGroup = "User";
                user.Text = "<font>" + login_name + "</font>";
                //user.Text = login_name;
                user.TextChanged += new EventHandler(user_TextChanged);
                user.Click += new EventHandler(UserItem_Click);
                if (_previousUserName == login_name)
                    user.RaiseClick();
                itemPanel1.Items.Add(user);
            }

            itemPanel1.Refresh();
        }

        void user_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserItem_Click(object sender, EventArgs e)
        {
            User user = sender as User;

            if (_currentUser != null && _currentUser == user)
                return;

            _previousUserName = user.UserName;

            //檢查上一個使用者角色是否有變動
            if (_currentUser != null && _valueManager.IsDirty)
            {
                ConfirmMsgBox confirm = new ConfirmMsgBox("", "使用者 " + _currentUser.UserName + " 角色已被修改但尚未儲存\n是否儲存？", "儲存", "不儲存", "取消");
                confirm.Button1Click += new EventHandler(confirm_Button1Click);
                confirm.Button2Click += new EventHandler(confirm_Button2Click);
                confirm.Button3Click += new EventHandler(confirm_Button3Click);
                confirm.ShowDialog();
                if (confirm.DialogResult == ConfirmMsgBox.Result.Button3)
                    return;
            }

            if (_isRegisterItemChecked)
                listViewEx1.ItemChecked -= new ItemCheckedEventHandler(listViewEx1_ItemChecked);
            _isRegisterItemChecked = false;

            FillListView(user.Roles);

            _valueManager.InitRole(listViewEx1);

            listViewEx1.ItemChecked += new ItemCheckedEventHandler(listViewEx1_ItemChecked);
            _isRegisterItemChecked = true;

            lblAccount.Text = user.UserName;
            txtPasswd.Text = "";
            txtConfirmPasswd.Text = "";
            errorProvider1.Clear();
            _currentUser = user;
        }

        void FillListView(List<string> roles)
        {
            //清除 ListView 所有 Checked
            foreach (ListViewItem listViewItem in listViewEx1.Items)
                listViewItem.Checked = false;

            foreach (string role in roles)
                _rolesDict[role].Checked = true;
        }

        void confirm_Button3Click(object sender, EventArgs e)
        {
            _currentUser.Click -= new EventHandler(UserItem_Click);
            _currentUser.RaiseClick();
            _currentUser.Click += new EventHandler(UserItem_Click);
            _previousUserName = _currentUser.UserName;
        }

        void confirm_Button2Click(object sender, EventArgs e)
        {
            //不儲存
            _currentUser.Text = lblAccount.Text;
            buttonX2.Enabled = false;
        }

        void confirm_Button1Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPasswd.Text) || !string.IsNullOrEmpty(txtConfirmPasswd.Text))
            {
                if (ValidatePassword() == false)
                {
                    MsgBox.Show("使用者 " + _currentUser.UserName + " 無法儲存\n" + errorProvider1.GetError(txtPasswd));
                    return;
                }
            }
            Save();
        }

        //private bool IsPreviousUserChanged()
        //{
        //    foreach (ListViewItem item in listViewEx1.Items)
        //    {
        //        if (item.Checked)
        //        {
        //            if (!_currentUser.Roles.Contains(item.Text))
        //                return true;
        //        }
        //        else
        //        {
        //            if (_currentUser.Roles.Contains(item.Text))
        //                return true;
        //        }
        //    }
        //    return false;
        //}

        private void buttonX1_Click(object sender, EventArgs e)
        {
            UserAddForm addForm = new UserAddForm(itemPanel1);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                //log
                Log(new User("", addForm.NewUser, null), Action.新增, null, false);
                LoadUsersAndRoles();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (CurrentUser.Instance.UserName == _currentUser.UserName)
            {
                MsgBox.Show("您不能刪除自己的帳號");
                return;
            }

            DialogResult result = MsgBox.Show("您確定要刪除使用者 " + lblAccount.Text + " 嗎？", "刪除使用者", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Feature.Security.DeleteLogin(_currentUser.ID);
                    //Log
                    Log(_currentUser, Action.刪除, null, false);
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MsgBox.Show("刪除失敗，錯誤訊息：" + ex.Message);
                }
                LoadUsersAndRoles();
            }
        }

        private bool ValidatePassword()
        {
            //檢查密碼長度
            if (txtPasswd.Text.Length < 4 || txtPasswd.Text.Length > 30)
            {
                errorProvider1.SetError(txtPasswd, "密碼必須介於 4 和 30 之間的字元長度");
                return false;
            }

            //核對兩個密碼字串是否相同
            if (txtPasswd.Text != txtConfirmPasswd.Text)
            {
                errorProvider1.SetError(txtPasswd, "密碼 與 確認密碼 不一致");
                return false;
            }
            return true;
        }

        private void Save()
        {
            if (_currentUser == null)
                return;

            bool passwordIsChanged = false;
            List<string> newRoleList = new List<string>();

            //儲存密碼
            if (!string.IsNullOrEmpty(txtPasswd.Text) || !string.IsNullOrEmpty(txtConfirmPasswd.Text))
            {
                if (ValidatePassword() == false)
                    return;
                try
                {
                    Feature.Security.UpdateLogin(_currentUser.UserName, PasswordHash.Compute(txtPasswd.Text));
                    passwordIsChanged = true;
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MsgBox.Show("儲存失敗，錯誤訊息：" + ex.Message);
                    return;
                }
            }

            //儲存角色
            List<string> idlist = new List<string>();
            foreach (ListViewItem item in listViewEx1.Items)
            {
                if (item.Checked)
                {
                    idlist.Add(item.Tag as string);
                    newRoleList.Add(item.Text);
                }
            }
            try
            {
                Feature.Security.DeleteLRBelong(_currentUser.ID);
                if (idlist.Count > 0)
                    Feature.Security.InsertLRBelong(_currentUser.ID, idlist.ToArray());
                MsgBox.Show("儲存完成。");
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show("儲存失敗，錯誤訊息：" + ex.Message);
                return;
            }

            //Log
            Log(_currentUser, Action.修改, newRoleList, passwordIsChanged);
            LoadUsersAndRoles();
        }

        private void lblAccount_TextChanged(object sender, EventArgs e)
        {
            buttonX3.Enabled = true;
            //buttonX2.Enabled = true;
            listViewEx1.Enabled = true;
            if (string.IsNullOrEmpty(lblAccount.Text))
            {
                buttonX3.Enabled = false;
                buttonX2.Enabled = false;
                listViewEx1.Enabled = false;
            }

            //buttonX2.Enabled = !(lblAccount.Text == CurrentUser.Instance.UserName);
            txtPasswd.Enabled = !(lblAccount.Text == CurrentUser.Instance.UserName);
            txtConfirmPasswd.Enabled = !(lblAccount.Text == CurrentUser.Instance.UserName);
            //listViewEx1.Enabled = !(lblAccount.Text == CurrentUser.Instance.UserName);
        }

        private void listViewEx1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_currentUser == null)
                return;

            _valueManager.SetRole(listViewEx1);

            if (_valueManager.IsDirty)
            {
                StringBuilder text = new StringBuilder("");
                text.Append("<font color=\"#FF2020\">★</font>" + lblAccount.Text);
                _currentUser.Text = text.ToString();
                buttonX2.Enabled = true;
            }
            else
            {
                _currentUser.Text = "<font>" + lblAccount.Text + "</font>";
                buttonX2.Enabled = false;
            }
            itemPanel1.Refresh();
        }

        private void txtPasswd_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPasswd.Text))
                buttonX2.Enabled = true;
            else
                buttonX2.Enabled = _valueManager.IsDirty;
        }

        private void Log(User user, Action action, List<string> newRoleList, bool passwordIsChanged)
        {
            StringBuilder desc = new StringBuilder("");
            if (action == Action.修改)
            {
                desc.Append(" \n");
                if (passwordIsChanged == true)
                    desc.AppendLine("密碼已變更。");
                desc.AppendLine("角色變更為：");
                foreach (string each_role in newRoleList)
                    desc.AppendLine("- " + each_role);
            }
            CurrentUser.Instance.AppLog.Write(action + "使用者", action + "使用者 " + user.UserName + desc.ToString(), "使用者管理", "");
        }
    }

    internal enum Action { 新增, 修改, 刪除 }

    internal class User : ButtonItem
    {
        public User(string id, string userName, List<string> roles)
        {
            _id = id;
            _user_name = userName;
            _roles = roles;
        }

        private string _id;
        public string ID
        {
            get { return _id; }
        }

        private string _user_name;
        public string UserName
        {
            get { return _user_name; }
        }

        private List<string> _roles;
        public List<string> Roles
        {
            get { return _roles; }
        }
    }

    internal class RoleValueManager
    {
        private Dictionary<string, bool> _old = new Dictionary<string, bool>();
        private Dictionary<string, bool> _new = new Dictionary<string, bool>();

        private bool _isDirty = false;
        public bool IsDirty
        {
            get { return _isDirty; }
        }

        public void Clear()
        {
            _old.Clear();
            _new.Clear();
            _isDirty = false;
        }

        public void InitRole(DevComponents.DotNetBar.Controls.ListViewEx listView)
        {
            _old.Clear();
            _isDirty = false;
            foreach (ListViewItem item in listView.Items)
                _old.Add(item.Text, item.Checked);
        }

        public void SetRole(DevComponents.DotNetBar.Controls.ListViewEx listView)
        {
            _new.Clear();
            _isDirty = false;
            foreach (ListViewItem item in listView.Items)
                _new.Add(item.Text, item.Checked);

            if (_old != null)
                foreach (string name in _old.Keys)
                    if (_old[name] != _new[name])
                    {
                        _isDirty = true;
                        break;
                    }
        }
    }
}