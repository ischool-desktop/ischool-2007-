using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.API.StudentExtension
{
    // 已經搬到 GeneralSHUpdateRecord
    internal class UpdateRecord : Customization.Data.StudentExtension.UpdateRecordInfo
    {
        private Dictionary<string, string> _Items = new Dictionary<string, string>();

        private void SetValue(string name, string value)
        {
            if (_Items.ContainsKey(name))
                _Items[name] = value;
            else
                _Items.Add(name, value);
        }

        private string GetValue(string name)
        {
            if (_Items.ContainsKey(name))
                return _Items[name];
            else
                return "";
        }

        private XmlElement _Detail;

        public UpdateRecord(XmlElement UpdateCodeMappingElement, XmlElement UpdateRecordElement)
        {
            _Detail = UpdateRecordElement;
            Dictionary<string, string> updateCodeMapping = new Dictionary<string, string>();
            foreach (XmlNode var in UpdateCodeMappingElement.SelectNodes("異動"))
            {
                string UpdateCode, UpdateType;
                UpdateCode = var.SelectSingleNode("代號").InnerText;
                UpdateType = var.SelectSingleNode("分類").InnerText;
                if (!updateCodeMapping.ContainsKey(UpdateCode))
                {
                    updateCodeMapping.Add(UpdateCode, UpdateType);
                }
            }

            foreach (XmlNode node in UpdateRecordElement.ChildNodes)
            {
                if (node.Name != "ContextInfo")
                {
                    if (node.Name == "UpdateCode" && updateCodeMapping.ContainsKey(node.InnerText))
                    {
                        this.SetValue(UpdateRecordType, updateCodeMapping[node.InnerText]);
                    }
                    this.SetValue(node.Name, node.InnerText);
                }
                else
                {
                    if (node.SelectSingleNode("ContextInfo") != null)
                    {
                        foreach (XmlNode contextInfo in node.SelectSingleNode("ContextInfo").ChildNodes)
                        {
                            this.SetValue(contextInfo.Name, contextInfo.InnerText);
                        }
                    }
                }
            }
        }

        #region UpdateRecordInfo 成員

        //科別
        public string Department { get { return GetValue("Department"); } }
        //核準日期(回填)
        public string ADDate { get { return GetValue("ADDate"); } }
        //核準文號(回填)
        public string ADNumber { get { return GetValue("ADNumber"); } }
        //異動日期
        public string UpdateDate { get { return GetValue("UpdateDate"); } }
        //異動代碼
        public string UpdateCode { get { return GetValue("UpdateCode"); } }
        //原因及事項
        public string UpdateDescription { get { return GetValue("UpdateDescription"); } }
        //姓名
        public new string Name { get { return GetValue("Name"); } }
        //學號
        public string StudentNumber { get { return GetValue("StudentNumber"); } }
        //性別
        public string Gender { get { return GetValue("Gender"); } }
        //身份證號
        public string IDNumber { get { return GetValue("IDNumber"); } }
        //生日
        public string BirthDate { get { return GetValue("Birthdate"); } }
        //年級
        public string GradeYear { get { return GetValue("GradeYear"); } }
        //最後核準日期
        public string LastADDate { get { return GetValue("LastADDate"); } }
        //最後核準文號
        public string LastADNumber { get { return GetValue("LastADNumber"); } }
        //備註
        public string Comment { get { return GetValue("Comment"); } }
        //最後核準異動代碼
        public string LastUpdateCode { get { return GetValue("LastUpdateCode"); } }
        //新學號
        public string NewStudentNumber { get { return GetValue("NewStudentNumber"); } }
        //轉入前學生資料-學校
        public string PreviousSchool { get { return GetValue("PreviousSchool"); } }
        //轉入前學生資料-學號
        public string PreviousStudentNumber { get { return GetValue("PreviousStudentNumber"); } }
        //轉入前學生資料-科別
        public string PreviousDepartment { get { return GetValue("PreviousDepartment"); } }
        //轉入前學生資料-(最後核準日期)
        public string PreviousSchoolLastADDate { get { return GetValue("PreviousSchoolLastADDate"); } }
        //轉入前學生資料-(最後核準文號)
        public string PreviousSchoolLastADNumber { get { return GetValue("PreviousSchoolLastADNumber"); } }
        //轉入前學生資料-年級
        public string PreviousGradeYear { get { return GetValue("PreviousGradeYear"); } }
        //入學資格-畢業國中所在地代碼
        public string GraduateSchoolLocationCode { get { return GetValue("GraduateSchoolLocationCode"); } }
        //入學資格-畢業國中
        public string GraduateSchool { get { return GetValue("GraduateSchool"); } }
        //畢(結)業證書字號
        public string GraduateCertificateNumber { get { return GetValue("GraduateCertificateNumber"); } }
        // 異動種類(新生異動、轉入異動、...)
        public string UpdateRecordType { get { return GetValue("UpdateRecordType"); } }

        public XmlElement Detail
        {
            get { return _Detail; }
        }

        #endregion
    }
}
