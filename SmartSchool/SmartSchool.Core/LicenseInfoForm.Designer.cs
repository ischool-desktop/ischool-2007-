namespace SmartSchool
{
    partial class LicenseInfoForm
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
            this.lblExpiration = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lblAccessPoint = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.lvIPList = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lblExpiration
            // 
            this.lblExpiration.BackColor = System.Drawing.Color.Transparent;
            this.lblExpiration.Location = new System.Drawing.Point(77, 39);
            this.lblExpiration.Name = "lblExpiration";
            this.lblExpiration.Size = new System.Drawing.Size(296, 19);
            this.lblExpiration.TabIndex = 23;
            this.lblExpiration.Text = "<未設定 >";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(9, 39);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(60, 19);
            this.labelX4.TabIndex = 24;
            this.labelX4.Text = "到  期  日";
            // 
            // lblAccessPoint
            // 
            this.lblAccessPoint.BackColor = System.Drawing.Color.Transparent;
            this.lblAccessPoint.Location = new System.Drawing.Point(77, 12);
            this.lblAccessPoint.Name = "lblAccessPoint";
            this.lblAccessPoint.Size = new System.Drawing.Size(296, 19);
            this.lblAccessPoint.TabIndex = 21;
            this.lblAccessPoint.Text = "<未設定>";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(9, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 19);
            this.labelX3.TabIndex = 22;
            this.labelX3.Text = "授權登入";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(9, 64);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(59, 19);
            this.labelX1.TabIndex = 24;
            this.labelX1.Text = "IP  範  圍";
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(310, 207);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(63, 21);
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "離開(&X)";
            // 
            // lvIPList
            // 
            // 
            // 
            // 
            this.lvIPList.Border.Class = "ListViewBorder";
            this.lvIPList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvIPList.FullRowSelect = true;
            this.lvIPList.Location = new System.Drawing.Point(9, 94);
            this.lvIPList.Name = "lvIPList";
            this.lvIPList.Size = new System.Drawing.Size(364, 101);
            this.lvIPList.TabIndex = 26;
            this.lvIPList.UseCompatibleStateImageBehavior = false;
            this.lvIPList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP 限制";
            this.columnHeader1.Width = 322;
            // 
            // LicenseInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(386, 237);
            this.Controls.Add(this.lvIPList);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblExpiration);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lblAccessPoint);
            this.Controls.Add(this.labelX3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LicenseInfoForm";
            this.Text = "授權資訊";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblExpiration;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX lblAccessPoint;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.ListViewEx lvIPList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}