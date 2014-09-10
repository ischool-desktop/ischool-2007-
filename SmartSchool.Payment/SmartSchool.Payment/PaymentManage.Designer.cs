namespace SmartSchool.Payment
{
    partial class PaymentManage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentManage));
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ipPayments = new DevComponents.DotNetBar.ItemPanel();
            this.PaymentLoading = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnReports = new DevComponents.DotNetBar.ButtonX();
            this.btnPayStatus = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.tabDetail = new DevComponents.DotNetBar.TabControl();
            this.tcpOverview = new DevComponents.DotNetBar.TabControlPanel();
            this.panelOverview = new System.Windows.Forms.Panel();
            this.txtHelp = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblTotalProgress = new DevComponents.DotNetBar.LabelX();
            this.pgTotalProgress = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lblOverLimit = new DevComponents.DotNetBar.LabelX();
            this.lblBankName = new DevComponents.DotNetBar.LabelX();
            this.lblUnpaidCount = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.lblPaidCount = new DevComponents.DotNetBar.LabelX();
            this.lblTotalAmount = new DevComponents.DotNetBar.LabelX();
            this.lblDetailCount = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.lblPaidAmount = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.tbOverview = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnExportVA = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnViewBill = new DevComponents.DotNetBar.ButtonX();
            this.btnUploadBill = new DevComponents.DotNetBar.ButtonX();
            this.btnSaveBill = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboBillBatchs = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tcStudentList = new DevComponents.DotNetBar.TabControlPanel();
            this.detaillist = new SmartSchool.Payment.StudentList.PaymentDetailView();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btiGenBill = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.txtFilter = new DevComponents.DotNetBar.TextBoxItem();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ipPayments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentLoading)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabDetail)).BeginInit();
            this.tabDetail.SuspendLayout();
            this.tcpOverview.SuspendLayout();
            this.panelOverview.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.tcStudentList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            // 
            // mainContainer.Panel1
            // 
            this.mainContainer.Panel1.Controls.Add(this.panel2);
            this.mainContainer.Panel1.Controls.Add(this.panel1);
            // 
            // mainContainer.Panel2
            // 
            this.mainContainer.Panel2.Controls.Add(this.tabDetail);
            this.mainContainer.Size = new System.Drawing.Size(812, 514);
            this.mainContainer.SplitterDistance = 207;
            this.mainContainer.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ipPayments);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 480);
            this.panel2.TabIndex = 3;
            // 
            // ipPayments
            // 
            // 
            // 
            // 
            this.ipPayments.BackgroundStyle.BackColor = System.Drawing.Color.White;
            this.ipPayments.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipPayments.BackgroundStyle.BorderBottomWidth = 1;
            this.ipPayments.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.ipPayments.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipPayments.BackgroundStyle.BorderLeftWidth = 1;
            this.ipPayments.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipPayments.BackgroundStyle.BorderRightWidth = 1;
            this.ipPayments.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ipPayments.BackgroundStyle.BorderTopWidth = 1;
            this.ipPayments.BackgroundStyle.PaddingBottom = 1;
            this.ipPayments.BackgroundStyle.PaddingLeft = 1;
            this.ipPayments.BackgroundStyle.PaddingRight = 1;
            this.ipPayments.BackgroundStyle.PaddingTop = 1;
            this.ipPayments.Controls.Add(this.PaymentLoading);
            this.ipPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipPayments.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.ipPayments.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ipPayments.Location = new System.Drawing.Point(0, 0);
            this.ipPayments.Name = "ipPayments";
            this.ipPayments.Size = new System.Drawing.Size(207, 378);
            this.ipPayments.TabIndex = 1;
            this.ipPayments.Text = "itemPanel1";
            // 
            // PaymentLoading
            // 
            this.PaymentLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PaymentLoading.Image = global::SmartSchool.Payment.Properties.Resources.loading;
            this.PaymentLoading.Location = new System.Drawing.Point(77, 163);
            this.PaymentLoading.Name = "PaymentLoading";
            this.PaymentLoading.Size = new System.Drawing.Size(32, 32);
            this.PaymentLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PaymentLoading.TabIndex = 0;
            this.PaymentLoading.TabStop = false;
            this.PaymentLoading.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnImport);
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Controls.Add(this.btnReports);
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 378);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 102);
            this.panel3.TabIndex = 3;
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImport.Location = new System.Drawing.Point(0, 0);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(207, 25);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "匯入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExport.Location = new System.Drawing.Point(0, 25);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(207, 26);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "匯出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnReports
            // 
            this.btnReports.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReports.AutoExpandOnClick = true;
            this.btnReports.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReports.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnReports.Location = new System.Drawing.Point(0, 51);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(207, 25);
            this.btnReports.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnPayStatus});
            this.btnReports.TabIndex = 3;
            this.btnReports.Text = "　報表";
            // 
            // btnPayStatus
            // 
            this.btnPayStatus.GlobalItem = false;
            this.btnPayStatus.ImagePaddingHorizontal = 8;
            this.btnPayStatus.Name = "btnPayStatus";
            this.btnPayStatus.Text = "繳費狀況表";
            this.btnPayStatus.Click += new System.EventHandler(this.btnPayStatus_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDelete.Location = new System.Drawing.Point(0, 76);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(207, 26);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboSchoolYear);
            this.panel1.Controls.Add(this.cboSemester);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 34);
            this.panel1.TabIndex = 2;
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.ItemHeight = 19;
            this.cboSchoolYear.Location = new System.Drawing.Point(5, 4);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(106, 25);
            this.cboSchoolYear.TabIndex = 0;
            this.cboSchoolYear.SelectedIndexChanged += new System.EventHandler(this.cboSchoolYear_SelectedIndexChanged);
            // 
            // cboSemester
            // 
            this.cboSemester.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 19;
            this.cboSemester.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboSemester.Location = new System.Drawing.Point(117, 4);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(91, 25);
            this.cboSemester.TabIndex = 0;
            this.cboSemester.SelectedIndexChanged += new System.EventHandler(this.cboSemester_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "2";
            // 
            // tabDetail
            // 
            this.tabDetail.BackColor = System.Drawing.Color.Transparent;
            this.tabDetail.CanReorderTabs = true;
            this.tabDetail.Controls.Add(this.tcpOverview);
            this.tabDetail.Controls.Add(this.tabControlPanel1);
            this.tabDetail.Controls.Add(this.tcStudentList);
            this.tabDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetail.Location = new System.Drawing.Point(0, 0);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.SelectedTabFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabDetail.SelectedTabIndex = 0;
            this.tabDetail.Size = new System.Drawing.Size(601, 514);
            this.tabDetail.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabDetail.TabIndex = 0;
            this.tabDetail.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabDetail.Tabs.Add(this.tbOverview);
            this.tabDetail.Tabs.Add(this.tabItem2);
            this.tabDetail.Tabs.Add(this.tabItem1);
            this.tabDetail.Text = "總覽";
            // 
            // tcpOverview
            // 
            this.tcpOverview.Controls.Add(this.panelOverview);
            this.tcpOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcpOverview.Location = new System.Drawing.Point(0, 28);
            this.tcpOverview.Name = "tcpOverview";
            this.tcpOverview.Padding = new System.Windows.Forms.Padding(1);
            this.tcpOverview.Size = new System.Drawing.Size(601, 486);
            this.tcpOverview.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tcpOverview.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tcpOverview.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tcpOverview.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tcpOverview.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tcpOverview.Style.GradientAngle = 90;
            this.tcpOverview.TabIndex = 4;
            this.tcpOverview.TabItem = this.tbOverview;
            // 
            // panelOverview
            // 
            this.panelOverview.BackColor = System.Drawing.Color.Transparent;
            this.panelOverview.Controls.Add(this.txtHelp);
            this.panelOverview.Controls.Add(this.lblTotalProgress);
            this.panelOverview.Controls.Add(this.pgTotalProgress);
            this.panelOverview.Controls.Add(this.labelX6);
            this.panelOverview.Controls.Add(this.lblOverLimit);
            this.panelOverview.Controls.Add(this.lblBankName);
            this.panelOverview.Controls.Add(this.lblUnpaidCount);
            this.panelOverview.Controls.Add(this.labelX8);
            this.panelOverview.Controls.Add(this.lblPaidCount);
            this.panelOverview.Controls.Add(this.lblTotalAmount);
            this.panelOverview.Controls.Add(this.lblDetailCount);
            this.panelOverview.Controls.Add(this.labelX10);
            this.panelOverview.Controls.Add(this.lblPaidAmount);
            this.panelOverview.Controls.Add(this.labelX4);
            this.panelOverview.Controls.Add(this.labelX7);
            this.panelOverview.Controls.Add(this.labelX12);
            this.panelOverview.Controls.Add(this.labelX14);
            this.panelOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOverview.Location = new System.Drawing.Point(1, 1);
            this.panelOverview.Name = "panelOverview";
            this.panelOverview.Size = new System.Drawing.Size(599, 484);
            this.panelOverview.TabIndex = 3;
            // 
            // txtHelp
            // 
            this.txtHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHelp.BackColor = System.Drawing.SystemColors.InactiveCaption;
            // 
            // 
            // 
            this.txtHelp.Border.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground2;
            this.txtHelp.Border.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.txtHelp.Border.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtHelp.Border.BorderBottomWidth = 1;
            this.txtHelp.Border.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground2;
            this.txtHelp.Border.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtHelp.Border.BorderLeftWidth = 1;
            this.txtHelp.Border.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtHelp.Border.BorderRightWidth = 1;
            this.txtHelp.Border.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtHelp.Border.BorderTopWidth = 1;
            this.txtHelp.Border.Class = "TextBoxBorder";
            this.txtHelp.Border.TextShadowOffset = new System.Drawing.Point(5, 5);
            this.txtHelp.Location = new System.Drawing.Point(20, 408);
            this.txtHelp.Multiline = true;
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.ReadOnly = true;
            this.txtHelp.Size = new System.Drawing.Size(564, 67);
            this.txtHelp.TabIndex = 30;
            this.txtHelp.Text = resources.GetString("txtHelp.Text");
            this.txtHelp.Visible = false;
            // 
            // lblTotalProgress
            // 
            this.lblTotalProgress.AutoSize = true;
            this.lblTotalProgress.Location = new System.Drawing.Point(20, 14);
            this.lblTotalProgress.Name = "lblTotalProgress";
            this.lblTotalProgress.Size = new System.Drawing.Size(74, 19);
            this.lblTotalProgress.TabIndex = 1;
            this.lblTotalProgress.Text = "收費總進度";
            // 
            // pgTotalProgress
            // 
            this.pgTotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgTotalProgress.Location = new System.Drawing.Point(20, 40);
            this.pgTotalProgress.Name = "pgTotalProgress";
            this.pgTotalProgress.Size = new System.Drawing.Size(562, 23);
            this.pgTotalProgress.TabIndex = 0;
            this.pgTotalProgress.Text = "pgTotalProgress";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.SystemColors.Control;
            this.labelX6.Location = new System.Drawing.Point(22, 75);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(134, 23);
            this.labelX6.TabIndex = 1;
            this.labelX6.Text = "應收總金額(新台幣)";
            // 
            // lblOverLimit
            // 
            this.lblOverLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOverLimit.BackColor = System.Drawing.SystemColors.Control;
            this.lblOverLimit.Location = new System.Drawing.Point(162, 220);
            this.lblOverLimit.Name = "lblOverLimit";
            this.lblOverLimit.Size = new System.Drawing.Size(422, 23);
            this.lblOverLimit.TabIndex = 1;
            this.lblOverLimit.Text = "0";
            // 
            // lblBankName
            // 
            this.lblBankName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBankName.BackColor = System.Drawing.SystemColors.Control;
            this.lblBankName.Location = new System.Drawing.Point(162, 249);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(422, 23);
            this.lblBankName.TabIndex = 1;
            // 
            // lblUnpaidCount
            // 
            this.lblUnpaidCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnpaidCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblUnpaidCount.Location = new System.Drawing.Point(162, 191);
            this.lblUnpaidCount.Name = "lblUnpaidCount";
            this.lblUnpaidCount.Size = new System.Drawing.Size(422, 23);
            this.lblUnpaidCount.TabIndex = 1;
            this.lblUnpaidCount.Text = "0";
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.SystemColors.Control;
            this.labelX8.Location = new System.Drawing.Point(22, 104);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(134, 23);
            this.labelX8.TabIndex = 1;
            this.labelX8.Text = "已收金額(新台幣)";
            // 
            // lblPaidCount
            // 
            this.lblPaidCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaidCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblPaidCount.Location = new System.Drawing.Point(162, 162);
            this.lblPaidCount.Name = "lblPaidCount";
            this.lblPaidCount.Size = new System.Drawing.Size(422, 23);
            this.lblPaidCount.TabIndex = 1;
            this.lblPaidCount.Text = "0";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmount.BackColor = System.Drawing.SystemColors.Control;
            this.lblTotalAmount.Location = new System.Drawing.Point(162, 75);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(422, 23);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "0";
            // 
            // lblDetailCount
            // 
            this.lblDetailCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetailCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblDetailCount.Location = new System.Drawing.Point(162, 133);
            this.lblDetailCount.Name = "lblDetailCount";
            this.lblDetailCount.Size = new System.Drawing.Size(422, 23);
            this.lblDetailCount.TabIndex = 1;
            this.lblDetailCount.Text = "0";
            // 
            // labelX10
            // 
            this.labelX10.BackColor = System.Drawing.SystemColors.Control;
            this.labelX10.Location = new System.Drawing.Point(22, 133);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(134, 23);
            this.labelX10.TabIndex = 1;
            this.labelX10.Text = "學生總數";
            // 
            // lblPaidAmount
            // 
            this.lblPaidAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaidAmount.BackColor = System.Drawing.SystemColors.Control;
            this.lblPaidAmount.Location = new System.Drawing.Point(162, 104);
            this.lblPaidAmount.Name = "lblPaidAmount";
            this.lblPaidAmount.Size = new System.Drawing.Size(422, 23);
            this.lblPaidAmount.TabIndex = 1;
            this.lblPaidAmount.Text = "0";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.SystemColors.Control;
            this.labelX4.Location = new System.Drawing.Point(22, 220);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(134, 23);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "繳費超額人數";
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.SystemColors.Control;
            this.labelX7.Location = new System.Drawing.Point(22, 249);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(134, 23);
            this.labelX7.TabIndex = 1;
            this.labelX7.Text = "銀行組態名稱";
            // 
            // labelX12
            // 
            this.labelX12.BackColor = System.Drawing.SystemColors.Control;
            this.labelX12.Location = new System.Drawing.Point(22, 162);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(134, 23);
            this.labelX12.TabIndex = 1;
            this.labelX12.Text = "已繳人數";
            // 
            // labelX14
            // 
            this.labelX14.BackColor = System.Drawing.SystemColors.Control;
            this.labelX14.Location = new System.Drawing.Point(22, 191);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(134, 23);
            this.labelX14.TabIndex = 1;
            this.labelX14.Text = "未繳人數";
            // 
            // tbOverview
            // 
            this.tbOverview.AttachedControl = this.tcpOverview;
            this.tbOverview.Name = "tbOverview";
            this.tbOverview.Text = "總覽";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.groupPanel1);
            this.tabControlPanel1.Controls.Add(this.labelX5);
            this.tabControlPanel1.Controls.Add(this.labelX3);
            this.tabControlPanel1.Controls.Add(this.labelX2);
            this.tabControlPanel1.Controls.Add(this.btnViewBill);
            this.tabControlPanel1.Controls.Add(this.btnUploadBill);
            this.tabControlPanel1.Controls.Add(this.btnSaveBill);
            this.tabControlPanel1.Controls.Add(this.labelX1);
            this.tabControlPanel1.Controls.Add(this.cboBillBatchs);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 28);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(601, 486);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 5;
            this.tabControlPanel1.TabItem = this.tabItem1;
            this.tabControlPanel1.DoubleClick += new System.EventHandler(this.tabControlPanel1_DoubleClick);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnExportVA);
            this.groupPanel1.Location = new System.Drawing.Point(29, 343);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(542, 131);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 29;
            this.groupPanel1.Text = "密技功能";
            this.groupPanel1.Visible = false;
            // 
            // btnExportVA
            // 
            this.btnExportVA.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportVA.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportVA.Location = new System.Drawing.Point(23, 3);
            this.btnExportVA.Name = "btnExportVA";
            this.btnExportVA.Size = new System.Drawing.Size(115, 23);
            this.btnExportVA.TabIndex = 0;
            this.btnExportVA.Text = "匯出繳費單資料";
            this.btnExportVA.Click += new System.EventHandler(this.btnExportVA_Click);
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            this.labelX5.Location = new System.Drawing.Point(30, 245);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(558, 19);
            this.labelX5.TabIndex = 28;
            this.labelX5.Text = "依據目前設定的繳費單樣版，將選擇批次的繳費單資料合併輸出成包含條碼的 Word 繳費單。";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(29, 180);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(315, 19);
            this.labelX3.TabIndex = 28;
            this.labelX3.Text = "上傳自定的繳費單樣版，並設定成選擇批次的樣版。";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(29, 114);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(343, 19);
            this.labelX2.TabIndex = 28;
            this.labelX2.Text = "下載選擇批次所使用的樣版，儲存成 Word 文件後開啟。";
            // 
            // btnViewBill
            // 
            this.btnViewBill.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewBill.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewBill.Location = new System.Drawing.Point(29, 90);
            this.btnViewBill.Name = "btnViewBill";
            this.btnViewBill.Size = new System.Drawing.Size(141, 23);
            this.btnViewBill.TabIndex = 3;
            this.btnViewBill.Text = "檢視目前樣版";
            this.btnViewBill.Click += new System.EventHandler(this.btnViewBill_Click);
            // 
            // btnUploadBill
            // 
            this.btnUploadBill.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUploadBill.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUploadBill.Location = new System.Drawing.Point(29, 155);
            this.btnUploadBill.Name = "btnUploadBill";
            this.btnUploadBill.Size = new System.Drawing.Size(141, 23);
            this.btnUploadBill.TabIndex = 3;
            this.btnUploadBill.Text = "上傳樣版";
            this.btnUploadBill.Click += new System.EventHandler(this.btnUploadBill_Click);
            // 
            // btnSaveBill
            // 
            this.btnSaveBill.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveBill.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveBill.Location = new System.Drawing.Point(30, 221);
            this.btnSaveBill.Name = "btnSaveBill";
            this.btnSaveBill.Size = new System.Drawing.Size(141, 23);
            this.btnSaveBill.TabIndex = 3;
            this.btnSaveBill.Text = "列印繳費單";
            this.btnSaveBill.Click += new System.EventHandler(this.btnSaveBill_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(29, 27);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(101, 19);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "繳費單資料批次";
            // 
            // cboBillBatchs
            // 
            this.cboBillBatchs.DisplayMember = "Text";
            this.cboBillBatchs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBillBatchs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBillBatchs.FormattingEnabled = true;
            this.cboBillBatchs.ItemHeight = 19;
            this.cboBillBatchs.Location = new System.Drawing.Point(30, 49);
            this.cboBillBatchs.Name = "cboBillBatchs";
            this.cboBillBatchs.Size = new System.Drawing.Size(541, 25);
            this.cboBillBatchs.TabIndex = 0;
            this.cboBillBatchs.SelectedIndexChanged += new System.EventHandler(this.cboBillBatchs_SelectedIndexChanged);
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "繳費單";
            // 
            // tcStudentList
            // 
            this.tcStudentList.Controls.Add(this.detaillist);
            this.tcStudentList.Controls.Add(this.bar1);
            this.tcStudentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStudentList.Location = new System.Drawing.Point(0, 28);
            this.tcStudentList.Name = "tcStudentList";
            this.tcStudentList.Padding = new System.Windows.Forms.Padding(1);
            this.tcStudentList.Size = new System.Drawing.Size(601, 486);
            this.tcStudentList.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tcStudentList.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tcStudentList.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tcStudentList.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tcStudentList.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tcStudentList.Style.GradientAngle = 90;
            this.tcStudentList.TabIndex = 2;
            this.tcStudentList.TabItem = this.tabItem2;
            // 
            // detaillist
            // 
            this.detaillist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detaillist.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.detaillist.Location = new System.Drawing.Point(1, 28);
            this.detaillist.Margin = new System.Windows.Forms.Padding(4);
            this.detaillist.Name = "detaillist";
            this.detaillist.Size = new System.Drawing.Size(599, 457);
            this.detaillist.TabIndex = 2;
            // 
            // bar1
            // 
            this.bar1.AccessibleDescription = "bar1 (bar1)";
            this.bar1.AccessibleName = "bar1";
            this.bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.FadeEffect = true;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btiGenBill,
            this.buttonItem1,
            this.txtFilter});
            this.bar1.Location = new System.Drawing.Point(1, 1);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(599, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.bar1.TabIndex = 3;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btiGenBill
            // 
            this.btiGenBill.BeginGroup = true;
            this.btiGenBill.ImagePaddingHorizontal = 8;
            this.btiGenBill.Name = "btiGenBill";
            this.btiGenBill.Text = "產生繳費單資料";
            this.btiGenBill.Click += new System.EventHandler(this.btiGenBill_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.BeginGroup = true;
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "過濾";
            // 
            // txtFilter
            // 
            this.txtFilter.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.TextBoxWidth = 120;
            this.txtFilter.InputTextChanged += new DevComponents.DotNetBar.TextBoxItem.TextChangedEventHandler(this.txtFilter_InputTextChanged);
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tcStudentList;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "收費明細";
            // 
            // colItem
            // 
            this.colItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colItem.HeaderText = "項目";
            this.colItem.Name = "colItem";
            // 
            // PaymentManage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(812, 514);
            this.Controls.Add(this.mainContainer);
            this.Name = "PaymentManage";
            this.Text = "收費管理";
            this.Load += new System.EventHandler(this.PaymentManage_Load);
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel2.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ipPayments.ResumeLayout(false);
            this.ipPayments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentLoading)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabDetail)).EndInit();
            this.tabDetail.ResumeLayout(false);
            this.tcpOverview.ResumeLayout(false);
            this.panelOverview.ResumeLayout(false);
            this.panelOverview.PerformLayout();
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.tcStudentList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainContainer;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ItemPanel ipPayments;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.TabControl tabDetail;
        private DevComponents.DotNetBar.TabControlPanel tcpOverview;
        private DevComponents.DotNetBar.TabItem tbOverview;
        private DevComponents.DotNetBar.TabControlPanel tcStudentList;
        private SmartSchool.Payment.StudentList.PaymentDetailView detaillist;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private DevComponents.DotNetBar.LabelX lblPaidAmount;
        private DevComponents.DotNetBar.LabelX lblTotalAmount;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX lblTotalProgress;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgTotalProgress;
        private DevComponents.DotNetBar.LabelX lblDetailCount;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX lblUnpaidCount;
        private DevComponents.DotNetBar.LabelX lblPaidCount;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.LabelX labelX12;
        private System.Windows.Forms.PictureBox PaymentLoading;
        private System.Windows.Forms.Panel panelOverview;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.TextBoxItem txtFilter;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboBillBatchs;
        private DevComponents.DotNetBar.ButtonX btnSaveBill;
        private DevComponents.DotNetBar.LabelX lblBankName;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonItem btiGenBill;
        private DevComponents.DotNetBar.ButtonX btnViewBill;
        private DevComponents.DotNetBar.ButtonX btnUploadBill;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtHelp;
        private DevComponents.DotNetBar.LabelX lblOverLimit;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnExportVA;
        private DevComponents.DotNetBar.ButtonX btnReports;
        private DevComponents.DotNetBar.ButtonItem btnPayStatus;
    }
}