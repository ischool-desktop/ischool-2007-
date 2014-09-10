using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using System.Xml;

namespace SmartSchool.Feature.GraduationPlan
{
    [QueryRequest()]
    public class QueryGraduationPlan
    {
        public static DSResponse GetGraduationPlan(params string[] idList)
        {
            DSXmlHelper helper = new DSXmlHelper("GetDetailList");
            DSRequest dsreq = new DSRequest();
            helper.AddElement("Field");
            helper.AddElement("Field", "ID");
            helper.AddElement("Field", "Name");
            helper.AddElement("Field", "Content");
            helper.AddElement("Condition");
            if (idList.Length > 0)
            {
                helper.AddElement("Condition", "IDList");
                foreach (string var in idList)
                {
                    helper.AddElement("Condition/IDList", "ID",var);
                }
            }
            helper.AddElement("Order");
            helper.AddElement("Order", "Name");
            dsreq.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.GraduationPlan.GetDetailList", dsreq);
            return dsrsp;
        }

        public static DSResponse GetGraduationPlanList()
        {
            DSXmlHelper request=new DSXmlHelper("Request");
            request.AddElement(".","Field","<ID/><Name/>",true);
            return FeatureBase.CallService("SmartSchool.GraduationPlan.GetDetailList", new DSRequest(request));
        }

        public static DSResponse GetCommon()
        {
            DSXmlHelper helper = new DSXmlHelper("GetCommonRequest");
            DSRequest dsreq = new DSRequest();
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            dsreq.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.GraduationPlan.GetCommon", dsreq);
            return dsrsp;
        }

        public static Dictionary<string, string> GetClassReference()
        {
            DSXmlHelper helper = new DSXmlHelper("GetClassReferenceRequest");
            DSRequest dsreq = new DSRequest();
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            dsreq.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.GraduationPlan.GetClassReference", dsreq);
            //return dsrsp;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach ( XmlElement var in dsrsp.GetContent().GetElements("ClassReference") )
            {
                dic.Add(var.GetAttribute("ClassID"), var.GetAttribute("GraduationPlanID"));
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
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.GraduationPlan.GetStudentReference", dsreq);
            //return dsrsp;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach ( XmlElement var in dsrsp.GetContent().GetElements("StudentReference") )
            {
                dic.Add(var.GetAttribute("StudentID"), var.GetAttribute("GraduationPlanID"));
            }
            return dic;
        }
    }
}