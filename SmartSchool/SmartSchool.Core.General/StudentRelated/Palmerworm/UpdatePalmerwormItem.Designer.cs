namespace SmartSchool.StudentRelated.Palmerworm
{
    partial class UpdatePalmerwormItem
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
            this.btnRemove = new DevComponents.DotNetBar.ButtonX();
            this.bthUpdate = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.lstRecord = new SmartSchool.Common.ListViewEX();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRemove
            // 
            this.btnRemove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRemove.Location = new System.Drawing.Point(193, 201);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 25);
            this.btnRemove.TabIndex = 11;
            this.btnRemove.Text = "刪除";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // bthUpdate
            // 
            this.bthUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bthUpdate.Location = new System.Drawing.Point(112, 201);
            this.bthUpdate.Name = "bthUpdate";
            this.bthUpdate.Size = new System.Drawing.Size(75, 25);
            this.bthUpdate.TabIndex = 10;
            this.bthUpdate.Text = "修改";
            this.bthUpdate.Click += new System.EventHandler(this.bthUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(31, 201);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstRecord
            // 
            // 
            // 
            // 
            this.lstRecord.Border.Class = "ListViewBorder";
            this.lstRecord.FullRowSelect = true;
            this.lstRecord.Location = new System.Drawing.Point(31, 7);
            this.lstRecord.MultiSelect = false;
            this.lstRecord.Name = "lstRecord";
            this.lstRecord.Size = new System.Drawing.Size(483, 186);
            this.lstRecord.TabIndex = 13;
            this.lstRecord.UseCompatibleStateImageBehavior = false;
            this.lstRecord.View = System.Windows.Forms.View.Details;
            // 
            // UpdatePalmerwormItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstRecord);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.bthUpdate);
            this.Controls.Add(this.btnAdd);
            this.Name = "UpdatePalmerwormItem";
            this.Size = new System.Drawing.Size(550, 236);
            this.DoubleClick += new System.EventHandler(this.UpdatePalmerwormItem_DoubleClick);
            this.Controls.SetChildIndex(this.picWaiting, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.bthUpdate, 0);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.Controls.SetChildIndex(this.lstRecord, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnRemove;
        private DevComponents.DotNetBar.ButtonX bthUpdate;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private SmartSchool.Common.ListViewEX lstRecord;

    }
}
