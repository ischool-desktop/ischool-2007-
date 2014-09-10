//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Xml;
//using System.Text.RegularExpressions;

//namespace SmartSchool.StudentRelated.Search
//{
//    abstract class SearchStudentInXML : ISearchStudent
//    {
//        bool _SearchInName, _SearchInStudentID, _SearchInSSN;
//        XmlElement _DataSource;

//        public XmlElement DataSource
//        {
//            get { return _DataSource; }
//            set { _DataSource = value; }
//        }

//        #region ISearchStudent 成員

//        public bool SearchInName
//        {
//            get
//            {
//                return _SearchInName;
//            }
//            set
//            {
//                _SearchInName = value;
//            }
//        }

//        public bool SearchInStudentID
//        {
//            get
//            {
//                return _SearchInStudentID;
//            }
//            set
//            {
//                _SearchInStudentID = value;
//            }
//        }

//        public bool SearchInSSN
//        {
//            get
//            {
//                return _SearchInSSN;
//            }
//            set
//            {
//                _SearchInSSN = value;
//            }
//        }

//        public System.Xml.XmlElement Search(string key, int pageSize, int startPage)
//        {
//            if (_DataSource == null) return null;
//            Regex rx = new Regex(key.Replace("*", ".*"));
//            XmlDocument doc = new XmlDocument();
//            doc.LoadXml("<root/>");
//            int matchCount = 0;
//            int maxCount = pageSize * startPage;
//            int minCount = pageSize * (startPage - 1);
//            foreach (XmlElement var in getStudentElement())
//            {
//                if (matchCount == maxCount) break;
//                if (_SearchInName && rx.IsMatch(var.SelectSingleNode("Name").InnerText))
//                {
//                    matchCount++;
//                    if (matchCount > minCount )
//                        doc.DocumentElement.AppendChild(doc.ImportNode(var, true));
//                    continue;
//                }
//                if (_SearchInStudentID && rx.IsMatch(var.SelectSingleNode("StudentID").InnerText))
//                {
//                    matchCount++;
//                    if (matchCount > minCount )
//                        doc.DocumentElement.AppendChild(doc.ImportNode(var, true));
//                    continue;
//                }
//                if (_SearchInSSN && rx.IsMatch(var.SelectSingleNode("SSN").InnerText))
//                {
//                    matchCount++;
//                    if (matchCount > minCount)
//                        doc.DocumentElement.AppendChild(doc.ImportNode(var, true));
//                    continue;
//                }
//            }
//            return doc.DocumentElement;
//        }

//        #endregion
//        abstract protected List<XmlElement> getStudentElement();
//    }
//}
