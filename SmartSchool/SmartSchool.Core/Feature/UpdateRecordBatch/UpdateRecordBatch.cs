using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.UpdateRecordBatch
{
    public class UpdateRecordBatch
    {
        public static void InsertUpdateRecordBatch(string name, string schoolYear, string semester, XmlElement content)
        {
            DSRequest dsreq = new DSRequest("<UpdateRecordBatchRequest>" +
                "<UpdateRecordBatch>" +
                    "<Field>" +
                        "<Name>" + name + "</Name>" +
                        "<SchoolYear>" + schoolYear + "</SchoolYear>" +
                        "<Semester>" + semester + "</Semester>" +
                        "<Content>" + content.OuterXml + "</Content>" +
                    "</Field>" +
                "</UpdateRecordBatch>" +
            "</UpdateRecordBatchRequest>");
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Student.UpdateRecord.InsertBatch", dsreq);
        }

        [QueryRequest()]
        public static void DeleteBatch(string id)
        {
            DSXmlHelper helper = new DSXmlHelper("DeleteUpdateRecordBatchRequest");
            helper.AddElement("UpdateRecord");
            helper.AddElement("UpdateRecord", "ID", id);
            DSRequest dsreq = new DSRequest(helper);
            CurrentUser.Instance.CallService("SmartSchool.Student.UpdateRecord.DeleteBatch", dsreq);
        }
    }
}
