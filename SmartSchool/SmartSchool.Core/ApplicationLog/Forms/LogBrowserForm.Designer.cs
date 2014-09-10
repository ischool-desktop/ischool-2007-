namespace SmartSchool.ApplicationLog.Forms
{
    partial class LogBrowserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnRefresn = new DevComponents.DotNetBar.ButtonX();
            this.txtEndTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtStartTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkByTime = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkListAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dgvLogs = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx1.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.dgvLogs ) ).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.btnRefresn);
            this.panelEx1.Controls.Add(this.txtEndTime);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.txtStartTime);
            this.panelEx1.Controls.Add(this.chkByTime);
            this.panelEx1.Controls.Add(this.chkListAll);
            this.panelEx1.Location = new System.Drawing.Point(12, 12);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(768, 45);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.CornerDiameter = 10;
            this.panelEx1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnRefresn
            // 
            this.btnRefresn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresn.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnRefresn.Location = new System.Drawing.Point(676, 12);
            this.btnRefresn.Name = "btnRefresn";
            this.btnRefresn.Size = new System.Drawing.Size(82, 23);
            this.btnRefresn.TabIndex = 4;
            this.btnRefresn.Text = "重新整理";
            this.btnRefresn.Click += new System.EventHandler(this.btnRefresn_Click);
            // 
            // txtEndTime
            // 
            // 
            // 
            // 
            this.txtEndTime.Border.Class = "TextBoxBorder";
            this.txtEndTime.Location = new System.Drawing.Point(471, 11);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(150, 25);
            this.txtEndTime.TabIndex = 3;
            this.txtEndTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTime_KeyDown);
            this.txtEndTime.Validating += new System.ComponentModel.CancelEventHandler(this.txtTime_Validating);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(452, 14);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(17, 19);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "~";
            // 
            // txtStartTime
            // 
            // 
            // 
            // 
            this.txtStartTime.Border.Class = "TextBoxBorder";
            this.txtStartTime.Location = new System.Drawing.Point(297, 11);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(150, 25);
            this.txtStartTime.TabIndex = 2;
            this.txtStartTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTime_KeyDown);
            this.txtStartTime.Validating += new System.ComponentModel.CancelEventHandler(this.txtTime_Validating);
            // 
            // chkByTime
            // 
            this.chkByTime.AutoSize = true;
            this.chkByTime.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkByTime.Checked = true;
            this.chkByTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByTime.CheckValue = "Y";
            this.chkByTime.Location = new System.Drawing.Point(148, 15);
            this.chkByTime.Name = "chkByTime";
            this.chkByTime.Size = new System.Drawing.Size(146, 21);
            this.chkByTime.TabIndex = 1;
            this.chkByTime.Text = "依日期顯示修改歷程";
            this.chkByTime.CheckedChanged += new System.EventHandler(this.chkByTime_CheckedChanged);
            // 
            // chkListAll
            // 
            this.chkListAll.AutoSize = true;
            this.chkListAll.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkListAll.Location = new System.Drawing.Point(13, 15);
            this.chkListAll.Name = "chkListAll";
            this.chkListAll.Size = new System.Drawing.Size(133, 21);
            this.chkListAll.TabIndex = 0;
            this.chkListAll.Text = "顯示所有修改歷程";
            this.chkListAll.CheckedChanged += new System.EventHandler(this.chkListAll_CheckedChanged);
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvLogs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLogs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDateTime,
            this.colUserName,
            this.colIP,
            this.colAction,
            this.colDesc});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLogs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLogs.GridColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 208 ) ) ) ), ( (int)( ( (byte)( 215 ) ) ) ), ( (int)( ( (byte)( 229 ) ) ) ));
            this.dgvLogs.HighlightSelectedColumnHeaders = false;
            this.dgvLogs.Location = new System.Drawing.Point(12, 67);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.RowTemplate.Height = 24;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.Size = new System.Drawing.Size(769, 464);
            this.dgvLogs.TabIndex = 1;
            this.dgvLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogs_CellDoubleClick);
            // 
            // colDateTime
            // 
            this.colDateTime.HeaderText = "時間";
            this.colDateTime.Name = "colDateTime";
            this.colDateTime.ReadOnly = true;
            this.colDateTime.Width = 150;
            // 
            // colUserName
            // 
            this.colUserName.HeaderText = "帳號";
            this.colUserName.Name = "colUserName";
            this.colUserName.ReadOnly = true;
            // 
            // colIP
            // 
            this.colIP.HeaderText = "IP位置";
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "動作";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Width = 80;
            // 
            // colDesc
            // 
            this.colDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDesc.HeaderText = "說明";
            this.colDesc.Name = "colDesc";
            this.colDesc.ReadOnly = true;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.buttonX1.Enabled = false;
            this.buttonX1.Location = new System.Drawing.Point(13, 541);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(113, 23);
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "匯出至 Excel";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx2.Controls.Add(this.panelEx1);
            this.panelEx2.Controls.Add(this.buttonX1);
            this.panelEx2.Controls.Add(this.dgvLogs);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(792, 573);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 3;
            // 
            // LogBrowserForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.panelEx2);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(645, 420);
            this.Name = "LogBrowserForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "日誌瀏覽";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LogBrowserForm_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.dgvLogs ) ).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEndTime;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStartTime;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkByTime;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkListAll;
        private DevComponents.DotNetBar.ButtonX btnRefresn;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvLogs;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private DevComponents.DotNetBar.PanelEx panelEx2;
    }
}