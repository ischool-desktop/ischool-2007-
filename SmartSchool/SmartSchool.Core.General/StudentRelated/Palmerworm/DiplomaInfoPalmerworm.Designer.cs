namespace SmartSchool.StudentRelated.Palmerworm
{
    partial class DiplomaInfoPalmerworm
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
            if ( disposing && ( components != null ) )
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtSchoolYear = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtReason = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtClass = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDept = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(18, 86);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(101, 19);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "畢業證書字號：";
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Location = new System.Drawing.Point(121, 83);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(339, 25);
            this.textBoxX1.TabIndex = 4;
            this.textBoxX1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.HighlightSelectedColumnHeaders = false;
            this.dataGridViewX1.Location = new System.Drawing.Point(18, 118);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowHeadersWidth = 27;
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(514, 105);
            this.dataGridViewX1.TabIndex = 5;
            this.dataGridViewX1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseDown);
            this.dataGridViewX1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewX1_CurrentCellDirtyStateChanged);
            this.dataGridViewX1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellEnter);
            this.dataGridViewX1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridViewX1_RowsRemoved);
            this.dataGridViewX1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_RowHeaderMouseClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 30F;
            this.Column1.HeaderText = "畢業相關訊息";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 70F;
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 40F;
            this.dataGridViewTextBoxColumn1.HeaderText = "畢業相關訊息";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 60F;
            this.dataGridViewTextBoxColumn2.HeaderText = "";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(18, 14);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(87, 19);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "離校學年度：";
            // 
            // txtSchoolYear
            // 
            // 
            // 
            // 
            this.txtSchoolYear.Border.Class = "TextBoxBorder";
            this.txtSchoolYear.Location = new System.Drawing.Point(121, 11);
            this.txtSchoolYear.Name = "txtSchoolYear";
            this.txtSchoolYear.Size = new System.Drawing.Size(60, 25);
            this.txtSchoolYear.TabIndex = 0;
            this.txtSchoolYear.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.Location = new System.Drawing.Point(264, 14);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(74, 19);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "離校類別：";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.Location = new System.Drawing.Point(18, 50);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(74, 19);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "離校科別：";
            // 
            // txtReason
            // 
            // 
            // 
            // 
            this.txtReason.Border.Class = "TextBoxBorder";
            this.txtReason.Location = new System.Drawing.Point(340, 11);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(120, 25);
            this.txtReason.TabIndex = 1;
            this.txtReason.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // txtClass
            // 
            // 
            // 
            // 
            this.txtClass.Border.Class = "TextBoxBorder";
            this.txtClass.Location = new System.Drawing.Point(340, 47);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(120, 25);
            this.txtClass.TabIndex = 3;
            this.txtClass.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // txtDept
            // 
            // 
            // 
            // 
            this.txtDept.Border.Class = "TextBoxBorder";
            this.txtDept.Location = new System.Drawing.Point(121, 47);
            this.txtDept.Name = "txtDept";
            this.txtDept.Size = new System.Drawing.Size(120, 25);
            this.txtDept.TabIndex = 2;
            this.txtDept.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.Location = new System.Drawing.Point(263, 50);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(74, 19);
            this.labelX5.TabIndex = 5;
            this.labelX5.Text = "離校班級：";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkRate = 0;
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // DiplomaInfoPalmerworm
            // 
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtSchoolYear);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.txtDept);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.dataGridViewX1);
            this.Name = "DiplomaInfoPalmerworm";
            this.Size = new System.Drawing.Size(550, 239);
            this.Title = "畢業及離校資訊";
            this.Controls.SetChildIndex(this.dataGridViewX1, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.textBoxX1, 0);
            this.Controls.SetChildIndex(this.labelX5, 0);
            this.Controls.SetChildIndex(this.txtDept, 0);
            this.Controls.SetChildIndex(this.txtClass, 0);
            this.Controls.SetChildIndex(this.txtReason, 0);
            this.Controls.SetChildIndex(this.labelX4, 0);
            this.Controls.SetChildIndex(this.labelX3, 0);
            this.Controls.SetChildIndex(this.txtSchoolYear, 0);
            this.Controls.SetChildIndex(this.labelX2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtReason;
        private DevComponents.DotNetBar.Controls.TextBoxX txtClass;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDept;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
