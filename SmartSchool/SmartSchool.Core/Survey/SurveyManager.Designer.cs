namespace SmartSchool.Survey
{
    partial class SurveyManager
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
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.dgSurveyList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.chSurveyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chExpireation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chPublished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnSetExpireation = new DevComponents.DotNetBar.ButtonItem();
            this.btnEditQuestion = new DevComponents.DotNetBar.ButtonItem();
            this.btnEditSurveyee = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelete = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgSurveyList)).BeginInit();
            this.SuspendLayout();
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.ItemHeight = 19;
            this.cboSchoolYear.Location = new System.Drawing.Point(62, 12);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(106, 25);
            this.cboSchoolYear.TabIndex = 2;
            this.cboSchoolYear.SelectedIndexChanged += new System.EventHandler(this.cboSchoolYear_SelectedIndexChanged);
            this.cboSchoolYear.TextChanged += new System.EventHandler(this.cboSchoolYear_TextChanged);
            // 
            // cboSemester
            // 
            this.cboSemester.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 19;
            this.cboSemester.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboSemester.Location = new System.Drawing.Point(222, 12);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(91, 25);
            this.cboSemester.TabIndex = 1;
            this.cboSemester.SelectedIndexChanged += new System.EventHandler(this.cboSemester_SelectedIndexChanged);
            this.cboSemester.TextChanged += new System.EventHandler(this.cboSemester_TextChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "2";
            // 
            // dgSurveyList
            // 
            this.dgSurveyList.AllowUserToAddRows = false;
            this.dgSurveyList.AllowUserToDeleteRows = false;
            this.dgSurveyList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgSurveyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSurveyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chSurveyName,
            this.chExpireation,
            this.chPublished});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSurveyList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgSurveyList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSurveyList.Location = new System.Drawing.Point(14, 45);
            this.dgSurveyList.Name = "dgSurveyList";
            this.dgSurveyList.ReadOnly = true;
            this.dgSurveyList.RowTemplate.Height = 24;
            this.dgSurveyList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSurveyList.Size = new System.Drawing.Size(607, 330);
            this.dgSurveyList.TabIndex = 3;
            // 
            // chSurveyName
            // 
            this.chSurveyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chSurveyName.HeaderText = "問卷名稱";
            this.chSurveyName.Name = "chSurveyName";
            this.chSurveyName.ReadOnly = true;
            // 
            // chExpireation
            // 
            this.chExpireation.HeaderText = "截止日";
            this.chExpireation.Name = "chExpireation";
            this.chExpireation.ReadOnly = true;
            // 
            // chPublished
            // 
            this.chPublished.HeaderText = "開放";
            this.chPublished.Name = "chPublished";
            this.chPublished.ReadOnly = true;
            this.chPublished.Width = 60;
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(382, 384);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "匯出結果";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Location = new System.Drawing.Point(463, 384);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(546, 384);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "關閉";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(14, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "學年度";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(187, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "學期";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "問卷名稱";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "過期日";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "已發佈";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.AutoExpandOnClick = true;
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(282, 384);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(94, 23);
            this.buttonX1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSetExpireation,
            this.btnEditQuestion,
            this.btnEditSurveyee,
            this.btnDelete});
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "其他動作";
            this.buttonX1.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSetExpireation
            // 
            this.btnSetExpireation.GlobalItem = false;
            this.btnSetExpireation.ImagePaddingHorizontal = 8;
            this.btnSetExpireation.Name = "btnSetExpireation";
            this.btnSetExpireation.Text = "設定截止日";
            this.btnSetExpireation.Click += new System.EventHandler(this.btnSetExpireation_Click);
            // 
            // btnEditQuestion
            // 
            this.btnEditQuestion.GlobalItem = false;
            this.btnEditQuestion.ImagePaddingHorizontal = 8;
            this.btnEditQuestion.Name = "btnEditQuestion";
            this.btnEditQuestion.Text = "編輯問卷內容";
            this.btnEditQuestion.Click += new System.EventHandler(this.btnEditQuestion_Click);
            // 
            // btnEditSurveyee
            // 
            this.btnEditSurveyee.Enabled = false;
            this.btnEditSurveyee.GlobalItem = false;
            this.btnEditSurveyee.ImagePaddingHorizontal = 8;
            this.btnEditSurveyee.Name = "btnEditSurveyee";
            this.btnEditSurveyee.Text = "編輯班級清單";
            this.btnEditSurveyee.Click += new System.EventHandler(this.btnEditSurveyee_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.GlobalItem = false;
            this.btnDelete.ImagePaddingHorizontal = 8;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "刪除問卷";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // SurveyManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(634, 414);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgSurveyList);
            this.Controls.Add(this.cboSchoolYear);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Name = "SurveyManager";
            this.Text = "問卷調查管理(Beta)";
            ((System.ComponentModel.ISupportInitialize)(this.dgSurveyList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgSurveyList;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn chSurveyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn chExpireation;
        private System.Windows.Forms.DataGridViewTextBoxColumn chPublished;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonItem btnDelete;
        private DevComponents.DotNetBar.ButtonItem btnEditQuestion;
        private DevComponents.DotNetBar.ButtonItem btnEditSurveyee;
        private DevComponents.DotNetBar.ButtonItem btnSetExpireation;
    }
}