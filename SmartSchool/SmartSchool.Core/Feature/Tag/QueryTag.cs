using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Tag
{
    [QueryRequest()]
    public class QueryTag : FeatureBase
    {
        /// <summary>
        /// 將得所有的 Tag 資訊。
        /// </summary>
        /// <returns></returns>
        public static XmlElement GetDetailList(TagCategory category)
        {
            string strServiceName = "SmartSchool.Tag.GetDetailList";
            DSXmlHelper request = new DSXmlHelper("GetDetailListRequest");
            request.AddElement(".", "Field", "<All/>", true);
            request.AddElement(".", "Condition", "<Category>" + EditTag.GetCategoryString(category) + "</Category>", true);
            request.AddElement(".", "Order", "<Prefix>Desc</Prefix><Name>Desc</Name>", true);

            return CallService(strServiceName, new DSRequest(request)).GetContent().BaseElement;
        }

        /// <summary>
        /// 取得使用此 Tag 的學生。
        /// </summary>
        /// <param name="id"></param>
        public static XmlElement GetUseStudentList(int tagId)
        {
            string strServiceName = "SmartSchool.Tag.GetUseStudentList";

            DSXmlHelper request = new DSXmlHelper("GetUseStudentListRequest");
            request.AddElement(".", "Field", "<StudentID/>", true);
            request.AddElement(".", "Condition", "<TagID>" + tagId + "</TagID>", true);

            return CallService(strServiceName, new DSRequest(request)).GetContent().BaseElement;
        }

        /// <summary>
        /// 取得學生所使用的 Tag。
        /// </summary>
        public static XmlElement GetDetailListByStudent(int studentId)
        {
            List<int> students = new List<int>(new int[] { studentId });
            return GetDetailListByStudent(students);
        }

        /// <summary>
        /// 取得學生所使用的 Tag。
        /// </summary>
        public static XmlElement GetDetailListByStudent(List<int> students)
        {
            string strServiceName = "SmartSchool.Tag.GetDetailListByStudent";

            if (students.Count <= 0)
                return DSXmlHelper.LoadXml("<GetDetailListByStudentResponse/>");

            DSXmlHelper request = new DSXmlHelper("GetDetailListByStudentRequest");
            request.AddElement(".", "Field", "<All/>", true);

            request.AddElement("Condition");
            foreach (int each in students)
                request.AddElement("Condition", "StudentID", each.ToString());

            request.AddElement(".", "Order", "<Order><StudentID/><Name/><Prefix/></Order>", true);

            return CallService(strServiceName, new DSRequest(request)).GetContent().BaseElement;
        }
    }
}
