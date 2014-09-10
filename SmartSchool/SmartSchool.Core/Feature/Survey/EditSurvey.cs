using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common.DateTimeProcess;

namespace SmartSchool.Feature.Survey
{
    public class EditSurvey : FeatureBase
    {
        public static void UpdateSurvey(DSXmlHelper request)
        {
            string srvName = "SmartSchool.Survey.Update";

            CallService(srvName, new DSRequest(request));
        }

        public static void InsertSurveyResponse(DSXmlHelper request)
        {
            string srvName = "SmartSchool.Survey.InsertSurveyResponse";

            CallService(srvName, new DSRequest(request));
        }
    }
}
