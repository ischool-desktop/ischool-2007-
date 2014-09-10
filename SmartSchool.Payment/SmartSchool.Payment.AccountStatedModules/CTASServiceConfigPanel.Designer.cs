namespace SmartSchool.Payment.AccountStatedModules
{
    partial class CTASServiceConfigPanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFTPUrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFTPAccount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFTPPassword = new System.Windows.Forms.TextBox();
            this.dgSchool = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEnterpirseCode = new System.Windows.Forms.TextBox();
            this.chLicense = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chPin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chSchoolCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchool)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "FTP 位置";
            // 
            // txtFTPUrl
            // 
            this.txtFTPUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFTPUrl.Location = new System.Drawing.Point(160, 149);
            this.txtFTPUrl.Name = "txtFTPUrl";
            this.txtFTPUrl.Size = new System.Drawing.Size(355, 25);
            this.txtFTPUrl.TabIndex = 11;
            this.txtFTPUrl.Text = "203.66.193.20";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "FTP 帳號";
            // 
            // txtFTPAccount
            // 
            this.txtFTPAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFTPAccount.Location = new System.Drawing.Point(160, 180);
            this.txtFTPAccount.Name = "txtFTPAccount";
            this.txtFTPAccount.Size = new System.Drawing.Size(355, 25);
            this.txtFTPAccount.TabIndex = 12;
            this.txtFTPAccount.Text = "cslc_md@outFTP03@203.66.193.20";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "FTP 密碼";
            // 
            // txtFTPPassword
            // 
            this.txtFTPPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFTPPassword.Location = new System.Drawing.Point(160, 211);
            this.txtFTPPassword.Name = "txtFTPPassword";
            this.txtFTPPassword.PasswordChar = '*';
            this.txtFTPPassword.Size = new System.Drawing.Size(355, 25);
            this.txtFTPPassword.TabIndex = 13;
            this.txtFTPPassword.Text = "s9reeb1r@fuxu4zp8";
            // 
            // dgSchool
            // 
            this.dgSchool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSchool.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgSchool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSchool.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chLicense,
            this.chPin,
            this.chSchoolCode,
            this.chEnabled});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSchool.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgSchool.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSchool.Location = new System.Drawing.Point(38, 13);
            this.dgSchool.Name = "dgSchool";
            this.dgSchool.RowHeadersWidth = 30;
            this.dgSchool.RowTemplate.Height = 24;
            this.dgSchool.Size = new System.Drawing.Size(477, 97);
            this.dgSchool.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "企業代碼";
            // 
            // txtEnterpirseCode
            // 
            this.txtEnterpirseCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnterpirseCode.Location = new System.Drawing.Point(160, 119);
            this.txtEnterpirseCode.Name = "txtEnterpirseCode";
            this.txtEnterpirseCode.Size = new System.Drawing.Size(355, 25);
            this.txtEnterpirseCode.TabIndex = 11;
            // 
            // chLicense
            // 
            this.chLicense.HeaderText = "授權檔路徑";
            this.chLicense.MinimumWidth = 200;
            this.chLicense.Name = "chLicense";
            this.chLicense.Width = 200;
            // 
            // chPin
            // 
            this.chPin.HeaderText = "PinCode";
            this.chPin.MinimumWidth = 80;
            this.chPin.Name = "chPin";
            this.chPin.Width = 80;
            // 
            // chSchoolCode
            // 
            this.chSchoolCode.HeaderText = "學校代碼";
            this.chSchoolCode.MinimumWidth = 60;
            this.chSchoolCode.Name = "chSchoolCode";
            // 
            // chEnabled
            // 
            this.chEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chEnabled.FalseValue = "False";
            this.chEnabled.HeaderText = "啟用";
            this.chEnabled.Name = "chEnabled";
            this.chEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chEnabled.TrueValue = "True";
            // 
            // CTASServiceConfigPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.dgSchool);
            this.Controls.Add(this.txtFTPPassword);
            this.Controls.Add(this.txtFTPAccount);
            this.Controls.Add(this.txtEnterpirseCode);
            this.Controls.Add(this.txtFTPUrl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "CTASServiceConfigPanel";
            this.Size = new System.Drawing.Size(549, 244);
            ((System.ComponentModel.ISupportInitialize)(this.dgSchool)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFTPUrl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFTPAccount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFTPPassword;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgSchool;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEnterpirseCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn chLicense;
        private System.Windows.Forms.DataGridViewTextBoxColumn chPin;
        private System.Windows.Forms.DataGridViewTextBoxColumn chSchoolCode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chEnabled;

    }
}
