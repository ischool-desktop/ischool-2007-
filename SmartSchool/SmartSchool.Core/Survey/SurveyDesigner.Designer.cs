namespace SmartSchool.Survey
{
    partial class SurveyDesigner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lblSurveyeeType = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtComment = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgQuestions = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.chQuestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chData = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.chValueList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lblSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lblSemester = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuestions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(14, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(39, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "對象";
            // 
            // lblSurveyeeType
            // 
            this.lblSurveyeeType.AutoSize = true;
            this.lblSurveyeeType.BackColor = System.Drawing.Color.Transparent;
            this.lblSurveyeeType.Location = new System.Drawing.Point(59, 12);
            this.lblSurveyeeType.Name = "lblSurveyeeType";
            this.lblSurveyeeType.Size = new System.Drawing.Size(53, 19);
            this.lblSurveyeeType.TabIndex = 1;
            this.lblSurveyeeType.Text = "<班級>";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(13, 40);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(39, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "名稱";
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Location = new System.Drawing.Point(58, 39);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(527, 25);
            this.txtName.TabIndex = 3;
            this.txtName.WatermarkText = "請輸入問卷的抬頭";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(511, 533);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "關閉";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(430, 533);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(14, 71);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(39, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "描述";
            // 
            // txtComment
            // 
            // 
            // 
            // 
            this.txtComment.Border.Class = "TextBoxBorder";
            this.txtComment.Location = new System.Drawing.Point(59, 70);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(527, 74);
            this.txtComment.TabIndex = 5;
            this.txtComment.WatermarkText = "請輸入問卷的描述";
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
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgQuestions.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgQuestions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgQuestions.Location = new System.Drawing.Point(14, 152);
            this.dgQuestions.Name = "dgQuestions";
            this.dgQuestions.RowTemplate.Height = 24;
            this.dgQuestions.Size = new System.Drawing.Size(571, 371);
            this.dgQuestions.TabIndex = 6;
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
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(162, 10);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(55, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "學年度";
            // 
            // lblSchoolYear
            // 
            this.lblSchoolYear.AutoSize = true;
            this.lblSchoolYear.BackColor = System.Drawing.Color.Transparent;
            this.lblSchoolYear.Location = new System.Drawing.Point(223, 12);
            this.lblSchoolYear.Name = "lblSchoolYear";
            this.lblSchoolYear.Size = new System.Drawing.Size(22, 19);
            this.lblSchoolYear.TabIndex = 1;
            this.lblSchoolYear.Text = "95";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            this.labelX6.Location = new System.Drawing.Point(318, 10);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(39, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "學期";
            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.BackColor = System.Drawing.Color.Transparent;
            this.lblSemester.Location = new System.Drawing.Point(363, 12);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(15, 19);
            this.lblSemester.TabIndex = 1;
            this.lblSemester.Text = "1";
            // 
            // SurveyDesigner
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(601, 564);
            this.Controls.Add(this.dgQuestions);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.lblSemester);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.lblSchoolYear);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lblSurveyeeType);
            this.Controls.Add(this.labelX1);
            this.Name = "SurveyDesigner";
            this.Text = "問卷內容";
            ((System.ComponentModel.ISupportInitialize)(this.dgQuestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblSurveyeeType;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtComment;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgQuestions;
        private System.Windows.Forms.DataGridViewTextBoxColumn chQuestion;
        private System.Windows.Forms.DataGridViewComboBoxColumn chData;
        private System.Windows.Forms.DataGridViewTextBoxColumn chValueList;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX lblSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX lblSemester;
    }
}