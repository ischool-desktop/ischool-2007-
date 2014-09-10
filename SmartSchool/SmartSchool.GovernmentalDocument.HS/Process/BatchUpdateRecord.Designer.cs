namespace SmartSchool.GovernmentalDocument.Process
{
    partial class BatchUpdateRecord
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
            this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
            this.btnStudentUR = new DevComponents.DotNetBar.ButtonItem();
            this.btnChangeInfo = new DevComponents.DotNetBar.ButtonItem();
            this.btnChangeDept = new DevComponents.DotNetBar.ButtonItem();
            this.btnBatchGraduate = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // ribbonBar1
            // 
            this.ribbonBar1.AutoOverflowEnabled = true;
            this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnStudentUR});
            this.ribbonBar1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBar1.Location = new System.Drawing.Point(14, 14);
            this.ribbonBar1.Name = "ribbonBar1";
            this.ribbonBar1.Size = new System.Drawing.Size(271, 106);
            this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonBar1.TabIndex = 0;
            this.ribbonBar1.Text = "ribbonBar1";
            // 
            // btnStudentUR
            // 
            this.btnStudentUR.AutoExpandOnClick = true;
            this.btnStudentUR.ImagePaddingHorizontal = 8;
            this.btnStudentUR.Name = "btnStudentUR";
            this.btnStudentUR.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnChangeDept,
            this.btnChangeInfo,
            this.btnBatchGraduate});
            this.btnStudentUR.SubItemsExpandWidth = 14;
            this.btnStudentUR.Text = "學生異動";
            // 
            // btnChangeInfo
            // 
            this.btnChangeInfo.Enabled = false;
            this.btnChangeInfo.ImagePaddingHorizontal = 8;
            this.btnChangeInfo.Name = "btnChangeInfo";
            this.btnChangeInfo.Text = "學籍更正";
            this.btnChangeInfo.Click += new System.EventHandler(this.btnChangeInfo_Click);
            // 
            // btnChangeDept
            // 
            this.btnChangeDept.Enabled = false;
            this.btnChangeDept.ImagePaddingHorizontal = 8;
            this.btnChangeDept.Name = "btnChangeDept";
            this.btnChangeDept.Text = "轉科";
            this.btnChangeDept.Click += new System.EventHandler(this.btnChangeDept_Click);
            // 
            // btnBatchGraduate
            // 
            this.btnBatchGraduate.Enabled = false;
            this.btnBatchGraduate.ImagePaddingHorizontal = 8;
            this.btnBatchGraduate.Name = "btnBatchGraduate";
            this.btnBatchGraduate.SubItemsExpandWidth = 14;
            this.btnBatchGraduate.Text = "批次畢業異動";
            this.btnBatchGraduate.Click += new System.EventHandler(this.btnBatchGraduate_Click);
            // 
            // BatchGraduate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ribbonBar1);
            this.Name = "BatchGraduate";
            this.Size = new System.Drawing.Size(438, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonBar ribbonBar1;
        private DevComponents.DotNetBar.ButtonItem btnBatchGraduate;
        private DevComponents.DotNetBar.ButtonItem btnStudentUR;
        private DevComponents.DotNetBar.ButtonItem btnChangeInfo;
        private DevComponents.DotNetBar.ButtonItem btnChangeDept;
    }
}
