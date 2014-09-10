namespace SmartSchool.GovernmentalDocument.Process
{
    partial class NameList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameList));
            this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
            this.btnItemNameList = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // ribbonBar1
            // 
            this.ribbonBar1.AutoOverflowEnabled = true;
            this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnItemNameList});
            this.ribbonBar1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBar1.Location = new System.Drawing.Point(22, 18);
            this.ribbonBar1.Name = "ribbonBar1";
            this.ribbonBar1.Size = new System.Drawing.Size(277, 110);
            this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonBar1.TabIndex = 0;
            this.ribbonBar1.Text = "ribbonBar1";
            // 
            // btnItemNameList
            // 
            this.btnItemNameList.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnItemNameList.FixedSize = new System.Drawing.Size(68, 68);
            this.btnItemNameList.Image = ((System.Drawing.Image)(resources.GetObject("btnItemNameList.Image")));
            this.btnItemNameList.ImagePaddingHorizontal = 8;
            this.btnItemNameList.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnItemNameList.Name = "btnItemNameList";
            this.btnItemNameList.SubItemsExpandWidth = 14;
            this.btnItemNameList.Text = "函報名冊";
            this.btnItemNameList.Click += new System.EventHandler(this.btnItemNameList_Click);
            // 
            // NameList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ribbonBar1);
            this.Name = "NameList";
            this.Size = new System.Drawing.Size(351, 144);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonBar ribbonBar1;
        private DevComponents.DotNetBar.ButtonItem btnItemNameList;
    }
}
