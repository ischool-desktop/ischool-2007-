using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Class
{
    public class ClassBulkProcess
    {
        public static XmlElement GetExportDescription()
        {
            return CallNoneRequestService("SmartSchool.Class.BulkProcess.GetExportDescription");
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
            return FeatureBase.CallService("SmartSchool.Class.BulkProcess.Export", request);
        }

        public static XmlElement GetImportFieldList()
        {
            return CallNoneRequestService("SmartSchool.Class.BulkProcess.GetImportFieldList");
        }

        public static XmlElement GetValidateFieldRule()
        {
            return CallNoneRequestService("SmartSchool.Class.BulkProcess.GetValidateFieldRule");
        }

        public static XmlElement GetUniqueFieldData()
        {
            return CallNoneRequestService("SmartSchool.Class.BulkProcess.GetUniqueFieldData");
        }

        public static XmlElement GetShiftCheckList(params string[] fieldList)
        {
            DSXmlHelper request = new DSXmlHelper("Request");
            foreach (string each in fieldList)
                request.AddElement(".", each);

            string sn = "SmartSchool.Class.BulkProcess.GetShiftCheckList";
            return FeatureBase.CallService(sn, new DSRequest(request)).GetContent().BaseElement;
        }

        public static void InsertImportData(XmlElement data)
        {
            FeatureBase.CallService("SmartSchool.Class.BulkProcess.InsertImportClass", new DSRequest(data));
        }

        public static void UpdateImportData(XmlElement data)
        {
            FeatureBase.CallService("SmartSchool.Class.BulkProcess.UpdateImportClass", new DSRequest(data));
        }
    }
}
