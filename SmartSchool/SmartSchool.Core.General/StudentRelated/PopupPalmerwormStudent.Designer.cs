namespace SmartSchool.StudentRelated
{
    partial class PopupPalmerwormStudent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupPalmerwormStudent));
            this.palmerwormStudent1 = new SmartSchool.StudentRelated.PalmerwormStudent();
            this.SuspendLayout();
            // 
            // palmerwormStudent1
            // 
            this.palmerwormStudent1.BackColor = System.Drawing.Color.Transparent;
            this.palmerwormStudent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palmerwormStudent1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.palmerwormStudent1.Location = new System.Drawing.Point(0, 0);
            this.palmerwormStudent1.Manager = null;
            this.palmerwormStudent1.Margin = new System.Windows.Forms.Padding(4);
            this.palmerwormStudent1.Name = "palmerwormStudent1";
            this.palmerwormStudent1.Size = new System.Drawing.Size(584, 554);
            this.palmerwormStudent1.StudentInfo = null;
            this.palmerwormStudent1.TabIndex = 0;
            // 
            // PopupPalmerwormStudent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(584, 554);
            this.Controls.Add(this.palmerwormStudent1);
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject("$this.Icon") ) );
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "PopupPalmerwormStudent";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "學生資料";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PopupPalmerwormStudent_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private PalmerwormStudent palmerwormStudent1;
    }
}