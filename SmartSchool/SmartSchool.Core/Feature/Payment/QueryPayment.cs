using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Payment
{
    [QueryRequest()]
    public static class QueryPayment
    {
        public static DSXmlHelper GetAbstractList()
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");

            return FeatureBase.CallService("SmartSchool.Payment.GetDetailList", new DSRequest(req)).GetContent();
        }

        public static DSXmlHelper GetAbstractList(string schoolYear, string semester)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "SchoolYear", schoolYear);
            req.AddElement("Condition", "Semester", semester);

            return FeatureBase.CallService("SmartSchool.Payment.GetDetailList", new DSRequest(req)).GetContent();
        }

        /// <summary>
        /// 取得一筆收費的詳細資料。
        /// </summary>
        public static DSXmlHelper GetPayment(string id)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "ID", id);

            return FeatureBase.CallService("SmartSchool.Payment.GetDetailList", new DSRequest(req)).GetContent();
        }

        /// <summary>
        /// 取得指定收費的「收費細項」資料。
        /// </summary>
        public static DSXmlHelper GetPaymentDetails(string id)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "RefPaymentID", id);

            return FeatureBase.CallService("SmartSchool.Payment.Detail.GetDetailList", new DSRequest(req)).GetContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student_ids"></param>
        /// <returns></returns>
        public static DSXmlHelper GetPaymentDetails(string payment_id, params string[] student_ids)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "RefPaymentID", payment_id);
            foreach (string each_student_id in student_ids)
                req.AddElement("Condition", "RefStudentID", each_student_id);

            return FeatureBase.CallService("SmartSchool.Payment.Detail.GetDetailList", new DSRequest(req)).GetContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment_id"></param>
        /// <returns></returns>
        public static DSXmlHelper GetPaymentDetailStudents(string payment_id)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "RefPaymentID", payment_id);

            return FeatureBase.CallService("SmartSchool.Payment.Detail.GetStudentList", new DSRequest(req)).GetContent();
        }

        public static DSXmlHelper GetPaymentDetailStudents(string payment_id, bool isDirty)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "RefPaymentID", payment_id);
            req.AddElement("Condition", "IsDirty", isDirty ? "1" : "0");

            return FeatureBase.CallService("SmartSchool.Payment.Detail.GetStudentList", new DSRequest(req)).GetContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student_ids"></param>
        /// <returns></returns>
        public static DSXmlHelper GetPaymentDetailsByStudents(params string[] student_ids)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            foreach (string each_student_id in student_ids)
                req.AddElement("Condition", "RefStudentID", each_student_id);

            return FeatureBase.CallService("SmartSchool.Payment.Detail.GetDetailList", new DSRequest(req)).GetContent();
        }

        /// <summary>
        /// 取得繳費記錄資料。
        /// </summary>
        public static DSXmlHelper GetPaymentHistories(params string[] payment_detail_id)
        {
            if (payment_detail_id.Length <= 0)
                return new DSXmlHelper("Response");

            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");

            foreach (string each in payment_detail_id)
                req.AddElement("Condition", "RefPaymentDetailID", each);

            return FeatureBase.CallService("SmartSchool.Payment.History.GetDetailList", new DSRequest(req)).GetContent();
        }

        /// <summary>
        /// 取得繳費記錄資料。
        /// </summary>
        public static DSXmlHelper GetPaymentHistories(string paymentId, string batchRef)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");

            req.AddElement("Condition", "RefPaymentID", paymentId);
            req.AddElement("Condition", "BatchRef", batchRef);

            return FeatureBase.CallService("SmartSchool.Payment.History.GetDetailList", new DSRequest(req)).GetContent();
        }

        /// <summary>
        /// 取得有繳費單資料的繳費記錄(對帳專用)。
        /// </summary>
        /// <returns></returns>
        public static DSXmlHelper GetPaymentForms(params string[] virtualAccountT)
        {
            bool executeRequired = false;
            DSXmlHelper request = new DSXmlHelper("Request");
            request.AddElement("All");
            request.AddElement("Condition");

            foreach (string each in virtualAccountT)
            {
                executeRequired = true;
                request.AddElement("Condition", "VirtualAccountT", each);
            }

            if (executeRequired)
                return FeatureBase.CallService("SmartSchool.Payment.History.GetDetailList", new DSRequest(request)).GetContent();
            else
                return new DSXmlHelper("Response");
        }

        public static DSXmlHelper GetTransactions(params string[] status)
        {
            bool executeRequired = false;
            DSXmlHelper request = new DSXmlHelper("Request");
            request.AddElement("All");
            request.AddElement("Condition");

            foreach (string each in status)
            {
                executeRequired = true;
                request.AddElement("Condition", "Status", each);
            }

            if (executeRequired)
                return FeatureBase.CallService("SmartSchool.Payment.Transaction.GetDetailList", new DSRequest(request)).GetContent();
            else
                return new DSXmlHelper("Response");
        }

        public static DSXmlHelper GetTransactionsByHistory(params string[] historyid)
        {
            bool executeRequired = false;
            DSXmlHelper request = new DSXmlHelper("Request");
            request.AddElement("All");
            request.AddElement("Condition");

            foreach (string each in historyid)
            {
                executeRequired = true;
                request.AddElement("Condition", "RefPaymentHistoryID", each);
            }

            if (executeRequired)
                return FeatureBase.CallService("SmartSchool.Payment.Transaction.GetDetailList", new DSRequest(request)).GetContent();
            else
                return new DSXmlHelper("Response");
        }
    }
}
