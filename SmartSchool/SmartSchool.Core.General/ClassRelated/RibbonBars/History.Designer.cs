namespace SmartSchool.ClassRelated.RibbonBars
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
            this.btnSurvey = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnHistory,
            this.btnSurvey});
            this.MainRibbonBar.Text = "其它";
            // 
            // btnHistory
            // 
            this.btnHistory.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnHistory.Enabled = false;
            this.btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnHistory.Image")));
            this.btnHistory.ImagePaddingHorizontal = 8;
            this.btnHistory.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.SubItemsExpandWidth = 14;
            this.btnHistory.Text = "修改歷程";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnSurvey
            // 
            this.btnSurvey.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSurvey.Image = ((System.Drawing.Image)(resources.GetObject("btnSurvey.Image")));
            this.btnSurvey.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.btnSurvey.ImagePaddingHorizontal = 8;
            this.btnSurvey.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnSurvey.Name = "btnSurvey";
            this.btnSurvey.SubItemsExpandWidth = 14;
            this.btnSurvey.Text = "問卷";
            this.btnSurvey.Click += new System.EventHandler(this.btnSurvey_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "History";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnHistory;
        private DevComponents.DotNetBar.ButtonItem btnSurvey;
    }
}
