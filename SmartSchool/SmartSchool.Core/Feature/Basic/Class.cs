using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Common;
//using SmartSchool.ClassRelated;

namespace SmartSchool.Feature.Basic
{
    [QueryRequest()]
    public class Class
    {
        public static DSResponse GetGradeClass()
        {
            return CurrentUser.Instance.CallService("SmartSchool.Class.GetAbstractList", 
                    new DSRequest("<GetClassListRequest><Field><ClassID/><GradeYear/><Teacher/><ClassName/><StudentCount/></Field><Condition>"
                       + "<SchoolYear>" + CurrentUser.Instance.SchoolYear + "</SchoolYear>"
                       + "<Semester>" + CurrentUser.Instance.Semester + "</Semester>"
                       //+ "<Status>一般</Status>"
                       + "</Condition><Order><GradeYear/></Order></GetClassListRequest>")
                    );
        }

        /// <summary>
        /// 依年級取得班級清單
        /// </summary>
        /// <param name="gradeYear">年級</param>
        /// <returns>
        /// key : 班級編號
        /// value : 班級名稱
        /// </returns>
        public static Dictionary<string,string> GetClassListByGradeYear(string gradeYear)
        {
            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("GetClassListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "GradeYear", gradeYear);
            helper.AddElement("Order");
            helper.AddElement("Order", "DisplayOrder", "ASC");
            helper.AddElement("Order", "ID", "ASC");
            dsreq.SetContent(helper);
            DSResponse rsp = CurrentUser.Instance.CallService("SmartSchool.Class.GetAbstractListByGradeYear", dsreq);
           
            Dictionary<string, string> classList = new Dictionary<string, string>();
            foreach (XmlNode node in rsp.GetContent().GetElements("Class"))
            {
                classList.Add(node.Attributes["ID"].Value, node.Attributes["ClassName"].Value);
            }
            return classList;
        }

        /// <summary>
        /// 依年級取得班級清單(含班導師)
        /// </summary>
        /// <param name="gradeYear">年級</param>
        /// <returns>
        /// DSResponse
        /// </returns>
        //public static Dictionary<string, ClassInfo> GetDetailClassListByGradeYear(string gradeYear, string status)
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

        //internal static DSResponse GetDeletedClassList()
        //{
        //    DSRequest dsreq = new DSRequest();
        //    DSXmlHelper helper = new DSXmlHelper("GetClassListRequest");
        //    helper.AddElement("Field");
        //    helper.AddElement("Field", "All");
        //    helper.AddElement("Condition");
        //    helper.AddElement("Condition", "Status", "256");
        //    dsreq.SetContent(helper);
        //    return CurrentUser.Instance.CallService("SmartSchool.Class.GetDetailListByGradeYear", dsreq);
        //}

        public static List<int> ListSeatNo(string classID)
        {
            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("ListSeatNoRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "RefClassID", classID);

            dsreq.SetContent(helper);
            DSResponse rsp = CurrentUser.Instance.CallService("SmartSchool.Class.ListSeatNo", dsreq);

            List<int> list = new List<int>();
            foreach (XmlNode node in rsp.GetContent().GetElements("SeatNo"))
            {
                int no;
                if (int.TryParse(node.InnerText, out no))
                    list.Add(no);
            }
            return list;            
        }

        public static List<int> ListEmptySeatNo(string classID)
        {
            List<int> seatNoList = ListSeatNo(classID);
            int maxNo = 0;
            if (seatNoList.Count > 0)
                maxNo = seatNoList[seatNoList.Count - 1];
            List<int> needSeatNoList = new List<int>();
            for (int i = 1; i <= maxNo + 1; i++)
            {
                if (!seatNoList.Contains(i))
                    needSeatNoList.Add(i);
            }
            return needSeatNoList;
        }

        public static DSResponse GetDepartmentList(string gradeYear)
        {
            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("GetDepartmentListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "GradeYear", gradeYear);
            helper.AddElement("Order");
            helper.AddElement("Order", "GradeYear", "ASC");
            helper.AddElement("Order", "Name", "ASC");
            dsreq.SetContent(helper);
            return CurrentUser.Instance.CallService("SmartSchool.Class.GetDepartmentList", dsreq);
        }


    }
}
