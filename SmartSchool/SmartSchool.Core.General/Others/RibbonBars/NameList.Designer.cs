namespace SmartSchool.Others.RibbonBars
{
    partial class NameList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameList));
            this.buttonItem78 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem77 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem79 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer2,
            this.itemContainer1});
            this.MainRibbonBar.Text = "學籍作業";
            // 
            // buttonItem78
            // 
            this.buttonItem78.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem78.Enabled = false;
            this.buttonItem78.FixedSize = new System.Drawing.Size(68, 68);
            this.buttonItem78.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem78.Image")));
            this.buttonItem78.ImagePaddingHorizontal = 8;
            this.buttonItem78.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem78.Name = "buttonItem78";
            this.buttonItem78.SubItemsExpandWidth = 14;
            this.buttonItem78.Text = "函報名冊";
            this.buttonItem78.Visible = false;
            this.buttonItem78.Click += new System.EventHandler(this.buttonItem78_Click);
            // 
            // buttonItem77
            // 
            this.buttonItem77.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem77.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem77.Image")));
            this.buttonItem77.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.buttonItem77.ImagePaddingHorizontal = 3;
            this.buttonItem77.ImagePaddingVertical = 2;
            this.buttonItem77.Name = "buttonItem77";
            this.buttonItem77.SubItemsExpandWidth = 14;
            this.buttonItem77.Text = "教育程度資料檔(新生)";
            this.buttonItem77.Visible = false;
            // 
            // buttonItem79
            // 
            this.buttonItem79.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem79.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem79.Image")));
            this.buttonItem79.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.buttonItem79.ImagePaddingHorizontal = 3;
            this.buttonItem79.ImagePaddingVertical = 2;
            this.buttonItem79.Name = "buttonItem79";
            this.buttonItem79.SubItemsExpandWidth = 14;
            this.buttonItem79.Text = "教育程度資料檔(畢業)";
            this.buttonItem79.Visible = false;
            // 
            // itemContainer1
            // 
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem77,
            this.buttonItem79});
            // 
            // itemContainer2
            // 
            this.itemContainer2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer2.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer2.Name = "itemContainer2";
            this.itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem78});
            // 
            // NameList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "NameList";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem78;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
        private DevComponents.DotNetBar.ButtonItem buttonItem77;
        private DevComponents.DotNetBar.ButtonItem buttonItem79;
    }
}
