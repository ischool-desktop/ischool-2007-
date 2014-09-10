using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Class
{
    public class AddClass
    {
        public static string Insert(DSRequest dsreq)
        {
            DSResponse dsrsp = FeatureBase.CallService("SmartSchool.Class.Insert", dsreq);
            if (dsrsp.HasContent)
            {
                DSXmlHelper helper = dsrsp.GetContent();
                string newid = helper.GetText("NewID");
                return newid;
            }
            return "";
        }
    }
}
