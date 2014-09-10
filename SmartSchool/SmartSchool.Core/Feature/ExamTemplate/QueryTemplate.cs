using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.ExamTemplate
{
    [QueryRequest()]
    public class QueryTemplate
    {
        public static XmlElement GetAbstractList()
        {
            DSXmlHelper req = new DSXmlHelper("GetAbstractListRequest");
            req.AddElement("Field");
            req.AddElement("Field", "All");

            DSResponse rsp = FeatureBase.CallService("SmartSchool.ExamTemplate.GetAbstractList", new DSRequest(req));

            return rsp.GetContent().BaseElement;
        }

        public static XmlElement GetTempalteInfo(string id)
        {
            DSXmlHelper req = new DSXmlHelper("GetAbstractListRequest");
            req.AddElement("Field");
            req.AddElement("Field", "All");
            req.AddElement("Condition");
            req.AddElement("Condition", "ID", id);

            DSResponse rsp = FeatureBase.CallService("SmartSchool.ExamTemplate.GetAbstractList", new DSRequest(req));

            return rsp.GetContent().BaseElement;
        }

        public static XmlElement GetIncludeExamList(string templateId)
        {
            DSXmlHelper req = new DSXmlHelper("GetTemplateExamListRequest");
            req.AddElement("Field");
            req.AddElement("Field", "All");
            req.AddElement("Condition");
            req.AddElement("Condition", "ExamTemplateID", templateId);

            DSResponse rsp = FeatureBase.CallService("SmartSchool.ExamTemplate.GetIncludeExamList", new DSRequest(req));

            return rsp.GetContent().BaseElement;
        }

        public static XmlElement GetIncludeExamList()
        {
            DSXmlHelper req = new DSXmlHelper("GetTemplateExamListRequest");
            req.AddElement("Field");
            req.AddElement("Field", "All");

            DSResponse rsp = FeatureBase.CallService("SmartSchool.ExamTemplate.GetIncludeExamList", new DSRequest(req));

            return rsp.GetContent().BaseElement;
        }
    }
}
