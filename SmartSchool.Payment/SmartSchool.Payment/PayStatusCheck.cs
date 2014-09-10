using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Payment.GT;
using SmartSchool.Feature.Payment;
using System.Threading;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Payment
{
    /// <summary>
    /// 重覆繳費
    /// 繳費金額錯誤
    /// 使用了錯誤的繳費單
    /// </summary>
    public partial class PayStatusCheck : BaseForm
    {
        public PayStatusCheck()
        {
            InitializeComponent();
        }

        private PayStatusChecker _pay_status;

        private PayStatusChecker PayStatus
        {
            get { return _pay_status; }
            set { _pay_status = value; }
        }

        #region 核對交易
        private void PayStatusCheck_Load(object sender, EventArgs e)
        {
            Arguments args = new Arguments();
            AsyncDataLoader loader = new AsyncDataLoader();
            loader.Execute(new LoaderExecutor(CalculateThread),
                new LoaderCallback(CalculateCallback),
                args);
        }

        private object CalculateThread(Arguments args)
        {
            DSXmlHelper hlpTrans = QueryPayment.GetTransactions("0"); //0 代表新建立的交易。
            TransactionCollection objtrans = TransactionCollection.Parse(hlpTrans);
            DSXmlHelper hlpForms = QueryPayment.GetPaymentForms(objtrans.GetVirtualAccountListT().ToArray());
            PaymentFormCollection objforms = PaymentFormCollection.Parse(hlpForms);

            PayStatusChecker checker = new PayStatusChecker();
            checker.AllPaymentForm = objforms;
            checker.AllTransaction = objtrans;
            checker.Calculate();

            return checker;
        }

        private void CalculateCallback(ExecuteResult result)
        {
            if (result.Success)
            {
                PayStatus = result.Result as PayStatusChecker;
                lblTotal.Text = PayStatus.AllTransaction.Count.ToString();
                lblSuccess.Text = PayStatus.SuccessTransactions.Count.ToString();
                lblFail.Text = PayStatus.FailTransactions.Count.ToString();
                btnSaveResult.Enabled = true;
            }
            else
            {
                lblTotal.Text = "錯誤";
                lblSuccess.Text = "錯誤";
                lblFail.Text = "錯誤";

                if (result.Error == null) return;

                CurrentUser.ReportError(result.Error);
                MsgBox.Show(result.Error.Message);
            }
        }
        #endregion

        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            try
            {
                btnSaveResult.Enabled = false;
                btnExit.Enabled = false;
                Text = "對帳處理 (儲存結果中...)";

                List<DSXmlHelper> transreqs = new List<DSXmlHelper>();
                List<DSXmlHelper> formreqs = new List<DSXmlHelper>();

                DSXmlHelper reqTrans = null, reqForms = null;
                int packagesize = 100, count = 0;

                foreach (Transaction each in PayStatus.SuccessTransactions)
                {
                    if (count % packagesize == 0)
                    {
                        reqTrans = new DSXmlHelper("Request");
                        reqForms = new DSXmlHelper("Request");

                        transreqs.Add(reqTrans);
                        formreqs.Add(reqForms);
                    }

                    reqTrans.AddElement(".", each.ToUpdateXml());

                    PaymentForm form = each.RefPaymentFormObject;
                    DSXmlHelper hlpform = new DSXmlHelper(reqForms.AddElement("PaymentHistory"));
                    hlpform.AddElement(".", "Paid", "1");
                    hlpform.AddElement(".", "PaidAmount", form.PaidAmount.ToString());
                    hlpform.AddElement("Condition");
                    hlpform.AddElement("Condition", "ID", form.Identity);

                    count++;
                }

                foreach (Transaction each in PayStatus.FailTransactions)
                {
                    if (count % packagesize == 0)
                    {
                        reqTrans = new DSXmlHelper("Request");
                        transreqs.Add(reqTrans);
                    }

                    reqTrans.AddElement(".", each.ToUpdateXml());

                    count++;
                }

                foreach (DSXmlHelper each in transreqs)
                    EditPayment.UpdateTransactions(each);

                foreach (DSXmlHelper each in formreqs)
                    EditPayment.UpdatePaymentHistory(each);

                Close();
            }
            catch (Exception ex)
            {
                btnSaveResult.Enabled = false;
                btnExit.Enabled = true;
                Text = "對帳處理";

                CurrentUser.ReportError(new PaymentModuleException("收類模組在儲存對帳結果時錯誤(未造成系統失敗)。", ex));
                MsgBox.Show(ex.Message);
            }

        }

        private void btnExportFailTrans_Click(object sender, EventArgs e)
        {
        }

        //private delegate void SetProgressInvoker(int value);

        //private void SetProgress(int value)
        //{
        //    if (progressBarX1.InvokeRequired)
        //        progressBarX1.Invoke(new SetProgressInvoker(SetProgress), value);
        //    else
        //        progressBarX1.Value = value;
        //}

        private class PayStatusChecker
        {
            private PaymentFormCollection _forms;
            public PaymentFormCollection AllPaymentForm
            {
                get { return _forms; }
                set { _forms = value; }
            }

            private TransactionCollection _trans;
            public TransactionCollection AllTransaction
            {
                get { return _trans; }
                set { _trans = value; }
            }

            private TransactionCollection _success_trans;
            public TransactionCollection SuccessTransactions
            {
                get { return _success_trans; }
            }

            private TransactionCollection _fail_trans;
            public TransactionCollection FailTransactions
            {
                get { return _fail_trans; }
            }

            public void Calculate()
            {
                _success_trans = new TransactionCollection();
                _fail_trans = new TransactionCollection();

                foreach (Transaction each in AllTransaction)
                {
                    if (AllPaymentForm.ContainsKey(each.VirtualAccountT))
                    {
                        each.RefPaymentFormObject = AllPaymentForm[each.VirtualAccountT];
                        each.RefPaymentFormObject.PaidAmount += each.Fee; //計算已付金額。
                        each.Status = TransactionStatus.Success;
                        SuccessTransactions.Add(each);
                    }
                    else
                    {
                        each.Status = TransactionStatus.Fail;
                        FailTransactions.Add(each);
                    }
                }
            }
        }
    }
}