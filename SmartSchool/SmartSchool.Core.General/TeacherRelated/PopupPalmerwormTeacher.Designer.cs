namespace SmartSchool.TeacherRelated
{
    partial class PopupPalmerwormTeacher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupPalmerwormTeacher));
            this.palmerwormTeacher1 = new SmartSchool.TeacherRelated.PalmerwormTeacher();
            this.SuspendLayout();
            // 
            // palmerwormTeacher1
            // 
            this.palmerwormTeacher1.BackColor = System.Drawing.Color.Transparent;
            this.palmerwormTeacher1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palmerwormTeacher1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.palmerwormTeacher1.Location = new System.Drawing.Point(0, 0);
            this.palmerwormTeacher1.Manager = null;
            this.palmerwormTeacher1.Name = "palmerwormTeacher1";
            this.palmerwormTeacher1.Size = new System.Drawing.Size(584, 554);
            this.palmerwormTeacher1.TabIndex = 0;
            this.palmerwormTeacher1.TeacherInfo = null;
            // 
            // PopupPalmerwormTeacher
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(584, 554);
            this.Controls.Add(this.palmerwormTeacher1);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject("$this.Icon") ) );
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "PopupPalmerwormTeacher";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "PopupPalmerwormTeacher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PopupPalmerwormTeacher_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private PalmerwormTeacher palmerwormTeacher1;
    }
}