using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.Globalization;

namespace SmartSchool.Payment.GT
{
    #region TransactionStatus
    internal enum TransactionStatus
    {
        /// <summary>
        /// 代表此交易是新產生。
        /// </summary>
        New = 0,
        /// <summary>
        /// 代表此交易已完成對帳。
        /// </summary>
        Success = 1,
        /// <summary>
        /// 代表此交易已對帳，但對帳失敗。
        /// </summary>
        Fail = 2
    }
    #endregion

    #region Transaction
    internal class Transaction
    {
        private string _identity;

        public string Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        private string _va;
        public string VirtualAccount
        {
            get { return _va; }
            set { _va = value; }
        }

        /// <summary>
        /// 去除檢查碼的虛擬帳號。
        /// </summary>
        public string VirtualAccountT
        {
            get
            {
                int len = VirtualAccount.Length;
                return VirtualAccount.Remove(len - 1);
            }
        }

        private string _ref_payment_history_id;
        public string RefPaymentHistoryID
        {
            get { return _ref_payment_history_id; }
            set { _ref_payment_history_id = value; }
        }

        private PaymentForm _ref_payment_form_obj;
        public PaymentForm RefPaymentFormObject
        {
            get { return _ref_payment_form_obj; }
            set
            {
                _ref_payment_form_obj = value;
                RefPaymentHistoryID = value.Identity;
            }
        }

        private string _channel_code;
        public string ChannelCode
        {
            get { return _channel_code; }
            set { _channel_code = value; }
        }

        private int _channel_charge;
        public int ChannelCharge
        {
            get { return _channel_charge; }
            set { _channel_charge = value; }
        }

        private int _fee;
        public int Fee
        {
            get { return _fee; }
            set { _fee = value; }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        private DateTime _create_date;
        public DateTime CreateDate
        {
            get { return _create_date; }
            set { _create_date = value; }
        }

        private DateTime _pay_date;
        public DateTime PayDate
        {
            get { return _pay_date; }
            set { _pay_date = value; }
        }

        private DateTime _last_process_date;
        public DateTime LastProcessDate
        {
            get { return _last_process_date; }
            set { _last_process_date = value; }
        }

        private XmlElement _full_detail;
        public XmlElement FullDetail
        {
            get { return _full_detail; }
            set { _full_detail = value; }
        }

        private TransactionStatus _status;
        public TransactionStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 產生更新類型的 Xml。只更新二個欄位：RefPaymentHistory、Status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public XmlElement ToUpdateXml()
        {
            DSXmlHelper hlp = new DSXmlHelper("Transaction");
            hlp.AddElement(".", "RefPaymentHistoryID", this.RefPaymentHistoryID);
            hlp.AddElement(".", "Status", ParseStatus(this.Status));
            hlp.AddElement(".", "Condition");
            hlp.AddElement("Condition", "ID", this.Identity);

            return hlp.BaseElement;
        }

        #region Static Methods
        public static Transaction Parse(XmlElement data)
        {
            DSXmlHelper hlpdata = new DSXmlHelper(data);
            Transaction objdata = new Transaction();

            objdata.Identity = hlpdata.GetText("@ID");
            objdata.VirtualAccount = hlpdata.GetText("VirtualAccount");
            objdata.RefPaymentHistoryID = hlpdata.GetText("RefPaymentHistoryID");
            objdata.ChannelCode = hlpdata.GetText("ChannelCode");
            objdata.ChannelCharge = int.Parse(hlpdata.GetText("ChannelCharge"));
            objdata.Fee = int.Parse(hlpdata.GetText("Fee"));
            objdata.Comment = hlpdata.GetText("Comment");
            objdata.CreateDate = ParsePGSqlTimestamp(hlpdata.GetText("CreateDate"));
            objdata.PayDate = ParsePGSqlTimestamp(hlpdata.GetText("PayDate"));
            objdata.LastProcessDate = ParsePGSqlTimestamp(hlpdata.GetText("LastProcessDate"));
            objdata.FullDetail = GetFullDetailXml(hlpdata);
            objdata.Status = ParseStatus(hlpdata.GetText("Status"));

            return objdata;
        }

        private static string PGSqlDateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        private static string PGSqlDateTimeFormat2 = "yyyy-MM-dd HH:mm:ss";

        private static DateTime ParsePGSqlTimestamp(string timestamp)
        {
            if (string.IsNullOrEmpty(timestamp))
                return DateTime.MinValue;

            DateTime dt;

            if (DateTime.TryParseExact(timestamp,
                PGSqlDateTimeFormat,
                DateTimeFormatInfo.InvariantInfo,
                DateTimeStyles.None,
                out dt))

                return dt;
            else
            {
                if (DateTime.TryParseExact(timestamp,
                    PGSqlDateTimeFormat2,
                    DateTimeFormatInfo.InvariantInfo,
                    DateTimeStyles.None,
                    out dt))

                    return dt;
                else
                    return DateTime.MinValue;
            }
        }

        private static string ToPGSqlTimestampString(DateTime datetime)
        {
            return datetime.ToString(PGSqlDateTimeFormat);
        }

        private static XmlElement GetFullDetailXml(DSXmlHelper hlpdata)
        {
            string fullDetail = hlpdata.GetText("FullDetail");

            if (string.IsNullOrEmpty(fullDetail))
                return null;

            return DSXmlHelper.LoadXml(fullDetail);
        }

        private static TransactionStatus ParseStatus(string value)
        {
            int v = int.Parse(value);
            return (TransactionStatus)v;
        }

        private static string ParseStatus(TransactionStatus value)
        {
            return ((int)value).ToString();
        }
        #endregion
    }
    #endregion

    #region TransactionCollection
    internal class TransactionCollection : List<Transaction>
    {
        public static TransactionCollection Parse(DSXmlHelper data)
        {
            TransactionCollection trans = new TransactionCollection();

            foreach (XmlElement each in data.GetElements("Transaction"))
                trans.Add(Transaction.Parse(each));

            return trans;
        }

        public List<string> GetVirtualAccountListT()
        {
            List<string> valist = new List<string>();
            foreach (Transaction each in this)
            {
                valist.Add(each.VirtualAccountT);
            }

            return valist;
        }
    }
    #endregion

}
