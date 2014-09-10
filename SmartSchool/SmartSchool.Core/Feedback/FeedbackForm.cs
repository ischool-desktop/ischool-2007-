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
using System.IO;
using Aspose.Cells;

namespace SmartSchool.Feedback
{
    internal partial class FeedbackForm : BaseForm
    {
        private Image _img = null;

        private Mode _mode = Mode.User;
        public Mode CurrentMode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        private string _user = "";
        private string _access_point = "";

        private bool _loading = false;

        public FeedbackForm()
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
                userPanel.Enabled = false;
                userPanel.Visible = false;
                adminPanel.Enabled = true;
                adminPanel.Visible = true;
                MaximizeBox = true;
                if (DSA.Instance.IsDev) this.Text += " (測試用)";
            }
            else if (CurrentMode == Mode.User)
            {
                this.Size = new Size(520, 600);
                this.MaximumSize = new Size(520, 600);
                this.MinimumSize = new Size(520, 600);
                _user = CurrentUser.Instance.UserName.ToUpper();
                _access_point = CurrentUser.Instance.AccessPoint;
            }
        }

        private void FeedbackForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (CurrentMode == Mode.User)
                {
                    GetImageFromClipboard();
                }
                else if (CurrentMode == Mode.Admin)
                {
                    LoadFeedback(DateTime.Today.AddDays(-7), DateTime.Today);
                    txtStartDate.Text = DateTime.Today.AddDays(-7).ToString("yyyy/MM/dd");
                    txtEndDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
                    //dgFeedback.ClearSelection();
                }
                else
                    throw new Exception("你這是什麼模式？");

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void ResizePicture(PictureBox pic, Image img)
        {
            if (img.Width < pic.Width && img.Height < pic.Height)
            {
                pic.Padding = new Padding((int)((pic.Width - 4 - img.Width) / 2), (int)((pic.Height - 4 - img.Height) / 2), 0, 0);
                pic.Image = img;
            }
            else
            {
                Bitmap bmp;
                double rate_w = (double)img.Width / (double)pic.Width;
                double rate_h = (double)img.Height / (double)pic.Height;
                if (rate_w > rate_h)
                {
                    int height = (int)Math.Ceiling(img.Height / rate_w);
                    bmp = new Bitmap(img, pic.Width - 4, height);
                    pic.Padding = new Padding(0, (int)((pic.Height - 4 - height) / 2), 0, 0);
                }
                else if (rate_w < rate_h)
                {
                    int width = (int)Math.Ceiling(img.Width / rate_h);
                    bmp = new Bitmap(img, width, pic.Height - 4);
                    pic.Padding = new Padding((int)((pic.Width - 4 - width) / 2), 0, 0, 0);
                }
                else
                    bmp = new Bitmap(img, pic.Width - 4, pic.Height - 4);
                pic.Image = bmp;
            }
        }

        #region User side

        private void btnClearPicture_Click(object sender, EventArgs e)
        {
            picUploadImage.Image = null;
            _img = null;
        }

        private void GetImageFromClipboard()
        {
            _img = Clipboard.GetImage();
            if (_img == null)
            {
                picUploadImage.Image = null;
                return;
            }

            ResizePicture(picUploadImage, _img);
            //Bitmap bmp = new Bitmap(_img, picUploadImage.Width, picUploadImage.Height);
            //picUploadImage.Image = bmp;
        }

        private void picUploadImage_DoubleClick(object sender, EventArgs e)
        {
            if (_img == null)
                return;

            PictureViewer viewer = new PictureViewer(_img);
            viewer.ShowDialog();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDesc.Text))
                return;

            #region Image 轉成 base64 字串

            string base64string = "";
            if (_img != null)
            {
                MemoryStream stream = new MemoryStream();
                _img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] buffer = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(buffer, 0, (int)stream.Length);

                base64string = Convert.ToBase64String(buffer);
            }

            #endregion

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement(".", "AccessPoint", CurrentUser.Instance.AccessPoint);
            helper.AddElement(".", "School", CurrentUser.Instance.SchoolChineseName);
            helper.AddElement(".", "User", CurrentUser.Instance.UserName);

            helper.AddElement(".", "Text", txtDesc.Text);
            helper.AddElement(".", "Image", base64string);

            try
            {
                Service.InsertFeedback(new DSRequest(helper));
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }

            this.Close();
        }

        private void btnRePaste_Click(object sender, EventArgs e)
        {
            GetImageFromClipboard();
        }

        private void btnFromFile_Click(object sender, EventArgs e)
        {
            //點陣圖檔案(*.bmp)|*.bmp|JPEG (*.jpg;*.jpeg;*.jpe)|*.jpg;*.jpeg;*.jpe|GIF (*.gif)|*.gif|PNG (*.png)|*.png|所有圖片檔案|*.bmp;*.jpg;*.jpeg;*.jpe;*.gif;*.png|所有檔案|*.*
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                Image orig = new Bitmap(openFileDialog1.FileName);
                _img = orig;
                ResizePicture(picUploadImage, orig);
                //Bitmap resize = new Bitmap(orig, picUploadImage.Width, picUploadImage.Height);
                //picUploadImage.Image = resize;
            }
            catch (ArgumentException ex)
            {
                MsgBox.Show("您選擇的檔案不是圖片檔案");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            userPanel.Enabled = false;
            userPanel.Visible = false;
            adminPanel.Enabled = true;
            adminPanel.Visible = true;
            MaximizeBox = true;
            colSchool.Visible = false;
            colUser.Visible = false;
            colComments.Visible = false;
            contextMenuBar2.SetContextMenuEx(dgFeedback, null);
            this.MaximumSize = new Size(0, 0);
            this.MinimumSize = new Size(0, 0);
            this.Size = new Size(800, 625);
            this.ResumeLayout();

            LoadFeedback(DateTime.Today.AddDays(-7), DateTime.Today);
            txtStartDate.Text = DateTime.Today.AddDays(-7).ToString("yyyy/MM/dd");
            txtEndDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
        }

        #endregion

        #region Admin side

        private void LoadFeedback(DateTime start, DateTime end)
        {
            LoadFeedback(start, end, _user, _access_point);
        }

        private void LoadFeedback(DateTime start, DateTime end, string user, string access_point)
        {
            _loading = true;
            dgFeedback.Rows.Clear();
            dgFeedback.SuspendLayout();

            DSXmlHelper helper;
            try
            {
                helper = Service.GetFeedback(GetHelper(start, end, user, access_point));
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                helper = new DSXmlHelper("BOOM");
            }
            foreach (XmlElement fb in helper.GetElements("Feedback"))
            {
                DSXmlHelper fbHelper = new DSXmlHelper(fb);

                #region base64 轉成 Image

                string base64string = fbHelper.GetText("Image");
                Image img = null;
                if (!string.IsNullOrEmpty(base64string))
                {
                    try
                    {
                        byte[] buffer = Convert.FromBase64String(base64string);
                        MemoryStream stream = new MemoryStream(buffer);
                        img = new Bitmap(stream);
                    }
                    catch (Exception ex)
                    {
                        img = null;
                    }
                }

                #endregion

                #region 調整時間格式

                string posttime = fbHelper.GetText("PostTime");
                DateTime try_time;
                if (DateTime.TryParse(posttime, out try_time))
                    posttime = try_time.ToShortDateString() + " " + try_time.ToShortTimeString();

                #endregion

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgFeedback,
                    fbHelper.GetText("@ID"),
                    fbHelper.GetText("AccessPoint"),
                    fbHelper.GetText("School"),
                    fbHelper.GetText("User"),
                    fbHelper.GetText("Text"),
                    posttime);
                dgFeedback.Rows.Add(row);
                row.Tag = img;
                row.Cells[colDesc.Index].Tag = fbHelper.GetText("Text");

                #region Comments
                XmlElement comments = fbHelper.GetElement("Comments");
                if (comments != null)
                {
                    DSXmlHelper cmtHelper = new DSXmlHelper(comments);
                    row.Cells[colComments.Index].Tag = cmtHelper;
                    row.Cells[colComments.Index].Value = cmtHelper.GetText("Status");
                }
                #endregion
            }

            dgFeedback.ResumeLayout();
            dgFeedback.ClearSelection();
            _loading = false;
        }

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            if (_loading || dgFeedback.SelectedRows.Count <= 0) return;

            DataGridViewRow row = dgFeedback.SelectedRows[0];
            txtPreview.Text = "" + row.Cells[colDesc.Index].Tag;
            txtPreview.Text = txtPreview.Text.Replace("\n", "\r\n");

            if (row.Tag != null && row.Tag is Image)
            {
                ResizePicture(picPreview, (row.Tag as Image));
                picPreview.Tag = row.Tag as Image;
            }
            else
            {
                picPreview.Image = null;
                picPreview.Tag = null;
            }
        }

        private void picPreview_DoubleClick(object sender, EventArgs e)
        {
            if (picPreview.Tag == null)
                return;

            PictureViewer viewer = new PictureViewer(picPreview.Tag as Image);
            viewer.ShowDialog();
        }

        private void FeedbackForm_Resize(object sender, EventArgs e)
        {
            picPreview.SuspendLayout();
            if (picPreview.Tag != null && picPreview.Tag is Image)
            {
                ResizePicture(picPreview, (picPreview.Tag as Image));
                //Bitmap bmp = new Bitmap((picPreview.Tag as Image), picPreview.Width, picPreview.Height);
                //picPreview.Image = bmp;
            }
            picPreview.ResumeLayout();
        }

        private void expandableSplitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            picPreview.SuspendLayout();
            if (picPreview.Tag != null && picPreview.Tag is Image)
            {
                ResizePicture(picPreview, (picPreview.Tag as Image));
                //Bitmap bmp = new Bitmap((picPreview.Tag as Image), picPreview.Width, picPreview.Height);
                //picPreview.Image = bmp;
            }
            picPreview.ResumeLayout();
        }

        private void txtPreview_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyValue == 187 && txtPreview.Font.Size < 100)
                    txtPreview.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, txtPreview.Font.Size + 2);
                else if (e.KeyValue == 189 && txtPreview.Font.Size > 0)
                    txtPreview.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, txtPreview.Font.Size - 2);
            }
        }

        private void btnSendNews_Click(object sender, EventArgs e)
        {
            if (dgFeedback.SelectedRows.Count <= 0) return;

            List<string> user_list = new List<string>();
            foreach (DataGridViewRow row in dgFeedback.SelectedRows)
            {
                string user = "" + row.Cells[colAccessPoint.Index].Value + "/" + row.Cells[colUser.Index].Value;
                if (!user_list.Contains(user))
                    user_list.Add(user);
            }
            NewsCreator creator = new NewsCreator(user_list);
            creator.Show();
        }

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DateTime try_start, try_end;
            DateTime.TryParse(txtStartDate.Text, out try_start);
            DateTime.TryParse(txtEndDate.Text, out try_end);
            LoadFeedback(try_start, try_end);
        }

        private DSXmlHelper GetHelper(DateTime start, DateTime end, string user, string access_point)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("All");
            helper.AddElement("Condition");
            if (start > DateTime.MinValue) helper.AddElement("Condition", "StartDate", start.ToString("yyyy-MM-dd"));
            if (end > DateTime.MinValue) helper.AddElement("Condition", "EndDate", end.AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(user)) helper.AddElement("Condition", "User", user);
            if (!string.IsNullOrEmpty(access_point)) helper.AddElement("Condition", "AccessPoint", access_point);
            return helper;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Workbook wb = new Workbook();
            wb.Worksheets.Clear();
            Worksheet ws = wb.Worksheets[wb.Worksheets.Add()];

            int headerIndex = 0, descIndex = 0;
            ws.Cells[0, headerIndex++].PutValue("ID");
            ws.Cells[0, headerIndex++].PutValue("AccessPoint");
            ws.Cells[0, headerIndex++].PutValue("學校名稱");
            ws.Cells[0, headerIndex++].PutValue("使用者帳號");
            descIndex = headerIndex;
            ws.Cells[0, headerIndex++].PutValue("描述");
            ws.Cells[0, headerIndex++].PutValue("張貼時間");

            foreach (DataGridViewRow row in dgFeedback.Rows)
            {
                int col_index = 0, row_index = row.Index + 1;
                ws.Cells[row_index, col_index++].PutValue("" + row.Cells[colID.Index].Value);
                ws.Cells[row_index, col_index++].PutValue("" + row.Cells[colAccessPoint.Index].Value);
                ws.Cells[row_index, col_index++].PutValue("" + row.Cells[colSchool.Index].Value);
                ws.Cells[row_index, col_index++].PutValue("" + row.Cells[colUser.Index].Value);
                ws.Cells[row_index, col_index++].PutValue("" + row.Cells[colDesc.Index].Value);
                ws.Cells[row_index, col_index++].PutValue("" + row.Cells[colPostTime.Index].Value);
            }

            ws.AutoFitColumns();
            ws.Cells.SetColumnWidth(descIndex, 50);

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    wb.Save(saveFileDialog1.FileName, FileFormatType.Excel2003);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("匯出失敗。" + ex.Message);
                }
            }
        }

        private void dgFeedback_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (CurrentMode == Mode.User) return;

            DataGridViewRow row = dgFeedback.Rows[e.RowIndex];
            string id = "" + row.Cells[colID.Index].Value;
            DSXmlHelper helper;
            if (row.Cells[colComments.Index].Tag != null)
                helper = row.Cells[colComments.Index].Tag as DSXmlHelper;
            else
                helper = new DSXmlHelper("Comments");
            
            CommentEditor editor = new CommentEditor(helper, id);
            if (editor.ShowDialog() == DialogResult.OK)
                btnRefresh_Click(null, null);
        }
    }
}