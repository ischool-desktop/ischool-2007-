using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.Feature.Basic
{
    public class Personal
    {
        [LeaveOnError()]
        public static void SetPreference(XmlElement element)
        {
            DSXmlHelper request = new DSXmlHelper();
            request.Load("<SetPreferenceRequest>" + element.InnerXml + "</SetPreferenceRequest>");
            request.SetAttribute(".", "UserName", CurrentUser.Instance.UserName);

            CurrentUser.Instance.CallService("SmartSchool.Personal.SetPreference", new DSRequest(request));
        }

        [QueryRequest()]
        public static DSResponse GetPreference()
        {
            DSXmlHelper request = new DSXmlHelper("Content");
            request.AddElement(".", "UserName", CurrentUser.Instance.UserName);

            return CurrentUser.Instance.CallService("SmartSchool.Personal.GetPreference", new DSRequest(request));
        }
    }
}
