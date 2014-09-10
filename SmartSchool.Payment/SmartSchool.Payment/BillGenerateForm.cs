using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Payment;
using System.Xml;
using SmartSchool.Payment.Interfaces;
using Aspose.Words;
using System.IO;
using SmartSchool.Payment.GT;
using SmartSchool.Payment.BillTemplate;

namespace SmartSchool.Payment
{
    internal partial class BillGenerateForm : BaseForm
    {
        public BillGenerateForm(GT.Payment payment, IBankService service, BankConfig config)
        {
            InitializeComponent();

            Payment = payment;
            BankService = service;
            BankConfig = config;

            txtBatchName.Text = string.Format("{0}繳費資料 (日期時間：{1})", Payment.Name, DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            txtExpiration.Text = Payment.Config.DefaultExpiration;
        }

        #region Properties
        private GT.Payment _payment;
        public GT.Payment Payment
        {
            get { return _payment; }
            private set { _payment = value; }
        }

        private IBankService _service;
        public IBankService BankService
        {
            get { return _service; }
            private set { _service = value; }
        }
        private BankConfig _config;
        public BankConfig BankConfig
        {
            get { return _config; }
            private set { _config = value; }
        }

        private string _batch_identity;
        /// <summary>
        /// 此次批次的唯一編號。
        /// </summary>
        public string BatchIdentity
        {
            get { return _batch_identity; }
        }

        #endregion

        private EnhancedErrorProvider _errors = new EnhancedErrorProvider();
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            btnGenerate.Enabled = false;
            lblMsg.Text = "準備產生繳費單資料...";

            #region Valid Data
            _errors.Clear();

            if (string.IsNullOrEmpty(txtBatchName.Text))
            {
                _errors.SetError(txtBatchName, "請輸入批次名稱。");
                return;
            }

            DateTime expiration;
            if (!DateTime.TryParse(txtExpiration.Text, out expiration))
            {
                _errors.SetError(txtExpiration, "截止日期格式不正確，例：2008/10/15。");
                return;
            }
            #endregion

            int amountLimit = BankService.GetAmountLimit(BankConfig);
            IBillCodeGenerator billgen = BankService.CreateGenerator(BankConfig, expiration);

            bool executeRequired = false;

            #region 產生虛擬帳號與通路條碼。 results
            DSXmlHelper historyHelper = new DSXmlHelper("Request");
            List<BillCodeParameter> args = new List<BillCodeParameter>();

            List<PaymentDetail> sortedDetails = new List<PaymentDetail>(Payment.Details);
            sortedDetails.Sort(new Comparison<PaymentDetail>(ByClassNameAmount));

            foreach (PaymentDetail each in sortedDetails)
            {
                //如果不必要產生繳費單...
                if (!GenerateRequired(each)) continue;

                executeRequired = true; //確定 Service 呼叫要被執行。

                //each.Identity -> PaymentDetailID
                BillCodeParameter arg = new BillCodeParameter(each.Identity);

                if (each.GetPaidAmount() > 0)
                    arg.Amount = each.Amount - each.GetPaidAmount();
                else
                    arg.Amount = each.Amount;

                args.Add(arg);
            }

            List<BillCodeResult> results = billgen.Generate(args);
            #endregion

            #region 產生繳費記錄。 hlphistories
            //由 PaymentDetailID 當 Key。
            Dictionary<string, BillCodeResult> dicresults = new Dictionary<string, BillCodeResult>();
            foreach (BillCodeResult each in results)
                dicresults.Add(each.Identity, each);

            List<DSXmlHelper> requests = new List<DSXmlHelper>();
            DSXmlHelper hlphistories = null;
            List<string> detailidlist = new List<string>();
            _batch_identity = Guid.NewGuid().ToString();//建立一個唯一編號。

            int package_size = 100, count = 0;

            lblMsg.Text = "開始產生繳費單資料...";
            foreach (PaymentDetail each in Payment.Details)
            {
                //如果不必要產生繳費單...
                if (!GenerateRequired(each)) continue;

                //將需要產生繳費單資料的記錄放到 List 中。
                detailidlist.Add(each.Identity);

                if (count % package_size == 0)
                {
                    hlphistories = new DSXmlHelper("Request");
                    requests.Add(hlphistories);
                }

                BillCodeResult result = dicresults[each.Identity];
                BillCode code = result.BillCode;
                DSXmlHelper hlphistory = new DSXmlHelper(hlphistories.AddElement("PaymentHistory"));

                hlphistory.AddElement(".", "RefPaymentDetailID", each.Identity);
                hlphistory.AddElement(".", "Amount", (each.Amount - each.GetPaidAmount()).ToString());
                hlphistory.AddElement(".", "VirtualAccount", code.VirtualAccount);
                hlphistory.AddElement(".", "VirtualAccountT", code.VirtualAccount.Substring(0, 15));
                hlphistory.AddElement(".", "Sequence", code.Sequence.ToString());
                hlphistory.AddElement(".", "BatchRef", this.BatchIdentity);
                #region 建立繳費單資料 BillData Element
                hlphistory.AddElement(".", "BillData");
                DSXmlHelper hlpbilldata = new DSXmlHelper(hlphistory.AddElement("BillData", "Content"));

                hlpbilldata.AddElement(".", PaymentDetail.SerialMergeFieldsToXml(each.MergeFields, "MergeFields"));

                DSXmlHelper hlpPayItems = new DSXmlHelper(PaymentDetail.SerialPayItemsToXml(each.PayItems, "PaymentItems"));
                if (each.GetPaidAmount() > 0)
                {
                    XmlElement divItem = hlpPayItems.AddElement(".", "Item");
                    divItem.SetAttribute("Name", "已繳金額");
                    divItem.SetAttribute("Value", (each.GetPaidAmount() * -1).ToString());
                }

                hlpbilldata.AddElement(".", hlpPayItems.BaseElement);

                // 7-11 條碼。
                if (code.SupplyChains.Contains(SupplyChains.Shop))
                {
                    hlpbilldata.AddElement(".", "ShopCodes");
                    foreach (string eachCode in code.SupplyChains.GetCode(SupplyChains.Shop).Codes)
                        hlpbilldata.AddElement("ShopCodes", "Code", eachCode);
                }

                //郵局條碼。
                if (code.SupplyChains.Contains(SupplyChains.Post))
                {
                    hlpbilldata.AddElement(".", "PostCodes");
                    foreach (string eachCode in code.SupplyChains.GetCode(SupplyChains.Post).Codes)
                        hlpbilldata.AddElement("PostCodes", "Code", eachCode);
                }

                hlpbilldata.AddElement(".", "Expiration", expiration.ToString("yyyy/MM/dd"));
                #endregion

                count++;
            }
            #endregion

            if (executeRequired)
            {
                try
                {
                    pbProgress.Maximum = requests.Count + 3;

                    //取消之前產生的繳費記錄。
                    EditPayment.CancelPreviousBill(detailidlist.ToArray());
                    pbProgress.Value = 1;

                    //新增繳費記錄。
                    foreach (DSXmlHelper each in requests)
                    {
                        EditPayment.InsertPaymentHistory(each);
                        pbProgress.Value++;
                    }

                    //建立批次資訊，並將批次資訊儲存到收費中。
                    BillBatchInformation billbatch = CreateBillBatchInformation();
                    Payment.BillBatchInformations.Add(billbatch);
                    Payment.Save();
                    pbProgress.Value++;

                    //重設指定收費的收費明細 Dirty 狀態。
                    EditPayment.ResetPaymentDetailsDirty(Payment.Identity);
                    pbProgress.Value++;
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MsgBox.Show(ex.Message);
                    DialogResult = DialogResult.None;
                }
            }
            else
            {
                MsgBox.Show("目前沒有任何明細需要產生繳費資料。");
                DialogResult = DialogResult.None;
            }

            lblMsg.Text = "";
            btnGenerate.Enabled = true;
        }

        private int ByClassNameAmount(PaymentDetail x, PaymentDetail y)
        {
            string xx = string.Format("{0}:{1}", x.ClassName, x.Amount.ToString().PadLeft(6, '0'));
            string yy = string.Format("{0}:{1}", y.ClassName, y.Amount.ToString().PadLeft(6, '0'));

            return xx.CompareTo(yy);
        }

        private BillBatchInformation CreateBillBatchInformation()
        {
            BillBatchInformation billbatch = new BillBatchInformation();
            billbatch.Identity = BatchIdentity;
            billbatch.Expiration = txtExpiration.Text;
            billbatch.Name = txtBatchName.Text;
            billbatch.TemplateBase64 = Utilities.GetBase64String(TemplateManager.DefaultTemplate);
            return billbatch;
        }

        private bool GenerateRequired(PaymentDetail data)
        {
            //如果已繳清的人不產生繳費單。
            if (data.Amount - data.GetPaidAmount() <= 0)
                return false;

            //未更動過的資料不產生繳費單。
            if (!data.IsDirtyRecord)
                return false;

            return true;
        }
    }
}