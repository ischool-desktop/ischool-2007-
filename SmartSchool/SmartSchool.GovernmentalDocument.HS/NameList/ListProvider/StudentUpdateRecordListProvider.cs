using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.GovernmentalDocument.NameList
{
    class StudentUpdateRecordListProvider : INameListProvider 
    {
        //���j�p
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

        //�ƧǾǸ�
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

        //�ƧǦ~��
        private static int GradeYearComparison(XmlElement a, XmlElement b)
        {
            string gy_a = new DSXmlHelper(a).GetText("@�~��");
            string gy_b = new DSXmlHelper(b).GetText("@�~��");

            if (gy_a.CompareTo(gy_b) == 0) //�~�Ŭ۵�
                return DepartmentComparison(a, b);

            int try_a, try_b;
            if (int.TryParse(gy_a, out try_a) && int.TryParse(gy_b, out try_b))
                return try_a.CompareTo(try_b);
            else if (int.TryParse(gy_a, out try_a))
                return -1;
            else if (int.TryParse(gy_b, out try_b))
                return 1;
            else
                return gy_a.CompareTo(gy_b);
        }

        //�ƧǬ�O
        private static int DepartmentComparison(XmlElement a, XmlElement b)
        {
            string dept_a = new DSXmlHelper(a).GetText("@��O�N��");
            string dept_b = new DSXmlHelper(b).GetText("@��O�N��");

            if (dept_a.CompareTo(dept_b) == 0) //��O�۵�
                return UpdateCodeComparison(a, b);

            int try_a, try_b;
            if (int.TryParse(dept_a, out try_a) && int.TryParse(dept_b, out try_b))
                return try_a.CompareTo(try_b);
            else if (int.TryParse(dept_a, out try_a))
                return -1;
            else if (int.TryParse(dept_b, out try_b))
                return 1;
            else
                return dept_a.CompareTo(dept_b);
        }

        //�Ƨǲ��ʥN�X
        private static int UpdateCodeComparison(XmlElement a, XmlElement b)
        {
            string uc_a = new DSXmlHelper(a).GetText("���ʬ���/@���ʥN��");
            string uc_b = new DSXmlHelper(b).GetText("���ʬ���/@���ʥN��");

            if (!string.IsNullOrEmpty(uc_a)) uc_a = uc_a.Substring(0, 1);
            if (!string.IsNullOrEmpty(uc_b)) uc_b = uc_b.Substring(0, 1);

            int try_a, try_b;
            if (int.TryParse(uc_a, out try_a) && int.TryParse(uc_b, out try_b))
                return try_a.CompareTo(try_b);
            else if (int.TryParse(uc_a, out try_a))
                return -1;
            else if (int.TryParse(uc_b, out try_b))
                return 1;
            else
                return uc_a.CompareTo(uc_b);
        }

        //���\�N���C��
        private string[] _CodeList = new string[] {
            "211", "221", "222", "223", "224", "231", "232", "233", "234", "235", "236", "237", "238", 
            "301", "311", "312", "313", "314", "315", "321", "323", "325", "326", "341", "342", "343", "345", "346", "347", "348", "351", "361", "362", "363", "364", "365", "366", 
            "401", "402", "403", "404", "405", "406", "407", "408" };
        //����~�ഫ
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

        #region INameListProvider ����

        public string Title
        {
            get { return "���y���ʦW�U"; }
        }

        public List<System.Xml.XmlElement> GetExpectantList()
        {
            List<XmlElement> list = new List<XmlElement>();
            foreach (XmlElement var in SmartSchool.Feature.QueryStudent.GetUpdateRecordByCode(_CodeList).GetContent().GetElements("UpdateRecord"))
            {
                if (var.SelectSingleNode("GradeYear").InnerText != "���ץ�")
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

            #region �إ߬�O�N�X�d�ߪ�
            foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetDepartment().GetContent().GetElements("Department"))
            {
                deptCode.Add(var.SelectSingleNode("Name").InnerText, var.SelectSingleNode("Code").InnerText);
            }
            #endregion

            //�̦~�Ŭ�O�ƧǸ��
            //list.Sort(CompareUpdateRecord);

            list.Sort(StudentNumberComparison);

            #region ����Xml

            Dictionary<string, Dictionary<string, Dictionary<string, XmlElement>>> code_gradeyear_dept_map = new Dictionary<string, Dictionary<string, Dictionary<string, XmlElement>>>();
            doc.LoadXml("<���ʦW�U ���O=\"���y���ʦW�U\" �Ǧ~��=\"" + schoolYear + "\" �Ǵ�=\"" + semester + "\" �ǮեN��=\""+CurrentUser.Instance.SchoolCode+"\" �ǮզW��=\""+CurrentUser.Instance.SchoolChineseName+"\"/>");

            foreach (XmlElement var in list)
            {
                DSXmlHelper helper = new DSXmlHelper(var);
                string gradeyear = helper.GetText("GradeYear");
                string dept = helper.GetText("Department");
                string code = helper.GetText("UpdateCode").Substring(0, 1);
                XmlElement node;
                #region �M��
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
                    node = doc.CreateElement("�M��");
                    node.SetAttribute("��O", dept);
                    node.SetAttribute("�~��", gradeyear);
                    node.SetAttribute("��O�N��", (deptCode.ContainsKey(dept) ? deptCode[dept] : ""));
                    code_gradeyear_dept_map[gradeyear][dept].Add(code, node);
                    doc.DocumentElement.AppendChild(node);
                }
                else
                {
                    node = code_gradeyear_dept_map[gradeyear][dept][code];
                }
                #endregion

                #region ���ʬ���
                XmlElement dataElement = doc.CreateElement("���ʬ���");
                dataElement.SetAttribute("�s��", helper.GetText("@ID"));
                dataElement.SetAttribute("���ʥN��", helper.GetText("UpdateCode"));
                dataElement.SetAttribute("���ʤ��", CDATE(helper.GetText("UpdateDate")));
                dataElement.SetAttribute("��]�Ψƶ�", helper.GetText("UpdateDescription"));
                dataElement.SetAttribute("�Ǹ�", helper.GetText("StudentNumber"));
                dataElement.SetAttribute("�m�W", helper.GetText("Name"));
                dataElement.SetAttribute("�ʧO", helper.GetText("Gender"));

                // �ʧO�N�� �k 1  �k 2
                string genderCode = "";
                if (helper.GetText("Gender") == "�k") genderCode = "1";
                if (helper.GetText("Gender") == "�k") genderCode = "2";
                dataElement.SetAttribute("�ʧO�N��", genderCode);

                dataElement.SetAttribute("�X�ͦ~���", CDATE(helper.GetText("Birthdate")));
                dataElement.SetAttribute("�����Ҹ�", helper.GetText("IDNumber"));
                dataElement.SetAttribute("�Ƭd���", CDATE(helper.GetText("LastADDate")));
                dataElement.SetAttribute("�Ƭd�帹", helper.GetText("LastADNumber"));
                dataElement.SetAttribute("�s�Ǹ�", helper.GetText("ContextInfo/ContextInfo/NewStudentNumber"));
                dataElement.SetAttribute("�󥿫���", helper.GetText("ContextInfo/ContextInfo/NewData"));
                
                //�p�G�O�u�󥿥ͤ�v�A�����~
                if (helper.GetText("UpdateCode") == "405")
                    dataElement.SetAttribute("�󥿫���", CDATE(helper.GetText("ContextInfo/ContextInfo/NewData")));

                dataElement.SetAttribute("�Ƶ�", helper.GetText("Comment"));
                #endregion

                node.AppendChild(dataElement);
            }
            #endregion

            #region �ƧǦ~�šB��O�N�X�B���ʥN�X

            List<XmlElement> deptList = new List<XmlElement>();
            foreach (XmlElement var in doc.DocumentElement.SelectNodes("�M��"))
                deptList.Add(var);
            deptList.Sort(GradeYearComparison);

            DSXmlHelper docHelper = new DSXmlHelper(doc.DocumentElement);
            while (docHelper.PathExist("�M��")) docHelper.RemoveElement("�M��");

            foreach (XmlElement var in deptList)
                docHelper.AddElement(".", var);

            #endregion

            return doc.DocumentElement;
        }

        #endregion
    }
}
