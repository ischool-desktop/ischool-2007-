namespace SmartSchool.ClassRelated.RibbonBars
{
    partial class History
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(History));
            this.btnHistory = new DevComponents.DotNetBar.ButtonItem();
            this.btnSurvey = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnHistory,
            this.btnSurvey});
            this.MainRibbonBar.Text = "�䥦";
            // 
            // btnHistory
            // 
            this.btnHistory.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnHistory.Enabled = false;
            this.btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnHistory.Image")));
            this.btnHistory.ImagePaddingHorizontal = 8;
            this.btnHistory.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.SubItemsExpandWidth = 14;
            this.btnHistory.Text = "�ק���{";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnSurvey
            // 
            this.btnSurvey.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSurvey.Image = ((System.Drawing.Image)(resources.GetObject("btnSurvey.Image")));
            this.btnSurvey.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.btnSurvey.ImagePaddingHorizontal = 8;
            this.btnSurvey.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnSurvey.Name = "btnSurvey";
            this.btnSurvey.SubItemsExpandWidth = 14;
            this.btnSurvey.Text = "�ݨ�";
            this.btnSurvey.Click += new System.EventHandler(this.btnSurvey_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "History";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnHistory;
        private DevComponents.DotNetBar.ButtonItem btnSurvey;
    }
}
