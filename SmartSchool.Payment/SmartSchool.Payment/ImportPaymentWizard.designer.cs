namespace SmartSchool.Payment
{
    partial class ImportPaymentWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportPaymentWizard));
            this.SelectSourceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ImportWizard = new DevComponents.DotNetBar.Wizard();
            this.wpSelectTargetPayment = new DevComponents.DotNetBar.WizardPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboPaymentList = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.lblSemester = new DevComponents.DotNetBar.LabelX();
            this.lblSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.lblPaymentDescritpion = new DevComponents.DotNetBar.LabelX();
            this.lblImport = new DevComponents.DotNetBar.LabelX();
            this.label4 = new System.Windows.Forms.Label();
            this.wpSelectFileAndAction = new DevComponents.DotNetBar.WizardPage();
            this.pUser = new System.Windows.Forms.Panel();
            this.cboSheetList = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.chkInsert = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtSourceFile = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSelectFile = new DevComponents.DotNetBar.ButtonX();
            this.chkUpdate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblInserDesc = new System.Windows.Forms.Label();
            this.lblUpdateDesc = new System.Windows.Forms.Label();
            this.pProgram = new System.Windows.Forms.Panel();
            this.lblCollectMsg = new DevComponents.DotNetBar.LabelX();
            this.wpCollectKeyInfo = new DevComponents.DotNetBar.WizardPage();
            this.lblValidDesc = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboValidateField = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblValid = new System.Windows.Forms.Label();
            this.cboIdField = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblIdField = new System.Windows.Forms.Label();
            this.wpValidation = new DevComponents.DotNetBar.WizardPage();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.chkSepErrors = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblCorrectCount = new DevComponents.DotNetBar.LabelX();
            this.lblWarningCount = new DevComponents.DotNetBar.LabelX();
            this.lblErrorCount = new DevComponents.DotNetBar.LabelX();
            this.lblCorrect = new DevComponents.DotNetBar.LabelX();
            this.lblContinueMsg = new DevComponents.DotNetBar.LabelX();
            this.chkContinue = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblWarning = new DevComponents.DotNetBar.LabelX();
            this.lblError = new DevComponents.DotNetBar.LabelX();
            this.btnViewResult = new DevComponents.DotNetBar.ButtonX();
            this.btnValidate = new DevComponents.DotNetBar.ButtonX();
            this.lnkCancelValid = new System.Windows.Forms.LinkLabel();
            this.lblValidMsg = new System.Windows.Forms.Label();
            this.pgValidProgress = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.wpImport = new DevComponents.DotNetBar.WizardPage();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.lnkCancelImport = new System.Windows.Forms.LinkLabel();
            this.lblImportProgress = new System.Windows.Forms.Label();
            this.pgImport = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.ImportWizard.SuspendLayout();
            this.wpSelectTargetPayment.SuspendLayout();
            this.panel1.SuspendLayout();
            this.wpSelectFileAndAction.SuspendLayout();
            this.pUser.SuspendLayout();
            this.pProgram.SuspendLayout();
            this.wpCollectKeyInfo.SuspendLayout();
            this.wpValidation.SuspendLayout();
            this.wpImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectSourceFileDialog
            // 
            this.SelectSourceFileDialog.Filter = "Excel 檔案 (*.xls)| *.xls";
            // 
            // ImportWizard
            // 
            this.ImportWizard.BackButtonText = "< 上一步";
            this.ImportWizard.BackColor = System.Drawing.Color.Transparent;
            this.ImportWizard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ImportWizard.BackgroundImage")));
            this.ImportWizard.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007;
            this.ImportWizard.CancelButtonText = "取消";
            this.ImportWizard.Cursor = System.Windows.Forms.Cursors.Default;
            this.ImportWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImportWizard.FinishButtonTabIndex = 3;
            this.ImportWizard.FinishButtonText = "完成";
            // 
            // 
            // 
            this.ImportWizard.FooterStyle.BackColor = System.Drawing.Color.Transparent;
            this.ImportWizard.FooterStyle.BackColor2 = System.Drawing.Color.Transparent;
            this.ImportWizard.FooterStyle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ImportWizard.FooterStyle.BackgroundImage")));
            this.ImportWizard.FooterStyle.BorderBottomWidth = 1;
            this.ImportWizard.FooterStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground2;
            this.ImportWizard.FooterStyle.BorderLeftWidth = 1;
            this.ImportWizard.FooterStyle.BorderRightWidth = 1;
            this.ImportWizard.FooterStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ImportWizard.FooterStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText;
            this.ImportWizard.FooterStyle.BorderTopWidth = 1;
            this.ImportWizard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
            this.ImportWizard.HeaderCaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportWizard.HeaderImage = ((System.Drawing.Image)(resources.GetObject("ImportWizard.HeaderImage")));
            this.ImportWizard.HeaderImageSize = new System.Drawing.Size(30, 30);
            // 
            // 
            // 
            this.ImportWizard.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.ImportWizard.HeaderStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.ImportWizard.HeaderStyle.BackColorGradientAngle = 90;
            this.ImportWizard.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ImportWizard.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(157)))), ((int)(((byte)(182)))));
            this.ImportWizard.HeaderStyle.BorderBottomWidth = 1;
            this.ImportWizard.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.ImportWizard.HeaderStyle.BorderLeftWidth = 1;
            this.ImportWizard.HeaderStyle.BorderRightWidth = 1;
            this.ImportWizard.HeaderStyle.BorderTopWidth = 1;
            this.ImportWizard.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ImportWizard.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.ImportWizard.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ImportWizard.Location = new System.Drawing.Point(0, 0);
            this.ImportWizard.Name = "ImportWizard";
            this.ImportWizard.NextButtonText = "下一步 >";
            this.ImportWizard.Size = new System.Drawing.Size(542, 423);
            this.ImportWizard.TabIndex = 0;
            this.ImportWizard.WizardPages.AddRange(new DevComponents.DotNetBar.WizardPage[] {
            this.wpSelectTargetPayment,
            this.wpSelectFileAndAction,
            this.wpCollectKeyInfo,
            this.wpValidation,
            this.wpImport});
            this.ImportWizard.Load += new System.EventHandler(this.ImportWizard_Load);
            // 
            // wpSelectTargetPayment
            // 
            this.wpSelectTargetPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wpSelectTargetPayment.AntiAlias = false;
            this.wpSelectTargetPayment.BackColor = System.Drawing.Color.Transparent;
            this.wpSelectTargetPayment.Controls.Add(this.panel1);
            this.wpSelectTargetPayment.HelpButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wpSelectTargetPayment.Location = new System.Drawing.Point(7, 72);
            this.wpSelectTargetPayment.Name = "wpSelectTargetPayment";
            this.wpSelectTargetPayment.PageDescription = "您可以選擇現有「收費」或是選擇「新增」建立新的「收費」。";
            this.wpSelectTargetPayment.PageTitle = "選擇收費";
            this.wpSelectTargetPayment.Size = new System.Drawing.Size(528, 293);
            this.wpSelectTargetPayment.TabIndex = 12;
            this.wpSelectTargetPayment.NextButtonClick += new System.ComponentModel.CancelEventHandler(this.wpSelectTargetPayment_NextButtonClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.cboPaymentList);
            this.panel1.Controls.Add(this.cboSchoolYear);
            this.panel1.Controls.Add(this.cboSemester);
            this.panel1.Controls.Add(this.lblSemester);
            this.panel1.Controls.Add(this.lblSchoolYear);
            this.panel1.Controls.Add(this.lblPaymentDescritpion);
            this.panel1.Controls.Add(this.lblImport);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(53, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 277);
            this.panel1.TabIndex = 29;
            // 
            // cboPaymentList
            // 
            this.cboPaymentList.DisplayMember = "Text";
            this.cboPaymentList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPaymentList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentList.FormattingEnabled = true;
            this.cboPaymentList.ItemHeight = 19;
            this.cboPaymentList.Location = new System.Drawing.Point(33, 169);
            this.cboPaymentList.Name = "cboPaymentList";
            this.cboPaymentList.Size = new System.Drawing.Size(339, 25);
            this.cboPaymentList.TabIndex = 0;
            this.cboPaymentList.Validating += new System.ComponentModel.CancelEventHandler(this.cboPaymentList_Validating);
            this.cboPaymentList.SelectedIndexChanged += new System.EventHandler(this.cboPaymentList_SelectedIndexChanged);
            this.cboPaymentList.Validated += new System.EventHandler(this.cboPaymentList_Validated);
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.Enabled = false;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.ItemHeight = 19;
            this.cboSchoolYear.Location = new System.Drawing.Point(82, 109);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(96, 25);
            this.cboSchoolYear.TabIndex = 39;
            this.cboSchoolYear.TextChanged += new System.EventHandler(this.cboSchoolYear_TextChanged);
            // 
            // cboSemester
            // 
            this.cboSemester.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.Enabled = false;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 19;
            this.cboSemester.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboSemester.Location = new System.Drawing.Point(242, 109);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(96, 25);
            this.cboSemester.TabIndex = 38;
            this.cboSemester.TextChanged += new System.EventHandler(this.cboSemester_TextChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "2";
            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.Location = new System.Drawing.Point(202, 112);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(34, 19);
            this.lblSemester.TabIndex = 37;
            this.lblSemester.Text = "學期";
            // 
            // lblSchoolYear
            // 
            this.lblSchoolYear.AutoSize = true;
            this.lblSchoolYear.Location = new System.Drawing.Point(33, 112);
            this.lblSchoolYear.Name = "lblSchoolYear";
            this.lblSchoolYear.Size = new System.Drawing.Size(47, 19);
            this.lblSchoolYear.TabIndex = 37;
            this.lblSchoolYear.Text = "學年度";
            // 
            // lblPaymentDescritpion
            // 
            this.lblPaymentDescritpion.Location = new System.Drawing.Point(32, 23);
            this.lblPaymentDescritpion.Name = "lblPaymentDescritpion";
            this.lblPaymentDescritpion.Size = new System.Drawing.Size(367, 81);
            this.lblPaymentDescritpion.TabIndex = 34;
            this.lblPaymentDescritpion.Text = "匯入資料時，一次只能將資料匯入到一個收費中，您必須指定要將資料匯入到哪個收費中。如果要將資料匯入到已存在的收費，請在清單中選擇，如果要將資料匯入到新收費中，請選擇" +
                "「(新增...)」項目，並輸入新收費名稱。";
            this.lblPaymentDescritpion.WordWrap = true;
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Location = new System.Drawing.Point(32, 149);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(87, 19);
            this.lblImport.TabIndex = 33;
            this.lblImport.Text = "選擇收費名稱";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(5, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 17);
            this.label4.TabIndex = 0;
            // 
            // wpSelectFileAndAction
            // 
            this.wpSelectFileAndAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wpSelectFileAndAction.AntiAlias = false;
            this.wpSelectFileAndAction.BackColor = System.Drawing.Color.Transparent;
            this.wpSelectFileAndAction.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.wpSelectFileAndAction.Controls.Add(this.pUser);
            this.wpSelectFileAndAction.Controls.Add(this.pProgram);
            this.wpSelectFileAndAction.HelpButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wpSelectFileAndAction.Location = new System.Drawing.Point(7, 72);
            this.wpSelectFileAndAction.Name = "wpSelectFileAndAction";
            this.wpSelectFileAndAction.PageDescription = "選擇檔案與決定要用何種方式匯入資料。";
            this.wpSelectFileAndAction.PageTitle = "選擇檔案與匯入方式";
            this.wpSelectFileAndAction.Size = new System.Drawing.Size(528, 293);
            this.wpSelectFileAndAction.TabIndex = 0;
            this.wpSelectFileAndAction.BeforePageDisplayed += new DevComponents.DotNetBar.WizardCancelPageChangeEventHandler(this.wpSelectFileAndAction_BeforePageDisplayed);
            this.wpSelectFileAndAction.NextButtonClick += new System.ComponentModel.CancelEventHandler(this.wpSelectFileAndAction_NextButtonClick);
            // 
            // pUser
            // 
            this.pUser.BackColor = System.Drawing.Color.Transparent;
            this.pUser.Controls.Add(this.cboSheetList);
            this.pUser.Controls.Add(this.label1);
            this.pUser.Controls.Add(this.chkInsert);
            this.pUser.Controls.Add(this.txtSourceFile);
            this.pUser.Controls.Add(this.label8);
            this.pUser.Controls.Add(this.label7);
            this.pUser.Controls.Add(this.btnSelectFile);
            this.pUser.Controls.Add(this.chkUpdate);
            this.pUser.Controls.Add(this.lblInserDesc);
            this.pUser.Controls.Add(this.lblUpdateDesc);
            this.pUser.Location = new System.Drawing.Point(53, 8);
            this.pUser.Name = "pUser";
            this.pUser.Size = new System.Drawing.Size(422, 277);
            this.pUser.TabIndex = 28;
            // 
            // cboSheetList
            // 
            this.cboSheetList.DisplayMember = "Text";
            this.cboSheetList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSheetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSheetList.FormattingEnabled = true;
            this.cboSheetList.ItemHeight = 19;
            this.cboSheetList.Location = new System.Drawing.Point(18, 87);
            this.cboSheetList.Name = "cboSheetList";
            this.cboSheetList.Size = new System.Drawing.Size(185, 25);
            this.cboSheetList.TabIndex = 37;
            this.cboSheetList.SelectedIndexChanged += new System.EventHandler(this.cboSheetList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(5, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 36;
            this.label1.Text = "● 選擇工作表";
            // 
            // chkInsert
            // 
            this.chkInsert.AutoSize = true;
            this.chkInsert.BackColor = System.Drawing.Color.Transparent;
            this.chkInsert.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkInsert.Location = new System.Drawing.Point(15, 142);
            this.chkInsert.Name = "chkInsert";
            this.chkInsert.Size = new System.Drawing.Size(106, 21);
            this.chkInsert.TabIndex = 28;
            this.chkInsert.Text = "新增收費明細";
            this.chkInsert.CheckedChanged += new System.EventHandler(this.chkInsert_CheckedChanged);
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSourceFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtSourceFile.Border.Class = "TextBoxBorder";
            this.txtSourceFile.Location = new System.Drawing.Point(18, 32);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.ReadOnly = true;
            this.txtSourceFile.Size = new System.Drawing.Size(345, 25);
            this.txtSourceFile.TabIndex = 1;
            this.txtSourceFile.WatermarkText = "請選擇檔案位置";
            this.txtSourceFile.TextChanged += new System.EventHandler(this.txtSourceFile_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(6, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "● 選擇匯入方式";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(5, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "● 選擇來源檔案(匯入來源)";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFile.Location = new System.Drawing.Point(369, 32);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(29, 22);
            this.btnSelectFile.TabIndex = 35;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // chkUpdate
            // 
            this.chkUpdate.AutoSize = true;
            this.chkUpdate.BackColor = System.Drawing.Color.Transparent;
            this.chkUpdate.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkUpdate.Location = new System.Drawing.Point(15, 211);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(106, 21);
            this.chkUpdate.TabIndex = 29;
            this.chkUpdate.Text = "更新收費明細";
            this.chkUpdate.CheckedChanged += new System.EventHandler(this.chkUpdate_CheckedChanged);
            // 
            // lblInserDesc
            // 
            this.lblInserDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblInserDesc.Location = new System.Drawing.Point(34, 162);
            this.lblInserDesc.Name = "lblInserDesc";
            this.lblInserDesc.Size = new System.Drawing.Size(338, 37);
            this.lblInserDesc.TabIndex = 30;
            this.lblInserDesc.Text = "此選項是將所有資料新增到資料庫中，不會對現有的資料進行任何修改動作。";
            this.lblInserDesc.Click += new System.EventHandler(this.lblInserDesc_Click);
            // 
            // lblUpdateDesc
            // 
            this.lblUpdateDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdateDesc.Location = new System.Drawing.Point(34, 232);
            this.lblUpdateDesc.Name = "lblUpdateDesc";
            this.lblUpdateDesc.Size = new System.Drawing.Size(338, 40);
            this.lblUpdateDesc.TabIndex = 31;
            this.lblUpdateDesc.Text = "此選項將修改資料庫中的現有資料，會依據您所指定的識別欄修改資料庫中具有相同識別的資料。";
            this.lblUpdateDesc.Click += new System.EventHandler(this.lblUpdateDesc_Click);
            // 
            // pProgram
            // 
            this.pProgram.BackColor = System.Drawing.Color.Transparent;
            this.pProgram.Controls.Add(this.lblCollectMsg);
            this.pProgram.Location = new System.Drawing.Point(53, 8);
            this.pProgram.Name = "pProgram";
            this.pProgram.Size = new System.Drawing.Size(422, 277);
            this.pProgram.TabIndex = 0;
            this.pProgram.Visible = false;
            // 
            // lblCollectMsg
            // 
            this.lblCollectMsg.Location = new System.Drawing.Point(40, 89);
            this.lblCollectMsg.Name = "lblCollectMsg";
            this.lblCollectMsg.Size = new System.Drawing.Size(343, 99);
            this.lblCollectMsg.TabIndex = 1;
            this.lblCollectMsg.Text = "訊息…";
            // 
            // wpCollectKeyInfo
            // 
            this.wpCollectKeyInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wpCollectKeyInfo.AntiAlias = false;
            this.wpCollectKeyInfo.BackColor = System.Drawing.Color.Transparent;
            this.wpCollectKeyInfo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.wpCollectKeyInfo.Controls.Add(this.lblValidDesc);
            this.wpCollectKeyInfo.Controls.Add(this.label9);
            this.wpCollectKeyInfo.Controls.Add(this.cboValidateField);
            this.wpCollectKeyInfo.Controls.Add(this.lblValid);
            this.wpCollectKeyInfo.Controls.Add(this.cboIdField);
            this.wpCollectKeyInfo.Controls.Add(this.lblIdField);
            this.wpCollectKeyInfo.HelpButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wpCollectKeyInfo.Location = new System.Drawing.Point(7, 72);
            this.wpCollectKeyInfo.Name = "wpCollectKeyInfo";
            this.wpCollectKeyInfo.PageDescription = "識別欄是更新資料的依據，驗證欄則是幫助檢查資料合理性。";
            this.wpCollectKeyInfo.PageTitle = "選擇識別欄與驗證欄";
            this.wpCollectKeyInfo.Size = new System.Drawing.Size(528, 293);
            this.wpCollectKeyInfo.TabIndex = 8;
            this.wpCollectKeyInfo.BeforePageDisplayed += new DevComponents.DotNetBar.WizardCancelPageChangeEventHandler(this.wpCollectKeyInfo_BeforePageDisplayed);
            this.wpCollectKeyInfo.NextButtonClick += new System.ComponentModel.CancelEventHandler(this.wpCollectKeyInfo_NextButtonClick);
            // 
            // lblValidDesc
            // 
            this.lblValidDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblValidDesc.Enabled = false;
            this.lblValidDesc.Location = new System.Drawing.Point(64, 214);
            this.lblValidDesc.Name = "lblValidDesc";
            this.lblValidDesc.Size = new System.Drawing.Size(396, 41);
            this.lblValidDesc.TabIndex = 23;
            this.lblValidDesc.Text = "此欄位是用來防止因操作而產生資料錯誤的問題。此欄位的內容不會被匯入資料庫中，只會拿來檢查資料用。";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(63, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(397, 58);
            this.label9.TabIndex = 22;
            this.label9.Text = "此欄位是系統用來識別資料用，在來源資料中必須是唯一的，不可重覆的。目前系統只提供三種欄位可當作識別：學生系統編號、身分證號、學號。";
            // 
            // cboValidateField
            // 
            this.cboValidateField.DisplayMember = "Text";
            this.cboValidateField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboValidateField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboValidateField.Enabled = false;
            this.cboValidateField.FormattingEnabled = true;
            this.cboValidateField.ItemHeight = 19;
            this.cboValidateField.Location = new System.Drawing.Point(68, 183);
            this.cboValidateField.Name = "cboValidateField";
            this.cboValidateField.Size = new System.Drawing.Size(203, 25);
            this.cboValidateField.TabIndex = 21;
            // 
            // lblValid
            // 
            this.lblValid.AutoSize = true;
            this.lblValid.BackColor = System.Drawing.Color.Transparent;
            this.lblValid.Enabled = false;
            this.lblValid.Location = new System.Drawing.Point(64, 162);
            this.lblValid.Name = "lblValid";
            this.lblValid.Size = new System.Drawing.Size(60, 17);
            this.lblValid.TabIndex = 20;
            this.lblValid.Text = "驗證欄位";
            // 
            // cboIdField
            // 
            this.cboIdField.DisplayMember = "Text";
            this.cboIdField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboIdField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIdField.FormattingEnabled = true;
            this.cboIdField.ItemHeight = 19;
            this.cboIdField.Location = new System.Drawing.Point(67, 35);
            this.cboIdField.Name = "cboIdField";
            this.cboIdField.Size = new System.Drawing.Size(203, 25);
            this.cboIdField.TabIndex = 19;
            // 
            // lblIdField
            // 
            this.lblIdField.AutoSize = true;
            this.lblIdField.BackColor = System.Drawing.Color.Transparent;
            this.lblIdField.Location = new System.Drawing.Point(63, 14);
            this.lblIdField.Name = "lblIdField";
            this.lblIdField.Size = new System.Drawing.Size(60, 17);
            this.lblIdField.TabIndex = 18;
            this.lblIdField.Text = "識別欄位";
            // 
            // wpValidation
            // 
            this.wpValidation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wpValidation.AntiAlias = false;
            this.wpValidation.BackColor = System.Drawing.Color.Transparent;
            this.wpValidation.Controls.Add(this.buttonX1);
            this.wpValidation.Controls.Add(this.chkSepErrors);
            this.wpValidation.Controls.Add(this.lblCorrectCount);
            this.wpValidation.Controls.Add(this.lblWarningCount);
            this.wpValidation.Controls.Add(this.lblErrorCount);
            this.wpValidation.Controls.Add(this.lblCorrect);
            this.wpValidation.Controls.Add(this.lblContinueMsg);
            this.wpValidation.Controls.Add(this.chkContinue);
            this.wpValidation.Controls.Add(this.lblWarning);
            this.wpValidation.Controls.Add(this.lblError);
            this.wpValidation.Controls.Add(this.btnViewResult);
            this.wpValidation.Controls.Add(this.btnValidate);
            this.wpValidation.Controls.Add(this.lnkCancelValid);
            this.wpValidation.Controls.Add(this.lblValidMsg);
            this.wpValidation.Controls.Add(this.pgValidProgress);
            this.wpValidation.HelpButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wpValidation.Location = new System.Drawing.Point(7, 72);
            this.wpValidation.Name = "wpValidation";
            this.wpValidation.PageDescription = "程式會修改檔案內容，如果您要保存原來資料，請備份原始檔案。";
            this.wpValidation.PageTitle = "檢查資料正確性";
            this.wpValidation.Size = new System.Drawing.Size(528, 293);
            this.wpValidation.TabIndex = 10;
            this.wpValidation.AfterPageDisplayed += new DevComponents.DotNetBar.WizardPageChangeEventHandler(this.wpValidation_AfterPageDisplayed);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Enabled = false;
            this.buttonX1.Location = new System.Drawing.Point(262, 199);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 25;
            this.buttonX1.Text = "建立備份";
            this.buttonX1.Tooltip = "功能未完成…";
            // 
            // chkSepErrors
            // 
            this.chkSepErrors.Location = new System.Drawing.Point(95, 228);
            this.chkSepErrors.Name = "chkSepErrors";
            this.chkSepErrors.Size = new System.Drawing.Size(242, 23);
            this.chkSepErrors.TabIndex = 24;
            this.chkSepErrors.Text = "將錯誤資料複制到另一工作表";
            this.chkSepErrors.Visible = false;
            // 
            // lblCorrectCount
            // 
            this.lblCorrectCount.AutoSize = true;
            this.lblCorrectCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCorrectCount.Location = new System.Drawing.Point(166, 162);
            this.lblCorrectCount.Name = "lblCorrectCount";
            this.lblCorrectCount.Size = new System.Drawing.Size(15, 19);
            this.lblCorrectCount.TabIndex = 21;
            this.lblCorrectCount.Text = "0";
            // 
            // lblWarningCount
            // 
            this.lblWarningCount.AutoSize = true;
            this.lblWarningCount.BackColor = System.Drawing.Color.Transparent;
            this.lblWarningCount.Location = new System.Drawing.Point(166, 125);
            this.lblWarningCount.Name = "lblWarningCount";
            this.lblWarningCount.Size = new System.Drawing.Size(15, 19);
            this.lblWarningCount.TabIndex = 20;
            this.lblWarningCount.Text = "0";
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.AutoSize = true;
            this.lblErrorCount.BackColor = System.Drawing.Color.Transparent;
            this.lblErrorCount.Location = new System.Drawing.Point(166, 88);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new System.Drawing.Size(15, 19);
            this.lblErrorCount.TabIndex = 19;
            this.lblErrorCount.Text = "0";
            // 
            // lblCorrect
            // 
            this.lblCorrect.BackColor = System.Drawing.Color.Transparent;
            this.lblCorrect.Location = new System.Drawing.Point(95, 159);
            this.lblCorrect.Name = "lblCorrect";
            this.lblCorrect.Size = new System.Drawing.Size(78, 23);
            this.lblCorrect.TabIndex = 18;
            this.lblCorrect.Text = "自動修正：";
            // 
            // lblContinueMsg
            // 
            this.lblContinueMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblContinueMsg.Location = new System.Drawing.Point(115, 260);
            this.lblContinueMsg.Name = "lblContinueMsg";
            this.lblContinueMsg.Size = new System.Drawing.Size(147, 23);
            this.lblContinueMsg.TabIndex = 23;
            this.lblContinueMsg.Text = "略過警告，強制匯入。";
            this.lblContinueMsg.Visible = false;
            // 
            // chkContinue
            // 
            this.chkContinue.BackColor = System.Drawing.Color.Transparent;
            this.chkContinue.Location = new System.Drawing.Point(95, 260);
            this.chkContinue.Name = "chkContinue";
            this.chkContinue.Size = new System.Drawing.Size(21, 23);
            this.chkContinue.TabIndex = 22;
            this.chkContinue.Visible = false;
            // 
            // lblWarning
            // 
            this.lblWarning.BackColor = System.Drawing.Color.Transparent;
            this.lblWarning.Location = new System.Drawing.Point(96, 122);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(78, 23);
            this.lblWarning.TabIndex = 17;
            this.lblWarning.Text = "提示數量：";
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Location = new System.Drawing.Point(96, 85);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(78, 23);
            this.lblError.TabIndex = 16;
            this.lblError.Text = "錯誤數量：";
            // 
            // btnViewResult
            // 
            this.btnViewResult.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewResult.Location = new System.Drawing.Point(177, 199);
            this.btnViewResult.Name = "btnViewResult";
            this.btnViewResult.Size = new System.Drawing.Size(75, 23);
            this.btnViewResult.TabIndex = 14;
            this.btnViewResult.Text = "檢視結果";
            this.btnViewResult.Click += new System.EventHandler(this.btnViewResult_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnValidate.Location = new System.Drawing.Point(96, 199);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 15;
            this.btnValidate.Text = "開始驗證";
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // lnkCancelValid
            // 
            this.lnkCancelValid.AutoSize = true;
            this.lnkCancelValid.BackColor = System.Drawing.Color.Transparent;
            this.lnkCancelValid.Location = new System.Drawing.Point(369, 36);
            this.lnkCancelValid.Name = "lnkCancelValid";
            this.lnkCancelValid.Size = new System.Drawing.Size(60, 17);
            this.lnkCancelValid.TabIndex = 13;
            this.lnkCancelValid.TabStop = true;
            this.lnkCancelValid.Text = "取消檢查";
            this.lnkCancelValid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkCancelValid.Visible = false;
            this.lnkCancelValid.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // lblValidMsg
            // 
            this.lblValidMsg.AutoSize = true;
            this.lblValidMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblValidMsg.Location = new System.Drawing.Point(98, 37);
            this.lblValidMsg.Name = "lblValidMsg";
            this.lblValidMsg.Size = new System.Drawing.Size(86, 17);
            this.lblValidMsg.TabIndex = 12;
            this.lblValidMsg.Text = "檢查資料進度";
            this.lblValidMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgValidProgress
            // 
            this.pgValidProgress.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pgValidProgress.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.pgValidProgress.Location = new System.Drawing.Point(96, 56);
            this.pgValidProgress.Name = "pgValidProgress";
            this.pgValidProgress.Size = new System.Drawing.Size(336, 23);
            this.pgValidProgress.TabIndex = 11;
            // 
            // wpImport
            // 
            this.wpImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wpImport.AntiAlias = false;
            this.wpImport.BackColor = System.Drawing.Color.Transparent;
            this.wpImport.Controls.Add(this.btnImport);
            this.wpImport.Controls.Add(this.lnkCancelImport);
            this.wpImport.Controls.Add(this.lblImportProgress);
            this.wpImport.Controls.Add(this.pgImport);
            this.wpImport.FinishButtonEnabled = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wpImport.HelpButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wpImport.Location = new System.Drawing.Point(7, 72);
            this.wpImport.Name = "wpImport";
            this.wpImport.PageDescription = "將資料匯入到資料庫中";
            this.wpImport.PageTitle = "匯入資料";
            this.wpImport.Size = new System.Drawing.Size(528, 293);
            this.wpImport.TabIndex = 11;
            this.wpImport.BeforePageDisplayed += new DevComponents.DotNetBar.WizardCancelPageChangeEventHandler(this.wpImport_BeforePageDisplayed);
            this.wpImport.FinishButtonClick += new System.ComponentModel.CancelEventHandler(this.wpImport_FinishButtonClick);
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.Location = new System.Drawing.Point(357, 130);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 17;
            this.btnImport.Text = "開始匯入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lnkCancelImport
            // 
            this.lnkCancelImport.AutoSize = true;
            this.lnkCancelImport.BackColor = System.Drawing.Color.Transparent;
            this.lnkCancelImport.Location = new System.Drawing.Point(369, 81);
            this.lnkCancelImport.Name = "lnkCancelImport";
            this.lnkCancelImport.Size = new System.Drawing.Size(60, 17);
            this.lnkCancelImport.TabIndex = 16;
            this.lnkCancelImport.TabStop = true;
            this.lnkCancelImport.Text = "取消匯入";
            this.lnkCancelImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkCancelImport.Visible = false;
            this.lnkCancelImport.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // lblImportProgress
            // 
            this.lblImportProgress.AutoSize = true;
            this.lblImportProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblImportProgress.Location = new System.Drawing.Point(98, 82);
            this.lblImportProgress.Name = "lblImportProgress";
            this.lblImportProgress.Size = new System.Drawing.Size(86, 17);
            this.lblImportProgress.TabIndex = 15;
            this.lblImportProgress.Text = "資料匯入進度";
            this.lblImportProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgImport
            // 
            this.pgImport.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pgImport.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.pgImport.Location = new System.Drawing.Point(96, 101);
            this.pgImport.Name = "pgImport";
            this.pgImport.Size = new System.Drawing.Size(336, 23);
            this.pgImport.TabIndex = 14;
            this.pgImport.Value = 75;
            // 
            // ImportPaymentWizard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(542, 423);
            this.ControlBox = false;
            this.Controls.Add(this.ImportWizard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ImportPaymentWizard";
            this.Text = "匯入收費資料";
            this.ImportWizard.ResumeLayout(false);
            this.wpSelectTargetPayment.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.wpSelectFileAndAction.ResumeLayout(false);
            this.pUser.ResumeLayout(false);
            this.pUser.PerformLayout();
            this.pProgram.ResumeLayout(false);
            this.wpCollectKeyInfo.ResumeLayout(false);
            this.wpCollectKeyInfo.PerformLayout();
            this.wpValidation.ResumeLayout(false);
            this.wpValidation.PerformLayout();
            this.wpImport.ResumeLayout(false);
            this.wpImport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Wizard ImportWizard;
        private DevComponents.DotNetBar.WizardPage wpSelectFileAndAction;
        private System.Windows.Forms.OpenFileDialog SelectSourceFileDialog;
        private DevComponents.DotNetBar.WizardPage wpCollectKeyInfo;
        private System.Windows.Forms.Label lblValidDesc;
        private System.Windows.Forms.Label label9;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cboValidateField;
        private System.Windows.Forms.Label lblValid;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cboIdField;
        private System.Windows.Forms.Label lblIdField;
        private DevComponents.DotNetBar.WizardPage wpValidation;
        private System.Windows.Forms.Panel pUser;
        public DevComponents.DotNetBar.Controls.CheckBoxX chkInsert;
        public DevComponents.DotNetBar.Controls.TextBoxX txtSourceFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        public DevComponents.DotNetBar.ButtonX btnSelectFile;
        public DevComponents.DotNetBar.Controls.CheckBoxX chkUpdate;
        public System.Windows.Forms.Label lblInserDesc;
        public System.Windows.Forms.Label lblUpdateDesc;
        private System.Windows.Forms.Panel pProgram;
        private DevComponents.DotNetBar.LabelX lblCollectMsg;
        private DevComponents.DotNetBar.LabelX lblCorrectCount;
        private DevComponents.DotNetBar.LabelX lblWarningCount;
        private DevComponents.DotNetBar.LabelX lblErrorCount;
        private DevComponents.DotNetBar.LabelX lblCorrect;
        private DevComponents.DotNetBar.LabelX lblWarning;
        private DevComponents.DotNetBar.LabelX lblError;
        private DevComponents.DotNetBar.ButtonX btnViewResult;
        private System.Windows.Forms.LinkLabel lnkCancelValid;
        private System.Windows.Forms.Label lblValidMsg;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgValidProgress;
        private DevComponents.DotNetBar.WizardPage wpImport;
        private System.Windows.Forms.LinkLabel lnkCancelImport;
        private System.Windows.Forms.Label lblImportProgress;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgImport;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSheetList;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.WizardPage wpSelectTargetPayment;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboPaymentList;
        private DevComponents.DotNetBar.LabelX lblImport;
        private DevComponents.DotNetBar.LabelX lblPaymentDescritpion;
        private DevComponents.DotNetBar.LabelX lblSemester;
        private DevComponents.DotNetBar.LabelX lblSchoolYear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSepErrors;
        private DevComponents.DotNetBar.LabelX lblContinueMsg;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkContinue;
        private DevComponents.DotNetBar.ButtonX btnValidate;
    }
}