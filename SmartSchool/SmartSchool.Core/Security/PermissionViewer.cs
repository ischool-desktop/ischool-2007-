using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using SmartSchool.AccessControl;

namespace SmartSchool.Security
{
    internal partial class PermissionViewer : UserControl
    {
        public event EventHandler ViewerCloseButtonClick;
        private Role _role;
        private Dictionary<string, RowFeature> _features;

        private Dictionary<Control, bool> _list;
        public Dictionary<Control, bool> CtrlList
        {
            get
            {
                if (_list == null)
                    return new Dictionary<Control, bool>();
                return _list;
            }
            set { _list = value; }
        }

        public PermissionViewer(Role role, Dictionary<string, RowFeature> features)
        {
            InitializeComponent();
            _role = role;
            _features = features;
        }

        private void PermissionViewer_Load(object sender, EventArgs e)
        {
            foreach (FeatureAce ace in _role.Acl)
            {
                int index = dataGridViewX1.Rows.Add();
                DataGridViewRow row = dataGridViewX1.Rows[index];
                row.Cells[colCode.Name].Value = ace.FeatureCode;
                row.Cells[colPath.Name].Value = _features[ace.FeatureCode].Tag as string;
                row.Cells[colTitle.Name].Value = _features[ace.FeatureCode].Cells[0].Value.ToString();
                row.Cells[colPermission.Name].Value = ace.Access.ToString();
            }

            labelX1.Text = _role.RoleName;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (ViewerCloseButtonClick != null)
                ViewerCloseButtonClick.Invoke(this, new EventArgs());
        }
    }
}
