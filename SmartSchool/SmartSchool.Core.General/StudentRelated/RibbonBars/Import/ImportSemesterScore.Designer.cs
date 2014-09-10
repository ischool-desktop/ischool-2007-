namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    partial class ImportSemesterScore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSemesterScore));
            this.wizard1 = new DevComponents.DotNetBar.Wizard();
            this.wizardPage1 = new DevComponents.DotNetBar.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSourceFile = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSelectFile = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.wizardPage2 = new DevComponents.DotNetBar.WizardPage();
            this.wizardPage3 = new DevComponents.DotNetBar.WizardPage();
            this.wizardPage4 = new DevComponents.DotNetBar.WizardPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.wizard1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.BackButtonText = "上一步";
            this.wizard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(229)))), ((int)(((byte)(253)))));
            this.wizard1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("wizard1.BackgroundImage")));
            this.wizard1.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007;
            this.wizard1.CancelButtonText = "關閉";
            this.wizard1.Cursor = System.Windows.Forms.Cursors.Default;
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.FinishButtonTabIndex = 3;
            this.wizard1.FinishButtonText = "完成";
            // 
            // 
            // 
            this.wizard1.FooterStyle.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
            this.wizard1.HeaderImageSize = new System.Drawing.Size(48, 48);
            // 
            // 
            // 
            this.wizard1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.wizard1.HeaderStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.wizard1.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard1.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.wizard1.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(157)))), ((int)(((byte)(182)))));
            this.wizard1.HeaderStyle.BorderBottomWidth = 1;
            this.wizard1.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard1.HeaderStyle.BorderLeftWidth = 1;
            this.wizard1.HeaderStyle.BorderRightWidth = 1;
            this.wizard1.HeaderStyle.BorderTopWidth = 1;
            this.wizard1.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard1.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard1.HelpButtonVisible = false;
            this.wizard1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.NextButtonText = "下一步";
            this.wizard1.Size = new System.Drawing.Size(581, 430);
            this.wizard1.TabIndex = 0;
            this.wizard1.WizardPages.AddRange(new DevComponents.DotNetBar.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3,
            this.wizardPage4});
            // 
            // wizardPage1
            // 
            this.wizardPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage1.AntiAlias = false;
            this.wizardPage1.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage1.Controls.Add(this.label4);
            this.wizardPage1.Controls.Add(this.txtSourceFile);
            this.wizardPage1.Controls.Add(this.btnSelectFile);
            this.wizardPage1.Controls.Add(this.labelX1);
            this.wizardPage1.Location = new System.Drawing.Point(7, 72);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.NextButtonEnabled = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wizardPage1.PageDescription = "載入匯入檔案";
            this.wizardPage1.PageTitle = "匯入學期科目成績";
            this.wizardPage1.Size = new System.Drawing.Size(567, 300);
            this.wizardPage1.TabIndex = 7;
            this.wizardPage1.NextButtonClick += new System.ComponentModel.CancelEventHandler(this.wizardPage1_NextButtonClick);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(126, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 38);
            this.label4.TabIndex = 37;
            this.label4.Text = "程式會讀取「學生成績資料」的工作表資料，如果找不到此名稱工作表，會改讀取第一張工作表。";
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSourceFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtSourceFile.Border.Class = "TextBoxBorder";
            this.txtSourceFile.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSourceFile.Location = new System.Drawing.Point(129, 65);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(345, 22);
            this.txtSourceFile.TabIndex = 36;
            this.txtSourceFile.WatermarkText = "請選擇或輸入檔案位置";
            this.txtSourceFile.TextChanged += new System.EventHandler(this.txtSourceFile_TextChanged);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFile.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectFile.Location = new System.Drawing.Point(480, 65);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(29, 22);
            this.btnSelectFile.TabIndex = 38;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(59, 67);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(74, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "開啟檔案：";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage2.AntiAlias = false;
            this.wizardPage2.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage2.Location = new System.Drawing.Point(7, 72);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.PageDescription = "< Wizard step description >";
            this.wizardPage2.PageTitle = "< Wizard step title >";
            this.wizardPage2.Size = new System.Drawing.Size(567, 300);
            this.wizardPage2.TabIndex = 8;
            // 
            // wizardPage3
            // 
            this.wizardPage3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage3.AntiAlias = false;
            this.wizardPage3.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage3.Location = new System.Drawing.Point(7, 72);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.PageDescription = "< Wizard step description >";
            this.wizardPage3.PageTitle = "< Wizard step title >";
            this.wizardPage3.Size = new System.Drawing.Size(567, 300);
            this.wizardPage3.TabIndex = 9;
            // 
            // wizardPage4
            // 
            this.wizardPage4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage4.AntiAlias = false;
            this.wizardPage4.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage4.Location = new System.Drawing.Point(7, 72);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.PageDescription = "< Wizard step description >";
            this.wizardPage4.PageTitle = "< Wizard step title >";
            this.wizardPage4.Size = new System.Drawing.Size(567, 300);
            this.wizardPage4.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            this.openFileDialog1.Title = "匯入成績";
            // 
            // ImportSemesterScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 430);
            this.Controls.Add(this.wizard1);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportSemesterScore";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.wizard1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Wizard wizard1;
        private DevComponents.DotNetBar.WizardPage wizardPage1;
        private DevComponents.DotNetBar.WizardPage wizardPage2;
        private DevComponents.DotNetBar.WizardPage wizardPage3;
        private DevComponents.DotNetBar.WizardPage wizardPage4;
        private System.Windows.Forms.Label label4;
        public DevComponents.DotNetBar.Controls.TextBoxX txtSourceFile;
        public DevComponents.DotNetBar.ButtonX btnSelectFile;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}