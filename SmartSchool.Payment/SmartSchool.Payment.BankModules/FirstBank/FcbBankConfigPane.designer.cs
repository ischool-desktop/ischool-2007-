namespace FirstBankPayment.FirstBank
{
    partial class FcbBankConfigPane
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX1 = new System.Windows.Forms.Label();
            this.dgEnterpriseCode = new System.Windows.Forms.DataGridView();
            this.chStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chShop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chEnterCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSchoolCode = new System.Windows.Forms.TextBox();
            this.labelX4 = new System.Windows.Forms.Label();
            this.cboChargeOnus = new System.Windows.Forms.ComboBox();
            this.labelX2 = new System.Windows.Forms.Label();
            this.txtPostCharge = new System.Windows.Forms.TextBox();
            this.labelX3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgEnterpriseCode)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(14, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(86, 17);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "企業代碼設定";
            // 
            // dgEnterpriseCode
            // 
            this.dgEnterpriseCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEnterpriseCode.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEnterpriseCode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgEnterpriseCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEnterpriseCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chStart,
            this.chEnd,
            this.chShop,
            this.chEnterCode});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEnterpriseCode.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgEnterpriseCode.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgEnterpriseCode.Location = new System.Drawing.Point(14, 37);
            this.dgEnterpriseCode.Margin = new System.Windows.Forms.Padding(4);
            this.dgEnterpriseCode.Name = "dgEnterpriseCode";
            this.dgEnterpriseCode.RowTemplate.Height = 24;
            this.dgEnterpriseCode.Size = new System.Drawing.Size(527, 177);
            this.dgEnterpriseCode.TabIndex = 3;
            this.dgEnterpriseCode.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEnterpriseCode_RowValidated);
            // 
            // chStart
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
            this.chStart.DefaultCellStyle = dataGridViewCellStyle2;
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
            // txtSchoolCode
            // 
            this.txtSchoolCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSchoolCode.Location = new System.Drawing.Point(466, 220);
            this.txtSchoolCode.Name = "txtSchoolCode";
            this.txtSchoolCode.Size = new System.Drawing.Size(75, 25);
            this.txtSchoolCode.TabIndex = 9;
            // 
            // labelX4
            // 
            this.labelX4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX4.AutoSize = true;
            this.labelX4.Location = new System.Drawing.Point(400, 223);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(60, 17);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "學校代碼";
            // 
            // cboChargeOnus
            // 
            this.cboChargeOnus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboChargeOnus.DisplayMember = "Text";
            this.cboChargeOnus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChargeOnus.FormattingEnabled = true;
            this.cboChargeOnus.ItemHeight = 17;
            this.cboChargeOnus.Items.AddRange(new object[] {
            "學校負擔",
            "學生負擔"});
            this.cboChargeOnus.Location = new System.Drawing.Point(105, 220);
            this.cboChargeOnus.Name = "cboChargeOnus";
            this.cboChargeOnus.Size = new System.Drawing.Size(114, 25);
            this.cboChargeOnus.TabIndex = 6;
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(12, 223);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(86, 17);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "通路手續費由";
            // 
            // txtPostCharge
            // 
            this.txtPostCharge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPostCharge.Location = new System.Drawing.Point(307, 220);
            this.txtPostCharge.Name = "txtPostCharge";
            this.txtPostCharge.Size = new System.Drawing.Size(81, 25);
            this.txtPostCharge.TabIndex = 11;
            this.txtPostCharge.Visible = false;
            // 
            // labelX3
            // 
            this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX3.AutoSize = true;
            this.labelX3.Location = new System.Drawing.Point(231, 223);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(73, 17);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "郵局手續費";
            this.labelX3.Visible = false;
            // 
            // FcbBankConfigPane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.txtPostCharge);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtSchoolCode);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.cboChargeOnus);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.dgEnterpriseCode);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FcbBankConfigPane";
            this.Size = new System.Drawing.Size(550, 250);
            ((System.ComponentModel.ISupportInitialize)(this.dgEnterpriseCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelX1;
        private System.Windows.Forms.DataGridView dgEnterpriseCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn chStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn chEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn chShop;
        private System.Windows.Forms.DataGridViewTextBoxColumn chEnterCode;
        private System.Windows.Forms.TextBox txtSchoolCode;
        private System.Windows.Forms.Label labelX4;
        private System.Windows.Forms.ComboBox cboChargeOnus;
        private System.Windows.Forms.Label labelX2;
        private System.Windows.Forms.TextBox txtPostCharge;
        private System.Windows.Forms.Label labelX3;
    }
}
