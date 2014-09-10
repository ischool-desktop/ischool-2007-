namespace SmartSchool.ClassRelated.RibbonBars
{
    partial class Upgrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Upgrade));
            this.btnUpgrade = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.MainRibbonBar.Text = "教務作業";
            // 
            // btnUpgrade
            // 
            this.btnUpgrade.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnUpgrade.Enabled = false;
            this.btnUpgrade.Image = ( (System.Drawing.Image)( resources.GetObject("btnUpgrade.Image") ) );
            this.btnUpgrade.ImagePaddingHorizontal = 8;
            this.btnUpgrade.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnUpgrade.Name = "btnUpgrade";
            this.btnUpgrade.SubItemsExpandWidth = 14;
            this.btnUpgrade.Text = "班級升級";
            this.btnUpgrade.Click += new System.EventHandler(this.btnUpgrade_Click);
            // 
            // itemContainer1
            // 
            this.itemContainer1.ItemSpacing = 0;
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.MultiLine = true;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.ResizeItemsToFit = false;
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnUpgrade});
            // 
            // Upgrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Upgrade";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnUpgrade;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;

    }
}
