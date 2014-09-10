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

            //���o�Ҧ�����
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

            //���o�ϥΪ̲M��Ω��ݨ���
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

            //�ˬd�W�@�ӨϥΪ̨���O�_���ܰ�
            if (_currentUser != null && _valueManager.IsDirty)
            {
                ConfirmMsgBox confirm = new ConfirmMsgBox("", "�ϥΪ� " + _currentUser.UserName + " ����w�Q�ק���|���x�s\n�O�_�x�s�H", "�x�s", "���x�s", "����");
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
            //�M�� ListView �Ҧ� Checked
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
            //���x�s
            _currentUser.Text = lblAccount.Text;
            buttonX2.Enabled = false;
        }

        void confirm_Button1Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPasswd.Text) || !string.IsNullOrEmpty(txtConfirmPasswd.Text))
            {
                if (ValidatePassword() == false)
                {
                    MsgBox.Show("�ϥΪ� " + _currentUser.UserName + " �L�k�x�s\n" + errorProvider1.GetError(txtPasswd));
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
                Log(new User("", addForm.NewUser, null), Action.�s�W, null, false);
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
                MsgBox.Show("�z����R���ۤv���b��");
                return;
            }

            DialogResult result = MsgBox.Show("�z�T�w�n�R���ϥΪ� " + lblAccount.Text + " �ܡH", "�R���ϥΪ�", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Feature.Security.DeleteLogin(_currentUser.ID);
                    //Log
                    Log(_currentUser, Action.�R��, null, false);
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MsgBox.Show("�R�����ѡA���~�T���G" + ex.Message);
                }
                LoadUsersAndRoles();
            }
        }

        private bool ValidatePassword()
        {
            //�ˬd�K�X����
            if (txtPasswd.Text.Length < 4 || txtPasswd.Text.Length > 30)
            {
                errorProvider1.SetError(txtPasswd, "�K�X�������� 4 �M 30 �������r������");
                return false;
            }

            //�ֹ��ӱK�X�r��O�_�ۦP
            if (txtPasswd.Text != txtConfirmPasswd.Text)
            {
                errorProvider1.SetError(txtPasswd, "�K�X �P �T�{�K�X ���@�P");
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

            //�x�s�K�X
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
                    MsgBox.Show("�x�s���ѡA���~�T���G" + ex.Message);
                    return;
                }
            }

            //�x�s����
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
                MsgBox.Show("�x�s�����C");
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show("�x�s���ѡA���~�T���G" + ex.Message);
                return;
            }

            //Log
            Log(_currentUser, Action.�ק�, newRoleList, passwordIsChanged);
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
                text.Append("<font color=\"#FF2020\">��</font>" + lblAccount.Text);
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
            if (action == Action.�ק�)
            {
                desc.Append(" \n");
                if (passwordIsChanged == true)
                    desc.AppendLine("�K�X�w�ܧ�C");
                desc.AppendLine("�����ܧ󬰡G");
                foreach (string each_role in newRoleList)
                    desc.AppendLine("- " + each_role);
            }
            CurrentUser.Instance.AppLog.Write(action + "�ϥΪ�", action + "�ϥΪ� " + user.UserName + desc.ToString(), "�ϥΪ̺޲z", "");
        }
    }

    internal enum Action { �s�W, �ק�, �R�� }

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