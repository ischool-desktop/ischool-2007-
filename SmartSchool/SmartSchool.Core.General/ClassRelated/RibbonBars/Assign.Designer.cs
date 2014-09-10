namespace SmartSchool.ClassRelated.RibbonBars
{
    partial class Assign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Assign));
            this.btnAssignStudent = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem124 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem125 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem126 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem127 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem128 = new DevComponents.DotNetBar.ButtonItem();
            this.btnPlan = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.btnCalcRule = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem71 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem72 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem73 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem74 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem75 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem76 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer2});
            this.MainRibbonBar.Size = new System.Drawing.Size(261, 141);
            this.MainRibbonBar.Text = "指定";
            // 
            // btnAssignStudent
            // 
            this.btnAssignStudent.AutoExpandOnClick = true;
            this.btnAssignStudent.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAssignStudent.Image = ((System.Drawing.Image)(resources.GetObject("btnAssignStudent.Image")));
            this.btnAssignStudent.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnAssignStudent.ImagePaddingHorizontal = 3;
            this.btnAssignStudent.ImagePaddingVertical = 3;
            this.btnAssignStudent.Name = "btnAssignStudent";
            this.btnAssignStudent.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem124,
            this.buttonItem125,
            this.buttonItem126,
            this.buttonItem127,
            this.buttonItem128});
            this.btnAssignStudent.Text = "學生";
            this.btnAssignStudent.PopupShowing += new System.EventHandler(this.btnAssignStudent_PopupShowing);
            // 
            // buttonItem124
            // 
            this.buttonItem124.ImagePaddingHorizontal = 8;
            this.buttonItem124.Name = "buttonItem124";
            this.buttonItem124.Text = "陳小聰";
            // 
            // buttonItem125
            // 
            this.buttonItem125.ImagePaddingHorizontal = 8;
            this.buttonItem125.Name = "buttonItem125";
            this.buttonItem125.Text = "李小明";
            // 
            // buttonItem126
            // 
            this.buttonItem126.ImagePaddingHorizontal = 8;
            this.buttonItem126.Name = "buttonItem126";
            this.buttonItem126.Text = "張小月";
            // 
            // buttonItem127
            // 
            this.buttonItem127.ImagePaddingHorizontal = 8;
            this.buttonItem127.Name = "buttonItem127";
            this.buttonItem127.Text = "曾小愛";
            // 
            // buttonItem128
            // 
            this.buttonItem128.ImagePaddingHorizontal = 8;
            this.buttonItem128.Name = "buttonItem128";
            this.buttonItem128.Text = "林小美";
            // 
            // btnPlan
            // 
            this.btnPlan.AutoExpandOnClick = true;
            this.btnPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPlan.Enabled = false;
            this.btnPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnPlan.Image")));
            this.btnPlan.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnPlan.ImagePaddingHorizontal = 3;
            this.btnPlan.ImagePaddingVertical = 3;
            this.btnPlan.Name = "btnPlan";
            this.btnPlan.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.btnPlan.SubItemsExpandWidth = 14;
            this.btnPlan.Text = "課程規劃";
            this.btnPlan.Visible = false;
            this.btnPlan.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.buttonItem56_PopupOpen);
            // 
            // itemContainer1
            // 
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem1});
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = true;
            this.controlContainerItem1.Control = null;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            this.controlContainerItem1.Text = "controlContainerItem1";
            // 
            // btnCalcRule
            // 
            this.btnCalcRule.AutoExpandOnClick = true;
            this.btnCalcRule.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnCalcRule.Enabled = false;
            this.btnCalcRule.Image = ((System.Drawing.Image)(resources.GetObject("btnCalcRule.Image")));
            this.btnCalcRule.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnCalcRule.ImagePaddingHorizontal = 3;
            this.btnCalcRule.ImagePaddingVertical = 3;
            this.btnCalcRule.Name = "btnCalcRule";
            this.btnCalcRule.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem71,
            this.buttonItem72,
            this.buttonItem73,
            this.buttonItem74,
            this.buttonItem75,
            this.buttonItem76});
            this.btnCalcRule.SubItemsExpandWidth = 14;
            this.btnCalcRule.Text = "計算規則";
            this.btnCalcRule.Visible = false;
            this.btnCalcRule.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.buttonItem65_PopupOpen);
            // 
            // buttonItem71
            // 
            this.buttonItem71.ImagePaddingHorizontal = 8;
            this.buttonItem71.Name = "buttonItem71";
            this.buttonItem71.Text = "一般及格標準";
            // 
            // buttonItem72
            // 
            this.buttonItem72.ImagePaddingHorizontal = 8;
            this.buttonItem72.Name = "buttonItem72";
            this.buttonItem72.Text = "40.50.60";
            // 
            // buttonItem73
            // 
            this.buttonItem73.ImagePaddingHorizontal = 8;
            this.buttonItem73.Name = "buttonItem73";
            this.buttonItem73.Text = "40.40.50";
            // 
            // buttonItem74
            // 
            this.buttonItem74.ImagePaddingHorizontal = 8;
            this.buttonItem74.Name = "buttonItem74";
            this.buttonItem74.Text = "50.50.60";
            // 
            // buttonItem75
            // 
            this.buttonItem75.ImagePaddingHorizontal = 8;
            this.buttonItem75.Name = "buttonItem75";
            this.buttonItem75.Text = "所有成績及格/補考標準…";
            // 
            // buttonItem76
            // 
            this.buttonItem76.ImagePaddingHorizontal = 8;
            this.buttonItem76.Name = "buttonItem76";
            this.buttonItem76.Text = "設定快速點選及格標準";
            // 
            // itemContainer2
            // 
            this.itemContainer2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer2.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer2.Name = "itemContainer2";
            this.itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAssignStudent,
            this.btnPlan,
            this.btnCalcRule});
            // 
            // Assign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Assign";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnAssignStudent;
        private DevComponents.DotNetBar.ButtonItem buttonItem124;
        private DevComponents.DotNetBar.ButtonItem buttonItem125;
        private DevComponents.DotNetBar.ButtonItem buttonItem126;
        private DevComponents.DotNetBar.ButtonItem buttonItem127;
        private DevComponents.DotNetBar.ButtonItem buttonItem128;
        private DevComponents.DotNetBar.ButtonItem btnPlan;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private DevComponents.DotNetBar.ButtonItem btnCalcRule;
        private DevComponents.DotNetBar.ButtonItem buttonItem71;
        private DevComponents.DotNetBar.ButtonItem buttonItem72;
        private DevComponents.DotNetBar.ButtonItem buttonItem73;
        private DevComponents.DotNetBar.ButtonItem buttonItem74;
        private DevComponents.DotNetBar.ButtonItem buttonItem75;
        private DevComponents.DotNetBar.ButtonItem buttonItem76;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
    }
}
