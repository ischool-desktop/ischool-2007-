namespace SmartSchool.TeacherRelated.RibbonBars
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
            this.btnAddTeacher = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer13 = new DevComponents.DotNetBar.ItemContainer();
            this.btnSaveTeacher = new DevComponents.DotNetBar.ButtonItem();
            this.btnDeleteTeacher = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddTeacher,
            this.itemContainer13});
            this.MainRibbonBar.TabStop = false;
            this.MainRibbonBar.Text = "�s��";
            // 
            // btnAddTeacher
            // 
            this.btnAddTeacher.Image = ( (System.Drawing.Image)( resources.GetObject("btnAddTeacher.Image") ) );
            this.btnAddTeacher.ImagePaddingHorizontal = 8;
            this.btnAddTeacher.Name = "btnAddTeacher";
            this.btnAddTeacher.SubItemsExpandWidth = 14;
            this.btnAddTeacher.Text = "�s�W";
            this.btnAddTeacher.Click += new System.EventHandler(this.buttonItem83_Click);
            // 
            // itemContainer13
            // 
            this.itemContainer13.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer13.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer13.Name = "itemContainer13";
            this.itemContainer13.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSaveTeacher,
            this.btnDeleteTeacher});
            // 
            // btnSaveTeacher
            // 
            this.btnSaveTeacher.Enabled = false;
            this.btnSaveTeacher.Image = ( (System.Drawing.Image)( resources.GetObject("btnSaveTeacher.Image") ) );
            this.btnSaveTeacher.ImagePaddingHorizontal = 8;
            this.btnSaveTeacher.ImagePaddingVertical = 10;
            this.btnSaveTeacher.Name = "btnSaveTeacher";
            this.btnSaveTeacher.Text = "�x�s";
            this.btnSaveTeacher.Click += new System.EventHandler(this.buttonItem84_Click);
            // 
            // btnDeleteTeacher
            // 
            this.btnDeleteTeacher.Enabled = false;
            this.btnDeleteTeacher.Image = ( (System.Drawing.Image)( resources.GetObject("btnDeleteTeacher.Image") ) );
            this.btnDeleteTeacher.ImagePaddingHorizontal = 8;
            this.btnDeleteTeacher.ImagePaddingVertical = 10;
            this.btnDeleteTeacher.Name = "btnDeleteTeacher";
            this.btnDeleteTeacher.Text = "�R��";
            this.btnDeleteTeacher.Click += new System.EventHandler(this.buttonItem85_Click);
            // 
            // Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "Manage";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnAddTeacher;
        private DevComponents.DotNetBar.ItemContainer itemContainer13;
        private DevComponents.DotNetBar.ButtonItem btnSaveTeacher;
        private DevComponents.DotNetBar.ButtonItem btnDeleteTeacher;
    }
}
