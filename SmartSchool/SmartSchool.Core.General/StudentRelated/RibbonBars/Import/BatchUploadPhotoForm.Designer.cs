namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    partial class BatchUploadPhotoForm
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
            this.rbIDNumber = new System.Windows.Forms.RadioButton();
            this.rbStudentNumber = new System.Windows.Forms.RadioButton();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnUpload = new DevComponents.DotNetBar.ButtonX();
            this.rbRookie = new System.Windows.Forms.RadioButton();
            this.lblFile = new DevComponents.DotNetBar.LabelX();
            this.lblDataSource = new DevComponents.DotNetBar.LabelX();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rbGraduate = new System.Windows.Forms.RadioButton();
            this.btnBrowser = new DevComponents.DotNetBar.ButtonX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupPanel2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // rbIDNumber
            // 
            this.rbIDNumber.AutoSize = true;
            this.rbIDNumber.BackColor = System.Drawing.Color.Transparent;
            this.rbIDNumber.Location = new System.Drawing.Point(111, 3);
            this.rbIDNumber.Name = "rbIDNumber";
            this.rbIDNumber.Size = new System.Drawing.Size(78, 21);
            this.rbIDNumber.TabIndex = 0;
            this.rbIDNumber.TabStop = true;
            this.rbIDNumber.Text = "身分證號";
            this.rbIDNumber.UseVisualStyleBackColor = false;
            // 
            // rbStudentNumber
            // 
            this.rbStudentNumber.AutoSize = true;
            this.rbStudentNumber.BackColor = System.Drawing.Color.Transparent;
            this.rbStudentNumber.Checked = true;
            this.rbStudentNumber.Location = new System.Drawing.Point(23, 3);
            this.rbStudentNumber.Name = "rbStudentNumber";
            this.rbStudentNumber.Size = new System.Drawing.Size(101, 21);
            this.rbStudentNumber.TabIndex = 0;
            this.rbStudentNumber.TabStop = true;
            this.rbStudentNumber.Text = "學       號";
            this.rbStudentNumber.UseVisualStyleBackColor = false;
            // 
            // progressBarX1
            // 
            this.progressBarX1.Location = new System.Drawing.Point(12, 188);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(210, 24);
            this.progressBarX1.TabIndex = 11;
            this.progressBarX1.TextVisible = true;
            this.progressBarX1.Visible = false;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.rbIDNumber);
            this.groupPanel2.Controls.Add(this.rbStudentNumber);
            this.groupPanel2.Location = new System.Drawing.Point(12, 111);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(371, 59);
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
            this.groupPanel2.TabIndex = 10;
            this.groupPanel2.Text = "命名方式";
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(309, 188);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(73, 24);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpload.Location = new System.Drawing.Point(230, 188);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(73, 24);
            this.btnUpload.TabIndex = 8;
            this.btnUpload.Text = "上傳";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // rbRookie
            // 
            this.rbRookie.AutoSize = true;
            this.rbRookie.BackColor = System.Drawing.Color.Transparent;
            this.rbRookie.Checked = true;
            this.rbRookie.Location = new System.Drawing.Point(23, 3);
            this.rbRookie.Name = "rbRookie";
            this.rbRookie.Size = new System.Drawing.Size(78, 21);
            this.rbRookie.TabIndex = 0;
            this.rbRookie.TabStop = true;
            this.rbRookie.Text = "入學照片";
            this.rbRookie.UseVisualStyleBackColor = false;
            // 
            // lblFile
            // 
            this.lblFile.BackColor = System.Drawing.Color.White;
            this.lblFile.Location = new System.Drawing.Point(79, 11);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(241, 24);
            this.lblFile.TabIndex = 5;
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(12, 14);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(60, 19);
            this.lblDataSource.TabIndex = 4;
            this.lblDataSource.Text = "資料來源";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.rbGraduate);
            this.groupPanel1.Controls.Add(this.rbRookie);
            this.groupPanel1.Location = new System.Drawing.Point(12, 43);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(371, 59);
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
            this.groupPanel1.TabIndex = 9;
            this.groupPanel1.Text = "照片項目";
            // 
            // rbGraduate
            // 
            this.rbGraduate.AutoSize = true;
            this.rbGraduate.BackColor = System.Drawing.Color.Transparent;
            this.rbGraduate.Location = new System.Drawing.Point(111, 3);
            this.rbGraduate.Name = "rbGraduate";
            this.rbGraduate.Size = new System.Drawing.Size(78, 21);
            this.rbGraduate.TabIndex = 0;
            this.rbGraduate.TabStop = true;
            this.rbGraduate.Text = "畢業照片";
            this.rbGraduate.UseVisualStyleBackColor = false;
            // 
            // btnBrowser
            // 
            this.btnBrowser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBrowser.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBrowser.Location = new System.Drawing.Point(329, 11);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(53, 24);
            this.btnBrowser.TabIndex = 6;
            this.btnBrowser.Text = "瀏覽";
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx3.Controls.Add(this.lblMsg);
            this.panelEx3.Controls.Add(this.btnBrowser);
            this.panelEx3.Controls.Add(this.groupPanel1);
            this.panelEx3.Controls.Add(this.lblDataSource);
            this.panelEx3.Controls.Add(this.progressBarX1);
            this.panelEx3.Controls.Add(this.lblFile);
            this.panelEx3.Controls.Add(this.groupPanel2);
            this.panelEx3.Controls.Add(this.btnUpload);
            this.panelEx3.Controls.Add(this.btnExit);
            this.panelEx3.Controls.Add(this.pictureBox1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(395, 224);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.BorderWidth = 0;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 13;
            // 
            // lblMsg
            // 
            this.lblMsg.Location = new System.Drawing.Point(105, 229);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(277, 106);
            this.lblMsg.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 229);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 106);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BatchUploadPhotoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 224);
            this.Controls.Add(this.panelEx3);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BatchUploadPhotoForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "匯入批次照片";
            this.Load += new System.EventHandler(this.BatchUploadPhotoForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatchUploadPhotoForm_FormClosing);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbIDNumber;
        private System.Windows.Forms.RadioButton rbStudentNumber;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnUpload;
        private System.Windows.Forms.RadioButton rbRookie;
        private DevComponents.DotNetBar.LabelX lblFile;
        private DevComponents.DotNetBar.LabelX lblDataSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.RadioButton rbGraduate;
        private DevComponents.DotNetBar.ButtonX btnBrowser;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.LabelX lblMsg;
    }
}