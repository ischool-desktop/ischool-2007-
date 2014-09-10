using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Payment.Interfaces
{
    public class BankConfig
    {
        public BankConfig()
        {
            _base_xml = DSXmlHelper.LoadXml("<BankConfig/>");
        }

        internal BankConfig(XmlElement conf)
        {
            _base_xml = conf;
        }

        public string Name
        {
            get { return _base_xml.GetAttribute("Name"); }
            internal set { _base_xml.SetAttribute("Name", value); }
        }

        public string ConfigID
        {
            get { return _base_xml.GetAttribute("ConfigID"); }
            internal set { _base_xml.SetAttribute("ConfigID", value); }
        }

        public string ModuleCode
        {
            get { return _base_xml.GetAttribute("ModuleCode"); }
            internal set { _base_xml.SetAttribute("ModuleCode", value); }
        }

        /// <summary>
        /// 組態內容。
        /// </summary>
        public XmlElement Content
        {
            get
            {
                return _base_xml.SelectSingleNode("Content") as XmlElement;
            }
            set
            {
                if (value.LocalName != "Content")
                    throw new PaymentModuleException("Xml 根名稱必需是「Content」。", null);

                XmlNode oldNode = _base_xml.SelectSingleNode("Content");
                XmlNode newNode = _base_xml.OwnerDocument.ImportNode(value, true);

                if (oldNode == null)
                    _base_xml.AppendChild(newNode);
                else
                    _base_xml.ReplaceChild(newNode, oldNode);
            }
        }

        private XmlElement _base_xml;
        public XmlElement BaseXml
        {
            get { return _base_xml; }
        }
    }
}
