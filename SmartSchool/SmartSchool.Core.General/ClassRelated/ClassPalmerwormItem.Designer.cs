namespace SmartSchool.ClassRelated
{
    partial class ClassPalmerwormItem
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
            this.picWaiting = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // picWaiting
            // 
            this.picWaiting.Image = global::SmartSchool.Properties.Resources.loading5;
            this.picWaiting.Location = new System.Drawing.Point(4, 4);
            this.picWaiting.Margin = new System.Windows.Forms.Padding(4);
            this.picWaiting.Name = "picWaiting";
            this.picWaiting.Size = new System.Drawing.Size(32, 32);
            this.picWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWaiting.TabIndex = 1;
            this.picWaiting.TabStop = false;
            this.picWaiting.Visible = false;
            // 
            // ClassPalmerwormItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picWaiting);
            this.Name = "ClassPalmerwormItem";
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox picWaiting;
    }
}
