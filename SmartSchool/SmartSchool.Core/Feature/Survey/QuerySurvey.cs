using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using SmartSchool.Survey;

namespace SmartSchool.Feature.Survey
{
    [QueryRequest()]
    public class QuerySurvey : FeatureBase
    {
        public static DSXmlHelper GetDetailList(SurveyeeType[] surveyeeType, string schoolYear, string semester, string fields)
        {
            DSXmlHelper request = new DSXmlHelper("Request");

            foreach (string each in fields.Split(','))
                request.AddElement(each);

            request.AddElement("Condition");

            foreach (SurveyeeType each in surveyeeType)
                request.AddElement("Condition", "SurveyeeType", each.ToString());

            request.AddElement("Condition", "SchoolYear", schoolYear);
            request.AddElement("Condition", "Semester", semester);

            DSResponse response = CallService("SmartSchool.Survey.GetDetailList", new DSRequest(request));

            return response.GetContent();
        }

        public static DSXmlHelper GetDetailList(params string[] surveyId)
        {
            bool callRequired = false;

            DSXmlHelper request = new DSXmlHelper("Request");
            request.AddElement("All");
            request.AddElement(".", "Condition");

            foreach (string each in surveyId)
            {
                request.AddElement("Condition", "ID", each);
                callRequired = true;
            }

            if (callRequired)
            {
                DSResponse response = CallService("SmartSchool.Survey.GetDetailList", new DSRequest(request));
                return response.GetContent();
            }
            else
                return new DSXmlHelper("Response");
        }

        //public static DSXmlHelper GetDetailList()
        //{
        //    DSXmlHelper request = new DSXmlHelper("Request");
        //    request.AddElement(".", "Field", "<All/>", true);

        //    DSResponse response = CallService("SmartSchool.Survey.GetDetailList", new DSRequest(request));

        //    return response.GetContent();
        //}
    }
}
