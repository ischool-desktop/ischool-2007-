using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Feature.Course
{
    public class CourseBulkProcess
    {
        public static System.Xml.XmlElement GetExportDescription()
        {
            return CallNoneRequestService("SmartSchool.Course.BulkProcess.GetExportDescription");
        }

        private static XmlElement CallNoneRequestService(string serviceName)
        {
            string strServiceName = serviceName;
            DSResponse rsp = FeatureBase.CallService(serviceName, null);

            if (rsp.GetContent() == null)
                throw new Exception("服務未回傳任何欄位資訊。(" + strServiceName + ")");

            return rsp.GetContent().BaseElement;
        }

        public static DSResponse GetExportList(DSRequest request)
        {
            return FeatureBase.CallService("SmartSchool.Course.BulkProcess.Export", request);
        }

        public static XmlElement GetImportDescription()
        {
            return CallNoneRequestService("SmartSchool.Course.BulkProcess.GetImportFieldList");
        }

        public static XmlElement GetFieldValidationRule()
        {
            return CallNoneRequestService("SmartSchool.Course.BulkProcess.GetValidateFieldRule");
        }

        public static XmlElement GetPrimaryKeyList()
        {
            return CallNoneRequestService("SmartSchool.Course.BulkProcess.GetUniqueFieldData");
        }

        public static XmlElement GetShiftCheckList(params string[] fieldList)
        {
            DSXmlHelper request = new DSXmlHelper("Request");
            foreach (string each in fieldList)
                request.AddElement(".", each);

            string sn = "SmartSchool.Course.BulkProcess.GetShiftCheckList";
            return FeatureBase.CallService(sn, new DSRequest(request)).GetContent().BaseElement;
        }

        public static XmlElement GetCourseTeachers(IEnumerable<string> fieldList)
        {
            DSXmlHelper request = new DSXmlHelper("Request");
            foreach (string each in fieldList)
                request.AddElement(".", each);

            string sn = "SmartSchool.Course.BulkProcess.GetCourseTeachers";
            return FeatureBase.CallService(sn, new DSRequest(request)).GetContent().BaseElement;
        }

        public static void InsertImportCourse(XmlElement data)
        {
            FeatureBase.CallService("SmartSchool.Course.BulkProcess.InsertImportCourse", new DSRequest(data));
        }

        public static void UpdateImportCourse(XmlElement data)
        {
            FeatureBase.CallService("SmartSchool.Course.BulkProcess.UpdateImportCourse", new DSRequest(data));
        }
    }
}
