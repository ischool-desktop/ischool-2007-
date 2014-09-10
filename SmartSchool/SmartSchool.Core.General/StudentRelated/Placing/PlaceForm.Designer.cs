namespace SmartSchool.StudentRelated.Placing
{
    partial class PlaceForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSort = new DevComponents.DotNetBar.ButtonX();
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.lblSemester = new DevComponents.DotNetBar.LabelX();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.gpSortType = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rbUndupicate = new System.Windows.Forms.RadioButton();
            this.rbDupicate = new System.Windows.Forms.RadioButton();
            this.gpSource = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkRepeat = new System.Windows.Forms.CheckBox();
            this.chkResit = new System.Windows.Forms.CheckBox();
            this.chkOrigin = new System.Windows.Forms.CheckBox();
            this.gpScoreType = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkAvg2 = new System.Windows.Forms.CheckBox();
            this.chkAvg1 = new System.Windows.Forms.CheckBox();
            this.chkTotal = new System.Windows.Forms.CheckBox();
            this.chkSubject = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMessage = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpSortType.SuspendLayout();
            this.gpSource.SuspendLayout();
            this.gpScoreType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSort
            // 
            this.btnSort.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSort.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSort.Location = new System.Drawing.Point(469, 401);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 0;
            this.btnSort.Text = "排名";
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.Location = new System.Drawing.Point(89, 44);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(96, 24);
            this.cboSchoolYear.TabIndex = 3;
            this.cboSchoolYear.TextChanged += new System.EventHandler(this.cboSchoolYear_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(13, 44);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(70, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "學年度";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblSemester
            // 
            this.lblSemester.Location = new System.Drawing.Point(206, 45);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(37, 23);
            this.lblSemester.TabIndex = 4;
            this.lblSemester.Text = "學期";
            // 
            // cboSemester
            // 
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.Location = new System.Drawing.Point(249, 44);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(54, 24);
            this.cboSemester.TabIndex = 3;
            this.cboSemester.TextChanged += new System.EventHandler(this.cboSemester_TextChanged);
            // 
            // cboType
            // 
            this.cboType.DisplayMember = "Text";
            this.cboType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(89, 12);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(214, 24);
            this.cboType.TabIndex = 3;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(13, 12);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(70, 23);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "排名依據";
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(550, 401);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // gpSortType
            // 
            this.gpSortType.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpSortType.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpSortType.Controls.Add(this.rbUndupicate);
            this.gpSortType.Controls.Add(this.rbDupicate);
            this.gpSortType.Location = new System.Drawing.Point(320, 12);
            this.gpSortType.Name = "gpSortType";
            this.gpSortType.Size = new System.Drawing.Size(306, 97);
            // 
            // 
            // 
            this.gpSortType.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpSortType.Style.BackColorGradientAngle = 90;
            this.gpSortType.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpSortType.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSortType.Style.BorderBottomWidth = 1;
            this.gpSortType.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpSortType.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSortType.Style.BorderLeftWidth = 1;
            this.gpSortType.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSortType.Style.BorderRightWidth = 1;
            this.gpSortType.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSortType.Style.BorderTopWidth = 1;
            this.gpSortType.Style.CornerDiameter = 4;
            this.gpSortType.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpSortType.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpSortType.TabIndex = 5;
            this.gpSortType.Tag = "";
            this.gpSortType.Text = "排名選項";
            // 
            // rbUndupicate
            // 
            this.rbUndupicate.AutoSize = true;
            this.rbUndupicate.BackColor = System.Drawing.Color.Transparent;
            this.rbUndupicate.Location = new System.Drawing.Point(14, 37);
            this.rbUndupicate.Name = "rbUndupicate";
            this.rbUndupicate.Size = new System.Drawing.Size(166, 21);
            this.rbUndupicate.TabIndex = 0;
            this.rbUndupicate.TabStop = true;
            this.rbUndupicate.Text = "接序排名(例:1.2.3.3.4)";
            this.rbUndupicate.UseVisualStyleBackColor = false;
            this.rbUndupicate.CheckedChanged += new System.EventHandler(this.rbUndupicate_CheckedChanged);
            // 
            // rbDupicate
            // 
            this.rbDupicate.AutoSize = true;
            this.rbDupicate.BackColor = System.Drawing.Color.Transparent;
            this.rbDupicate.Location = new System.Drawing.Point(14, 10);
            this.rbDupicate.Name = "rbDupicate";
            this.rbDupicate.Size = new System.Drawing.Size(180, 21);
            this.rbDupicate.TabIndex = 0;
            this.rbDupicate.TabStop = true;
            this.rbDupicate.Text = "不接序排名(例:1.2.3.3.5)";
            this.rbDupicate.UseVisualStyleBackColor = false;
            this.rbDupicate.CheckedChanged += new System.EventHandler(this.rbDupicate_CheckedChanged);
            // 
            // gpSource
            // 
            this.gpSource.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpSource.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpSource.Controls.Add(this.chkRepeat);
            this.gpSource.Controls.Add(this.chkResit);
            this.gpSource.Controls.Add(this.chkOrigin);
            this.gpSource.Location = new System.Drawing.Point(320, 115);
            this.gpSource.Name = "gpSource";
            this.gpSource.Size = new System.Drawing.Size(306, 131);
            // 
            // 
            // 
            this.gpSource.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpSource.Style.BackColorGradientAngle = 90;
            this.gpSource.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpSource.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSource.Style.BorderBottomWidth = 1;
            this.gpSource.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpSource.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSource.Style.BorderLeftWidth = 1;
            this.gpSource.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSource.Style.BorderRightWidth = 1;
            this.gpSource.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSource.Style.BorderTopWidth = 1;
            this.gpSource.Style.CornerDiameter = 4;
            this.gpSource.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpSource.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpSource.TabIndex = 5;
            this.gpSource.Tag = "";
            this.gpSource.Text = "排名成績來源";
            // 
            // chkRepeat
            // 
            this.chkRepeat.AutoSize = true;
            this.chkRepeat.BackColor = System.Drawing.Color.Transparent;
            this.chkRepeat.Location = new System.Drawing.Point(14, 66);
            this.chkRepeat.Name = "chkRepeat";
            this.chkRepeat.Size = new System.Drawing.Size(83, 21);
            this.chkRepeat.TabIndex = 0;
            this.chkRepeat.Text = "重修成績";
            this.chkRepeat.UseVisualStyleBackColor = false;
            // 
            // chkResit
            // 
            this.chkResit.AutoSize = true;
            this.chkResit.BackColor = System.Drawing.Color.Transparent;
            this.chkResit.Location = new System.Drawing.Point(14, 39);
            this.chkResit.Name = "chkResit";
            this.chkResit.Size = new System.Drawing.Size(83, 21);
            this.chkResit.TabIndex = 0;
            this.chkResit.Text = "補考成績";
            this.chkResit.UseVisualStyleBackColor = false;
            // 
            // chkOrigin
            // 
            this.chkOrigin.AutoSize = true;
            this.chkOrigin.BackColor = System.Drawing.Color.Transparent;
            this.chkOrigin.Location = new System.Drawing.Point(14, 12);
            this.chkOrigin.Name = "chkOrigin";
            this.chkOrigin.Size = new System.Drawing.Size(83, 21);
            this.chkOrigin.TabIndex = 0;
            this.chkOrigin.Text = "原始成績";
            this.chkOrigin.UseVisualStyleBackColor = false;
            // 
            // gpScoreType
            // 
            this.gpScoreType.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpScoreType.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpScoreType.Controls.Add(this.chkAvg2);
            this.gpScoreType.Controls.Add(this.chkAvg1);
            this.gpScoreType.Controls.Add(this.chkTotal);
            this.gpScoreType.Controls.Add(this.chkSubject);
            this.gpScoreType.Location = new System.Drawing.Point(320, 252);
            this.gpScoreType.Name = "gpScoreType";
            this.gpScoreType.Size = new System.Drawing.Size(306, 143);
            // 
            // 
            // 
            this.gpScoreType.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpScoreType.Style.BackColorGradientAngle = 90;
            this.gpScoreType.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpScoreType.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpScoreType.Style.BorderBottomWidth = 1;
            this.gpScoreType.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpScoreType.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpScoreType.Style.BorderLeftWidth = 1;
            this.gpScoreType.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpScoreType.Style.BorderRightWidth = 1;
            this.gpScoreType.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpScoreType.Style.BorderTopWidth = 1;
            this.gpScoreType.Style.CornerDiameter = 4;
            this.gpScoreType.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpScoreType.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpScoreType.TabIndex = 5;
            this.gpScoreType.Tag = "";
            this.gpScoreType.Text = "排名成績排名選項";
            // 
            // chkAvg2
            // 
            this.chkAvg2.AutoSize = true;
            this.chkAvg2.BackColor = System.Drawing.Color.Transparent;
            this.chkAvg2.Location = new System.Drawing.Point(14, 85);
            this.chkAvg2.Name = "chkAvg2";
            this.chkAvg2.Size = new System.Drawing.Size(191, 21);
            this.chkAvg2.TabIndex = 0;
            this.chkAvg2.Text = "加權平均(無成績列入計算)";
            this.chkAvg2.UseVisualStyleBackColor = false;
            // 
            // chkAvg1
            // 
            this.chkAvg1.AutoSize = true;
            this.chkAvg1.BackColor = System.Drawing.Color.Transparent;
            this.chkAvg1.Location = new System.Drawing.Point(14, 58);
            this.chkAvg1.Name = "chkAvg1";
            this.chkAvg1.Size = new System.Drawing.Size(177, 21);
            this.chkAvg1.TabIndex = 0;
            this.chkAvg1.Text = "加權平均(無成績不計算)";
            this.chkAvg1.UseVisualStyleBackColor = false;
            // 
            // chkTotal
            // 
            this.chkTotal.AutoSize = true;
            this.chkTotal.BackColor = System.Drawing.Color.Transparent;
            this.chkTotal.Location = new System.Drawing.Point(14, 31);
            this.chkTotal.Name = "chkTotal";
            this.chkTotal.Size = new System.Drawing.Size(279, 21);
            this.chkTotal.TabIndex = 0;
            this.chkTotal.Text = "加權總分 (依計算比例進行加權運算而得)";
            this.chkTotal.UseVisualStyleBackColor = false;
            // 
            // chkSubject
            // 
            this.chkSubject.AutoSize = true;
            this.chkSubject.BackColor = System.Drawing.Color.Transparent;
            this.chkSubject.Location = new System.Drawing.Point(14, 4);
            this.chkSubject.Name = "chkSubject";
            this.chkSubject.Size = new System.Drawing.Size(83, 21);
            this.chkSubject.TabIndex = 0;
            this.chkSubject.Text = "分科排名";
            this.chkSubject.UseVisualStyleBackColor = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = Properties.Resources.loading5;
            this.pictureBox1.Location = new System.Drawing.Point(137, 212);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 34);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(317, 402);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(130, 23);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Visible = false;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelected,
            this.colSubject,
            this.colCount,
            this.colWeight});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(13, 74);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(291, 321);
            this.dataGridViewX1.TabIndex = 8;
            this.dataGridViewX1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellValueChanged);
            this.dataGridViewX1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_ColumnHeaderMouseClick);
            // 
            // colSelected
            // 
            this.colSelected.HeaderText = "";
            this.colSelected.Name = "colSelected";
            this.colSelected.Width = 20;
            // 
            // colSubject
            // 
            this.colSubject.HeaderText = "科目";
            this.colSubject.Name = "colSubject";
            this.colSubject.ReadOnly = true;
            this.colSubject.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSubject.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSubject.Width = 160;
            // 
            // colCount
            // 
            this.colCount.HeaderText = "人數";
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            this.colCount.Visible = false;
            this.colCount.Width = 60;
            // 
            // colWeight
            // 
            this.colWeight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colWeight.HeaderText = "計算比例";
            this.colWeight.Name = "colWeight";
            // 
            // PlaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 432);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.gpScoreType);
            this.Controls.Add(this.gpSource);
            this.Controls.Add(this.gpSortType);
            this.Controls.Add(this.lblSemester);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.cboSchoolYear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.dataGridViewX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PlaceForm";
            this.ShowIcon = false;
            this.Text = "即時排名";
            this.gpSortType.ResumeLayout(false);
            this.gpSortType.PerformLayout();
            this.gpSource.ResumeLayout(false);
            this.gpSource.PerformLayout();
            this.gpScoreType.ResumeLayout(false);
            this.gpScoreType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSort;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX lblSemester;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboType;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.GroupPanel gpSortType;
        private System.Windows.Forms.RadioButton rbDupicate;
        private System.Windows.Forms.RadioButton rbUndupicate;
        private DevComponents.DotNetBar.Controls.GroupPanel gpSource;
        private DevComponents.DotNetBar.Controls.GroupPanel gpScoreType;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.LabelX lblMessage;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.CheckBox chkAvg2;
        private System.Windows.Forms.CheckBox chkAvg1;
        private System.Windows.Forms.CheckBox chkTotal;
        private System.Windows.Forms.CheckBox chkSubject;
        private System.Windows.Forms.CheckBox chkRepeat;
        private System.Windows.Forms.CheckBox chkResit;
        private System.Windows.Forms.CheckBox chkOrigin;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeight;
    }
}