namespace SmartSchool.Others.Configuration.ScoresTemplate
{
    partial class TemplateManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataview = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.ipTemplateList = new DevComponents.DotNetBar.ItemPanel();
            this.loading = new System.Windows.Forms.PictureBox();
            this.btnAddNew = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.peTemplateName1 = new DevComponents.DotNetBar.PanelEx();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.navigationPanePanel1 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.npLeft = new DevComponents.DotNetBar.NavigationPane();
            this.panel1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cboScoreSource = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.ciTeacher = new DevComponents.Editors.ComboItem();
            this.ciSchool = new DevComponents.Editors.ComboItem();
            this.txtEndTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtStartTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.ExamID = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseScore = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UseText = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OpenTeacherAccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputRequired = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).BeginInit();
            this.ipTemplateList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).BeginInit();
            this.navigationPanePanel1.SuspendLayout();
            this.npLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataview
            // 
            this.dataview.AllowUserToAddRows = false;
            this.dataview.AllowUserToResizeRows = false;
            this.dataview.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExamID,
            this.Weight,
            this.UseScore,
            this.UseText,
            this.OpenTeacherAccess,
            this.StartTime,
            this.EndTime,
            this.InputRequired});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataview.Location = new System.Drawing.Point(159, 37);
            this.dataview.Name = "dataview";
            this.dataview.RowHeadersWidth = 42;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataview.RowTemplate.Height = 24;
            this.dataview.Size = new System.Drawing.Size(622, 287);
            this.dataview.TabIndex = 2;
            this.dataview.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataview_RowValidating);
            this.dataview.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataview_CellValidated);
            this.dataview.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataview_CellValidating);
            this.dataview.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataview_RowsAdded);
            this.dataview.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataview_DataError);
            this.dataview.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataview_RowsRemoved);
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
            this.expandableSplitter1.Location = new System.Drawing.Point(156, 37);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 377);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 12;
            this.expandableSplitter1.TabStop = false;
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.OptionGroup = "TemplateItem";
            this.buttonItem3.Text = "國防通識";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.OptionGroup = "TemplateItem";
            this.buttonItem2.Text = "體育評量";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.OptionGroup = "TemplateItem";
            this.buttonItem1.Text = "一般科目<b><font color=\"#ED1C24\">(已修改)</font></b>";
            // 
            // ipTemplateList
            // 
            this.ipTemplateList.AutoScroll = true;
            // 
            // 
            // 
            this.ipTemplateList.BackgroundStyle.BackColor = System.Drawing.Color.White;
            this.ipTemplateList.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipTemplateList.BackgroundStyle.BorderBottomWidth = 1;
            this.ipTemplateList.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.ipTemplateList.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipTemplateList.BackgroundStyle.BorderLeftWidth = 1;
            this.ipTemplateList.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipTemplateList.BackgroundStyle.BorderRightWidth = 1;
            this.ipTemplateList.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipTemplateList.BackgroundStyle.BorderTopWidth = 1;
            this.ipTemplateList.BackgroundStyle.PaddingBottom = 1;
            this.ipTemplateList.BackgroundStyle.PaddingLeft = 1;
            this.ipTemplateList.BackgroundStyle.PaddingRight = 1;
            this.ipTemplateList.BackgroundStyle.PaddingTop = 1;
            this.ipTemplateList.Controls.Add(this.loading);
            this.ipTemplateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipTemplateList.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem3});
            this.ipTemplateList.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.ipTemplateList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ipTemplateList.Location = new System.Drawing.Point(0, 0);
            this.ipTemplateList.Name = "ipTemplateList";
            this.ipTemplateList.Size = new System.Drawing.Size(154, 287);
            this.ipTemplateList.TabIndex = 0;
            this.ipTemplateList.Text = "itemPanel1";
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
            // btnAddNew
            // 
            this.btnAddNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddNew.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddNew.Location = new System.Drawing.Point(0, 287);
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
            this.btnSave.Location = new System.Drawing.Point(0, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(154, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDelete.Location = new System.Drawing.Point(0, 333);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(154, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // peTemplateName1
            // 
            this.peTemplateName1.CanvasColor = System.Drawing.SystemColors.Control;
            this.peTemplateName1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.peTemplateName1.Dock = System.Windows.Forms.DockStyle.Top;
            this.peTemplateName1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.peTemplateName1.Location = new System.Drawing.Point(156, 0);
            this.peTemplateName1.Name = "peTemplateName1";
            this.peTemplateName1.Size = new System.Drawing.Size(625, 37);
            this.peTemplateName1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.peTemplateName1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.peTemplateName1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.peTemplateName1.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.peTemplateName1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.peTemplateName1.Style.GradientAngle = 90;
            this.peTemplateName1.Style.MarginLeft = 15;
            this.peTemplateName1.TabIndex = 1;
            this.peTemplateName1.Text = "一般科目";
            // 
            // buttonItem4
            // 
            this.buttonItem4.Checked = true;
            this.buttonItem4.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem4.Image")));
            this.buttonItem4.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.OptionGroup = "navBar";
            this.buttonItem4.Text = "評分樣版";
            // 
            // navigationPanePanel1
            // 
            this.navigationPanePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.navigationPanePanel1.Controls.Add(this.ipTemplateList);
            this.navigationPanePanel1.Controls.Add(this.btnAddNew);
            this.navigationPanePanel1.Controls.Add(this.btnSave);
            this.navigationPanePanel1.Controls.Add(this.btnDelete);
            this.navigationPanePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPanePanel1.Location = new System.Drawing.Point(1, 25);
            this.navigationPanePanel1.Name = "navigationPanePanel1";
            this.navigationPanePanel1.ParentItem = this.buttonItem4;
            this.navigationPanePanel1.Size = new System.Drawing.Size(154, 356);
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
            this.npLeft.Size = new System.Drawing.Size(156, 414);
            this.npLeft.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.npLeft.TabIndex = 0;
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
            this.npLeft.TitlePanel.Text = "評分樣版";
            // 
            // panel1
            // 
            this.panel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panel1.Controls.Add(this.labelX4);
            this.panel1.Controls.Add(this.cboScoreSource);
            this.panel1.Controls.Add(this.txtEndTime);
            this.panel1.Controls.Add(this.txtStartTime);
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(159, 324);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 90);
            this.panel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panel1.Style.GradientAngle = 90;
            this.panel1.TabIndex = 13;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(19, 16);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(87, 20);
            this.labelX4.TabIndex = 17;
            this.labelX4.Text = "課程成績來源";
            // 
            // cboScoreSource
            // 
            this.cboScoreSource.DisplayMember = "Text";
            this.cboScoreSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboScoreSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScoreSource.FormattingEnabled = true;
            this.cboScoreSource.ItemHeight = 19;
            this.cboScoreSource.Items.AddRange(new object[] {
            this.ciTeacher,
            this.ciSchool});
            this.cboScoreSource.Location = new System.Drawing.Point(112, 14);
            this.cboScoreSource.Name = "cboScoreSource";
            this.cboScoreSource.Size = new System.Drawing.Size(152, 25);
            this.cboScoreSource.TabIndex = 16;
            this.cboScoreSource.SelectedIndexChanged += new System.EventHandler(this.cboScoreSource_SelectedIndexChanged);
            // 
            // ciTeacher
            // 
            this.ciTeacher.Text = "由教師提供";
            // 
            // ciSchool
            // 
            this.ciSchool.ImagePosition = System.Windows.Forms.HorizontalAlignment.Right;
            this.ciSchool.Text = "由學校計算";
            // 
            // txtEndTime
            // 
            // 
            // 
            // 
            this.txtEndTime.Border.Class = "TextBoxBorder";
            this.txtEndTime.Location = new System.Drawing.Point(287, 52);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(152, 25);
            this.txtEndTime.TabIndex = 13;
            this.txtEndTime.Validating += new System.ComponentModel.CancelEventHandler(this.txtEndTime_Validating);
            this.txtEndTime.Validated += new System.EventHandler(this.txtEndTime_Validated);
            // 
            // txtStartTime
            // 
            // 
            // 
            // 
            this.txtStartTime.Border.Class = "TextBoxBorder";
            this.txtStartTime.Location = new System.Drawing.Point(112, 52);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(152, 25);
            this.txtStartTime.TabIndex = 12;
            this.txtStartTime.Validating += new System.ComponentModel.CancelEventHandler(this.txtStartTime_Validating);
            this.txtStartTime.Validated += new System.EventHandler(this.txtStartTime_Validated);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(19, 55);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(90, 19);
            this.labelX1.TabIndex = 14;
            this.labelX1.Text = "繳   交   時   間";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(266, 55);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(20, 19);
            this.labelX2.TabIndex = 15;
            this.labelX2.Text = "～";
            // 
            // ExamID
            // 
            this.ExamID.DataPropertyName = "ExamID";
            this.ExamID.DisplayStyleForCurrentCellOnly = true;
            this.ExamID.HeaderText = "評量名稱";
            this.ExamID.Name = "ExamID";
            this.ExamID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ExamID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Weight
            // 
            this.Weight.DataPropertyName = "Weight";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Weight.DefaultCellStyle = dataGridViewCellStyle2;
            this.Weight.HeaderText = "比重";
            this.Weight.Name = "Weight";
            this.Weight.Width = 50;
            // 
            // UseScore
            // 
            this.UseScore.DataPropertyName = "UseScore";
            this.UseScore.FalseValue = "否";
            this.UseScore.HeaderText = "分數評量";
            this.UseScore.Name = "UseScore";
            this.UseScore.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UseScore.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.UseScore.TrueValue = "是";
            this.UseScore.Width = 75;
            // 
            // UseText
            // 
            this.UseText.DataPropertyName = "UseText";
            this.UseText.FalseValue = "否";
            this.UseText.HeaderText = "文字評量";
            this.UseText.Name = "UseText";
            this.UseText.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UseText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.UseText.TrueValue = "是";
            this.UseText.Width = 75;
            // 
            // OpenTeacherAccess
            // 
            this.OpenTeacherAccess.DataPropertyName = "OpenTeacherAccess";
            this.OpenTeacherAccess.FalseValue = "否";
            this.OpenTeacherAccess.HeaderText = "開放繳交";
            this.OpenTeacherAccess.Name = "OpenTeacherAccess";
            this.OpenTeacherAccess.TrueValue = "是";
            this.OpenTeacherAccess.Visible = false;
            this.OpenTeacherAccess.Width = 75;
            // 
            // StartTime
            // 
            this.StartTime.DataPropertyName = "StartTime";
            this.StartTime.HeaderText = "開始時間";
            this.StartTime.Name = "StartTime";
            this.StartTime.Width = 70;
            // 
            // EndTime
            // 
            this.EndTime.DataPropertyName = "EndTime";
            this.EndTime.HeaderText = "結束時間";
            this.EndTime.Name = "EndTime";
            this.EndTime.Width = 70;
            // 
            // InputRequired
            // 
            this.InputRequired.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InputRequired.DataPropertyName = "InputRequired";
            this.InputRequired.FalseValue = "是";
            this.InputRequired.HeaderText = "不強制繳交成績";
            this.InputRequired.Name = "InputRequired";
            this.InputRequired.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.InputRequired.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.InputRequired.TrueValue = "否";
            // 
            // TemplateManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(781, 414);
            this.Controls.Add(this.dataview);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.peTemplateName1);
            this.Controls.Add(this.npLeft);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(600, 296);
            this.Name = "TemplateManager";
            this.Text = "評分樣版管理";
            this.Load += new System.EventHandler(this.TemplateManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).EndInit();
            this.ipTemplateList.ResumeLayout(false);
            this.ipTemplateList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).EndInit();
            this.navigationPanePanel1.ResumeLayout(false);
            this.npLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataview;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ItemPanel ipTemplateList;
        private DevComponents.DotNetBar.ButtonX btnAddNew;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.PanelEx peTemplateName1;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel1;
        private DevComponents.DotNetBar.NavigationPane npLeft;
        private System.Windows.Forms.PictureBox loading;
        private DevComponents.DotNetBar.PanelEx panel1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboScoreSource;
        private DevComponents.Editors.ComboItem ciTeacher;
        private DevComponents.Editors.ComboItem ciSchool;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEndTime;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStartTime;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewComboBoxColumn ExamID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UseScore;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UseText;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OpenTeacherAccess;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn InputRequired;

    }
}