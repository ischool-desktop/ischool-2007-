namespace SmartSchool
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboAccount = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnLogin = new DevComponents.DotNetBar.ButtonX();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lnkSysAdmin = new System.Windows.Forms.LinkLabel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.lnkViewLicense = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(388, 96);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(22, 142);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 19);
            this.labelX2.TabIndex = 17;
            this.labelX2.Text = "密碼";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(22, 113);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(34, 19);
            this.labelX1.TabIndex = 18;
            this.labelX1.Text = "帳號";
            // 
            // cboAccount
            // 
            this.cboAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAccount.DisplayMember = "Text";
            this.cboAccount.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboAccount.DropDownHeight = 100;
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.IntegralHeight = false;
            this.cboAccount.ItemHeight = 19;
            this.cboAccount.Location = new System.Drawing.Point(71, 110);
            this.cboAccount.Margin = new System.Windows.Forms.Padding(4);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(324, 25);
            this.cboAccount.TabIndex = 11;
            this.cboAccount.WatermarkText = "請輸入帳號";
            this.cboAccount.Enter += new System.EventHandler(this.cboAccount_Enter);
            this.cboAccount.TextChanged += new System.EventHandler(this.cboAccount_TextChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.Location = new System.Drawing.Point(257, 195);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(70, 21);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "登入(&L)";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.AutoSize = true;
            this.checkBoxX1.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxX1.Checked = true;
            this.checkBoxX1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxX1.CheckValue = "Y";
            this.checkBoxX1.Location = new System.Drawing.Point(71, 169);
            this.checkBoxX1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(79, 21);
            this.checkBoxX1.TabIndex = 13;
            this.checkBoxX1.Text = "記住帳號";
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(332, 195);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(63, 21);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "離開(&X)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtPassword.Border.Class = "TextBoxBorder";
            this.txtPassword.Location = new System.Drawing.Point(71, 139);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(324, 25);
            this.txtPassword.TabIndex = 12;
            this.txtPassword.WatermarkText = "請輸入密碼";
            // 
            // lnkSysAdmin
            // 
            this.lnkSysAdmin.AutoSize = true;
            this.lnkSysAdmin.BackColor = System.Drawing.Color.Transparent;
            this.lnkSysAdmin.Location = new System.Drawing.Point(9, 197);
            this.lnkSysAdmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkSysAdmin.Name = "lnkSysAdmin";
            this.lnkSysAdmin.Size = new System.Drawing.Size(60, 17);
            this.lnkSysAdmin.TabIndex = 16;
            this.lnkSysAdmin.TabStop = true;
            this.lnkSysAdmin.Text = "變更設定";
            this.lnkSysAdmin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSysAdmin_LinkClicked);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.pictureBox1);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.lnkViewLicense);
            this.panelEx1.Controls.Add(this.lnkSysAdmin);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.txtPassword);
            this.panelEx1.Controls.Add(this.cboAccount);
            this.panelEx1.Controls.Add(this.btnExit);
            this.panelEx1.Controls.Add(this.btnLogin);
            this.panelEx1.Controls.Add(this.checkBoxX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(408, 226);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx1.Style.BorderWidth = 2;
            this.panelEx1.Style.CornerDiameter = 12;
            this.panelEx1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 20;
            // 
            // lnkViewLicense
            // 
            this.lnkViewLicense.AutoSize = true;
            this.lnkViewLicense.BackColor = System.Drawing.Color.Transparent;
            this.lnkViewLicense.Enabled = false;
            this.lnkViewLicense.Location = new System.Drawing.Point(72, 197);
            this.lnkViewLicense.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkViewLicense.Name = "lnkViewLicense";
            this.lnkViewLicense.Size = new System.Drawing.Size(60, 17);
            this.lnkViewLicense.TabIndex = 16;
            this.lnkViewLicense.TabStop = true;
            this.lnkViewLicense.Text = "檢視授權";
            this.lnkViewLicense.Visible = false;
            this.lnkViewLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewLicense_LinkClicked);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkMagenta;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(408, 226);
            this.ControlBox = false;
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "使用者登入";
            this.TransparencyKey = System.Drawing.Color.DarkMagenta;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboAccount;
        private DevComponents.DotNetBar.ButtonX btnLogin;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPassword;
        private System.Windows.Forms.LinkLabel lnkSysAdmin;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.LinkLabel lnkViewLicense;


    }
}