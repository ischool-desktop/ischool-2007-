namespace SmartSchool
{
    partial class SkillSchool
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
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtSchoolName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtSchoolYear = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtSemester = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtSchoolCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtTelephone = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Location = new System.Drawing.Point(177, 156);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(18, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "學校名稱";
            // 
            // txtSchoolName
            // 
            // 
            // 
            // 
            this.txtSchoolName.Border.Class = "TextBoxBorder";
            this.txtSchoolName.Location = new System.Drawing.Point(99, 12);
            this.txtSchoolName.Name = "txtSchoolName";
            this.txtSchoolName.Size = new System.Drawing.Size(150, 23);
            this.txtSchoolName.TabIndex = 0;
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(18, 69);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "學年度";
            // 
            // txtSchoolYear
            // 
            // 
            // 
            // 
            this.txtSchoolYear.Border.Class = "TextBoxBorder";
            this.txtSchoolYear.Location = new System.Drawing.Point(99, 69);
            this.txtSchoolYear.Name = "txtSchoolYear";
            this.txtSchoolYear.Size = new System.Drawing.Size(150, 23);
            this.txtSchoolYear.TabIndex = 2;
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(18, 98);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "學期";
            // 
            // txtSemester
            // 
            // 
            // 
            // 
            this.txtSemester.Border.Class = "TextBoxBorder";
            this.txtSemester.Location = new System.Drawing.Point(99, 98);
            this.txtSemester.Name = "txtSemester";
            this.txtSemester.Size = new System.Drawing.Size(150, 23);
            this.txtSemester.TabIndex = 3;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(18, 40);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(75, 23);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "學校代碼";
            // 
            // txtSchoolCode
            // 
            // 
            // 
            // 
            this.txtSchoolCode.Border.Class = "TextBoxBorder";
            this.txtSchoolCode.Location = new System.Drawing.Point(99, 40);
            this.txtSchoolCode.Name = "txtSchoolCode";
            this.txtSchoolCode.Size = new System.Drawing.Size(150, 23);
            this.txtSchoolCode.TabIndex = 1;
            // 
            // txtTelephone
            // 
            // 
            // 
            // 
            this.txtTelephone.Border.Class = "TextBoxBorder";
            this.txtTelephone.Location = new System.Drawing.Point(99, 127);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(150, 23);
            this.txtTelephone.TabIndex = 7;
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(18, 127);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 23);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "電話";
            // 
            // SkillSchool
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(264, 193);
            this.Controls.Add(this.txtTelephone);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtSemester);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtSchoolYear);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtSchoolCode);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtSchoolName);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnSave);
            this.Name = "SkillSchool";
            this.Text = "SkillSchool";
            this.Load += new System.EventHandler(this.SkillSchool_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSchoolName;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSemester;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSchoolCode;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTelephone;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}