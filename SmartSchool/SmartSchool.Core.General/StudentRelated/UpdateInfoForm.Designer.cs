namespace SmartSchool.StudentRelated
{
    partial class UpdateInfoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUpdateDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboUpdateType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboUpdateReason = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboUpdateCode = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtpUpdateDate = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.pnlSnapshoot = new System.Windows.Forms.Panel();
            this.rbtGirl = new System.Windows.Forms.RadioButton();
            this.rbtBoy = new System.Windows.Forms.RadioButton();
            this.txtLastNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtStudentID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtIDNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboDept = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboGradeYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtpBirthdate = new SmartSchool.StudentRelated.Palmerworm.DateTimePickerAdv();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlIn = new System.Windows.Forms.Panel();
            this.txtFromNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label19 = new System.Windows.Forms.Label();
            this.txtFromDept = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label18 = new System.Windows.Forms.Label();
            this.txtFromStudentID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label17 = new System.Windows.Forms.Label();
            this.txtFromSchool = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlSnapshoot.SuspendLayout();
            this.pnlIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "異動日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "異動類別";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "備註事項";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "異動代碼";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "異動原因";
            // 
            // txtUpdateDescription
            // 
            // 
            // 
            // 
            this.txtUpdateDescription.Border.Class = "TextBoxBorder";
            this.txtUpdateDescription.Location = new System.Drawing.Point(90, 74);
            this.txtUpdateDescription.Name = "txtUpdateDescription";
            this.txtUpdateDescription.Size = new System.Drawing.Size(371, 22);
            this.txtUpdateDescription.TabIndex = 5;
            // 
            // cboUpdateType
            // 
            this.cboUpdateType.DisplayMember = "Text";
            this.cboUpdateType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboUpdateType.FormattingEnabled = true;
            this.cboUpdateType.ItemHeight = 15;
            this.cboUpdateType.Location = new System.Drawing.Point(90, 45);
            this.cboUpdateType.Name = "cboUpdateType";
            this.cboUpdateType.Size = new System.Drawing.Size(133, 21);
            this.cboUpdateType.TabIndex = 6;
            this.cboUpdateType.TextChanged += new System.EventHandler(this.cboUpdateType_TextChanged);
            // 
            // cboUpdateReason
            // 
            this.cboUpdateReason.DisplayMember = "Text";
            this.cboUpdateReason.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboUpdateReason.FormattingEnabled = true;
            this.cboUpdateReason.ItemHeight = 15;
            this.cboUpdateReason.Location = new System.Drawing.Point(332, 47);
            this.cboUpdateReason.Name = "cboUpdateReason";
            this.cboUpdateReason.Size = new System.Drawing.Size(129, 21);
            this.cboUpdateReason.TabIndex = 7;
            // 
            // cboUpdateCode
            // 
            this.cboUpdateCode.DisplayMember = "Text";
            this.cboUpdateCode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboUpdateCode.FormattingEnabled = true;
            this.cboUpdateCode.ItemHeight = 15;
            this.cboUpdateCode.Location = new System.Drawing.Point(332, 17);
            this.cboUpdateCode.Name = "cboUpdateCode";
            this.cboUpdateCode.Size = new System.Drawing.Size(129, 21);
            this.cboUpdateCode.TabIndex = 8;
            this.cboUpdateCode.TextChanged += new System.EventHandler(this.cboUpdateCode_TextChanged);
            // 
            // dtpUpdateDate
            // 
            this.dtpUpdateDate.Location = new System.Drawing.Point(90, 17);
            this.dtpUpdateDate.Name = "dtpUpdateDate";
            this.dtpUpdateDate.Size = new System.Drawing.Size(133, 22);
            this.dtpUpdateDate.TabIndex = 9;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Location = new System.Drawing.Point(307, 343);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "確定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(388, 343);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlSnapshoot
            // 
            this.pnlSnapshoot.Controls.Add(this.rbtGirl);
            this.pnlSnapshoot.Controls.Add(this.rbtBoy);
            this.pnlSnapshoot.Controls.Add(this.txtLastNumber);
            this.pnlSnapshoot.Controls.Add(this.txtStudentID);
            this.pnlSnapshoot.Controls.Add(this.txtIDNumber);
            this.pnlSnapshoot.Controls.Add(this.txtName);
            this.pnlSnapshoot.Controls.Add(this.cboDept);
            this.pnlSnapshoot.Controls.Add(this.cboGradeYear);
            this.pnlSnapshoot.Controls.Add(this.cboSemester);
            this.pnlSnapshoot.Controls.Add(this.cboSchoolYear);
            this.pnlSnapshoot.Controls.Add(this.dtpBirthdate);
            this.pnlSnapshoot.Controls.Add(this.label11);
            this.pnlSnapshoot.Controls.Add(this.label12);
            this.pnlSnapshoot.Controls.Add(this.label13);
            this.pnlSnapshoot.Controls.Add(this.label14);
            this.pnlSnapshoot.Controls.Add(this.label15);
            this.pnlSnapshoot.Controls.Add(this.label10);
            this.pnlSnapshoot.Controls.Add(this.label9);
            this.pnlSnapshoot.Controls.Add(this.label8);
            this.pnlSnapshoot.Controls.Add(this.label7);
            this.pnlSnapshoot.Controls.Add(this.label6);
            this.pnlSnapshoot.Location = new System.Drawing.Point(12, 102);
            this.pnlSnapshoot.Name = "pnlSnapshoot";
            this.pnlSnapshoot.Size = new System.Drawing.Size(473, 159);
            this.pnlSnapshoot.TabIndex = 12;
            // 
            // rbtGirl
            // 
            this.rbtGirl.AutoSize = true;
            this.rbtGirl.Location = new System.Drawing.Point(123, 97);
            this.rbtGirl.Name = "rbtGirl";
            this.rbtGirl.Size = new System.Drawing.Size(35, 16);
            this.rbtGirl.TabIndex = 21;
            this.rbtGirl.TabStop = true;
            this.rbtGirl.Text = "女";
            this.rbtGirl.UseVisualStyleBackColor = true;
            // 
            // rbtBoy
            // 
            this.rbtBoy.AutoSize = true;
            this.rbtBoy.Location = new System.Drawing.Point(79, 98);
            this.rbtBoy.Name = "rbtBoy";
            this.rbtBoy.Size = new System.Drawing.Size(35, 16);
            this.rbtBoy.TabIndex = 20;
            this.rbtBoy.TabStop = true;
            this.rbtBoy.Text = "男";
            this.rbtBoy.UseVisualStyleBackColor = true;
            // 
            // txtLastNumber
            // 
            // 
            // 
            // 
            this.txtLastNumber.Border.Class = "TextBoxBorder";
            this.txtLastNumber.Location = new System.Drawing.Point(320, 126);
            this.txtLastNumber.Name = "txtLastNumber";
            this.txtLastNumber.Size = new System.Drawing.Size(131, 22);
            this.txtLastNumber.TabIndex = 19;
            // 
            // txtStudentID
            // 
            // 
            // 
            // 
            this.txtStudentID.Border.Class = "TextBoxBorder";
            this.txtStudentID.Location = new System.Drawing.Point(80, 126);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new System.Drawing.Size(131, 22);
            this.txtStudentID.TabIndex = 18;
            // 
            // txtIDNumber
            // 
            // 
            // 
            // 
            this.txtIDNumber.Border.Class = "TextBoxBorder";
            this.txtIDNumber.Location = new System.Drawing.Point(320, 69);
            this.txtIDNumber.Name = "txtIDNumber";
            this.txtIDNumber.Size = new System.Drawing.Size(129, 22);
            this.txtIDNumber.TabIndex = 17;
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Location = new System.Drawing.Point(74, 69);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(137, 22);
            this.txtName.TabIndex = 16;
            // 
            // cboDept
            // 
            this.cboDept.DisplayMember = "Text";
            this.cboDept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDept.FormattingEnabled = true;
            this.cboDept.ItemHeight = 15;
            this.cboDept.Location = new System.Drawing.Point(320, 39);
            this.cboDept.Name = "cboDept";
            this.cboDept.Size = new System.Drawing.Size(131, 21);
            this.cboDept.TabIndex = 15;
            // 
            // cboGradeYear
            // 
            this.cboGradeYear.DisplayMember = "Text";
            this.cboGradeYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGradeYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGradeYear.FormattingEnabled = true;
            this.cboGradeYear.ItemHeight = 15;
            this.cboGradeYear.Location = new System.Drawing.Point(76, 39);
            this.cboGradeYear.Name = "cboGradeYear";
            this.cboGradeYear.Size = new System.Drawing.Size(135, 21);
            this.cboGradeYear.TabIndex = 14;
            this.cboGradeYear.SelectedIndexChanged += new System.EventHandler(this.cboGradeYear_SelectedIndexChanged);
            // 
            // cboSemester
            // 
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 15;
            this.cboSemester.Location = new System.Drawing.Point(320, 9);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(131, 21);
            this.cboSemester.TabIndex = 13;
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.ItemHeight = 15;
            this.cboSchoolYear.Location = new System.Drawing.Point(76, 9);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(135, 21);
            this.cboSchoolYear.TabIndex = 12;
            // 
            // dtpBirthdate
            // 
            this.dtpBirthdate.Checked = false;
            this.dtpBirthdate.Location = new System.Drawing.Point(320, 95);
            this.dtpBirthdate.Name = "dtpBirthdate";
            this.dtpBirthdate.ShowCheckBox = true;
            this.dtpBirthdate.Size = new System.Drawing.Size(129, 22);
            this.dtpBirthdate.TabIndex = 11;
            this.dtpBirthdate.WatermarkText = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(234, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "最後學籍文號";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(258, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 9;
            this.label12.Text = "出生日期";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(258, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 8;
            this.label13.Text = "身分證號";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(258, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 7;
            this.label14.Text = "原  科  別";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(258, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 6;
            this.label15.Text = "學　　期";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "學　　號";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "性　　別";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "姓　　名";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "年　　級";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "學  年  度";
            // 
            // pnlIn
            // 
            this.pnlIn.Controls.Add(this.txtFromNumber);
            this.pnlIn.Controls.Add(this.label19);
            this.pnlIn.Controls.Add(this.txtFromDept);
            this.pnlIn.Controls.Add(this.label18);
            this.pnlIn.Controls.Add(this.txtFromStudentID);
            this.pnlIn.Controls.Add(this.label17);
            this.pnlIn.Controls.Add(this.txtFromSchool);
            this.pnlIn.Controls.Add(this.label16);
            this.pnlIn.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.pnlIn.Location = new System.Drawing.Point(12, 267);
            this.pnlIn.Name = "pnlIn";
            this.pnlIn.Size = new System.Drawing.Size(473, 70);
            this.pnlIn.TabIndex = 13;
            // 
            // txtFromNumber
            // 
            // 
            // 
            // 
            this.txtFromNumber.Border.Class = "TextBoxBorder";
            this.txtFromNumber.Location = new System.Drawing.Point(320, 35);
            this.txtFromNumber.Name = "txtFromNumber";
            this.txtFromNumber.Size = new System.Drawing.Size(131, 22);
            this.txtFromNumber.TabIndex = 26;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(246, 37);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 15);
            this.label19.TabIndex = 25;
            this.label19.Text = "轉入前文號";
            // 
            // txtFromDept
            // 
            // 
            // 
            // 
            this.txtFromDept.Border.Class = "TextBoxBorder";
            this.txtFromDept.Location = new System.Drawing.Point(320, 7);
            this.txtFromDept.Name = "txtFromDept";
            this.txtFromDept.Size = new System.Drawing.Size(131, 22);
            this.txtFromDept.TabIndex = 24;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(258, 10);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 15);
            this.label18.TabIndex = 23;
            this.label18.Text = "轉入科別";
            // 
            // txtFromStudentID
            // 
            // 
            // 
            // 
            this.txtFromStudentID.Border.Class = "TextBoxBorder";
            this.txtFromStudentID.Location = new System.Drawing.Point(80, 35);
            this.txtFromStudentID.Name = "txtFromStudentID";
            this.txtFromStudentID.Size = new System.Drawing.Size(131, 22);
            this.txtFromStudentID.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 38);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 15);
            this.label17.TabIndex = 21;
            this.label17.Text = "轉入學號";
            // 
            // txtFromSchool
            // 
            // 
            // 
            // 
            this.txtFromSchool.Border.Class = "TextBoxBorder";
            this.txtFromSchool.Location = new System.Drawing.Point(80, 7);
            this.txtFromSchool.Name = "txtFromSchool";
            this.txtFromSchool.Size = new System.Drawing.Size(131, 22);
            this.txtFromSchool.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 15);
            this.label16.TabIndex = 19;
            this.label16.Text = "轉入學校";
            // 
            // UpdateInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ClientSize = new System.Drawing.Size(497, 381);
            this.Controls.Add(this.pnlIn);
            this.Controls.Add(this.pnlSnapshoot);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dtpUpdateDate);
            this.Controls.Add(this.cboUpdateCode);
            this.Controls.Add(this.cboUpdateReason);
            this.Controls.Add(this.cboUpdateType);
            this.Controls.Add(this.txtUpdateDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateInfoForm";
            this.ShowIcon = false;
            this.Text = "管理異動學生資料";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateInfoForm_FormClosing);
            this.pnlSnapshoot.ResumeLayout(false);
            this.pnlSnapshoot.PerformLayout();
            this.pnlIn.ResumeLayout(false);
            this.pnlIn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUpdateDescription;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboUpdateType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboUpdateReason;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboUpdateCode;
        private System.Windows.Forms.DateTimePicker dtpUpdateDate;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.Panel pnlSnapshoot;
        private System.Windows.Forms.Panel pnlIn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGradeYear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private SmartSchool.StudentRelated.Palmerworm.DateTimePickerAdv dtpBirthdate;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLastNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStudentID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtIDNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDept;
        private System.Windows.Forms.RadioButton rbtGirl;
        private System.Windows.Forms.RadioButton rbtBoy;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFromSchool;
        private System.Windows.Forms.Label label16;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFromNumber;
        private System.Windows.Forms.Label label19;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFromDept;
        private System.Windows.Forms.Label label18;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFromStudentID;
        private System.Windows.Forms.Label label17;
    }
}