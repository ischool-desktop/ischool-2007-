namespace SmartSchool.Payment.BankModules.Chinatrust
{
    partial class CTBankConfigPane
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgEnterpriseCode = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.chStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chShop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chEnterCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cboChargeOnus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtPostCharge = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtSchoolCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dgEnterpriseCode)).BeginInit();
            this.SuspendLayout();
            // 
            // dgEnterpriseCode
            // 
            this.dgEnterpriseCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEnterpriseCode.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEnterpriseCode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgEnterpriseCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEnterpriseCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chStart,
            this.chEnd,
            this.chShop,
            this.chEnterCode});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEnterpriseCode.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgEnterpriseCode.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgEnterpriseCode.Location = new System.Drawing.Point(14, 37);
            this.dgEnterpriseCode.Margin = new System.Windows.Forms.Padding(4);
            this.dgEnterpriseCode.Name = "dgEnterpriseCode";
            this.dgEnterpriseCode.RowTemplate.Height = 24;
            this.dgEnterpriseCode.Size = new System.Drawing.Size(527, 177);
            this.dgEnterpriseCode.TabIndex = 0;
            this.dgEnterpriseCode.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEnterpriseCode_RowValidated);
            this.dgEnterpriseCode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEnterpriseCode_CellClick);
            // 
            // chStart
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Gainsboro;
            this.chStart.DefaultCellStyle = dataGridViewCellStyle5;
            this.chStart.HeaderText = "金額(>=)";
            this.chStart.Name = "chStart";
            this.chStart.ReadOnly = true;
            this.chStart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.chStart.Width = 80;
            // 
            // chEnd
            // 
            this.chEnd.HeaderText = "金額(<=)";
            this.chEnd.Name = "chEnd";
            this.chEnd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.chEnd.Width = 80;
            // 
            // chShop
            // 
            this.chShop.HeaderText = "便利商店手續費";
            this.chShop.Name = "chShop";
            this.chShop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.chShop.Width = 110;
            // 
            // chEnterCode
            // 
            this.chEnterCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chEnterCode.HeaderText = "企業代碼";
            this.chEnterCode.Name = "chEnterCode";
            this.chEnterCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(14, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 19);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "企業代碼設定";
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(12, 223);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(87, 19);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "通路手續費由";
            // 
            // cboChargeOnus
            // 
            this.cboChargeOnus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboChargeOnus.DisplayMember = "Text";
            this.cboChargeOnus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboChargeOnus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChargeOnus.FormattingEnabled = true;
            this.cboChargeOnus.ItemHeight = 19;
            this.cboChargeOnus.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboChargeOnus.Location = new System.Drawing.Point(105, 220);
            this.cboChargeOnus.Name = "cboChargeOnus";
            this.cboChargeOnus.Size = new System.Drawing.Size(114, 25);
            this.cboChargeOnus.TabIndex = 2;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "學校負擔";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "學生負擔";
            // 
            // labelX3
            // 
            this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX3.AutoSize = true;
            this.labelX3.Location = new System.Drawing.Point(231, 223);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(74, 19);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "郵局手續費";
            // 
            // txtPostCharge
            // 
            this.txtPostCharge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtPostCharge.Border.Class = "TextBoxBorder";
            this.txtPostCharge.Location = new System.Drawing.Point(313, 220);
            this.txtPostCharge.Name = "txtPostCharge";
            this.txtPostCharge.Size = new System.Drawing.Size(81, 25);
            this.txtPostCharge.TabIndex = 4;
            // 
            // labelX4
            // 
            this.labelX4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX4.AutoSize = true;
            this.labelX4.Location = new System.Drawing.Point(400, 223);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(60, 19);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "學校代碼";
            // 
            // txtSchoolCode
            // 
            this.txtSchoolCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSchoolCode.Border.Class = "TextBoxBorder";
            this.txtSchoolCode.Location = new System.Drawing.Point(466, 220);
            this.txtSchoolCode.Name = "txtSchoolCode";
            this.txtSchoolCode.Size = new System.Drawing.Size(75, 25);
            this.txtSchoolCode.TabIndex = 4;
            // 
            // CTBankConfigPane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.txtSchoolCode);
            this.Controls.Add(this.txtPostCharge);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cboChargeOnus);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.dgEnterpriseCode);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CTBankConfigPane";
            this.Size = new System.Drawing.Size(550, 250);
            ((System.ComponentModel.ISupportInitialize)(this.dgEnterpriseCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgEnterpriseCode;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboChargeOnus;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPostCharge;
        private System.Windows.Forms.DataGridViewTextBoxColumn chStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn chEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn chShop;
        private System.Windows.Forms.DataGridViewTextBoxColumn chEnterCode;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSchoolCode;
    }
}
