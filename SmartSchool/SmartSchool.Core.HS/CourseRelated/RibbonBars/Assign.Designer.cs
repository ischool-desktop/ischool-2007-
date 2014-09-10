namespace SmartSchool.CourseRelated.RibbonBars
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Assign));
            this.buttonItem101 = new DevComponents.DotNetBar.ButtonItem();
            this.btnAttendStudent = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem18 = new DevComponents.DotNetBar.ButtonItem();
            this.btnAssignTeacher = new DevComponents.DotNetBar.ButtonItem();
            this.路西華 = new DevComponents.DotNetBar.ButtonItem();
            this.btnScores = new DevComponents.DotNetBar.ButtonItem();
            this.btnManage = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem48 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.SuspendLayout();
            // 
            // MainRibbonBar
            // 
            this.MainRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.MainRibbonBar.Size = new System.Drawing.Size(461, 104);
            this.MainRibbonBar.Text = "指定";
            // 
            // buttonItem101
            // 
            this.buttonItem101.ImagePaddingHorizontal = 8;
            this.buttonItem101.Name = "buttonItem101";
            this.buttonItem101.Text = "buttonItem101";
            // 
            // btnAttendStudent
            // 
            this.btnAttendStudent.AutoExpandOnClick = true;
            this.btnAttendStudent.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAttendStudent.Image = ((System.Drawing.Image)(resources.GetObject("btnAttendStudent.Image")));
            this.btnAttendStudent.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnAttendStudent.ImagePaddingHorizontal = 8;
            this.btnAttendStudent.ImagePaddingVertical = 3;
            this.btnAttendStudent.Name = "btnAttendStudent";
            this.btnAttendStudent.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem10,
            this.buttonItem12,
            this.buttonItem13,
            this.buttonItem14,
            this.buttonItem18});
            this.btnAttendStudent.Text = "修課學生";
            this.btnAttendStudent.Tooltip = "修課學生";
            this.btnAttendStudent.PopupShowing += new System.EventHandler(this.buttonItem54_PopupShowing);
            // 
            // buttonItem10
            // 
            this.buttonItem10.ImagePaddingHorizontal = 8;
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Text = "陳小聰";
            // 
            // buttonItem12
            // 
            this.buttonItem12.ImagePaddingHorizontal = 8;
            this.buttonItem12.Name = "buttonItem12";
            this.buttonItem12.Text = "李小明";
            // 
            // buttonItem13
            // 
            this.buttonItem13.ImagePaddingHorizontal = 8;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Text = "張小月";
            // 
            // buttonItem14
            // 
            this.buttonItem14.ImagePaddingHorizontal = 8;
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.Text = "曾小愛";
            // 
            // buttonItem18
            // 
            this.buttonItem18.ImagePaddingHorizontal = 8;
            this.buttonItem18.Name = "buttonItem18";
            this.buttonItem18.Text = "林小美";
            // 
            // btnAssignTeacher
            // 
            this.btnAssignTeacher.AutoExpandOnClick = true;
            this.btnAssignTeacher.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAssignTeacher.Image = ((System.Drawing.Image)(resources.GetObject("btnAssignTeacher.Image")));
            this.btnAssignTeacher.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnAssignTeacher.ImagePaddingHorizontal = 8;
            this.btnAssignTeacher.ImagePaddingVertical = 3;
            this.btnAssignTeacher.Name = "btnAssignTeacher";
            this.btnAssignTeacher.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.路西華});
            this.btnAssignTeacher.Text = "評分教師";
            this.btnAssignTeacher.Tooltip = "評分教師";
            this.btnAssignTeacher.PopupShowing += new System.EventHandler(this.btnAssignTeacher_PopupShowing);
            // 
            // 路西華
            // 
            this.路西華.ImagePaddingHorizontal = 8;
            this.路西華.Name = "路西華";
            this.路西華.Text = "New Item";
            // 
            // btnScores
            // 
            this.btnScores.AutoExpandOnClick = true;
            this.btnScores.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnScores.Enabled = false;
            this.btnScores.Image = ((System.Drawing.Image)(resources.GetObject("btnScores.Image")));
            this.btnScores.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnScores.ImagePaddingHorizontal = 8;
            this.btnScores.ImagePaddingVertical = 3;
            this.btnScores.Name = "btnScores";
            this.btnScores.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnManage,
            this.buttonItem48});
            this.btnScores.Text = "評分樣版";
            this.btnScores.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btnScores_PopupOpen);
            // 
            // btnManage
            // 
            this.btnManage.BeginGroup = true;
            this.btnManage.ImagePaddingHorizontal = 8;
            this.btnManage.Name = "btnManage";
            this.btnManage.Text = "管理樣版…";
            // 
            // buttonItem48
            // 
            this.buttonItem48.ImagePaddingHorizontal = 8;
            this.buttonItem48.Name = "buttonItem48";
            this.buttonItem48.Text = "設定快速點選樣版";
            // 
            // itemContainer1
            // 
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAttendStudent,
            this.btnAssignTeacher,
            this.btnScores});
            // 
            // Assign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "Assign";
            this.Size = new System.Drawing.Size(683, 144);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem101;
        private DevComponents.DotNetBar.ButtonItem btnAttendStudent;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItem12;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.ButtonItem buttonItem18;
        private DevComponents.DotNetBar.ButtonItem btnAssignTeacher;
        private DevComponents.DotNetBar.ButtonItem 路西華;
        private DevComponents.DotNetBar.ButtonItem btnScores;
        private DevComponents.DotNetBar.ButtonItem btnManage;
        private DevComponents.DotNetBar.ButtonItem buttonItem48;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
    }
}
