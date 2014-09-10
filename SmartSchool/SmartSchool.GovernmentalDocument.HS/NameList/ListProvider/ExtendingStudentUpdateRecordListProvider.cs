using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

//using SmartPlugIn.StudentRecord.NameList.ＮameListBuilder;

namespace SmartSchool.GovernmentalDocument.NameList
{
    class ExtendingStudentUpdateRecordListProvider : INameListProvider
    {
        //比對大小
        private static int CompareUpdateRecord(XmlElement x, XmlElement y)
        {
            string gradeyear = x.SelectSingleNode("GradeYear").InnerText;
            string dept = x.SelectSingleNode("Department").InnerText;
            string code = x.SelectSingleNode("UpdateCode").InnerText.Substring(0, 1);
            int c = gradeyear.CompareTo(y.SelectSingleNode("GradeYear").InnerText);
            if (c == 0)
            {
                int c2 = dept.CompareTo(y.SelectSingleNode("Department").InnerText);
                if (c2 == 0)
                {
                    return code.CompareTo(y.SelectSingleNode("UpdateCode").InnerText.Substring(0, 1));
                }
                else
                {
                    return c2;
                }
            }
            else
            {
                return c;
            }
        }
        //允許代號列表
        private string[] _CodeList = new string[] {
            "211", "221", "222", "223", "224", "231", "232", "233", "234", "235", "236", "237", "238", 
            "301", "311", "312", "313", "314", "315", "321", "323", "325", "326", "341", "342", "343", "345", "346", "347", "348", "351", "361", "362", "363", "364", "365", "366", 
            "401", "402", "403", "404", "405", "406", "407", "408" };
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
            get { return "延修生學籍異動名冊"; }
        }

        public List<System.Xml.XmlElement> GetExpectantList()
        {
            List<XmlElement> list = new List<XmlElement>();
            foreach (XmlElement var in SmartSchool.Feature.QueryStudent.GetUpdateRecordByCode(_CodeList).GetContent().GetElements("UpdateRecord"))
            {
                if (var.SelectSingleNode("GradeYear").InnerText == "延修生")
                {
                    list.Add(var);
                }
            }
            return (list);
        }

        public System.Xml.XmlElement CreateNameList(string schoolYear, string semester, List<System.Xml.XmlElement> list)
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

            Dictionary<string, Dictionary<string, Dictionary<string, XmlElement>>> code_gradeyear_dept_map = new Dictionary<string, Dictionary<string, Dictionary<string, XmlElement>>>();
            doc.LoadXml("<異動名冊 類別=\"延修生學籍異動名冊\" 學年度=\"" + schoolYear + "\" 學期=\"" + semester + "\" 學校代號=\"" + CurrentUser.Instance.SchoolCode + "\" 學校名稱=\"" + CurrentUser.Instance.SchoolChineseName + "\"/>");

            foreach (XmlElement var in list)
            {
                DSXmlHelper helper = new DSXmlHelper(var);
                string gradeyear = helper.GetText("GradeYear");
                string dept = helper.GetText("Department");
                string code = helper.GetText("UpdateCode").Substring(0, 1);
                XmlElement node;
                #region 清單
                if (!code_gradeyear_dept_map.ContainsKey(gradeyear))
                {
                    code_gradeyear_dept_map.Add(gradeyear, new Dictionary<string, Dictionary<string, XmlElement>>());
                }
                if (!(code_gradeyear_dept_map[gradeyear].ContainsKey(dept)))
                {
                    code_gradeyear_dept_map[gradeyear].Add(dept, new Dictionary<string, XmlElement>());
                }
                if (!(code_gradeyear_dept_map[gradeyear][dept].ContainsKey(code)))
                {
                    node = doc.CreateElement("清單");
                    node.SetAttribute("科別", dept);
                    node.SetAttribute("年級", gradeyear);
                    node.SetAttribute("科別代號", (deptCode.ContainsKey(dept) ? deptCode[dept] : ""));
                    code_gradeyear_dept_map[gradeyear][dept].Add(code, node);
                    doc.DocumentElement.AppendChild(node);
                }
                else
                {
                    node = code_gradeyear_dept_map[gradeyear][dept][code];
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

                node.AppendChild(dataElement);
            }
            #endregion
            return doc.DocumentElement;
        }

        #endregion
    }
}

