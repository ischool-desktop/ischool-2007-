using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Payment.GT
{
    internal class PaymentConfig
    {
        public PaymentConfig()
        {
        }

        public XmlElement ToXml()
        {
            DSXmlHelper hlpxml = new DSXmlHelper("PaymentConfig");
            hlpxml.SetAttribute(".", "DefaultExpiration", DefaultExpiration);
            hlpxml.SetAttribute(".", "BankConfigID", BankConfigID);

            return hlpxml.BaseElement;
        }

        public static PaymentConfig Parse(XmlElement xmldata)
        {
            DSXmlHelper hlpxml = new DSXmlHelper(xmldata);

            PaymentConfig conf = new PaymentConfig();
            conf.DefaultExpiration = hlpxml.GetText("@DefaultExpiration");
            conf.BankConfigID = hlpxml.GetText("@BankConfigID");

            return conf;
        }

        private string _default_expiration;
        public string DefaultExpiration
        {
            get { return _default_expiration; }
            set { _default_expiration = value; }
        }

        private string _bank_configid;
        public string BankConfigID
        {
            get { return _bank_configid; }
            set { _bank_configid = value; }
        }
    }
}
