using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using CodeCollection = SmartSchool.Others.Configuration.WordCommentMapping.TextCommentForm.CodeItemCollection;
using DevComponents.Editors;

namespace SmartSchool.Others.Configuration.WordCommentMapping
{
    public partial class AddTextCommentFace : BaseForm
    {
        private List<string> _check_list;

        /// <summary>
        /// 用於改名。
        /// </summary>
        /// <param name="face"></param>
        /// <param name="checkList"></param>
        public AddTextCommentFace(string face, IEnumerable<string> checkList)
        {
            InitializeComponent();

            txtItemName.Text = face;
            cboNames.Enabled = false;

            _check_list = new List<string>();
            _check_list.AddRange(checkList);
        }

        /// <summary>
        /// 用於新增。
        /// </summary>
        /// <param name="copyList"></param>
        public AddTextCommentFace(Dictionary<string, CodeCollection> copyList)
        {
            InitializeComponent();

            _check_list = new List<string>();
            _check_list.AddRange(copyList.Keys);

            cboNames.Items.Clear();
            foreach (KeyValuePair<string, CodeCollection> each in copyList)
            {
                ComboItem item = new ComboItem();
                item.Text = each.Key;
                item.Tag = each.Value;

                cboNames.Items.Add(item);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (_check_list.Contains(txtItemName.Text))
            {
                MsgBox.Show("名稱重覆。");
                DialogResult = DialogResult.None;
                return;
            }

            DialogResult = DialogResult.OK;
        }

        public string NewName
        {
            get { return txtItemName.Text; }
        }

        public ComboItem SelectedItem
        {
            get { return cboNames.SelectedItem as ComboItem; }
        }
    }
}