//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Xml;
//using System.Text.RegularExpressions;

//namespace SmartSchool.StudentRelated.Search
//{
//    class SearchStudentInGrade : SearchStudentInXML
//    {
//        protected override List<XmlElement> getStudentElement()
//        {
//            List<XmlElement> list = new List<XmlElement>();
//            foreach (XmlNode var in DataSource.SelectNodes("Class/Student"))
//            {
//                list.Add((XmlElement)var);
//            }
//            return list;
//        }
//    }
//}
