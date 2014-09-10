using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Payment
{
    public static class EditPayment
    {
        public static string Insert(DSXmlHelper dsreq)
        {
            DSResponse dsrsp = FeatureBase.CallService("SmartSchool.Payment.Insert", new DSRequest(dsreq));
            if (dsrsp.HasContent)
            {
                DSXmlHelper helper = dsrsp.GetContent();
                string newid = helper.GetText("NewID");
                return newid;
            }
            return "";
        }

        public static void Update(DSXmlHelper dsreq)
        {
            FeatureBase.CallService("SmartSchool.Payment.Update", new DSRequest(dsreq));
        }

        public static void Delete(int identity)
        {
            DSXmlHelper dsreq = new DSXmlHelper("Request");
            dsreq.AddElement("Payment");
            dsreq.AddElement("Payment", "ID", identity.ToString());

            FeatureBase.CallService("SmartSchool.Payment.Delete", new DSRequest(dsreq));
        }

        /// <summary>
        /// 更新指定收費的「收費明細」資料。
        /// </summary>
        public static void UpdatePaymentDetails(DSXmlHelper req)
        {
            FeatureBase.CallService("SmartSchool.Payment.Detail.Update", new DSRequest(req));
        }

        /// <summary>
        /// 新增指定收費的「收費明細」資料。
        /// </summary>
        /// <param name="req"></param>
        public static void InsertPaymentDetails(DSXmlHelper req)
        {
            FeatureBase.CallService("SmartSchool.Payment.Detail.Insert", new DSRequest(req));
        }

        public static void ResetPaymentDetailsDirty(string paymentId)
        {
            DSXmlHelper dsreq = new DSXmlHelper("Request");
            dsreq.AddElement("PaymentDetail");
            dsreq.AddElement("PaymentDetail", "Condition");
            dsreq.AddElement("PaymentDetail/Condition", "RefPaymentID", paymentId);

            FeatureBase.CallService("SmartSchool.Payment.Detail.ResetDirty", new DSRequest(dsreq));
        }

        /// <summary>
        /// 將未繳費的繳費單標記為取消。
        /// </summary>
        /// <param name="paymentDetails"></param>
        public static void CancelPreviousBill(params string[] paymentDetails)
        {
            if (paymentDetails.Length <= 0) return;

            DSXmlHelper dsreq = new DSXmlHelper("Request");
            dsreq.AddElement("PaymentDetail");
            dsreq.AddElement("PaymentDetail", "Condition");

            foreach (string each in paymentDetails)
                dsreq.AddElement("PaymentDetail/Condition", "RefPaymentDetailID", each);

            FeatureBase.CallService("SmartSchool.Payment.History.CancelBill", new DSRequest(dsreq));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment_detail_id"></param>
        public static void DeletePaymentDetail(string payment_detail_id)
        {
            DSXmlHelper dsreq = new DSXmlHelper("Request");
            dsreq.AddElement("PaymentDetail");
            dsreq.AddElement("PaymentDetail", "ID", payment_detail_id);

            FeatureBase.CallService("SmartSchool.Payment.Detail.Delete", new DSRequest(dsreq));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        public static void InsertPaymentHistory(DSXmlHelper req)
        {
            FeatureBase.CallService("SmartSchool.Payment.History.Insert", new DSRequest(req));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        public static void DeletePaymentHistory(params string[] payment_detail_ids)
        {
            DSXmlHelper dsreq = new DSXmlHelper("Request");
            dsreq.AddElement("PaymentHistory");
            foreach (string each_id in payment_detail_ids)
                dsreq.AddElement("PaymentHistory", "RefPaymentDetailID", each_id);

            FeatureBase.CallService("SmartSchool.Payment.History.Delete", new DSRequest(dsreq));
        }

        public static void UpdateTransactions(DSXmlHelper req)
        {
            FeatureBase.CallService("SmartSchool.Payment.Transaction.Update", new DSRequest(req));
        }

        public static void UpdatePaymentHistory(DSXmlHelper req)
        {
            FeatureBase.CallService("SmartSchool.Payment.History.Update", new DSRequest(req));
        }
    }
}
