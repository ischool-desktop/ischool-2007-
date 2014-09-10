using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Payment.GT
{
    internal class PaymentForm
    {
        private string _ref_payment_detail_id;
        public string RefPaymentDetailID
        {
            get { return _ref_payment_detail_id; }
            set { _ref_payment_detail_id = value; }
        }

        private string _identity;

        public string Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private bool _paid;
        public bool Paid
        {
            get { return _paid; }
            set { _paid = value; }
        }

        private int _paid_amount;
        public int PaidAmount
        {
            get { return _paid_amount; }
            set { _paid_amount = value; }
        }

        private string _va;
        public string VirtualAccount
        {
            get { return _va; }
            set { _va = value; }
        }

        private string _va_t;
        public string VirtualAccountT
        {
            get { return _va_t; }
            set { _va_t = value; }
        }

        private bool _cancelled;
        public bool Cancelled
        {
            get { return _cancelled; }
            set { _cancelled = value; }
        }

        #region Static Methods
        public static PaymentForm Parse(XmlElement data)
        {
            DSXmlHelper hlpdata = new DSXmlHelper(data);
            PaymentForm form = new PaymentForm();

            form.Identity = hlpdata.GetText("@ID");
            form.RefPaymentDetailID = hlpdata.GetText("RefPaymentDetailID");
            form.Amount = int.Parse(hlpdata.GetText("Amount"));
            form.PaidAmount = int.Parse(hlpdata.GetText("PaidAmount"));
            form.VirtualAccount = hlpdata.GetText("VirtualAccount");
            form.VirtualAccountT = hlpdata.GetText("VirtualAccountT");
            form.Paid = ParseBit(hlpdata.GetText("Paid"));
            form.Cancelled = ParseBit(hlpdata.GetText("Cancelled"));

            return form;
        }

        private static bool ParseBit(string value)
        {
            if (value == "0")
                return false;
            else if (value == "1")
                return true;
            else
                throw new ArgumentException("值只允許「0」、「1」。");
        }
        #endregion
    }

    internal class PaymentFormCollection : Dictionary<string, PaymentForm>
    {
        public static PaymentFormCollection Parse(DSXmlHelper data)
        {
            PaymentFormCollection forms = new PaymentFormCollection();

            foreach (XmlElement each in data.GetElements("PaymentHistory"))
            {
                PaymentForm form = PaymentForm.Parse(each);

                if (forms.ContainsKey(form.VirtualAccountT))
                    throw new PaymentModuleException("資料庫中的虛擬帳號重覆，請連絡澔學處理。", null);

                //使用去除檢查碼的虛擬帳號對帳。
                forms.Add(form.VirtualAccountT, form);
            }

            return forms;
        }
    }
}