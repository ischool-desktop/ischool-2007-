namespace SmartSchool.Payment.AccountStatedService
{
    partial class ConfigMain
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
            this.lstConfigs = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.ctxServiceControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxiRunNow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.ctxServiceControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstConfigs
            // 
            this.lstConfigs.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstConfigs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConfigs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lstConfigs.ContextMenuStrip = this.ctxServiceControl;
            this.lstConfigs.FullRowSelect = true;
            this.lstConfigs.HideSelection = false;
            this.lstConfigs.HotTracking = true;
            this.lstConfigs.HoverSelection = true;
            this.lstConfigs.Location = new System.Drawing.Point(25, 12);
            this.lstConfigs.Name = "lstConfigs";
            this.lstConfigs.Size = new System.Drawing.Size(354, 217);
            this.lstConfigs.TabIndex = 0;
            this.lstConfigs.UseCompatibleStateImageBehavior = false;
            this.lstConfigs.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "學校名稱";
            this.chName.Width = 262;
            // 
            // ctxServiceControl
            // 
            this.ctxServiceControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxiRunNow});
            this.ctxServiceControl.Name = "ctxServiceControl";
            this.ctxServiceControl.Size = new System.Drawing.Size(125, 26);
            // 
            // ctxiRunNow
            // 
            this.ctxiRunNow.Name = "ctxiRunNow";
            this.ctxiRunNow.Size = new System.Drawing.Size(124, 22);
            this.ctxiRunNow.Text = "立即執行";
            this.ctxiRunNow.Click += new System.EventHandler(this.ctxiRunNow_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(142, 235);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 26);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(302, 235);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 26);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "刪除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(222, 235);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 26);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnServiceStatus
            // 
            this.btnServiceStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnServiceStatus.Location = new System.Drawing.Point(25, 235);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(86, 26);
            this.btnServiceStatus.TabIndex = 1;
            this.btnServiceStatus.Text = "服務狀態";
            this.btnServiceStatus.UseVisualStyleBackColor = true;
            this.btnServiceStatus.Click += new System.EventHandler(this.btnServiceStatus_Click);
            // 
            // ConfigMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(391, 273);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnServiceStatus);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstConfigs);
            this.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "ConfigMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "學校對帳設定";
            this.Load += new System.EventHandler(this.ConfigMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigMain_FormClosing);
            this.ctxServiceControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstConfigs;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnServiceStatus;
        private System.Windows.Forms.ContextMenuStrip ctxServiceControl;
        private System.Windows.Forms.ToolStripMenuItem ctxiRunNow;
    }
}