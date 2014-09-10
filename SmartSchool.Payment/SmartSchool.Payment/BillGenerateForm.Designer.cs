namespace SmartSchool.Payment
{
    partial class BillGenerateForm
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
            this.btnGenerate = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.txtExpiration = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtBatchName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.pbProgress = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGenerate.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGenerate.Location = new System.Drawing.Point(271, 227);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "產生";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(361, 227);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "關閉";
            // 
            // txtExpiration
            // 
            // 
            // 
            // 
            this.txtExpiration.Border.Class = "TextBoxBorder";
            this.txtExpiration.Location = new System.Drawing.Point(96, 70);
            this.txtExpiration.Name = "txtExpiration";
            this.txtExpiration.Size = new System.Drawing.Size(340, 25);
            this.txtExpiration.TabIndex = 3;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(22, 73);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(74, 19);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "繳費截止日";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(22, 30);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(70, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "批 次 名 稱";
            // 
            // txtBatchName
            // 
            // 
            // 
            // 
            this.txtBatchName.Border.Class = "TextBoxBorder";
            this.txtBatchName.Location = new System.Drawing.Point(96, 27);
            this.txtBatchName.Name = "txtBatchName";
            this.txtBatchName.Size = new System.Drawing.Size(340, 25);
            this.txtBatchName.TabIndex = 1;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(22, 112);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(307, 71);
            this.labelX2.TabIndex = 6;
            this.labelX2.Text = "說明：\r\n1.過去產生之繳費單將註銷(仍繼續列入對帳範圍)。\r\n2.已繳費之金額將自動扣除(列入已繳金額減項)。\r\n3.在收費明細中是藍色的項目才會產生繳費單。";
            // 
            // pbProgress
            // 
            this.pbProgress.BackColor = System.Drawing.Color.Transparent;
            this.pbProgress.Location = new System.Drawing.Point(18, 192);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(419, 26);
            this.pbProgress.TabIndex = 7;
            this.pbProgress.Text = "progressBarX1";
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Location = new System.Drawing.Point(20, 227);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(247, 23);
            this.lblMsg.TabIndex = 6;
            // 
            // BillGenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(449, 256);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtBatchName);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtExpiration);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BillGenerateForm";
            this.Text = "產生繳費資料";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnGenerate;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExpiration;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBatchName;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ProgressBarX pbProgress;
        private DevComponents.DotNetBar.LabelX lblMsg;
    }
}