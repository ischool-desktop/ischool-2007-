namespace SmartSchool.Payment.StudentList
{
    partial class PaymentDetailView
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
            this.dgPaymentDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.chClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chStuName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chStuNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgPaymentDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgPaymentDetail
            // 
            this.dgPaymentDetail.AllowUserToAddRows = false;
            this.dgPaymentDetail.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPaymentDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPaymentDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPaymentDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chClass,
            this.chStuName,
            this.chStuNumber,
            this.Column4,
            this.Column5,
            this.Column6,
            this.chAmount,
            this.Column1});
            this.dgPaymentDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPaymentDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPaymentDetail.Location = new System.Drawing.Point(0, 0);
            this.dgPaymentDetail.Margin = new System.Windows.Forms.Padding(4);
            this.dgPaymentDetail.MultiSelect = false;
            this.dgPaymentDetail.Name = "dgPaymentDetail";
            this.dgPaymentDetail.PaintEnhancedSelection = false;
            this.dgPaymentDetail.RowTemplate.Height = 24;
            this.dgPaymentDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPaymentDetail.Size = new System.Drawing.Size(846, 367);
            this.dgPaymentDetail.TabIndex = 1;
            this.dgPaymentDetail.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgPaymentDetail_ColumnHeaderMouseClick);
            // 
            // chClass
            // 
            this.chClass.HeaderText = "班級";
            this.chClass.Name = "chClass";
            this.chClass.ReadOnly = true;
            this.chClass.Width = 50;
            // 
            // chStuName
            // 
            this.chStuName.HeaderText = "姓名";
            this.chStuName.Name = "chStuName";
            this.chStuName.ReadOnly = true;
            this.chStuName.Width = 50;
            // 
            // chStuNumber
            // 
            this.chStuNumber.HeaderText = "學號";
            this.chStuNumber.Name = "chStuNumber";
            this.chStuNumber.ReadOnly = true;
            this.chStuNumber.Width = 50;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "$A";
            this.Column4.Name = "Column4";
            this.Column4.Width = 45;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "$B";
            this.Column5.Name = "Column5";
            this.Column5.Width = 45;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "$C";
            this.Column6.Name = "Column6";
            this.Column6.Width = 45;
            // 
            // chAmount
            // 
            this.chAmount.HeaderText = "總金額";
            this.chAmount.Name = "chAmount";
            this.chAmount.ReadOnly = true;
            this.chAmount.Width = 60;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "已繳金額";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // PaymentDetailView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dgPaymentDetail);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PaymentDetailView";
            this.Size = new System.Drawing.Size(846, 367);
            ((System.ComponentModel.ISupportInitialize)(this.dgPaymentDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgPaymentDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn chClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn chStuName;
        private System.Windows.Forms.DataGridViewTextBoxColumn chStuNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn chAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}
