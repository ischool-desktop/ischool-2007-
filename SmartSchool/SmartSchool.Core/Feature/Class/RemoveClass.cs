using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Class
{
    [QueryRequest()]
    public class RemoveClass
    {
        public static void DeleteClass(string id)
        {
            DSRequest dsreq = new DSRequest("<DeleteRequest><Class><ID>" + id + "</ID></Class></DeleteRequest>");
            DSResponse dsrsp = FeatureBase.CallService("SmartSchool.Class.Delete", dsreq);
        }
    }
}
