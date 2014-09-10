namespace SmartSchool.CourseRelated.RibbonBars.OtherTab
{
    partial class AssignTeacherTeachCourse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignTeacherTeachCourse));
            this.btnAssign = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem66 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem67 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem68 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem69 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem70 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAssign});
            this.MainRibbonBar.Text = "指定";
            // 
            // btnAssign
            // 
            this.btnAssign.AutoExpandOnClick = true;
            this.btnAssign.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAssign.CanCustomize = false;
            this.btnAssign.Image = ( (System.Drawing.Image)( resources.GetObject("btnAssign.Image") ) );
            this.btnAssign.ImagePaddingHorizontal = 8;
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem66,
            this.buttonItem67,
            this.buttonItem68,
            this.buttonItem69,
            this.buttonItem70});
            this.btnAssign.SubItemsExpandWidth = 14;
            this.btnAssign.Text = "授課";
            this.btnAssign.PopupShowing += new System.EventHandler(this.btnAssign_PopupShowing);
            // 
            // buttonItem66
            // 
            this.buttonItem66.ImagePaddingHorizontal = 8;
            this.buttonItem66.Name = "buttonItem66";
            this.buttonItem66.Text = "基本電學Ⅰ";
            // 
            // buttonItem67
            // 
            this.buttonItem67.ImagePaddingHorizontal = 8;
            this.buttonItem67.Name = "buttonItem67";
            this.buttonItem67.Text = "電子學Ⅰ";
            // 
            // buttonItem68
            // 
            this.buttonItem68.ImagePaddingHorizontal = 8;
            this.buttonItem68.Name = "buttonItem68";
            this.buttonItem68.Text = "程式設計Ⅰ";
            // 
            // buttonItem69
            // 
            this.buttonItem69.ImagePaddingHorizontal = 8;
            this.buttonItem69.Name = "buttonItem69";
            this.buttonItem69.Text = "電子學實習Ⅰ";
            // 
            // buttonItem70
            // 
            this.buttonItem70.ImagePaddingHorizontal = 8;
            this.buttonItem70.Name = "buttonItem70";
            this.buttonItem70.Text = "單晶片控制實習Ⅰ";
            // 
            // Attend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Attend";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnAssign;
        private DevComponents.DotNetBar.ButtonItem buttonItem66;
        private DevComponents.DotNetBar.ButtonItem buttonItem67;
        private DevComponents.DotNetBar.ButtonItem buttonItem68;
        private DevComponents.DotNetBar.ButtonItem buttonItem69;
        private DevComponents.DotNetBar.ButtonItem buttonItem70;

    }
}
