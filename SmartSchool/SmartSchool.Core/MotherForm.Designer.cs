namespace SmartSchool
{
    partial class MotherForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotherForm));
            this.ribbonControl1 = new DevComponents.DotNetBar.RibbonControl();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.lblShowSecTab = new DevComponents.DotNetBar.LabelItem();
            this.btnShowHidden = new DevComponents.DotNetBar.ButtonItem();
            this.lblProcess = new DevComponents.DotNetBar.LabelItem();
            this.buttonChangeStyle = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2007Blue = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2007Black = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2007Silver = new DevComponents.DotNetBar.ButtonItem();
            this.office2007StartButton2 = new DevComponents.DotNetBar.Office2007StartButton();
            this.btnQueryLog = new DevComponents.DotNetBar.ButtonItem();
            this.btnAllLog = new DevComponents.DotNetBar.ButtonItem();
            this.btnSchoolInfo = new DevComponents.DotNetBar.ButtonItem();
            this.btnEPaperManager = new DevComponents.DotNetBar.ButtonItem();
            this.btnSurveyMan = new DevComponents.DotNetBar.ButtonItem();
            this.btnFeedbackTop = new DevComponents.DotNetBar.ButtonItem();
            this.btnFeedback = new DevComponents.DotNetBar.ButtonItem();
            this.btnNews = new DevComponents.DotNetBar.ButtonItem();
            this.btnVote = new DevComponents.DotNetBar.ButtonItem();
            this.btnSecurity = new DevComponents.DotNetBar.ButtonItem();
            this.btnChangePassword = new DevComponents.DotNetBar.ButtonItem();
            this.btnUserManagement = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoleManagement = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonTabItemGroup1 = new DevComponents.DotNetBar.RibbonTabItemGroup();
            this.office2007StartButton1 = new DevComponents.DotNetBar.Office2007StartButton();
            this.navigationPanePanel1 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.lblBarMessage = new DevComponents.DotNetBar.LabelItem();
            this.progressBarMessage = new DevComponents.DotNetBar.ProgressBarItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.navigationPane1 = new DevComponents.DotNetBar.NavigationPane();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ( (System.ComponentModel.ISupportInitialize)( this.bar1 ) ).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.CanCustomize = false;
            this.ribbonControl1.CaptionVisible = true;
            this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonControl1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2,
            this.buttonChangeStyle});
            this.ribbonControl1.KeyTipsFont = new System.Drawing.Font("微軟正黑體", 7F);
            this.ribbonControl1.Location = new System.Drawing.Point(4, 1);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.ribbonControl1.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.office2007StartButton2});
            this.ribbonControl1.Size = new System.Drawing.Size(1008, 163);
            this.ribbonControl1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonControl1.TabGroupHeight = 14;
            this.ribbonControl1.TabGroups.AddRange(new DevComponents.DotNetBar.RibbonTabItemGroup[] {
            this.ribbonTabItemGroup1});
            this.ribbonControl1.TabGroupsVisible = true;
            this.ribbonControl1.TabIndex = 0;
            this.ribbonControl1.Text = "ribbonControl1";
            this.ribbonControl1.ExpandedChanged += new System.EventHandler(this.ribbonControl1_ExpandedChanged);
            // 
            // buttonItem2
            // 
            this.buttonItem2.AutoCollapseOnClick = false;
            this.buttonItem2.AutoExpandOnClick = true;
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblShowSecTab,
            this.btnShowHidden,
            this.lblProcess});
            this.buttonItem2.Text = "自訂";
            this.buttonItem2.ExpandChange += new System.EventHandler(this.buttonItem2_ExpandChange);
            // 
            // lblShowSecTab
            // 
            this.lblShowSecTab.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 221 ) ) ) ), ( (int)( ( (byte)( 231 ) ) ) ), ( (int)( ( (byte)( 238 ) ) ) ));
            this.lblShowSecTab.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.lblShowSecTab.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.lblShowSecTab.ForeColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 21 ) ) ) ), ( (int)( ( (byte)( 110 ) ) ) ));
            this.lblShowSecTab.Name = "lblShowSecTab";
            this.lblShowSecTab.PaddingBottom = 1;
            this.lblShowSecTab.PaddingLeft = 10;
            this.lblShowSecTab.PaddingTop = 1;
            this.lblShowSecTab.SingleLineColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 197 ) ) ) ), ( (int)( ( (byte)( 197 ) ) ) ), ( (int)( ( (byte)( 197 ) ) ) ));
            this.lblShowSecTab.Text = "隱藏項目將於第二頁籤中顯示";
            // 
            // btnShowHidden
            // 
            this.btnShowHidden.AutoCheckOnClick = true;
            this.btnShowHidden.AutoCollapseOnClick = false;
            this.btnShowHidden.Checked = true;
            this.btnShowHidden.ImagePaddingHorizontal = 8;
            this.btnShowHidden.Name = "btnShowHidden";
            this.btnShowHidden.Text = "顯示第二頁籤";
            this.btnShowHidden.CheckedChanged += new System.EventHandler(this.chkShowHidden_CheckedChanged);
            // 
            // lblProcess
            // 
            this.lblProcess.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 221 ) ) ) ), ( (int)( ( (byte)( 231 ) ) ) ), ( (int)( ( (byte)( 238 ) ) ) ));
            this.lblProcess.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.lblProcess.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.lblProcess.ForeColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 21 ) ) ) ), ( (int)( ( (byte)( 110 ) ) ) ));
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.PaddingBottom = 1;
            this.lblProcess.PaddingLeft = 10;
            this.lblProcess.PaddingTop = 1;
            this.lblProcess.SingleLineColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 197 ) ) ) ), ( (int)( ( (byte)( 197 ) ) ) ), ( (int)( ( (byte)( 197 ) ) ) ));
            this.lblProcess.Text = "labelItem2";
            // 
            // buttonChangeStyle
            // 
            this.buttonChangeStyle.AutoExpandOnClick = true;
            this.buttonChangeStyle.ImagePaddingHorizontal = 8;
            this.buttonChangeStyle.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.buttonChangeStyle.Name = "buttonChangeStyle";
            this.buttonChangeStyle.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonStyleOffice2007Blue,
            this.buttonStyleOffice2007Black,
            this.buttonStyleOffice2007Silver});
            this.buttonChangeStyle.Text = "色彩配置";
            this.buttonChangeStyle.PopupFinalized += new System.EventHandler(this.StyleReset);
            this.buttonChangeStyle.ExpandChange += new System.EventHandler(this.buttonChangeStyle_ExpandChange);
            // 
            // buttonStyleOffice2007Blue
            // 
            this.buttonStyleOffice2007Blue.Checked = true;
            this.buttonStyleOffice2007Blue.ImagePaddingHorizontal = 8;
            this.buttonStyleOffice2007Blue.Name = "buttonStyleOffice2007Blue";
            this.buttonStyleOffice2007Blue.OptionGroup = "style";
            this.buttonStyleOffice2007Blue.Text = "<font color=\"Blue\"><b>藍色</b></font>";
            this.buttonStyleOffice2007Blue.MouseHover += new System.EventHandler(this.StylePreview);
            this.buttonStyleOffice2007Blue.Click += new System.EventHandler(this.StyleChange);
            // 
            // buttonStyleOffice2007Black
            // 
            this.buttonStyleOffice2007Black.ImagePaddingHorizontal = 8;
            this.buttonStyleOffice2007Black.Name = "buttonStyleOffice2007Black";
            this.buttonStyleOffice2007Black.OptionGroup = "style";
            this.buttonStyleOffice2007Black.Text = "<font color=\"black\"><b>黑色</b></font>";
            this.buttonStyleOffice2007Black.MouseHover += new System.EventHandler(this.StylePreview);
            this.buttonStyleOffice2007Black.Click += new System.EventHandler(this.StyleChange);
            // 
            // buttonStyleOffice2007Silver
            // 
            this.buttonStyleOffice2007Silver.ImagePaddingHorizontal = 8;
            this.buttonStyleOffice2007Silver.Name = "buttonStyleOffice2007Silver";
            this.buttonStyleOffice2007Silver.OptionGroup = "style";
            this.buttonStyleOffice2007Silver.Text = "<font color=\"Silver\"><b>銀色</b></font>";
            this.buttonStyleOffice2007Silver.MouseHover += new System.EventHandler(this.StylePreview);
            this.buttonStyleOffice2007Silver.Click += new System.EventHandler(this.StyleChange);
            // 
            // office2007StartButton2
            // 
            this.office2007StartButton2.AutoExpandOnClick = true;
            this.office2007StartButton2.CanCustomize = false;
            this.office2007StartButton2.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.office2007StartButton2.ImagePaddingHorizontal = 2;
            this.office2007StartButton2.ImagePaddingVertical = 2;
            this.office2007StartButton2.Name = "office2007StartButton2";
            this.office2007StartButton2.ShowSubItems = false;
            this.office2007StartButton2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnQueryLog,
            this.btnAllLog,
            this.btnSchoolInfo,
            this.btnEPaperManager,
            this.btnSurveyMan,
            this.btnFeedbackTop,
            this.btnSecurity,
            this.buttonItem1});
            // 
            // btnQueryLog
            // 
            this.btnQueryLog.ImagePaddingHorizontal = 8;
            this.btnQueryLog.Name = "btnQueryLog";
            this.btnQueryLog.Text = "查詢個人日誌";
            this.btnQueryLog.Click += new System.EventHandler(this.btnQueryLog_Click);
            // 
            // btnAllLog
            // 
            this.btnAllLog.ImagePaddingHorizontal = 8;
            this.btnAllLog.Name = "btnAllLog";
            this.btnAllLog.Text = "查詢所有日誌";
            this.btnAllLog.Click += new System.EventHandler(this.btnAllLog_Click);
            // 
            // btnSchoolInfo
            // 
            this.btnSchoolInfo.ImagePaddingHorizontal = 8;
            this.btnSchoolInfo.Name = "btnSchoolInfo";
            this.btnSchoolInfo.Text = "編輯學校資訊";
            this.btnSchoolInfo.Click += new System.EventHandler(this.btnSchoolInfo_Click);
            // 
            // btnEPaperManager
            // 
            this.btnEPaperManager.ImagePaddingHorizontal = 8;
            this.btnEPaperManager.Name = "btnEPaperManager";
            this.btnEPaperManager.Text = "電子報表管理";
            this.btnEPaperManager.Click += new System.EventHandler(this.btnEPaperManager_Click);
            // 
            // btnSurveyMan
            // 
            this.btnSurveyMan.ImagePaddingHorizontal = 8;
            this.btnSurveyMan.Name = "btnSurveyMan";
            this.btnSurveyMan.Text = "問卷管理";
            this.btnSurveyMan.Click += new System.EventHandler(this.btnSurveyMan_Click);
            // 
            // btnFeedbackTop
            // 
            this.btnFeedbackTop.ImagePaddingHorizontal = 8;
            this.btnFeedbackTop.Name = "btnFeedbackTop";
            this.btnFeedbackTop.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnFeedback,
            this.btnNews,
            this.btnVote});
            this.btnFeedbackTop.Text = "使用者回饋";
            // 
            // btnFeedback
            // 
            this.btnFeedback.ImagePaddingHorizontal = 8;
            this.btnFeedback.Name = "btnFeedback";
            this.btnFeedback.Text = "問題回報與建議";
            this.btnFeedback.Click += new System.EventHandler(this.btnFeedback_Click);
            // 
            // btnNews
            // 
            this.btnNews.ImagePaddingHorizontal = 8;
            this.btnNews.Name = "btnNews";
            this.btnNews.Text = "最新消息";
            this.btnNews.Click += new System.EventHandler(this.btnNews_Click);
            // 
            // btnVote
            // 
            this.btnVote.ImagePaddingHorizontal = 8;
            this.btnVote.Name = "btnVote";
            this.btnVote.Text = "功能投票";
            this.btnVote.Click += new System.EventHandler(this.btnVote_Click);
            // 
            // btnSecurity
            // 
            this.btnSecurity.ImagePaddingHorizontal = 8;
            this.btnSecurity.Name = "btnSecurity";
            this.btnSecurity.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnChangePassword,
            this.btnUserManagement,
            this.btnRoleManagement});
            this.btnSecurity.Text = "安全性";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.ImagePaddingHorizontal = 8;
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Text = "變更密碼";
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.ImagePaddingHorizontal = 8;
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Text = "使用者管理";
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // btnRoleManagement
            // 
            this.btnRoleManagement.ImagePaddingHorizontal = 8;
            this.btnRoleManagement.Name = "btnRoleManagement";
            this.btnRoleManagement.Text = "角色權限管理";
            this.btnRoleManagement.Click += new System.EventHandler(this.btnRoleManagement_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.BeginGroup = true;
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "重新登入";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // ribbonTabItemGroup1
            // 
            this.ribbonTabItemGroup1.GroupTitle = "資料管理";
            this.ribbonTabItemGroup1.Name = "ribbonTabItemGroup1";
            // 
            // 
            // 
            this.ribbonTabItemGroup1.Style.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 174 ) ) ) ), ( (int)( ( (byte)( 109 ) ) ) ), ( (int)( ( (byte)( 148 ) ) ) ));
            this.ribbonTabItemGroup1.Style.BackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 144 ) ) ) ), ( (int)( ( (byte)( 72 ) ) ) ), ( (int)( ( (byte)( 123 ) ) ) ));
            this.ribbonTabItemGroup1.Style.BackColorGradientAngle = 90;
            this.ribbonTabItemGroup1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup1.Style.BorderBottomWidth = 1;
            this.ribbonTabItemGroup1.Style.BorderColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 154 ) ) ) ), ( (int)( ( (byte)( 58 ) ) ) ), ( (int)( ( (byte)( 59 ) ) ) ));
            this.ribbonTabItemGroup1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup1.Style.BorderLeftWidth = 1;
            this.ribbonTabItemGroup1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup1.Style.BorderRightWidth = 1;
            this.ribbonTabItemGroup1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup1.Style.BorderTopWidth = 1;
            this.ribbonTabItemGroup1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ribbonTabItemGroup1.Style.TextColor = System.Drawing.Color.White;
            this.ribbonTabItemGroup1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.ribbonTabItemGroup1.Style.TextShadowColor = System.Drawing.Color.Black;
            this.ribbonTabItemGroup1.Style.TextShadowOffset = new System.Drawing.Point(1, 1);
            // 
            // office2007StartButton1
            // 
            this.office2007StartButton1.AutoExpandOnClick = true;
            this.office2007StartButton1.CanCustomize = false;
            this.office2007StartButton1.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.office2007StartButton1.Image = ( (System.Drawing.Image)( resources.GetObject("office2007StartButton1.Image") ) );
            this.office2007StartButton1.ImagePaddingHorizontal = 2;
            this.office2007StartButton1.ImagePaddingVertical = 2;
            this.office2007StartButton1.Name = "office2007StartButton1";
            this.office2007StartButton1.ShowSubItems = false;
            this.office2007StartButton1.Text = "&File";
            // 
            // navigationPanePanel1
            // 
            this.navigationPanePanel1.Location = new System.Drawing.Point(0, 0);
            this.navigationPanePanel1.Name = "navigationPanePanel1";
            this.navigationPanePanel1.ParentItem = null;
            this.navigationPanePanel1.Size = new System.Drawing.Size(200, 100);
            this.navigationPanePanel1.TabIndex = 0;
            // 
            // bar1
            // 
            this.bar1.BarType = DevComponents.DotNetBar.eBarType.StatusBar;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblBarMessage,
            this.progressBarMessage});
            this.bar1.Location = new System.Drawing.Point(4, 665);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 2;
            this.bar1.PaddingRight = 2;
            this.bar1.PaddingTop = 3;
            this.bar1.Size = new System.Drawing.Size(1008, 24);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.bar1.TabIndex = 2;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // lblBarMessage
            // 
            this.lblBarMessage.Name = "lblBarMessage";
            // 
            // progressBarMessage
            // 
            // 
            // 
            // 
            this.progressBarMessage.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.progressBarMessage.CanCustomize = false;
            this.progressBarMessage.ChunkGradientAngle = 0F;
            this.progressBarMessage.ColorTable = DevComponents.DotNetBar.eProgressBarItemColor.Paused;
            this.progressBarMessage.MarqueeAnimationSpeed = 0;
            this.progressBarMessage.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.progressBarMessage.Name = "progressBarMessage";
            this.progressBarMessage.RecentlyUsed = false;
            this.progressBarMessage.Visible = false;
            this.progressBarMessage.Width = 240;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelContent);
            this.panel1.Controls.Add(this.expandableSplitter1);
            this.panel1.Controls.Add(this.navigationPane1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 501);
            this.panel1.TabIndex = 5;
            // 
            // panelContent
            // 
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(163, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(845, 501);
            this.panelContent.TabIndex = 5;
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Expandable = false;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 252 ) ) ) ), ( (int)( ( (byte)( 151 ) ) ) ), ( (int)( ( (byte)( 61 ) ) ) ));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 184 ) ) ) ), ( (int)( ( (byte)( 94 ) ) ) ));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ), ( (int)( ( (byte)( 0 ) ) ) ));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 101 ) ) ) ), ( (int)( ( (byte)( 147 ) ) ) ), ( (int)( ( (byte)( 207 ) ) ) ));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 227 ) ) ) ), ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(160, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 501);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 4;
            this.expandableSplitter1.TabStop = false;
            // 
            // navigationPane1
            // 
            this.navigationPane1.CanCollapse = true;
            this.navigationPane1.ConfigureAddRemoveVisible = false;
            this.navigationPane1.ConfigureNavOptionsVisible = false;
            this.navigationPane1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navigationPane1.ItemPaddingBottom = 1;
            this.navigationPane1.ItemPaddingTop = 1;
            this.navigationPane1.Location = new System.Drawing.Point(0, 0);
            this.navigationPane1.Name = "navigationPane1";
            this.navigationPane1.NavigationBarHeight = 38;
            this.navigationPane1.Padding = new System.Windows.Forms.Padding(1);
            this.navigationPane1.Size = new System.Drawing.Size(160, 501);
            this.navigationPane1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.navigationPane1.TabIndex = 2;
            // 
            // 
            // 
            this.navigationPane1.TitlePanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.navigationPane1.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationPane1.TitlePanel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.navigationPane1.TitlePanel.Location = new System.Drawing.Point(1, 1);
            this.navigationPane1.TitlePanel.Name = "panelTitle";
            this.navigationPane1.TitlePanel.Size = new System.Drawing.Size(158, 24);
            this.navigationPane1.TitlePanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.navigationPane1.TitlePanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.navigationPane1.TitlePanel.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.navigationPane1.TitlePanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPane1.TitlePanel.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.navigationPane1.TitlePanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.navigationPane1.TitlePanel.Style.GradientAngle = 90;
            this.navigationPane1.TitlePanel.Style.MarginLeft = 4;
            this.navigationPane1.TitlePanel.TabIndex = 0;
            this.navigationPane1.TitlePanel.Text = "Title";
            this.navigationPane1.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.navigationPane1_ExpandedChanged);
            this.navigationPane1.LocalizeString += new DevComponents.DotNetBar.DotNetBarManager.LocalizeStringEventHandler(this.navigationPane1_LocalizeString);
            // 
            // timer1
            // 
            this.timer1.Interval = 75;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MotherForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1016, 691);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.bar1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject("$this.Icon") ) );
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MotherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartSchool";
            this.Load += new System.EventHandler(this.MotherForm_Load);
            this.SizeChanged += new System.EventHandler(this.MotherForm_SizeChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MotherForm_FormClosed);
            ( (System.ComponentModel.ISupportInitialize)( this.bar1 ) ).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonControl ribbonControl1;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem buttonChangeStyle;
        private DevComponents.DotNetBar.ButtonItem buttonStyleOffice2007Blue;
        private DevComponents.DotNetBar.ButtonItem buttonStyleOffice2007Black;
        private DevComponents.DotNetBar.ButtonItem buttonStyleOffice2007Silver;
        private DevComponents.DotNetBar.Office2007StartButton office2007StartButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelContent;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.NavigationPane navigationPane1;
        private DevComponents.DotNetBar.LabelItem lblBarMessage;
        private DevComponents.DotNetBar.ProgressBarItem progressBarMessage;
        private DevComponents.DotNetBar.Office2007StartButton office2007StartButton2;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.RibbonTabItemGroup ribbonTabItemGroup1;
        private DevComponents.DotNetBar.ButtonItem btnChangePassword;
        private DevComponents.DotNetBar.ButtonItem btnQueryLog;
        private DevComponents.DotNetBar.ButtonItem btnAllLog;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.ButtonItem btnSchoolInfo;
        private DevComponents.DotNetBar.ButtonItem btnSecurity;
        private DevComponents.DotNetBar.ButtonItem btnUserManagement;
        private DevComponents.DotNetBar.ButtonItem btnRoleManagement;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem btnShowHidden;
        private DevComponents.DotNetBar.LabelItem lblShowSecTab;
        private DevComponents.DotNetBar.LabelItem lblProcess;
        private DevComponents.DotNetBar.ButtonItem btnFeedbackTop;
        private DevComponents.DotNetBar.ButtonItem btnFeedback;
        private DevComponents.DotNetBar.ButtonItem btnNews;
        private DevComponents.DotNetBar.ButtonItem btnVote;
        private DevComponents.DotNetBar.ButtonItem btnEPaperManager;
        private DevComponents.DotNetBar.ButtonItem btnSurveyMan;
    }
}