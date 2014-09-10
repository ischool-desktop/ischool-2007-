using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;
using IntelliSchool.DSA30.Util;
using DevComponents.DotNetBar.Controls;
using SmartSchool.AccessControl;
using SmartSchool.Common;

namespace SmartSchool.Security
{
    [FeatureCode("System0060")]
    public partial class RoleManager : Office2007Form
    {
        private Role _currentRole;
        private PermissionValueManager _valueManager; //�޲z�e���W���v�����ܨƥ�
        private List<FeatureCatalog> _featureCatalogList;
        private bool _isRegisterPermissionChanged; //FeatureCatalog �O�_���U PermissionChanged
        private Dictionary<string, Control> _mapDataGridView; //Feature Path ���� DataGridView
        private Dictionary<string, RowFeature> _features; //�Ҧ� Feature
        private string _previousRoleName;

        public RoleManager()
        {
            InitializeComponent();
            _currentRole = null;
            _valueManager = new PermissionValueManager();
            _featureCatalogList = new List<FeatureCatalog>();
            _isRegisterPermissionChanged = false;
            _mapDataGridView = new Dictionary<string, Control>();
            _feature_def = new DSXmlHelper(DSXmlHelper.LoadXml(Properties.Resources.FeatureDefinition));
            _features = new Dictionary<string, RowFeature>();
            _previousRoleName = "";
        }

        private DSXmlHelper _feature_def;
        public DSXmlHelper FeatureDefinition
        {
            get { return _feature_def; }
        }

        private void RoleManager_Load(object sender, EventArgs e)
        {
            LoadRoles();
            BuildDataGridViewMap();
            FillFeatureDefinition();
        }

        private void LoadRoles()
        {
            itemPanel1.Items.Clear();
            _currentRole = null;
            txtRoleName.Text = "";
            _valueManager.Clear();

            DSResponse dsrsp = Feature.Security.GetRoleDetailList();
            foreach (XmlElement roleElement in dsrsp.GetContent().GetElements("Role"))
            {
                DSXmlHelper roleHelper = new DSXmlHelper(roleElement);
                string role_id = roleHelper.GetText("@ID");
                string role_name = roleHelper.GetText("RoleName");
                string role_desc = roleHelper.GetText("Description");
                FeatureAcl acl = new FeatureAcl();

                foreach (XmlElement featureElement in roleElement.SelectNodes("Permission/Permissions/Feature"))
                {
                    DSXmlHelper featureHelper = new DSXmlHelper(featureElement);
                    FeatureAce ace = new FeatureAce(featureHelper.GetText("@Code"), featureHelper.GetText("@Permission"));
                    acl.MergeAce(ace);
                }

                Role role = new Role(role_id, role_name, acl);
                role.OptionGroup = "Role";
                role.Tooltip = role_desc;
                role.Text = "<font>" + role_name + "</font>";
                role.DoubleClick += new EventHandler(RoleItem_DoubleClick);
                role.Click += new EventHandler(RoleItem_Click);
                if (_previousRoleName == role_name)
                    role.RaiseClick();
                itemPanel1.Items.Add(role);
            }
            itemPanel1.Refresh();
        }

        private void BuildDataGridViewMap()
        {
            _mapDataGridView.Add("�ǥ�/�\����s", dataGridViewX1);
            _mapDataGridView.Add("�ǥ�/��ƶ���", dataGridViewX2);
            _mapDataGridView.Add("�ǥ�/����", dataGridViewX3);

            _mapDataGridView.Add("�Z��/�\����s", dataGridViewX4);
            _mapDataGridView.Add("�Z��/��ƶ���", dataGridViewX5);
            _mapDataGridView.Add("�Z��/����", dataGridViewX6);

            _mapDataGridView.Add("�Юv/�\����s", dataGridViewX7);
            _mapDataGridView.Add("�Юv/��ƶ���", dataGridViewX8);
            _mapDataGridView.Add("�Юv/����", dataGridViewX9);

            _mapDataGridView.Add("�ҵ{/�\����s", dataGridViewX10);
            _mapDataGridView.Add("�ҵ{/��ƶ���", dataGridViewX11);
            _mapDataGridView.Add("�ҵ{/����", dataGridViewX12);

            _mapDataGridView.Add("�аȧ@�~/�\����s", dataGridViewX13);

            _mapDataGridView.Add("�ǰȧ@�~/�\����s", dataGridViewX14);

            _mapDataGridView.Add("�t��/�t�Υ\��", dataGridViewX15);

            dataGridViewX1.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX2.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX3.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX4.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX5.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX6.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX7.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX8.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX9.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX10.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX11.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX12.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX13.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX14.DoubleClick += new EventHandler(DG_DoubleClick);
            dataGridViewX15.DoubleClick += new EventHandler(DG_DoubleClick);
        }

        private void DG_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewX dg = sender as DataGridViewX;
            if (dg == null) return;

            RowFeature row = dg.CurrentRow as RowFeature;
            if (row == null) return;
            if (row.FeatureData == null) return;

            Clipboard.SetText(row.FeatureData.SelectSingleNode("@FeatureCode").InnerText);
            MessageBox.Show(row.FeatureData.SelectSingleNode("@FeatureCode").InnerText);
        }

        private void FillFeatureDefinition()
        {
            if (FeatureDefinition == null)
            {
                MsgBox.Show("FeatureDefinition ���J���ѡA�����v���޲z�Y�N�����C");
                this.Close();
            }

            foreach (XmlElement catalogElement in _feature_def.GetElements("FeatureCatalog"))
            {
                string path = catalogElement.GetAttribute("Path");
                if (!_mapDataGridView.ContainsKey(path)) continue;
                DataGridViewX dgv = _mapDataGridView[path] as DataGridViewX;
                dgv.SuspendLayout();
                FeatureCatalog catalog = new FeatureCatalog(catalogElement, dgv);
                _featureCatalogList.Add(catalog);
                foreach (string code in catalog.Features.Keys)
                    _features.Add(code, catalog.Features[code]);
                dgv.ResumeLayout();
            }
        }

        void RoleItem_DoubleClick(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                OpenViewer(sender as Role);
        }

        void OpenViewer(Role role)
        {
            this.Resize += new EventHandler(RoleManager_Resize);
            PermissionViewer viewer = new PermissionViewer(role, _features);
            viewer.Location = new Point(((this.Width - viewer.Width) / 2) - 8, ((this.Height - viewer.Height) / 2) - 18);
            viewer.Visible = true;
            Dictionary<Control, bool> ctrlList = new Dictionary<Control, bool>();
            foreach (Control var in this.Controls)
            {
                ctrlList.Add(var, var.Enabled);
                var.Enabled = false;
            }
            this.SuspendLayout();
            foreach (Control var in ctrlList.Keys)
                this.Controls.Remove(var);
            this.Controls.Add(viewer);
            foreach (Control var in ctrlList.Keys)
                this.Controls.Add(var);
            this.ResumeLayout();
            viewer.CtrlList = ctrlList;
            viewer.ViewerCloseButtonClick += new EventHandler(CloseViewer);
        }

        void RoleManager_Resize(object sender, EventArgs e)
        {
            if (this.Controls[0] is PermissionViewer)
            {
                this.SuspendLayout();
                PermissionViewer viewer = this.Controls[0] as PermissionViewer;
                viewer.Location = new Point(((this.Width - viewer.Width) / 2) - 8, ((this.Height - viewer.Height) / 2) - 18);
                this.ResumeLayout();
            }
        }

        void CloseViewer(object sender, EventArgs e)
        {
            this.Resize -= new EventHandler(RoleManager_Resize);
            PermissionViewer viewer = sender as PermissionViewer;
            foreach (Control var in viewer.CtrlList.Keys)
                var.Enabled = viewer.CtrlList[var];
            viewer.Visible = false;
            this.Controls.Remove(viewer);
        }

        void RoleItem_Click(object sender, EventArgs e)
        {
            Role role = sender as Role;

            if (_currentRole != null && _currentRole == role)
                return;

            _previousRoleName = role.RoleName;

            if (_currentRole != null && _valueManager.IsDirty)
            {
                ConfirmMsgBox confirmBox = new ConfirmMsgBox("", "�ϥΪ� " + txtRoleName.Text + " �v���w�ק�|���x�s\n�O�_�x�s�H", "�x�s", "���x�s", "����");
                confirmBox.Button1Click += new EventHandler(confirm_Button1Click);
                confirmBox.Button2Click += new EventHandler(confirm_Button2Click);
                confirmBox.Button3Click += new EventHandler(confirm_Button3Click);
                confirmBox.ShowDialog();
                if (confirmBox.DialogResult == ConfirmMsgBox.Result.Button3)
                    return;
            }

            if (_isRegisterPermissionChanged)
                foreach (FeatureCatalog catalog in _featureCatalogList)
                    catalog.PermissionChanged -= new EventHandler(catalog_PermissionChanged);
            _isRegisterPermissionChanged = false;

            #region ��J���⪺�v��
            foreach (RowFeature row in _features.Values)
                row.Permission = AccessOptions.None;

            foreach (FeatureAce ace in role.Acl)
                if (_features.ContainsKey(ace.FeatureCode))
                    _features[ace.FeatureCode].Permission = ace.Access;
            #endregion

            //�O�������v������l���A
            _valueManager.InitFeature(_features);

            foreach (FeatureCatalog catalog in _featureCatalogList)
                catalog.PermissionChanged += new EventHandler(catalog_PermissionChanged);
            _isRegisterPermissionChanged = true;

            _currentRole = role;
            txtRoleName.Text = role.RoleName;
        }

        void confirm_Button3Click(object sender, EventArgs e)
        {
            //����
            _currentRole.Click -= new EventHandler(RoleItem_Click);
            _currentRole.RaiseClick();
            _currentRole.Click += new EventHandler(RoleItem_Click);

            _previousRoleName = _currentRole.RoleName;
        }

        void confirm_Button2Click(object sender, EventArgs e)
        {
            //���x�s
            _currentRole.Text = txtRoleName.Text;
            buttonX2.Enabled = false;
        }

        void confirm_Button1Click(object sender, EventArgs e)
        {
            //�x�s
            Save();
        }

        private void Save()
        {
            if (_currentRole == null)
                return;

            DSXmlHelper helper = new DSXmlHelper("Permissions");
            foreach (RowFeature row in _features.Values)
            {
                if (row.Permission != AccessOptions.None)
                {
                    helper.AddElement("Feature");
                    helper.SetAttribute("Feature", "Code", row.FeatureCode);
                    helper.SetAttribute("Feature", "Permission", row.Permission.ToString());
                }
            }
            XmlElement element = helper.BaseElement;

            try
            {
                Feature.Security.UpdateRole(_currentRole.RoleID, element);
                MsgBox.Show("�x�s�����C");
            }
            catch (Exception ex)
            {
                //CurrentUser.ReportError(ex);
                MsgBox.Show("�x�s���ѡA���~�T���G" + ex.Message);
                return;
            }

            LoadRoles();
        }

        void catalog_PermissionChanged(object sender, EventArgs e)
        {
            if (_currentRole == null)
                return;

            _valueManager.SetFeature(_features);

            if (_valueManager.IsDirty)
            {
                StringBuilder text = new StringBuilder("");
                text.Append("<font color=\"#FF2020\">��</font>" + txtRoleName.Text);
                _currentRole.Text = text.ToString();
                buttonX2.Enabled = true;
            }
            else
            {
                _currentRole.Text = "<font>" + txtRoleName.Text + "</font>";
                buttonX2.Enabled = false;
            }
            itemPanel1.Refresh();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            RoleAddForm addForm = new RoleAddForm(itemPanel1);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadRoles();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DialogResult result = MsgBox.Show("�z�T�w�n�R������ " + txtRoleName.Text + " �ܡH", "�R������", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Feature.Security.DeleteRole(_currentRole.RoleID);

                }
                catch (Exception ex)
                {
                    //CurrentUser.ReportError(ex);
                    MsgBox.Show("�R�����ѡA���~�T���G" + ex.Message);
                }
                LoadRoles();
            }
        }

        private void txtRoleName_TextChanged(object sender, EventArgs e)
        {
            buttonX3.Enabled = true;
            //buttonX2.Enabled = true;
            tabControl1.Enabled = true;
            if (string.IsNullOrEmpty(txtRoleName.Text))
            {
                buttonX3.Enabled = false;
                buttonX2.Enabled = false;
                tabControl1.Enabled = false;
            }
        }
    }

    internal class Role : ButtonItem
    {
        public Role(string id, string name, FeatureAcl acl)
        {
            _id = id;
            _name = name;
            _acl = acl;
            Text = name;
        }

        private string _id;
        public string RoleID
        {
            get { return _id; }
        }

        private string _name;
        public string RoleName
        {
            get { return _name; }
        }

        private FeatureAcl _acl;
        public FeatureAcl Acl
        {
            get { return _acl; }
        }
    }

    internal class PermissionValueManager
    {
        private Dictionary<string, AccessOptions> _old = new Dictionary<string, AccessOptions>();
        private Dictionary<string, AccessOptions> _new = new Dictionary<string, AccessOptions>();

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

        public void InitFeature(Dictionary<string, RowFeature> features)
        {
            _old.Clear();
            _isDirty = false;
            foreach (string code in features.Keys)
            {
                _old.Add(code, features[code].Permission);
            }
        }

        public void SetFeature(Dictionary<string, RowFeature> features)
        {
            _new.Clear();
            foreach (string code in features.Keys)
            {
                _new.Add(code, features[code].Permission);
            }

            _isDirty = false;

            if (_old != null)
            {
                foreach (string code in _old.Keys)
                {
                    if (_old[code] != _new[code])
                    {
                        _isDirty = true;
                        break;
                    }
                }
            }
        }
    }


}