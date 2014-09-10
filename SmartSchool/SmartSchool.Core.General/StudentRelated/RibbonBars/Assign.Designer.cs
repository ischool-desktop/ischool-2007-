namespace SmartSchool.StudentRelated.RibbonBars
{
    partial class Assign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Assign));
            this.ctxTag = new DevComponents.DotNetBar.ButtonItem();
            this.ctxiClear = new DevComponents.DotNetBar.ButtonItem();
            this.ctxiAllTag = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem41 = new DevComponents.DotNetBar.ButtonItem();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
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
            // ctxTag
            // 
            this.ctxTag.AutoExpandOnClick = true;
            this.ctxTag.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.ctxTag.Image = ( (System.Drawing.Image)( resources.GetObject("ctxTag.Image") ) );
            this.ctxTag.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.ctxTag.ImagePaddingHorizontal = 3;
            this.ctxTag.ImagePaddingVertical = 0;
            this.ctxTag.Name = "ctxTag";
            this.ctxTag.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ctxiClear,
            this.ctxiAllTag,
            this.buttonItem41});
            this.ctxTag.SubItemsExpandWidth = 14;
            this.superTooltip1.SetSuperTooltip(this.ctxTag, new DevComponents.DotNetBar.SuperTooltipInfo("設定學生類別", "", "學生類別用以給予單/多筆學生單/多標籤，可以用來設定學生成績身分等。\r\n\r\n可自訂學生類別群組及類別。", null, null, DevComponents.DotNetBar.eTooltipColor.System));
            this.ctxTag.Text = "類別";
            this.ctxTag.PopupShowing += new System.EventHandler(this.ctxTag_PopupShowing);
            // 
            // ctxiClear
            // 
            this.ctxiClear.ImagePaddingHorizontal = 8;
            this.ctxiClear.Name = "ctxiClear";
            this.ctxiClear.Text = "<b><i>清除所有類別</i></b>";
            // 
            // ctxiAllTag
            // 
            this.ctxiAllTag.BeginGroup = true;
            this.ctxiAllTag.ImagePaddingHorizontal = 8;
            this.ctxiAllTag.Name = "ctxiAllTag";
            this.ctxiAllTag.Text = "所有學生類別…";
            // 
            // buttonItem41
            // 
            this.buttonItem41.Enabled = false;
            this.buttonItem41.ImagePaddingHorizontal = 8;
            this.buttonItem41.Name = "buttonItem41";
            this.buttonItem41.Text = "設定快速點選類別";
            // 
            // superTooltip1
            // 
            this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // itemContainer1
            // 
            this.itemContainer1.CanCustomize = false;
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.MultiLine = true;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ctxTag});
            // 
            // Category
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Category";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem ctxTag;
        private DevComponents.DotNetBar.ButtonItem ctxiClear;
        private DevComponents.DotNetBar.ButtonItem ctxiAllTag;
        private DevComponents.DotNetBar.ButtonItem buttonItem41;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;

    }
}
