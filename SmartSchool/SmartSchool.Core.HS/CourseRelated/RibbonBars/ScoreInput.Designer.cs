namespace SmartSchool.CourseRelated.RibbonBars
{
    partial class ScoreInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreInput));
            this.btnEditScore = new DevComponents.DotNetBar.ButtonItem();
            this.btnCalculate = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.MainRibbonBar.Text = "���Z�@�~";
            // 
            // btnEditScore
            // 
            this.btnEditScore.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnEditScore.Enabled = false;
            this.btnEditScore.Image = ( (System.Drawing.Image)( resources.GetObject("btnEditScore.Image") ) );
            this.btnEditScore.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.btnEditScore.ImagePaddingHorizontal = 3;
            this.btnEditScore.ImagePaddingVertical = 10;
            this.btnEditScore.Name = "btnEditScore";
            this.btnEditScore.SubItemsExpandWidth = 14;
            this.btnEditScore.Text = "���Z��J";
            this.btnEditScore.Click += new System.EventHandler(this.btnEditScore_Click);
            // 
            // btnCalcuate
            // 
            this.btnCalculate.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnCalculate.Enabled = false;
            this.btnCalculate.Image = ( (System.Drawing.Image)( resources.GetObject("btnCalcuate.Image") ) );
            this.btnCalculate.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.btnCalculate.ImagePaddingHorizontal = 3;
            this.btnCalculate.ImagePaddingVertical = 10;
            this.btnCalculate.Name = "btnCalcuate";
            this.btnCalculate.SubItemsExpandWidth = 14;
            this.btnCalculate.Text = "���Z�p��";
            this.btnCalculate.Click += new System.EventHandler(this.btnCalcuate_Click);
            // 
            // itemContainer1
            // 
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnEditScore,
            this.btnCalculate});
            // 
            // ScoreInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ScoreInput";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnEditScore;
        private DevComponents.DotNetBar.ButtonItem btnCalculate;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
    }
}
