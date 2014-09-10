namespace SmartSchool.ClassRelated.RibbonBars
{
    partial class AssignConfirm
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
            this.btnKeep = new DevComponents.DotNetBar.ButtonX();
            this.btnDrop = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnKeep
            // 
            this.btnKeep.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeep.Location = new System.Drawing.Point(13, 39);
            this.btnKeep.Name = "btnKeep";
            this.btnKeep.Size = new System.Drawing.Size(139, 23);
            this.btnKeep.TabIndex = 0;
            this.btnKeep.Text = "保留學生原有座號";
            this.btnKeep.Click += new System.EventHandler(this.btnKeep_Click);
            // 
            // btnDrop
            // 
            this.btnDrop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDrop.Location = new System.Drawing.Point(158, 39);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(132, 23);
            this.btnDrop.TabIndex = 1;
            this.btnDrop.Text = "清除學生原有座號";
            this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(296, 39);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(52, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(13, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(335, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "請決定學生座號處理方式";
            // 
            // AssignConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 74);
            this.ControlBox = false;
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDrop);
            this.Controls.Add(this.btnKeep);
            this.Name = "AssignConfirm";
            this.ShowIcon = false;
            this.Text = "確認";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnKeep;
        private DevComponents.DotNetBar.ButtonX btnDrop;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}