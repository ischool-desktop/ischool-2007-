namespace SmartSchool.Diagnostics
{
    partial class DiagMainForm
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
            this.btnRun = new System.Windows.Forms.Button();
            this.btnBenchNetwork = new System.Windows.Forms.Button();
            this.btnBenchThread = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(47, 43);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(112, 37);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "進行偵錯及報告";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnBenchNetwork
            // 
            this.btnBenchNetwork.Enabled = false;
            this.btnBenchNetwork.Location = new System.Drawing.Point(47, 107);
            this.btnBenchNetwork.Name = "btnBenchNetwork";
            this.btnBenchNetwork.Size = new System.Drawing.Size(112, 37);
            this.btnBenchNetwork.TabIndex = 0;
            this.btnBenchNetwork.Text = "進行網路效能評估";
            this.btnBenchNetwork.UseVisualStyleBackColor = true;
            this.btnBenchNetwork.Click += new System.EventHandler(this.btnBenchNetwork_Click);
            // 
            // btnBenchThread
            // 
            this.btnBenchThread.Enabled = false;
            this.btnBenchThread.Location = new System.Drawing.Point(41, 175);
            this.btnBenchThread.Name = "btnBenchThread";
            this.btnBenchThread.Size = new System.Drawing.Size(125, 37);
            this.btnBenchThread.TabIndex = 0;
            this.btnBenchThread.Text = "進行多線程效能評估";
            this.btnBenchThread.UseVisualStyleBackColor = true;
            this.btnBenchThread.Click += new System.EventHandler(this.btnBenchThread_Click);
            // 
            // DiagMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 254);
            this.Controls.Add(this.btnBenchThread);
            this.Controls.Add(this.btnBenchNetwork);
            this.Controls.Add(this.btnRun);
            this.Name = "DiagMainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnBenchNetwork;
        private System.Windows.Forms.Button btnBenchThread;
    }
}