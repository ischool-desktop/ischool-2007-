using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.ClientModule
{
    public class Preference
    {
        public static DSXmlHelper GetModulePreference(string name)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("All");
            req.AddElement("Condition");
            req.AddElement("Condition", "Name", name);

            return FeatureBase.CallService("SmartSchool.ClientModule.GetPreference", new DSRequest(req)).GetContent();
        }

        public static DSXmlHelper GetModulePreferenceList()
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("ID");
            req.AddElement("Name");

            return FeatureBase.CallService("SmartSchool.ClientModule.GetPreference", new DSRequest(req)).GetContent();
        }

        public static void InsertPreference(string name, XmlElement preference)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("ModulePreference");
            req.AddElement("ModulePreference", "Name", name);
            req.AddElement("ModulePreference", "Content", preference.OuterXml, true);

            FeatureBase.CallService("SmartSchool.ClientModule.InsertPreference", new DSRequest(req));
        }

        public static void UpdatePreference(string name, XmlElement preference)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("ModulePreference");
            req.AddElement("ModulePreference", "Content", preference.OuterXml, true);
            req.AddElement("ModulePreference", "Condition");
            req.AddElement("ModulePreference/Condition", "Name", name);

            FeatureBase.CallService("SmartSchool.ClientModule.UpdatePreference", new DSRequest(req));
        }

        public static void DeletePreference(string name)
        {
            DSXmlHelper req = new DSXmlHelper("Request");
            req.AddElement("ModulePreference");
            req.AddElement("ModulePreference", "Name", name);

            FeatureBase.CallService("SmartSchool.ClientModule.DeletePreference", new DSRequest(req));
        }
    }
}
