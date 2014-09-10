using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.ApplicationLog
{
    public class Manage : FeatureBase
    {
        public static void InsertLog(XmlElement logs)
        {

            CallService("SmartSchool.ApplicationLog.Insert", new DSRequest(logs));
        }

        [QueryRequest()]
        public static XmlElement QueryLog(XmlElement request)
        {
            return CallService("SmartSchool.ApplicationLog.Query", new DSRequest(request)).GetContent().BaseElement;
        }
    }
}
