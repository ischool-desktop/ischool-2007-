namespace SmartSchool.StudentRelated.Palmerworm
{
    partial class ShortSummaryPanel
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblSnum = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.pxStatus = new DevComponents.DotNetBar.PanelEx();
            this.ctxMenuBar = new DevComponents.DotNetBar.ContextMenuBar();
            this.ctxChangeStatus = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctxMenuBar)).BeginInit();
            this.SuspendLayout();
            // 
            // picWaiting
            // 
            this.picWaiting.Location = new System.Drawing.Point(103, 8);
            this.picWaiting.Margin = new System.Windows.Forms.Padding(5);
            this.picWaiting.VisibleChanged += new System.EventHandler(this.picWaiting_VisibleChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblName.Location = new System.Drawing.Point(94, 7);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(54, 17);
            this.lblName.TabIndex = 178;
            this.lblName.Text = "李大明 ";
            // 
            // lblSnum
            // 
            this.lblSnum.AutoSize = true;
            this.lblSnum.BackColor = System.Drawing.Color.Transparent;
            this.lblSnum.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSnum.Location = new System.Drawing.Point(148, 7);
            this.lblSnum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSnum.Name = "lblSnum";
            this.lblSnum.Size = new System.Drawing.Size(64, 17);
            this.lblSnum.TabIndex = 179;
            this.lblSnum.Text = "9410001";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.BackColor = System.Drawing.Color.Transparent;
            this.lblClass.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblClass.Location = new System.Drawing.Point(44, 7);
            this.lblClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(50, 17);
            this.lblClass.TabIndex = 181;
            this.lblClass.Text = "園三智";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Location = new System.Drawing.Point(49, 25);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(333, 2);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 182;
            this.panelEx1.Text = "panelEx1";
            // 
            // pxStatus
            // 
            this.pxStatus.CanvasColor = System.Drawing.SystemColors.Control;
            this.pxStatus.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.pxStatus.Location = new System.Drawing.Point(284, 3);
            this.pxStatus.Margin = new System.Windows.Forms.Padding(4);
            this.pxStatus.Name = "pxStatus";
            this.pxStatus.Size = new System.Drawing.Size(96, 21);
            this.pxStatus.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pxStatus.Style.BackColor1.Color = System.Drawing.Color.LightBlue;
            this.pxStatus.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.pxStatus.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.pxStatus.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.pxStatus.Style.BorderWidth = 0;
            this.pxStatus.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.pxStatus.Style.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.pxStatus.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.pxStatus.Style.GradientAngle = 90;
            this.pxStatus.Style.TextTrimming = System.Drawing.StringTrimming.Word;
            this.pxStatus.TabIndex = 183;
            this.pxStatus.Text = "一般";
            this.pxStatus.Click += new System.EventHandler(this.pxStatus_Click);
            // 
            // ctxMenuBar
            // 
            this.ctxMenuBar.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ctxMenuBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ctxChangeStatus});
            this.ctxMenuBar.Location = new System.Drawing.Point(188, 1);
            this.ctxMenuBar.Margin = new System.Windows.Forms.Padding(4);
            this.ctxMenuBar.Name = "ctxMenuBar";
            this.ctxMenuBar.Size = new System.Drawing.Size(104, 28);
            this.ctxMenuBar.Stretch = true;
            this.ctxMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ctxMenuBar.TabIndex = 184;
            this.ctxMenuBar.TabStop = false;
            this.ctxMenuBar.Text = "contextMenuBar1";
            // 
            // ctxChangeStatus
            // 
            this.ctxChangeStatus.AutoExpandOnClick = true;
            this.ctxChangeStatus.ImagePaddingHorizontal = 8;
            this.ctxChangeStatus.Name = "ctxChangeStatus";
            this.ctxChangeStatus.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2,
            this.buttonItem3,
            this.buttonItem4,
            this.buttonItem1,
            this.buttonItem5});
            this.ctxChangeStatus.Text = "ChangeStatus";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Tag = "一般";
            this.buttonItem2.Text = "一般";
            this.buttonItem2.Click += new System.EventHandler(this.ChangeStatus_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Tag = "畢業或離校";
            this.buttonItem3.Text = "畢業或離校";
            this.buttonItem3.Click += new System.EventHandler(this.ChangeStatus_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Tag = "休學";
            this.buttonItem4.Text = "休學";
            this.buttonItem4.Click += new System.EventHandler(this.ChangeStatus_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Tag = "延修";
            this.buttonItem1.Text = "延修";
            this.buttonItem1.Click += new System.EventHandler(this.ChangeStatus_Click);
            // 
            // buttonItem5
            // 
            this.buttonItem5.ImagePaddingHorizontal = 8;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Tag = "輟學";
            this.buttonItem5.Text = "輟學";
            this.buttonItem5.Visible = false;
            this.buttonItem5.Click += new System.EventHandler(this.ChangeStatus_Click);
            // 
            // ShortSummaryPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ctxMenuBar);
            this.Controls.Add(this.pxStatus);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.lblSnum);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblClass);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ShortSummaryPanel";
            this.Padding = new System.Windows.Forms.Padding(44, 7, 7, 7);
            this.Size = new System.Drawing.Size(391, 32);
            this.Controls.SetChildIndex(this.lblClass, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.lblSnum, 0);
            this.Controls.SetChildIndex(this.picWaiting, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.pxStatus, 0);
            this.Controls.SetChildIndex(this.ctxMenuBar, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctxMenuBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblName;
        internal System.Windows.Forms.Label lblSnum;
        internal System.Windows.Forms.Label lblClass;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx pxStatus;
        private DevComponents.DotNetBar.ContextMenuBar ctxMenuBar;
        private DevComponents.DotNetBar.ButtonItem ctxChangeStatus;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
    }
}
