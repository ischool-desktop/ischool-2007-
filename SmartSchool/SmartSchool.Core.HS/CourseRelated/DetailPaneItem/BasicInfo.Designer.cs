using SmartSchool.TeacherRelated;
using SmartSchool.ClassRelated;
using ClassEntity = SmartSchool.ClassRelated.Class;
using System;
namespace SmartSchool.CourseRelated.DetailPaneItem
{
    partial class BasicInfo
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
                Teacher.Instance.TeacherDataChanged -= new EventHandler<TeacherDataChangedEventArgs>(Instance_TeacherDataChanged);
                Teacher.Instance.TeacherInserted -= new EventHandler(Instance_TeacherInserted);
                Teacher.Instance.TeacherDeleted -= new EventHandler<TeacherDeletedEventArgs>(Instance_TeacherDeleted);
                ClassEntity.Instance.ClassInserted-= new EventHandler<InsertClassEventArgs>(Instance_ClassInserted);
                ClassEntity.Instance.ClassUpdated -= new EventHandler<UpdateClassEventArgs>(Instance_ClassUpdated);
                ClassEntity.Instance.ClassDeleted -= new EventHandler<DeleteClassEventArgs>(Instance_ClassDeleted);
                CourseEntity.Instance.ForeignTableChanged -= new EventHandler(Instance_ForeignTableChanged);
                CourseEntity.Instance.CourseChanged -= new EventHandler<CourseChangeEventArgs>(Instance_CourseChanged);
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtCourseName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSubject = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboSubjectLevel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.cboClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.txtCredit = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.cboEntry = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.cboExamTemplate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.comboItem19 = new DevComponents.Editors.ComboItem();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.btnTeachers = new DevComponents.DotNetBar.ButtonX();
            this.btnTeacher1 = new DevComponents.DotNetBar.ButtonItem();
            this.btnTeacher2 = new DevComponents.DotNetBar.ButtonItem();
            this.btnTeacher3 = new DevComponents.DotNetBar.ButtonItem();
            this.cboMultiTeacher = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboRequired = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem22 = new DevComponents.Editors.ComboItem();
            this.comboItem20 = new DevComponents.Editors.ComboItem();
            this.comboItem21 = new DevComponents.Editors.ComboItem();
            this.cboRequiredBy = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem23 = new DevComponents.Editors.ComboItem();
            this.comboItem24 = new DevComponents.Editors.ComboItem();
            this.comboItem25 = new DevComponents.Editors.ComboItem();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.rdoCalcTrue = new System.Windows.Forms.RadioButton();
            this.rdoCalcFalse = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdoCreditTrue = new System.Windows.Forms.RadioButton();
            this.rdoCreditFalse = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(38, 20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(70, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "課程名稱";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtCourseName
            // 
            // 
            // 
            // 
            this.txtCourseName.Border.Class = "TextBoxBorder";
            this.txtCourseName.Location = new System.Drawing.Point(113, 19);
            this.txtCourseName.MaxLength = 50;
            this.txtCourseName.Name = "txtCourseName";
            this.txtCourseName.Size = new System.Drawing.Size(151, 25);
            this.txtCourseName.TabIndex = 1;
            this.txtCourseName.TextChanged += new System.EventHandler(this.txtCourseName_TextChanged);
            // 
            // txtSubject
            // 
            // 
            // 
            // 
            this.txtSubject.Border.Class = "TextBoxBorder";
            this.txtSubject.Location = new System.Drawing.Point(113, 48);
            this.txtSubject.MaxLength = 50;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(151, 25);
            this.txtSubject.TabIndex = 3;
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(38, 49);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(70, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "科目名稱";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(38, 78);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(70, 23);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "科目級別";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cboSubjectLevel
            // 
            this.cboSubjectLevel.DisplayMember = "Text";
            this.cboSubjectLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSubjectLevel.FormattingEnabled = true;
            this.cboSubjectLevel.ItemHeight = 19;
            this.cboSubjectLevel.Items.AddRange(new object[] {
            this.comboItem17,
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6});
            this.cboSubjectLevel.Location = new System.Drawing.Point(113, 77);
            this.cboSubjectLevel.MaxDropDownItems = 6;
            this.cboSubjectLevel.Name = "cboSubjectLevel";
            this.cboSubjectLevel.Size = new System.Drawing.Size(151, 25);
            this.cboSubjectLevel.TabIndex = 5;
            this.cboSubjectLevel.Tag = "";
            this.cboSubjectLevel.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            this.cboSubjectLevel.TextChanged += new System.EventHandler(this.cboSubjectLevel_TextChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "2";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "3";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "4";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "5";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "6";
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(287, 20);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(70, 23);
            this.labelX4.TabIndex = 12;
            this.labelX4.Text = "所屬班級";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(287, 49);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(70, 23);
            this.labelX5.TabIndex = 14;
            this.labelX5.Text = "學 年 度";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(287, 78);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(70, 23);
            this.labelX6.TabIndex = 16;
            this.labelX6.Text = "學　　期";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(287, 107);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(70, 23);
            this.labelX7.TabIndex = 18;
            this.labelX7.Text = "學　　分";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cboClass
            // 
            this.cboClass.DisplayMember = "Text";
            this.cboClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboClass.FormattingEnabled = true;
            this.cboClass.ItemHeight = 19;
            this.cboClass.Location = new System.Drawing.Point(364, 19);
            this.cboClass.MaxDropDownItems = 6;
            this.cboClass.Name = "cboClass";
            this.cboClass.Size = new System.Drawing.Size(151, 25);
            this.cboClass.TabIndex = 13;
            this.cboClass.Tag = "ForceValidate";
            this.cboClass.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            this.cboClass.SelectedIndexChanged += new System.EventHandler(this.cboClass_SelectedIndexChanged);
            this.cboClass.TextChanged += new System.EventHandler(this.cboClass_TextChanged);
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.ItemHeight = 19;
            this.cboSchoolYear.Items.AddRange(new object[] {
            this.comboItem9});
            this.cboSchoolYear.Location = new System.Drawing.Point(364, 48);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(151, 25);
            this.cboSchoolYear.TabIndex = 15;
            this.cboSchoolYear.Tag = "";
            this.cboSchoolYear.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            this.cboSchoolYear.TextChanged += new System.EventHandler(this.cboSchoolYear_TextChanged);
            // 
            // cboSemester
            // 
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 19;
            this.cboSemester.Items.AddRange(new object[] {
            this.comboItem10,
            this.comboItem7,
            this.comboItem8});
            this.cboSemester.Location = new System.Drawing.Point(364, 77);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(151, 25);
            this.cboSemester.TabIndex = 17;
            this.cboSemester.Tag = "";
            this.cboSemester.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            this.cboSemester.TextChanged += new System.EventHandler(this.cboSemester_TextChanged);
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "1";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "2";
            // 
            // txtCredit
            // 
            // 
            // 
            // 
            this.txtCredit.Border.Class = "TextBoxBorder";
            this.txtCredit.Location = new System.Drawing.Point(364, 106);
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new System.Drawing.Size(151, 25);
            this.txtCredit.TabIndex = 19;
            this.txtCredit.TextChanged += new System.EventHandler(this.txtCredit_TextChanged);
            // 
            // labelX9
            // 
            this.labelX9.Location = new System.Drawing.Point(25, 166);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(83, 23);
            this.labelX9.TabIndex = 8;
            this.labelX9.Text = "學分設定";
            this.labelX9.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX10
            // 
            this.labelX10.Location = new System.Drawing.Point(287, 166);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(70, 23);
            this.labelX10.TabIndex = 20;
            this.labelX10.Text = "評分設定";
            this.labelX10.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cboEntry
            // 
            this.cboEntry.DisplayMember = "Text";
            this.cboEntry.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboEntry.FormattingEnabled = true;
            this.cboEntry.ItemHeight = 19;
            this.cboEntry.Items.AddRange(new object[] {
            this.comboItem15,
            this.comboItem16});
            this.cboEntry.Location = new System.Drawing.Point(113, 106);
            this.cboEntry.MaxDropDownItems = 6;
            this.cboEntry.Name = "cboEntry";
            this.cboEntry.Size = new System.Drawing.Size(151, 25);
            this.cboEntry.TabIndex = 7;
            this.cboEntry.Tag = "ForceValidate";
            this.cboEntry.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            this.cboEntry.TextChanged += new System.EventHandler(this.cboEntry_TextChanged);
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "是";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "否";
            // 
            // labelX11
            // 
            this.labelX11.Location = new System.Drawing.Point(25, 107);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(83, 23);
            this.labelX11.TabIndex = 6;
            this.labelX11.Text = "分項類別";
            this.labelX11.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cboExamTemplate
            // 
            this.cboExamTemplate.DisplayMember = "Text";
            this.cboExamTemplate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboExamTemplate.FormattingEnabled = true;
            this.cboExamTemplate.ItemHeight = 19;
            this.cboExamTemplate.Items.AddRange(new object[] {
            this.comboItem18,
            this.comboItem19});
            this.cboExamTemplate.Location = new System.Drawing.Point(364, 193);
            this.cboExamTemplate.MaxDropDownItems = 6;
            this.cboExamTemplate.Name = "cboExamTemplate";
            this.cboExamTemplate.Size = new System.Drawing.Size(151, 25);
            this.cboExamTemplate.TabIndex = 23;
            this.cboExamTemplate.Tag = "ForceValidate";
            this.cboExamTemplate.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            this.cboExamTemplate.SelectedIndexChanged += new System.EventHandler(this.cboExamTemplate_SelectedIndexChanged);
            this.cboExamTemplate.TextChanged += new System.EventHandler(this.cboExamTemplate_TextChanged);
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "是";
            // 
            // comboItem19
            // 
            this.comboItem19.Text = "否";
            // 
            // labelX12
            // 
            this.labelX12.Location = new System.Drawing.Point(287, 194);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(70, 23);
            this.labelX12.TabIndex = 22;
            this.labelX12.Text = "評分樣版";
            this.labelX12.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnTeachers
            // 
            this.btnTeachers.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTeachers.AutoExpandOnClick = true;
            this.btnTeachers.Location = new System.Drawing.Point(6, 194);
            this.btnTeachers.Margin = new System.Windows.Forms.Padding(4);
            this.btnTeachers.Name = "btnTeachers";
            this.btnTeachers.Size = new System.Drawing.Size(103, 22);
            this.btnTeachers.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnTeacher1,
            this.btnTeacher2,
            this.btnTeacher3});
            this.btnTeachers.TabIndex = 10;
            // 
            // btnTeacher1
            // 
            this.btnTeacher1.GlobalItem = false;
            this.btnTeacher1.ImagePaddingHorizontal = 8;
            this.btnTeacher1.Name = "btnTeacher1";
            this.btnTeacher1.Text = "教師一(評分)";
            // 
            // btnTeacher2
            // 
            this.btnTeacher2.GlobalItem = false;
            this.btnTeacher2.ImagePaddingHorizontal = 8;
            this.btnTeacher2.Name = "btnTeacher2";
            this.btnTeacher2.Text = "教師二";
            // 
            // btnTeacher3
            // 
            this.btnTeacher3.GlobalItem = false;
            this.btnTeacher3.ImagePaddingHorizontal = 8;
            this.btnTeacher3.Name = "btnTeacher3";
            this.btnTeacher3.Text = "教師三";
            // 
            // cboMultiTeacher
            // 
            this.cboMultiTeacher.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboMultiTeacher.DisplayMember = "Text";
            this.cboMultiTeacher.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMultiTeacher.FormattingEnabled = true;
            this.cboMultiTeacher.ItemHeight = 19;
            this.cboMultiTeacher.Location = new System.Drawing.Point(113, 193);
            this.cboMultiTeacher.MaxDropDownItems = 6;
            this.cboMultiTeacher.Name = "cboMultiTeacher";
            this.cboMultiTeacher.Size = new System.Drawing.Size(151, 25);
            this.cboMultiTeacher.TabIndex = 11;
            this.cboMultiTeacher.Tag = "ForceValidate";
            this.cboMultiTeacher.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            // 
            // cboRequired
            // 
            this.cboRequired.DisplayMember = "Text";
            this.cboRequired.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboRequired.FormattingEnabled = true;
            this.cboRequired.ItemHeight = 19;
            this.cboRequired.Items.AddRange(new object[] {
            this.comboItem22,
            this.comboItem20,
            this.comboItem21});
            this.cboRequired.Location = new System.Drawing.Point(364, 135);
            this.cboRequired.MaxDropDownItems = 6;
            this.cboRequired.Name = "cboRequired";
            this.cboRequired.Size = new System.Drawing.Size(151, 25);
            this.cboRequired.TabIndex = 21;
            this.cboRequired.Tag = "ForceValidate";
            this.cboRequired.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            this.cboRequired.TextChanged += new System.EventHandler(this.cboRequired_TextChanged);
            // 
            // comboItem20
            // 
            this.comboItem20.Text = "必修";
            // 
            // comboItem21
            // 
            this.comboItem21.Text = "選修";
            // 
            // cboRequiredBy
            // 
            this.cboRequiredBy.DisplayMember = "Text";
            this.cboRequiredBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboRequiredBy.FormattingEnabled = true;
            this.cboRequiredBy.ItemHeight = 19;
            this.cboRequiredBy.Items.AddRange(new object[] {
            this.comboItem23,
            this.comboItem24,
            this.comboItem25});
            this.cboRequiredBy.Location = new System.Drawing.Point(113, 135);
            this.cboRequiredBy.MaxDropDownItems = 6;
            this.cboRequiredBy.Name = "cboRequiredBy";
            this.cboRequiredBy.Size = new System.Drawing.Size(151, 25);
            this.cboRequiredBy.TabIndex = 9;
            this.cboRequiredBy.Tag = "ForceValidate";
            this.cboRequiredBy.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxItem_Validating);
            this.cboRequiredBy.TextChanged += new System.EventHandler(this.cboRequiredBy_TextChanged);
            // 
            // comboItem24
            // 
            this.comboItem24.Text = "校訂";
            // 
            // comboItem25
            // 
            this.comboItem25.Text = "部訂";
            // 
            // labelX8
            // 
            this.labelX8.Location = new System.Drawing.Point(287, 136);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(70, 23);
            this.labelX8.TabIndex = 20;
            this.labelX8.Text = "必 選 修";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX13
            // 
            this.labelX13.Location = new System.Drawing.Point(25, 136);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(83, 23);
            this.labelX13.TabIndex = 8;
            this.labelX13.Text = "校 部 訂";
            this.labelX13.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // rdoCalcTrue
            // 
            this.rdoCalcTrue.AutoSize = true;
            this.rdoCalcTrue.Location = new System.Drawing.Point(7, 2);
            this.rdoCalcTrue.Name = "rdoCalcTrue";
            this.rdoCalcTrue.Size = new System.Drawing.Size(52, 21);
            this.rdoCalcTrue.TabIndex = 24;
            this.rdoCalcTrue.TabStop = true;
            this.rdoCalcTrue.Text = "評分";
            this.rdoCalcTrue.UseVisualStyleBackColor = true;
            this.rdoCalcTrue.CheckedChanged += new System.EventHandler(this.rdoCalcTrue_CheckedChanged);
            // 
            // rdoCalcFalse
            // 
            this.rdoCalcFalse.AutoSize = true;
            this.rdoCalcFalse.Location = new System.Drawing.Point(66, 2);
            this.rdoCalcFalse.Name = "rdoCalcFalse";
            this.rdoCalcFalse.Size = new System.Drawing.Size(65, 21);
            this.rdoCalcFalse.TabIndex = 24;
            this.rdoCalcFalse.TabStop = true;
            this.rdoCalcFalse.Text = "不評分";
            this.rdoCalcFalse.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoCalcTrue);
            this.panel1.Controls.Add(this.rdoCalcFalse);
            this.panel1.Location = new System.Drawing.Point(369, 165);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 25);
            this.panel1.TabIndex = 25;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdoCreditTrue);
            this.panel2.Controls.Add(this.rdoCreditFalse);
            this.panel2.Location = new System.Drawing.Point(121, 165);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 25);
            this.panel2.TabIndex = 25;
            // 
            // rdoCreditTrue
            // 
            this.rdoCreditTrue.AutoSize = true;
            this.rdoCreditTrue.Location = new System.Drawing.Point(7, 2);
            this.rdoCreditTrue.Name = "rdoCreditTrue";
            this.rdoCreditTrue.Size = new System.Drawing.Size(52, 21);
            this.rdoCreditTrue.TabIndex = 24;
            this.rdoCreditTrue.TabStop = true;
            this.rdoCreditTrue.Text = "計入";
            this.rdoCreditTrue.UseVisualStyleBackColor = true;
            this.rdoCreditTrue.CheckedChanged += new System.EventHandler(this.rdoCreditTrue_CheckedChanged);
            // 
            // rdoCreditFalse
            // 
            this.rdoCreditFalse.AutoSize = true;
            this.rdoCreditFalse.Location = new System.Drawing.Point(66, 2);
            this.rdoCreditFalse.Name = "rdoCreditFalse";
            this.rdoCreditFalse.Size = new System.Drawing.Size(65, 21);
            this.rdoCreditFalse.TabIndex = 24;
            this.rdoCreditFalse.TabStop = true;
            this.rdoCreditFalse.Text = "不計入";
            this.rdoCreditFalse.UseVisualStyleBackColor = true;
            // 
            // BasicInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelX13);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.btnTeachers);
            this.Controls.Add(this.cboSubjectLevel);
            this.Controls.Add(this.cboClass);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.cboSchoolYear);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.cboEntry);
            this.Controls.Add(this.cboRequiredBy);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cboExamTemplate);
            this.Controls.Add(this.cboRequired);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.cboMultiTeacher);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtCourseName);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtCredit);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(550, 0);
            this.Name = "BasicInfo";
            this.Size = new System.Drawing.Size(550, 235);
            this.DoubleClick += new System.EventHandler(this.BasicInfo_DoubleClick);
            this.Controls.SetChildIndex(this.txtCredit, 0);
            this.Controls.SetChildIndex(this.txtSubject, 0);
            this.Controls.SetChildIndex(this.txtCourseName, 0);
            this.Controls.SetChildIndex(this.labelX2, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.labelX4, 0);
            this.Controls.SetChildIndex(this.cboMultiTeacher, 0);
            this.Controls.SetChildIndex(this.labelX5, 0);
            this.Controls.SetChildIndex(this.labelX6, 0);
            this.Controls.SetChildIndex(this.labelX7, 0);
            this.Controls.SetChildIndex(this.cboRequired, 0);
            this.Controls.SetChildIndex(this.cboExamTemplate, 0);
            this.Controls.SetChildIndex(this.labelX3, 0);
            this.Controls.SetChildIndex(this.cboRequiredBy, 0);
            this.Controls.SetChildIndex(this.cboEntry, 0);
            this.Controls.SetChildIndex(this.cboSemester, 0);
            this.Controls.SetChildIndex(this.cboSchoolYear, 0);
            this.Controls.SetChildIndex(this.labelX10, 0);
            this.Controls.SetChildIndex(this.cboClass, 0);
            this.Controls.SetChildIndex(this.cboSubjectLevel, 0);
            this.Controls.SetChildIndex(this.btnTeachers, 0);
            this.Controls.SetChildIndex(this.labelX11, 0);
            this.Controls.SetChildIndex(this.labelX9, 0);
            this.Controls.SetChildIndex(this.labelX8, 0);
            this.Controls.SetChildIndex(this.labelX12, 0);
            this.Controls.SetChildIndex(this.labelX13, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.picWaiting, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        protected DevComponents.DotNetBar.Controls.TextBoxX txtCourseName;
        protected DevComponents.DotNetBar.Controls.TextBoxX txtSubject;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSubjectLevel;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX7;
        protected DevComponents.DotNetBar.Controls.TextBoxX txtCredit;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboClass;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboEntry;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboExamTemplate;
        private DevComponents.Editors.ComboItem comboItem18;
        private DevComponents.Editors.ComboItem comboItem19;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.ButtonX btnTeachers;
        private DevComponents.DotNetBar.ButtonItem btnTeacher1;
        private DevComponents.DotNetBar.ButtonItem btnTeacher2;
        private DevComponents.DotNetBar.ButtonItem btnTeacher3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboMultiTeacher;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboRequired;
        private DevComponents.Editors.ComboItem comboItem20;
        private DevComponents.Editors.ComboItem comboItem21;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboRequiredBy;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.Editors.ComboItem comboItem24;
        private DevComponents.Editors.ComboItem comboItem25;
        private DevComponents.Editors.ComboItem comboItem22;
        private DevComponents.Editors.ComboItem comboItem23;
        private System.Windows.Forms.RadioButton rdoCalcTrue;
        private System.Windows.Forms.RadioButton rdoCalcFalse;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoCreditTrue;
        private System.Windows.Forms.RadioButton rdoCreditFalse;

    }
}
