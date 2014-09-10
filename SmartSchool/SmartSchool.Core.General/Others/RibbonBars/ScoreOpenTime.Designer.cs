namespace SmartSchool.Others.RibbonBars
{
    partial class ScoreOpenTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreOpenTime));
            this.btnSetup = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSetup});
            this.MainRibbonBar.Text = "其它";
            // 
            // btnSetup
            // 
            this.btnSetup.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSetup.Image = ( (System.Drawing.Image)( resources.GetObject("btnSetup.Image") ) );
            this.btnSetup.ImagePaddingHorizontal = 8;
            this.btnSetup.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.SubItemsExpandWidth = 14;
            this.btnSetup.Text = "開放時間設定";
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // ScoreOpenTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ScoreOpenTime";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnSetup;

    }
}
