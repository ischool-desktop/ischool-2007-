using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.GovernmentalDocument.NameList
{
    class EnrollmentListProvider:INameListProvider
    {
        //比對大小
        private static int CompareUpdateRecord(XmlElement x, XmlElement y)
        {
            string gradeyear = x.SelectSingleNode("GradeYear").InnerText;
            string dept = x.SelectSingleNode("Department").InnerText;
            int c = gradeyear.CompareTo(y.SelectSingleNode("GradeYear").InnerText);
            if (c == 0)
            {
                int d=dept.CompareTo(y.SelectSingleNode("Department").InnerText);
                return d == 0 ? 1 : d;
            }
            else
            {
                return c;
            }
        }

        //排序學號
        private static int StudentNumberComparison(XmlElement a, XmlElement b)
        {
            string sa = new DSXmlHelper(a).GetText("StudentNumber");
            string sb = new DSXmlHelper(b).GetText("StudentNumber");
            int ia, ib;
            if (int.TryParse(sa, out ia) && int.TryParse(sb, out ib))
                return ia.CompareTo(ib);
            else
                return sa.CompareTo(sb);
        }

        //排序科別代碼
        private static int DepartmentCodeComparison(XmlElement a, XmlElement b)
        {
            string sa = new DSXmlHelper(a).GetText("@科別代號");
            string sb = new DSXmlHelper(b).GetText("@科別代號");
            int ia, ib;
            if (int.TryParse(sa, out ia) && int.TryParse(sb, out ib))
                return ia.CompareTo(ib);
            else
                return sa.CompareTo(sb);
        }

        //允許代號列表
        private string[] _CodeList = new string[] { "001", "002", "003", "004", "005", "006", "007", "008" };
        //明國年轉換
        private string CDATE(string p)
        {
            DateTime d = DateTime.Now;
            if (p != ""&&DateTime.TryParse(p, out d))
            {
                return "" + (d.Year - 1911) + "/" + d.Month + "/" + d.Day;
            }
            else
                return "";
        }
        #region INameListProvider 成員

        public string Title
        {
            get { return "新生名冊"; }
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
            Dictionary<string, string> deptCode=new Dictionary<string,string>();
            #region 建立科別代碼查詢表
            foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetDepartment().GetContent().GetElements("Department"))
            {
                deptCode.Add(var.SelectSingleNode("Name").InnerText, var.SelectSingleNode("Code").InnerText);
            } 
            #endregion
            //依年級科別排序資料
            //list.Sort(CompareUpdateRecord);

            //排序學號
            list.Sort(StudentNumberComparison);

            #region 產生Xml
            Dictionary<string, Dictionary<string, XmlElement>> gradeyear_dept_map = new Dictionary<string, Dictionary<string, XmlElement>>();
            doc.LoadXml("<異動名冊 類別=\"新生名冊\" 學年度=\"" + schoolYear + "\" 學期=\"" + semester + "\" 學校代號=\"" + CurrentUser.Instance.SchoolCode + "\" 學校名稱=\"" + CurrentUser.Instance.SchoolChineseName + "\"/>");
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
                dataElement.SetAttribute("學號", helper.GetText("StudentNumber"));
                dataElement.SetAttribute("姓名", helper.GetText("Name"));
                dataElement.SetAttribute("身分證號", helper.GetText("IDNumber"));
                dataElement.SetAttribute("性別", helper.GetText("Gender"));
                dataElement.SetAttribute("性別代號", (helper.GetText("Gender") == "男" ? "1" :(helper.GetText("Gender") == "女" ? "2" : "")));
                dataElement.SetAttribute("出生年月日", CDATE(helper.GetText("Birthdate")));
                dataElement.SetAttribute("入學資格代號", helper.GetText("UpdateCode"));

                string schoolName = helper.GetText("ContextInfo/ContextInfo/GraduateSchool");
                if (schoolName != "")
                {
                    switch (helper.GetText("UpdateCode"))
                    {
                        case "001": schoolName += " 畢業"; break; 
                        case "003": schoolName += " 結業"; break;
                        case "004":schoolName += " 修滿"; break;
                        case "002":
                        case "005":
                        case "006":
                        case "007":
                        case "008": 
                        default:
                            break;
                    }
                }

                dataElement.SetAttribute("畢業國中",schoolName);
                dataElement.SetAttribute("畢業國中所在縣市代號", helper.GetText("ContextInfo/ContextInfo/GraduateSchoolLocationCode"));
                dataElement.SetAttribute("備註", helper.GetText("Comment"));
                #endregion
                deptgradeNode.AppendChild(dataElement);
            } 
            #endregion

            #region 排序科別代碼

            List<XmlElement> deptList = new List<XmlElement>();
            foreach (XmlElement var in doc.DocumentElement.SelectNodes("清單"))
                deptList.Add(var);
            deptList.Sort(DepartmentCodeComparison);

            DSXmlHelper docHelper = new DSXmlHelper(doc.DocumentElement);
            while (docHelper.PathExist("清單")) docHelper.RemoveElement("清單");

            foreach (XmlElement var in deptList)
                docHelper.AddElement(".", var);

            #endregion

            return doc.DocumentElement;
        }


        #endregion
    }
}
