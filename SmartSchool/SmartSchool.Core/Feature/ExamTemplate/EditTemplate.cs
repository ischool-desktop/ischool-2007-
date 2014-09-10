using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Feature.ExamTemplate
{
    public class EditTemplate : FeatureBase
    {
        public static string Insert(string name)
        {
            DSXmlHelper req = new DSXmlHelper("InsertRequest");
            req.AddElement("ExamTemplate");
            req.AddElement("ExamTemplate", "TemplateName", name);
            req.AddElement("ExamTemplate", "AllowUpload", "是");

            DSResponse rsp = CallService("SmartSchool.ExamTemplate.Insert", new DSRequest(req));

            return rsp.GetContent().GetText("NewID");
        }

        [QueryRequest()]
        public static void Delete(string identity)
        {
            DSXmlHelper req = new DSXmlHelper("DeleteRequest");
            req.AddElement("ExamTemplate");
            req.AddElement("ExamTemplate", "ID", identity);

            CallService("SmartSchool.ExamTemplate.Delete", new DSRequest(req));
        }

        [QueryRequest()]
        public static void Rename(string identity, string newName)
        {
            DSXmlHelper req = new DSXmlHelper("UpdateRequest");
            req.AddElement("ExamTemplate");
            req.AddElement("ExamTemplate", "TemplateName", newName);
            req.AddElement("ExamTemplate", "Condition", "<ID>" + identity + "</ID>", true);

            CallService("SmartSchool.ExamTemplate.Update", new DSRequest(req));
        }

        [QueryRequest()]
        public static void DeleteIncludeExam(XmlElement request)
        {
            CallService("SmartSchool.ExamTemplate.DeleteIncludeExam", new DSRequest(request));
        }

        public static void InsertIncludeExam(XmlElement request)
        {
            CallService("SmartSchool.ExamTemplate.InsertIncludeExam", new DSRequest(request));
        }

        [QueryRequest()]
        public static void UpdateIncludeExam(XmlElement request)
        {
            CallService("SmartSchool.ExamTemplate.UpdateIncludeExam", new DSRequest(request));
        }

        [QueryRequest()]
        public static void UpdateTemplate(string identity, string name, string allowUpload, string startTime, string endTime)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("ExamTemplate");
            req.AddElement("ExamTemplate", "TemplateName", name);
            req.AddElement("ExamTemplate", "AllowUpload", allowUpload);
            req.AddElement("ExamTemplate", "StartTime", startTime);
            req.AddElement("ExamTemplate", "EndTime", endTime);
            req.AddElement("ExamTemplate", "Condition");
            req.AddElement("ExamTemplate/Condition", "ID", identity);

            CallService("SmartSchool.ExamTemplate.Update", new DSRequest(req));
        }
    }
}
