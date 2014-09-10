using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using Aspose.Words;
using System.IO;

namespace SmartSchool.Payment.GT
{
    internal class BillBatchInformation
    {
        private string _identity = Guid.NewGuid().ToString();
        public string Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _expiration;
        public string Expiration
        {
            get { return _expiration; }
            set { _expiration = value; }
        }

        private string _tempalte_base64;
        public string TemplateBase64
        {
            get { return _tempalte_base64; }
            set { _tempalte_base64 = value; }
        }

        public Document TemplateDocument
        {
            get
            {
                if (string.IsNullOrEmpty(TemplateBase64))
                    return null;

                byte[] rawData = Convert.FromBase64String(TemplateBase64);
                Stream stream = new MemoryStream(rawData);
                Document doc = new Document(stream);

                return doc;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    internal class BillBatchInformationCollection : List<BillBatchInformation>
    {
        public BillBatchInformationCollection()
        {
        }

        public static XmlElement SerialToXml(BillBatchInformationCollection batchs)
        {
            DSXmlHelper hlpresult = new DSXmlHelper("Content");

            foreach (BillBatchInformation each in batchs)
            {
                XmlElement batch = hlpresult.AddElement("BillBatch");
                DSXmlHelper hlpbatch = new DSXmlHelper(batch);

                batch.SetAttribute("Identity", each.Identity);
                batch.SetAttribute("Name", each.Name);
                batch.SetAttribute("Expiration", each.Expiration);
                hlpbatch.AddElement(".", "BillTemplate", each.TemplateBase64);
            }

            return hlpresult.BaseElement;
        }

        public static BillBatchInformationCollection Parse(XmlElement xmldata)
        {
            BillBatchInformationCollection batchs = new BillBatchInformationCollection();

            if (xmldata == null) return batchs; //一個資料都沒有的時後。

            foreach (XmlElement each in xmldata.SelectNodes("BillBatch"))
            {
                BillBatchInformation batch = new BillBatchInformation();
                DSXmlHelper hlpxml = new DSXmlHelper(each);

                batch.Identity = hlpxml.GetText("@Identity");
                batch.Name = hlpxml.GetText("@Name");
                batch.Expiration = hlpxml.GetText("@Expiration");
                batch.TemplateBase64 = hlpxml.GetText("BillTemplate");

                batchs.Add(batch);
            }

            return batchs;
        }
    }
}
