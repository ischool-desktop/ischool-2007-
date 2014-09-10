using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Survey
{
    public class AddSurvey : FeatureBase
    {
        public static string InsertSurvey(DSXmlHelper request)
        {
            string srvName = "SmartSchool.Survey.Insert";

            DSResponse rsp = CallService(srvName, new DSRequest(request));

            return rsp.GetContent().GetText("NewID");
        }
    }
}
