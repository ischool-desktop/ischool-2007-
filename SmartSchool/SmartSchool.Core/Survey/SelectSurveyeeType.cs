using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.Survey
{
    public partial class SelectSurveyeeType : BaseForm
    {
        public SelectSurveyeeType(params SurveyeeTypeItem[] items)
        {
            InitializeComponent();

            cboSurveyeeType.Items.Clear();

            if (items.Length <= 0) return;

            cboSurveyeeType.Items.AddRange(items);
            cboSurveyeeType.ValueMember = "Value";
            cboSurveyeeType.DisplayMember = "Name";
            lblDesc.Text = string.Empty;
        }

        private void cboSurveyeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SurveyeeTypeItem item = (cboSurveyeeType.SelectedItem as SurveyeeTypeItem);
            _selected_value = item.Value;
            lblDesc.Text = item.Description;
        }

        private SurveyeeType _selected_value;
        public SurveyeeType SelectedValue
        {
            get { return _selected_value; }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cboSurveyeeType.SelectedItem == null)
            {
                MsgBox.Show("請選擇問卷調查對象。");
                DialogResult = DialogResult.None;
            }
            else
                DialogResult = DialogResult.OK;
        }
    }
}