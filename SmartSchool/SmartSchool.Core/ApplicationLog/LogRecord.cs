using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.ApplicationLog
{
    public class LogRecord
    {
        private DSXmlHelper _record;

        public LogRecord()
        {
            _record = new DSXmlHelper("Log");
            _record.AddElement(".", "UserName");
            _record.AddElement(".", "Timestamp");
            _record.AddElement(".", "Entity");
            _record.AddElement(".", "EntityID");
            _record.AddElement(".", "Action");
            _record.AddElement(".", "Source");
            _record.AddElement(".", "Description");
            _record.AddElement(".", "Diagnostics");
        }

        public string UserName
        {
            get { return GetText("UserName"); }
            set { SetText("UserName", value); }
        }

        public string Timestamp
        {
            get { return GetText("Timestamp"); }
            set { SetText("Timestamp", value); }
        }

        public string Entity
        {
            get { return GetText("Entity"); }
            set { SetText("Entity", value); }
        }

        public string EntityID
        {
            get { return GetText("EntityID"); }
            set { SetText("EntityID", value); }
        }

        public string Action
        {
            get { return GetText("Action"); }
            set { SetText("Action", value); }
        }

        public string Source
        {
            get { return GetText("Source"); }
            set { SetText("Source", value); }
        }

        public string Description
        {
            get { return GetText("Description"); }
            set { SetText("Description", value); }
        }

        /// <summary>
        /// 內部使用 CDATASection 儲存資料。
        /// </summary>
        public string Diagnostics
        {
            get { return GetText("Diagnostics"); }
            set { SetCDataSection("Diagnostics", value); }
        }

        public XmlElement BaseElement
        {
            get { return _record.BaseElement; }
        }

        private string GetText(string name)
        {
            return _record.GetText(name);
        }

        private void SetText(string name, string value)
        {
            _record.GetElement(name).InnerText = value;
        }

        private void SetCDataSection(string name, string value)
        {
            _record.GetElement(name).InnerXml = "<![CDATA[" + value + "]]>";
        }
    }
}
