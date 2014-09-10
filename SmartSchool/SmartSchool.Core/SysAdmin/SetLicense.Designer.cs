namespace SmartSchool.SysAdmin
{
    partial class SetLicense
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
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.txtLicenseFile = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lnkGenLicense = new System.Windows.Forms.LinkLabel();
            this.btnBrowser = new DevComponents.DotNetBar.ButtonX();
            this.ofdLicenseFile = new System.Windows.Forms.OpenFileDialog();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtPinCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(11, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "授權檔";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(333, 94);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(80, 24);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "確定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(421, 94);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(73, 24);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "離開";
            // 
            // txtLicenseFile
            // 
            this.txtLicenseFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtLicenseFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtLicenseFile.Border.Class = "TextBoxBorder";
            this.txtLicenseFile.Location = new System.Drawing.Point(63, 13);
            this.txtLicenseFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtLicenseFile.Name = "txtLicenseFile";
            this.txtLicenseFile.Size = new System.Drawing.Size(396, 25);
            this.txtLicenseFile.TabIndex = 1;
            this.txtLicenseFile.WatermarkText = "輸入授權檔路徑或選擇授權檔";
            // 
            // lnkGenLicense
            // 
            this.lnkGenLicense.AutoSize = true;
            this.lnkGenLicense.BackColor = System.Drawing.Color.Transparent;
            this.lnkGenLicense.Location = new System.Drawing.Point(13, 98);
            this.lnkGenLicense.Name = "lnkGenLicense";
            this.lnkGenLicense.Size = new System.Drawing.Size(86, 17);
            this.lnkGenLicense.TabIndex = 8;
            this.lnkGenLicense.TabStop = true;
            this.lnkGenLicense.Text = "建立授權檔案";
            this.lnkGenLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGenLicense_LinkClicked);
            // 
            // btnBrowser
            // 
            this.btnBrowser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBrowser.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowser.Location = new System.Drawing.Point(465, 13);
            this.btnBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(27, 24);
            this.btnBrowser.TabIndex = 7;
            this.btnBrowser.Text = "...";
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // ofdLicenseFile
            // 
            this.ofdLicenseFile.Filter = "*.lic|*.lic";
            this.ofdLicenseFile.Title = "開啟授權檔";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(11, 54);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(47, 19);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "授權碼";
            // 
            // txtPinCode
            // 
            this.txtPinCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPinCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtPinCode.Border.Class = "TextBoxBorder";
            this.txtPinCode.Location = new System.Drawing.Point(63, 51);
            this.txtPinCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtPinCode.Name = "txtPinCode";
            this.txtPinCode.PasswordChar = '*';
            this.txtPinCode.Size = new System.Drawing.Size(189, 25);
            this.txtPinCode.TabIndex = 10;
            this.txtPinCode.WatermarkText = "請輸入授權碼";
            // 
            // SetLicense
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(501, 128);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtPinCode);
            this.Controls.Add(this.lnkGenLicense);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtLicenseFile);
            this.Name = "SetLicense";
            this.Text = "安裝 SmartSchool 授權";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLicenseFile;
        private System.Windows.Forms.LinkLabel lnkGenLicense;
        private DevComponents.DotNetBar.ButtonX btnBrowser;
        private System.Windows.Forms.OpenFileDialog ofdLicenseFile;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPinCode;
    }
}