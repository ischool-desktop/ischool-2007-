namespace SmartSchool.StudentRelated
{
    partial class StudentTagEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentTagEditor));
            this.cboGroups = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cpColor = new DevComponents.DotNetBar.ColorPickerButton();
            this.SuspendLayout();
            // 
            // cboGroups
            // 
            this.cboGroups.DisplayMember = "Text";
            this.cboGroups.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGroups.FormattingEnabled = true;
            this.cboGroups.ItemHeight = 18;
            this.cboGroups.Location = new System.Drawing.Point(45, 6);
            this.cboGroups.Name = "cboGroups";
            this.cboGroups.Size = new System.Drawing.Size(188, 24);
            this.cboGroups.TabIndex = 0;
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Location = new System.Drawing.Point(45, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(148, 23);
            this.txtName.TabIndex = 1;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(7, 9);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(34, 19);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "群組";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(99, 74);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(64, 23);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "確定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(169, 74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(7, 42);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 19);
            this.labelX2.TabIndex = 8;
            this.labelX2.Text = "名稱";
            // 
            // cpColor
            // 
            this.cpColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cpColor.AutoExpandOnClick = true;
            this.cpColor.Image = ((System.Drawing.Image)(resources.GetObject("cpColor.Image")));
            this.cpColor.Location = new System.Drawing.Point(196, 40);
            this.cpColor.Name = "cpColor";
            this.cpColor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.cpColor.Size = new System.Drawing.Size(37, 23);
            this.cpColor.TabIndex = 10;
            this.cpColor.LocalizeString += new DevComponents.DotNetBar.DotNetBarManager.LocalizeStringEventHandler(this.cpColor_LocalizeString);
            // 
            // StudentTagEditor
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(243, 102);
            this.Controls.Add(this.cpColor);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cboGroups);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StudentTagEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新增類別";
            this.Load += new System.EventHandler(this.TagInsert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGroups;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ColorPickerButton cpColor;
    }
}