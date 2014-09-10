namespace SmartSchool.ClassRelated.RibbonBars
{
    partial class AssignSeatNoPicker
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
            this.cboSeatNo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // cboSeatNo
            // 
            this.cboSeatNo.DisplayMember = "Text";
            this.cboSeatNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSeatNo.FormattingEnabled = true;
            this.cboSeatNo.Location = new System.Drawing.Point(21, 45);
            this.cboSeatNo.Name = "cboSeatNo";
            this.cboSeatNo.Size = new System.Drawing.Size(210, 24);
            this.cboSeatNo.TabIndex = 0;
            this.cboSeatNo.WatermarkText = "請選擇座號";
            this.cboSeatNo.Validating += new System.ComponentModel.CancelEventHandler(this.cboSeatNo_Validating);
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(21, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(199, 26);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "請選擇或輸入新班級座號";
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.Location = new System.Drawing.Point(119, 77);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(52, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "確定";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Location = new System.Drawing.Point(177, 77);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(54, 23);
            this.buttonX2.TabIndex = 3;
            this.buttonX2.Text = "離開";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // AssignSeatNoPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 112);
            this.ControlBox = false;
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cboSeatNo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssignSeatNoPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選擇新班級座號";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssignSeatNoPicker_FormClosing);
            this.Load += new System.EventHandler(this.AssignSeatNoPicker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSeatNo;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private DevComponents.DotNetBar.ButtonX buttonX2;
    }
}