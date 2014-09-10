namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    partial class ImportExport
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExport));
            this.btnImport = new DevComponents.DotNetBar.ButtonItem();
            this.btnExport = new DevComponents.DotNetBar.ButtonItem();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.galleryContainer1 = new DevComponents.DotNetBar.GalleryContainer();
            this.btnImportPhoto = new DevComponents.DotNetBar.ButtonItem();
            this.btnExportList = new DevComponents.DotNetBar.ButtonItem();
            this.btnImportList = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.AutoSizeItems = false;
            this.MainRibbonBar.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.galleryContainer1,
            this.btnExportList,
            this.btnImportList});
            this.MainRibbonBar.Location = new System.Drawing.Point(20, 3);
            this.MainRibbonBar.OverflowButtonImage = ( (System.Drawing.Image)( resources.GetObject("MainRibbonBar.OverflowButtonImage") ) );
            this.MainRibbonBar.ResizeOrderIndex = 55;
            this.MainRibbonBar.Size = new System.Drawing.Size(269, 99);
            this.MainRibbonBar.Text = "匯出/匯入";
            // 
            // 
            // 
            this.MainRibbonBar.TitleStyle.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.MainRibbonBar.TitleStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            // 
            // 
            // 
            this.MainRibbonBar.TitleStyleMouseOver.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F);
            this.MainRibbonBar.TitleStyleMouseOver.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.MainRibbonBar.ItemClick += new System.EventHandler(this.MainRibbonBar_ItemClick);
            // 
            // btnImport
            // 
            this.btnImport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnImport.FixedSize = new System.Drawing.Size(120, 23);
            this.btnImport.Image = ( (System.Drawing.Image)( resources.GetObject("btnImport.Image") ) );
            this.btnImport.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnImport.ImagePaddingHorizontal = 0;
            this.btnImport.ImagePaddingVertical = 0;
            this.btnImport.Name = "btnImport";
            this.btnImport.RibbonWordWrap = false;
            this.btnImport.ShowSubItems = false;
            this.btnImport.SubItemsExpandWidth = 140;
            this.btnImport.Text = "匯入學籍資料";
            this.btnImport.Tooltip = "匯入學籍資料";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnExport.FixedSize = new System.Drawing.Size(120, 23);
            this.btnExport.Image = ( (System.Drawing.Image)( resources.GetObject("btnExport.Image") ) );
            this.btnExport.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnExport.ImagePaddingHorizontal = 0;
            this.btnExport.ImagePaddingVertical = 0;
            this.btnExport.Name = "btnExport";
            this.btnExport.RibbonWordWrap = false;
            this.btnExport.ShowSubItems = false;
            this.btnExport.SubItemsExpandWidth = 140;
            this.btnExport.Text = "匯出學籍資料";
            this.btnExport.Tooltip = "匯出學籍資料";
            this.btnExport.Click += new System.EventHandler(this.buttonItem88_Click);
            // 
            // superTooltip1
            // 
            this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // galleryContainer1
            // 
            this.galleryContainer1.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.galleryContainer1.CanCustomize = false;
            this.galleryContainer1.DefaultSize = new System.Drawing.Size(10, 71);
            this.galleryContainer1.MinimumSize = new System.Drawing.Size(10, 71);
            this.galleryContainer1.Name = "galleryContainer1";
            this.galleryContainer1.PopupGallerySize = new System.Drawing.Size(320, 480);
            this.galleryContainer1.StretchGallery = true;
            this.galleryContainer1.Visible = false;
            // 
            // btnImportPhoto
            // 
            this.btnImportPhoto.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnImportPhoto.FixedSize = new System.Drawing.Size(120, 23);
            this.btnImportPhoto.Image = ( (System.Drawing.Image)( resources.GetObject("btnImportPhoto.Image") ) );
            this.btnImportPhoto.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnImportPhoto.ImagePaddingHorizontal = 0;
            this.btnImportPhoto.ImagePaddingVertical = 0;
            this.btnImportPhoto.Name = "btnImportPhoto";
            this.btnImportPhoto.SubItemsExpandWidth = 14;
            this.btnImportPhoto.Text = "匯入照片";
            this.btnImportPhoto.Tooltip = "批次匯入照片";
            this.btnImportPhoto.Click += new System.EventHandler(this.buttonItem1_Click_1);
            // 
            // btnExportList
            // 
            this.btnExportList.AutoExpandOnClick = true;
            this.btnExportList.Image = ( (System.Drawing.Image)( resources.GetObject("btnExportList.Image") ) );
            this.btnExportList.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.btnExportList.ImagePaddingHorizontal = 0;
            this.btnExportList.ImagePaddingVertical = 0;
            this.btnExportList.Name = "btnExportList";
            this.btnExportList.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnExport});
            this.btnExportList.SubItemsExpandWidth = 14;
            this.btnExportList.Text = "匯出";
            this.btnExportList.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btnExportList_PopupOpen);
            // 
            // btnImportList
            // 
            this.btnImportList.AutoExpandOnClick = true;
            this.btnImportList.Image = ( (System.Drawing.Image)( resources.GetObject("btnImportList.Image") ) );
            this.btnImportList.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.btnImportList.ImagePaddingHorizontal = 0;
            this.btnImportList.ImagePaddingVertical = 0;
            this.btnImportList.Name = "btnImportList";
            this.btnImportList.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnImport,
            this.buttonItem4,
            this.btnImportPhoto});
            this.btnImportList.SubItemsExpandWidth = 14;
            this.btnImportList.Text = "匯入";
            this.btnImportList.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btnImportList_PopupOpen);
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.Enabled = false;
            this.buttonItem4.FixedSize = new System.Drawing.Size(120, 23);
            this.buttonItem4.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.buttonItem4.ImagePaddingHorizontal = 0;
            this.buttonItem4.ImagePaddingVertical = 0;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "匯入學籍異動";
            // 
            // ImportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ImportExport";
            this.Size = new System.Drawing.Size(394, 102);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnImport;
        private DevComponents.DotNetBar.ButtonItem btnExport;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private DevComponents.DotNetBar.GalleryContainer galleryContainer1;
        private DevComponents.DotNetBar.ButtonItem btnImportPhoto;
        private DevComponents.DotNetBar.ButtonItem btnExportList;
        private DevComponents.DotNetBar.ButtonItem btnImportList;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
    }
}
