//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Xml;
//using System.Text.RegularExpressions;

//namespace SmartSchool.StudentRelated.Search
//{
//    class SearchStudentInNormalStatus :SearchStudentInXML
//    {
//        protected override List<XmlElement> getStudentElement()
//        {
//            List<XmlElement> list=new List<XmlElement>();
//            foreach (XmlNode var in DataSource.SelectNodes("GradeYear/Class/Student"))
//            {
//                list.Add((XmlElement)var);
//            }
//            return list;
//        }
//    }
//}
