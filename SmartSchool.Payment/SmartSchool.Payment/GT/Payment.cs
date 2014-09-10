using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using SmartSchool.Feature.Payment;

namespace SmartSchool.Payment.GT
{
    internal class Payment
    {
        #region Property Identity
        private string _identity;
        public string Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }
        #endregion

        #region Property SchoolYear
        private string _school_year;
        public string SchoolYear
        {
            get { return _school_year; }
            set { _school_year = value; }
        }
        #endregion

        #region Property Semester
        private string _semester;
        public string Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }
        #endregion

        #region Property Name
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region Property Config
        private PaymentConfig _config;
        public PaymentConfig Config
        {
            get { return _config; }
            set { _config = value; }
        }
        #endregion

        #region Property BillBatchInformations
        private BillBatchInformationCollection _bill_batch_infos = new BillBatchInformationCollection();
        public BillBatchInformationCollection BillBatchInformations
        {
            get { return _bill_batch_infos; }
            set { _bill_batch_infos = value; }
        }
        #endregion

        #region Property PaymentDetails
        private List<PaymentDetail> _details = new List<PaymentDetail>();
        /// <summary>
        /// 變更此屬性不會引發任何事件。
        /// </summary>
        public List<PaymentDetail> Details
        {
            get { return _details; }
            set { _details = value; }
        }
        #endregion

        #region Method Save
        public bool Save()
        {
            return SavePayment(this);
        }
        #endregion

        #region Method ReloadBasicData
        /// <summary>
        /// 重新向 Server 要資料，但僅限基本資料，不包含明細資料。
        /// </summary>
        public void ReloadBasicData()
        {
            if (string.IsNullOrEmpty(Identity))
                return;

            DSXmlHelper helper = SmartSchool.Feature.Payment.QueryPayment.GetPayment(Identity);

            DSXmlHelper payHelper = new DSXmlHelper(helper.GetElement("Payment"));

            string id = payHelper.GetText("@ID");
            string name = payHelper.GetText("PaymentName");
            string sy = payHelper.GetText("SchoolYear");
            string sems = payHelper.GetText("Semester");
            PaymentConfig hlpConfig = PaymentConfig.Parse(payHelper.GetElement("Config/PaymentConfig"));
            BillBatchInformationCollection billbatchs = BillBatchInformationCollection.Parse(payHelper.GetElement("BillBatchs/Content"));

            _name = name;
            _school_year = sy;
            _semester = sems;
            _config = hlpConfig;
            _bill_batch_infos = billbatchs;
        }
        #endregion

        #region Static Method Save
        internal static bool SavePayment(GT.Payment payment)
        {
            //儲存...
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("Payment");
            req.AddElement("Payment", "PaymentName", payment.Name);
            req.AddElement("Payment", "SchoolYear", payment.SchoolYear);
            req.AddElement("Payment", "Semester", payment.Semester);
            req.AddElement("Payment", "Config", payment.Config.ToXml().OuterXml, true);
            req.AddElement("Payment", "BillBatchs", BillBatchInformationCollection.SerialToXml(payment.BillBatchInformations).OuterXml, true);

            if (!string.IsNullOrEmpty(payment.Identity))
            {
                req.AddElement("Payment", "Condition");
                req.AddElement("Payment/Condition", "ID", payment.Identity);
            }

            try
            {
                if (string.IsNullOrEmpty(payment.Identity))
                    payment.Identity = EditPayment.Insert(req);
                else
                    EditPayment.Update(req);
            }
            catch (Exception ex)
            {
                PaymentModuleException exp = new PaymentModuleException("呼叫 Service 儲存 Payment 資料錯誤。", ex);
                CurrentUser.ReportError(exp);
                MsgBox.Show(ex.Message);

                return false;
            }

            return true;
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
