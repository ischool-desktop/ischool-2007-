using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool.Feature.SubjectTable
{
    public class AddSubejctTable
    {
        public static void Insert(string name, string catalog, XmlElement content)
        {
            DSRequest dsreq = new DSRequest("<Request><SubjectTable><Name>" + name + "</Name><Catalog>" + catalog + "</Catalog><Content>" + content.OuterXml + "</Content></SubjectTable></Request>");
            CurrentUser.Instance.CallService("SmartSchool.SubjectTable.Insert", dsreq);
        }
    }
}
