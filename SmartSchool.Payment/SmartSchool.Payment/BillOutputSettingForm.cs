using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using DevComponents.DotNetBar.Controls;
using System.IO;

namespace SmartSchool.Payment
{
    public partial class BillOutputSettingForm : BaseForm
    {
        public BillOutputSettingForm()
        {
            InitializeComponent();
        }

        public string OutputFolder
        {
            get { return txtSourceFile.Text; }
        }

        private string _split_xpath;
        public string SplitXPath
        {
            get { return _split_xpath; }
        }

        public bool OnlyPreview
        {
            get { return chkPreview.Checked; }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.None;

                foreach (Control each in gpSplitField.Controls)
                {
                    CheckBoxX chk = each as CheckBoxX;
                    if (chk != null)
                    {
                        if (chk.Checked)
                            _split_xpath = chk.CheckValue.ToString();
                    }
                }

                if (string.IsNullOrEmpty(txtSourceFile.Text.Trim()))
                {
                    MsgBox.Show("請選擇繳費單的儲存目錄。");
                    return;
                }

                if (!Directory.Exists(txtSourceFile.Text))
                {
                    MsgBox.Show("您選擇的目錄並不存在。");
                    return;
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (dialog.ShowDialog() == DialogResult.OK)
                txtSourceFile.Text = dialog.SelectedPath;
        }

        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            //gpSplitField.Enabled = (!chkPreview.Checked);
        }
    }
}