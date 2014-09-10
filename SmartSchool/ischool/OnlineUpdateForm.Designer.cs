namespace ischool
{
    partial class OnlineUpdateForm
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
            this.lblMessage = new DevComponents.DotNetBar.LabelX();
            this.pb = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(12, 12);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(473, 23);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "更新中 ...";
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(11, 41);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(474, 23);
            this.pb.TabIndex = 2;
            this.pb.Text = "progressBarX1";
            // 
            // labelX1
            // 
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelX1.Location = new System.Drawing.Point(11, 70);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(473, 24);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "IntelliSchool Online Update System";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // OnlineUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(503, 99);
            this.ControlBox = false;
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OnlineUpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新 SmartSchool";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnlineUpdateForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnlineUpdateForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblMessage;
        private DevComponents.DotNetBar.Controls.ProgressBarX pb;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}