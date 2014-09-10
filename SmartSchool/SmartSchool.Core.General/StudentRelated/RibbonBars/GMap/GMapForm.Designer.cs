namespace SmartSchool.StudentRelated.RibbonBars.GMap
{
    partial class GMapForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.picAddressLoading = new System.Windows.Forms.PictureBox();
            this.lblGMapLoading = new DevComponents.DotNetBar.LabelX();
            this.flpStatistics = new System.Windows.Forms.FlowLayoutPanel();
            this.chClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chSeatNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAddressLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(185, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(799, 614);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chClass,
            this.chSeatNo,
            this.chName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(185, 614);
            this.dataGridViewX1.TabIndex = 1;
            this.dataGridViewX1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridViewX1_SortCompare);
            this.dataGridViewX1.SelectionChanged += new System.EventHandler(this.dataGridViewX1_SelectionChanged);
            // 
            // picAddressLoading
            // 
            this.picAddressLoading.Image = Properties.Resources.loading5;
            this.picAddressLoading.Location = new System.Drawing.Point(65, 300);
            this.picAddressLoading.Name = "picAddressLoading";
            this.picAddressLoading.Size = new System.Drawing.Size(32, 32);
            this.picAddressLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picAddressLoading.TabIndex = 2;
            this.picAddressLoading.TabStop = false;
            this.picAddressLoading.Visible = false;
            // 
            // lblGMapLoading
            // 
            this.lblGMapLoading.AutoSize = true;
            this.lblGMapLoading.Location = new System.Drawing.Point(489, 212);
            this.lblGMapLoading.Name = "lblGMapLoading";
            this.lblGMapLoading.Size = new System.Drawing.Size(110, 19);
            this.lblGMapLoading.TabIndex = 3;
            this.lblGMapLoading.Text = "載入地圖資訊中...";
            this.lblGMapLoading.Visible = false;
            // 
            // flpStatistics
            // 
            this.flpStatistics.AutoScroll = true;
            this.flpStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpStatistics.Location = new System.Drawing.Point(185, 537);
            this.flpStatistics.Name = "flpStatistics";
            this.flpStatistics.Size = new System.Drawing.Size(799, 77);
            this.flpStatistics.TabIndex = 4;
            // 
            // chClass
            // 
            this.chClass.HeaderText = "班級";
            this.chClass.Name = "chClass";
            this.chClass.ReadOnly = true;
            this.chClass.Width = 60;
            // 
            // chSeatNo
            // 
            this.chSeatNo.HeaderText = "座號";
            this.chSeatNo.Name = "chSeatNo";
            this.chSeatNo.ReadOnly = true;
            this.chSeatNo.Width = 50;
            // 
            // chName
            // 
            this.chName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chName.HeaderText = "姓名";
            this.chName.Name = "chName";
            this.chName.ReadOnly = true;
            // 
            // GMapForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(984, 614);
            this.Controls.Add(this.flpStatistics);
            this.Controls.Add(this.lblGMapLoading);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.picAddressLoading);
            this.Controls.Add(this.dataGridViewX1);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GMapForm";
            this.Text = "地圖資訊";
            this.Load += new System.EventHandler(this.GMapForm_Load);
            this.ResizeBegin += new System.EventHandler(this.GMapForm_ResizeBegin);
            this.SizeChanged += new System.EventHandler(this.GMapForm_SizeChanged);
            this.ResizeEnd += new System.EventHandler(this.GMapForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAddressLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.PictureBox picAddressLoading;
        private DevComponents.DotNetBar.LabelX lblGMapLoading;
        private System.Windows.Forms.FlowLayoutPanel flpStatistics;
        private System.Windows.Forms.DataGridViewTextBoxColumn chClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn chSeatNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn chName;
    }
}