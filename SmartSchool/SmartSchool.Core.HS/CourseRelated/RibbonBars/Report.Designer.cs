namespace SmartSchool.CourseRelated.RibbonBars
{
    partial class Report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report));
            this.buttonItem99 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem99});
            this.MainRibbonBar.Text = "�έp����";
            // 
            // buttonItem99
            // 
            this.buttonItem99.AutoExpandOnClick = true;
            this.buttonItem99.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem99.Enabled = false;
            this.buttonItem99.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem99.Image") ) );
            this.buttonItem99.ImagePaddingHorizontal = 8;
            this.buttonItem99.Name = "buttonItem99";
            this.buttonItem99.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.buttonItem99.SubItemsExpandWidth = 14;
            this.buttonItem99.Text = "����";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "�ҵ{�׽ҾǥͲM��";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "Report";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem99;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;

    }
}
