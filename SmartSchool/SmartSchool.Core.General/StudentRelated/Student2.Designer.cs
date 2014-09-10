namespace SmartSchool.StudentRelated
{
    partial class Student
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.btn所有學生 = new DevComponents.DotNetBar.ButtonItem();
            this.comboBoxItem1 = new DevComponents.DotNetBar.ComboBoxItem();
            this.checkBoxItem1 = new DevComponents.DotNetBar.CheckBoxItem();
            this.btn在校學生 = new DevComponents.DotNetBar.ButtonItem();
            this.btn一般生 = new DevComponents.DotNetBar.ButtonItem();
            this.btn休學生 = new DevComponents.DotNetBar.ButtonItem();
            this.btn延修生 = new DevComponents.DotNetBar.ButtonItem();
            this.btn畢業及離校生 = new DevComponents.DotNetBar.ButtonItem();
            this.btn刪除 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.btn學籍在校學生 = new DevComponents.DotNetBar.ButtonItem();
            this.palmerwormStudent1 = new SmartSchool.StudentRelated.PalmerwormStudent();
            this.chkSearchInSSN = new DevComponents.DotNetBar.CheckBoxItem();
            this.chkSearchInName = new DevComponents.DotNetBar.CheckBoxItem();
            this.chkSearchInStudentId = new DevComponents.DotNetBar.CheckBoxItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.navigationPane1.SuspendLayout();
            this.navigationPanePanel1.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelList.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.eppView.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
            this.panel3.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.contextMenuBar1 ) ).BeginInit();
            this.SuspendLayout();
            // 
            // navigationPane1
            // 
            this.navigationPane1.Size = new System.Drawing.Size(189, 698);
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
            this.navigationPane1.TitlePanel.Text = "學生";
            this.navigationPane1.Controls.SetChildIndex(this.navigationPanePanel1, 0);
            // 
            // navigationPanePanel1
            // 
            this.navigationPanePanel1.Size = new System.Drawing.Size(187, 640);
            this.navigationPanePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.navigationPanePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.navigationPanePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.navigationPanePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPanePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.navigationPanePanel1.Style.GradientAngle = 90;
            // 
            // buttonItem1
            // 
            this.buttonItem1.Image = global::SmartSchool.Properties.Resources.Navigation_Student_New;
            this.buttonItem1.Text = "學生";
            // 
            // panelContent
            // 
            this.panelContent.Size = new System.Drawing.Size(748, 698);
            // 
            // splitterListDetial
            // 
            this.splitterListDetial.Location = new System.Drawing.Point(248, 0);
            this.splitterListDetial.Size = new System.Drawing.Size(3, 698);
            this.splitterListDetial.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.splitterListDetial_ExpandedChanged);
            // 
            // panelList
            // 
            this.panelList.Size = new System.Drawing.Size(248, 698);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(248, 667);
            // 
            // expandableSplitter3
            // 
            this.expandableSplitter3.Size = new System.Drawing.Size(248, 1);
            // 
            // panelEx1
            // 
            this.panelEx1.Size = new System.Drawing.Size(248, 30);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            // 
            // txtSearch
            // 
            this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Size = new System.Drawing.Size(172, 25);
            this.txtSearch.WatermarkText = "搜尋學生";
            // 
            // buttonExpand
            // 
            this.buttonExpand.Location = new System.Drawing.Point(221, 5);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(178, 5);
            this.btnSearch.Size = new System.Drawing.Size(40, 21);
            this.btnSearch.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem3,
            this.chkSearchInName,
            this.chkSearchInStudentId,
            this.chkSearchInSSN});
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // eppViewSelector
            // 
            this.eppViewSelector.Location = new System.Drawing.Point(0, 555);
            this.eppViewSelector.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.eppViewSelector.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppViewSelector.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppViewSelector.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.eppViewSelector.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.eppViewSelector.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.eppViewSelector.Style.GradientAngle = 90;
            this.eppViewSelector.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.eppViewSelector.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppViewSelector.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppViewSelector.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.eppViewSelector.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.eppViewSelector.TitleStyle.CornerDiameter = 2;
            this.eppViewSelector.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.eppViewSelector.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.eppViewSelector.TitleStyle.GradientAngle = 90;
            // 
            // eppView
            // 
            this.eppView.Size = new System.Drawing.Size(187, 502);
            this.eppView.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.eppView.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppView.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppView.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.eppView.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.eppView.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.eppView.Style.GradientAngle = 90;
            this.eppView.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.eppView.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppView.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppView.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.eppView.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.eppView.TitleStyle.CornerDiameter = 2;
            this.eppView.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.eppView.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.eppView.TitleStyle.GradientAngle = 90;
            this.eppView.Controls.SetChildIndex(this.panel2, 0);
            this.eppView.Controls.SetChildIndex(this.pictureBox1, 0);
            // 
            // panel2
            // 
            this.panel2.Size = new System.Drawing.Size(187, 479);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(77, 156);
            // 
            // btnFilter
            // 
            this.btnFilter.AutoExpandOnClick = true;
            this.btnFilter.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.btn所有學生,
            this.btn在校學生,
            this.btn學籍在校學生,
            this.labelItem2,
            this.btn一般生,
            this.btn休學生,
            this.btn延修生,
            this.btn畢業及離校生,
            this.btn刪除});
            this.btnFilter.SubItemsExpandWidth = 16;
            this.btnFilter.PopupClose += new System.EventHandler(this.btnFilter_PopupClose);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.palmerwormStudent1);
            this.panel3.Location = new System.Drawing.Point(251, 0);
            this.panel3.Size = new System.Drawing.Size(497, 698);
            // 
            // itemContainer1
            // 
            this.itemContainer1.Enabled = false;
            // 
            // addToTemp
            // 
            this.addToTemp.Enabled = false;
            // 
            // removeFormTemp
            // 
            this.removeFormTemp.Enabled = false;
            // 
            // btn所有學生
            // 
            this.btn所有學生.AutoCheckOnClick = true;
            this.btn所有學生.AutoCollapseOnClick = false;
            this.btn所有學生.ClickAutoRepeat = true;
            this.btn所有學生.GlobalItem = false;
            this.btn所有學生.ImagePaddingHorizontal = 8;
            this.btn所有學生.Name = "btn所有學生";
            this.btn所有學生.Text = "所有學生";
            this.btn所有學生.CheckedChanged += new System.EventHandler(this.filterAllStudent_Click);
            // 
            // comboBoxItem1
            // 
            this.comboBoxItem1.DropDownHeight = 106;
            this.comboBoxItem1.ItemHeight = 17;
            this.comboBoxItem1.Name = "comboBoxItem1";
            // 
            // checkBoxItem1
            // 
            this.checkBoxItem1.Name = "checkBoxItem1";
            this.checkBoxItem1.Text = "checkBoxItem1";
            // 
            // btn在校學生
            // 
            this.btn在校學生.AutoCheckOnClick = true;
            this.btn在校學生.AutoCollapseOnClick = false;
            this.btn在校學生.Checked = true;
            this.btn在校學生.ClickAutoRepeat = true;
            this.btn在校學生.ImagePaddingHorizontal = 8;
            this.btn在校學生.Name = "btn在校學生";
            this.btn在校學生.Text = "在校學生";
            this.btn在校學生.CheckedChanged += new System.EventHandler(this.btn在校學生_Click);
            // 
            // btn一般生
            // 
            this.btn一般生.AutoCheckOnClick = true;
            this.btn一般生.AutoCollapseOnClick = false;
            this.btn一般生.Checked = true;
            this.btn一般生.ClickAutoRepeat = true;
            this.btn一般生.GlobalItem = false;
            this.btn一般生.ImagePaddingHorizontal = 8;
            this.btn一般生.Name = "btn一般生";
            this.btn一般生.Text = "一般生";
            this.btn一般生.CheckedChanged += new System.EventHandler(this.statusFilterChanged);
            // 
            // btn休學生
            // 
            this.btn休學生.AutoCheckOnClick = true;
            this.btn休學生.AutoCollapseOnClick = false;
            this.btn休學生.ClickAutoRepeat = true;
            this.btn休學生.GlobalItem = false;
            this.btn休學生.ImagePaddingHorizontal = 8;
            this.btn休學生.Name = "btn休學生";
            this.btn休學生.Text = "休學生";
            this.btn休學生.CheckedChanged += new System.EventHandler(this.statusFilterChanged);
            // 
            // btn延修生
            // 
            this.btn延修生.AutoCheckOnClick = true;
            this.btn延修生.AutoCollapseOnClick = false;
            this.btn延修生.Checked = true;
            this.btn延修生.ClickAutoRepeat = true;
            this.btn延修生.GlobalItem = false;
            this.btn延修生.ImagePaddingHorizontal = 8;
            this.btn延修生.Name = "btn延修生";
            this.btn延修生.Text = "延修生";
            this.btn延修生.CheckedChanged += new System.EventHandler(this.statusFilterChanged);
            // 
            // btn畢業及離校生
            // 
            this.btn畢業及離校生.AutoCheckOnClick = true;
            this.btn畢業及離校生.AutoCollapseOnClick = false;
            this.btn畢業及離校生.ClickAutoRepeat = true;
            this.btn畢業及離校生.GlobalItem = false;
            this.btn畢業及離校生.ImagePaddingHorizontal = 8;
            this.btn畢業及離校生.Name = "btn畢業及離校生";
            this.btn畢業及離校生.Text = "畢業及離校生";
            this.btn畢業及離校生.CheckedChanged += new System.EventHandler(this.statusFilterChanged);
            // 
            // btn刪除
            // 
            this.btn刪除.AutoCheckOnClick = true;
            this.btn刪除.AutoCollapseOnClick = false;
            this.btn刪除.ClickAutoRepeat = true;
            this.btn刪除.GlobalItem = false;
            this.btn刪除.ImagePaddingHorizontal = 8;
            this.btn刪除.Name = "btn刪除";
            this.btn刪除.Text = "刪除";
            this.btn刪除.CheckedChanged += new System.EventHandler(this.statusFilterChanged);
            // 
            // labelItem1
            // 
            this.labelItem1.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.labelItem1.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem1.GlobalItem = false;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "通用篩選";
            this.labelItem1.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // labelItem2
            // 
            this.labelItem2.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.labelItem2.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem2.GlobalItem = false;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "依狀態篩選";
            this.labelItem2.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // btn學籍在校學生
            // 
            this.btn學籍在校學生.AutoCheckOnClick = true;
            this.btn學籍在校學生.AutoCollapseOnClick = false;
            this.btn學籍在校學生.ClickAutoRepeat = true;
            this.btn學籍在校學生.GlobalItem = false;
            this.btn學籍在校學生.ImagePaddingHorizontal = 8;
            this.btn學籍在校學生.Name = "btn學籍在校學生";
            this.btn學籍在校學生.Text = "學籍在校學生";
            this.btn學籍在校學生.CheckedChanged += new System.EventHandler(this.btn學籍在校學生_Click);
            // 
            // palmerwormStudent1
            // 
            this.palmerwormStudent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palmerwormStudent1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.palmerwormStudent1.Location = new System.Drawing.Point(0, 0);
            this.palmerwormStudent1.Manager = null;
            this.palmerwormStudent1.Margin = new System.Windows.Forms.Padding(4);
            this.palmerwormStudent1.Name = "palmerwormStudent1";
            this.palmerwormStudent1.Size = new System.Drawing.Size(497, 698);
            this.palmerwormStudent1.StudentInfo = null;
            this.palmerwormStudent1.TabIndex = 0;
            this.palmerwormStudent1.Visible = false;
            // 
            // chkSearchInSSN
            // 
            this.chkSearchInSSN.AutoCollapseOnClick = false;
            this.chkSearchInSSN.Checked = true;
            this.chkSearchInSSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchInSSN.GlobalItem = false;
            this.chkSearchInSSN.Name = "chkSearchInSSN";
            this.chkSearchInSSN.Text = "身分證號";
            this.chkSearchInSSN.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.chkSearchInName_CheckedChanged);
            // 
            // chkSearchInName
            // 
            this.chkSearchInName.AutoCollapseOnClick = false;
            this.chkSearchInName.Checked = true;
            this.chkSearchInName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchInName.GlobalItem = false;
            this.chkSearchInName.Name = "chkSearchInName";
            this.chkSearchInName.Text = "姓名";
            this.chkSearchInName.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.chkSearchInName_CheckedChanged);
            // 
            // chkSearchInStudentId
            // 
            this.chkSearchInStudentId.AutoCollapseOnClick = false;
            this.chkSearchInStudentId.Checked = true;
            this.chkSearchInStudentId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchInStudentId.GlobalItem = false;
            this.chkSearchInStudentId.Name = "chkSearchInStudentId";
            this.chkSearchInStudentId.Text = "學號";
            this.chkSearchInStudentId.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.chkSearchInName_CheckedChanged);
            // 
            // labelItem3
            // 
            this.labelItem3.AutoCollapseOnClick = false;
            this.labelItem3.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelItem3.GlobalItem = false;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "搜尋欄位";
            // 
            // Student
            // 
            this.Name = "Student";
            this.Size = new System.Drawing.Size(937, 698);
            this.navigationPane1.ResumeLayout(false);
            this.navigationPanePanel1.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.eppView.ResumeLayout(false);
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
            this.panel3.ResumeLayout(false);
            ( (System.ComponentModel.ISupportInitialize)( this.contextMenuBar1 ) ).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btn所有學生;
        private DevComponents.DotNetBar.ButtonItem btn在校學生;
        private DevComponents.DotNetBar.ComboBoxItem comboBoxItem1;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem1;
        private DevComponents.DotNetBar.ButtonItem btn一般生;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ButtonItem btn休學生;
        private DevComponents.DotNetBar.ButtonItem btn延修生;
        private DevComponents.DotNetBar.ButtonItem btn畢業及離校生;
        private DevComponents.DotNetBar.ButtonItem btn刪除;
        private DevComponents.DotNetBar.ButtonItem btn學籍在校學生;
        private PalmerwormStudent palmerwormStudent1;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.CheckBoxItem chkSearchInSSN;
        private DevComponents.DotNetBar.CheckBoxItem chkSearchInName;
        private DevComponents.DotNetBar.CheckBoxItem chkSearchInStudentId;
    }
}
