namespace SmartSchool.CourseRelated.RibbonBars
{
    partial class Manage
    {
        /// <summary>
        /// �]�p�u��һݪ��ܼơC
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// �M������ϥΤ����귽�C
        /// </summary>
        /// <param name="disposing">�p�G���Ӥ��} Managed �귽�h�� true�A�_�h�� false�C</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form �]�p�u�㲣�ͪ��{���X

        /// <summary>
        /// �����]�p�u��䴩�һݪ���k - �ФŨϥε{���X�s�边�ק�o�Ӥ�k�����e�C
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
            this.MainRibbonBar.Text = "�s��";
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
            this.btnSaveCourse.Text = "���ͽҵ{";
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
            this.btnDeleteCourse.Text = "�R���ҵ{";
            this.btnDeleteCourse.Click += new System.EventHandler(this.btnDeleteCourse_Click);
            // 
            // btnAddCourse
            // 
            this.btnAddCourse.Image = ( (System.Drawing.Image)( resources.GetObject("btnAddCourse.Image") ) );
            this.btnAddCourse.ImagePaddingHorizontal = 8;
            this.btnAddCourse.Name = "btnAddCourse";
            this.btnAddCourse.Text = "�s�W�ҵ{";
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
