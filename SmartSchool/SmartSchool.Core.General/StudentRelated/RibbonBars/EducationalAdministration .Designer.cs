namespace SmartSchool.StudentRelated.RibbonBars
{
    partial class EducationalAdministration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EducationalAdministration));
            this.btnDiploma = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
            this.btnPlacing = new DevComponents.DotNetBar.ButtonItem();
            this.btnMetagenesis = new DevComponents.DotNetBar.ButtonItem();
            this.btnEduLevel = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.AutoOverflowEnabled = false;
            this.MainRibbonBar.AutoSizeItems = false;
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer2});
            this.MainRibbonBar.ResizeItemsToFit = false;
            this.MainRibbonBar.Text = "教務作業";
            // 
            // btnDiploma
            // 
            this.btnDiploma.AutoCollapseOnClick = false;
            this.btnDiploma.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDiploma.Enabled = false;
            this.btnDiploma.Image = ( (System.Drawing.Image)( resources.GetObject("btnDiploma.Image") ) );
            this.btnDiploma.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.btnDiploma.ImagePaddingHorizontal = 3;
            this.btnDiploma.ImagePaddingVertical = 10;
            this.btnDiploma.Name = "btnDiploma";
            this.btnDiploma.SubItemsExpandWidth = 14;
            this.btnDiploma.Text = "證書字號";
            this.btnDiploma.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // itemContainer2
            // 
            this.itemContainer2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer2.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer2.MultiLine = true;
            this.itemContainer2.Name = "itemContainer2";
            this.itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnPlacing,
            this.btnMetagenesis});
            // 
            // btnPlacing
            // 
            this.btnPlacing.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPlacing.Enabled = false;
            this.btnPlacing.Image = ( (System.Drawing.Image)( resources.GetObject("btnPlacing.Image") ) );
            this.btnPlacing.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.btnPlacing.ImagePaddingHorizontal = 3;
            this.btnPlacing.ImagePaddingVertical = 10;
            this.btnPlacing.Name = "btnPlacing";
            this.btnPlacing.SubItemsExpandWidth = 14;
            this.btnPlacing.Text = "排名";
            this.btnPlacing.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // btnMetagenesis
            // 
            this.btnMetagenesis.AutoExpandOnClick = true;
            this.btnMetagenesis.ImagePaddingHorizontal = 8;
            this.btnMetagenesis.Name = "btnMetagenesis";
            this.btnMetagenesis.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnDiploma,
            this.btnEduLevel});
            this.btnMetagenesis.Text = "新生/畢業";
            // 
            // btnEduLevel
            // 
            this.btnEduLevel.Enabled = false;
            this.btnEduLevel.ImagePaddingHorizontal = 8;
            this.btnEduLevel.Name = "btnEduLevel";
            this.btnEduLevel.Text = "產生教育程度資料檔";
            this.btnEduLevel.Click += new System.EventHandler(this.btnEduLevel_Click);
            // 
            // EducationalAdministration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "EducationalAdministration";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnDiploma;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
        private DevComponents.DotNetBar.ButtonItem btnPlacing;
        private DevComponents.DotNetBar.ButtonItem btnMetagenesis;
        private DevComponents.DotNetBar.ButtonItem btnEduLevel;
    }
}
