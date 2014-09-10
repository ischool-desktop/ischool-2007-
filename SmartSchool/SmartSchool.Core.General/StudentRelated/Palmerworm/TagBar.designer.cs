namespace SmartSchool.StudentRelated.Palmerworm
{
    partial class TagBar
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
            this.mark_container = new System.Windows.Forms.FlowLayoutPanel();
            this.tooltip = new DevComponents.DotNetBar.SuperTooltip();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // mark_container
            // 
            this.mark_container.AutoSize = true;
            this.mark_container.BackColor = System.Drawing.Color.Transparent;
            this.mark_container.Dock = System.Windows.Forms.DockStyle.Top;
            this.mark_container.Location = new System.Drawing.Point(0, 0);
            this.mark_container.Name = "mark_container";
            this.mark_container.Size = new System.Drawing.Size(550, 0);
            this.mark_container.TabIndex = 0;
            // 
            // superTooltip1
            // 
            this.tooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // TagBar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.mark_container);
            this.MinimumSize = new System.Drawing.Size(550, 0);
            this.Name = "TagBar";
            this.Size = new System.Drawing.Size(550, 40);
            this.Controls.SetChildIndex(this.mark_container, 0);
            this.Controls.SetChildIndex(this.picWaiting, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel mark_container;
        private DevComponents.DotNetBar.SuperTooltip tooltip;


    }
}
