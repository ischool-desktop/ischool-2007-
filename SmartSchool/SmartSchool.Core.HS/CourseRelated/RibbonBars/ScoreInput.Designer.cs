namespace SmartSchool.CourseRelated.RibbonBars
{
    partial class ScoreInput
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
            this.MainRibbonBar.Text = "成績作業";
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
            this.btnEditScore.Text = "成績輸入";
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
            this.btnCalculate.Text = "成績計算";
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
