using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Student
{
    public class StudentSnapShop : FeatureBase
    {
        private string _RefStudentID = "";
        private string _SnapshopName = "";
        private string _PresentType = "";
        private XmlElement _XmlContent = null;
        private string _PresentContent = "";
        private int _Version = 0;

        public string RefStudentID { get { return _RefStudentID; } set { _RefStudentID = value; } }
        public string SnapshopName { get { return _SnapshopName; } set { _SnapshopName = value; } }
        public string PresentType { get { return _PresentType; } set { _PresentType = value; } }
        public XmlElement XmlContent { get { return _XmlContent; } set { _XmlContent = value; } }
        public string PresentContent { get { return _PresentContent; } set { _PresentContent = value; } }
        public int Version { get { return _Version; } }

        public static void AddSnapShop(params  StudentSnapShop[] items)
        {
            if ( items.Length == 0 )
                throw new Exception("至少需傳入一項");

            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("Request");
            foreach ( StudentSnapShop snp in items )
            {
                helper.AddElement("Snapshop");
                helper.AddElement("Snapshop", "RefStudentID", snp.RefStudentID);
                helper.AddElement("Snapshop", "SnapshopName", snp.SnapshopName);
                helper.AddElement("Snapshop", "PresentType", snp.PresentType);
                helper.AddElement("Snapshop", "XmlContent");
                if ( snp.XmlContent != null )
                    helper.AddElement("Snapshop/XmlContent", snp.XmlContent);
                helper.AddElement("Snapshop", "PresentContent", snp.PresentContent);
            }
            dsreq.SetContent(helper);
            DSResponse dsrsp = CallService("SmartSchool.Student.Snapshop.InsertSnapshop", dsreq);     
        }

        [QueryRequest()]
        public static List<StudentSnapShop> GetSnapShop(params string[] refIdList)
        {
            if ( refIdList.Length == 0 )
                throw new Exception("至少需傳入一項");

            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "SnapshopID");
            helper.AddElement("Field", "RefStudentID");
            helper.AddElement("Field", "SnapshopName");
            helper.AddElement("Field", "PresentType");
            helper.AddElement("Field", "XmlContent");
            helper.AddElement("Field", "PresentContent");
            helper.AddElement("Field", "Version");
            helper.AddElement("Field", "SnapshopTime");
            helper.AddElement("Condition");
            foreach ( string id in refIdList )
            {
                helper.AddElement("Condition", "RefStudentID", id);
            }
            dsreq.SetContent(helper);
            DSResponse dsrsp = CallService("SmartSchool.Student.Snapshop.GetSnapshop", dsreq);
            List<StudentSnapShop> list = new List<StudentSnapShop>();
            foreach ( XmlElement element in dsrsp.GetContent().GetElements("Snapshop") )
            {
                DSXmlHelper h = new DSXmlHelper(element);
                StudentSnapShop newSnapShop = new StudentSnapShop();
                newSnapShop.PresentContent = h.GetText("PresentContent");
                newSnapShop.PresentType = h.GetText("PresentType");
                newSnapShop.RefStudentID = h.GetText("RefStudentID");
                newSnapShop.SnapshopName = h.GetText("SnapshopName");
                newSnapShop.XmlContent = h.GetElement("XmlContent");
                int.TryParse(h.GetText("Version"), out newSnapShop._Version);
                list.Add(newSnapShop);
            }
            return list;
        }
    }
}
