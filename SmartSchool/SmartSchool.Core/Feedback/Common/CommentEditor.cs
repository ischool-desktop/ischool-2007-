using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feedback.Feature;

namespace SmartSchool.Feedback
{
    public partial class CommentEditor : BaseForm
    {
        private DSXmlHelper _helper;
        private string _id;

        public CommentEditor(DSXmlHelper helper, string id)
        {
            InitializeComponent();
            _helper = helper;
            _id = id;
        }

        private void CommentEditor_Load(object sender, EventArgs e)
        {
            txtStatus.Text = _helper.GetText("Status");
            txtProcedure.Text = _helper.GetText("Procedure");
            txtProcedure.Text = txtProcedure.Text.Replace("\n", "\r\n");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!_helper.PathExist("Procedure")) _helper.AddElement("Procedure");
            if (!_helper.PathExist("Status")) _helper.AddElement("Status");
            _helper.SetText("Procedure", txtProcedure.Text);
            _helper.SetText("Status", txtStatus.Text);

            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("Comments");
            req.AddElement("Comments", _helper.BaseElement);
            req.AddElement("Condition");
            req.AddElement("Condition", "ID", _id);

            try
            {
                Service.UpdateFeedback(new DSRequest(req));
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}