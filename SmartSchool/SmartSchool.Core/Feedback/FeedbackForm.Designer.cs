namespace SmartSchool.Feedback
{
    partial class FeedbackForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeedbackForm));
            this.txtDesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.picUploadImage = new System.Windows.Forms.PictureBox();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btnRePaste = new DevComponents.DotNetBar.ButtonItem();
            this.btnFromFile = new DevComponents.DotNetBar.ButtonItem();
            this.btnClearPicture = new DevComponents.DotNetBar.ButtonItem();
            this.userPanel = new DevComponents.DotNetBar.PanelEx();
            this.btnHistory = new DevComponents.DotNetBar.ButtonX();
            this.adminPanel = new DevComponents.DotNetBar.PanelEx();
            this.contextMenuBar2 = new DevComponents.DotNetBar.ContextMenuBar();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.btnSendNews = new DevComponents.DotNetBar.ButtonItem();
            this.dgFeedback = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtEndDate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtStartDate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.txtPreview = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccessPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSchool = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPostTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.picUploadImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            this.userPanel.SuspendLayout();
            this.adminPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFeedback)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDesc
            // 
            // 
            // 
            // 
            this.txtDesc.Border.Class = "TextBoxBorder";
            this.txtDesc.Location = new System.Drawing.Point(6, 29);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDesc.Size = new System.Drawing.Size(500, 190);
            this.txtDesc.TabIndex = 0;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(6, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(34, 19);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "描述";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(6, 226);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 19);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "畫面";
            // 
            // picUploadImage
            // 
            this.picUploadImage.BackColor = System.Drawing.Color.Transparent;
            this.picUploadImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.contextMenuBar1.SetContextMenuEx(this.picUploadImage, this.buttonItem1);
            this.picUploadImage.Location = new System.Drawing.Point(6, 249);
            this.picUploadImage.Name = "picUploadImage";
            this.picUploadImage.Size = new System.Drawing.Size(500, 280);
            this.picUploadImage.TabIndex = 3;
            this.picUploadImage.TabStop = false;
            this.picUploadImage.DoubleClick += new System.EventHandler(this.picUploadImage_DoubleClick);
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubmit.Location = new System.Drawing.Point(406, 535);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 25);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "發送";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.contextMenuBar1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.contextMenuBar1.Location = new System.Drawing.Point(337, 270);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(115, 28);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.contextMenuBar1.TabIndex = 5;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            // 
            // buttonItem1
            // 
            this.buttonItem1.AutoExpandOnClick = true;
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnRePaste,
            this.btnFromFile,
            this.btnClearPicture});
            this.buttonItem1.Text = "buttonItem1";
            // 
            // btnRePaste
            // 
            this.btnRePaste.ImagePaddingHorizontal = 8;
            this.btnRePaste.Name = "btnRePaste";
            this.btnRePaste.Text = "從剪貼簿重新貼上";
            this.btnRePaste.Click += new System.EventHandler(this.btnRePaste_Click);
            // 
            // btnFromFile
            // 
            this.btnFromFile.ImagePaddingHorizontal = 8;
            this.btnFromFile.Name = "btnFromFile";
            this.btnFromFile.Text = "從檔案插入圖片...";
            this.btnFromFile.Click += new System.EventHandler(this.btnFromFile_Click);
            // 
            // btnClearPicture
            // 
            this.btnClearPicture.ImagePaddingHorizontal = 8;
            this.btnClearPicture.Name = "btnClearPicture";
            this.btnClearPicture.Text = "清除圖片";
            this.btnClearPicture.Click += new System.EventHandler(this.btnClearPicture_Click);
            // 
            // userPanel
            // 
            this.userPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.userPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.userPanel.Controls.Add(this.btnHistory);
            this.userPanel.Controls.Add(this.btnSubmit);
            this.userPanel.Controls.Add(this.contextMenuBar1);
            this.userPanel.Controls.Add(this.labelX1);
            this.userPanel.Controls.Add(this.picUploadImage);
            this.userPanel.Controls.Add(this.txtDesc);
            this.userPanel.Controls.Add(this.labelX2);
            this.userPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userPanel.Location = new System.Drawing.Point(0, 0);
            this.userPanel.Name = "userPanel";
            this.userPanel.Size = new System.Drawing.Size(792, 591);
            this.userPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.userPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.userPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.userPanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.userPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.userPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.userPanel.Style.GradientAngle = 90;
            this.userPanel.TabIndex = 6;
            // 
            // btnHistory
            // 
            this.btnHistory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnHistory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnHistory.Location = new System.Drawing.Point(299, 535);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(100, 25);
            this.btnHistory.TabIndex = 6;
            this.btnHistory.Text = "檢視回報記錄";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // adminPanel
            // 
            this.adminPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.adminPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.adminPanel.Controls.Add(this.contextMenuBar2);
            this.adminPanel.Controls.Add(this.dgFeedback);
            this.adminPanel.Controls.Add(this.panelEx1);
            this.adminPanel.Controls.Add(this.expandableSplitter1);
            this.adminPanel.Controls.Add(this.tableLayoutPanel1);
            this.adminPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adminPanel.Enabled = false;
            this.adminPanel.Location = new System.Drawing.Point(0, 0);
            this.adminPanel.Name = "adminPanel";
            this.adminPanel.Size = new System.Drawing.Size(792, 591);
            this.adminPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.adminPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.adminPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.adminPanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.adminPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.adminPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.adminPanel.Style.GradientAngle = 90;
            this.adminPanel.TabIndex = 7;
            this.adminPanel.Visible = false;
            // 
            // contextMenuBar2
            // 
            this.contextMenuBar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.contextMenuBar2.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.contextMenuBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2});
            this.contextMenuBar2.Location = new System.Drawing.Point(390, 82);
            this.contextMenuBar2.Name = "contextMenuBar2";
            this.contextMenuBar2.Size = new System.Drawing.Size(116, 28);
            this.contextMenuBar2.Stretch = true;
            this.contextMenuBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.contextMenuBar2.TabIndex = 5;
            this.contextMenuBar2.TabStop = false;
            this.contextMenuBar2.Text = "contextMenuBar2";
            // 
            // buttonItem2
            // 
            this.buttonItem2.AutoExpandOnClick = true;
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSendNews});
            this.buttonItem2.Text = "buttonItem2";
            // 
            // btnSendNews
            // 
            this.btnSendNews.ImagePaddingHorizontal = 8;
            this.btnSendNews.Name = "btnSendNews";
            this.btnSendNews.Text = "發送最新消息";
            this.btnSendNews.Click += new System.EventHandler(this.btnSendNews_Click);
            // 
            // dgFeedback
            // 
            this.dgFeedback.AllowUserToAddRows = false;
            this.dgFeedback.AllowUserToDeleteRows = false;
            this.dgFeedback.AllowUserToResizeRows = false;
            this.dgFeedback.BackgroundColor = System.Drawing.Color.White;
            this.dgFeedback.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFeedback.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colAccessPoint,
            this.colSchool,
            this.colUser,
            this.colDesc,
            this.colPostTime,
            this.colComments});
            this.contextMenuBar2.SetContextMenuEx(this.dgFeedback, this.buttonItem2);
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgFeedback.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFeedback.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgFeedback.Location = new System.Drawing.Point(0, 34);
            this.dgFeedback.Name = "dgFeedback";
            this.dgFeedback.RowHeadersWidth = 25;
            this.dgFeedback.RowTemplate.Height = 24;
            this.dgFeedback.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFeedback.Size = new System.Drawing.Size(792, 354);
            this.dgFeedback.TabIndex = 4;
            this.dgFeedback.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFeedback_CellDoubleClick);
            this.dgFeedback.SelectionChanged += new System.EventHandler(this.dataGridViewX1_SelectionChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.txtEndDate);
            this.panelEx1.Controls.Add(this.txtStartDate);
            this.panelEx1.Controls.Add(this.btnRefresh);
            this.panelEx1.Controls.Add(this.btnExport);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(792, 34);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 6;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.Location = new System.Drawing.Point(179, 8);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(17, 19);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "~";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.Location = new System.Drawing.Point(8, 8);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 19);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "張貼日期";
            // 
            // txtEndDate
            // 
            // 
            // 
            // 
            this.txtEndDate.Border.Class = "TextBoxBorder";
            this.txtEndDate.Location = new System.Drawing.Point(200, 5);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(100, 25);
            this.txtEndDate.TabIndex = 1;
            this.txtEndDate.WatermarkText = "西元年/月/日";
            // 
            // txtStartDate
            // 
            // 
            // 
            // 
            this.txtStartDate.Border.Class = "TextBoxBorder";
            this.txtStartDate.Location = new System.Drawing.Point(73, 5);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(100, 25);
            this.txtStartDate.TabIndex = 0;
            this.txtStartDate.WatermarkText = "西元年/月/日";
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(310, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 25);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "重新整理";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(390, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 25);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "匯出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.expandableSplitter1.ExpandActionClick = false;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 388);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(792, 3);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 4;
            this.expandableSplitter1.TabStop = false;
            this.expandableSplitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.expandableSplitter1_SplitterMoved);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.picPreview, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPreview, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 391);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 200);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // picPreview
            // 
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(4, 4);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(270, 192);
            this.picPreview.TabIndex = 5;
            this.picPreview.TabStop = false;
            this.picPreview.DoubleClick += new System.EventHandler(this.picPreview_DoubleClick);
            // 
            // txtPreview
            // 
            // 
            // 
            // 
            this.txtPreview.Border.Class = "TextBoxBorder";
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPreview.Location = new System.Drawing.Point(281, 4);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Size = new System.Drawing.Size(507, 192);
            this.txtPreview.TabIndex = 5;
            this.txtPreview.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPreview_KeyUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 20F;
            this.dataGridViewTextBoxColumn1.HeaderText = "學校名稱";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 20F;
            this.dataGridViewTextBoxColumn2.HeaderText = "使用者帳號";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.FillWeight = 35F;
            this.dataGridViewTextBoxColumn3.HeaderText = "描述";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.FillWeight = 25F;
            this.dataGridViewTextBoxColumn4.HeaderText = "張貼時間";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "問題回報與建議";
            this.saveFileDialog1.Filter = "Excel 檔案|*.xls";
            this.saveFileDialog1.Title = "另存檔案";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "點陣圖檔案(*.bmp)|*.bmp|JPEG (*.jpg;*.jpeg;*.jpe)|*.jpg;*.jpeg;*.jpe|GIF (*.gif)|*.gif" +
                "|PNG (*.png)|*.png|所有圖片檔案|*.bmp;*.jpg;*.jpeg;*.jpe;*.gif;*.png|所有檔案|*.*";
            this.openFileDialog1.FilterIndex = 5;
            this.openFileDialog1.Title = "開啟圖片";
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            // 
            // colAccessPoint
            // 
            this.colAccessPoint.HeaderText = "AccessPoint";
            this.colAccessPoint.Name = "colAccessPoint";
            this.colAccessPoint.ReadOnly = true;
            this.colAccessPoint.Visible = false;
            // 
            // colSchool
            // 
            this.colSchool.FillWeight = 20F;
            this.colSchool.HeaderText = "學校名稱";
            this.colSchool.Name = "colSchool";
            this.colSchool.ReadOnly = true;
            this.colSchool.Width = 160;
            // 
            // colUser
            // 
            this.colUser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colUser.FillWeight = 20F;
            this.colUser.HeaderText = "使用者帳號";
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.Width = 98;
            // 
            // colDesc
            // 
            this.colDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDesc.FillWeight = 35F;
            this.colDesc.HeaderText = "描述";
            this.colDesc.Name = "colDesc";
            this.colDesc.ReadOnly = true;
            // 
            // colPostTime
            // 
            this.colPostTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colPostTime.FillWeight = 25F;
            this.colPostTime.HeaderText = "張貼時間";
            this.colPostTime.Name = "colPostTime";
            this.colPostTime.ReadOnly = true;
            this.colPostTime.Width = 85;
            // 
            // colComments
            // 
            this.colComments.HeaderText = "處理狀況";
            this.colComments.Name = "colComments";
            this.colComments.ReadOnly = true;
            // 
            // FeedbackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 591);
            this.Controls.Add(this.adminPanel);
            this.Controls.Add(this.userPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = true;
            this.Name = "FeedbackForm";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "問題回報與建議";
            this.Load += new System.EventHandler(this.FeedbackForm_Load);
            this.Resize += new System.EventHandler(this.FeedbackForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picUploadImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            this.userPanel.ResumeLayout(false);
            this.userPanel.PerformLayout();
            this.adminPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFeedback)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtDesc;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.PictureBox picUploadImage;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem btnClearPicture;
        private DevComponents.DotNetBar.PanelEx userPanel;
        private DevComponents.DotNetBar.PanelEx adminPanel;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgFeedback;
        private DevComponents.DotNetBar.ButtonItem btnRePaste;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.ButtonItem btnFromFile;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar2;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem btnSendNews;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEndDate;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStartDate;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevComponents.DotNetBar.ButtonX btnHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccessPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSchool;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPostTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComments;
    }
}