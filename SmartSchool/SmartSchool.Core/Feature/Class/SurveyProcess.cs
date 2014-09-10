using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Class
{
    public class SurveyProcess : FeatureBase
    {
        public static void AddCSResult(string surveyId, params string[] classId)
        {
            DSXmlHelper request = new DSXmlHelper("Request");

            foreach (string eachClass in classId)
            {
                request.AddElement("CSResult");
                request.AddElement("CSResult", "RefSurveyID", surveyId);
                request.AddElement("CSResult", "RefClassID", eachClass);
            }

            CallService("SmartSchool.Class.Survey.Insert", new DSRequest(request));
        }

        public static void UpdateCSResult(DSXmlHelper request)
        {
            CallService("SmartSchool.Class.Survey.Update", new DSRequest(request));
        }

        public static DSXmlHelper GetCSResultDetailList()
        {
            DSXmlHelper request = new DSXmlHelper("Request");

            request.AddElement("Field");
            request.AddElement("Field", "ID");
            request.AddElement("Field", "RefClassID");
            request.AddElement("Field", "RefSurveyID");

            DSResponse rsp = CallService("SmartSchool.Class.Survey.GetDetailList", new DSRequest(request));

            return rsp.GetContent();
        }

        public static DSXmlHelper GetFormDetailList(string formType)
        {
            DSXmlHelper request = new DSXmlHelper("Request");

            request.AddElement("Field");

            request.AddElement("Field", "ID");
            request.AddElement("Field", "ID");
            request.AddElement("Field", "FormType");
            request.AddElement("Field", "From");
            request.AddElement("Field", "To");
            request.AddElement("Field", "Title");
            request.AddElement("Field", "PostDate");
            request.AddElement("Field", "RefClassID");
            request.AddElement("Field", "Content");
            request.AddElement("Condition");
            request.AddElement("Condition", "FormType", formType);
            request.AddElement("Condition", "To", "School");
            request.AddElement("Condition", "Abandon", "0"); //還有效的。

            DSResponse rsp = CallService("SmartSchool.Class.Form.GetDetailList", new DSRequest(request));

            return rsp.GetContent();
        }

        public static void DeactivateForm(params string[] formId)
        {
            bool execute = false;

            DSXmlHelper request = new DSXmlHelper("Request");

            foreach (string each in formId)
            {
                request.AddElement("Form");
                request.AddElement("Form", "Abandon", "1");
                request.AddElement("Form", "Condition");
                request.AddElement("Form/Condition", "ID", each);
                execute = true;
            }

            if (execute)
                CallService("SmartSchool.Class.Form.Update", new DSRequest(request));
        }
    }
}
