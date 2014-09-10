using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Basic;
using SmartSchool.Feature.ClientModule;

namespace SmartSchool.Payment
{
    public class PreferenceManager
    {
        private static bool ContainPreference(string moduleName)
        {
            DSXmlHelper pres = Preference.GetModulePreferenceList();

            foreach (XmlElement each in pres.GetElements("ModulePreference"))
            {
                string name = each.SelectSingleNode("Name").InnerText;

                if (name.Trim() == moduleName.Trim())
                    return true;
            }

            return false;
        }

        public static ModulePreference GetPreference(string moduleName)
        {
            ModulePreference pref = null;
            if (ContainPreference(moduleName))
            {
                XmlElement mps = Preference.GetModulePreference(moduleName).BaseElement;
                pref = new ModulePreference(moduleName, mps.SelectSingleNode("ModulePreference/Content/PreferenceRoot") as XmlElement);
            }
            else
                pref = new ModulePreference(moduleName);

            return pref;
        }

        public static void SavePreference(ModulePreference preference)
        {
            string moduleName = preference.Name;

            if (ContainPreference(moduleName))
                Preference.UpdatePreference(moduleName.Trim(), preference.BaseXml);
            else
                Preference.InsertPreference(moduleName.Trim(), preference.BaseXml);
        }

        public static void DelPreference(string moduleName)
        {
            Preference.DeletePreference(moduleName.Trim());
        }
    }

    public class ModulePreference
    {
        private DSXmlHelper _preference;

        internal ModulePreference(string name)
        {
            _preference = new DSXmlHelper("PreferenceRoot");
            _name = name;
        }

        internal ModulePreference(string name, XmlElement preferenceData)
        {
            _preference = new DSXmlHelper(preferenceData.CloneNode(true) as XmlElement);
            _name = name;
        }

        public XmlElement BaseXml
        {
            get { return _preference.BaseElement; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 取得 Xml 組態，如果不存在則回傳 Null。
        /// </summary>
        public XmlElement GetXml(string key)
        {
            if (_preference.GetElement(key) == null)
                return null;

            foreach (XmlNode each in _preference.GetElement(key).ChildNodes)
            {
                if (each is XmlElement)
                    return each as XmlElement;
            }

            return null;
        }

        public string GetString(string key, string defaultValue)
        {
            XmlElement result = _preference.GetElement(key);

            if (result == null)
                return defaultValue;
            else
                return result.InnerText;
        }

        public int GetInteger(string key, int defaultValue)
        {
            string result = GetString(key, "");

            if (string.IsNullOrEmpty(result))
                return defaultValue;
            else
                return int.Parse(result);
        }

        public bool GetBoolean(string key, bool defaultValue)
        {
            string result = GetString(key, "");

            if (string.IsNullOrEmpty(result))
                return defaultValue;
            else
                return bool.Parse(result);
        }

        public void SetXml(string key, XmlElement value)
        {
            XmlElement result = _preference.GetElement(key);

            if (result != null)
                result.ParentNode.RemoveChild(result);

            _preference.AddElement(key);
            _preference.AddElement(key, value);
        }

        public void SetString(string key, string value)
        {
            XmlElement result = _preference.GetElement(key);

            if (result == null)
                _preference.AddElement(".", key, value);
            else
                result.InnerText = value;
        }

        public void SetInteger(string key, int value)
        {
            SetString(key, value.ToString());
        }

        public void SetBoolean(string key, bool value)
        {
            SetString(key, value.ToString());
        }
    }
}
