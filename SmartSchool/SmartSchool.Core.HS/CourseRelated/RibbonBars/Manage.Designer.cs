namespace SmartSchool.CourseRelated.RibbonBars
{
    partial class Manage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manage));
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.btnSaveCourse = new DevComponents.DotNetBar.ButtonItem();
            this.btnDeleteCourse = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddCourse = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddCourse,
            this.itemContainer1});
            this.MainRibbonBar.Text = "編輯";
            // 
            // itemContainer1
            // 
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSaveCourse,
            this.btnDeleteCourse});
            // 
            // btnSaveCourse
            // 
            this.btnSaveCourse.Enabled = false;
            this.btnSaveCourse.Image = ( (System.Drawing.Image)( resources.GetObject("btnSaveCourse.Image") ) );
            this.btnSaveCourse.ImagePaddingHorizontal = 8;
            this.btnSaveCourse.ImagePaddingVertical = 10;
            this.btnSaveCourse.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnSaveCourse.Name = "btnSaveCourse";
            this.btnSaveCourse.Text = "產生課程";
            this.btnSaveCourse.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDeleteCourse
            // 
            this.btnDeleteCourse.Enabled = false;
            this.btnDeleteCourse.Image = ( (System.Drawing.Image)( resources.GetObject("btnDeleteCourse.Image") ) );
            this.btnDeleteCourse.ImagePaddingHorizontal = 8;
            this.btnDeleteCourse.ImagePaddingVertical = 10;
            this.btnDeleteCourse.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnDeleteCourse.Name = "btnDeleteCourse";
            this.btnDeleteCourse.Text = "刪除課程";
            this.btnDeleteCourse.Click += new System.EventHandler(this.btnDeleteCourse_Click);
            // 
            // btnAddCourse
            // 
            this.btnAddCourse.Image = ( (System.Drawing.Image)( resources.GetObject("btnAddCourse.Image") ) );
            this.btnAddCourse.ImagePaddingHorizontal = 8;
            this.btnAddCourse.Name = "btnAddCourse";
            this.btnAddCourse.Text = "新增課程";
            this.btnAddCourse.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "Manage";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ButtonItem btnSaveCourse;
        private DevComponents.DotNetBar.ButtonItem btnDeleteCourse;
        private DevComponents.DotNetBar.ButtonItem btnAddCourse;
    }
}
