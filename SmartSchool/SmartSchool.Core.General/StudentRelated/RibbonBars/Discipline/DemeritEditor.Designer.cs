namespace SmartSchool.StudentRelated.RibbonBars.Discipline
{
    partial class DemeritEditor
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
            this.cboReasonRef = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.txtReason = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dateText = new SmartSchool.Common.DateTimeTextBox();
            this.txt3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lbl3 = new DevComponents.DotNetBar.LabelX();
            this.lbl2 = new DevComponents.DotNetBar.LabelX();
            this.lbl1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.chkAsshole = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.SuspendLayout();
            // 
            // cboReasonRef
            // 
            this.cboReasonRef.DisplayMember = "Text";
            this.cboReasonRef.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboReasonRef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReasonRef.FormattingEnabled = true;
            this.cboReasonRef.ItemHeight = 17;
            this.cboReasonRef.Location = new System.Drawing.Point(95, 128);
            this.cboReasonRef.Name = "cboReasonRef";
            this.cboReasonRef.Size = new System.Drawing.Size(215, 23);
            this.cboReasonRef.TabIndex = 35;
            this.cboReasonRef.SelectedIndexChanged += new System.EventHandler(this.cboReasonRef_SelectedIndexChanged);
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(20, 128);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(68, 23);
            this.labelX3.TabIndex = 34;
            this.labelX3.Text = "事由代碼";
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(254, 258);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(57, 23);
            this.btnExit.TabIndex = 33;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Location = new System.Drawing.Point(191, 258);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(57, 23);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtReason
            // 
            // 
            // 
            // 
            this.txtReason.Border.Class = "TextBoxBorder";
            this.txtReason.Location = new System.Drawing.Point(95, 157);
            this.txtReason.MaxLength = 100;
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(215, 95);
            this.txtReason.TabIndex = 31;
            // 
            // dateText
            // 
            this.dateText.AllowNull = true;
            // 
            // 
            // 
            this.dateText.Border.Class = "TextBoxBorder";
            this.dateText.EmptyString = "";
            this.dateText.Location = new System.Drawing.Point(95, 99);
            this.dateText.Name = "dateText";
            this.dateText.Size = new System.Drawing.Size(215, 23);
            this.dateText.TabIndex = 30;
            this.dateText.WatermarkText = "請輸入西元日期";
            this.dateText.Validated += new System.EventHandler(this.dateText_Validated);
            // 
            // txt3
            // 
            // 
            // 
            // 
            this.txt3.Border.Class = "TextBoxBorder";
            this.txt3.Location = new System.Drawing.Point(281, 42);
            this.txt3.Name = "txt3";
            this.txt3.Size = new System.Drawing.Size(29, 23);
            this.txt3.TabIndex = 29;
            this.txt3.Validated += new System.EventHandler(this.txt3_Validated);
            // 
            // txt2
            // 
            // 
            // 
            // 
            this.txt2.Border.Class = "TextBoxBorder";
            this.txt2.Location = new System.Drawing.Point(191, 42);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(29, 23);
            this.txt2.TabIndex = 28;
            this.txt2.Validated += new System.EventHandler(this.txt2_Validated);
            // 
            // txt1
            // 
            // 
            // 
            // 
            this.txt1.Border.Class = "TextBoxBorder";
            this.txt1.Location = new System.Drawing.Point(93, 42);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(29, 23);
            this.txt1.TabIndex = 27;
            this.txt1.Validated += new System.EventHandler(this.txt1_Validated);
            // 
            // cboSemester
            // 
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 17;
            this.cboSemester.Location = new System.Drawing.Point(233, 12);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(77, 23);
            this.cboSemester.TabIndex = 26;
            this.cboSemester.Validated += new System.EventHandler(this.cboSemester_Validated);
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.ItemHeight = 17;
            this.cboSchoolYear.Location = new System.Drawing.Point(93, 12);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(70, 23);
            this.cboSchoolYear.TabIndex = 25;
            this.cboSchoolYear.Validated += new System.EventHandler(this.cboSchoolYear_Validated);
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(51, 157);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(37, 23);
            this.labelX7.TabIndex = 24;
            this.labelX7.Text = "事由";
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(51, 99);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(37, 23);
            this.labelX6.TabIndex = 23;
            this.labelX6.Text = "日期";
            // 
            // lbl3
            // 
            this.lbl3.Location = new System.Drawing.Point(241, 42);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(37, 23);
            this.lbl3.TabIndex = 22;
            this.lbl3.Text = "警告";
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(148, 42);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(37, 23);
            this.lbl2.TabIndex = 21;
            this.lbl2.Text = "小過";
            // 
            // lbl1
            // 
            this.lbl1.Location = new System.Drawing.Point(51, 42);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(37, 23);
            this.lbl1.TabIndex = 20;
            this.lbl1.Text = "大過";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(191, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(43, 23);
            this.labelX2.TabIndex = 19;
            this.labelX2.Text = "學期";
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(20, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(67, 23);
            this.labelX1.TabIndex = 18;
            this.labelX1.Text = "學 年 度";
            // 
            // chkAsshole
            // 
            this.chkAsshole.Location = new System.Drawing.Point(93, 70);
            this.chkAsshole.Name = "chkAsshole";
            this.chkAsshole.Size = new System.Drawing.Size(217, 23);
            this.chkAsshole.TabIndex = 36;
            this.chkAsshole.Text = "標記為留校察看生";
            this.chkAsshole.CheckedChanged += new System.EventHandler(this.chkAsshole_CheckedChanged);
            // 
            // DemeritEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 295);
            this.ControlBox = false;
            this.Controls.Add(this.chkAsshole);
            this.Controls.Add(this.cboReasonRef);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.dateText);
            this.Controls.Add(this.txt3);
            this.Controls.Add(this.txt2);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.cboSchoolYear);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DemeritEditor";
            this.Text = "懲戒管理";
            this.Load += new System.EventHandler(this.DemeritEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboReasonRef;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.TextBoxX txtReason;
        private SmartSchool.Common.DateTimeTextBox dateText;
        private DevComponents.DotNetBar.Controls.TextBoxX txt3;
        private DevComponents.DotNetBar.Controls.TextBoxX txt2;
        private DevComponents.DotNetBar.Controls.TextBoxX txt1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX lbl3;
        private DevComponents.DotNetBar.LabelX lbl2;
        private DevComponents.DotNetBar.LabelX lbl1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAsshole;
    }
}