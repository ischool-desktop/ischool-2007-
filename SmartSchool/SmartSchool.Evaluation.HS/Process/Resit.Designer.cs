namespace SmartSchool.Evaluation.Process
{
    partial class Resit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resit));
            this.buttonItem89 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem89});
            this.MainRibbonBar.Location = new System.Drawing.Point(4, 4);
            this.MainRibbonBar.Margin = new System.Windows.Forms.Padding(4);
            this.MainRibbonBar.Size = new System.Drawing.Size(348, 147);
            this.MainRibbonBar.Text = "�ɦҳB�z";
            // 
            // buttonItem89
            // 
            this.buttonItem89.AutoExpandOnClick = true;
            this.buttonItem89.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem89.CanCustomize = false;
            this.buttonItem89.GlobalItem = false;
            this.buttonItem89.Image = ( (System.Drawing.Image)( resources.GetObject("buttonItem89.Image") ) );
            this.buttonItem89.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.buttonItem89.ImagePaddingHorizontal = 3;
            this.buttonItem89.ImagePaddingVertical = 10;
            this.buttonItem89.Name = "buttonItem89";
            this.buttonItem89.StopPulseOnMouseOver = false;
            this.buttonItem89.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2,
            this.buttonItem1,
            this.buttonItem3});
            this.buttonItem89.SubItemsExpandWidth = 14;
            this.buttonItem89.Text = "�ɦҳB�z";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "�ɦҦW��-�̬��";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "�ɦҦW��-�̾ǥ�";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "�ɦҦ��Z�פJ��";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // Resit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Resit";
            this.Size = new System.Drawing.Size(389, 204);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem89;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;

    }
}
