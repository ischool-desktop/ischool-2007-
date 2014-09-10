namespace SmartSchool.StudentRelated.RibbonBars
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
            this.buttonItem86 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btnStatistics = new DevComponents.DotNetBar.ButtonItem();
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
            this.MainRibbonBar.ResizeOrderIndex = 3;
            this.MainRibbonBar.Text = "統計報表";
            // 
            // buttonItem86
            // 
            this.buttonItem86.AutoExpandOnClick = true;
            this.buttonItem86.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem86.CanCustomize = false;
            this.buttonItem86.Enabled = false;
            this.buttonItem86.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem86.Image") ) );
            this.buttonItem86.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.buttonItem86.ImagePaddingHorizontal = 3;
            this.buttonItem86.ImagePaddingVertical = 0;
            this.buttonItem86.Name = "buttonItem86";
            this.buttonItem86.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem4});
            this.buttonItem86.SubItemsExpandWidth = 14;
            this.buttonItem86.Text = "報表";
            // 
            // buttonItem4
            // 
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2,
            this.buttonItem3,
            this.buttonItem1});
            this.buttonItem4.Text = "學務相關報表";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "學生個人缺曠明細";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "學生個人獎懲明細";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "歷年功過及出席統計表";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // btnStatistics
            // 
            this.btnStatistics.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnStatistics.CanCustomize = false;
            this.btnStatistics.Enabled = false;
            this.btnStatistics.Image = ( (System.Drawing.Image)( resources.GetObject("btnStatistics.Image") ) );
            this.btnStatistics.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.btnStatistics.ImagePaddingHorizontal = 3;
            this.btnStatistics.ImagePaddingVertical = 0;
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.SubItemsExpandWidth = 14;
            this.btnStatistics.Text = "統計";
            // 
            // itemContainer1
            // 
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.ResizeItemsToFit = false;
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnStatistics,
            this.buttonItem86});
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Report";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem86;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem btnStatistics;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
    }
}
