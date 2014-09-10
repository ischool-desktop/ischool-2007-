namespace SmartSchool.CourseRelated
{
    partial class CourseContentPane
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CourseContentPane));
            this.ContentPaneControl = new System.Windows.Forms.Panel();
            this.splitterListDetial = new DevComponents.DotNetBar.ExpandableSplitter();
            this.DetailPaneContainer = new System.Windows.Forms.Panel();
            this.DetailPane = new SmartSchool.CourseRelated.CourseDetailPane();
            this.ListPaneControl = new System.Windows.Forms.Panel();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.ctxDefault = new DevComponents.DotNetBar.ButtonItem();
            this.ctxiAddPrepare = new DevComponents.DotNetBar.ButtonItem();
            this.ctxiRemovePrepare = new DevComponents.DotNetBar.ButtonItem();
            this._grid_view = new SmartSchool.Common.DataGridViewEx();
            this.HideBug = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTeacher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExamTemplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this._txt_search = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnExpand = new DevComponents.DotNetBar.ButtonX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.chkSearchCourseName = new DevComponents.DotNetBar.CheckBoxItem();
            this.chkSearchTeacher = new DevComponents.DotNetBar.CheckBoxItem();
            this.chkSearchClass = new DevComponents.DotNetBar.CheckBoxItem();
            this.dcmCourse = new IntelliSchool.DSA.ClientFramework.ControlCommunication.DGVColumnManager();
            this.ContentPaneControl.SuspendLayout();
            this.DetailPaneContainer.SuspendLayout();
            this.ListPaneControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._grid_view)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPaneControl
            // 
            this.ContentPaneControl.Controls.Add(this.splitterListDetial);
            this.ContentPaneControl.Controls.Add(this.DetailPaneContainer);
            this.ContentPaneControl.Controls.Add(this.ListPaneControl);
            this.ContentPaneControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPaneControl.Location = new System.Drawing.Point(0, 0);
            this.ContentPaneControl.Name = "ContentPaneControl";
            this.ContentPaneControl.Size = new System.Drawing.Size(788, 573);
            this.ContentPaneControl.TabIndex = 6;
            // 
            // splitterListDetial
            // 
            this.splitterListDetial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.splitterListDetial.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.splitterListDetial.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.splitterListDetial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterListDetial.Expandable = false;
            this.splitterListDetial.ExpandableControl = this.DetailPaneContainer;
            this.splitterListDetial.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.splitterListDetial.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.splitterListDetial.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitterListDetial.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.splitterListDetial.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitterListDetial.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.splitterListDetial.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.splitterListDetial.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.splitterListDetial.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.splitterListDetial.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.splitterListDetial.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.splitterListDetial.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.splitterListDetial.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.splitterListDetial.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitterListDetial.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.splitterListDetial.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.splitterListDetial.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.splitterListDetial.Location = new System.Drawing.Point(208, 0);
            this.splitterListDetial.MinExtra = 150;
            this.splitterListDetial.MinSize = 150;
            this.splitterListDetial.Name = "splitterListDetial";
            this.splitterListDetial.Size = new System.Drawing.Size(3, 573);
            this.splitterListDetial.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.splitterListDetial.TabIndex = 1;
            this.splitterListDetial.TabStop = false;
            // 
            // DetailPaneContainer
            // 
            this.DetailPaneContainer.Controls.Add(this.DetailPane);
            this.DetailPaneContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailPaneContainer.Location = new System.Drawing.Point(208, 0);
            this.DetailPaneContainer.Name = "DetailPaneContainer";
            this.DetailPaneContainer.Size = new System.Drawing.Size(580, 573);
            this.DetailPaneContainer.TabIndex = 2;
            // 
            // DetailPane
            // 
            this.DetailPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailPane.Location = new System.Drawing.Point(0, 0);
            this.DetailPane.Manager = null;
            this.DetailPane.Name = "DetailPane";
            this.DetailPane.Size = new System.Drawing.Size(580, 573);
            this.DetailPane.TabIndex = 0;
            this.DetailPane.Visible = false;
            // 
            // ListPaneControl
            // 
            this.ListPaneControl.Controls.Add(this.contextMenuBar1);
            this.ListPaneControl.Controls.Add(this._grid_view);
            this.ListPaneControl.Controls.Add(this.panelEx1);
            this.ListPaneControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.ListPaneControl.Location = new System.Drawing.Point(0, 0);
            this.ListPaneControl.Name = "ListPaneControl";
            this.ListPaneControl.Size = new System.Drawing.Size(208, 573);
            this.ListPaneControl.TabIndex = 0;
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.DockSide = DevComponents.DotNetBar.eDockSide.Left;
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ctxDefault});
            this.contextMenuBar1.Location = new System.Drawing.Point(22, 62);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(159, 25);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.contextMenuBar1.TabIndex = 1;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            // 
            // ctxDefault
            // 
            this.ctxDefault.AutoExpandOnClick = true;
            this.ctxDefault.ImagePaddingHorizontal = 8;
            this.ctxDefault.Name = "ctxDefault";
            this.ctxDefault.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ctxiAddPrepare,
            this.ctxiRemovePrepare});
            this.ctxDefault.Text = "預設功能表";
            this.ctxDefault.PopupShowing += new System.EventHandler(this.ctxDefault_PopupShowing);
            this.ctxDefault.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.ctxDefault_PopupOpen);
            // 
            // ctxiAddPrepare
            // 
            this.ctxiAddPrepare.ImagePaddingHorizontal = 8;
            this.ctxiAddPrepare.Name = "ctxiAddPrepare";
            this.ctxiAddPrepare.Text = "新增至待處理";
            this.ctxiAddPrepare.Click += new System.EventHandler(this.ctxiAddPrepare_Click);
            // 
            // ctxiRemovePrepare
            // 
            this.ctxiRemovePrepare.ImagePaddingHorizontal = 8;
            this.ctxiRemovePrepare.Name = "ctxiRemovePrepare";
            this.ctxiRemovePrepare.Text = "自待處理移除";
            this.ctxiRemovePrepare.Click += new System.EventHandler(this.ctxiRemovePrepare_Click);
            // 
            // _grid_view
            // 
            this._grid_view.AllowUserToAddRows = false;
            this._grid_view.AllowUserToDeleteRows = false;
            this._grid_view.AllowUserToOrderColumns = true;
            this._grid_view.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this._grid_view.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._grid_view.BackgroundColor = System.Drawing.Color.White;
            this._grid_view.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._grid_view.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this._grid_view.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid_view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grid_view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HideBug,
            this.colCourseName,
            this.colCredit,
            this.colSubject,
            this.colClass,
            this.colTeacher,
            this.colExamTemplate});
            this.contextMenuBar1.SetContextMenuEx(this._grid_view, this.ctxDefault);
            this._grid_view.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid_view.DefaultCellStyle = dataGridViewCellStyle3;
            this._grid_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid_view.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this._grid_view.EnableHeadersVisualStyles = false;
            this._grid_view.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this._grid_view.Location = new System.Drawing.Point(0, 30);
            this._grid_view.Name = "_grid_view";
            this._grid_view.ReadOnly = true;
            this._grid_view.RowHeadersVisible = false;
            this._grid_view.RowTemplate.Height = 24;
            this._grid_view.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid_view.ShowCellErrors = false;
            this._grid_view.ShowEditingIcon = false;
            this._grid_view.ShowRowErrors = false;
            this._grid_view.Size = new System.Drawing.Size(208, 543);
            this._grid_view.StandardTab = true;
            this._grid_view.TabIndex = 13;
            this._grid_view.Tag = "2";
            this._grid_view.DoubleClick += new System.EventHandler(this._grid_view_DoubleClick);
            this._grid_view.SelectionChanged += new System.EventHandler(this.GridView_SelectionChanged);
            // 
            // HideBug
            // 
            this.HideBug.HeaderText = "HideBug";
            this.HideBug.Name = "HideBug";
            this.HideBug.ReadOnly = true;
            this.HideBug.Visible = false;
            // 
            // colCourseName
            // 
            this.colCourseName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCourseName.FillWeight = 200F;
            this.colCourseName.HeaderText = "課程名稱";
            this.colCourseName.MinimumWidth = 50;
            this.colCourseName.Name = "colCourseName";
            this.colCourseName.ReadOnly = true;
            // 
            // colCredit
            // 
            this.colCredit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCredit.HeaderText = "學分數";
            this.colCredit.MinimumWidth = 50;
            this.colCredit.Name = "colCredit";
            this.colCredit.ReadOnly = true;
            // 
            // colSubject
            // 
            this.colSubject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSubject.HeaderText = "科目";
            this.colSubject.MinimumWidth = 50;
            this.colSubject.Name = "colSubject";
            this.colSubject.ReadOnly = true;
            this.colSubject.Visible = false;
            // 
            // colClass
            // 
            this.colClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colClass.HeaderText = "班級";
            this.colClass.MinimumWidth = 50;
            this.colClass.Name = "colClass";
            this.colClass.ReadOnly = true;
            this.colClass.Visible = false;
            // 
            // colTeacher
            // 
            this.colTeacher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTeacher.HeaderText = "教師";
            this.colTeacher.MinimumWidth = 50;
            this.colTeacher.Name = "colTeacher";
            this.colTeacher.ReadOnly = true;
            this.colTeacher.Visible = false;
            // 
            // colExamTemplate
            // 
            this.colExamTemplate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colExamTemplate.HeaderText = "評分樣版";
            this.colExamTemplate.Name = "colExamTemplate";
            this.colExamTemplate.ReadOnly = true;
            this.colExamTemplate.Visible = false;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this._txt_search);
            this.panelEx1.Controls.Add(this.btnExpand);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(208, 30);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // _txt_search
            // 
            this._txt_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this._txt_search.Border.Class = "TextBoxBorder";
            this._txt_search.Location = new System.Drawing.Point(3, 4);
            this._txt_search.Name = "_txt_search";
            this._txt_search.Size = new System.Drawing.Size(126, 22);
            this._txt_search.TabIndex = 0;
            this._txt_search.WatermarkColor = System.Drawing.Color.Peru;
            this._txt_search.WatermarkFont = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._txt_search.WatermarkText = "搜尋所有課程";
            this._txt_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextSearch_KeyDown);
            // 
            // btnExpand
            // 
            this.btnExpand.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpand.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExpand.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExpand.Location = new System.Drawing.Point(175, 5);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(28, 21);
            this.btnExpand.TabIndex = 2;
            this.btnExpand.Text = ">>";
            this.btnExpand.Tooltip = "最大化";
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(132, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 21);
            this.btnSearch.SplitButton = true;
            this.btnSearch.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.chkSearchCourseName,
            this.chkSearchTeacher,
            this.chkSearchClass});
            this.btnSearch.SubItemsExpandWidth = 17;
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Tooltip = "搜尋";
            this.btnSearch.Click += new System.EventHandler(this.DoSearch_Click);
            // 
            // labelItem1
            // 
            this.labelItem1.AutoCollapseOnClick = false;
            this.labelItem1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelItem1.GlobalItem = false;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "搜尋欄位";
            // 
            // chkSearchCourseName
            // 
            this.chkSearchCourseName.AutoCollapseOnClick = false;
            this.chkSearchCourseName.Checked = true;
            this.chkSearchCourseName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchCourseName.GlobalItem = false;
            this.chkSearchCourseName.Name = "chkSearchCourseName";
            this.chkSearchCourseName.Text = "課程名稱";
            // 
            // chkSearchTeacher
            // 
            this.chkSearchTeacher.AutoCollapseOnClick = false;
            this.chkSearchTeacher.GlobalItem = false;
            this.chkSearchTeacher.Name = "chkSearchTeacher";
            this.chkSearchTeacher.Text = "授課教師";
            // 
            // chkSearchClass
            // 
            this.chkSearchClass.AutoCollapseOnClick = false;
            this.chkSearchClass.GlobalItem = false;
            this.chkSearchClass.Name = "chkSearchClass";
            this.chkSearchClass.Text = "班級";
            // 
            // dcmCourse
            // 
            this.dcmCourse.Target = this._grid_view;
            // 
            // CourseContentPane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.ContentPaneControl);
            this.Name = "CourseContentPane";
            this.Size = new System.Drawing.Size(788, 573);
            this.ContentPaneControl.ResumeLayout(false);
            this.DetailPaneContainer.ResumeLayout(false);
            this.ListPaneControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._grid_view)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ContentPaneControl;
        private System.Windows.Forms.Panel DetailPaneContainer;
        private CourseDetailPane DetailPane;
        private DevComponents.DotNetBar.ExpandableSplitter splitterListDetial;
        private System.Windows.Forms.Panel ListPaneControl;
        private SmartSchool.Common.DataGridViewEx _grid_view;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX _txt_search;
        private DevComponents.DotNetBar.ButtonX btnExpand;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.CheckBoxItem chkSearchCourseName;
        private DevComponents.DotNetBar.CheckBoxItem chkSearchTeacher;
        private DevComponents.DotNetBar.CheckBoxItem chkSearchClass;
        private IntelliSchool.DSA.ClientFramework.ControlCommunication.DGVColumnManager dcmCourse;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
        private DevComponents.DotNetBar.ButtonItem ctxDefault;
        private DevComponents.DotNetBar.ButtonItem ctxiAddPrepare;
        private DevComponents.DotNetBar.ButtonItem ctxiRemovePrepare;
        private System.Windows.Forms.DataGridViewTextBoxColumn HideBug;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTeacher;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExamTemplate;
    }
}
