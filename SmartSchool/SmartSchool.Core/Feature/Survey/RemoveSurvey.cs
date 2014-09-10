using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Survey
{
    public class RemoveSurvey : FeatureBase
    {
        public static void DeleteSurvey(params string[] surveyId)
        {
            bool doRequired = false;
            string srvName = "SmartSchool.Survey.Delete";

            DSXmlHelper request = new DSXmlHelper("Request");
            request.AddElement("Survey");

            foreach (string each in surveyId)
            {
                request.AddElement("Survey", "ID", each);
                doRequired = true;
            }

            if (doRequired)
                CallService(srvName, new DSRequest(request));
        }
    }
}
