namespace SmartSchool.Payment
{
    partial class BillOutputSettingForm
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
            this.txtSourceFile = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSelectFile = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.gpSplitField = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkDeptName = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkNoSplit = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkClassName = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkSingle = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkPreview = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.gpSplitField.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSourceFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtSourceFile.Border.Class = "TextBoxBorder";
            this.txtSourceFile.Location = new System.Drawing.Point(15, 32);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.ReadOnly = true;
            this.txtSourceFile.Size = new System.Drawing.Size(354, 25);
            this.txtSourceFile.TabIndex = 1;
            this.txtSourceFile.WatermarkText = "請選擇檔案位置";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(15, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "繳費單存放目錄";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFile.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectFile.Location = new System.Drawing.Point(375, 33);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(29, 22);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(329, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Location = new System.Drawing.Point(243, 224);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(72, 25);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "確定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // gpSplitField
            // 
            this.gpSplitField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpSplitField.BackColor = System.Drawing.Color.Transparent;
            this.gpSplitField.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpSplitField.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpSplitField.Controls.Add(this.chkDeptName);
            this.gpSplitField.Controls.Add(this.chkNoSplit);
            this.gpSplitField.Controls.Add(this.chkClassName);
            this.gpSplitField.Controls.Add(this.chkSingle);
            this.gpSplitField.Location = new System.Drawing.Point(15, 71);
            this.gpSplitField.Name = "gpSplitField";
            this.gpSplitField.Size = new System.Drawing.Size(389, 142);
            // 
            // 
            // 
            this.gpSplitField.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpSplitField.Style.BackColorGradientAngle = 90;
            this.gpSplitField.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpSplitField.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSplitField.Style.BorderBottomWidth = 1;
            this.gpSplitField.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpSplitField.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSplitField.Style.BorderLeftWidth = 1;
            this.gpSplitField.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSplitField.Style.BorderRightWidth = 1;
            this.gpSplitField.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSplitField.Style.BorderTopWidth = 1;
            this.gpSplitField.Style.CornerDiameter = 4;
            this.gpSplitField.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpSplitField.Style.TextColor = System.Drawing.SystemColors.ControlText;
            this.gpSplitField.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gpSplitField.TabIndex = 3;
            this.gpSplitField.Text = "繳費單分割設定";
            // 
            // chkDeptName
            // 
            this.chkDeptName.AutoSize = true;
            this.chkDeptName.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkDeptName.CheckValueChecked = "DeptName";
            this.chkDeptName.Location = new System.Drawing.Point(12, 61);
            this.chkDeptName.Name = "chkDeptName";
            this.chkDeptName.Size = new System.Drawing.Size(342, 21);
            this.chkDeptName.TabIndex = 1;
            this.chkDeptName.Text = "依科別資訊分成不同檔案(使用科別名稱當檔案名稱)。";
            // 
            // chkNoSplit
            // 
            this.chkNoSplit.AutoSize = true;
            this.chkNoSplit.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkNoSplit.Checked = true;
            this.chkNoSplit.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkNoSplit.CheckValue = "";
            this.chkNoSplit.CheckValueChecked = "ClassName";
            this.chkNoSplit.Location = new System.Drawing.Point(12, 7);
            this.chkNoSplit.Name = "chkNoSplit";
            this.chkNoSplit.Size = new System.Drawing.Size(213, 21);
            this.chkNoSplit.TabIndex = 0;
            this.chkNoSplit.Text = "所有繳費單集中在同一個檔案。";
            // 
            // chkClassName
            // 
            this.chkClassName.AutoSize = true;
            this.chkClassName.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkClassName.CheckValueChecked = "ClassName";
            this.chkClassName.Location = new System.Drawing.Point(12, 34);
            this.chkClassName.Name = "chkClassName";
            this.chkClassName.Size = new System.Drawing.Size(342, 21);
            this.chkClassName.TabIndex = 0;
            this.chkClassName.Text = "依班級資訊分成不同檔案(使用班級名稱當檔案名稱)。";
            // 
            // chkSingle
            // 
            this.chkSingle.AutoSize = true;
            this.chkSingle.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkSingle.CheckValueChecked = "StudentNumber";
            this.chkSingle.Location = new System.Drawing.Point(12, 88);
            this.chkSingle.Name = "chkSingle";
            this.chkSingle.Size = new System.Drawing.Size(316, 21);
            this.chkSingle.TabIndex = 2;
            this.chkSingle.Text = "每張繳費單分成不同檔案(使用學號當檔案名稱)。";
            // 
            // chkPreview
            // 
            this.chkPreview.AutoSize = true;
            this.chkPreview.BackColor = System.Drawing.Color.Transparent;
            this.chkPreview.CheckValueChecked = "ClassName";
            this.chkPreview.Location = new System.Drawing.Point(15, 228);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(189, 21);
            this.chkPreview.TabIndex = 6;
            this.chkPreview.Text = "我只要試印前10筆繳費單。";
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // BillOutputSettingForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(414, 258);
            this.Controls.Add(this.chkPreview);
            this.Controls.Add(this.gpSplitField);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSourceFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSelectFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BillOutputSettingForm";
            this.Text = "繳費單輸出設定";
            this.gpSplitField.ResumeLayout(false);
            this.gpSplitField.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevComponents.DotNetBar.Controls.TextBoxX txtSourceFile;
        private System.Windows.Forms.Label label7;
        public DevComponents.DotNetBar.ButtonX btnSelectFile;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.Controls.GroupPanel gpSplitField;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkDeptName;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkClassName;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSingle;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNoSplit;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPreview;
    }
}