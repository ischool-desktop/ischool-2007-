namespace SmartSchool.Survey
{
    partial class SelectSurveyeeType
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
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.cboSurveyeeType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblDesc = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Location = new System.Drawing.Point(197, 148);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(73, 25);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "確定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // cboSurveyeeType
            // 
            this.cboSurveyeeType.DisplayMember = "Text";
            this.cboSurveyeeType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSurveyeeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSurveyeeType.FormattingEnabled = true;
            this.cboSurveyeeType.ItemHeight = 19;
            this.cboSurveyeeType.Location = new System.Drawing.Point(28, 26);
            this.cboSurveyeeType.Name = "cboSurveyeeType";
            this.cboSurveyeeType.Size = new System.Drawing.Size(319, 25);
            this.cboSurveyeeType.TabIndex = 1;
            this.cboSurveyeeType.SelectedIndexChanged += new System.EventHandler(this.cboSurveyeeType_SelectedIndexChanged);
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Location = new System.Drawing.Point(28, 57);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(319, 85);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "<文字說明>";
            this.lblDesc.TextLineAlignment = System.Drawing.StringAlignment.Near;
            this.lblDesc.WordWrap = true;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(276, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // SelectSurveyeeType
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(372, 185);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.cboSurveyeeType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Name = "SelectSurveyeeType";
            this.Text = "選擇問卷對象";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSurveyeeType;
        private DevComponents.DotNetBar.LabelX lblDesc;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}