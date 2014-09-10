namespace SmartSchool.Survey
{
    partial class SurveyWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SurveyWizard));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizard1 = new DevComponents.DotNetBar.Wizard();
            this.wizardPage1 = new DevComponents.DotNetBar.WizardPage();
            this.txtComment = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtExpireation = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.lblSemester = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.lblSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.wizardPage2 = new DevComponents.DotNetBar.WizardPage();
            this.chkClassStudent = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dgQuestions = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.chQuestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chData = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.chValueList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wizard1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuestions)).BeginInit();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.BackButtonText = "< 上一頁";
            this.wizard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(229)))), ((int)(((byte)(253)))));
            this.wizard1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("wizard1.BackgroundImage")));
            this.wizard1.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007;
            this.wizard1.CancelButtonText = "取消";
            this.wizard1.Cursor = System.Windows.Forms.Cursors.Default;
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.FinishButtonTabIndex = 3;
            this.wizard1.FinishButtonText = "完成";
            // 
            // 
            // 
            this.wizard1.FooterStyle.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
            this.wizard1.HeaderCaptionFont = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 12F, System.Drawing.FontStyle.Bold);
            this.wizard1.HeaderDescriptionVisible = false;
            this.wizard1.HeaderImageSize = new System.Drawing.Size(48, 48);
            // 
            // 
            // 
            this.wizard1.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard1.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.wizard1.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(157)))), ((int)(((byte)(182)))));
            this.wizard1.HeaderStyle.BorderBottomWidth = 1;
            this.wizard1.HelpButtonVisible = false;
            this.wizard1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.NextButtonText = "下一頁 >";
            this.wizard1.Size = new System.Drawing.Size(562, 434);
            this.wizard1.TabIndex = 0;
            this.wizard1.WizardPages.AddRange(new DevComponents.DotNetBar.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2});
            // 
            // wizardPage1
            // 
            this.wizardPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage1.AntiAlias = false;
            this.wizardPage1.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage1.Controls.Add(this.txtComment);
            this.wizardPage1.Controls.Add(this.txtExpireation);
            this.wizardPage1.Controls.Add(this.txtName);
            this.wizardPage1.Controls.Add(this.labelX3);
            this.wizardPage1.Controls.Add(this.labelX4);
            this.wizardPage1.Controls.Add(this.labelX2);
            this.wizardPage1.Controls.Add(this.lblSemester);
            this.wizardPage1.Controls.Add(this.labelX7);
            this.wizardPage1.Controls.Add(this.lblSchoolYear);
            this.wizardPage1.Controls.Add(this.labelX5);
            this.wizardPage1.Controls.Add(this.labelX1);
            this.wizardPage1.Location = new System.Drawing.Point(7, 72);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.PageTitle = "問卷基本設定";
            this.wizardPage1.Size = new System.Drawing.Size(548, 304);
            this.wizardPage1.TabIndex = 0;
            this.wizardPage1.NextButtonClick += new System.ComponentModel.CancelEventHandler(this.wizardPage1_NextButtonClick);
            // 
            // txtComment
            // 
            // 
            // 
            // 
            this.txtComment.Border.Class = "TextBoxBorder";
            this.txtComment.Location = new System.Drawing.Point(49, 204);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(421, 86);
            this.txtComment.TabIndex = 10;
            // 
            // txtExpireation
            // 
            // 
            // 
            // 
            this.txtExpireation.Border.Class = "TextBoxBorder";
            this.txtExpireation.Location = new System.Drawing.Point(49, 144);
            this.txtExpireation.Name = "txtExpireation";
            this.txtExpireation.Size = new System.Drawing.Size(190, 25);
            this.txtExpireation.TabIndex = 7;
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Location = new System.Drawing.Point(49, 84);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(421, 25);
            this.txtName.TabIndex = 5;
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(35, 175);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 9;
            this.labelX3.Text = "問卷描述";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.Location = new System.Drawing.Point(245, 147);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(256, 19);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "(如果未指定截止日期，代表未開放填寫。)";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(35, 115);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 6;
            this.labelX2.Text = "截止日期";
            // 
            // lblSemester
            // 
            this.lblSemester.Location = new System.Drawing.Point(176, 20);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(49, 23);
            this.lblSemester.TabIndex = 3;
            this.lblSemester.Text = "0";
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(142, 20);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(43, 23);
            this.labelX7.TabIndex = 2;
            this.labelX7.Text = "學期";
            // 
            // lblSchoolYear
            // 
            this.lblSchoolYear.Location = new System.Drawing.Point(84, 20);
            this.lblSchoolYear.Name = "lblSchoolYear";
            this.lblSchoolYear.Size = new System.Drawing.Size(52, 23);
            this.lblSchoolYear.TabIndex = 1;
            this.lblSchoolYear.Text = "0";
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(37, 20);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(52, 23);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "學年度";
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(35, 55);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "問卷名稱";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage2.AntiAlias = false;
            this.wizardPage2.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage2.Controls.Add(this.chkClassStudent);
            this.wizardPage2.Controls.Add(this.dgQuestions);
            this.wizardPage2.Location = new System.Drawing.Point(7, 72);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.PageTitle = "編輯問卷內容";
            this.wizardPage2.Size = new System.Drawing.Size(548, 304);
            this.wizardPage2.TabIndex = 8;
            this.wizardPage2.FinishButtonClick += new System.ComponentModel.CancelEventHandler(this.wizardPage2_FinishButtonClick);
            // 
            // chkClassStudent
            // 
            this.chkClassStudent.Location = new System.Drawing.Point(0, 281);
            this.chkClassStudent.Name = "chkClassStudent";
            this.chkClassStudent.Size = new System.Drawing.Size(169, 23);
            this.chkClassStudent.TabIndex = 0;
            this.chkClassStudent.Text = "問題針對每位學生。";
            // 
            // dgQuestions
            // 
            this.dgQuestions.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgQuestions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chQuestion,
            this.chData,
            this.chValueList});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgQuestions.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgQuestions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgQuestions.Location = new System.Drawing.Point(1, 0);
            this.dgQuestions.Name = "dgQuestions";
            this.dgQuestions.RowTemplate.Height = 24;
            this.dgQuestions.Size = new System.Drawing.Size(545, 279);
            this.dgQuestions.TabIndex = 1;
            this.dgQuestions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgQuestions_CellClick);
            // 
            // chQuestion
            // 
            this.chQuestion.HeaderText = "問題";
            this.chQuestion.Name = "chQuestion";
            // 
            // chData
            // 
            this.chData.HeaderText = "資料類型";
            this.chData.Items.AddRange(new object[] {
            "文字",
            "段落文字",
            "數字",
            "日期",
            "多選",
            "單選"});
            this.chData.Name = "chData";
            this.chData.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // chValueList
            // 
            this.chValueList.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chValueList.HeaderText = "值清單";
            this.chValueList.Name = "chValueList";
            // 
            // SurveyWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 434);
            this.Controls.Add(this.wizard1);
            this.Name = "SurveyWizard";
            this.Text = "問卷建立精靈";
            this.wizard1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgQuestions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Wizard wizard1;
        private DevComponents.DotNetBar.WizardPage wizardPage1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtComment;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExpireation;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.WizardPage wizardPage2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgQuestions;
        private System.Windows.Forms.DataGridViewTextBoxColumn chQuestion;
        private System.Windows.Forms.DataGridViewComboBoxColumn chData;
        private System.Windows.Forms.DataGridViewTextBoxColumn chValueList;
        private DevComponents.DotNetBar.LabelX lblSemester;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX lblSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkClassStudent;
    }
}