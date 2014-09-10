using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature;
using SmartSchool.Common;

namespace SmartSchool.Feedback.Feature
{
    /// <summary>
    /// DSA
    /// </summary>
    internal class DSA
    {
        private const string PRODUCTION_ACCESSPOINT = "quality";
        private const string DEVELOPMENT_ACCESSPOINT = "quality@dev";

        private static DSA _instance;
        public static DSA Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DSA();
                return _instance;
            }
        }

        private DSConnection _conn;
        public DSResponse CallService(string service, DSRequest request)
        {
            return _conn.SendRequest(service, request);
        }

        public bool IsDev
        {
            get { return (_conn.AccessPoint.Name == DEVELOPMENT_ACCESSPOINT); }
        }

        private DSA()
        {
            try
            {
                //_conn = new DSConnection(PRODUCTION_ACCESSPOINT, "anonymous", "");
                _conn = new DSConnection(DEVELOPMENT_ACCESSPOINT, "anonymous", "");
                _conn.Connect();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show("使用者回饋機制發生連線錯誤。");
            }
        }
    }

    internal static class Service
    {
        public static DSXmlHelper GetFeedback(DSXmlHelper helper)
        {
            //DSXmlHelper req = new DSXmlHelper("Request");
            //req.AddElement("All");

            return DSA.Instance.CallService("Feedback.GetFeedback", new DSRequest(helper)).GetContent();
        }

        public static void InsertFeedback(DSRequest req)
        {
            DSA.Instance.CallService("Feedback.InsertFeedback", req);
        }

        public static void UpdateFeedback(DSRequest req)
        {
            DSA.Instance.CallService("Feedback.UpdateFeedback", req);
        }

        public static DSXmlHelper GetFunction()
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");

            return DSA.Instance.CallService("Feedback.GetFunction", new DSRequest(req)).GetContent();
        }

        public static string InsertFunction(DSRequest req)
        {
            DSResponse rsp = DSA.Instance.CallService("Feedback.InsertFunction", req);
            return rsp.GetContent().GetText("NewID");
        }

        public static void UpdateFunction(DSRequest req)
        {
            DSA.Instance.CallService("Feedback.UpdateFunction", req);
        }

        public static void DeleteFunction(string function_id)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("Function");
            req.AddElement("Function", "ID", function_id);

            DSA.Instance.CallService("Feedback.DeleteFunction", new DSRequest(req));
        }

        public static DSXmlHelper GetVote(string function_id)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "FunctionID", function_id);

            return DSA.Instance.CallService("Feedback.GetVote", new DSRequest(req)).GetContent();
        }

        public static void InsertVote(string user, string function_id)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("Vote");
            req.AddElement("Vote", "User", user);
            req.AddElement("Vote", "FunctionID", function_id);

            DSA.Instance.CallService("Feedback.InsertVote", new DSRequest(req));
        }

        public static void DeleteVote(string function_id)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("Vote");
            req.AddElement("Vote", "FunctionID", function_id);

            DSA.Instance.CallService("Feedback.DeleteVote", new DSRequest(req));
        }

        public static DSXmlHelper GetNews()
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "Within", "3 months");

            return DSA.Instance.CallService("Feedback.GetNews", new DSRequest(req)).GetContent();
        }

        public static DSXmlHelper GetNewsForUsers()
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "Within", "3 months");

            return DSA.Instance.CallService("Feedback.GetNewsForUsers", new DSRequest(req)).GetContent();
        }

        public static void InsertNews(DSRequest req)
        {
            DSA.Instance.CallService("Feedback.InsertNews", req);
        }

        public static void UpdateNews(DSRequest req)
        {
            DSA.Instance.CallService("Feedback.UpdateNews", req);
        }

        public static void DeleteNews(string id)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("News");
            req.AddElement("News", "ID", id);

            DSA.Instance.CallService("Feedback.DeleteNews", new DSRequest(req));
        }

        public static DSXmlHelper GetDateTimeNow()
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("DateTime");
            return DSA.Instance.CallService("Feedback.GetDateTimeNow", new DSRequest(req)).GetContent();
        }
    }
}
