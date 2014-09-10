namespace SmartSchool.SysAdmin
{
    partial class LicenseManager
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
            this.txtLoginName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtAccessPoint = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtExpireDate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPinCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtConfirmPin = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtIPScope = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.btnGenerate = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.sfdLicenseFile = new System.Windows.Forms.SaveFileDialog();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(11, 17);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(74, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "管理員帳號";
            // 
            // txtLoginName
            // 
            this.txtLoginName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtLoginName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtLoginName.Border.Class = "TextBoxBorder";
            this.txtLoginName.Location = new System.Drawing.Point(90, 14);
            this.txtLoginName.Margin = new System.Windows.Forms.Padding(4);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(288, 25);
            this.txtLoginName.TabIndex = 1;
            this.txtLoginName.WatermarkText = "請輸入管理員登入帳號";
            // 
            // txtPassword
            // 
            this.txtPassword.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPassword.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtPassword.Border.Class = "TextBoxBorder";
            this.txtPassword.Location = new System.Drawing.Point(90, 47);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(288, 25);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.WatermarkText = "請輸入管理員登入密碼";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(11, 50);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(74, 19);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "管理員密碼";
            // 
            // txtAccessPoint
            // 
            this.txtAccessPoint.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAccessPoint.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtAccessPoint.Border.Class = "TextBoxBorder";
            this.txtAccessPoint.Location = new System.Drawing.Point(90, 81);
            this.txtAccessPoint.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccessPoint.Name = "txtAccessPoint";
            this.txtAccessPoint.Size = new System.Drawing.Size(288, 25);
            this.txtAccessPoint.TabIndex = 5;
            this.txtAccessPoint.WatermarkText = "請輸入服務取存點位置，例：trialschool.tw";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(11, 84);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(74, 19);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "服務取存點";
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtLoginName);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.txtPassword);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.txtAccessPoint);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(401, 152);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "管理員登入資訊";
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.txtExpireDate);
            this.groupPanel2.Controls.Add(this.txtPinCode);
            this.groupPanel2.Controls.Add(this.labelX5);
            this.groupPanel2.Controls.Add(this.labelX4);
            this.groupPanel2.Controls.Add(this.txtConfirmPin);
            this.groupPanel2.Controls.Add(this.txtIPScope);
            this.groupPanel2.Controls.Add(this.labelX6);
            this.groupPanel2.Location = new System.Drawing.Point(12, 176);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(401, 154);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "SmartSchool 授權資料";
            // 
            // txtExpireDate
            // 
            this.txtExpireDate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtExpireDate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtExpireDate.Border.Class = "TextBoxBorder";
            this.txtExpireDate.Location = new System.Drawing.Point(90, 50);
            this.txtExpireDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtExpireDate.Name = "txtExpireDate";
            this.txtExpireDate.Size = new System.Drawing.Size(288, 25);
            this.txtExpireDate.TabIndex = 3;
            this.txtExpireDate.WatermarkText = "例：2015/12/10";
            // 
            // txtPinCode
            // 
            this.txtPinCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPinCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtPinCode.Border.Class = "TextBoxBorder";
            this.txtPinCode.Location = new System.Drawing.Point(90, 86);
            this.txtPinCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtPinCode.Name = "txtPinCode";
            this.txtPinCode.PasswordChar = '*';
            this.txtPinCode.Size = new System.Drawing.Size(128, 25);
            this.txtPinCode.TabIndex = 5;
            this.txtPinCode.WatermarkText = "請輸入授權碼";
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            this.labelX5.Location = new System.Drawing.Point(12, 53);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(74, 19);
            this.labelX5.TabIndex = 2;
            this.labelX5.Text = "授權到期日";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(12, 17);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(49, 19);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "IP 範圍";
            // 
            // txtConfirmPin
            // 
            this.txtConfirmPin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtConfirmPin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtConfirmPin.Border.Class = "TextBoxBorder";
            this.txtConfirmPin.Location = new System.Drawing.Point(221, 86);
            this.txtConfirmPin.Margin = new System.Windows.Forms.Padding(4);
            this.txtConfirmPin.Name = "txtConfirmPin";
            this.txtConfirmPin.PasswordChar = '*';
            this.txtConfirmPin.Size = new System.Drawing.Size(158, 25);
            this.txtConfirmPin.TabIndex = 6;
            this.txtConfirmPin.WatermarkText = "確認授權碼";
            // 
            // txtIPScope
            // 
            this.txtIPScope.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtIPScope.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtIPScope.Border.Class = "TextBoxBorder";
            this.txtIPScope.Location = new System.Drawing.Point(90, 14);
            this.txtIPScope.Margin = new System.Windows.Forms.Padding(4);
            this.txtIPScope.Name = "txtIPScope";
            this.txtIPScope.Size = new System.Drawing.Size(288, 25);
            this.txtIPScope.TabIndex = 1;
            this.txtIPScope.WatermarkText = "例：192.168.110.1,192.168.111.0/24";
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            this.labelX6.ForeColor = System.Drawing.Color.Red;
            this.labelX6.Location = new System.Drawing.Point(12, 89);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(47, 19);
            this.labelX6.TabIndex = 4;
            this.labelX6.Text = "授權碼";
            // 
            // btnGenerate
            // 
            this.btnGenerate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGenerate.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGenerate.Location = new System.Drawing.Point(257, 346);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "產生授權檔";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(338, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "離開";
            // 
            // sfdLicenseFile
            // 
            this.sfdLicenseFile.Filter = "*.lic|*.lic";
            this.sfdLicenseFile.Title = "儲存授權檔";
            // 
            // LicenseManager
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 378);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Name = "LicenseManager";
            this.Text = "產生 SmartSchool 授權檔案";
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLoginName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPassword;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAccessPoint;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPinCode;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtConfirmPin;
        private DevComponents.DotNetBar.Controls.TextBoxX txtIPScope;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExpireDate;
        private DevComponents.DotNetBar.ButtonX btnGenerate;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private System.Windows.Forms.SaveFileDialog sfdLicenseFile;
    }
}