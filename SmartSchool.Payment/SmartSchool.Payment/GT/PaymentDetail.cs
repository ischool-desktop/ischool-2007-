using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Payment.GT
{
    internal class PaymentDetail
    {
        private string _identity;

        public string Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        private string _class_name;
        public string ClassName
        {
            get { return _class_name; }
            set { _class_name = value; }
        }

        private string _student_nmae;
        public string StudentName
        {
            get { return _student_nmae; }
            set { _student_nmae = value; }
        }

        private string _student_number;
        public string StudentNumber
        {
            get { return _student_number; }
            set { _student_number = value; }
        }

        private string _seat_number;
        public string SeatNumber
        {
            get { return _seat_number; }
            set { _seat_number = value; }
        }

        private string _ref_student_id;
        public string RefStudentID
        {
            get { return _ref_student_id; }
            set { _ref_student_id = value; }
        }


        private string _ref_payment_id;
        public string RefPaymentID
        {
            get { return _ref_payment_id; }
            set { _ref_payment_id = value; }
        }

        private string _payment_name;
        public string PaymentName
        {
            get { return _payment_name; }
            set { _payment_name = value; }
        }

        private bool _is_dirty;
        public bool IsDirtyRecord
        {
            get { return _is_dirty; }
            set { _is_dirty = value; }
        }

        private int _total_amount;
        public int Amount
        {
            get { return _total_amount; }
            set { _total_amount = value; }
        }

        public bool IsPaidSuccess
        {
            get { return GetPaidAmount() >= Amount; }
        }

        public int GetPaidAmount()
        {
            int paidAmount = 0;
            foreach (PaymentHistory each in Histories)
            {
                if (each.Paid)
                    paidAmount += each.PaidAmount;
            }

            return paidAmount;
        }

        public static Dictionary<string, string> ParseMergeFields(XmlElement xmldata)
        {
            Dictionary<string, string> mfields = new Dictionary<string, string>();

            foreach (XmlElement each in xmldata.SelectNodes("Item"))
                mfields.Add(each.GetAttribute("Name"), each.GetAttribute("Value"));

            return mfields;
        }

        public static XmlElement SerialMergeFieldsToXml(Dictionary<string, string> data, string rootName)
        {
            DSXmlHelper hlpdata = new DSXmlHelper(rootName);
            foreach (KeyValuePair<string, string> each in data)
            {
                XmlElement item = hlpdata.AddElement(".", "Item");
                item.SetAttribute("Name", each.Key);
                item.SetAttribute("Value", each.Value);
            }

            return hlpdata.BaseElement;
        }

        private Dictionary<string, string> _merge_fields = new Dictionary<string, string>();
        /// <summary>
        /// 取得或設定合併欄位資訊。
        /// </summary>
        public Dictionary<string, string> MergeFields
        {
            get { return _merge_fields; }
            set { _merge_fields = value; }
        }

        public static Dictionary<string, int> ParsePayItems(XmlElement xmldata)
        {
            Dictionary<string, int> mfields = new Dictionary<string, int>();

            foreach (XmlElement each in xmldata.SelectNodes("Item"))
            {
                int amount;
                if (!int.TryParse(each.GetAttribute("Value"), out amount))
                    amount = int.MinValue; //沒有 Parse 成功就....。

                mfields.Add(each.GetAttribute("Name"), amount);
            }

            return mfields;
        }

        public static XmlElement SerialPayItemsToXml(Dictionary<string, int> data, string rootName)
        {
            DSXmlHelper hlpdata = new DSXmlHelper(rootName);
            foreach (KeyValuePair<string, int> each in data)
            {
                XmlElement item = hlpdata.AddElement(".", "Item");
                item.SetAttribute("Name", each.Key);
                item.SetAttribute("Value", each.Value.ToString());
            }

            return hlpdata.BaseElement;
        }

        private Dictionary<string, int> _pay_items = new Dictionary<string, int>();
        /// <summary>
        /// 取得或設定費用細項。
        /// </summary>
        public Dictionary<string, int> PayItems
        {
            get { return _pay_items; }
            set { _pay_items = value; }
        }

        private List<PaymentHistory> _histories = new List<PaymentHistory>();
        /// <summary>
        /// 取得或設定繳費記錄。
        /// </summary>
        public List<PaymentHistory> Histories
        {
            get { return _histories; }
            set { _histories = value; }
        }

    }
}
