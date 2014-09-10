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
using System.Xml;

namespace SmartSchool.Feedback
{
    public partial class NewsCreator : BaseForm
    {
        private string _news_id;

        public NewsCreator()
        {
            InitializeComponent();
        }

        public NewsCreator(DSXmlHelper helper) : this()
        {
            _news_id = helper.GetText("@ID");
            txtMessage.Text = helper.GetText("Message");
            txtUrl.Text = helper.GetText("Url");

            foreach (XmlElement each_user in helper.GetElements("To/To/User"))
            {
                if (!string.IsNullOrEmpty(txtUsers.Text)) txtUsers.Text += ", ";
                txtUsers.Text += each_user.InnerText;
            }

            btnAnnounce.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        public NewsCreator(List<string> user_list) : this()
        {
            foreach (string user in user_list)
            {
                if (!string.IsNullOrEmpty(txtUsers.Text)) txtUsers.Text += ", ";
                txtUsers.Text += user;
            }
        }

        private void btnAnnounce_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtUsers.Text))
                errorProvider1.SetError(labelX1, "不可空白");
            if (string.IsNullOrEmpty(txtMessage.Text))
                errorProvider1.SetError(labelX2, "不可空白");
            if (errorProvider1.HasError)
                return;

            string[] each_user = txtUsers.Text.Split(',');
            DSXmlHelper userHelper = new DSXmlHelper("To");
            foreach (string each in each_user)
                userHelper.AddElement(".", "User", each.Trim());

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement(".", "To", userHelper.GetRawXml(), true);
            helper.AddElement(".", "Message", txtMessage.Text);
            helper.AddElement(".", "Url", txtUrl.Text);

            try
            {
                Service.InsertNews(new DSRequest(helper));
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MessageBox.Show(ex.Message);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtUsers.Text))
                errorProvider1.SetError(labelX1, "不可空白");
            if (string.IsNullOrEmpty(txtMessage.Text))
                errorProvider1.SetError(labelX2, "不可空白");
            if (errorProvider1.HasError)
                return;

            string[] each_user = txtUsers.Text.Split(',');
            DSXmlHelper userHelper = new DSXmlHelper("To");
            foreach (string each in each_user)
                userHelper.AddElement(".", "User", each.Trim());

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement(".", "To", userHelper.GetRawXml(), true);
            helper.AddElement(".", "Message", txtMessage.Text);
            helper.AddElement(".", "Url", txtUrl.Text);
            helper.AddElement("Condition");
            helper.AddElement("Condition", "ID", _news_id);

            try
            {
                Service.UpdateNews(new DSRequest(helper));
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MessageBox.Show(ex.Message);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Service.DeleteNews(_news_id);
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MessageBox.Show(ex.Message);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}