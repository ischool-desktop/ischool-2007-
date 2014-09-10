using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.Common
{
    public interface IXmlDataObject
    {
        void Initialize(XmlNode baseXml);
    }
}
