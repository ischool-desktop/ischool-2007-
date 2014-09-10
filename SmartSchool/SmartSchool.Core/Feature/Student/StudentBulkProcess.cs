using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Feature.Student
{
    public class StudentBulkProcess : FeatureBase
    {
        [QueryRequest()]
        public static XmlElement GetBulkDescription()
        {
            return CallNoneRequestService("SmartSchool.Student.BulkProcess.GetBulkDescription");
        }

        [QueryRequest()]
        public static XmlElement GetFieldValidationRule()
        {
            return CallNoneRequestService("SmartSchool.Student.BulkProcess.GetFieldValidationRule");
        }

        [QueryRequest()]
        public static XmlElement GetPrimaryKeyList()
        {
            return CallNoneRequestService("SmartSchool.Student.BulkProcess.GetPrimaryKeyList");
        }

        [QueryRequest()]
        public static XmlElement GetShiftCheckList(string key, string value)
        {
            DSXmlHelper request = new DSXmlHelper("GetShiftCheckList");
            request.AddElement(key);
            request.AddElement(value);

            return CallService("SmartSchool.Student.BulkProcess.GetShiftCheckList", new DSRequest(request)).GetContent().BaseElement;
        }

        public static void InsertImportStudent(XmlElement request)
        {
            CallService("SmartSchool.Student.BulkProcess.InsertImportStudent", new DSRequest(request));
        }

        public static void UpdateImportStudent(XmlElement request)
        {
            CallService("SmartSchool.Student.BulkProcess.UpdateImportStudent", new DSRequest(request));
        }

        public static void SyncEnrollmentInfoToUpdateRecord()
        {
            CallService("SmartSchool.Student.BulkProcess.SyncEnrollmentInfoToUpdateRecord",null);
        }

        private static XmlElement CallNoneRequestService(string serviceName)
        {
            string strServiceName = serviceName;
            DSResponse rsp = CallService(serviceName, null);

            if (rsp.GetContent() == null)
                throw new Exception("服務未回傳任何欄位資訊。(" + strServiceName + ")");

            return rsp.GetContent().BaseElement;
        }


        [QueryRequest()]
        public static XmlElement GetExportDescription()
        {
            return CallNoneRequestService("SmartSchool.Student.BulkProcess.GetExportDescription");
        }
    }
}
