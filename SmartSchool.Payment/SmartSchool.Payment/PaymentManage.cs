using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Payment.GT;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.Feature.Payment;
using SmartSchool.Payment.Interfaces;
using SmartSchool.Payment.BankManagement;
using Aspose.Words;
using SmartSchool.Payment.BillTemplate;
using System.Diagnostics;
using System.IO;
using Aspose.Cells;
using SmartSchool.ePaper;

namespace SmartSchool.Payment
{
    public partial class PaymentManage : BaseForm
    {
        static PaymentManage()
        {
            Private.UDT.Behavior.Instance = new Private.UDT.Behavior(new Private.UDT.CallServiceDelegate(CurrentUser.Instance.CallService));
        }

        public PaymentManage()
        {
            InitializeComponent();
            InitializeSemester();
            ClearDetailPanelData();
        }

        #region (Process) InitializeSemester
        private void InitializeSemester()
        {
            try
            {
                for (int i = -2; i <= 2; i++) //只顯示前後兩個學年的選項，其他的用手動輸入。
                {
                    cboSchoolYear.Items.Add(CurrentUser.Instance.SchoolYear + i);
                    //cboConfSchoolYear.Items.Add(CurrentUser.Instance.SchoolYear + i);
                }

                cboSchoolYear.Text = CurrentUser.Instance.SchoolYear.ToString();
                cboSemester.Text = CurrentUser.Instance.Semester.ToString();
                origin_schoolyear = cboSchoolYear.Text; //用於復原。
                origin_semester = cboSemester.Text;      //用於復原。
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(new PaymentModuleException("填入學年度學期選項清單時發生錯誤。", ex));
            }
        }
        #endregion

        #region (Process) Semester Control Events
        private string origin_schoolyear, origin_semester;
        private bool semester_changing = false; //這是一個特殊技巧，不懂來問我吧。

        private void cboSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (semester_changing) return;

            //當要更換學年度學期時，先 Select 到 Null，因為 SelectPayment 動作會進行儲存相關的流程。
            //使用者的資料不會因為誤改學年度/學期 Filter 而遺失資料。
            if (SelectPayment(null))
            {
                LoadPaymentList();
                origin_schoolyear = cboSchoolYear.Text;
            }
            else
            {//如果更換學年度/學期失敗，則還原之前的 Filter 狀態。
                semester_changing = true;
                cboSchoolYear.Text = origin_schoolyear;
                semester_changing = false;
            }
        }

        private void cboSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (semester_changing) return;

            //當要更換學年度學期時，先 Select 到 Null，因為 SelectPayment 動作會進行儲存相關的流程。
            //使用者的資料不會因為誤改學年度/學期 Filter 而遺失資料。
            if (SelectPayment(null))
            {
                LoadPaymentList();
                origin_semester = cboSemester.Text;
            }
            else
            {//如果更換學年度/學期失敗，則還原之前的 Filter 狀態。
                semester_changing = true;
                cboSemester.Text = origin_semester;
                semester_changing = false;
            }
        }

        #region (Process) Load Payment List
        // 提供非同步載入資料的功能。
        private AsyncDataLoader _payment_loader = new AsyncDataLoader();
        internal AsyncDataLoader PaymentLoader
        {
            get { return _payment_loader; }
        }

        private void LoadPaymentListComplete(ExecuteResult result)
        {
            PaymentLoading.Visible = false;

            if (!result.Success)
            {
                MsgBox.Show(result.Error.Message);
                return;
            }

            List<GT.Payment> payments = result.Result as List<GT.Payment>;

            ipPayments.Items.Clear();
            foreach (GT.Payment each in payments)
                AddPaymentItem(each, false);
            ipPayments.Refresh();

            if (ipPayments.Items.Count <= 0)
                SelectPayment(null);
            else
            {
                if (string.IsNullOrEmpty(PaddingSelectPayment))
                    (ipPayments.Items[0] as PaymentButtonItem).RaiseClick();
                else
                {
                    foreach (PaymentButtonItem each in ipPayments.Items)
                    {
                        if (each.Payment.Name == PaddingSelectPayment)
                        {
                            each.RaiseClick();
                            break;
                        }
                    }

                    PaddingSelectPayment = string.Empty;
                }
            }
        }

        internal object LoadPaymentListThread(Arguments args)
        {
            string schoolyear = args["SchoolYear"].ToString();
            string semester = args["Semester"].ToString();

            List<GT.Payment> payments = new List<GT.Payment>();

            DSXmlHelper helper = SmartSchool.Feature.Payment.QueryPayment.GetAbstractList(schoolyear, semester);
            foreach (XmlElement payElement in helper.GetElements("Payment"))
            {
                DSXmlHelper payHelper = new DSXmlHelper(payElement);

                string id = payHelper.GetText("@ID");
                string name = payHelper.GetText("PaymentName");
                string sy = payHelper.GetText("SchoolYear");
                string sems = payHelper.GetText("Semester");
                PaymentConfig hlpConfig = PaymentConfig.Parse(payHelper.GetElement("Config/PaymentConfig"));
                BillBatchInformationCollection billbatchs = BillBatchInformationCollection.Parse(payHelper.GetElement("BillBatchs/Content"));

                GT.Payment payment = new GT.Payment();
                payment.Identity = id;
                payment.Name = name;
                payment.SchoolYear = sy;
                payment.Semester = sems;
                payment.Config = hlpConfig;
                payment.BillBatchInformations = billbatchs;

                payments.Add(payment);
            }

            return payments;
        }
        #endregion

        #endregion

        #region (Process) Selecte Payment Item
        private void PaymentItem_Click(object sender, EventArgs e)
        {
            SelectPayment(sender as PaymentButtonItem);
        }

        private bool PaymentSelectChanging()
        {
            ChangePaymentCommandStatus(false);
            ChangePaymentDetailCommandStatus(false);
            ClearDetailPanelData(); //清除畫面上的資料。
            SetSummaryInfoToClean();
            cboBillBatchs.Items.Clear();
            cboBillBatchs.Items.Add("");
            cboBillBatchs.SelectedIndex = 0;
            cboBillBatchs.SelectedItem = "";

            string msg = string.Empty;
            if (SelectedPayment == null)
                return true; //如果之前的 Selected Payment 是 Null 就不問了。
            else
                return true;
        }

        /// <summary>
        /// 清除畫面上的資料(包含錯誤訊息)。
        /// </summary>
        private void ClearDetailPanelData()
        {
            txtFilter.TextBox.Text = string.Empty;
            detaillist.ClearDataGridView();
        }

        private void PaymentSelectChanged()
        {
            tabDetail.Enabled = (SelectedPayment != null);

            if (SelectedPayment != null)
            {
                ChangePaymentCommandStatus(true);

                SelectedPayment.SyncDisplayText(); //同步畫面上的名稱資訊。

                GT.Payment payment = SelectedPayment.Payment;

                string bankconfigId = SelectedPayment.Payment.Config.BankConfigID;
                if (BankDictionary.ContainsKey(bankconfigId))
                    lblBankName.Text = BankDictionary[bankconfigId].Name;
                else
                    lblBankName.Text = "<未指定銀行組態或是組態已刪除>";

                //初始化繳費單批認的資料與畫面。
                PopuplateBillBatchs(payment);

                Arguments args = new Arguments();
                args.Add("PaymentObject", payment);

                SetSummaryInfoToLoading();
                _detail_loader.Execute(new LoaderExecutor(LoadPaymentDetailThread),
                    new LoaderCallback(LoadPaymentDetailComplete),
                    args);
            }
        }

        private void PopuplateBillBatchs(GT.Payment payment)
        {
            cboBillBatchs.Enabled = true;
            cboBillBatchs.Items.Clear();
            cboBillBatchs.SelectedIndex = -1;
            cboBillBatchs.Items.Add("");
            foreach (BillBatchInformation each in payment.BillBatchInformations)
                cboBillBatchs.Items.Add(each);
            btnViewBill.Enabled = false;
            btnUploadBill.Enabled = false;
            btnSaveBill.Enabled = false;
        }

        #region Load Payment Detail
        private AsyncDataLoader _detail_loader = new AsyncDataLoader();
        private void LoadPaymentDetailComplete(ExecuteResult result)
        {
            if (result.Success)
            {
                ChangePaymentDetailCommandStatus(true);

                GT.Payment payment = result.Result as GT.Payment;
                int totalAmount = 0, paidAmount = 0;
                int paidCount = 0, overCount = 0;
                foreach (PaymentDetail each in payment.Details)
                {
                    totalAmount += each.Amount;

                    if (each.IsPaidSuccess)
                    {
                        paidAmount += each.GetPaidAmount();
                        paidCount++;

                        //繳費超額的學生數。
                        if (each.GetPaidAmount() > each.Amount)
                            overCount++;
                    }
                }

                lblTotalAmount.Text = totalAmount.ToString("C");
                lblPaidAmount.Text = paidAmount.ToString("C");
                lblDetailCount.Text = payment.Details.Count.ToString("#,0");
                lblPaidCount.Text = paidCount.ToString("#,0");
                lblUnpaidCount.Text = (payment.Details.Count - paidCount).ToString("#,0");
                lblOverLimit.Text = overCount.ToString("#,0");

                if (overCount > 0)
                    lblOverLimit.ForeColor = Color.Red;
                else
                    lblOverLimit.ForeColor = SystemColors.ControlText;

                double countProgress = 0;
                if (payment.Details.Count > 0)
                    countProgress = ((double)paidCount / payment.Details.Count) * 50f;

                double amountProgress = 0;
                if (totalAmount > 0)
                    amountProgress = ((double)paidAmount / totalAmount) * 50f;

                pgTotalProgress.Value = (int)(countProgress + amountProgress);
                lblTotalProgress.Text = string.Format("收費總進度 ({0}%)", pgTotalProgress.Value);

                detaillist.PopulatePaymentDetail(payment);
            }
            else
            {
                MsgBox.Show(result.Error.Message);
                CurrentUser.ReportError(result.Error);
                SetSummaryInfoToClean();
            }
        }

        private object LoadPaymentDetailThread(Arguments args)
        {
            GT.Payment payment = args["PaymentObject"] as GT.Payment;

            DSXmlHelper details = QueryPayment.GetPaymentDetails(payment.Identity);

            List<string> detailidlist = new List<string>();
            Dictionary<string, PaymentDetail> dicDetials = new Dictionary<string, PaymentDetail>();
            foreach (XmlElement each in details.GetElements("PaymentDetail"))
            {
                DSXmlHelper hlpeach = new DSXmlHelper(each);
                PaymentDetail detail = new PaymentDetail();

                detail.Identity = hlpeach.GetText("@ID");
                detail.IsDirtyRecord = hlpeach.GetText("IsDirty") == "1" ? true : false;
                detail.PaymentName = hlpeach.GetText("PaymentName");
                detail.RefPaymentID = hlpeach.GetText("RefPaymentID");
                detail.RefStudentID = hlpeach.GetText("RefStudentID");
                detail.StudentName = hlpeach.GetText("StudentName");
                detail.SeatNumber = hlpeach.GetText("SeatNumber");
                detail.StudentNumber = hlpeach.GetText("StudentNumber");
                detail.ClassName = hlpeach.GetText("ClassName");
                detail.Amount = hlpeach.TryGetInteger("Amount", 0);
                detail.MergeFields = PaymentDetail.ParseMergeFields(hlpeach.GetElement("MergeFields"));
                detail.PayItems = PaymentDetail.ParsePayItems(hlpeach.GetElement("PaymentItems"));

                detailidlist.Add(detail.Identity);
                dicDetials.Add(detail.Identity, detail);
            }

            DSXmlHelper histories = QueryPayment.GetPaymentHistories(detailidlist.ToArray());
            List<PaymentHistory> listhistory = new List<PaymentHistory>();
            foreach (XmlElement each in histories.GetElements("PaymentHistory"))
            {
                DSXmlHelper hlpeach = new DSXmlHelper(each);
                PaymentHistory history = new PaymentHistory();

                history.Identity = hlpeach.GetText("@ID");
                history.Amount = hlpeach.TryGetInteger("Amount", 0);
                history.PaidAmount = hlpeach.TryGetInteger("PaidAmount", 0);
                history.RefPaymentDetailID = hlpeach.GetText("RefPaymentDetailID");
                history.Paid = hlpeach.GetText("Paid") == "1" ? true : false;
                history.Cancelled = hlpeach.GetText("Cancelled") == "1" ? true : false;

                listhistory.Add(history);
            }

            foreach (PaymentHistory each in listhistory)
            {
                if (dicDetials.ContainsKey(each.RefPaymentDetailID))
                {
                    PaymentDetail detail = dicDetials[each.RefPaymentDetailID];
                    detail.Histories.Add(each);
                }
                else
                {
                    throw new PaymentModuleException("「繳費記錄」找不到對應的「收費明細」，資料狀態錯誤。", null);
                }
            }

            payment.Details = new List<PaymentDetail>(dicDetials.Values);
            payment.Details.Sort(new Comparison<PaymentDetail>(PaymentDetailComparer));
            return payment;
        }

        private int PaymentDetailComparer(PaymentDetail x, PaymentDetail y)
        {
            return x.StudentNumber.CompareTo(y.StudentNumber);
        }

        #endregion

        #endregion

        #region (Process) Command Button Events
        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportPaymentWizard wizard = new ImportPaymentWizard(
                SchoolYear,
                Semester,
                GetPaymentList(),
                SelectedPayment == null ? null : SelectedPayment.Payment);

            DialogResult dr = wizard.ShowDialog();

            LoadPaymentList();

            if (dr == DialogResult.OK)
                PaddingSelectPayment = wizard.TargetPayment.Name;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (SelectedPayment == null) return;

            PaymentLoader.Wait();

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "收費資料匯出.xls";
            dialog.Filter = "Excel 檔案(*.xls)|*.xls";

            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                GT.Payment payment = SelectedPayment.Payment;

                List<string> fields = GetAllExportableField(payment);
                fields.Sort(new Comparison<string>(CompareField));

                Dictionary<string, int> columns = new Dictionary<string, int>();
                for (int i = 0; i < fields.Count; i++)
                    columns.Add(fields[i], i);

                Workbook book = new Workbook();
                book.Worksheets.Clear();
                Worksheet sheet = book.Worksheets[book.Worksheets.Add()];

                foreach (KeyValuePair<string, int> each in columns)
                    sheet.Cells[0, each.Value].PutValue(each.Key);

                ProgressForm progress = new ProgressForm("匯出學生資料進度");
                progress.Minimum = 1;
                progress.Maximum = payment.Details.Count;
                progress.Value = 1;
                progress.Show();

                int rowIndex = 1;
                foreach (PaymentDetail each in payment.Details)
                {
                    sheet.Cells[rowIndex, columns["學生系統編號"]].PutValue(each.RefStudentID);
                    sheet.Cells[rowIndex, columns["班級名稱"]].PutValue(each.ClassName);
                    sheet.Cells[rowIndex, columns["學生姓名"]].PutValue(each.StudentName);
                    sheet.Cells[rowIndex, columns["學號"]].PutValue(each.StudentNumber);
                    sheet.Cells[rowIndex, columns[ImportPaymentWizard.TOTAL_AMOUNT_FIELD_NAME]].PutValue(each.Amount);

                    foreach (KeyValuePair<string, int> eachPay in each.PayItems)
                        sheet.Cells[rowIndex, columns["$" + eachPay.Key]].PutValue(eachPay.Value);

                    foreach (KeyValuePair<string, string> eachMerge in each.MergeFields)
                        sheet.Cells[rowIndex, columns["#" + eachMerge.Key]].PutValue(eachMerge.Value);

                    progress.Value = rowIndex;
                    rowIndex++;
                }

                progress.Finish();
                book.Save(dialog.FileName);
                Process.Start(dialog.FileName);
            }
        }

        private static int CompareField(string x, string y)
        {
            string Xfirst = x.Substring(0, 1);
            string Yfirst = y.Substring(0, 1);
            string Xstring = x.Substring(1);
            string Ystring = y.Substring(1);

            if (x == "$應繳金額")
                return 1;

            if (y == "$應繳金額")
                return -1;

            if ((Xfirst != "$" && Xfirst != "#") && (Yfirst == "$" || Yfirst == "#"))
                return -1;

            if ((Yfirst != "$" && Yfirst != "#") && (Xfirst == "$" || Xfirst == "#"))
                return 1;

            return Xfirst.CompareTo(Yfirst);
        }

        private static List<string> GetAllExportableField(GT.Payment payment)
        {
            Dictionary<string, int> fields = new Dictionary<string, int>();
            fields.Add("學生系統編號", 0);
            fields.Add("班級名稱", 0);
            fields.Add("學生姓名", 0);
            fields.Add("學號", 0);
            fields.Add(ImportPaymentWizard.TOTAL_AMOUNT_FIELD_NAME, 0);

            foreach (PaymentDetail each in payment.Details)
            {
                foreach (KeyValuePair<string, int> eachPay in each.PayItems)
                {
                    if (!fields.ContainsKey("$" + eachPay.Key))
                        fields.Add("$" + eachPay.Key, 0);
                }

                foreach (KeyValuePair<string, string> eachMerge in each.MergeFields)
                {
                    if (!fields.ContainsKey("#" + eachMerge.Key))
                        fields.Add("#" + eachMerge.Key, 0);
                }
            }

            return new List<string>(fields.Keys);
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            try
            {
                BillBatchInformation history = cboBillBatchs.SelectedItem as BillBatchInformation;

                if (history != null)
                {
                    Document template = history.TemplateDocument;

                    if (template == null)
                    {
                        MsgBox.Show("您未決定繳費單樣版，無法列印繳費單。");
                        return;
                    }

                    BillOutputSettingForm outputsetting = new BillOutputSettingForm();
                    DialogResult dr = outputsetting.ShowDialog(this);

                    if (dr != DialogResult.OK) return; //如果不 ok 的話，下面就不執行了。

                    ProgressForm progress = new ProgressForm("繳費單產生進度 (資料準備中...)");
                    progress.Minimum = 1;
                    progress.Value = 1;
                    progress.Show();

                    //TODO Service  的呼叫集中，比較容易管理。
                    DSXmlHelper hlpHistories = QueryPayment.GetPaymentHistories(SelectedPayment.Payment.Identity, history.Identity);

                    Document bills = OutputAllBill(template, progress, outputsetting, hlpHistories);

                    Process.Start(outputsetting.OutputFolder);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private Document OutputAllBill(Document template, ProgressForm progress, BillOutputSettingForm outputsetting, DSXmlHelper hlpHistories)
        {
            XmlElement[] histories = hlpHistories.GetElements("PaymentHistory");
            progress.Text = "繳費單產生進度";
            progress.Maximum = histories.Length;
            string savefolder = outputsetting.OutputFolder;
            string splitxpath = outputsetting.SplitXPath;

            //ElectronicPaper epaper = new ElectronicPaper("呵呵，這是繳費單！",
            //    CurrentUser.Instance.SchoolYear.ToString(),
            //    CurrentUser.Instance.Semester.ToString(),
            //    ViewerType.Student);

            Document bills = new Document();
            bills.Sections.Clear();

            //按照指定的分割排序。
            histories = Sort(histories, splitxpath);

            string splitString = string.Empty;
            foreach (XmlElement each in histories)
            {
                DSXmlHelper hlp = new DSXmlHelper(each);

                if (!string.IsNullOrEmpty(splitxpath)) //需要才分割檔案。
                {
                    if (hlp.GetText(splitxpath) != splitString)
                    {
                        if (bills.Sections.Count > 0)
                            bills.Save(Path.Combine(savefolder, GenerateFileName(splitString)));

                        bills = new Document();
                        bills.Sections.Clear();
                    }
                }

                DSXmlHelper hlpeach = new DSXmlHelper(each);
                Document single = GenerateBill(hlpeach, template);

                //電子報表。
                //MemoryStream mstream = new MemoryStream();
                //single.Save(mstream, SaveFormat.Doc);
                //PaperItem paper = new PaperItem(PaperFormat.Office2003Doc, mstream, hlpeach.GetText("RefStudentID"));
                //epaper.Append(paper);

                bills.Sections.Add(bills.ImportNode(single.Sections[0], true));

                if (!string.IsNullOrEmpty(splitxpath))
                {
                    splitString = hlp.GetText(splitxpath);
                    if (string.IsNullOrEmpty(splitString))
                        splitString = "剩下的";
                }

                progress.Value++;
                Application.DoEvents();

                if (outputsetting.OnlyPreview)
                {
                    if (progress.Value > 10)
                        break;
                }
            }

            if (bills.Sections.Count > 0)
            {
                bills.Save(Path.Combine(savefolder, GenerateFileName(splitString)));

                //epaper.ProgressReceiver = new ElectronicPaperProgress();
                //DispatcherProvider.Dispatch(epaper);
            }

            progress.Finish();
            return bills;
        }

        #region Sort Payment Histories
        private XmlElement[] Sort(XmlElement[] histories, string splitxpath)
        {
            if (string.IsNullOrEmpty(splitxpath))
                return histories;

            List<XmlElement> records = new List<XmlElement>(histories);
            records.Sort(new PaymentHistoryComparer(splitxpath));

            return records.ToArray();
        }

        private class PaymentHistoryComparer : IComparer<XmlElement>
        {
            private string _xpath;

            public PaymentHistoryComparer(string xpath)
            {
                _xpath = xpath;
            }

            #region IComparer<XmlElement> 成員

            public int Compare(XmlElement x, XmlElement y)
            {
                DSXmlHelper xx, xy;
                xx = new DSXmlHelper(x);
                xy = new DSXmlHelper(y);

                string xvalue = string.Format("{0}:{1}", xx.GetText(_xpath), xx.GetText("VirtualAccount"));
                string yvalue = string.Format("{0}:{1}", xy.GetText(_xpath), xy.GetText("VirtualAccount"));

                return xvalue.CompareTo(yvalue);
            }

            #endregion
        }
        #endregion

        private static string GenerateFileName(string splitString)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (splitString.IndexOf(c) >= 0)
                    splitString = splitString.Replace(c, '_');
            }

            if (string.IsNullOrEmpty(splitString))
                splitString = "繳費單";

            return splitString + ".doc";
        }

        private Document GenerateBill(DSXmlHelper hlpeach, Document template)
        {
            Dictionary<string, object> datasource = new Dictionary<string, object>();

            datasource.Add("UniqueNumber", hlpeach.GetText("Sequence"));
            datasource.Add("金額", hlpeach.GetText("Amount"));
            datasource.Add("截止日", GetDisplayDateString(hlpeach.GetText("BillData/Content/Expiration")));
            datasource.Add("虛擬帳號", hlpeach.GetText("VirtualAccount"));
            datasource.Add("姓名", hlpeach.GetText("StudentName"));
            datasource.Add("班級", hlpeach.GetText("ClassName"));
            datasource.Add("座號", hlpeach.GetText("SeatNumber"));
            datasource.Add("學號", hlpeach.GetText("StudentNumber"));
            datasource.Add("科別名稱", hlpeach.GetText("DeptName"));
            datasource.Add("超商條碼一", Utilities.CreateBarCode(hlpeach.GetText("BillData/Content/ShopCodes/Code[1]")));
            datasource.Add("超商條碼二", Utilities.CreateBarCode(hlpeach.GetText("BillData/Content/ShopCodes/Code[2]")));
            datasource.Add("超商條碼三", Utilities.CreateBarCode(hlpeach.GetText("BillData/Content/ShopCodes/Code[3]")));
            datasource.Add("郵局條碼一", Utilities.CreateBarCode(hlpeach.GetText("BillData/Content/PostCodes/Code[1]")));
            datasource.Add("郵局條碼二", Utilities.CreateBarCode(hlpeach.GetText("BillData/Content/PostCodes/Code[2]")));
            datasource.Add("郵局條碼三", Utilities.CreateBarCode(hlpeach.GetText("BillData/Content/PostCodes/Code[3]")));
            datasource.Add("繳款明細", GetPaymentItemsString(hlpeach));
            AddCustomeMergeField(datasource, hlpeach);

            Document payForm = template.Clone();
            payForm.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
            payForm.MailMerge.Execute(new List<string>(datasource.Keys).ToArray(), new List<object>(datasource.Values).ToArray());
            payForm.MailMerge.MergeField -= new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
            return payForm;
        }

        private void AddCustomeMergeField(Dictionary<string, object> datasource, DSXmlHelper hlpeach)
        {
            foreach (XmlElement each in hlpeach.GetElements("BillData/Content/MergeFields/Item"))
                datasource.Add("#" + each.GetAttribute("Name"), each.GetAttribute("Value"));
        }

        private static string GetPaymentItemsString(DSXmlHelper hlpeach)
        {
            StringBuilder items = new StringBuilder();
            foreach (XmlElement each in hlpeach.GetElements("BillData/Content/PaymentItems/Item"))
            {
                if (items.Length > 0)
                    items.AppendLine();

                items.Append(each.GetAttribute("Name"));
                items.Append("\t"); //一個 Tab
                items.Append(each.GetAttribute("Value"));
            }

            return items.ToString();
        }

        private void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (string.IsNullOrEmpty(e.FieldValue + ""))
                e.Field.Remove();

            if (e.FieldValue is Image)
            {
                DocumentBuilder builder = new DocumentBuilder(e.Document);
                builder.MoveToField(e.Field, false);
                double width1 = (builder.CurrentParagraph.ParentNode as Aspose.Words.Cell).CellFormat.Width;
                double width2 = (e.FieldValue as Image).Width / 6;
                double width = Math.Min(width1, width2);
                builder.InsertImage(e.FieldValue as Image, width, 40);
                e.Field.Remove();
            }
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            try
            {
                BillBatchInformation billbatch = cboBillBatchs.SelectedItem as BillBatchInformation;

                if (billbatch == null) return;

                if (billbatch.TemplateDocument != null)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Word 文件|*.doc";
                    dialog.FileName = "繳費單樣版.doc";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string file = dialog.FileName;
                        billbatch.TemplateDocument.Save(file);
                        Process.Start(file);
                    }
                }
                else
                    MsgBox.Show("此批次資料還未設定任何樣版。");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void btnUploadBill_Click(object sender, EventArgs e)
        {
            try
            {
                BillBatchInformation billbatch = cboBillBatchs.SelectedItem as BillBatchInformation;

                if (billbatch == null) return;

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Word 文件|*.doc";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document(dialog.FileName);
                    billbatch.TemplateBase64 = Utilities.GetBase64String(doc);
                    SelectedPayment.Payment.Save();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        #region GetDisplayDateString
        private static string GetDisplayDateString(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return string.Empty;
            else
            {
                DateTime dt;
                if (DateTime.TryParse(dateString, out dt))
                    return Utilities.GetMinGuoDateString(dt);
                else
                    return string.Empty;
            }
        }
        #endregion

        private void btiGenBill_Click(object sender, EventArgs e)
        {
            if (SelectedPayment == null) return;

            GT.Payment payment = SelectedPayment.Payment;

            BankConfig config = BankConfigManager.GetConfig(payment);

            if (config == null)
            {
                MsgBox.Show("您並未決定銀行組態，無法產生繳費單。");
                return;
            }

            IBankService service = BankServiceProvider.GetService(config);

            if (service == null)
            {
                MsgBox.Show(string.Format("找不到指定的銀行模組({0})。", config.ModuleCode));
                return;
            }

            BillGenerateForm bg = new BillGenerateForm(payment, service, config);
            if (bg.ShowDialog() == DialogResult.OK)
            {
                payment.ReloadBasicData();
                PaymentSelectChanged();
                foreach (object each in cboBillBatchs.Items)
                {
                    BillBatchInformation bill = each as BillBatchInformation;

                    if (bill == null) continue;

                    if (bill.Identity == bg.BatchIdentity)
                    {
                        cboBillBatchs.SelectedItem = bill;
                        break;
                    }
                }
            }
        }
        #endregion

        #region (Properties) Common
        internal string SchoolYear
        {
            get { return cboSchoolYear.Text; }
        }

        internal string Semester
        {
            get { return cboSemester.Text; }
        }

        private PaymentButtonItem _selected_payment;
        internal PaymentButtonItem SelectedPayment
        {
            get { return _selected_payment; }
        }

        private string _padding_select;
        internal string PaddingSelectPayment
        {
            get { return _padding_select; }
            set { _padding_select = value; }
        }

        private Dictionary<string, BankConfig> _bank_dic;
        internal Dictionary<string, BankConfig> BankDictionary
        {
            get
            {
                if (_bank_dic == null)
                    _bank_dic = BankConfigManager.GetBankDictionary();

                return _bank_dic;
            }
        }
        #endregion

        #region (Methods) Common
        /// <summary>
        /// 非同步副程式，使用時請注意。
        /// </summary>
        private void LoadPaymentList()
        {
            PaymentLoading.Visible = true;

            Arguments args = new Arguments();
            args.Add("SchoolYear", SchoolYear);
            args.Add("Semester", Semester);

            PaymentLoader.Execute(
                new LoaderExecutor(LoadPaymentListThread),       //第一個參數。
                new LoaderCallback(LoadPaymentListComplete),                   //第二個參數。
                args);                                                                                             //第三個參數。
        }

        private bool SelectPayment(PaymentButtonItem target)
        {
            if (target == SelectedPayment) return true;

            if (PaymentSelectChanging())
            {
                _selected_payment = target;
                PaymentSelectChanged();
                return true;
            }
            else
            {
                if (SelectedPayment != null)
                    SelectedPayment.RaiseClick();

                return false;
            }
        }

        /// <summary>
        /// 取得已下載到 Client 的 Payment 資料。
        /// </summary>
        private List<GT.Payment> GetPaymentList()
        {
            List<GT.Payment> payments = new List<SmartSchool.Payment.GT.Payment>();

            foreach (PaymentButtonItem each in ipPayments.Items)
                payments.Add(each.Payment);

            return payments;
        }

        private void AddPaymentItem(GT.Payment each, bool refresh)
        {
            PaymentButtonItem pitem = new PaymentButtonItem(each);
            pitem.Click += new EventHandler(PaymentItem_Click);
            pitem.DoubleClick += new EventHandler(PaymentItem_DoubleClick);
            ipPayments.Items.Add(pitem);

            if (refresh)
                ipPayments.Refresh();
        }

        private void ChangePaymentCommandStatus(bool enabled)
        {
        }

        private void ChangePaymentDetailCommandStatus(bool enabled)
        {
            btnExport.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnReports.Enabled = enabled;
        }

        private void SetSummaryInfoToClean()
        {
            lblTotalAmount.Text = string.Empty;
            lblPaidAmount.Text = string.Empty;
            lblDetailCount.Text = string.Empty;
            lblPaidCount.Text = string.Empty;
            lblUnpaidCount.Text = string.Empty;
            lblOverLimit.Text = string.Empty;
            pgTotalProgress.Value = 0;
            lblTotalProgress.Text = string.Format("收費總進度 ({0}%)", 0);
        }

        private void SetSummaryInfoToLoading()
        {
            lblTotalAmount.Text = "讀取資料中...";
            lblPaidAmount.Text = "讀取資料中...";
            lblDetailCount.Text = "讀取資料中...";
            lblPaidCount.Text = "讀取資料中...";
            lblUnpaidCount.Text = "讀取資料中...";
            lblOverLimit.Text = "讀取資料中...";
            pgTotalProgress.Value = 0;
            lblTotalProgress.Text = string.Format("讀取資料中...", 0);
        }
        #endregion

        private void txtFilter_InputTextChanged(object sender)
        {
            detaillist.FilterData(txtFilter.TextBox.Text);
        }

        private void PaymentItem_DoubleClick(object sender, EventArgs e)
        {
            ModifyPaymentForm modify = new ModifyPaymentForm(SelectedPayment.Payment);
            modify.PaymentSaving += new CancelEventHandler(Modify_PaymentSaving);

            if (modify.ShowDialog() == DialogResult.OK)
                PaymentSelectChanged(); //重新載入畫面上的資料。
        }

        private void Modify_PaymentSaving(object sender, CancelEventArgs e)
        {
            ModifyPaymentForm modify = sender as ModifyPaymentForm;
            GT.Payment targetPayment = modify.TargetPayment;

            foreach (PaymentButtonItem each in ipPayments.Items)
            {
                GT.Payment payment = each.Payment;

                if (payment == targetPayment)
                    continue;

                if (payment.Name == targetPayment.Name)
                {
                    MsgBox.Show("名稱重覆，請重新命名。");
                    e.Cancel = true;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (SelectedPayment == null) return;

            try
            {
                GT.Payment payment = SelectedPayment.Payment;
                string msg = string.Format("您確定要刪除「{0}」收費，目前有{1}項收費明細。", payment.Name, payment.Details.Count);
                DialogResult dr = MsgBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    EditPayment.Delete(int.Parse(SelectedPayment.Payment.Identity));
                    LoadPaymentList();
                }
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private void cboBillBatchs_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnViewBill.Enabled = (cboBillBatchs.Text != string.Empty);
            btnUploadBill.Enabled = (cboBillBatchs.Text != string.Empty);
            btnSaveBill.Enabled = (cboBillBatchs.Text != string.Empty);
        }

        private void PaymentManage_Load(object sender, EventArgs e)
        {

        }

        //密技
        private void tabControlPanel1_DoubleClick(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                groupPanel1.Visible = true;
        }

        private void btnExportVA_Click(object sender, EventArgs e)
        {
            try
            {
                BillBatchInformation history = cboBillBatchs.SelectedItem as BillBatchInformation;
                DSXmlHelper hlpHistories = QueryPayment.GetPaymentHistories(SelectedPayment.Payment.Identity, history.Identity);
                XmlElement[] histories = hlpHistories.GetElements("PaymentHistory");

                Workbook book = new Workbook();
                book.Worksheets.Clear();
                Worksheet sheet = book.Worksheets[book.Worksheets.Add()];

                int column = 0;

                sheet.Cells[0, column++].PutValue("UniqueNumber");
                sheet.Cells[0, column++].PutValue("金額");
                sheet.Cells[0, column++].PutValue("截止日");
                sheet.Cells[0, column++].PutValue("虛擬帳號");
                sheet.Cells[0, column++].PutValue("姓名");
                sheet.Cells[0, column++].PutValue("班級");
                sheet.Cells[0, column++].PutValue("座號");
                sheet.Cells[0, column++].PutValue("學號");

                int row = 1;
                foreach (XmlElement each in histories)
                {
                    DSXmlHelper hlpeach = new DSXmlHelper(each);

                    column = 0;

                    sheet.Cells[row, column++].PutValue(hlpeach.GetText("Sequence"));
                    sheet.Cells[row, column++].PutValue(hlpeach.GetText("Amount"));
                    sheet.Cells[row, column++].PutValue(hlpeach.GetText("BillData/Content/Expiration"));
                    sheet.Cells[row, column++].PutValue(hlpeach.GetText("VirtualAccount"));
                    sheet.Cells[row, column++].PutValue(hlpeach.GetText("StudentName"));
                    sheet.Cells[row, column++].PutValue(hlpeach.GetText("ClassName"));
                    sheet.Cells[row, column++].PutValue(hlpeach.GetText("SeatNumber"));
                    sheet.Cells[row, column++].PutValue(hlpeach.GetText("StudentNumber"));

                    row++;
                }

                book.Save("BillFormData.xls");
                Process.Start("BillFormData.xls");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void btnPayStatus_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> historyidlist = new List<string>();
                Dictionary<string, PaymentHistory> historylist = new Dictionary<string, PaymentHistory>();
                foreach (PaymentDetail eachDetail in SelectedPayment.Payment.Details)
                {
                    foreach (PaymentHistory eachHistory in eachDetail.Histories)
                    {
                        if (eachHistory.Cancelled)
                            continue;

                        historyidlist.Add(eachHistory.Identity);
                        historylist.Add(eachHistory.Identity, eachHistory);
                        eachHistory.Transactions.Clear();
                    }
                }

                DSXmlHelper trans = QueryPayment.GetTransactionsByHistory(historyidlist.ToArray());

                foreach (XmlElement each in trans.GetElements("Transaction"))
                {
                    Transaction tran = Transaction.Parse(each);
                    if (historylist.ContainsKey(tran.RefPaymentHistoryID))
                        historylist[tran.RefPaymentHistoryID].Transactions.Add(tran);
                }

                Workbook book = new Workbook();
                book.Worksheets.Clear();
                Worksheet sheet = book.Worksheets[book.Worksheets.Add()];

                List<PaymentDetail> sortedDetail = new List<PaymentDetail>(SelectedPayment.Payment.Details);
                SortPaymentDetail(sortedDetail);

                int column = 0;

                sheet.Cells[0, column++].PutValue("姓名");
                sheet.Cells[0, column++].PutValue("學號");
                sheet.Cells[0, column++].PutValue("班級");
                sheet.Cells[0, column++].PutValue("座號");
                sheet.Cells[0, column++].PutValue("應繳金額");
                sheet.Cells[0, column++].PutValue("已繳金額");
                sheet.Cells[0, column++].PutValue("繳費日期");
                sheet.Cells[0, column++].PutValue("繳費通路");
                sheet.Cells[0, column++].PutValue("手續費");

                int row = 1;
                foreach (PaymentDetail eachDetail in sortedDetail)
                {
                    foreach (PaymentHistory eachHistory in eachDetail.Histories)
                    {
                        if (eachHistory.Cancelled)
                            continue;

                        if (eachHistory.Transactions.Count <= 0)
                        {
                            column = 0;
                            sheet.Cells[row, column++].PutValue(eachDetail.StudentName);
                            sheet.Cells[row, column++].PutValue(eachDetail.StudentNumber);
                            sheet.Cells[row, column++].PutValue(eachDetail.ClassName);
                            sheet.Cells[row, column++].PutValue(eachDetail.SeatNumber);//座號
                            sheet.Cells[row, column++].PutValue(eachHistory.Amount);
                            sheet.Cells[row, column++].PutValue(eachHistory.PaidAmount);
                            row++;
                        }
                        else
                        {
                            foreach (Transaction eachTran in eachHistory.Transactions)
                            {
                                column = 0;
                                sheet.Cells[row, column++].PutValue(eachDetail.StudentName);
                                sheet.Cells[row, column++].PutValue(eachDetail.StudentNumber);
                                sheet.Cells[row, column++].PutValue(eachDetail.ClassName);
                                sheet.Cells[row, column++].PutValue(eachDetail.SeatNumber);//座號
                                sheet.Cells[row, column++].PutValue(eachHistory.Amount);
                                sheet.Cells[row, column++].PutValue(eachHistory.PaidAmount);
                                sheet.Cells[row, column++].PutValue(eachTran.PayDate.ToString("yyyy/MM/dd"));
                                sheet.Cells[row, column++].PutValue(GetShopDisplayName(eachTran));
                                sheet.Cells[row, column++].PutValue(eachTran.ChannelCharge);
                                row++;
                            }
                        }
                    }
                }

                book.Save("PayStatus.xls");
                Process.Start("PayStatus.xls");

            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }

        }

        private void SortPaymentDetail(List<PaymentDetail> details)
        {
            details.Sort(new DetailClassComparer());

            string previousClass = string.Empty;
            int sortCount = 0, sortIndex = 0;

            foreach (PaymentDetail each in new List<PaymentDetail>(details))
            {
                if (previousClass != each.ClassName)
                {
                    details.Sort(sortIndex, sortCount, new DetailSeatNoComparer());
                    sortIndex += sortCount;
                    sortCount = 0;
                }

                sortCount++;
                previousClass = each.ClassName;
            }
            details.Sort(sortIndex, sortCount, new DetailSeatNoComparer());
        }

        #region Detail Comparer
        private class DetailClassComparer : IComparer<PaymentDetail>
        {
            public int Compare(PaymentDetail x, PaymentDetail y)
            {
                return x.ClassName.CompareTo(y.ClassName);
            }
        }

        private class DetailSeatNoComparer : IComparer<PaymentDetail>
        {
            public int Compare(PaymentDetail x, PaymentDetail y)
            {
                int xv, yv;

                if (!int.TryParse(x.SeatNumber, out xv))
                    xv = 0;

                if (!int.TryParse(y.SeatNumber, out yv))
                    yv = 0;

                return xv.CompareTo(yv);
            }
        }
        #endregion

        private static string GetShopDisplayName(Transaction eachTran)
        {
            if (eachTran.ChannelCode == "SHOP")
                return "超商";
            else if (eachTran.ChannelCode == "POST")
                return "郵局";
            else
                return eachTran.ChannelCode;
        }

        private class ElectronicPaperProgress : IProgressReceiver
        {
            ProgressForm form;

            public ElectronicPaperProgress()
            {
                form = new ProgressForm("傳送電子報表…");
            }

            #region IProgressReceiver 成員

            public void ProcessStart()
            {
                form.Minimum = 1;
                form.Maximum = 100;
                form.Value = 1;
                form.Show();
            }

            public void ProcessEnd()
            {
                form.Value = 100;
                form.Close();
            }

            public void ProcessProgress(int progress)
            {
                form.Value = progress;
            }
            #endregion
        }
    }
}