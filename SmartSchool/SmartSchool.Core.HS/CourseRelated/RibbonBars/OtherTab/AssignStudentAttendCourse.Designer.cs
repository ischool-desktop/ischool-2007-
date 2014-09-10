namespace SmartSchool.CourseRelated.RibbonBars.OtherTab
{
    partial class AssignStudentAttendCourse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignStudentAttendCourse));
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.btnAttend = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem66 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem67 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem68 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem69 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem70 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.AutoSizeItems = false;
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.MainRibbonBar.OverflowButtonImage = ( (System.Drawing.Image)( resources.GetObject("MainRibbonBar.OverflowButtonImage") ) );
            this.MainRibbonBar.ResizeItemsToFit = false;
            this.MainRibbonBar.ResizeOrderIndex = 50;
            this.MainRibbonBar.Size = new System.Drawing.Size(138, 97);
            this.MainRibbonBar.Text = "指定";
            // 
            // superTooltip1
            // 
            this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // btnAttend
            // 
            this.btnAttend.AutoExpandOnClick = true;
            this.btnAttend.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAttend.Enabled = false;
            this.btnAttend.Image = ( (System.Drawing.Image)( resources.GetObject("btnAttend.Image") ) );
            this.btnAttend.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.btnAttend.ImagePaddingHorizontal = 3;
            this.btnAttend.ImagePaddingVertical = 0;
            this.btnAttend.Name = "btnAttend";
            this.btnAttend.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem66,
            this.buttonItem67,
            this.buttonItem68,
            this.buttonItem69,
            this.buttonItem70});
            this.btnAttend.SubItemsExpandWidth = 14;
            this.superTooltip1.SetSuperTooltip(this.btnAttend, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "將所選學生指派課程", null, null, DevComponents.DotNetBar.eTooltipColor.System));
            this.btnAttend.Text = "修課";
            this.btnAttend.PopupShowing += new System.EventHandler(this.buttonItem64_PopupShowing);
            // 
            // buttonItem66
            // 
            this.buttonItem66.ImagePaddingHorizontal = 8;
            this.buttonItem66.Name = "buttonItem66";
            this.buttonItem66.Text = "基本電學Ⅰ";
            // 
            // buttonItem67
            // 
            this.buttonItem67.ImagePaddingHorizontal = 8;
            this.buttonItem67.Name = "buttonItem67";
            this.buttonItem67.Text = "電子學Ⅰ";
            // 
            // buttonItem68
            // 
            this.buttonItem68.ImagePaddingHorizontal = 8;
            this.buttonItem68.Name = "buttonItem68";
            this.buttonItem68.Text = "程式設計Ⅰ";
            // 
            // buttonItem69
            // 
            this.buttonItem69.ImagePaddingHorizontal = 8;
            this.buttonItem69.Name = "buttonItem69";
            this.buttonItem69.Text = "電子學實習Ⅰ";
            // 
            // buttonItem70
            // 
            this.buttonItem70.ImagePaddingHorizontal = 8;
            this.buttonItem70.Name = "buttonItem70";
            this.buttonItem70.Text = "單晶片控制實習Ⅰ";
            // 
            // itemContainer1
            // 
            this.itemContainer1.CanCustomize = false;
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.MultiLine = true;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAttend});
            // 
            // Category
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Category";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private DevComponents.DotNetBar.ButtonItem btnAttend;
        private DevComponents.DotNetBar.ButtonItem buttonItem66;
        private DevComponents.DotNetBar.ButtonItem buttonItem67;
        private DevComponents.DotNetBar.ButtonItem buttonItem68;
        private DevComponents.DotNetBar.ButtonItem buttonItem69;
        private DevComponents.DotNetBar.ButtonItem buttonItem70;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;

    }
}
