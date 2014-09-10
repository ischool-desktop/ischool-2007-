namespace SmartSchool.ClassRelated.RibbonBars
{
    partial class Manage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manage));
            this.btnAddClass = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer12 = new DevComponents.DotNetBar.ItemContainer();
            this.btnSaveClass = new DevComponents.DotNetBar.ButtonItem();
            this.btnDeleteClass = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddClass,
            this.itemContainer12});
            this.MainRibbonBar.Text = "編輯";
            // 
            // btnAddClass
            // 
            this.btnAddClass.Image = ( (System.Drawing.Image)( resources.GetObject("btnAddClass.Image") ) );
            this.btnAddClass.ImagePaddingHorizontal = 8;
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.SubItemsExpandWidth = 14;
            this.btnAddClass.Text = "新增";
            this.btnAddClass.Click += new System.EventHandler(this.buttonItem14_Click);
            // 
            // itemContainer12
            // 
            this.itemContainer12.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer12.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer12.Name = "itemContainer12";
            this.itemContainer12.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSaveClass,
            this.btnDeleteClass});
            // 
            // btnSaveClass
            // 
            this.btnSaveClass.Enabled = false;
            this.btnSaveClass.Image = ( (System.Drawing.Image)( resources.GetObject("btnSaveClass.Image") ) );
            this.btnSaveClass.ImagePaddingHorizontal = 3;
            this.btnSaveClass.ImagePaddingVertical = 10;
            this.btnSaveClass.Name = "btnSaveClass";
            this.btnSaveClass.Text = "儲存";
            this.btnSaveClass.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDeleteClass
            // 
            this.btnDeleteClass.Enabled = false;
            this.btnDeleteClass.Image = ( (System.Drawing.Image)( resources.GetObject("btnDeleteClass.Image") ) );
            this.btnDeleteClass.ImagePaddingHorizontal = 3;
            this.btnDeleteClass.ImagePaddingVertical = 10;
            this.btnDeleteClass.Name = "btnDeleteClass";
            this.btnDeleteClass.Text = "刪除";
            this.btnDeleteClass.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Manage";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ItemContainer itemContainer12;
        private DevComponents.DotNetBar.ButtonItem btnAddClass;
        private DevComponents.DotNetBar.ButtonItem btnSaveClass;
        private DevComponents.DotNetBar.ButtonItem btnDeleteClass;

    }
}
