using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.TagManage;
using SmartSchool.Feature.Tag;

namespace SmartSchool.StudentRelated
{
    public partial class StudentTagEditor : BaseForm
    {
        private enum ManageMode
        {
            Update, Insert
        }

        private ManageMode _mode;
        private TagInfo _current_tag;
        private TagManager _manager;

        public StudentTagEditor()
        {
            InitializeComponent();

            _mode = ManageMode.Insert;
            _manager = Student.Instance.TagManager;
        }

        public StudentTagEditor(TagInfo tag)
        {
            InitializeComponent();

            _mode = ManageMode.Update;
            _current_tag = tag;
            _manager = Student.Instance.TagManager;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    MsgBox.Show("您必須輸入類別名稱。");
                    DialogResult = DialogResult.None;
                    return;
                }

                string prefix = cboGroups.Text.Trim();
                if (_mode == ManageMode.Insert)
                {
                    foreach (TagInfo each in _manager.Tags.Values)
                    {
                        if (each.Name == txtName.Text.Trim() && each.Prefix == prefix)
                        {
                            MsgBox.Show("名稱重覆，請選擇其他名稱。");
                            DialogResult = DialogResult.None;
                            return;
                        }
                    }

                    _manager.Insert(new TagInfo(prefix, txtName.Text.Trim(), cpColor.SelectedColor));
                }
                else
                {
                    _current_tag.Prefix = prefix;
                    _current_tag.Name = txtName.Text.Trim();
                    _current_tag.Color = cpColor.SelectedColor;

                    _manager.Update(_current_tag);
                    Student.Instance.InvokTagDefinitionChanged(_current_tag.Identity);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void TagInsert_Load(object sender, EventArgs e)
        {
            if (_mode == ManageMode.Insert)
                Text = "新增類別";
            else
                Text = "修改類別";

            cboGroups.Text = string.Empty;
            txtName.Text = string.Empty;
            cpColor.SelectedColor = Color.White;

            InitGroupCombobox();

            if (_mode == ManageMode.Insert) return; //_current_item 是 null 代表是使用「Insert」模式。

            int group_index = cboGroups.FindString(_current_tag.Prefix);

            if (string.IsNullOrEmpty(_current_tag.Prefix)) //如果 Prefix 是空字串的話，就不要選擇任何項目，因為這是為分類的 Prefix。
                group_index = -1;

            if (group_index != -1)
                cboGroups.SelectedIndex = group_index;

            txtName.Text = _current_tag.Name;

            cpColor.SelectedColor = _current_tag.Color;
        }

        private void InitGroupCombobox()
        {
            cboGroups.SelectedItem = null;
            cboGroups.Items.Clear();
            PrefixCollection prefixes = _manager.Prefixes;
            foreach (Prefix each in prefixes.Values)
            {
                if (string.IsNullOrEmpty(each.Name)) continue;
                cboGroups.Items.Add(each.Name);
            }
        }

        public static DialogResult ModifyTag(TagInfo tag)
        {
            StudentTagEditor editor = new StudentTagEditor(tag);

            return editor.ShowDialog();
        }

        public static DialogResult InsertTag()
        {
            StudentTagEditor editor = new StudentTagEditor();

            return editor.ShowDialog();
        }

        private void cpColor_LocalizeString(object sender, DevComponents.DotNetBar.LocalizeEventArgs e)
        {
            switch (e.Key)
            {
                case "colorpicker_themecolorslabel":
                    e.LocalizedValue = "佈景主題色彩";
                    e.Handled = true;
                    break;
                case "colorpicker_standardcolorslabel":
                    e.LocalizedValue = "標準色彩";
                    e.Handled = true;
                    break;
                case "colorpicker_morecolors":
                    e.LocalizedValue = "其他色彩";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_okbutton":
                    e.LocalizedValue = "確定";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_cancelbutton":
                    e.LocalizedValue = "取消";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_newcolorlabel":
                    e.LocalizedValue = "新增";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_currentcolorlabel":
                    e.LocalizedValue = "目前";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_standardcolorslabel":
                    e.LocalizedValue = "色彩(&C)";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_customcolorslabel":
                    e.LocalizedValue = "色彩(&C)";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_greenlabel":
                    e.LocalizedValue = "綠色(&G):";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_bluelabel":
                    e.LocalizedValue = "藍色(&B):";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_redlabel":
                    e.LocalizedValue = "紅色(&R):";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_tabstandard":
                    e.LocalizedValue = "標準";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_tabcustom":
                    e.LocalizedValue = "自訂";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_caption":
                    e.LocalizedValue = "色彩";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_colormodellabel":
                    e.LocalizedValue = "色彩模式(&D)";
                    e.Handled = true;
                    break;
                case "colorpickerdialog_rgblabel":
                    e.LocalizedValue = "RGB三原色";
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
    }
}