using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.GovernmentalDocument.NameList
{
    class ExtendingStudentListProvider : INameListProvider
    {
        //比對大小
        private static int CompareUpdateRecord(XmlElement x, XmlElement y)
        {
            string gradeyear = x.SelectSingleNode("GradeYear").InnerText;
            string dept = x.SelectSingleNode("Department").InnerText;
            int c = gradeyear.CompareTo(y.SelectSingleNode("GradeYear").InnerText);
            if (c == 0)
            {
                return dept.CompareTo(y.SelectSingleNode("Department").InnerText);
            }
            else
            {
                return c;
            }
        }
        //允許代號列表
        private string[] _CodeList = new string[] { "235", "236" };
        //明國年轉換
        private string CDATE(string p)
        {
            DateTime d = DateTime.Now;
            if (p != "" && DateTime.TryParse(p, out d))
            {
                return "" + (d.Year - 1911) + "/" + d.Month + "/" + d.Day;
            }
            else
                return "";
        }
        #region INameListProvider 成員

        public string Title
        {
            get { return "延修生名冊"; }
        }

        public List<XmlElement> GetExpectantList()
        {
            List<XmlElement> list = new List<XmlElement>();
            list.AddRange(SmartSchool.Feature.QueryStudent.GetUpdateRecordByCode(_CodeList).GetContent().GetElements("UpdateRecord"));
            return (list);
        }

        public System.Xml.XmlElement CreateNameList(string schoolYear, string semester, List<XmlElement> list)
        {
            XmlDocument doc = new XmlDocument();
            Dictionary<string, string> deptCode = new Dictionary<string, string>();
            #region 建立科別代碼查詢表
            foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetDepartment().GetContent().GetElements("Department"))
            {
                deptCode.Add(var.SelectSingleNode("Name").InnerText, var.SelectSingleNode("Code").InnerText);
            }
            #endregion
            //依年級科別排序資料
            //list.Sort(CompareUpdateRecord);
            #region 產生Xml
            Dictionary<string, Dictionary<string, XmlElement>> gradeyear_dept_map = new Dictionary<string, Dictionary<string, XmlElement>>();
            doc.LoadXml("<異動名冊 類別=\"延修生名冊\" 學年度=\"" + schoolYear + "\" 學期=\"" + semester + "\" 學校代號=\"" + CurrentUser.Instance.SchoolCode + "\" 學校名稱=\"" + CurrentUser.Instance.SchoolChineseName + "\"/>");
            foreach (XmlElement var in list)
            {
                DSXmlHelper helper = new DSXmlHelper(var);
                string gradeyear = helper.GetText("GradeYear");
                string dept = helper.GetText("Department");
                XmlElement deptgradeNode;
                #region 清單
                if (!gradeyear_dept_map.ContainsKey(gradeyear))
                {
                    gradeyear_dept_map.Add(gradeyear, new Dictionary<string, XmlElement>());
                }
                if (!(gradeyear_dept_map[gradeyear].ContainsKey(dept)))
                {
                    deptgradeNode = doc.CreateElement("清單");
                    deptgradeNode.SetAttribute("科別", dept);
                    deptgradeNode.SetAttribute("年級", gradeyear);
                    deptgradeNode.SetAttribute("科別代號", (deptCode.ContainsKey(dept) ? deptCode[dept] : ""));
                    gradeyear_dept_map[gradeyear].Add(dept, deptgradeNode);
                    doc.DocumentElement.AppendChild(deptgradeNode);
                }
                else
                {
                    deptgradeNode = gradeyear_dept_map[gradeyear][dept];
                }
                #endregion
                
                #region 異動紀錄
                XmlElement dataElement = doc.CreateElement("異動紀錄");
                dataElement.SetAttribute("編號", helper.GetText("@ID"));
                dataElement.SetAttribute("異動代號", helper.GetText("UpdateCode"));
                dataElement.SetAttribute("異動日期", CDATE(helper.GetText("UpdateDate")));
                dataElement.SetAttribute("原因及事項", helper.GetText("UpdateDescription"));
                dataElement.SetAttribute("學號", helper.GetText("StudentNumber"));
                dataElement.SetAttribute("姓名", helper.GetText("Name"));
                dataElement.SetAttribute("性別", helper.GetText("Gender"));
                dataElement.SetAttribute("身分證號", helper.GetText("IDNumber"));
                dataElement.SetAttribute("備查日期", CDATE(helper.GetText("LastADDate")));
                dataElement.SetAttribute("備查文號", helper.GetText("LastADNumber"));
                dataElement.SetAttribute("新學號", helper.GetText("ContextInfo/ContextInfo/NewStudentNumber"));
                dataElement.SetAttribute("備註", helper.GetText("Comment"));

                #endregion
                deptgradeNode.AppendChild(dataElement);
            }
            #endregion
            return doc.DocumentElement;
        }


        #endregion
    }
}
