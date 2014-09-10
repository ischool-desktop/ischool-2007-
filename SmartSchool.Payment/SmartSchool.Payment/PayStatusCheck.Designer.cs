namespace SmartSchool.Payment
{
    partial class PayStatusCheck
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnSaveResult = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.lblTotal = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lblFail = new DevComponents.DotNetBar.LabelX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.lblSuccess = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(103, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "本次核對總數";
            // 
            // btnSaveResult
            // 
            this.btnSaveResult.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveResult.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveResult.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveResult.Enabled = false;
            this.btnSaveResult.Location = new System.Drawing.Point(233, 103);
            this.btnSaveResult.Name = "btnSaveResult";
            this.btnSaveResult.Size = new System.Drawing.Size(75, 23);
            this.btnSaveResult.TabIndex = 1;
            this.btnSaveResult.Text = "儲存結果";
            this.btnSaveResult.Click += new System.EventHandler(this.btnSaveResult_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(314, 103);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "離開";
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Location = new System.Drawing.Point(126, 12);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(103, 23);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "計算中...";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(12, 68);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(103, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "核對失敗數";
            // 
            // lblFail
            // 
            this.lblFail.BackColor = System.Drawing.Color.Transparent;
            this.lblFail.Location = new System.Drawing.Point(126, 68);
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size(103, 23);
            this.lblFail.TabIndex = 0;
            this.lblFail.Text = "計算中...";
            // 
            // labelX13
            // 
            this.labelX13.BackColor = System.Drawing.Color.Transparent;
            this.labelX13.Location = new System.Drawing.Point(12, 39);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(103, 23);
            this.labelX13.TabIndex = 0;
            this.labelX13.Text = "核對成功數";
            // 
            // lblSuccess
            // 
            this.lblSuccess.BackColor = System.Drawing.Color.Transparent;
            this.lblSuccess.Location = new System.Drawing.Point(126, 39);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(103, 23);
            this.lblSuccess.TabIndex = 0;
            this.lblSuccess.Text = "計算中...";
            // 
            // PayStatusCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(396, 135);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSaveResult);
            this.Controls.Add(this.lblFail);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lblSuccess);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.labelX13);
            this.Controls.Add(this.labelX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PayStatusCheck";
            this.Text = "對帳處理";
            this.Load += new System.EventHandler(this.PayStatusCheck_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSaveResult;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.LabelX lblTotal;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX lblFail;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.LabelX lblSuccess;
    }
}