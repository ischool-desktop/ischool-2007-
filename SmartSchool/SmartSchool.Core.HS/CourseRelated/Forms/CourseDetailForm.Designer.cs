namespace SmartSchool.CourseRelated.Forms
{
    partial class CourseDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CourseDetailForm));
            this.courseDetailPane1 = new SmartSchool.CourseRelated.CourseDetailPane();
            this.SuspendLayout();
            // 
            // courseDetailPane1
            // 
            this.courseDetailPane1.BackColor = System.Drawing.Color.Transparent;
            this.courseDetailPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.courseDetailPane1.Location = new System.Drawing.Point(0, 0);
            this.courseDetailPane1.Manager = null;
            this.courseDetailPane1.Name = "courseDetailPane1";
            this.courseDetailPane1.Size = new System.Drawing.Size(584, 554);
            this.courseDetailPane1.TabIndex = 0;
            // 
            // CourseDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(584, 554);
            this.Controls.Add(this.courseDetailPane1);
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject("$this.Icon") ) );
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "CourseDetailForm";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "CourseDetailForm";
            this.Load += new System.EventHandler(this.CourseDetailForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CourseDetailPane courseDetailPane1;

        //private CourseDetailPane courseDetailPane1;
    }
}