using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using SmartSchool.AccessControl;

namespace SmartSchool.Security
{
    public partial class RoleAddForm : Office2007Form
    {
        private ItemPanel _itemPanel;

        public RoleAddForm(ItemPanel itemPanel)
        {
            InitializeComponent();
            _itemPanel = itemPanel;
        }

        private void RoleAddForm_Load(object sender, EventArgs e)
        {
            comboBoxEx1.Items.Add("<不進行複製>");
            comboBoxEx1.SelectedIndex = 0;

            foreach (Role role in _itemPanel.Items)
            {
                comboBoxEx1.Items.Add(role);
                comboBoxEx1.DisplayMember = "RoleName";
                comboBoxEx1.ValueMember = "RoleID";
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (ValidateRoleName())
            {
                DSXmlHelper helper = null;
                if (comboBoxEx1.SelectedItem is Role)
                {
                    helper = new DSXmlHelper("Permissions");
                    Role role = comboBoxEx1.SelectedItem as Role;
                    foreach (FeatureAce ace in role.Acl)
                    {
                        helper.AddElement("Feature");
                        helper.SetAttribute("Feature", "Code", ace.FeatureCode);
                        helper.SetAttribute("Feature", "Permission", ace.Access.ToString());
                    }
                }

                if (helper != null && helper.PathExist("Feature"))
                    Feature.Security.InsertRole(textBoxX1.Text, textBoxX2.Text, helper.BaseElement);
                else
                    Feature.Security.InsertRole(textBoxX1.Text, textBoxX2.Text);

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateRoleName()
        {
            errorProvider1.Clear();

            //角色名稱不可空白
            if (string.IsNullOrEmpty(textBoxX1.Text))
            {
                errorProvider1.SetError(textBoxX1, "角色名稱不可空白");
                return false;
            }

            //檢查角色名稱是否重複
            foreach (Role role in _itemPanel.Items)
            {
                if (role.RoleName.ToLower() == textBoxX1.Text.ToLower())
                {
                    errorProvider1.SetError(textBoxX1, "角色名稱不可重複");
                    return false;
                }
            }

            return true;
        }
    }
}