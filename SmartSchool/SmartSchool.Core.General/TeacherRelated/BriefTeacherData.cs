using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.ClassRelated;

namespace SmartSchool.TeacherRelated
{
    public class BriefTeacherData
    {
        private readonly string _ID;
        private readonly string _TeacherName;
        private readonly string _nickname;
        private readonly string _Status;
        private readonly string _Gender;
        private readonly string _IDNumber;
        private readonly string _ContactPhone;
        private readonly string _Category;
        //private List<SupervisedByClassInfo> _SupervisedByClassInfo;

        public string ID { get { return _ID; } }
        public string TeacherName { get { return _TeacherName; } }
        public string Nickname { get { return _nickname; } }
        public string UniqName
        {
            get
            {
                if (string.IsNullOrEmpty(Nickname))
                    return TeacherName;
                else
                    return string.Format("{0} ({1})", TeacherName, Nickname);
            }
        }
        public string Status { get { return _Status; } }
        public string Gender { get { return _Gender; } }
        public string IDNumber { get { return _IDNumber; } }
        public string ContactPhone { get { return _ContactPhone; } }
        public string Category { get { return _Category; } }
        public string SupervisedByClass
        {
            get
            {
                //string s = "";
                //foreach (SupervisedByClassInfo var in _SupervisedByClassInfo)
                //{
                //    s += (s == "" ? "" : "、") + var.SupervisedByClassName;
                //}
                //return s;

                if (_Status == "刪除")
                {
                    return "";
                }
                else
                {
                    string s = "";
                    foreach (ClassInfo var in Class.Instance.GetSupervise(_ID))
                    {
                        s += (s == "" ? "" : "、") + var.ClassName;
                    }
                    return s;
                }
            }
        }
        //public List<SupervisedByClassInfo> SupervisedByClassInfo { get { return _SupervisedByClassInfo; } }


        internal BriefTeacherData(XmlElement element)
        {
            _ID = element.SelectSingleNode("@ID").InnerText;
            _TeacherName = element.SelectSingleNode("TeacherName").InnerText;
            _nickname = element.SelectSingleNode("Nickname").InnerText;
            _Status = element.SelectSingleNode("Status").InnerText;
            _Gender = element.SelectSingleNode("Gender").InnerText;
            _IDNumber = element.SelectSingleNode("IDNumber").InnerText;
            _ContactPhone = element.SelectSingleNode("ContactPhone").InnerText;
            _Category = element.SelectSingleNode("Category").InnerText;
            //_SupervisedByClassInfo = new List<SupervisedByClassInfo>();
        }

        public override string ToString()
        {
            return UniqName;
        }
    }
    //public class SupervisedByClassInfo
    //{
    //    private readonly string _SupervisedByClassID;
    //    private readonly string _SupervisedByClassName;
    //    private readonly string _SupervisedByGradeYear;

    //    public SupervisedByClassInfo(string supervisedByClassID, string supervisedByClassName, string supervisedByGradeYear)
    //    {
    //        _SupervisedByClassID = supervisedByClassID;
    //        _SupervisedByClassName = supervisedByClassName;
    //        _SupervisedByGradeYear = supervisedByGradeYear;
    //    }

    //    public SupervisedByClassInfo(XmlElement element)
    //    {
    //        _SupervisedByClassID = element.SelectSingleNode("SupervisedByClassID").InnerText;
    //        _SupervisedByClassName = element.SelectSingleNode("SupervisedByClassName").InnerText;
    //        _SupervisedByGradeYear = element.SelectSingleNode("SupervisedByGradeYear").InnerText;
    //    }

    //    public string SupervisedByClassID { get { return _SupervisedByClassID; } }
    //    public string SupervisedByClassName { get { return _SupervisedByClassName; } }
    //    public string SupervisedByGradeYear { get { return _SupervisedByGradeYear; } }
    //}
}
