namespace DataManager
{
    partial class ModifyForm
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
            this.components = new System.ComponentModel.Container();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.txtNewReason = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.lblA = new DevComponents.DotNetBar.LabelX();
            this.lblB = new DevComponents.DotNetBar.LabelX();
            this.lblC = new DevComponents.DotNetBar.LabelX();
            this.txtA = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtB = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtC = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(220, 180);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "關閉";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtNewReason
            // 
            this.txtNewReason.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtNewReason.Border.Class = "TextBoxBorder";
            this.txtNewReason.Location = new System.Drawing.Point(45, 46);
            this.txtNewReason.Multiline = true;
            this.txtNewReason.Name = "txtNewReason";
            this.txtNewReason.Size = new System.Drawing.Size(250, 128);
            this.txtNewReason.TabIndex = 1;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(5, 46);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(34, 19);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "事由";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(139, 180);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.BackColor = System.Drawing.Color.Transparent;
            this.lblA.Location = new System.Drawing.Point(6, 12);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(34, 19);
            this.lblA.TabIndex = 4;
            this.lblA.Text = "大功";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.BackColor = System.Drawing.Color.Transparent;
            this.lblB.Location = new System.Drawing.Point(106, 12);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(34, 19);
            this.lblB.TabIndex = 4;
            this.lblB.Text = "小功";
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.BackColor = System.Drawing.Color.Transparent;
            this.lblC.Location = new System.Drawing.Point(206, 12);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(34, 19);
            this.lblC.TabIndex = 4;
            this.lblC.Text = "嘉獎";
            // 
            // txtA
            // 
            // 
            // 
            // 
            this.txtA.Border.Class = "TextBoxBorder";
            this.txtA.Location = new System.Drawing.Point(45, 9);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(50, 25);
            this.txtA.TabIndex = 5;
            // 
            // txtB
            // 
            // 
            // 
            // 
            this.txtB.Border.Class = "TextBoxBorder";
            this.txtB.Location = new System.Drawing.Point(145, 9);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(50, 25);
            this.txtB.TabIndex = 5;
            // 
            // txtC
            // 
            // 
            // 
            // 
            this.txtC.Border.Class = "TextBoxBorder";
            this.txtC.Location = new System.Drawing.Point(245, 9);
            this.txtC.Name = "txtC";
            this.txtC.Size = new System.Drawing.Size(50, 25);
            this.txtC.TabIndex = 5;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 210);
            this.Controls.Add(this.txtC);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.lblC);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtNewReason);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Name = "ModifyForm";
            this.Text = "修改獎懲";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewReason;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX lblA;
        private DevComponents.DotNetBar.LabelX lblB;
        private DevComponents.DotNetBar.LabelX lblC;
        private DevComponents.DotNetBar.Controls.TextBoxX txtA;
        private DevComponents.DotNetBar.Controls.TextBoxX txtB;
        private DevComponents.DotNetBar.Controls.TextBoxX txtC;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}