using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool;

namespace DataManager
{
    public partial class ModifyForm : BaseForm
    {
        private List<string> _list;
        private string _merit_flag;
        private DSXmlHelper _helper;

        public string NewReason
        {
            get { return txtNewReason.Text; }
        }

        public DSXmlHelper Helper
        {
            get { return _helper; }
        }

        public ModifyForm(DSXmlHelper helper, List<string> list)
        {
            InitializeComponent();
            _helper = helper;
            _list = list;

            _merit_flag = helper.GetText("MeritFlag");
            if (_merit_flag == "1")
            {
                lblA.Text = "大功";
                lblB.Text = "小功";
                lblC.Text = "嘉獎";
                txtA.Text = helper.GetText("Detail/Discipline/Merit/@A");
                txtB.Text = helper.GetText("Detail/Discipline/Merit/@B");
                txtC.Text = helper.GetText("Detail/Discipline/Merit/@C");
            }
            else if (_merit_flag == "0")
            {
                lblA.Text = "大過";
                lblB.Text = "小過";
                lblC.Text = "警告";
                txtA.Text = helper.GetText("Detail/Discipline/Demerit/@A");
                txtB.Text = helper.GetText("Detail/Discipline/Demerit/@B");
                txtC.Text = helper.GetText("Detail/Discipline/Demerit/@C");
            }
            else if (_merit_flag == "2")
            {
                lblA.Enabled = lblB.Enabled = lblC.Enabled = false;
                txtA.Enabled = txtB.Enabled = txtC.Enabled = false;
            }
            txtNewReason.Text = helper.GetText("Reason");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateDisciplineCount()) return;

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Discipline");
            helper.AddElement("Discipline", "Field");
            helper.AddElement("Discipline/Field", "Reason", txtNewReason.Text);

            if (_merit_flag == "1")
            {
                _helper.SetText("Detail/Discipline/Merit/@A", txtA.Text);
                _helper.SetText("Detail/Discipline/Merit/@B", txtB.Text);
                _helper.SetText("Detail/Discipline/Merit/@C", txtC.Text);
            }
            else if (_merit_flag == "0")
            {
                _helper.SetText("Detail/Discipline/Demerit/@A", txtA.Text);
                _helper.SetText("Detail/Discipline/Demerit/@B", txtB.Text);
                _helper.SetText("Detail/Discipline/Demerit/@C", txtC.Text);
            }
            _helper.SetText("Reason", txtNewReason.Text);

            helper.AddElement("Discipline/Field", _helper.GetElement("Detail"));
            helper.AddElement("Discipline", "Condition");
            foreach (string id in _list)
            {
                helper.AddElement("Discipline/Condition", "ID", id);
            }

            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(helper));
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show("更改事由失敗。");
            }

            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateDisciplineCount()
        {
            bool valid = true;
            errorProvider.Clear();
            int v;
            if (!int.TryParse(txtA.Text, out v))
            {
                errorProvider.SetIconAlignment(txtA, ErrorIconAlignment.MiddleLeft);
                errorProvider.SetError(txtA, "必須為數字");
                valid = false;
            }
            if (!int.TryParse(txtB.Text, out v))
            {
                errorProvider.SetIconAlignment(txtB, ErrorIconAlignment.MiddleLeft);
                errorProvider.SetError(txtB, "必須為數字");
                valid = false;
            }
            if (!int.TryParse(txtC.Text, out v))
            {
                errorProvider.SetIconAlignment(txtC, ErrorIconAlignment.MiddleLeft);
                errorProvider.SetError(txtC, "必須為數字");
                valid = false;
            }
            return valid;
        }
    }
}