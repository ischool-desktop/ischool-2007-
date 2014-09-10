namespace SmartSchool.Others.Configuration.ExamTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ipTemplateList = new DevComponents.DotNetBar.ItemPanel();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnAddNew = new DevComponents.DotNetBar.ButtonX();
            this.peTemplateName1 = new DevComponents.DotNetBar.PanelEx();
            this.npLeft = new DevComponents.DotNetBar.NavigationPane();
            this.navigationPanePanel1 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenTeacherAccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UseTextColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.WeightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseScoreColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ExamNameColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataview = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.npLeft.SuspendLayout();
            this.navigationPanePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.ipTemplateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipTemplateList.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem3});
            this.ipTemplateList.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.ipTemplateList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ipTemplateList.Location = new System.Drawing.Point(0, 0);
            this.ipTemplateList.Name = "ipTemplateList";
            this.ipTemplateList.Size = new System.Drawing.Size(154, 237);
            this.ipTemplateList.TabIndex = 0;
            this.ipTemplateList.Text = "itemPanel1";
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
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDelete.Location = new System.Drawing.Point(0, 283);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(154, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.Location = new System.Drawing.Point(0, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(154, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddNew.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddNew.Location = new System.Drawing.Point(0, 237);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(154, 23);
            this.btnAddNew.TabIndex = 0;
            this.btnAddNew.Text = "新增";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // peTemplateName1
            // 
            this.peTemplateName1.CanvasColor = System.Drawing.SystemColors.Control;
            this.peTemplateName1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.peTemplateName1.Dock = System.Windows.Forms.DockStyle.Top;
            this.peTemplateName1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.peTemplateName1.Location = new System.Drawing.Point(159, 0);
            this.peTemplateName1.Name = "peTemplateName1";
            this.peTemplateName1.Size = new System.Drawing.Size(625, 37);
            this.peTemplateName1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.peTemplateName1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.peTemplateName1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.peTemplateName1.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.peTemplateName1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.peTemplateName1.Style.GradientAngle = 90;
            this.peTemplateName1.Style.MarginLeft = 15;
            this.peTemplateName1.TabIndex = 0;
            this.peTemplateName1.Text = "一般科目";
            this.peTemplateName1.DoubleClick += new System.EventHandler(this.peTemplateName1_DoubleClick);
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
            this.npLeft.Size = new System.Drawing.Size(156, 364);
            this.npLeft.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.npLeft.TabIndex = 3;
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
            this.navigationPanePanel1.Size = new System.Drawing.Size(154, 306);
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
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 364);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 4;
            this.expandableSplitter1.TabStop = false;
            // 
            // EndTime
            // 
            this.EndTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EndTime.HeaderText = "結束時間";
            this.EndTime.Name = "EndTime";
            // 
            // StartTime
            // 
            this.StartTime.HeaderText = "開始時間";
            this.StartTime.Name = "StartTime";
            this.StartTime.Width = 125;
            // 
            // OpenTeacherAccess
            // 
            this.OpenTeacherAccess.HeaderText = "開放上傳";
            this.OpenTeacherAccess.Name = "OpenTeacherAccess";
            this.OpenTeacherAccess.Width = 80;
            // 
            // UseTextColumn
            // 
            this.UseTextColumn.HeaderText = "文字評量";
            this.UseTextColumn.Name = "UseTextColumn";
            this.UseTextColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UseTextColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.UseTextColumn.Width = 80;
            // 
            // WeightColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WeightColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.WeightColumn.HeaderText = "比重";
            this.WeightColumn.Name = "WeightColumn";
            this.WeightColumn.Width = 60;
            // 
            // UseScoreColumn
            // 
            this.UseScoreColumn.HeaderText = "分數評量";
            this.UseScoreColumn.Name = "UseScoreColumn";
            this.UseScoreColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UseScoreColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.UseScoreColumn.Width = 80;
            // 
            // ExamNameColumn
            // 
            this.ExamNameColumn.DisplayStyleForCurrentCellOnly = true;
            this.ExamNameColumn.HeaderText = "評量名稱";
            this.ExamNameColumn.Name = "ExamNameColumn";
            this.ExamNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ExamNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataview
            // 
            this.dataview.AllowUserToAddRows = false;
            this.dataview.AllowUserToResizeRows = false;
            this.dataview.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExamNameColumn,
            this.UseScoreColumn,
            this.WeightColumn,
            this.UseTextColumn,
            this.OpenTeacherAccess,
            this.StartTime,
            this.EndTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataview.Location = new System.Drawing.Point(159, 37);
            this.dataview.Name = "dataview";
            this.dataview.RowHeadersWidth = 25;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataview.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataview.RowTemplate.Height = 24;
            this.dataview.Size = new System.Drawing.Size(625, 264);
            this.dataview.TabIndex = 1;
            this.dataview.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvExamList_UserDeletingRow);
            this.dataview.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvExamList_UserAddedRow);
            this.dataview.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvExamList_RowValidating);
            this.dataview.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvExamList_UserDeletedRow);
            this.dataview.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExamList_RowValidated);
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.Location = new System.Drawing.Point(6, 6);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(172, 23);
            this.checkBoxX1.TabIndex = 5;
            this.checkBoxX1.Text = "允許上傳課程學期成績";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(6, 33);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 19);
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "開放時間";
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Location = new System.Drawing.Point(68, 30);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(152, 25);
            this.textBoxX1.TabIndex = 7;
            this.textBoxX1.WatermarkText = "例：2007/7/1 8:10";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(222, 33);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(20, 19);
            this.labelX2.TabIndex = 6;
            this.labelX2.Text = "～";
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Location = new System.Drawing.Point(243, 30);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(152, 25);
            this.textBoxX2.TabIndex = 7;
            this.textBoxX2.WatermarkText = "例：2007/7/5 16:00";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.labelX3);
            this.panel1.Controls.Add(this.textBoxX2);
            this.panel1.Controls.Add(this.checkBoxX1);
            this.panel1.Controls.Add(this.textBoxX1);
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(159, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(625, 63);
            this.panel1.TabIndex = 8;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.Location = new System.Drawing.Point(399, 33);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(109, 19);
            this.labelX3.TabIndex = 8;
            this.labelX3.Text = "(空白代表不限制)";
            // 
            // TemplateManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 364);
            this.Controls.Add(this.dataview);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.peTemplateName1);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.npLeft);
            this.MinimumSize = new System.Drawing.Size(600, 296);
            this.Name = "TemplateManager";
            this.Text = "評分樣版管理";
            this.Load += new System.EventHandler(this.TemplateManager_Load);
            this.npLeft.ResumeLayout(false);
            this.navigationPanePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ItemPanel ipTemplateList;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnAddNew;
        private DevComponents.DotNetBar.PanelEx peTemplateName1;
        private DevComponents.DotNetBar.NavigationPane npLeft;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel1;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OpenTeacherAccess;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UseTextColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeightColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UseScoreColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ExamNameColumn;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataview;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}