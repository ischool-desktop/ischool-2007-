namespace SmartSchool.TeacherRelated.RibbonBars
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
            this.buttonItem114 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem114});
            this.MainRibbonBar.Text = "�έp����";
            // 
            // buttonItem114
            // 
            this.buttonItem114.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem114.Enabled = false;
            this.buttonItem114.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem114.Image") ) );
            this.buttonItem114.ImagePaddingHorizontal = 8;
            this.buttonItem114.Name = "buttonItem114";
            this.buttonItem114.SubItemsExpandWidth = 14;
            this.buttonItem114.Text = "����";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "Report";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem114;
    }
}
