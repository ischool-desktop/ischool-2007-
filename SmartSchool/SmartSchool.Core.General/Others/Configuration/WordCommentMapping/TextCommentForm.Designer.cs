namespace SmartSchool.Others.Configuration.WordCommentMapping
{
    partial class TextCommentForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextCommentForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataview = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.chCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.npLeft = new DevComponents.DotNetBar.NavigationPane();
            this.navigationPanePanel1 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.ipMoralities = new DevComponents.DotNetBar.ItemPanel();
            this.loading = new System.Windows.Forms.PictureBox();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnAddNew = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.peTemplateName1 = new DevComponents.DotNetBar.PanelEx();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).BeginInit();
            this.npLeft.SuspendLayout();
            this.navigationPanePanel1.SuspendLayout();
            this.ipMoralities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).BeginInit();
            this.SuspendLayout();
            // 
            // dataview
            // 
            this.dataview.AllowUserToResizeRows = false;
            this.dataview.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chCode,
            this.chComment});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataview.Location = new System.Drawing.Point(159, 37);
            this.dataview.Name = "dataview";
            this.dataview.RowHeadersWidth = 42;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataview.RowTemplate.Height = 24;
            this.dataview.Size = new System.Drawing.Size(680, 432);
            this.dataview.TabIndex = 16;
            this.dataview.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataview_CellValidated);
            this.dataview.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataview_RowValidated);
            // 
            // chCode
            // 
            this.chCode.HeaderText = "代碼";
            this.chCode.Name = "chCode";
            // 
            // chComment
            // 
            this.chComment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chComment.HeaderText = "文字評量";
            this.chComment.Name = "chComment";
            // 
            // npLeft
            // 
            this.npLeft.BackColor = System.Drawing.Color.Transparent;
            this.npLeft.CanCollapse = true;
            this.npLeft.Controls.Add(this.navigationPanePanel1);
            this.npLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.npLeft.ItemPaddingBottom = 2;
            this.npLeft.ItemPaddingTop = 2;
            this.npLeft.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem4});
            this.npLeft.Location = new System.Drawing.Point(0, 0);
            this.npLeft.Name = "npLeft";
            this.npLeft.Padding = new System.Windows.Forms.Padding(1);
            this.npLeft.Size = new System.Drawing.Size(156, 469);
            this.npLeft.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.npLeft.TabIndex = 14;
            // 
            // 
            // 
            this.npLeft.TitlePanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.npLeft.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.npLeft.TitlePanel.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npLeft.TitlePanel.Location = new System.Drawing.Point(1, 1);
            this.npLeft.TitlePanel.Name = "panelTitle";
            this.npLeft.TitlePanel.Size = new System.Drawing.Size(154, 24);
            this.npLeft.TitlePanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.npLeft.TitlePanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.npLeft.TitlePanel.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.npLeft.TitlePanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.npLeft.TitlePanel.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.npLeft.TitlePanel.Style.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.npLeft.TitlePanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.npLeft.TitlePanel.Style.GradientAngle = 90;
            this.npLeft.TitlePanel.Style.MarginLeft = 4;
            this.npLeft.TitlePanel.TabIndex = 0;
            this.npLeft.TitlePanel.Text = "文字評量項目";
            // 
            // navigationPanePanel1
            // 
            this.navigationPanePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.navigationPanePanel1.Controls.Add(this.ipMoralities);
            this.navigationPanePanel1.Controls.Add(this.btnImport);
            this.navigationPanePanel1.Controls.Add(this.btnExport);
            this.navigationPanePanel1.Controls.Add(this.btnAddNew);
            this.navigationPanePanel1.Controls.Add(this.btnSave);
            this.navigationPanePanel1.Controls.Add(this.btnDelete);
            this.navigationPanePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPanePanel1.Location = new System.Drawing.Point(1, 25);
            this.navigationPanePanel1.Name = "navigationPanePanel1";
            this.navigationPanePanel1.ParentItem = this.buttonItem4;
            this.navigationPanePanel1.Size = new System.Drawing.Size(154, 411);
            this.navigationPanePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.navigationPanePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.navigationPanePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.navigationPanePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.navigationPanePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPanePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.navigationPanePanel1.Style.GradientAngle = 90;
            this.navigationPanePanel1.TabIndex = 2;
            this.navigationPanePanel1.Text = "這是什麼？";
            this.navigationPanePanel1.Visible = false;
            // 
            // ipMoralities
            // 
            this.ipMoralities.AutoScroll = true;
            // 
            // 
            // 
            this.ipMoralities.BackgroundStyle.BackColor = System.Drawing.Color.White;
            this.ipMoralities.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipMoralities.BackgroundStyle.BorderBottomWidth = 1;
            this.ipMoralities.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.ipMoralities.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipMoralities.BackgroundStyle.BorderLeftWidth = 1;
            this.ipMoralities.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipMoralities.BackgroundStyle.BorderRightWidth = 1;
            this.ipMoralities.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipMoralities.BackgroundStyle.BorderTopWidth = 1;
            this.ipMoralities.BackgroundStyle.PaddingBottom = 1;
            this.ipMoralities.BackgroundStyle.PaddingLeft = 1;
            this.ipMoralities.BackgroundStyle.PaddingRight = 1;
            this.ipMoralities.BackgroundStyle.PaddingTop = 1;
            this.ipMoralities.Controls.Add(this.loading);
            this.ipMoralities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipMoralities.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem3});
            this.ipMoralities.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.ipMoralities.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ipMoralities.Location = new System.Drawing.Point(0, 0);
            this.ipMoralities.Name = "ipMoralities";
            this.ipMoralities.Size = new System.Drawing.Size(154, 296);
            this.ipMoralities.TabIndex = 0;
            this.ipMoralities.Text = "itemPanel1";
            // 
            // loading
            // 
            this.loading.Image = global::SmartSchool.Properties.Resources.loading5;
            this.loading.Location = new System.Drawing.Point(61, 127);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(32, 32);
            this.loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.loading.TabIndex = 0;
            this.loading.TabStop = false;
            this.loading.Visible = false;
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.OptionGroup = "TemplateItem";
            this.buttonItem1.Text = "一般科目<b><font color=\"#ED1C24\">(已修改)</font></b>";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.OptionGroup = "TemplateItem";
            this.buttonItem2.Text = "體育評量";
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.OptionGroup = "TemplateItem";
            this.buttonItem3.Text = "國防通識";
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnImport.Location = new System.Drawing.Point(0, 296);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(154, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "匯入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExport.Location = new System.Drawing.Point(0, 319);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(154, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "匯出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddNew.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddNew.Location = new System.Drawing.Point(0, 342);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(154, 23);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "新增";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.Location = new System.Drawing.Point(0, 365);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(154, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "全部儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDelete.Location = new System.Drawing.Point(0, 388);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(154, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.Checked = true;
            this.buttonItem4.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem4.Image")));
            this.buttonItem4.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.OptionGroup = "navBar";
            this.buttonItem4.Text = "文字評量項目";
            // 
            // peTemplateName1
            // 
            this.peTemplateName1.CanvasColor = System.Drawing.SystemColors.Control;
            this.peTemplateName1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.peTemplateName1.Dock = System.Windows.Forms.DockStyle.Top;
            this.peTemplateName1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.peTemplateName1.Location = new System.Drawing.Point(159, 0);
            this.peTemplateName1.Name = "peTemplateName1";
            this.peTemplateName1.Size = new System.Drawing.Size(680, 37);
            this.peTemplateName1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.peTemplateName1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.peTemplateName1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.peTemplateName1.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.peTemplateName1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.peTemplateName1.Style.GradientAngle = 90;
            this.peTemplateName1.Style.MarginLeft = 15;
            this.peTemplateName1.TabIndex = 15;
            this.peTemplateName1.Text = "一般科目";
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(156, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 469);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 17;
            this.expandableSplitter1.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ExamID";
            this.dataGridViewTextBoxColumn1.HeaderText = "代碼";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Weight";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "文字評量";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // TextCommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 469);
            this.Controls.Add(this.dataview);
            this.Controls.Add(this.peTemplateName1);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.npLeft);
            this.Name = "TextCommentForm";
            this.Text = "文字評量管理";
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).EndInit();
            this.npLeft.ResumeLayout(false);
            this.navigationPanePanel1.ResumeLayout(false);
            this.ipMoralities.ResumeLayout(false);
            this.ipMoralities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataview;
        private DevComponents.DotNetBar.NavigationPane npLeft;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel1;
        private DevComponents.DotNetBar.ItemPanel ipMoralities;
        private System.Windows.Forms.PictureBox loading;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonX btnAddNew;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.PanelEx peTemplateName1;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn chCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn chComment;
    }
}