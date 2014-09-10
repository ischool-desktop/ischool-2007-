namespace SmartSchool.ClassRelated.RibbonBars
{
    partial class TeacherBiasRibbon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeacherBiasRibbon));
            this.btnAdjust = new DevComponents.DotNetBar.ButtonItem();
            this.btnWordComment = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAdjust,
            this.btnWordComment});
            this.MainRibbonBar.Text = "學務作業";
            // 
            // btnAdjust
            // 
            this.btnAdjust.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAdjust.Enabled = false;
            this.btnAdjust.GlobalItem = false;
            this.btnAdjust.Image = ((System.Drawing.Image)(resources.GetObject("btnAdjust.Image")));
            this.btnAdjust.ImagePaddingHorizontal = 8;
            this.btnAdjust.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.SubItemsExpandWidth = 14;
            this.btnAdjust.Text = "德行加減分";
            this.btnAdjust.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // btnWordComment
            // 
            this.btnWordComment.Enabled = false;
            this.btnWordComment.ImagePaddingHorizontal = 8;
            this.btnWordComment.Name = "btnWordComment";
            this.btnWordComment.SubItemsExpandWidth = 14;
            this.btnWordComment.Text = "文字評量";
            this.btnWordComment.Click += new System.EventHandler(this.btnWordComment_Click);
            // 
            // TeacherBiasRibbon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TeacherBiasRibbon";
            this.Size = new System.Drawing.Size(294, 110);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnAdjust;
        private DevComponents.DotNetBar.ButtonItem btnWordComment;
    }
}
