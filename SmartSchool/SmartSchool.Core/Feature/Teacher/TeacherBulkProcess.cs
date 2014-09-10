using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Teacher
{
    public class TeacherBulkProcess
    {
        [QueryRequest()]
        public static XmlElement GetExportDescription()
        {
            return CallNoneRequestService("SmartSchool.Teacher.BulkProcess.GetExportDescription");
        }

        [QueryRequest()]
        public static XmlElement GetBulkDescription()
        {
            return CallNoneRequestService("SmartSchool.Teacher.BulkProcess.GetBulkDescription");
        }

        #region 2008/04/02 �Юv�פJ��g�A���եΡA���_

        public static XmlElement GetImportFieldList()
        {
            return CallNoneRequestService("SmartSchool.Teacher.BulkProcess.GetImportFieldList");
        }

        public static XmlElement GetUniqueFieldData()
        {
            return CallNoneRequestService("SmartSchool.Teacher.BulkProcess.GetUniqueFieldData");
        }

        public static XmlElement GetShiftCheckList(params string[] fieldList)
        {
            DSXmlHelper request = new DSXmlHelper("Request");
            foreach (string each in fieldList)
                request.AddElement(".", each);

            string sn = "SmartSchool.Teacher.BulkProcess.GetShiftCheckList";
            return FeatureBase.CallService(sn, new DSRequest(request)).GetContent().BaseElement;
        }

        #endregion

        private static XmlElement CallNoneRequestService(string serviceName)
        {
            string strServiceName = serviceName;
            DSResponse rsp = FeatureBase.CallService(serviceName, null);

            if (rsp.GetContent() == null)
                throw new Exception("�A�ȥ��^�ǥ�������T�C(" + strServiceName + ")");

            return rsp.GetContent().BaseElement;
        }

        public static DSResponse GetExportList(DSRequest request)
        {
            return FeatureBase.CallService("SmartSchool.Teacher.BulkProcess.Export", request);
        }

        [QueryRequest()]
        public static XmlElement GetFieldValidationRule()
        {
            return CallNoneRequestService("SmartSchool.Teacher.BulkProcess.GetFieldValidationRule");
        }

        [QueryRequest()]
        public static XmlElement GetPrimaryKeyList()
        {
            return CallNoneRequestService("SmartSchool.Teacher.BulkProcess.GetPrimaryKeyList");
        }

        [QueryRequest()]
        public static XmlElement GetShiftCheckList(string key, string value)
        {
            DSXmlHelper request = new DSXmlHelper("GetShiftCheckList");
            request.AddElement(key);
            request.AddElement(value);

            return FeatureBase.CallService("SmartSchool.Teacher.BulkProcess.GetShiftCheckList", new DSRequest(request)).GetContent().BaseElement;
        }

        public static void InsertImportTeacher(XmlElement request)
        {
            FeatureBase.CallService("SmartSchool.Teacher.BulkProcess.InsertImportTeacher", new DSRequest(request));
        }

        public static void UpdateImportTeacher(XmlElement request)
        {
            FeatureBase.CallService("SmartSchool.Teacher.BulkProcess.UpdateImportTeacher", new DSRequest(request));
        }
    }
}
