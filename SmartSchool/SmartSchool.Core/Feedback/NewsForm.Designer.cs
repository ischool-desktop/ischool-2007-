namespace SmartSchool.Feedback
{
    partial class NewsForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgNewsList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrl = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colPostTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddNews = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgNewsList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgNewsList
            // 
            this.dgNewsList.AllowUserToAddRows = false;
            this.dgNewsList.AllowUserToDeleteRows = false;
            this.dgNewsList.AllowUserToResizeRows = false;
            this.dgNewsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgNewsList.BackgroundColor = System.Drawing.Color.White;
            this.dgNewsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNewsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colUser,
            this.colMsg,
            this.colUrl,
            this.colPostTime});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgNewsList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgNewsList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgNewsList.Location = new System.Drawing.Point(4, 6);
            this.dgNewsList.Name = "dgNewsList";
            this.dgNewsList.RowHeadersWidth = 25;
            this.dgNewsList.RowTemplate.Height = 24;
            this.dgNewsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNewsList.Size = new System.Drawing.Size(785, 395);
            this.dgNewsList.TabIndex = 0;
            this.dgNewsList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgNewsList_CellDoubleClick);
            this.dgNewsList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellContentClick);
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            this.colID.Width = 40;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "使用者";
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.Visible = false;
            // 
            // colMsg
            // 
            this.colMsg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMsg.HeaderText = "訊息";
            this.colMsg.Name = "colMsg";
            this.colMsg.ReadOnly = true;
            // 
            // colUrl
            // 
            this.colUrl.ActiveLinkColor = System.Drawing.Color.Blue;
            this.colUrl.HeaderText = "詳細說明";
            this.colUrl.Name = "colUrl";
            this.colUrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colUrl.VisitedLinkColor = System.Drawing.Color.Blue;
            this.colUrl.Width = 90;
            // 
            // colPostTime
            // 
            this.colPostTime.HeaderText = "張貼時間";
            this.colPostTime.Name = "colPostTime";
            this.colPostTime.ReadOnly = true;
            this.colPostTime.Width = 160;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "To";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "訊息";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "詳細說明";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 90;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "張貼時間";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // btnAddNews
            // 
            this.btnAddNews.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddNews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNews.BackColor = System.Drawing.Color.Transparent;
            this.btnAddNews.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddNews.Enabled = false;
            this.btnAddNews.Location = new System.Drawing.Point(689, 375);
            this.btnAddNews.Name = "btnAddNews";
            this.btnAddNews.Size = new System.Drawing.Size(100, 25);
            this.btnAddNews.TabIndex = 1;
            this.btnAddNews.Text = "新增";
            this.btnAddNews.Visible = false;
            this.btnAddNews.Click += new System.EventHandler(this.btnAddNews_Click);
            // 
            // NewsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 405);
            this.Controls.Add(this.dgNewsList);
            this.Controls.Add(this.btnAddNews);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "NewsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "最新消息";
            this.Load += new System.EventHandler(this.NewsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgNewsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgNewsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DevComponents.DotNetBar.ButtonX btnAddNews;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMsg;
        private System.Windows.Forms.DataGridViewLinkColumn colUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPostTime;
    }
}