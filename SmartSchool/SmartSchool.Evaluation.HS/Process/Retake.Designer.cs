namespace SmartSchool.Evaluation.Process
{
    partial class Retake
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Retake));
            this.buttonItem52 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem52});
            this.MainRibbonBar.Location = new System.Drawing.Point(4, 4);
            this.MainRibbonBar.Margin = new System.Windows.Forms.Padding(4);
            this.MainRibbonBar.Size = new System.Drawing.Size(348, 147);
            this.MainRibbonBar.Text = "�ɦҳB�z";
            // 
            // buttonItem52
            // 
            this.buttonItem52.AutoExpandOnClick = true;
            this.buttonItem52.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem52.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem52.Image") ) );
            this.buttonItem52.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.buttonItem52.ImagePaddingHorizontal = 3;
            this.buttonItem52.ImagePaddingVertical = 10;
            this.buttonItem52.Name = "buttonItem52";
            this.buttonItem52.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem3});
            this.buttonItem52.SubItemsExpandWidth = 14;
            this.buttonItem52.Text = "���׳B�z";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "��ĳ���צW��-�̬��";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "��ĳ���צW��-�̾ǥ�";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "���צ��Z�פJ��";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // Retake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Retake";
            this.Size = new System.Drawing.Size(389, 204);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem52;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;


    }
}
