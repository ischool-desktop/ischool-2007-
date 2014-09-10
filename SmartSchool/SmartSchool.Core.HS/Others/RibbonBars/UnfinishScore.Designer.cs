namespace SmartSchool.Others.RibbonBars
{
    partial class UnfinishScore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnfinishScore));
            this.btnOpen = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOpen,
            this.itemContainer1});
            this.MainRibbonBar.Text = "成績作業";
            this.MainRibbonBar.Click += new System.EventHandler(this.MainRibbonBar_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnOpen.Image = ( (System.Drawing.Image)( resources.GetObject("btnOpen.Image") ) );
            this.btnOpen.ImagePaddingHorizontal = 8;
            this.btnOpen.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.SubItemsExpandWidth = 14;
            this.btnOpen.Text = "評量輸入狀況";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // itemContainer1
            // 
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.MultiLine = true;
            this.itemContainer1.Name = "itemContainer1";
            // 
            // UnfinishScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UnfinishScore";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnOpen;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;

    }
}
