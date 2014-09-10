namespace SmartSchool.CourseRelated
{
    partial class CourseDetailPane
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
            this.ctxChangeStatus = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.CheckItemRight = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainer3 = new DevComponents.DotNetBar.ItemContainer();
            this.CheckItemLeft = new DevComponents.DotNetBar.ItemContainer();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.lblCourseName = new DevComponents.DotNetBar.LabelX();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tableLayoutPanel1_Layer1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnGoto = new DevComponents.DotNetBar.ButtonX();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.palmerwormControlPanel = new DevComponents.DotNetBar.PanelEx();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainer4 = new DevComponents.DotNetBar.ItemContainer();
            this.DetailItemPane = new SmartSchool.Common.CardPanelEx();
            this.palmerwormControlPanel.SuspendLayout();
            this.SuspendLayout();
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
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Tag = "畢業或離校";
            this.buttonItem3.Text = "畢業或離校";
            // 
            // buttonItem4
            // 
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Tag = "休學";
            this.buttonItem4.Text = "休學";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Tag = "延修";
            this.buttonItem1.Text = "延修";
            // 
            // buttonItem5
            // 
            this.buttonItem5.ImagePaddingHorizontal = 8;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Tag = "輟學";
            this.buttonItem5.Text = "輟學";
            this.buttonItem5.Visible = false;
            // 
            // CheckItemRight
            // 
            this.CheckItemRight.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.CheckItemRight.MinimumSize = new System.Drawing.Size(120, 0);
            this.CheckItemRight.Name = "CheckItemRight";
            // 
            // itemContainer3
            // 
            this.itemContainer3.MinimumSize = new System.Drawing.Size(240, 0);
            this.itemContainer3.Name = "itemContainer3";
            this.itemContainer3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.CheckItemLeft,
            this.CheckItemRight});
            // 
            // CheckItemLeft
            // 
            this.CheckItemLeft.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.CheckItemLeft.MinimumSize = new System.Drawing.Size(120, 0);
            this.CheckItemLeft.Name = "CheckItemLeft";
            // 
            // labelItem1
            // 
            this.labelItem1.AutoCollapseOnClick = false;
            this.labelItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelItem1.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.labelItem1.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem1.Height = 18;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "課程資料";
            // 
            // lblCourseName
            // 
            this.lblCourseName.AutoSize = true;
            this.lblCourseName.Location = new System.Drawing.Point(21, 6);
            this.lblCourseName.Name = "lblCourseName";
            this.lblCourseName.Size = new System.Drawing.Size(54, 14);
            this.lblCourseName.TabIndex = 20;
            this.lblCourseName.Text = "              ";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 486F));
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(486, 463);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Etched;
            this.labelX1.BackgroundStyle.BorderBottomColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.labelX1.BackgroundStyle.BorderBottomWidth = 1;
            this.labelX1.BackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.labelX1.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.labelX1.Location = new System.Drawing.Point(23, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(265, 28);
            this.labelX1.TabIndex = 19;
            // 
            // tableLayoutPanel1_Layer1
            // 
            this.tableLayoutPanel1_Layer1.ColumnCount = 1;
            this.tableLayoutPanel1_Layer1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 486F));
            this.tableLayoutPanel1_Layer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1_Layer1.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel1_Layer1.Name = "tableLayoutPanel1_Layer1";
            this.tableLayoutPanel1_Layer1.RowCount = 2;
            this.tableLayoutPanel1_Layer1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1_Layer1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1_Layer1.Size = new System.Drawing.Size(486, 463);
            this.tableLayoutPanel1_Layer1.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 486F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(486, 463);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // btnGoto
            // 
            this.btnGoto.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoto.AutoExpandOnClick = true;
            this.btnGoto.Location = new System.Drawing.Point(278, 7);
            this.btnGoto.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(47, 20);
            this.btnGoto.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem3});
            this.btnGoto.TabIndex = 18;
            this.btnGoto.Text = "移至";
            // 
            // labelItem3
            // 
            this.labelItem3.AutoCollapseOnClick = false;
            this.labelItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelItem3.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.labelItem3.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem3.Height = 18;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "課程資料";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX2.AutoExpandOnClick = true;
            this.buttonX2.Location = new System.Drawing.Point(329, 7);
            this.buttonX2.Margin = new System.Windows.Forms.Padding(4);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 20);
            this.buttonX2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.itemContainer3});
            this.buttonX2.TabIndex = 17;
            this.buttonX2.Text = "自訂項目";
            // 
            // palmerwormControlPanel
            // 
            this.palmerwormControlPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.palmerwormControlPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.palmerwormControlPanel.Controls.Add(this.btnRefresh);
            this.palmerwormControlPanel.Controls.Add(this.lblCourseName);
            this.palmerwormControlPanel.Controls.Add(this.labelX1);
            this.palmerwormControlPanel.Controls.Add(this.btnGoto);
            this.palmerwormControlPanel.Controls.Add(this.buttonX2);
            this.palmerwormControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.palmerwormControlPanel.Location = new System.Drawing.Point(0, 0);
            this.palmerwormControlPanel.Margin = new System.Windows.Forms.Padding(4);
            this.palmerwormControlPanel.Name = "palmerwormControlPanel";
            this.palmerwormControlPanel.Size = new System.Drawing.Size(486, 35);
            this.palmerwormControlPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.palmerwormControlPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.palmerwormControlPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.palmerwormControlPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.palmerwormControlPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.palmerwormControlPanel.Style.GradientAngle = 90;
            this.palmerwormControlPanel.TabIndex = 11;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.AutoExpandOnClick = true;
            this.btnRefresh.Location = new System.Drawing.Point(408, 7);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 20);
            this.btnRefresh.TabIndex = 21;
            this.btnRefresh.Text = "重新整理";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.ImagePaddingHorizontal = 8;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "buttonItem6";
            // 
            // itemContainer2
            // 
            this.itemContainer2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer2.MinimumSize = new System.Drawing.Size(120, 0);
            this.itemContainer2.Name = "itemContainer2";
            // 
            // itemContainer4
            // 
            this.itemContainer4.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer4.MinimumSize = new System.Drawing.Size(120, 0);
            this.itemContainer4.Name = "itemContainer4";
            // 
            // DetailItemPane
            // 
            this.DetailItemPane.AutoScroll = true;
            this.DetailItemPane.CanvasColor = System.Drawing.SystemColors.Control;
            this.DetailItemPane.CardWidth = 550;
            this.DetailItemPane.CausesValidation = false;
            this.DetailItemPane.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.DetailItemPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailItemPane.Location = new System.Drawing.Point(0, 35);
            this.DetailItemPane.Margin = new System.Windows.Forms.Padding(4);
            this.DetailItemPane.MinWidth = 3;
            this.DetailItemPane.Name = "DetailItemPane";
            this.DetailItemPane.Padding = new System.Windows.Forms.Padding(7);
            this.DetailItemPane.Size = new System.Drawing.Size(486, 463);
            this.DetailItemPane.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.DetailItemPane.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.DetailItemPane.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.DetailItemPane.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.DetailItemPane.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.DetailItemPane.Style.GradientAngle = 90;
            this.DetailItemPane.TabIndex = 12;
            this.DetailItemPane.MouseEnter += new System.EventHandler(this.DetailItemPane_MouseEnter);
            // 
            // CourseDetailPane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.DetailItemPane);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1_Layer1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.palmerwormControlPanel);
            this.Name = "CourseDetailPane";
            this.Size = new System.Drawing.Size(486, 498);
            this.Load += new System.EventHandler(this.CourseDetailPane_Load);
            this.palmerwormControlPanel.ResumeLayout(false);
            this.palmerwormControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem ctxChangeStatus;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ItemContainer CheckItemRight;
        private DevComponents.DotNetBar.ItemContainer itemContainer3;
        private DevComponents.DotNetBar.ItemContainer CheckItemLeft;
        private SmartSchool.Common.CardPanelEx DetailItemPane;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelX lblCourseName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1_Layer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.ButtonX btnGoto;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.PanelEx palmerwormControlPanel;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
        private DevComponents.DotNetBar.ItemContainer itemContainer4;
    }
}
