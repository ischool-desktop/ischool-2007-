namespace SmartSchool.CourseRelated.RibbonBars
{
    partial class Assign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Assign));
            this.buttonItem101 = new DevComponents.DotNetBar.ButtonItem();
            this.btnAttendStudent = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem18 = new DevComponents.DotNetBar.ButtonItem();
            this.btnAssignTeacher = new DevComponents.DotNetBar.ButtonItem();
            this.����� = new DevComponents.DotNetBar.ButtonItem();
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
            this.MainRibbonBar.Text = "���w";
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
            this.btnAttendStudent.Text = "�׽Ҿǥ�";
            this.btnAttendStudent.Tooltip = "�׽Ҿǥ�";
            this.btnAttendStudent.PopupShowing += new System.EventHandler(this.buttonItem54_PopupShowing);
            // 
            // buttonItem10
            // 
            this.buttonItem10.ImagePaddingHorizontal = 8;
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Text = "���p�o";
            // 
            // buttonItem12
            // 
            this.buttonItem12.ImagePaddingHorizontal = 8;
            this.buttonItem12.Name = "buttonItem12";
            this.buttonItem12.Text = "���p��";
            // 
            // buttonItem13
            // 
            this.buttonItem13.ImagePaddingHorizontal = 8;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Text = "�i�p��";
            // 
            // buttonItem14
            // 
            this.buttonItem14.ImagePaddingHorizontal = 8;
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.Text = "���p�R";
            // 
            // buttonItem18
            // 
            this.buttonItem18.ImagePaddingHorizontal = 8;
            this.buttonItem18.Name = "buttonItem18";
            this.buttonItem18.Text = "�L�p��";
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
            this.�����});
            this.btnAssignTeacher.Text = "�����Юv";
            this.btnAssignTeacher.Tooltip = "�����Юv";
            this.btnAssignTeacher.PopupShowing += new System.EventHandler(this.btnAssignTeacher_PopupShowing);
            // 
            // �����
            // 
            this.�����.ImagePaddingHorizontal = 8;
            this.�����.Name = "�����";
            this.�����.Text = "New Item";
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
            this.btnScores.Text = "�����˪�";
            this.btnScores.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btnScores_PopupOpen);
            // 
            // btnManage
            // 
            this.btnManage.BeginGroup = true;
            this.btnManage.ImagePaddingHorizontal = 8;
            this.btnManage.Name = "btnManage";
            this.btnManage.Text = "�޲z�˪��K";
            // 
            // buttonItem48
            // 
            this.buttonItem48.ImagePaddingHorizontal = 8;
            this.buttonItem48.Name = "buttonItem48";
            this.buttonItem48.Text = "�]�w�ֳt�I��˪�";
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
        private DevComponents.DotNetBar.ButtonItem �����;
        private DevComponents.DotNetBar.ButtonItem btnScores;
        private DevComponents.DotNetBar.ButtonItem btnManage;
        private DevComponents.DotNetBar.ButtonItem buttonItem48;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
    }
}
