using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SmartSchool.Feature.Student;
using System.Xml;
using SmartSchool.StudentRelated.RibbonBars.Import.BulkModel;
using System.IO;
using Aspose.Cells;
using SmartSchool.StudentRelated.RibbonBars.Import.SheetModel;
using SmartSchool.StudentRelated.RibbonBars.Import.ValidateModel;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using SmartSchool.Feature.Class;
using SmartSchool.ApplicationLog;
using SmartSchool.ImportSupport.Validators;
using SmartSchool.Feature;
using ProgramProcess = System.Diagnostics.Process;
using SmartSchool.Feature.Payment;
using SmartSchool.ImportSupport.Lookups;
using System.Diagnostics;
using SmartSchool.Payment.Interfaces;
using SmartSchool.Payment.BankManagement;

namespace SmartSchool.Payment
{
    internal partial class ImportPaymentWizard : BaseForm
    {
        public enum ImportMode
        {
            None,
            Insert,
            Update
        }

        internal string[] IdentifiableFieldNames = new string[] { "學號", "身分證號", "學生系統編號" };

        public ImportPaymentWizard(string schoolYear, string semester, List<GT.Payment> savedPayments, GT.Payment currentSelected)
        {
            InitializeComponent();

            InitializeFormParameters(schoolYear, semester, savedPayments, currentSelected);
        }

        #region Initialize Form Parameters (此表單啟動時的參數)
        private void cboSchoolYear_TextChanged(object sender, EventArgs e)
        {
            SchoolYear = cboSchoolYear.Text;
        }

        private void cboSemester_TextChanged(object sender, EventArgs e)
        {
            Semester = cboSemester.Text;
        }

        /// <summary>
        /// 初始化相關參數。
        /// </summary>
        private void InitializeFormParameters(string schoolYear, string semester, List<GT.Payment> savedPayments, GT.Payment currentSelected)
        {
            InitializeSemester();

            cboSchoolYear.Text = schoolYear;
            cboSemester.Text = semester;

            SavedPayments = savedPayments;
            CurrentSelectedPayment = currentSelected;
        }

        private void InitializeSemester()
        {
            try
            {
                for (int i = -2; i <= 2; i++)
                {
                    cboSchoolYear.Items.Add(CurrentUser.Instance.SchoolYear + i);
                }
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(new PaymentModuleException("填入學年度學期選項清單時發生錯誤。", ex));
            }
        }

        private List<GT.Payment> _saved_payments;
        /// <summary>
        /// 現存的 Payment 清單。
        /// </summary>
        public List<GT.Payment> SavedPayments
        {
            get { return _saved_payments; }
            set { _saved_payments = value; }
        }

        private string _school_year;

        public string SchoolYear
        {
            get { return _school_year; }
            set { _school_year = value; }
        }

        private string _semester;

        public string Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }

        private GT.Payment _current_selected;
        /// <summary>
        /// 在主畫面中，目前所選擇的 Payment。(非目前要匯入的收費)
        /// </summary>
        public GT.Payment CurrentSelectedPayment
        {
            get { return _current_selected; }
            set { _current_selected = value; }
        }
        #endregion

        private void ImportWizard_Load(object sender, EventArgs e)
        {
            InitializePaymentList();
        }

        #region Method InitializePaymentList
        private void InitializePaymentList()
        {
            cboPaymentList.Items.Clear();
            foreach (GT.Payment each in SavedPayments)
                cboPaymentList.Items.Add(each);

            //增加一個項目，此項目是使用者想要將資料匯入到新收費時使用。
            GT.Payment payment = new GT.Payment();
            payment.Name = "(新增...)";
            payment.SchoolYear = SchoolYear;
            payment.Semester = Semester;

            cboPaymentList.Items.Add(payment);

            if (CurrentSelectedPayment != null)
                cboPaymentList.SelectedItem = CurrentSelectedPayment;
            else
                cboPaymentList.SelectedItem = payment;
        }
        #endregion

        #region Property Mode
        private ImportMode _mode;
        public ImportMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        #endregion

        #region Property WorkbookFileName
        private string _file_name;
        /// <summary>
        /// 工作簿檔案名稱。
        /// </summary>
        public string WorkbookFileName
        {
            get { return _file_name; }
            set { _file_name = value; }
        }
        #endregion

        #region Property ImportSource
        private SheetHelper _import_source;
        /// <summary>
        /// 要匯入的資料來源。
        /// </summary>
        internal SheetHelper ImportSource
        {
            get { return _import_source; }
            set { _import_source = value; }
        }
        #endregion

        #region Property StudentLookup
        private StudentLookup _lookup;
        public StudentLookup StudentLookup
        {
            get { return _lookup; }
            set { _lookup = value; }
        }
        #endregion

        #region Property TargetPayment
        private GT.Payment _target_payment;
        /// <summary>
        /// 要匯入的目標收費。
        /// </summary>
        public GT.Payment TargetPayment
        {
            get { return _target_payment; }
            set { _target_payment = value; }
        }
        #endregion

        #region Property EngancedErrorProvider
        private EnhancedErrorProvider _enganced_error = new EnhancedErrorProvider();
        public EnhancedErrorProvider ErrorProvider
        {
            get { return _enganced_error; }
        }
        #endregion

        #region Property CellStyle
        private Style _error_style;
        public Style ErrorStyle
        {
            get { return _error_style; }
            set { _error_style = value; }
        }

        private Style _normal_style;
        public Style NormalStyle
        {
            get { return _normal_style; }
            set { _normal_style = value; }
        }

        #endregion

        #region Page:Select Import Target
        private void wpSelectTargetPayment_NextButtonClick(object sender, CancelEventArgs e)
        {
            ErrorProvider.Clear();
            cboPaymentList_Validating(this, new CancelEventArgs());

            if (ErrorProvider.HasError)
            {
                MsgBox.Show("畫面中含有錯誤狀況，請確認後再繼續。");
                e.Cancel = true;
                return;
            }

            TargetPayment = cboPaymentList.SelectedItem as GT.Payment;
        }

        private void cboPaymentList_Validating(object sender, CancelEventArgs e)
        {
            if (cboPaymentList.SelectedItem == null)
            {
                ErrorProvider.SetError(cboPaymentList, "您必須要選擇要匯入的收費名稱。");
                e.Cancel = true;
            }
        }

        private void cboPaymentList_Validated(object sender, EventArgs e)
        {
            ErrorProvider.SetError(cboPaymentList, "");
        }

        AddPaymentForm addform;
        private void cboPaymentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GT.Payment payment = cboPaymentList.SelectedItem as GT.Payment;

            if (payment == null) return;

            if (string.IsNullOrEmpty(payment.Identity))
            {
                if (addform != null)
                    addform.PaymentSaving -= new CancelEventHandler(AddForm_PaymentSaving);

                addform = new AddPaymentForm(cboSchoolYear.Text, cboSemester.Text);
                addform.PaymentSaving += new CancelEventHandler(AddForm_PaymentSaving);

                if (addform.ShowDialog() == DialogResult.OK)
                {
                    cboPaymentList.Items.Add(addform.NewPayment);
                    cboPaymentList.SelectedItem = addform.NewPayment;
                }
                else
                    cboPaymentList.SelectedItem = null;
            }
        }

        private void AddForm_PaymentSaving(object sender, CancelEventArgs e)
        {
            AddPaymentForm addfrom = sender as AddPaymentForm;
            foreach (GT.Payment each in cboPaymentList.Items)
            {
                if (each.Name == addform.NewPayment.Name)
                {
                    MsgBox.Show("收費名稱重複，請重新命名。");
                    e.Cancel = true;
                    return;
                }
            }
        }
        #endregion

        #region Page:Select File and Action Page
        private void wpSelectFileAndAction_BeforePageDisplayed(object sender, WizardCancelPageChangeEventArgs e)
        {
            lblUpdateDesc.Enabled = !string.IsNullOrEmpty(TargetPayment.Identity);
            chkUpdate.Enabled = !string.IsNullOrEmpty(TargetPayment.Identity);
            chkInsert.Checked = string.IsNullOrEmpty(TargetPayment.Identity);
        }

        private void wpSelectFileAndAction_NextButtonClick(object sender, CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                ErrorProvider.Clear();

                if (string.IsNullOrEmpty(WorkbookFileName))
                {
                    ErrorProvider.SetError(txtSourceFile, "您必須選擇匯入來源檔案。");
                    return;
                }

                if (Mode == ImportMode.None)
                {
                    MsgBox.Show("您必須決定一種匯入方式。");
                    return;
                }

                ImportSource = cboSheetList.SelectedItem as SheetHelper;

                //檢查來源欄位是否正確。
                CheckSheetField(cboSheetList, ImportSource);

                if (ErrorProvider.HasError)
                {
                    MsgBox.Show("畫面中含有錯誤狀況，請確認後再繼續。");
                    return;
                }

                e.Cancel = false;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
            finally
            {
                pProgram.Visible = false;
                pUser.Visible = true;
            }
        }

        #region Control Events
        private void lblInserDesc_Click(object sender, EventArgs e)
        {
            Mode = ImportMode.Insert;
            chkInsert.Checked = true;
        }

        private void lblUpdateDesc_Click(object sender, EventArgs e)
        {
            Mode = ImportMode.Update;
            chkUpdate.Checked = true;
        }

        private void chkInsert_CheckedChanged(object sender, EventArgs e)
        {
            Mode = ImportMode.Insert;
        }

        private void chkUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Mode = ImportMode.Update;
        }

        private void txtSourceFile_TextChanged(object sender, EventArgs e)
        {
            WorkbookFileName = txtSourceFile.Text;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = SelectSourceFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                System.Windows.Forms.ToolTip tip = new System.Windows.Forms.ToolTip();
                FileInfo file = new FileInfo(SelectSourceFileDialog.FileName);
                txtSourceFile.Text = file.Name;
                tip.SetToolTip(txtSourceFile, file.DirectoryName);
                ErrorProvider.SetError(txtSourceFile, "");

                WorkbookFileName = file.FullName;
                ListWorksheet(WorkbookFileName);
            }
        }

        private void cboSheetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SheetHelper helper = cboSheetList.SelectedItem as SheetHelper;

            if (helper == null) return;

            //檢查欄位的相關狀態。
            CheckSheetField(cboSheetList, helper);
        }

        #region Method CheckSheetField
        private void CheckSheetField(Control msgControl, SheetHelper importSource)
        {
            ErrorProvider.SetError(msgControl, "");
            SheetHelper helper = importSource;

            if (helper == null) return;

            if (helper != null)
            {
                if (helper.IsFieldDuplicate)
                {
                    ErrorProvider.SetError(msgControl, "工作表中含有相同的欄位名稱。");
                    return;
                }
            }

            int idcount = helper.ExceptFields(IdentifiableFieldNames).Count;

            if (idcount <= 0)
            {
                ErrorProvider.SetError(msgControl, "來源資料中並沒有提供可識別的欄位。\n(姓名、學號、身分證號)");
                return;
            }

            if (!helper.Fields.ContainsKey(TOTAL_AMOUNT_FIELD_NAME))
            {
                ErrorProvider.SetError(msgControl, string.Format("來源資料中缺少必要欄位「{0}」。", TOTAL_AMOUNT_FIELD_NAME));
                return;
            }

        }
        #endregion

        #region ListWorksheet
        /// <summary>
        /// 列出 Excel  的工作表。
        /// </summary>
        /// <param name="fileName"></param>
        private void ListWorksheet(string fileName)
        {
            try
            {
                cboSheetList.Items.Clear();

                Workbook book = new Workbook();
                book.Open(fileName);

                for (int i = 0; i < book.Worksheets.Count; i++)
                    cboSheetList.Items.Add(new SheetHelper(book, book.Worksheets[i]));

                if (cboSheetList.Items.Count > 0)
                    cboSheetList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }
        #endregion
        #endregion

        #endregion

        #region Page:Select Identify Field
        private void wpCollectKeyInfo_BeforePageDisplayed(object sender, WizardCancelPageChangeEventArgs e)
        {
            cboIdField.Items.Clear();
            foreach (string each in ImportSource.ExceptFields(IdentifiableFieldNames))
                cboIdField.Items.Add(each);

            if (cboIdField.Items.Count > 0)
                cboIdField.SelectedIndex = 0;
        }

        private void wpCollectKeyInfo_NextButtonClick(object sender, CancelEventArgs e)
        {
            try
            {
                ImportSource.IdentifyField = cboIdField.SelectedItem.ToString();

                string fieldName = "ID";
                if (ImportSource.IdentifyField == "學號")
                    fieldName = "StudentNumber";
                else if (ImportSource.IdentifyField == "身分證號")
                    fieldName = "IDNumber";

                StudentLookup = new StudentLookup(fieldName);

            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(new PaymentModuleException("設定 ImportSource.IdentifyField 屬性錯誤。", ex));
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Page:Import
        private void wpImport_BeforePageDisplayed(object sender, WizardCancelPageChangeEventArgs e)
        {
            pgImport.Value = 0;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //前面應該要檢查是否有記錄，沒有記錄則不應該執行此程式。

            btnImport.Enabled = false;
            try
            {
                string paymentId = GetPaymentID();
                DSXmlHelper helper = new DSXmlHelper("Request");
                List<string> studentIdList = new List<string>();

                while (ImportSource.MoveNext())
                {
                    DSXmlHelper eachDetail = new DSXmlHelper(helper.AddElement("PaymentDetail"));
                    string studentId = StudentLookup.GetStudentID(ImportSource.GetString(ImportSource.IdentifyField));
                    studentIdList.Add(studentId);

                    if (Mode == ImportMode.Insert)
                    {
                        eachDetail.AddElement(".", "RefStudentID", studentId);
                        eachDetail.AddElement(".", "RefPaymentID", paymentId);
                    }
                    else if (Mode == ImportMode.Update)
                    {
                        eachDetail.AddElement("Condition");
                        eachDetail.AddElement("Condition", "RefStudentID", studentId);
                        eachDetail.AddElement("Condition", "RefPaymentID", paymentId);
                    }
                    else
                        throw new ArgumentException("沒有這種的啦！");

                    eachDetail.AddElement(".", "Amount", ImportSource.GetString(TOTAL_AMOUNT_FIELD_NAME));

                    //PaymentItems 欄位。
                    DSXmlHelper items = new DSXmlHelper(eachDetail.AddElement("PaymentItems"));
                    items = new DSXmlHelper(items.AddElement("PaymentItems")); //建立第二層。
                    foreach (string eachItem in ImportSource.MoneyFields)
                    {
                        //「總金額」為特殊欄位，不算在「金額項目」中。
                        if (eachItem == TOTAL_AMOUNT_FIELD_NAME) continue;

                        string mergeName = SheetHelper.ToNormalField(eachItem);
                        string amount = ImportSource.GetString(eachItem);

                        if (string.IsNullOrEmpty(amount)) continue;

                        XmlElement item = items.AddElement(".", "Item");
                        item.SetAttribute("Name", mergeName);
                        item.SetAttribute("Value", amount);
                    }

                    //MergeFields 欄位。
                    DSXmlHelper mfields = new DSXmlHelper(eachDetail.AddElement("MergeFields"));
                    mfields = new DSXmlHelper(mfields.AddElement("MergeFields")); //建立第二層。
                    foreach (string eachItem in ImportSource.MergeFields)
                    {
                        string mergeName = SheetHelper.ToNormalField(eachItem);
                        XmlElement field = mfields.AddElement(".", "Item");
                        field.SetAttribute("Name", mergeName);
                        field.SetAttribute("Value", ImportSource.GetString(eachItem));
                    }
                }

                if (Mode == ImportMode.Insert)
                {
                    EditPayment.InsertPaymentDetails(helper);
                }
                else if (Mode == ImportMode.Update)
                {
                    EditPayment.UpdatePaymentDetails(helper);
                }
                else
                    throw new ArgumentException("沒有這種的啦。");

                wpImport.FinishButtonEnabled = eWizardButtonState.True;
                pgImport.Value = pgImport.Maximum;
                lblImportProgress.Text = "匯入完成";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnImport.Enabled = true;
        }

        private string GetPaymentID()
        {
            return TargetPayment.Identity;
        }

        private void wpImport_FinishButtonClick(object sender, CancelEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion

        #region Page:Validation
        private void wpValidation_AfterPageDisplayed(object sender, WizardPageChangeEventArgs e)
        {
            wpValidation.NextButtonEnabled = eWizardButtonState.False;

            ErrorStyle = ImportSource.Book.Styles[ImportSource.Book.Styles.Add()];
            ErrorStyle.Pattern = BackgroundType.Solid;
            ErrorStyle.ForegroundColor = Color.Red;

            NormalStyle = ImportSource.Book.Styles[ImportSource.Book.Styles.Add()];
            NormalStyle.Font.Color = Color.Black;
            NormalStyle.ForegroundColor = Color.White;
        }

        public const string TOTAL_AMOUNT_FIELD_NAME = "$$應繳金額";
        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                lblCorrectCount.Text = "0";
                lblErrorCount.Text = "0";
                lblWarningCount.Text = "0";
                BankConfig bconfig = BankConfigManager.GetConfig(TargetPayment);
                IBankService bservice = BankServiceProvider.GetService(bconfig);

                if (bservice == null)
                {
                    MsgBox.Show("您未指定銀行模組，無法驗證資料正確性。");
                    return;
                }

                ProgressMessage("載入驗證資訊…");
                ProgressChanged(0);
                string fieldName = "StudentID";
                if (ImportSource.IdentifyField == "學號")
                    fieldName = "StudentNumber";
                else if (ImportSource.IdentifyField == "身分證號")
                    fieldName = "IDNumber";

                List<string> identify_list = new List<string>();
                if (!string.IsNullOrEmpty(TargetPayment.Identity))
                {
                    DSXmlHelper helper = SmartSchool.Feature.Payment.QueryPayment.GetPaymentDetailStudents(TargetPayment.Identity);
                    foreach (XmlElement each in helper.GetElements("PaymentDetail"))
                    {
                        DSXmlHelper eachHelper = new DSXmlHelper(each);
                        string field_value = eachHelper.GetText(fieldName);
                        if (!identify_list.Contains(field_value))
                            identify_list.Add(field_value);
                        else
                        {
                            // do something...
                        }
                    }
                }

                //還原！
                ImportSource.Sheet.Comments.Clear();
                while (ImportSource.MoveNext())
                {
                    foreach (int col_index in ImportSource.Fields.Values)
                        ImportSource.Sheet.Cells[ImportSource.CurrentOffset, col_index].Style = NormalStyle;
                }
                ImportSource.ResetOffset();

                ProgressMessage("驗證資料中…");
                int error_count = 0;
                Dictionary<string, object> identity_duplicate = new Dictionary<string, object>();
                while (ImportSource.MoveNext())
                {
                    bool error = false;

                    //驗證金額是否正確
                    int amount = -1;
                    int count = 0;
                    if (int.TryParse(ImportSource.Sheet.Cells[ImportSource.CurrentOffset, ImportSource.Fields[TOTAL_AMOUNT_FIELD_NAME]].StringValue, out amount))
                    {
                        foreach (string each_item in ImportSource.MoneyFields)
                        {
                            if (each_item != TOTAL_AMOUNT_FIELD_NAME)
                            {
                                int m;
                                if (int.TryParse(ImportSource.Sheet.Cells[ImportSource.CurrentOffset, ImportSource.Fields[each_item]].StringValue, out m))
                                    count += m;
                            }
                        }

                        if (count != amount)
                        {
                            SetError(TOTAL_AMOUNT_FIELD_NAME, "應繳金額有誤。");
                            error = true;
                        }

                        if (amount <= 0 || amount > bservice.GetAmountLimit(bconfig))
                        {
                            SetError(TOTAL_AMOUNT_FIELD_NAME,
                                string.Format("應繳金額範圍不正確，應介於「{0}~{1}」之間。", 1, bservice.GetAmountLimit(bconfig)));
                            error = true;
                        }
                    }
                    else
                    {
                        SetError(TOTAL_AMOUNT_FIELD_NAME, "應繳金額有誤。");
                        error = true;
                    }

                    //驗證識別欄位是否合法
                    string identify_value = ImportSource.GetString(ImportSource.IdentifyField).Trim();
                    if (identity_duplicate.ContainsKey(identify_value))
                    {
                        SetError(ImportSource.IdentifyField, "資料重覆。");
                        error = true;
                    }
                    else
                        identity_duplicate.Add(identify_value, null);

                    if (!StudentLookup.Contains(identify_value))
                    {
                        SetError(ImportSource.IdentifyField, "系統中找不到此" + ImportSource.IdentifyField + "。");
                        error = true;
                    }
                    else
                    {
                        if (Mode == ImportMode.Insert && identify_list.Contains(identify_value))
                        {
                            SetError(ImportSource.IdentifyField, "收費明細中已有此" + ImportSource.IdentifyField + "。");
                            error = true;
                        }
                        else if (Mode == ImportMode.Update && !identify_list.Contains(identify_value))
                        {
                            SetError(ImportSource.IdentifyField, "收費明細中找不到此" + ImportSource.IdentifyField + "。");
                            error = true;
                        }
                    }

                    if (error)
                        error_count++;

                    ProgressChanged((int)((ImportSource.CurrentOffset * 100) / ImportSource.MaxOffset));
                }
                ImportSource.Save(WorkbookFileName);

                lblErrorCount.Text = error_count.ToString();
                ProgressMessage("驗證完成");

                if (error_count <= 0)
                {
                    wpValidation.NextButtonEnabled = eWizardButtonState.True;
                }
                else
                {
                    wpValidation.BackButtonEnabled = eWizardButtonState.False;
                    btnValidate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                ProgressMessage("");
                //wpValidation.NextButtonEnabled = eWizardButtonState.False;
            }
        }

        private void btnViewResult_Click(object sender, EventArgs e)
        {
            try
            {
                wpValidation.NextButtonEnabled = eWizardButtonState.False;
                Process.Start(WorkbookFileName);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void SetError(string field, string msg)
        {
            ImportSource.SetComment(field, msg);
            ImportSource.Sheet.Cells[ImportSource.CurrentOffset, ImportSource.Fields[field]].Style = ErrorStyle;
        }

        private void ProgressChanged(int percentage)
        {
            pgValidProgress.Value = percentage;
        }

        private void ProgressMessage(string msg)
        {
            lblValidMsg.Text = msg;
            Application.DoEvents();
        }
        #endregion

        #region Class SheetHelper
        internal class SheetHelper
        {
            public SheetHelper(Workbook book, Worksheet sheet)
            {
                _book = book;
                _sheet = sheet;

                for (int i = 0; i <= Sheet.Cells.MaxColumn; i++)
                {
                    string fieldName = sheet.Cells[0, i].StringValue;

                    if (string.IsNullOrEmpty(fieldName.Trim()))
                        continue;

                    if (Fields.ContainsKey(fieldName))
                    {
                        _field_duplicate = true;
                        continue;
                    }

                    Fields.Add(fieldName, i);

                    if (fieldName.StartsWith("$"))
                        MoneyFields.Add(fieldName);
                    if (fieldName.StartsWith("#"))
                        MergeFields.Add(fieldName);
                }

                _current_offset = 0;
                _max_offset = Sheet.Cells.MaxDataRow;
            }

            private bool _field_duplicate = false;
            public bool IsFieldDuplicate
            {
                get { return _field_duplicate; }
            }

            private Workbook _book;
            public Workbook Book
            {
                get { return _book; }
            }

            private Worksheet _sheet;
            public Worksheet Sheet
            {
                get { return _sheet; }
            }

            private Dictionary<string, int> _all_field = new Dictionary<string, int>();
            public Dictionary<string, int> Fields
            {
                get { return _all_field; }
            }

            private List<string> _money_field = new List<string>();
            public List<string> MoneyFields
            {
                get { return _money_field; }
            }

            private List<string> _merge_fields = new List<string>();
            public List<string> MergeFields
            {
                get { return _merge_fields; }
            }

            private string _identify_field;
            /// <summary>
            /// 取得或設定識別欄位。
            /// </summary>
            public string IdentifyField
            {
                get { return _identify_field; }
                set
                {
                    _identify_field = value;

                    if (!Fields.ContainsKey(value))
                        throw new PaymentModuleException("指定的識別欄位不存在。", null);

                    _max_offset = Sheet.Cells.MaxDataRowInColumn(Fields[value]);
                }
            }

            /// <summary>
            /// 取得欄位名稱的差集。
            /// </summary>
            public List<string> ExceptFields(params string[] fields)
            {
                List<string> result = new List<string>();
                foreach (string each in fields)
                {
                    if (Fields.ContainsKey(each))
                        result.Add(each);
                }

                return result;
            }

            private int _current_offset;
            public int CurrentOffset
            {
                get { return _current_offset; }
            }

            private int _max_offset;
            public int MaxOffset
            {
                get { return _max_offset; }
            }

            public bool MoveNext()
            {
                _current_offset++;

                return _current_offset <= Sheet.Cells.MaxDataRow;
            }

            public void ResetOffset()
            {
                _current_offset = 0;
            }

            public string GetString(string fieldName)
            {
                return Sheet.Cells[CurrentOffset, Fields[fieldName]].StringValue;
            }

            public void SetString(string fieldName, string value)
            {
                Sheet.Cells[CurrentOffset, Fields[fieldName]].PutValue(value);
            }

            public void SetComment(string fieldName, string comment)
            {
                //throw new Exception("未測試...");

                if (Sheet.Comments[CurrentOffset, Fields[fieldName]] == null)
                    Sheet.Comments.Add(CurrentOffset, (byte)Fields[fieldName]);
                Sheet.Comments[CurrentOffset, Fields[fieldName]].Note = comment;
            }

            public static string ToNormalField(string fieldName)
            {
                if (fieldName.StartsWith("$") || fieldName.StartsWith("#"))
                    return fieldName.Substring(1, fieldName.Length - 1);
                else
                    return fieldName;
            }

            public override string ToString()
            {
                return Sheet.Name;
            }

            public void Save(string fileName)
            {
                _book.Save(fileName);
                Reload(fileName);
            }
            private void Reload(string fileName)
            {
                Workbook book = new Workbook();
                book.Open(fileName);

                _book = book;
                _sheet = book.Worksheets[_sheet.Name];
                ResetOffset();
            }
        }
        #endregion

    }
}