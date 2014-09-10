namespace SmartSchool.TeacherRelated.Palmerworm
{
    internal partial  class BaseInfoItem
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtIDNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPhone = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCategory = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSTLoginAccount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSTLoginPwd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboGender = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtNickname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label10 = new System.Windows.Forms.Label();
            this.cboAccountType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::SmartSchool.Properties.Resources.studentsPic;
            this.pictureBox.InitialImage = global::SmartSchool.Properties.Resources.studentsPic;
            this.pictureBox.Location = new System.Drawing.Point(108, 17);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(91, 100);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "教師姓名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "性　　別";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "身分證號";
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Location = new System.Drawing.Point(363, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(140, 25);
            this.txtName.TabIndex = 4;
            this.txtName.Validated += new System.EventHandler(this.txtName_Validated);
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtIDNumber
            // 
            // 
            // 
            // 
            this.txtIDNumber.Border.Class = "TextBoxBorder";
            this.txtIDNumber.Location = new System.Drawing.Point(363, 104);
            this.txtIDNumber.Name = "txtIDNumber";
            this.txtIDNumber.Size = new System.Drawing.Size(140, 25);
            this.txtIDNumber.TabIndex = 8;
            this.txtIDNumber.TextChanged += new System.EventHandler(this.txtIDNumber_TextChanged);
            // 
            // txtPhone
            // 
            // 
            // 
            // 
            this.txtPhone.Border.Class = "TextBoxBorder";
            this.txtPhone.Location = new System.Drawing.Point(108, 134);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(140, 25);
            this.txtPhone.TabIndex = 5;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "聯絡電話";
            // 
            // txtEmail
            // 
            // 
            // 
            // 
            this.txtEmail.Border.Class = "TextBoxBorder";
            this.txtEmail.Location = new System.Drawing.Point(108, 163);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(140, 25);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "電子信箱";
            // 
            // txtCategory
            // 
            // 
            // 
            // 
            this.txtCategory.Border.Class = "TextBoxBorder";
            this.txtCategory.Location = new System.Drawing.Point(108, 192);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(140, 25);
            this.txtCategory.TabIndex = 9;
            this.txtCategory.TextChanged += new System.EventHandler(this.txtCategory_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "教師類別";
            // 
            // txtSTLoginAccount
            // 
            // 
            // 
            // 
            this.txtSTLoginAccount.Border.Class = "TextBoxBorder";
            this.txtSTLoginAccount.Location = new System.Drawing.Point(363, 133);
            this.txtSTLoginAccount.Name = "txtSTLoginAccount";
            this.txtSTLoginAccount.Size = new System.Drawing.Size(140, 25);
            this.txtSTLoginAccount.TabIndex = 1;
            this.txtSTLoginAccount.Validating += new System.ComponentModel.CancelEventHandler(this.txtSTLoginAccount_Validating);
            this.txtSTLoginAccount.TextChanged += new System.EventHandler(this.txtSTLoginAccount_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "登入帳號";
            // 
            // txtSTLoginPwd
            // 
            // 
            // 
            // 
            this.txtSTLoginPwd.Border.Class = "TextBoxBorder";
            this.txtSTLoginPwd.Location = new System.Drawing.Point(363, 162);
            this.txtSTLoginPwd.Name = "txtSTLoginPwd";
            this.txtSTLoginPwd.PasswordChar = '*';
            this.txtSTLoginPwd.Size = new System.Drawing.Size(140, 25);
            this.txtSTLoginPwd.TabIndex = 2;
            this.txtSTLoginPwd.TextChanged += new System.EventHandler(this.txtSTLoginPwd_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(285, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "登入密碼";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(285, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "帳號類型";
            // 
            // cboGender
            // 
            this.cboGender.DisplayMember = "Text";
            this.cboGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboGender.Location = new System.Drawing.Point(363, 74);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(140, 26);
            this.cboGender.TabIndex = 6;
            this.cboGender.Validated += new System.EventHandler(this.cboGender_Validated);
            this.cboGender.TextChanged += new System.EventHandler(this.cboGender_TextChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "男";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "女";
            // 
            // errors
            // 
            this.errors.ContainerControl = this;
            // 
            // txtNickname
            // 
            // 
            // 
            // 
            this.txtNickname.Border.Class = "TextBoxBorder";
            this.txtNickname.Location = new System.Drawing.Point(363, 45);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(140, 25);
            this.txtNickname.TabIndex = 4;
            this.txtNickname.Validated += new System.EventHandler(this.txtNickname_Validated);
            this.txtNickname.TextChanged += new System.EventHandler(this.txtNickname_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(285, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "暱　　稱";
            // 
            // cboAccountType
            // 
            this.cboAccountType.DisplayMember = "Text";
            this.cboAccountType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboAccountType.FormattingEnabled = true;
            this.cboAccountType.Items.AddRange(new object[] {
            this.comboItem3,
            this.comboItem4});
            this.cboAccountType.Location = new System.Drawing.Point(363, 191);
            this.cboAccountType.Name = "cboAccountType";
            this.cboAccountType.Size = new System.Drawing.Size(140, 26);
            this.cboAccountType.TabIndex = 18;
            this.cboAccountType.TextChanged += new System.EventHandler(this.cboAccountType_TextChanged);
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "Google";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "自定帳號";
            // 
            // BaseInfoItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.cboAccountType);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.cboGender);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSTLoginPwd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.txtSTLoginAccount);
            this.Controls.Add(this.txtIDNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNickname);
            this.Name = "BaseInfoItem";
            this.Size = new System.Drawing.Size(550, 226);
            this.Controls.SetChildIndex(this.txtNickname, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtIDNumber, 0);
            this.Controls.SetChildIndex(this.txtSTLoginAccount, 0);
            this.Controls.SetChildIndex(this.pictureBox, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtSTLoginPwd, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtPhone, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cboGender, 0);
            this.Controls.SetChildIndex(this.txtCategory, 0);
            this.Controls.SetChildIndex(this.picWaiting, 0);
            this.Controls.SetChildIndex(this.cboAccountType, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtIDNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhone;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEmail;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCategory;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSTLoginAccount;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSTLoginPwd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGender;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private System.Windows.Forms.ErrorProvider errors;
        private System.Windows.Forms.Label label10;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNickname;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboAccountType;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
    }
}
