using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool.Feature
{
    [QueryRequest()]
    public class Personal : FeatureBase
    {
        public static List<string> GetAllowFeatures()
        {
            List<string> allowcodes = new List<string>();
            DSResponse response = CallService("SmartSchool.Personal.GetAllowFeature", null);
            DSXmlHelper rspcodes = response.GetContent();

            //這樣不會產生 Exception。
            if (rspcodes == null) return new List<string>();

            foreach (XmlElement feature in rspcodes.GetElements("FeatureCode"))
            {
                allowcodes.Add(feature.InnerText);
            }

            return allowcodes;
        }

        public static void ChangePassword(string newPassword)
        {
            DSXmlHelper helper = new DSXmlHelper("ChangePassword");
            helper.AddElement("CurrentUser");
            helper.AddElement("CurrentUser", "NewPassword", newPassword);
            helper.AddElement("CurrentUser", "Condition");
            helper.AddElement("CurrentUser/Condition", "UserName", CurrentUser.Instance.UserName.ToUpper());
            SmartSchool.Feature.Personal.CallService("SmartSchool.Personal.ChangePassword", new DSRequest(helper));
        }
    }
}
