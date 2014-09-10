using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using System.Xml;

namespace SmartSchool.Feature.ScoreCalcRule
{
    [QueryRequest()]
    public class QueryScoreCalcRule
    {
        public static DSResponse GetList()
        {
            DSXmlHelper helper = new DSXmlHelper("GetScoreCalcRule");
            DSRequest dsreq = new DSRequest();
            helper.AddElement("Field");
            helper.AddElement("Field", "ID");
            helper.AddElement("Field", "Name");
            helper.AddElement("Field", "Content");
            helper.AddElement("Condition");
            helper.AddElement("Order");
            helper.AddElement("Order", "Name");
            dsreq.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.ScoreCalcRule.GetScoreCalcRule", dsreq);
            return dsrsp;
        }

        public static XmlElement GetMoralConductCalcRule()
        {
            DSRequest dsreq = new DSRequest("<GetMoralConductScoreCalcRuleRequest><Field><All/></Field></GetMoralConductScoreCalcRuleRequest>");
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.ScoreCalcRule.GetMoralConductScoreCalcRule", dsreq);
            return dsrsp.GetContent().GetElement("MoralConductScoreCalcRule");        
        }

        public static DSResponse GetScoreCalcRuleList()
        {
            DSXmlHelper request = new DSXmlHelper("Request");
            request.AddElement(".", "Field", "<ID/><Name/>", true);
            return FeatureBase.CallService("SmartSchool.ScoreCalcRule.GetScoreCalcRule", new DSRequest(request));
        }

        public static Dictionary<string, string> GetClassReference()
        {
            DSXmlHelper helper = new DSXmlHelper("GetClassReferenceRequest");
            DSRequest dsreq = new DSRequest();
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            dsreq.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.ScoreCalcRule.GetClassReference", dsreq);
            //return dsrsp;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach ( XmlElement var in dsrsp.GetContent().GetElements("ClassReference") )
            {
                dic.Add(var.GetAttribute("ClassID"), var.GetAttribute("ScoreCalcRuleID"));
            }
            return dic;
        }

        public static Dictionary<string, string> GetStudentReference()
        {
            DSXmlHelper helper = new DSXmlHelper("GetStudentReferenceRequest");
            DSRequest dsreq = new DSRequest();
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            dsreq.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.ScoreCalcRule.GetStudentReference", dsreq);
            //return dsrsp;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach ( XmlElement var in dsrsp.GetContent().GetElements("StudentReference") )
            {
                dic.Add(var.GetAttribute("StudentID"), var.GetAttribute("ScoreCalcRuleID"));
            }
            return dic;
        }
    }
}
