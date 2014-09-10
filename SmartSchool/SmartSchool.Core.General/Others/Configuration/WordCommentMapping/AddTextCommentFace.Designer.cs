namespace SmartSchool.Others.Configuration.WordCommentMapping
{
    partial class AddTextCommentFace
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
            this.cboNames = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.lblCopyExist = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.txtItemName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // cboNames
            // 
            this.cboNames.DisplayMember = "TemplateName";
            this.cboNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNames.FormattingEnabled = true;
            this.cboNames.ItemHeight = 19;
            this.cboNames.Items.AddRange(new object[] {
            this.comboItem1});
            this.cboNames.Location = new System.Drawing.Point(107, 45);
            this.cboNames.Name = "cboNames";
            this.cboNames.Size = new System.Drawing.Size(256, 25);
            this.cboNames.TabIndex = 11;
            this.cboNames.ValueMember = "Identity";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "<不複製>";
            // 
            // lblCopyExist
            // 
            this.lblCopyExist.AutoSize = true;
            this.lblCopyExist.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyExist.Location = new System.Drawing.Point(15, 48);
            this.lblCopyExist.Name = "lblCopyExist";
            this.lblCopyExist.Size = new System.Drawing.Size(87, 19);
            this.lblCopyExist.TabIndex = 10;
            this.lblCopyExist.Text = "複製現有項目";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(288, 82);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "離開";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(207, 82);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "確定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtItemName
            // 
            // 
            // 
            // 
            this.txtItemName.Border.Class = "TextBoxBorder";
            this.txtItemName.Location = new System.Drawing.Point(107, 14);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(256, 25);
            this.txtItemName.TabIndex = 7;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(15, 17);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 19);
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "項目名稱";
            // 
            // AddTextCommentFace
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(370, 112);
            this.Controls.Add(this.cboNames);
            this.Controls.Add(this.lblCopyExist);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.labelX1);
            this.Name = "AddTextCommentFace";
            this.Text = "新增文字評量項目";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNames;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.DotNetBar.LabelX lblCopyExist;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.Controls.TextBoxX txtItemName;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}