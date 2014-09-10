using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Feature.Student
{
    public class Student
    {
        public class Batch : FeatureBase
        {
            [QueryRequest()]
            public static XmlElement GetAllowFields()
            {
                string strServiceName = "SmartSchool.Student.Batch.GetAllowFields";
                return CallNoneRequestService(strServiceName);
            }

            [QueryRequest()]
            public static XmlElement GetValidateRule()
            {
                string strServiceName = "SmartSchool.Student.Batch.GetValidateRule";
                return CallNoneRequestService(strServiceName);
            }

            [QueryRequest()]
            public static XmlElement GetXmlGenerateDescription()
            {
                string strServiceName = "SmartSchool.Student.Batch.GetXmlGenerateDescription";
                return CallNoneRequestService(strServiceName);
            }

            private static XmlElement CallNoneRequestService(string serviceName)
            {
                string strServiceName = serviceName;
                DSResponse rsp = CallService(serviceName, null);

                if (rsp.GetContent() == null)
                    throw new Exception("服務未回傳任何欄位資訊。(" + strServiceName + ")");

                return rsp.GetContent().BaseElement;
            }
        }
    }
}
