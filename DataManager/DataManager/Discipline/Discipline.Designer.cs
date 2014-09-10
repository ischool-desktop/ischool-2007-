namespace DataManager
{
    partial class Discipline
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.txtStartDate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtEndDate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.picWaiting = new System.Windows.Forms.PictureBox();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOccurDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeatNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudentNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSchoolYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSemester = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisciplineCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMeritFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAwardA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAwardB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAwardC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaultA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaultB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaultC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsCleared = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClearDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClearReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colStudentID,
            this.colOccurDate,
            this.colClassName,
            this.colSeatNo,
            this.colStudentNumber,
            this.colStudentName,
            this.colGender,
            this.colSchoolYear,
            this.colSemester,
            this.colPlace,
            this.colDisciplineCount,
            this.colMeritFlag,
            this.colAwardA,
            this.colAwardB,
            this.colAwardC,
            this.colFaultA,
            this.colFaultB,
            this.colFaultC,
            this.colReason,
            this.colIsCleared,
            this.colClearDate,
            this.colClearReason,
            this.colUA});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(6, 41);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowHeadersWidth = 25;
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(780, 490);
            this.dataGridViewX1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(711, 536);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "關閉";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtStartDate
            // 
            // 
            // 
            // 
            this.txtStartDate.Border.Class = "TextBoxBorder";
            this.txtStartDate.Location = new System.Drawing.Point(71, 9);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(100, 25);
            this.txtStartDate.TabIndex = 2;
            this.txtStartDate.WatermarkText = "西元年/月/日";
            // 
            // txtEndDate
            // 
            // 
            // 
            // 
            this.txtEndDate.Border.Class = "TextBoxBorder";
            this.txtEndDate.Location = new System.Drawing.Point(247, 9);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(100, 25);
            this.txtEndDate.TabIndex = 2;
            this.txtEndDate.WatermarkText = "西元年/月/日";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(6, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 19);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "開始日期";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(182, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 19);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "結束日期";
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(362, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "重新整理";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.BackColor = System.Drawing.Color.Transparent;
            this.btnModify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnModify.Location = new System.Drawing.Point(630, 536);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 1;
            this.btnModify.Text = "修改獎懲";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // picWaiting
            // 
            this.picWaiting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picWaiting.BackColor = System.Drawing.Color.Transparent;
            this.picWaiting.Image = global::DataManager.Properties.Resources.loading5;
            this.picWaiting.Location = new System.Drawing.Point(360, 258);
            this.picWaiting.Name = "picWaiting";
            this.picWaiting.Size = new System.Drawing.Size(32, 32);
            this.picWaiting.TabIndex = 5;
            this.picWaiting.TabStop = false;
            this.picWaiting.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(447, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "匯出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "匯出獎懲管理";
            this.saveFileDialog1.Filter = "Excel (*.xls)|*.xls";
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            this.colID.Width = 80;
            // 
            // colStudentID
            // 
            this.colStudentID.HeaderText = "StudentID";
            this.colStudentID.Name = "colStudentID";
            this.colStudentID.ReadOnly = true;
            this.colStudentID.Visible = false;
            // 
            // colOccurDate
            // 
            this.colOccurDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colOccurDate.HeaderText = "日期";
            this.colOccurDate.Name = "colOccurDate";
            this.colOccurDate.ReadOnly = true;
            this.colOccurDate.Width = 59;
            // 
            // colClassName
            // 
            this.colClassName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colClassName.HeaderText = "班級";
            this.colClassName.Name = "colClassName";
            this.colClassName.ReadOnly = true;
            this.colClassName.Width = 59;
            // 
            // colSeatNo
            // 
            this.colSeatNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSeatNo.HeaderText = "座號";
            this.colSeatNo.Name = "colSeatNo";
            this.colSeatNo.Width = 59;
            // 
            // colStudentNumber
            // 
            this.colStudentNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colStudentNumber.HeaderText = "學號";
            this.colStudentNumber.Name = "colStudentNumber";
            this.colStudentNumber.ReadOnly = true;
            this.colStudentNumber.Width = 59;
            // 
            // colStudentName
            // 
            this.colStudentName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colStudentName.HeaderText = "姓名";
            this.colStudentName.Name = "colStudentName";
            this.colStudentName.ReadOnly = true;
            this.colStudentName.Width = 59;
            // 
            // colGender
            // 
            this.colGender.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colGender.HeaderText = "性別";
            this.colGender.Name = "colGender";
            this.colGender.ReadOnly = true;
            this.colGender.Width = 59;
            // 
            // colSchoolYear
            // 
            this.colSchoolYear.HeaderText = "學年度";
            this.colSchoolYear.Name = "colSchoolYear";
            this.colSchoolYear.ReadOnly = true;
            this.colSchoolYear.Visible = false;
            this.colSchoolYear.Width = 75;
            // 
            // colSemester
            // 
            this.colSemester.HeaderText = "學期";
            this.colSemester.Name = "colSemester";
            this.colSemester.ReadOnly = true;
            this.colSemester.Visible = false;
            this.colSemester.Width = 75;
            // 
            // colPlace
            // 
            this.colPlace.HeaderText = "地點";
            this.colPlace.Name = "colPlace";
            this.colPlace.ReadOnly = true;
            this.colPlace.Visible = false;
            // 
            // colDisciplineCount
            // 
            this.colDisciplineCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDisciplineCount.HeaderText = "獎懲次數";
            this.colDisciplineCount.Name = "colDisciplineCount";
            this.colDisciplineCount.Width = 85;
            // 
            // colMeritFlag
            // 
            this.colMeritFlag.HeaderText = "Flag";
            this.colMeritFlag.Name = "colMeritFlag";
            this.colMeritFlag.ReadOnly = true;
            this.colMeritFlag.Visible = false;
            // 
            // colAwardA
            // 
            this.colAwardA.HeaderText = "大功";
            this.colAwardA.Name = "colAwardA";
            this.colAwardA.ReadOnly = true;
            this.colAwardA.Visible = false;
            this.colAwardA.Width = 60;
            // 
            // colAwardB
            // 
            this.colAwardB.HeaderText = "小功";
            this.colAwardB.Name = "colAwardB";
            this.colAwardB.ReadOnly = true;
            this.colAwardB.Visible = false;
            this.colAwardB.Width = 60;
            // 
            // colAwardC
            // 
            this.colAwardC.HeaderText = "嘉獎";
            this.colAwardC.Name = "colAwardC";
            this.colAwardC.ReadOnly = true;
            this.colAwardC.Visible = false;
            this.colAwardC.Width = 60;
            // 
            // colFaultA
            // 
            this.colFaultA.HeaderText = "大過";
            this.colFaultA.Name = "colFaultA";
            this.colFaultA.ReadOnly = true;
            this.colFaultA.Visible = false;
            this.colFaultA.Width = 60;
            // 
            // colFaultB
            // 
            this.colFaultB.HeaderText = "小過";
            this.colFaultB.Name = "colFaultB";
            this.colFaultB.ReadOnly = true;
            this.colFaultB.Visible = false;
            this.colFaultB.Width = 60;
            // 
            // colFaultC
            // 
            this.colFaultC.HeaderText = "警告";
            this.colFaultC.Name = "colFaultC";
            this.colFaultC.ReadOnly = true;
            this.colFaultC.Visible = false;
            this.colFaultC.Width = 60;
            // 
            // colReason
            // 
            this.colReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colReason.HeaderText = "事由";
            this.colReason.Name = "colReason";
            this.colReason.ReadOnly = true;
            // 
            // colIsCleared
            // 
            this.colIsCleared.HeaderText = "是否銷過";
            this.colIsCleared.Name = "colIsCleared";
            this.colIsCleared.ReadOnly = true;
            this.colIsCleared.Visible = false;
            this.colIsCleared.Width = 90;
            // 
            // colClearDate
            // 
            this.colClearDate.HeaderText = "銷過日期";
            this.colClearDate.Name = "colClearDate";
            this.colClearDate.ReadOnly = true;
            this.colClearDate.Visible = false;
            // 
            // colClearReason
            // 
            this.colClearReason.HeaderText = "銷過事由";
            this.colClearReason.Name = "colClearReason";
            this.colClearReason.ReadOnly = true;
            this.colClearReason.Visible = false;
            // 
            // colUA
            // 
            this.colUA.HeaderText = "留校察看";
            this.colUA.Name = "colUA";
            this.colUA.ReadOnly = true;
            this.colUA.Visible = false;
            this.colUA.Width = 90;
            // 
            // Discipline
            // 
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.picWaiting);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtEndDate);
            this.Controls.Add(this.txtStartDate);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = true;
            this.Name = "Discipline";
            this.Text = "獎懲管理";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStartDate;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEndDate;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private System.Windows.Forms.PictureBox picWaiting;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOccurDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeatNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSchoolYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSemester;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisciplineCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMeritFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAwardA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAwardB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAwardC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaultA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaultB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaultC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsCleared;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClearDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClearReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUA;
    }
}

