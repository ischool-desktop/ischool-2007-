namespace SmartSchool.Others.RibbonBars
{
    partial class ScoreOpenTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreOpenTime));
            this.btnSetup = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSetup});
            this.MainRibbonBar.Text = "�䥦";
            // 
            // btnSetup
            // 
            this.btnSetup.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSetup.Image = ( (System.Drawing.Image)( resources.GetObject("btnSetup.Image") ) );
            this.btnSetup.ImagePaddingHorizontal = 8;
            this.btnSetup.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.SubItemsExpandWidth = 14;
            this.btnSetup.Text = "�}��ɶ��]�w";
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // ScoreOpenTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ScoreOpenTime";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnSetup;

    }
}
