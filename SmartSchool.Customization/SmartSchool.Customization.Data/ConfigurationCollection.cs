using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.Customization.Data
{
    public interface ConfigurationCollection
    {
        XmlElement this[string Key] { get;set;}
    }
}
