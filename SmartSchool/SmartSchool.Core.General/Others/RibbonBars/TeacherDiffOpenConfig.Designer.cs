namespace SmartSchool.Others.RibbonBars
{
    partial class TeacherDiffOpenConfig
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
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtTerm1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnSetup = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lblPreviousSetup = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txtTerm2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.lblSemester = new DevComponents.DotNetBar.LabelX();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(12, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 19);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "學年度";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(164, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 19);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "學期";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(3, 3);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(284, 77);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "您不需設定開始時間，儲存設定後即開始開放班導師輸入。\r\n\r\n請輸入您預計截止時間：";
            this.labelX3.WordWrap = true;
            // 
            // txtTerm1
            // 
            // 
            // 
            // 
            this.txtTerm1.Border.Class = "TextBoxBorder";
            this.txtTerm1.Location = new System.Drawing.Point(69, 83);
            this.txtTerm1.Name = "txtTerm1";
            this.txtTerm1.Size = new System.Drawing.Size(200, 25);
            this.txtTerm1.TabIndex = 2;
            this.txtTerm1.Validating += new System.ComponentModel.CancelEventHandler(this.txtTerm1_Validating);
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.Location = new System.Drawing.Point(3, 86);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(60, 19);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "截止時間";
            // 
            // btnSetup
            // 
            this.btnSetup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetup.BackColor = System.Drawing.Color.Transparent;
            this.btnSetup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetup.Location = new System.Drawing.Point(142, 366);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(75, 23);
            this.btnSetup.TabIndex = 7;
            this.btnSetup.Text = "設定";
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(223, 366);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "離開";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.labelX5);
            this.panel1.Controls.Add(this.labelX3);
            this.panel1.Controls.Add(this.labelX4);
            this.panel1.Controls.Add(this.txtTerm1);
            this.panel1.Location = new System.Drawing.Point(12, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 139);
            this.panel1.TabIndex = 5;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.Location = new System.Drawing.Point(71, 110);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(203, 19);
            this.labelX5.TabIndex = 3;
            this.labelX5.Text = "(如無輸入截止時間，則無法輸入)";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.labelX6);
            this.panel2.Controls.Add(this.lblPreviousSetup);
            this.panel2.Controls.Add(this.labelX7);
            this.panel2.Controls.Add(this.txtTerm2);
            this.panel2.Location = new System.Drawing.Point(12, 187);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(290, 170);
            this.panel2.TabIndex = 6;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.Location = new System.Drawing.Point(71, 144);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(203, 19);
            this.labelX6.TabIndex = 4;
            this.labelX6.Text = "(如無輸入截止時間，則無法輸入)";
            // 
            // lblPreviousSetup
            // 
            this.lblPreviousSetup.Location = new System.Drawing.Point(3, 3);
            this.lblPreviousSetup.Name = "lblPreviousSetup";
            this.lblPreviousSetup.Size = new System.Drawing.Size(287, 107);
            this.lblPreviousSetup.TabIndex = 0;
            this.lblPreviousSetup.Text = "您前次設定資訊如下：\r\n開始時間：<%StartTime%>\r\n截止時間：<%EndTime%>\r\n\r\n若您要修改或重新設定開放期間，請輸入截止時間後，重新設定即" +
                "可。";
            this.lblPreviousSetup.WordWrap = true;
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            this.labelX7.Location = new System.Drawing.Point(3, 119);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(60, 19);
            this.labelX7.TabIndex = 2;
            this.labelX7.Text = "截止時間";
            // 
            // txtTerm2
            // 
            // 
            // 
            // 
            this.txtTerm2.Border.Class = "TextBoxBorder";
            this.txtTerm2.Location = new System.Drawing.Point(69, 116);
            this.txtTerm2.Name = "txtTerm2";
            this.txtTerm2.Size = new System.Drawing.Size(200, 25);
            this.txtTerm2.TabIndex = 3;
            this.txtTerm2.Validating += new System.ComponentModel.CancelEventHandler(this.txtTerm2_Validating);
            // 
            // lblSchoolYear
            // 
            this.lblSchoolYear.BackColor = System.Drawing.Color.Transparent;
            this.lblSchoolYear.Location = new System.Drawing.Point(61, 9);
            this.lblSchoolYear.Name = "lblSchoolYear";
            this.lblSchoolYear.Size = new System.Drawing.Size(75, 23);
            this.lblSchoolYear.TabIndex = 8;
            this.lblSchoolYear.Text = "00";
            // 
            // lblSemester
            // 
            this.lblSemester.BackColor = System.Drawing.Color.Transparent;
            this.lblSemester.Location = new System.Drawing.Point(199, 9);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(75, 23);
            this.lblSemester.TabIndex = 8;
            this.lblSemester.Text = "00";
            // 
            // TeacherDiffOpenConfig
            // 
            this.AcceptButton = this.btnSetup;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(314, 401);
            this.Controls.Add(this.lblSemester);
            this.Controls.Add(this.lblSchoolYear);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TeacherDiffOpenConfig";
            this.Text = "導師期末輸入設定";
            this.Load += new System.EventHandler(this.TeacherDiffOpenConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTerm1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnSetup;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.LabelX lblPreviousSetup;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTerm2;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX lblSchoolYear;
        private DevComponents.DotNetBar.LabelX lblSemester;
    }
}