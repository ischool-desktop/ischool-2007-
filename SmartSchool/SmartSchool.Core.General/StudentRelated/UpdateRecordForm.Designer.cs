namespace SmartSchool.StudentRelated
{
    partial class UpdateRecordForm
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
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.updateRecordInfo1 = new SmartSchool.StudentRelated.UpdateRecordInfo();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnOK.Location = new System.Drawing.Point(335, 435);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(53, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "確定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonX2.Location = new System.Drawing.Point(394, 435);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(54, 23);
            this.buttonX2.TabIndex = 2;
            this.buttonX2.Text = "離開";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.comboBoxEx1);
            this.panelEx1.Controls.Add(this.updateRecordInfo1);
            this.panelEx1.Controls.Add(this.buttonX2);
            this.panelEx1.Controls.Add(this.btnOK);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(473, 471);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 3;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 19;
            this.comboBoxEx1.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4});
            this.comboBoxEx1.Location = new System.Drawing.Point(16, 9);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(121, 25);
            this.comboBoxEx1.TabIndex = 3;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "學籍異動";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "他校轉入";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "新生";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "畢業";
            // 
            // updateRecordInfo1
            // 
            this.updateRecordInfo1.ADDate = "";
            this.updateRecordInfo1.ADNumber = "";
            this.updateRecordInfo1.AllowedUpdateCode = null;
            this.updateRecordInfo1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.updateRecordInfo1.Birthdate = "";
            this.updateRecordInfo1.Comment = "";
            this.updateRecordInfo1.Department = "";
            this.updateRecordInfo1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.updateRecordInfo1.Gender = "";
            this.updateRecordInfo1.GradeYear = "";
            this.updateRecordInfo1.GraduateCertificateNumber = "";
            this.updateRecordInfo1.GraduateSchool = "";
            this.updateRecordInfo1.GraduateSchoolLocationCode = "";
            this.updateRecordInfo1.IDNumber = "";
            this.updateRecordInfo1.LastADDate = "";
            this.updateRecordInfo1.LastADNumber = "";
            this.updateRecordInfo1.LastUpdateCode = "";
            this.updateRecordInfo1.Location = new System.Drawing.Point(11, 37);
            this.updateRecordInfo1.Name = "updateRecordInfo1";
            this.updateRecordInfo1.NewStudentNumber = "";
            this.updateRecordInfo1.NewStudentNumberVisible = false;
            this.updateRecordInfo1.PreviousDepartment = "";
            this.updateRecordInfo1.PreviousGradeYear = "";
            this.updateRecordInfo1.PreviousSchool = "";
            this.updateRecordInfo1.PreviousSchoolLastADDate = "";
            this.updateRecordInfo1.PreviousSchoolLastADNumber = "";
            this.updateRecordInfo1.PreviousStudentNumber = "";
            this.updateRecordInfo1.Size = new System.Drawing.Size(453, 392);
            this.updateRecordInfo1.StudentID = "";
            this.updateRecordInfo1.StudentNumber = "";
            this.updateRecordInfo1.Style = SmartSchool.StudentRelated.UpdateRecordType.轉入異動;
            this.updateRecordInfo1.TabIndex = 0;
            this.updateRecordInfo1.UpdateCode = "";
            this.updateRecordInfo1.UpdateDate = "";
            this.updateRecordInfo1.UpdateDescription = "";
            // 
            // UpdateRecordForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 194 ) ) ) ), ( (int)( ( (byte)( 217 ) ) ) ), ( (int)( ( (byte)( 247 ) ) ) ));
            this.ClientSize = new System.Drawing.Size(473, 471);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateRecordForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理學生異動資料";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateRecordForm_FormClosing);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UpdateRecordInfo updateRecordInfo1;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;

    }
}