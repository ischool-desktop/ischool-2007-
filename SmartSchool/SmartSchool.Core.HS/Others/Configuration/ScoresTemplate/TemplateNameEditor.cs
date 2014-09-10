using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.Others.Configuration.ScoresTemplate
{
    internal partial class TemplateNameEditor : BaseForm
    {
        private TemplateManager _manager;
        private string _origin_name;

        public TemplateNameEditor(TemplateManager manager, List<Template> templates)
        {
            InitializeComponent();
            
            _manager = manager;
            _origin_name = string.Empty;
            Text = "新增評分樣版";

            foreach (Template tpl in templates)
                cboExistTemplates.Items.Add(tpl);
            cboExistTemplates.SelectedIndex = 0;
        }

        public TemplateNameEditor(TemplateManager manager, string oldName)
        {
            InitializeComponent();

            _manager = manager;
            _origin_name = oldName;
            txtTemplateName.Text = oldName;
            txtTemplateName.SelectAll();

            Text = "重新命名評分樣版";
            lblCopyExist.Enabled = lblCopyExist.Visible = false;
            cboExistTemplates.Enabled = cboExistTemplates.Visible = false;
            Size = new Size(373, 104);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTemplateName.Text.Trim()))
            {
                MsgBox.Show("您必須輸入名稱。");
                DialogResult = DialogResult.None;
                return;
            }

            if (_manager.ContainsTemplateName(txtTemplateName.Text.Trim()))
            {
                if (txtTemplateName.Text.Trim() != _origin_name)
                {
                    MsgBox.Show("名稱重覆，請選擇其他名稱。");
                    DialogResult = DialogResult.None;
                    txtTemplateName.SelectAll();
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        public string TemplateName
        {
            get { return txtTemplateName.Text; }
        }

        public Template ExistTemplate
        {
            get
            {
                if (cboExistTemplates.SelectedItem is Template)
                    return cboExistTemplates.SelectedItem as Template;
                return null;
            }
        }

        private void TemplateNameEditor_Load(object sender, EventArgs e)
        {

        }
    }
}