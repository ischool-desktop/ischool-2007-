namespace SmartSchool.StudentRelated.Palmerworm
{
    partial class BaseInfoPalmerwormItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseInfoPalmerwormItem));
            this.label77 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.txtSSN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label78 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label81 = new System.Windows.Forms.Label();
            this.txtEngName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label80 = new System.Windows.Forms.Label();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.pic2 = new System.Windows.Forms.PictureBox();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.ctxChange1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.ctxChange2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.od = new System.Windows.Forms.OpenFileDialog();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.txtBirthPlace = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label79 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtBirthDate = new SmartSchool.Common.DateTimeTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoginID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLoginPwd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGender = new SmartSchool.StudentRelated.Palmerworm.ComboBoxAdv();
            this.cboNationality = new SmartSchool.StudentRelated.Palmerworm.ComboBoxAdv();
            this.cboAccountType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // picWaiting
            // 
            this.picWaiting.Location = new System.Drawing.Point(8, 4);
            this.picWaiting.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.BackColor = System.Drawing.Color.Transparent;
            this.label77.Location = new System.Drawing.Point(294, 21);
            this.label77.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(60, 17);
            this.label77.TabIndex = 205;
            this.label77.Text = "姓　　名";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.BackColor = System.Drawing.Color.Transparent;
            this.label76.Location = new System.Drawing.Point(30, 139);
            this.label76.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(60, 17);
            this.label76.TabIndex = 207;
            this.label76.Text = "英文姓名";
            // 
            // txtSSN
            // 
            // 
            // 
            // 
            this.txtSSN.Border.Class = "TextBoxBorder";
            this.txtSSN.Location = new System.Drawing.Point(366, 48);
            this.txtSSN.Margin = new System.Windows.Forms.Padding(4);
            this.txtSSN.MaxLength = 20;
            this.txtSSN.Name = "txtSSN";
            this.txtSSN.Size = new System.Drawing.Size(138, 25);
            this.txtSSN.TabIndex = 2;
            this.txtSSN.Validating += new System.ComponentModel.CancelEventHandler(this.txtSSN_Validating);
            this.txtSSN.TextChanged += new System.EventHandler(this.txtSSN_TextChanged);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.BackColor = System.Drawing.Color.Transparent;
            this.label78.Location = new System.Drawing.Point(294, 111);
            this.label78.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(60, 17);
            this.label78.TabIndex = 195;
            this.label78.Text = "生　　日";
            this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.BackColor = System.Drawing.Color.Transparent;
            this.label82.Location = new System.Drawing.Point(294, 81);
            this.label82.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(60, 17);
            this.label82.TabIndex = 194;
            this.label82.Text = "性　　別";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Location = new System.Drawing.Point(366, 17);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.MaxLength = 10;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(138, 25);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.BackColor = System.Drawing.Color.Transparent;
            this.label81.Location = new System.Drawing.Point(294, 51);
            this.label81.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(60, 17);
            this.label81.TabIndex = 192;
            this.label81.Text = "身分證號";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEngName
            // 
            // 
            // 
            // 
            this.txtEngName.Border.Class = "TextBoxBorder";
            this.txtEngName.Location = new System.Drawing.Point(97, 137);
            this.txtEngName.Margin = new System.Windows.Forms.Padding(4);
            this.txtEngName.MaxLength = 30;
            this.txtEngName.Name = "txtEngName";
            this.txtEngName.Size = new System.Drawing.Size(152, 25);
            this.txtEngName.TabIndex = 4;
            this.txtEngName.TextChanged += new System.EventHandler(this.txtEngName_TextChanged);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.BackColor = System.Drawing.Color.Transparent;
            this.label80.Location = new System.Drawing.Point(30, 173);
            this.label80.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(75, 17);
            this.label80.TabIndex = 191;
            this.label80.Text = "出  生  地";
            this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pic1
            // 
            this.contextMenuBar1.SetContextMenuEx(this.pic1, this.ctxChange1);
            this.pic1.Image = ((System.Drawing.Image)(resources.GetObject("pic1.Image")));
            this.pic1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pic1.InitialImage")));
            this.pic1.Location = new System.Drawing.Point(49, 19);
            this.pic1.Margin = new System.Windows.Forms.Padding(4);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(90, 100);
            this.pic1.TabIndex = 208;
            this.pic1.TabStop = false;
            // 
            // pic2
            // 
            this.contextMenuBar1.SetContextMenuEx(this.pic2, this.ctxChange2);
            this.pic2.Image = ((System.Drawing.Image)(resources.GetObject("pic2.Image")));
            this.pic2.InitialImage = ((System.Drawing.Image)(resources.GetObject("pic2.InitialImage")));
            this.pic2.Location = new System.Drawing.Point(149, 19);
            this.pic2.Margin = new System.Windows.Forms.Padding(4);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(90, 100);
            this.pic2.TabIndex = 209;
            this.pic2.TabStop = false;
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ctxChange1,
            this.ctxChange2});
            this.contextMenuBar1.Location = new System.Drawing.Point(65, 69);
            this.contextMenuBar1.Margin = new System.Windows.Forms.Padding(4);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(239, 25);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.contextMenuBar1.TabIndex = 210;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            // 
            // ctxChange1
            // 
            this.ctxChange1.AutoExpandOnClick = true;
            this.ctxChange1.ImagePaddingHorizontal = 8;
            this.ctxChange1.Name = "ctxChange1";
            this.ctxChange1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem5});
            this.ctxChange1.Text = "Change 1";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "變更照片";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "另存照片";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem5
            // 
            this.buttonItem5.ImagePaddingHorizontal = 8;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Text = "清除照片";
            this.buttonItem5.Click += new System.EventHandler(this.buttonItem5_Click);
            // 
            // ctxChange2
            // 
            this.ctxChange2.AutoExpandOnClick = true;
            this.ctxChange2.ImagePaddingHorizontal = 8;
            this.ctxChange2.Name = "ctxChange2";
            this.ctxChange2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem3,
            this.buttonItem4,
            this.buttonItem6});
            this.ctxChange2.Text = "Change 2";
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "變更照片";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "另存照片";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.ImagePaddingHorizontal = 8;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "清除照片";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "comboItem1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "comboItem2";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "comboItem3";
            // 
            // txtBirthPlace
            // 
            // 
            // 
            // 
            this.txtBirthPlace.Border.Class = "TextBoxBorder";
            this.txtBirthPlace.Location = new System.Drawing.Point(97, 170);
            this.txtBirthPlace.Margin = new System.Windows.Forms.Padding(4);
            this.txtBirthPlace.MaxLength = 30;
            this.txtBirthPlace.Name = "txtBirthPlace";
            this.txtBirthPlace.Size = new System.Drawing.Size(152, 25);
            this.txtBirthPlace.TabIndex = 6;
            this.txtBirthPlace.TextChanged += new System.EventHandler(this.txtBirthPlace_TextChanged);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.BackColor = System.Drawing.Color.Transparent;
            this.label79.Location = new System.Drawing.Point(30, 204);
            this.label79.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(67, 17);
            this.label79.TabIndex = 193;
            this.label79.Text = "國 　　籍";
            this.label79.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtBirthDate
            // 
            this.txtBirthDate.AllowNull = true;
            // 
            // 
            // 
            this.txtBirthDate.Border.Class = "TextBoxBorder";
            this.txtBirthDate.EmptyString = "";
            this.txtBirthDate.Location = new System.Drawing.Point(366, 107);
            this.txtBirthDate.Name = "txtBirthDate";
            this.txtBirthDate.Size = new System.Drawing.Size(138, 25);
            this.txtBirthDate.TabIndex = 5;
            this.txtBirthDate.WatermarkText = "yyyy/mm/dd";
            this.txtBirthDate.Validated += new System.EventHandler(this.txtBirthDate_Validated_1);
            this.txtBirthDate.TextChanged += new System.EventHandler(this.txtBirthDate_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(294, 141);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 191;
            this.label1.Text = "登入帳號";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLoginID
            // 
            // 
            // 
            // 
            this.txtLoginID.Border.Class = "TextBoxBorder";
            this.txtLoginID.Location = new System.Drawing.Point(366, 138);
            this.txtLoginID.Margin = new System.Windows.Forms.Padding(4);
            this.txtLoginID.MaxLength = 25;
            this.txtLoginID.Name = "txtLoginID";
            this.txtLoginID.Size = new System.Drawing.Size(138, 25);
            this.txtLoginID.TabIndex = 7;
            this.txtLoginID.Validating += new System.ComponentModel.CancelEventHandler(this.txtLoginID_Validating);
            this.txtLoginID.TextChanged += new System.EventHandler(this.txtLoginID_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(294, 171);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 191;
            this.label2.Text = "登入密碼";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLoginPwd
            // 
            // 
            // 
            // 
            this.txtLoginPwd.Border.Class = "TextBoxBorder";
            this.txtLoginPwd.Location = new System.Drawing.Point(366, 169);
            this.txtLoginPwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtLoginPwd.MaxLength = 50;
            this.txtLoginPwd.Name = "txtLoginPwd";
            this.txtLoginPwd.PasswordChar = '●';
            this.txtLoginPwd.Size = new System.Drawing.Size(138, 25);
            this.txtLoginPwd.TabIndex = 9;
            this.txtLoginPwd.TextChanged += new System.EventHandler(this.txtLoginPwd_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(294, 201);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 193;
            this.label3.Text = "帳號類型";
            // 
            // cboGender
            // 
            this.cboGender.DisplayMember = "Text";
            this.cboGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboGender.FormattingEnabled = true;
            this.cboGender.ItemHeight = 16;
            this.cboGender.Location = new System.Drawing.Point(366, 79);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(138, 22);
            this.cboGender.TabIndex = 3;
            this.cboGender.SelectedIndexChanged += new System.EventHandler(this.cboGender_SelectedIndexChanged);
            // 
            // cboNationality
            // 
            this.cboNationality.DisplayMember = "Text";
            this.cboNationality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNationality.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboNationality.FormattingEnabled = true;
            this.cboNationality.ItemHeight = 16;
            this.cboNationality.Location = new System.Drawing.Point(97, 202);
            this.cboNationality.Margin = new System.Windows.Forms.Padding(4);
            this.cboNationality.Name = "cboNationality";
            this.cboNationality.Size = new System.Drawing.Size(152, 22);
            this.cboNationality.TabIndex = 8;
            this.cboNationality.TextChanged += new System.EventHandler(this.cboNationality_TextChanged);
            // 
            // cboAccountType
            // 
            this.cboAccountType.DisplayMember = "Text";
            this.cboAccountType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboAccountType.FormattingEnabled = true;
            this.cboAccountType.ItemHeight = 19;
            this.cboAccountType.Items.AddRange(new object[] {
            this.comboItem4,
            this.comboItem5});
            this.cboAccountType.Location = new System.Drawing.Point(366, 199);
            this.cboAccountType.Name = "cboAccountType";
            this.cboAccountType.Size = new System.Drawing.Size(138, 25);
            this.cboAccountType.TabIndex = 211;
            this.cboAccountType.TextChanged += new System.EventHandler(this.cboAccountType_TextChanged);
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "Google";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "自定帳號";
            // 
            // BaseInfoPalmerwormItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.txtBirthDate);
            this.Controls.Add(this.cboAccountType);
            this.Controls.Add(this.cboGender);
            this.Controls.Add(this.label76);
            this.Controls.Add(this.contextMenuBar1);
            this.Controls.Add(this.txtBirthPlace);
            this.Controls.Add(this.txtLoginID);
            this.Controls.Add(this.txtSSN);
            this.Controls.Add(this.cboNationality);
            this.Controls.Add(this.label77);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.pic2);
            this.Controls.Add(this.txtLoginPwd);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.label78);
            this.Controls.Add(this.txtEngName);
            this.Controls.Add(this.label79);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label82);
            this.Controls.Add(this.label81);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label80);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "BaseInfoPalmerwormItem";
            this.Size = new System.Drawing.Size(550, 236);
            this.Controls.SetChildIndex(this.label80, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label81, 0);
            this.Controls.SetChildIndex(this.label82, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label79, 0);
            this.Controls.SetChildIndex(this.txtEngName, 0);
            this.Controls.SetChildIndex(this.label78, 0);
            this.Controls.SetChildIndex(this.pic1, 0);
            this.Controls.SetChildIndex(this.txtLoginPwd, 0);
            this.Controls.SetChildIndex(this.pic2, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label77, 0);
            this.Controls.SetChildIndex(this.cboNationality, 0);
            this.Controls.SetChildIndex(this.txtSSN, 0);
            this.Controls.SetChildIndex(this.txtLoginID, 0);
            this.Controls.SetChildIndex(this.txtBirthPlace, 0);
            this.Controls.SetChildIndex(this.contextMenuBar1, 0);
            this.Controls.SetChildIndex(this.label76, 0);
            this.Controls.SetChildIndex(this.cboGender, 0);
            this.Controls.SetChildIndex(this.cboAccountType, 0);
            this.Controls.SetChildIndex(this.txtBirthDate, 0);
            this.Controls.SetChildIndex(this.picWaiting, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label77;
        internal System.Windows.Forms.Label label76;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtSSN;
        internal System.Windows.Forms.Label label78;
        internal System.Windows.Forms.Label label82;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtName;
        internal System.Windows.Forms.Label label81;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtEngName;
        internal System.Windows.Forms.Label label80;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.PictureBox pic2;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
        private DevComponents.DotNetBar.ButtonItem ctxChange1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem ctxChange2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private System.Windows.Forms.OpenFileDialog od;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;       
        internal DevComponents.DotNetBar.Controls.TextBoxX txtBirthPlace;
        internal System.Windows.Forms.Label label79;
        private ComboBoxAdv cboNationality;
        private ComboBoxAdv cboGender;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private SmartSchool.Common.DateTimeTextBox txtBirthDate;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtLoginID;
        internal System.Windows.Forms.Label label1;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtLoginPwd;
        internal System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        internal System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboAccountType;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
    }
}
