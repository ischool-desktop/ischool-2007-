namespace SmartSchool.TeacherRelated.RibbonBars
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
            this.buttonItem114 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem114});
            this.MainRibbonBar.Text = "統計報表";
            // 
            // buttonItem114
            // 
            this.buttonItem114.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem114.Enabled = false;
            this.buttonItem114.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem114.Image") ) );
            this.buttonItem114.ImagePaddingHorizontal = 8;
            this.buttonItem114.Name = "buttonItem114";
            this.buttonItem114.SubItemsExpandWidth = 14;
            this.buttonItem114.Text = "報表";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "Report";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem114;
    }
}
