using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Exam
{
    public class EditExam
    {
        public static void Insert(DSXmlHelper helper)
        {
            FeatureBase.CallService("SmartSchool.Exam.Insert", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void Update(DSXmlHelper helper)
        {

            FeatureBase.CallService("SmartSchool.Exam.Update", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void Delete(List<string> idList)
        {
            DSXmlHelper helper = new DSXmlHelper("DeleteRequest");
            foreach (string id in idList)
            {
                helper.AddElement("Exam");
                helper.AddElement("Exam", "ID", id);                
            }
            FeatureBase.CallService("SmartSchool.Exam.Delete", new DSRequest(helper));
        }
    }
}
