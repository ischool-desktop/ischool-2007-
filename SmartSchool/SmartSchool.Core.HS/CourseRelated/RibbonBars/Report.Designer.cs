namespace SmartSchool.CourseRelated.RibbonBars
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report));
            this.buttonItem99 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem99});
            this.MainRibbonBar.Text = "統計報表";
            // 
            // buttonItem99
            // 
            this.buttonItem99.AutoExpandOnClick = true;
            this.buttonItem99.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem99.Enabled = false;
            this.buttonItem99.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem99.Image") ) );
            this.buttonItem99.ImagePaddingHorizontal = 8;
            this.buttonItem99.Name = "buttonItem99";
            this.buttonItem99.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.buttonItem99.SubItemsExpandWidth = 14;
            this.buttonItem99.Text = "報表";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "課程修課學生清單";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "Report";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem99;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;

    }
}
