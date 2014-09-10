using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feedback.Feature;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Feedback
{
    internal partial class NewsForm : BaseForm
    {
        private Mode _mode = Mode.User;
        public Mode CurrentMode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        //private DateTime _serverDate = DateTime.MinValue;
        //private DateTime _lastViewTime = DateTime.MinValue;

        public NewsForm()
        {
            InitializeComponent();

            if (Control.ModifierKeys == Keys.Shift)
            {
                AuthBox auth = new AuthBox();
                if (auth.ShowDialog() == DialogResult.OK)
                    CurrentMode = Mode.Admin;
            }

            AdjustUI();
        }

        private void AdjustUI()
        {
            if (CurrentMode == Mode.Admin)
            {
                dgNewsList.Size = new Size(785, 365);
                btnAddNews.Enabled = true;
                btnAddNews.Visible = true;
                colID.Visible = true;
                colUser.Visible = true;
                if (DSA.Instance.IsDev) this.Text += " (測試用)";
            }
            else
            {

            }
        }

        private void NewsForm_Load(object sender, EventArgs e)
        {
            LoadNews();
        }

        private void LoadNews()
        {
            dgNewsList.Rows.Clear();
            dgNewsList.SuspendLayout();

            DSXmlHelper helper;
            try
            {
                if (CurrentMode == Mode.Admin)
                    helper = Service.GetNews();
                else
                    helper = Service.GetNewsForUsers();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                helper = new DSXmlHelper("BOOM");
            }

            foreach (XmlElement news in helper.GetElements("News"))
            {
                DSXmlHelper newsHelper = new DSXmlHelper(news);

                string user = "";
                if (CurrentMode == Mode.Admin)
                {
                    foreach (XmlElement each_user in newsHelper.GetElements("To/To/User"))
                    {
                        if (!string.IsNullOrEmpty(user)) user += ", ";
                        user += each_user.InnerText;
                    }
                }
                else if(IsIncorrect(newsHelper.GetText("To")))
                    continue;

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgNewsList,
                    newsHelper.GetText("@ID"),
                    user,
                    newsHelper.GetText("Message"),
                    string.IsNullOrEmpty(newsHelper.GetText("Url")) ? "" : "檢視",
                    newsHelper.GetText("PostTime"));
                dgNewsList.Rows.Add(row);
                row.Tag = newsHelper;
                row.Cells[colUrl.Name].Tag = newsHelper.GetText("Url");
            }

            dgNewsList.ResumeLayout();
        }

        private bool IsIncorrect(string user)
        {
            CurrentUser current = CurrentUser.Instance;
            if (user == "*/*") return false;
            if (user == current.AccessPoint + "/*") return false;
            if (user == current.AccessPoint + "/" + current.UserName) return false;
            return true;
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgNewsList.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null)
            {
                if (dgNewsList.Columns[e.ColumnIndex] == colUrl)
                {
                    string url = dgNewsList.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
                    try
                    {
                        System.Diagnostics.Process.Start(url);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                    //UrlBrowser browser = new UrlBrowser(url);
                    //browser.ShowDialog();
                }
            }
        }

        private void btnAddNews_Click(object sender, EventArgs e)
        {
            NewsCreator creator = new NewsCreator();
            if (creator.ShowDialog() == DialogResult.OK)
            {
                LoadNews();
            }
        }

        private void dgNewsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (CurrentMode == Mode.Admin && dgNewsList.Rows[e.RowIndex].Tag != null)
            {
                NewsCreator creator = new NewsCreator(dgNewsList.Rows[e.RowIndex].Tag as DSXmlHelper);
                if (creator.ShowDialog() == DialogResult.OK)
                {
                    LoadNews();
                }
            }
        }
    }
}