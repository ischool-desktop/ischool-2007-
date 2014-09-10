using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.Customization.Data
{
    public interface PreferenceCollection
    {
        XmlElement this[string Key] { get;set;}
    }
}
