namespace SmartSchool.CourseRelated.RibbonBars
{
    partial class History
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(History));
            this.btnHistory = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnHistory,
            this.buttonItem1});
            this.MainRibbonBar.Text = "其它";
            // 
            // btnHistory
            // 
            this.btnHistory.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnHistory.Enabled = false;
            this.btnHistory.Image = ( (System.Drawing.Image)( resources.GetObject("btnHistory.Image") ) );
            this.btnHistory.ImagePaddingHorizontal = 8;
            this.btnHistory.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.SubItemsExpandWidth = 14;
            this.btnHistory.Text = "修改歷程";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.Enabled = false;
            this.buttonItem1.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem1.Image") ) );
            this.buttonItem1.ImageFixedSize = new System.Drawing.Size(35, 35);
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.SubItemsExpandWidth = 14;
            this.buttonItem1.Text = "調換學生";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "History";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnHistory;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
    }
}
