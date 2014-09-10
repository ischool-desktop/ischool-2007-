namespace SmartSchool.StudentRelated.RibbonBars
{
    partial class Others
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
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Others));
            this.btnMap = new DevComponents.DotNetBar.ButtonItem();
            this.btnPerm = new DevComponents.DotNetBar.ButtonItem();
            this.btnConn = new DevComponents.DotNetBar.ButtonItem();
            this.btnOther = new DevComponents.DotNetBar.ButtonItem();
            this.btnQueryCoor = new DevComponents.DotNetBar.ButtonItem();
            this.btnHistory = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.AutoSizeItems = false;
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1,
            this.btnHistory});
            this.MainRibbonBar.Location = new System.Drawing.Point(4, 4);
            this.MainRibbonBar.Margin = new System.Windows.Forms.Padding(4);
            this.MainRibbonBar.OverflowButtonImage = ( (System.Drawing.Image)( resources.GetObject("MainRibbonBar.OverflowButtonImage") ) );
            this.MainRibbonBar.ResizeOrderIndex = 500;
            this.MainRibbonBar.Size = new System.Drawing.Size(381, 96);
            this.MainRibbonBar.Text = "其它";
            // 
            // btnMap
            // 
            this.btnMap.AutoExpandOnClick = true;
            this.btnMap.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnMap.Enabled = false;
            this.btnMap.Image = ( (System.Drawing.Image)( resources.GetObject("btnMap.Image") ) );
            this.btnMap.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.btnMap.ImagePaddingHorizontal = 3;
            this.btnMap.ImagePaddingVertical = 0;
            this.btnMap.Name = "btnMap";
            this.btnMap.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnPerm,
            this.btnConn,
            this.btnOther});
            this.btnMap.SubItemsExpandWidth = 14;
            this.btnMap.Text = "學生分佈圖";
            // 
            // btnPerm
            // 
            this.btnPerm.ImagePaddingHorizontal = 8;
            this.btnPerm.Name = "btnPerm";
            this.btnPerm.Text = "戶籍地址";
            this.btnPerm.Click += new System.EventHandler(this.btnPerm_Click);
            // 
            // btnConn
            // 
            this.btnConn.ImagePaddingHorizontal = 8;
            this.btnConn.Name = "btnConn";
            this.btnConn.Text = "聯絡地址";
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // btnOther
            // 
            this.btnOther.ImagePaddingHorizontal = 8;
            this.btnOther.Name = "btnOther";
            this.btnOther.Text = "其他地址";
            this.btnOther.Click += new System.EventHandler(this.btnOther_Click);
            // 
            // btnQueryCoor
            // 
            this.btnQueryCoor.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnQueryCoor.Enabled = false;
            this.btnQueryCoor.Image = ( (System.Drawing.Image)( resources.GetObject("btnQueryCoor.Image") ) );
            this.btnQueryCoor.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.btnQueryCoor.ImagePaddingHorizontal = 3;
            this.btnQueryCoor.ImagePaddingVertical = 0;
            this.btnQueryCoor.Name = "btnQueryCoor";
            this.btnQueryCoor.SubItemsExpandWidth = 14;
            this.btnQueryCoor.Text = "查詢經緯度";
            this.btnQueryCoor.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnHistory.Enabled = false;
            this.btnHistory.FixedSize = new System.Drawing.Size(62, 68);
            this.btnHistory.Image = ( (System.Drawing.Image)( resources.GetObject("btnHistory.Image") ) );
            this.btnHistory.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.btnHistory.ImagePaddingHorizontal = 5;
            this.btnHistory.ImagePaddingVertical = 0;
            this.btnHistory.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.SubItemsExpandWidth = 14;
            this.btnHistory.Text = "修改歷程";
            this.btnHistory.Click += new System.EventHandler(this.buttonItem51_Click);
            // 
            // itemContainer1
            // 
            this.itemContainer1.CanCustomize = false;
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.MultiLine = true;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.ResizeItemsToFit = false;
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnQueryCoor,
            this.btnMap});
            // 
            // Others
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Others";
            this.Size = new System.Drawing.Size(389, 204);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnMap;
        private DevComponents.DotNetBar.ButtonItem btnPerm;
        private DevComponents.DotNetBar.ButtonItem btnConn;
        private DevComponents.DotNetBar.ButtonItem btnOther;
        private DevComponents.DotNetBar.ButtonItem btnQueryCoor;
        private DevComponents.DotNetBar.ButtonItem btnHistory;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;

    }
}
