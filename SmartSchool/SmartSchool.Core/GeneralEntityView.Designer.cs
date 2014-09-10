using DevComponents.DotNetBar;
namespace SmartSchool
{
    partial class GeneralEntityView
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
            if ( disposing && ( components != null ) )
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralEntityView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.navigationPane1 = new DevComponents.DotNetBar.NavigationPane();
            this.navigationPanePanel1 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.eppView = new DevComponents.DotNetBar.ExpandablePanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.eppViewSelector = new DevComponents.DotNetBar.ExpandablePanel();
            this.btnFilter = new DevComponents.DotNetBar.ButtonX();
            this.btnTempory = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitterListDetial = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelList = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuBar2 = new DevComponents.DotNetBar.ContextMenuBar();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.dgvDisplayList = new SmartSchool.Common.DataGridViewEx();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.addToTemp = new DevComponents.DotNetBar.ButtonItem();
            this.removeFormTemp = new DevComponents.DotNetBar.ButtonItem();
            this.expandableSplitter3 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonExpand = new DevComponents.DotNetBar.ButtonX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.navigationPane1.SuspendLayout();
            this.navigationPanePanel1.SuspendLayout();
            this.eppView.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
            this.panelContent.SuspendLayout();
            this.panelList.SuspendLayout();
            this.panel1.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.contextMenuBar2 ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.dgvDisplayList ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.contextMenuBar1 ) ).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navigationPane1
            // 
            this.navigationPane1.Controls.Add(this.navigationPanePanel1);
            this.navigationPane1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navigationPane1.ItemPaddingBottom = 2;
            this.navigationPane1.ItemPaddingTop = 2;
            this.navigationPane1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.navigationPane1.Location = new System.Drawing.Point(0, 0);
            this.navigationPane1.Name = "navigationPane1";
            this.navigationPane1.Padding = new System.Windows.Forms.Padding(1);
            this.navigationPane1.Size = new System.Drawing.Size(189, 637);
            this.navigationPane1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.navigationPane1.TabIndex = 0;
            // 
            // 
            // 
            this.navigationPane1.TitlePanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.navigationPane1.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationPane1.TitlePanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.navigationPane1.TitlePanel.Location = new System.Drawing.Point(1, 1);
            this.navigationPane1.TitlePanel.Name = "panelTitle";
            this.navigationPane1.TitlePanel.Size = new System.Drawing.Size(187, 24);
            this.navigationPane1.TitlePanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.navigationPane1.TitlePanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.navigationPane1.TitlePanel.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.navigationPane1.TitlePanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPane1.TitlePanel.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.navigationPane1.TitlePanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.navigationPane1.TitlePanel.Style.GradientAngle = 90;
            this.navigationPane1.TitlePanel.Style.MarginLeft = 4;
            this.navigationPane1.TitlePanel.TabIndex = 0;
            this.navigationPane1.TitlePanel.Text = "buttonItem1";
            // 
            // navigationPanePanel1
            // 
            this.navigationPanePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.navigationPanePanel1.Controls.Add(this.eppView);
            this.navigationPanePanel1.Controls.Add(this.eppViewSelector);
            this.navigationPanePanel1.Controls.Add(this.btnFilter);
            this.navigationPanePanel1.Controls.Add(this.btnTempory);
            this.navigationPanePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPanePanel1.Location = new System.Drawing.Point(1, 25);
            this.navigationPanePanel1.Name = "navigationPanePanel1";
            this.navigationPanePanel1.ParentItem = this.buttonItem1;
            this.navigationPanePanel1.Size = new System.Drawing.Size(187, 579);
            this.navigationPanePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.navigationPanePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.navigationPanePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.navigationPanePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPanePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.navigationPanePanel1.Style.GradientAngle = 90;
            this.navigationPanePanel1.TabIndex = 2;
            // 
            // eppView
            // 
            this.eppView.CanvasColor = System.Drawing.SystemColors.Control;
            this.eppView.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.eppView.Controls.Add(this.pictureBox1);
            this.eppView.Controls.Add(this.panel2);
            this.eppView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eppView.Location = new System.Drawing.Point(0, 53);
            this.eppView.Name = "eppView";
            this.eppView.Size = new System.Drawing.Size(187, 441);
            this.eppView.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.eppView.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppView.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppView.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.eppView.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.eppView.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.eppView.Style.GradientAngle = 90;
            this.eppView.TabIndex = 1;
            this.eppView.TitleHeight = 23;
            this.eppView.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.eppView.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppView.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppView.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.eppView.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.eppView.TitleStyle.CornerDiameter = 2;
            this.eppView.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.eppView.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.eppView.TitleStyle.GradientAngle = 90;
            this.eppView.TitleText = "學生類別";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = global::SmartSchool.Properties.Resources.loading5;
            this.pictureBox1.Location = new System.Drawing.Point(77, 208);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(187, 418);
            this.panel2.TabIndex = 1;
            // 
            // eppViewSelector
            // 
            this.eppViewSelector.CanvasColor = System.Drawing.SystemColors.Control;
            this.eppViewSelector.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.TopToBottom;
            this.eppViewSelector.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.eppViewSelector.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.eppViewSelector.Location = new System.Drawing.Point(0, 494);
            this.eppViewSelector.Name = "eppViewSelector";
            this.eppViewSelector.Size = new System.Drawing.Size(187, 85);
            this.eppViewSelector.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.eppViewSelector.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppViewSelector.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppViewSelector.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.eppViewSelector.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.eppViewSelector.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.eppViewSelector.Style.GradientAngle = 90;
            this.eppViewSelector.TabIndex = 1;
            this.eppViewSelector.TitleHeight = 23;
            this.eppViewSelector.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.eppViewSelector.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppViewSelector.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppViewSelector.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.eppViewSelector.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.eppViewSelector.TitleStyle.CornerDiameter = 2;
            this.eppViewSelector.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.eppViewSelector.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.eppViewSelector.TitleStyle.GradientAngle = 90;
            this.eppViewSelector.TitleText = "檢視模式";
            // 
            // btnFilter
            // 
            this.btnFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFilter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFilter.FocusCuesEnabled = false;
            this.btnFilter.Location = new System.Drawing.Point(0, 30);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(187, 23);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "篩選";
            this.btnFilter.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            // 
            // btnTempory
            // 
            this.btnTempory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTempory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTempory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTempory.FocusCuesEnabled = false;
            this.btnTempory.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.btnTempory.Location = new System.Drawing.Point(0, 0);
            this.btnTempory.Name = "btnTempory";
            this.btnTempory.Size = new System.Drawing.Size(187, 30);
            this.btnTempory.TabIndex = 0;
            this.btnTempory.Text = "晚點再處理";
            this.btnTempory.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnTempory.Click += new System.EventHandler(this.btnTempory_Click);
            this.btnTempory.CheckedChanged += new System.EventHandler(this.btnTempory_CheckedChanged);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Checked = true;
            this.buttonItem1.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem1.Image") ) );
            this.buttonItem1.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.OptionGroup = "navBar";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panel3);
            this.panelContent.Controls.Add(this.splitterListDetial);
            this.panelContent.Controls.Add(this.panelList);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(189, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(699, 637);
            this.panelContent.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(257, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(442, 637);
            this.panel3.TabIndex = 4;
            // 
            // splitterListDetial
            // 
            this.splitterListDetial.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.splitterListDetial.BackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.splitterListDetial.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitterListDetial.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.splitterListDetial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterListDetial.Expandable = false;
            this.splitterListDetial.ExpandableControl = this.panel3;
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
            this.splitterListDetial.Size = new System.Drawing.Size(3, 637);
            this.splitterListDetial.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.splitterListDetial.TabIndex = 3;
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
            this.panelList.Size = new System.Drawing.Size(254, 637);
            this.panelList.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.contextMenuBar2);
            this.panel1.Controls.Add(this.contextMenuBar1);
            this.panel1.Controls.Add(this.dgvDisplayList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 606);
            this.panel1.TabIndex = 17;
            // 
            // contextMenuBar2
            // 
            this.contextMenuBar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.contextMenuBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem3});
            this.contextMenuBar2.Location = new System.Drawing.Point(31, 248);
            this.contextMenuBar2.Name = "contextMenuBar2";
            this.contextMenuBar2.Size = new System.Drawing.Size(75, 27);
            this.contextMenuBar2.Stretch = true;
            this.contextMenuBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.contextMenuBar2.TabIndex = 2;
            this.contextMenuBar2.TabStop = false;
            this.contextMenuBar2.Text = "contextMenuBar2";
            // 
            // buttonItem3
            // 
            this.buttonItem3.AutoCollapseOnClick = false;
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.StopPulseOnMouseOver = false;
            this.buttonItem3.Text = "欄位選單";
            this.buttonItem3.PopupClose += new System.EventHandler(this.buttonItem3_PopupClose);
            this.buttonItem3.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.buttonItem3_PopupOpen);
            // 
            // dgvDisplayList
            // 
            this.dgvDisplayList.AllowUserToAddRows = false;
            this.dgvDisplayList.AllowUserToDeleteRows = false;
            this.dgvDisplayList.AllowUserToOrderColumns = true;
            this.dgvDisplayList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvDisplayList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDisplayList.BackgroundColor = System.Drawing.Color.White;
            this.dgvDisplayList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDisplayList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDisplayList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvDisplayList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.contextMenuBar2.SetContextMenuEx(this.dgvDisplayList, this.buttonItem3);
            this.contextMenuBar1.SetContextMenuEx(this.dgvDisplayList, this.buttonItem2);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDisplayList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDisplayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDisplayList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDisplayList.EnableHeadersVisualStyles = false;
            this.dgvDisplayList.GridColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 208 ) ) ) ), ( (int)( ( (byte)( 215 ) ) ) ), ( (int)( ( (byte)( 229 ) ) ) ));
            this.dgvDisplayList.HighlightSelectedColumnHeaders = false;
            this.dgvDisplayList.Location = new System.Drawing.Point(0, 0);
            this.dgvDisplayList.Name = "dgvDisplayList";
            this.dgvDisplayList.ReadOnly = true;
            this.dgvDisplayList.RowHeadersVisible = false;
            this.dgvDisplayList.RowTemplate.Height = 24;
            this.dgvDisplayList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDisplayList.ShowCellErrors = false;
            this.dgvDisplayList.ShowEditingIcon = false;
            this.dgvDisplayList.ShowRowErrors = false;
            this.dgvDisplayList.Size = new System.Drawing.Size(254, 606);
            this.dgvDisplayList.StandardTab = true;
            this.dgvDisplayList.TabIndex = 13;
            this.dgvDisplayList.Tag = "2";
            this.dgvDisplayList.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvDisplayList_SortCompare);
            this.dgvDisplayList.Sorted += new System.EventHandler(this.dgvDisplayList_Sorted);
            this.dgvDisplayList.MouseHover += new System.EventHandler(this.SetFocus);
            this.dgvDisplayList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvDisplayList_RowsAdded);
            this.dgvDisplayList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDisplayList_DataError);
            this.dgvDisplayList.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvDisplayList_RowsRemoved);
            this.dgvDisplayList.SelectionChanged += new System.EventHandler(this.dgvDisplayList_SelectionChanged);
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2});
            this.contextMenuBar1.Location = new System.Drawing.Point(31, 167);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(87, 27);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.contextMenuBar1.TabIndex = 2;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            // 
            // buttonItem2
            // 
            this.buttonItem2.AutoExpandOnClick = true;
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.buttonItem2.Text = "右毽選單";
            this.buttonItem2.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.buttonItem2_PopupOpen);
            // 
            // itemContainer1
            // 
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.addToTemp,
            this.removeFormTemp});
            // 
            // addToTemp
            // 
            this.addToTemp.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.addToTemp.Image = ( (System.Drawing.Image)( resources.GetObject("addToTemp.Image") ) );
            this.addToTemp.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.addToTemp.ImagePaddingHorizontal = 8;
            this.addToTemp.Name = "addToTemp";
            this.addToTemp.Text = "加入至待處理";
            this.addToTemp.Click += new System.EventHandler(this.addToTemp_Click);
            // 
            // removeFormTemp
            // 
            this.removeFormTemp.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.removeFormTemp.Image = ( (System.Drawing.Image)( resources.GetObject("removeFormTemp.Image") ) );
            this.removeFormTemp.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.removeFormTemp.ImagePaddingHorizontal = 8;
            this.removeFormTemp.Name = "removeFormTemp";
            this.removeFormTemp.Text = "移出待處理";
            this.removeFormTemp.Click += new System.EventHandler(this.memoveFormTemp_Click);
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
            this.panelEx1.Controls.Add(this.btnSearch);
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
            this.txtSearch.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.txtSearch.Location = new System.Drawing.Point(3, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(194, 25);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.WatermarkColor = System.Drawing.Color.Peru;
            this.txtSearch.WatermarkFont = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // buttonExpand
            // 
            this.buttonExpand.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonExpand.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonExpand.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonExpand.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.buttonExpand.Location = new System.Drawing.Point(227, 5);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(21, 21);
            this.buttonExpand.TabIndex = 2;
            this.buttonExpand.Text = ">>";
            this.buttonExpand.Tooltip = "最大化";
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Image = ( (System.Drawing.Image)( resources.GetObject("btnSearch.Image") ) );
            this.btnSearch.Location = new System.Drawing.Point(200, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(24, 21);
            this.btnSearch.SplitButton = true;
            this.btnSearch.SubItemsExpandWidth = 17;
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Tooltip = "搜尋";
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem4.Image") ) );
            this.buttonItem4.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "加入至待處理";
            // 
            // GeneralEntityView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.navigationPane1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.Name = "GeneralEntityView";
            this.Size = new System.Drawing.Size(888, 637);
            this.navigationPane1.ResumeLayout(false);
            this.navigationPanePanel1.ResumeLayout(false);
            this.eppView.ResumeLayout(false);
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
            this.panelContent.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ( (System.ComponentModel.ISupportInitialize)( this.contextMenuBar2 ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.dgvDisplayList ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.contextMenuBar1 ) ).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected DevComponents.DotNetBar.NavigationPane navigationPane1;
        protected DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel1;
        protected DevComponents.DotNetBar.ButtonItem buttonItem1;
        protected System.Windows.Forms.Panel panelContent;
        protected DevComponents.DotNetBar.ExpandableSplitter splitterListDetial;
        protected System.Windows.Forms.Panel panelList;
        protected System.Windows.Forms.Panel panel1;
        protected SmartSchool.Common.DataGridViewEx dgvDisplayList;
        protected DevComponents.DotNetBar.ExpandableSplitter expandableSplitter3;
        protected DevComponents.DotNetBar.PanelEx panelEx1;
        protected DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        protected DevComponents.DotNetBar.ButtonX buttonExpand;
        protected DevComponents.DotNetBar.ButtonX btnSearch;
        protected DevComponents.DotNetBar.ExpandablePanel eppViewSelector;
        protected DevComponents.DotNetBar.ExpandablePanel eppView;
        protected System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.PictureBox pictureBox1;
        protected ButtonX btnTempory;
        protected ButtonX btnFilter;
        protected System.Windows.Forms.Panel panel3;
        protected ContextMenuBar contextMenuBar1;
        protected ItemContainer itemContainer1;
        protected ButtonItem buttonItem2;
        protected ButtonItem addToTemp;
        private ContextMenuBar contextMenuBar2;
        private ButtonItem buttonItem3;
        protected ButtonItem removeFormTemp;
        protected ButtonItem buttonItem4;
    }
}
