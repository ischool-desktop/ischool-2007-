using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Exam
{
    [QueryRequest()]
    public class QueryExam
    {
        public static XmlElement GetAbstractList()
        {
            DSXmlHelper req = new DSXmlHelper("GetAbstractListRequest");
            req.AddElement("Field");
            req.AddElement("Field", "All");
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Exam.GetAbstractList", new DSRequest(req));

            return rsp.GetContent().BaseElement;
        }

        public static DSResponse GetCourseBelong(string examid, int schoolYear, int semester)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("Field");
            req.AddElement("Field", "All");
            req.AddElement("Condition");
            req.AddElement("Condition", "RefExamID", examid);
            req.AddElement("Condition", "SchoolYear", schoolYear.ToString());
            req.AddElement("Condition", "Semester", semester.ToString());
            return FeatureBase.CallService("SmartSchool.Exam.GetCourseBelong", new DSRequest(req));
        }

        public static int GetExamTemplateUseCount(string examid)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement(".", "ID", examid);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Exam.GetExamTemplateUseCount", new DSRequest(req));

            return int.Parse(rsp.GetContent().GetElement("Count").InnerText);
        }

        public static int GetTextScoreUseCount(string examid)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement(".", "ID", examid);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Exam.GetTextScoreUseCount", new DSRequest(req));

            return int.Parse(rsp.GetContent().GetElement("Count").InnerText);
        }

        public static int GetNumberScoreUseCount(string examid)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement(".", "ID", examid);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Exam.GetNumberScoreUseCount", new DSRequest(req));

            return int.Parse(rsp.GetContent().GetElement("Count").InnerText);
        }

    }
}