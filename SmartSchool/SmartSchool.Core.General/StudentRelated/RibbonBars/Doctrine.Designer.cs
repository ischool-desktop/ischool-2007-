namespace SmartSchool.StudentRelated.RibbonBars
{
    partial class Doctrine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Doctrine));
            this.btnMerit = new DevComponents.DotNetBar.ButtonItem();
            this.btnAward = new DevComponents.DotNetBar.ButtonItem();
            this.btnDemerit = new DevComponents.DotNetBar.ButtonItem();
            this.btnClearDemerit = new DevComponents.DotNetBar.ButtonItem();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.btnAttendance = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.AutoOverflowEnabled = false;
            this.MainRibbonBar.AutoSizeItems = false;
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.MainRibbonBar.ResizeItemsToFit = false;
            this.MainRibbonBar.Size = new System.Drawing.Size(289, 88);
            this.MainRibbonBar.Text = "學務作業";
            // 
            // btnMerit
            // 
            this.btnMerit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnMerit.Enabled = false;
            this.btnMerit.Image = ( (System.Drawing.Image)( resources.GetObject("btnMerit.Image") ) );
            this.btnMerit.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.btnMerit.ImagePaddingHorizontal = 3;
            this.btnMerit.ImagePaddingVertical = 0;
            this.btnMerit.Name = "btnMerit";
            this.btnMerit.SubItemsExpandWidth = 14;
            this.superTooltip1.SetSuperTooltip(this.btnMerit, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "登錄學生獎勵資料", null, null, DevComponents.DotNetBar.eTooltipColor.System));
            this.btnMerit.Text = "獎勵";
            this.btnMerit.Click += new System.EventHandler(this.bItemMerit_Click);
            // 
            // btnAward
            // 
            this.btnAward.AutoExpandOnClick = true;
            this.btnAward.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAward.Enabled = false;
            this.btnAward.Image = ( (System.Drawing.Image)( resources.GetObject("btnAward.Image") ) );
            this.btnAward.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.btnAward.ImagePaddingHorizontal = 3;
            this.btnAward.ImagePaddingVertical = 0;
            this.btnAward.Name = "btnAward";
            this.btnAward.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnDemerit,
            this.btnClearDemerit});
            this.btnAward.SubItemsExpandWidth = 14;
            this.superTooltip1.SetSuperTooltip(this.btnAward, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "登錄學生懲戒資料", null, null, DevComponents.DotNetBar.eTooltipColor.System));
            this.btnAward.Text = "懲戒";
            // 
            // btnDemerit
            // 
            this.btnDemerit.ImagePaddingHorizontal = 8;
            this.btnDemerit.Name = "btnDemerit";
            this.btnDemerit.Text = "懲戒";
            this.btnDemerit.Click += new System.EventHandler(this.btnDemerit_Click);
            // 
            // btnClearDemerit
            // 
            this.btnClearDemerit.ImagePaddingHorizontal = 8;
            this.btnClearDemerit.Name = "btnClearDemerit";
            this.btnClearDemerit.Text = "銷過";
            this.btnClearDemerit.Click += new System.EventHandler(this.btnClearDemerit_Click);
            // 
            // superTooltip1
            // 
            this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // btnAttendance
            // 
            this.btnAttendance.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAttendance.Enabled = false;
            this.btnAttendance.Image = ( (System.Drawing.Image)( resources.GetObject("btnAttendance.Image") ) );
            this.btnAttendance.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.btnAttendance.ImagePaddingHorizontal = 3;
            this.btnAttendance.ImagePaddingVertical = 0;
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.SubItemsExpandWidth = 14;
            this.superTooltip1.SetSuperTooltip(this.btnAttendance, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "按此編輯學生缺曠紀錄", null, null, DevComponents.DotNetBar.eTooltipColor.System));
            this.btnAttendance.Text = "缺曠";
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // itemContainer1
            // 
            this.itemContainer1.ItemSpacing = 0;
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.MultiLine = true;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.ResizeItemsToFit = false;
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnMerit,
            this.btnAward,
            this.btnAttendance});
            // 
            // Award
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Award";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnMerit;
        private DevComponents.DotNetBar.ButtonItem btnAward;
        private DevComponents.DotNetBar.ButtonItem btnDemerit;
        private DevComponents.DotNetBar.ButtonItem btnClearDemerit;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private DevComponents.DotNetBar.ButtonItem btnAttendance;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
    }
}
