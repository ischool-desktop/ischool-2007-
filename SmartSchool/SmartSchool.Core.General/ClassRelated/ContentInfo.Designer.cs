namespace SmartSchool.ClassRelated
{
    partial class ContentInfo
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentInfo));
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelDetial = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.splitterListDetial = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelList = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvStudent = new SmartSchool.Common.DataGridViewEx();
            this.expandableSplitter3 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonExpand = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.dragDropTimer = new System.Windows.Forms.Timer(this.components);
            this.dcmStudent = new IntelliSchool.DSA.ClientFramework.ControlCommunication.DGVColumnManager();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudentCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTeacher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGradeYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelContent.SuspendLayout();
            this.panelDetial.SuspendLayout();
            this.panelList.SuspendLayout();
            this.panel1.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.dgvStudent ) ).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panelDetial);
            this.panelContent.Controls.Add(this.splitterListDetial);
            this.panelContent.Controls.Add(this.panelList);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(810, 634);
            this.panelContent.TabIndex = 5;
            this.panelContent.Visible = false;
            // 
            // panelDetial
            // 
            this.panelDetial.Controls.Add(this.button1);
            this.panelDetial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetial.Location = new System.Drawing.Point(257, 0);
            this.panelDetial.Name = "panelDetial";
            this.panelDetial.Size = new System.Drawing.Size(553, 634);
            this.panelDetial.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.button1.Location = new System.Drawing.Point(6, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 0);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.Text = "&F";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitterListDetial
            // 
            this.splitterListDetial.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.splitterListDetial.BackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.splitterListDetial.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.splitterListDetial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterListDetial.Expandable = false;
            this.splitterListDetial.ExpandableControl = this.panelDetial;
            this.splitterListDetial.ExpandFillColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.splitterListDetial.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.ExpandLineColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.splitterListDetial.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitterListDetial.GripDarkColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.splitterListDetial.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitterListDetial.GripLightColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.splitterListDetial.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.splitterListDetial.HotBackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 252 ) ) ) ), ( (int)( ( (byte)( 151 ) ) ) ), ( (int)( ( (byte)( 61 ) ) ) ));
            this.splitterListDetial.HotBackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 184 ) ) ) ), ( (int)( ( (byte)( 94 ) ) ) ));
            this.splitterListDetial.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.splitterListDetial.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.splitterListDetial.HotExpandFillColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.splitterListDetial.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.HotExpandLineColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.splitterListDetial.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitterListDetial.HotGripDarkColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.splitterListDetial.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.HotGripLightColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.splitterListDetial.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.splitterListDetial.Location = new System.Drawing.Point(254, 0);
            this.splitterListDetial.MinExtra = 150;
            this.splitterListDetial.MinSize = 150;
            this.splitterListDetial.Name = "splitterListDetial";
            this.splitterListDetial.Size = new System.Drawing.Size(3, 634);
            this.splitterListDetial.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.splitterListDetial.TabIndex = 1;
            this.splitterListDetial.TabStop = false;
            this.splitterListDetial.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.splitterListDetial_ExpandedChanged);
            this.splitterListDetial.ExpandedChanging += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.splitterListDetial_ExpandedChanging);
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.panel1);
            this.panelList.Controls.Add(this.expandableSplitter3);
            this.panelList.Controls.Add(this.panelEx1);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelList.Location = new System.Drawing.Point(0, 0);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(254, 634);
            this.panelList.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvStudent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 603);
            this.panel1.TabIndex = 17;
            // 
            // dgvStudent
            // 
            this.dgvStudent.AllowUserToAddRows = false;
            this.dgvStudent.AllowUserToDeleteRows = false;
            this.dgvStudent.AllowUserToOrderColumns = true;
            this.dgvStudent.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvStudent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStudent.BackgroundColor = System.Drawing.Color.White;
            this.dgvStudent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvStudent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvStudent.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvStudent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colClassName,
            this.colStudentCount,
            this.colTeacher,
            this.colGradeYear,
            this.colDepartment});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStudent.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStudent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvStudent.EnableHeadersVisualStyles = false;
            this.dgvStudent.GridColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 208 ) ) ) ), ( (int)( ( (byte)( 215 ) ) ) ), ( (int)( ( (byte)( 229 ) ) ) ));
            this.dgvStudent.Location = new System.Drawing.Point(0, 0);
            this.dgvStudent.Name = "dgvStudent";
            this.dgvStudent.ReadOnly = true;
            this.dgvStudent.RowHeadersVisible = false;
            this.dgvStudent.RowTemplate.Height = 24;
            this.dgvStudent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudent.ShowCellErrors = false;
            this.dgvStudent.ShowEditingIcon = false;
            this.dgvStudent.ShowRowErrors = false;
            this.dgvStudent.Size = new System.Drawing.Size(254, 603);
            this.dgvStudent.StandardTab = true;
            this.dgvStudent.TabIndex = 13;
            this.dgvStudent.Tag = "2";
            this.dgvStudent.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStudent_CellMouseUp);
            this.dgvStudent.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvStudent_SortCompare);
            this.dgvStudent.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudent_CellMouseLeave);
            this.dgvStudent.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudent_CellDoubleClick);
            this.dgvStudent.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStudent_CellMouseDown);
            this.dgvStudent.MouseHover += new System.EventHandler(this.SetFocus);
            this.dgvStudent.SelectionChanged += new System.EventHandler(this.dgvStudent_SelectionChanged);
            // 
            // expandableSplitter3
            // 
            this.expandableSplitter3.BackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.expandableSplitter3.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter3.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandableSplitter3.Enabled = false;
            this.expandableSplitter3.Expandable = false;
            this.expandableSplitter3.ExpandActionClick = false;
            this.expandableSplitter3.Expanded = false;
            this.expandableSplitter3.ExpandFillColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.expandableSplitter3.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter3.ExpandLineColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.expandableSplitter3.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter3.GripDarkColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.expandableSplitter3.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter3.GripLightColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.expandableSplitter3.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter3.HotBackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 252 ) ) ) ), ( (int)( ( (byte)( 151 ) ) ) ), ( (int)( ( (byte)( 61 ) ) ) ));
            this.expandableSplitter3.HotBackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 184 ) ) ) ), ( (int)( ( (byte)( 94 ) ) ) ));
            this.expandableSplitter3.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter3.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter3.HotExpandFillColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.expandableSplitter3.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter3.HotExpandLineColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.expandableSplitter3.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter3.HotGripDarkColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.expandableSplitter3.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter3.HotGripLightColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.expandableSplitter3.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter3.Location = new System.Drawing.Point(0, 30);
            this.expandableSplitter3.Name = "expandableSplitter3";
            this.expandableSplitter3.Size = new System.Drawing.Size(254, 1);
            this.expandableSplitter3.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter3.TabIndex = 16;
            this.expandableSplitter3.TabStop = false;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.txtSearch);
            this.panelEx1.Controls.Add(this.buttonExpand);
            this.panelEx1.Controls.Add(this.buttonX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(254, 30);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.txtSearch.Location = new System.Drawing.Point(3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(194, 25);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.WatermarkColor = System.Drawing.Color.Peru;
            this.txtSearch.WatermarkFont = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // buttonExpand
            // 
            this.buttonExpand.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonExpand.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonExpand.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonExpand.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.buttonExpand.Location = new System.Drawing.Point(227, 5);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(21, 21);
            this.buttonExpand.TabIndex = 2;
            this.buttonExpand.Text = ">>";
            this.buttonExpand.Tooltip = "最大化";
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Image = ( (System.Drawing.Image)( resources.GetObject("buttonX1.Image") ) );
            this.buttonX1.Location = new System.Drawing.Point(200, 5);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(24, 21);
            this.buttonX1.SplitButton = true;
            this.buttonX1.SubItemsExpandWidth = 17;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Tooltip = "搜尋";
            this.buttonX1.Click += new System.EventHandler(this.SearchClick);
            // 
            // dragDropTimer
            // 
            this.dragDropTimer.Interval = 300;
            this.dragDropTimer.Tick += new System.EventHandler(this.dragDropTimer_Tick);
            // 
            // dcmStudent
            // 
            this.dcmStudent.Target = this.dgvStudent;
            // 
            // colID
            // 
            this.colID.HeaderText = "編號";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            // 
            // colClassName
            // 
            this.colClassName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colClassName.FillWeight = 94.04964F;
            this.colClassName.HeaderText = "名稱";
            this.colClassName.MinimumWidth = 80;
            this.colClassName.Name = "colClassName";
            this.colClassName.ReadOnly = true;
            // 
            // colStudentCount
            // 
            this.colStudentCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStudentCount.FillWeight = 60.71066F;
            this.colStudentCount.HeaderText = "人數";
            this.colStudentCount.MinimumWidth = 65;
            this.colStudentCount.Name = "colStudentCount";
            this.colStudentCount.ReadOnly = true;
            // 
            // colTeacher
            // 
            this.colTeacher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTeacher.FillWeight = 75.23971F;
            this.colTeacher.HeaderText = "班導師";
            this.colTeacher.MinimumWidth = 75;
            this.colTeacher.Name = "colTeacher";
            this.colTeacher.ReadOnly = true;
            // 
            // colGradeYear
            // 
            this.colGradeYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGradeYear.FillWeight = 50F;
            this.colGradeYear.HeaderText = "年級";
            this.colGradeYear.MinimumWidth = 65;
            this.colGradeYear.Name = "colGradeYear";
            this.colGradeYear.ReadOnly = true;
            this.colGradeYear.Visible = false;
            // 
            // colDepartment
            // 
            this.colDepartment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDepartment.HeaderText = "科別";
            this.colDepartment.MinimumWidth = 70;
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.ReadOnly = true;
            this.colDepartment.Visible = false;
            // 
            // ContentInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.Name = "ContentInfo";
            this.Size = new System.Drawing.Size(810, 634);
            this.panelContent.ResumeLayout(false);
            this.panelDetial.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ( (System.ComponentModel.ISupportInitialize)( this.dgvStudent ) ).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelDetial;
        private DevComponents.DotNetBar.ExpandableSplitter splitterListDetial;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Panel panel1;
        private SmartSchool.Common.DataGridViewEx dgvStudent;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter3;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private DevComponents.DotNetBar.ButtonX buttonExpand;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private IntelliSchool.DSA.ClientFramework.ControlCommunication.DGVColumnManager dcmStudent;
        private System.Windows.Forms.Timer dragDropTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTeacher;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGradeYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartment;
    }
}
