using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Common;
//using SmartSchool.ClassRelated;

namespace SmartSchool.Feature.Class
{
    [QueryRequest()]
    public class QueryClass
    {
        /// <summary>
        /// 取得現有年級
        /// </summary>
        /// <returns></returns>
        public static DSResponse GetGradeYearList()
        {
            DSXmlHelper helper = new DSXmlHelper("GetGradeYearRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "ClassCount");
            helper.AddElement("Field", "GradeYear");
            //helper.AddElement("Field", "Status");
            DSRequest dsreq = new DSRequest(helper);
            return FeatureBase.CallService("SmartSchool.Class.GetGradeYearList", dsreq);
        }

        //internal static Dictionary<string, ClassInfo> SearchClass(DSRequest request)
        //{
        //    DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Class.SearchClass", request);
        //    DSXmlHelper rsphelper = dsrsp.GetContent();

        //    Dictionary<string, ClassInfo> classList = new Dictionary<string, ClassInfo>();
        //    foreach (XmlElement element in rsphelper.GetElements("Class"))
        //    {
        //        ClassInfo info = new ClassInfo();
        //        info.ClassID = element.GetAttribute("ID");
        //        info.ClassName = element.GetAttribute("ClassName");
        //        info.StudentCount = element.GetAttribute("StudentCount");
        //        info.Teacher = element.GetAttribute("TeacherName");
        //        info.Department = element.GetAttribute("Department");
        //        info.GradeYear = element.GetAttribute("GradeYear");
        //        classList.Add(info.ClassID, info);
        //    }
        //    return classList;
        //}

        ///// <summary>
        ///// 依年級取得班級清單(含班導師)
        ///// </summary>
        ///// <param name="gradeYear">年級</param>
        ///// <returns>
        ///// DSResponse
        ///// </returns>
        //internal static Dictionary<string, ClassInfo> GetDetailClassListByGradeYear(string gradeYear, string status)
        //{
        //    DSRequest dsreq = new DSRequest();
        //    DSXmlHelper helper = new DSXmlHelper("GetClassListRequest");
        //    helper.AddElement("Field");
        //    helper.AddElement("Field", "All");
        //    helper.AddElement("Condition");
        //    helper.AddElement("Condition", "GradeYear", gradeYear);
        //    //helper.AddElement("Condition", "Status", status);
        //    dsreq.SetContent(helper);
        //    DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Class.GetDetailListByGradeYear", dsreq);
        //    DSXmlHelper rsphelper = dsrsp.GetContent();

        //    Dictionary<string, ClassInfo> classList = new Dictionary<string, ClassInfo>();
        //    foreach (XmlElement element in rsphelper.GetElements("Class"))
        //    {
        //        ClassInfo info = new ClassInfo();
        //        info.ClassID = element.GetAttribute("ID");
        //        info.ClassName = element.GetAttribute("ClassName");
        //        info.StudentCount = element.GetAttribute("StudentCount");
        //        info.Teacher = element.GetAttribute("TeacherName");
        //        info.Department = element.GetAttribute("Department");
        //        info.GradeYear = element.GetAttribute("GradeYear");
        //        classList.Add(info.ClassID, info);
        //    }
        //    return classList;
        //}

        internal static DSResponse GetClass()
        {
            return FeatureBase.CallService("SmartSchool.Class.GetDetailList", new DSRequest("<GetClassListRequest><Field><All></All></Field><Condition></Condition><Order><GradeYear/><DisplayOrder/></Order></GetClassListRequest>"));
        }

        public static DSResponse GetClass(params string[] classIDList)
        {
            string req = "<GetClassListRequest><Field><All></All></Field><Condition>";
            foreach (string id in classIDList)
            {
                req += "<ID>" + id + "</ID>";
            }
            req += "</Condition><Order><GradeYear/><DisplayOrder/></Order></GetClassListRequest>";
            return FeatureBase.CallService("SmartSchool.Class.GetDetailList", new DSRequest(req));
        }

        //public static DSResponse GetDetail(string classid)
        //{
        //    DSXmlHelper helper = new DSXmlHelper("GetGradeYearRequest");
        //    helper.AddElement("Field");
        //    helper.AddElement("Field", "All");
        //    helper.AddElement("Condition");
        //    helper.AddElement("Condition", "ClassIDList");
        //    helper.AddElement("Condition/ClassIDList", "ClassID", classid);
        //    DSRequest dsreq = new DSRequest(helper);
        //    return FeatureBase.CallService("SmartSchool.Class.GetAbstractList", dsreq);
        //}

        public static DSResponse GetClassList()
        {
            DSXmlHelper helper = new DSXmlHelper("GetGradeYearRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "ClassID");
            helper.AddElement("Field", "ClassName");
            helper.AddElement("Order");
            helper.AddElement("Order", "DisplayOrder");
            helper.AddElement("Order", "ClassName");
            DSRequest dsreq = new DSRequest(helper);
            return FeatureBase.CallService("SmartSchool.Class.GetAbstractList", dsreq);
        }

        //internal static DSResponse GetClassStudentList(string classid)
        //{
        //    DSXmlHelper helper = new DSXmlHelper("GetStudentListRequest");
        //    helper.AddElement("Field");
        //    helper.AddElement("Field", "All");
        //    helper.AddElement("Condition");
        //    helper.AddElement("Condition", "ClassID", classid);
        //    helper.AddElement("Order");
        //    helper.AddElement("Order", "SeatNo", "asc");
        //    return FeatureBase.CallService("SmartSchool.Class.GetClassStudentList", new DSRequest(helper));
        //}

        //internal static DSResponse GetClassStudentList(string[] classid)
        //{
        //    DSXmlHelper helper = new DSXmlHelper("GetStudentListRequest");
        //    helper.AddElement("Field");
        //    helper.AddElement("Field", "ID");
        //    helper.AddElement("Condition");
        //    foreach (string cid in classid)
        //        helper.AddElement("Condition", "ClassID", cid);
        //    return FeatureBase.CallService("SmartSchool.Class.GetClassStudentList", new DSRequest(helper));
        //}

        public static DSResponse GenderStatistic(string[] classidList)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Condition");
            foreach (string id in classidList)
            {
                helper.AddElement("Condition","RefClassID", id);
            }
            return FeatureBase.CallService("SmartSchool.Class.GetGenderStatistic", new DSRequest(helper));
        }
    }
}
