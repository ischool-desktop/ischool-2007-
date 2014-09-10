using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Class
{
    public class EditClass
    {

        [QueryRequest()]
        public static void Update(DSRequest dSRequest)
        {
            FeatureBase.CallService("SmartSchool.Class.Update", dSRequest);
        }

        [QueryRequest()]
        public static void Delete(string classid)
        {
            //DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            //helper.AddElement("Class");
            //helper.AddElement("Class","Field");
            //helper.AddElement("Class/Field", "Status", "256");
            //helper.AddElement("Class","Condition");
            //helper.AddElement("Class/Condition", "ID", classid);
            //Update(new DSRequest(helper));
            ReleaseClassStudent(classid);
            DSXmlHelper helper = new DSXmlHelper("DeleteRequest");
            helper.AddElement("Class");
            helper.AddElement("Class", "ID", classid);
            FeatureBase.CallService("SmartSchool.Class.Delete", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void Delete(string[] classid)
        {
            //DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            //helper.AddElement("Class");
            //helper.AddElement("Class", "Field");
            //helper.AddElement("Class/Field", "Status", "256");
            //helper.AddElement("Class", "Condition");
            //foreach(string cid in classid)
            //    helper.AddElement("Class/Condition", "ID", cid);
            //Update(new DSRequest(helper));
            ReleaseClassStudent(classid);
            DSXmlHelper helper = new DSXmlHelper("DeleteRequest");
            helper.AddElement("Class");
            foreach (string cid in classid)
                helper.AddElement("Class", "ID", cid);
            FeatureBase.CallService("SmartSchool.Class.Delete", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void ReleaseClassStudent(string[] classid)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Class");
            foreach (string cid in classid)
                helper.AddElement("Class", "RefClassID", cid);
            FeatureBase.CallService("SmartSchool.Class.ReleaseClassStudent", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void ReleaseClassStudent(string classid)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Class");
            helper.AddElement("Class", "RefClassID", classid);
            FeatureBase.CallService("SmartSchool.Class.ReleaseClassStudent", new DSRequest(helper));
        }
    }
}
