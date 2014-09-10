namespace SmartSchool.ClassRelated.RibbonBars
{
    partial class Report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report));
            this.buttonItem131 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
            this.btnSearchAttendance = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem11 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.btnStatistics = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.MainRibbonBar.Text = "統計報表";
            // 
            // buttonItem131
            // 
            this.buttonItem131.AutoExpandOnClick = true;
            this.buttonItem131.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem131.CanCustomize = false;
            this.buttonItem131.Enabled = false;
            this.buttonItem131.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem131.Image")));
            this.buttonItem131.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.buttonItem131.ImagePaddingHorizontal = 3;
            this.buttonItem131.ImagePaddingVertical = 10;
            this.buttonItem131.Name = "buttonItem131";
            this.buttonItem131.SplitButton = true;
            this.buttonItem131.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem14,
            this.buttonItem15,
            this.buttonItem16});
            this.buttonItem131.SubItemsExpandWidth = 14;
            this.buttonItem131.Text = "報表";
            // 
            // buttonItem14
            // 
            this.buttonItem14.ImagePaddingHorizontal = 8;
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2,
            this.buttonItem8,
            this.buttonItem1});
            this.buttonItem14.Text = "學籍相關報表";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "班級名條";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem8
            // 
            this.buttonItem8.ImagePaddingHorizontal = 8;
            this.buttonItem8.Name = "buttonItem8";
            this.buttonItem8.Text = "班級點名表";
            this.buttonItem8.Click += new System.EventHandler(this.buttonItem8_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "班級通訊錄";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem15
            // 
            this.buttonItem15.ImagePaddingHorizontal = 8;
            this.buttonItem15.Name = "buttonItem15";
            this.buttonItem15.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem7,
            this.buttonItem3});
            this.buttonItem15.Text = "成績相關報表";
            // 
            // buttonItem7
            // 
            this.buttonItem7.ImagePaddingHorizontal = 8;
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Text = "班級考試成績單";
            this.buttonItem7.Click += new System.EventHandler(this.buttonItem7_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "班級考試成績單(Word)";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // buttonItem16
            // 
            this.buttonItem16.ImagePaddingHorizontal = 8;
            this.buttonItem16.Name = "buttonItem16";
            this.buttonItem16.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSearchAttendance,
            this.buttonItem13,
            this.buttonItem10,
            this.buttonItem9,
            this.buttonItem11,
            this.buttonItem4,
            this.buttonItem6,
            this.buttonItem5});
            this.buttonItem16.Text = "學務相關報表";
            // 
            // btnSearchAttendance
            // 
            this.btnSearchAttendance.ImagePaddingHorizontal = 8;
            this.btnSearchAttendance.Name = "btnSearchAttendance";
            this.btnSearchAttendance.Text = "德行表現特殊學生名單";
            this.btnSearchAttendance.Click += new System.EventHandler(this.btnSearchAttendance_Click);
            // 
            // buttonItem13
            // 
            this.buttonItem13.ImagePaddingHorizontal = 8;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Text = "缺曠通知單";
            this.buttonItem13.Click += new System.EventHandler(this.buttonItem13_Click);
            // 
            // buttonItem10
            // 
            this.buttonItem10.ImagePaddingHorizontal = 8;
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Text = "獎懲通知單";
            this.buttonItem10.Click += new System.EventHandler(this.buttonItem10_Click);
            // 
            // buttonItem9
            // 
            this.buttonItem9.ImagePaddingHorizontal = 8;
            this.buttonItem9.Name = "buttonItem9";
            this.buttonItem9.Text = "班級學生缺曠明細";
            this.buttonItem9.Click += new System.EventHandler(this.buttonItem9_Click);
            // 
            // buttonItem11
            // 
            this.buttonItem11.ImagePaddingHorizontal = 8;
            this.buttonItem11.Name = "buttonItem11";
            this.buttonItem11.Text = "班級學生獎懲明細";
            this.buttonItem11.Click += new System.EventHandler(this.buttonItem11_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.AutoExpandOnClick = true;
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "缺曠週報表 (依缺曠別統計)";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.ImagePaddingHorizontal = 8;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "缺曠週報表 (依節次統計)";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // buttonItem5
            // 
            this.buttonItem5.ImagePaddingHorizontal = 8;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Text = "獎懲週報表";
            this.buttonItem5.Click += new System.EventHandler(this.buttonItem5_Click);
            // 
            // btnStatistics
            // 
            this.btnStatistics.AutoExpandOnClick = true;
            this.btnStatistics.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnStatistics.CanCustomize = false;
            this.btnStatistics.Enabled = false;
            this.btnStatistics.Image = ((System.Drawing.Image)(resources.GetObject("btnStatistics.Image")));
            this.btnStatistics.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.btnStatistics.ImagePaddingHorizontal = 3;
            this.btnStatistics.ImagePaddingVertical = 10;
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.SubItemsExpandWidth = 14;
            this.btnStatistics.Text = "統計";
            // 
            // itemContainer1
            // 
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnStatistics,
            this.buttonItem131});
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Report";
            this.ResumeLayout(false);

        }

        #endregion        
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem131;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private DevComponents.DotNetBar.ButtonItem buttonItem8;
        private DevComponents.DotNetBar.ButtonItem buttonItem9;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItem11;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        private DevComponents.DotNetBar.ButtonItem buttonItem16;
        private DevComponents.DotNetBar.ButtonItem btnStatistics;
        private DevComponents.DotNetBar.ButtonItem btnSearchAttendance;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
    }
}
