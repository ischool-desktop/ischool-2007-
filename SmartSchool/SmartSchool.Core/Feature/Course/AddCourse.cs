using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Feature.Course
{
    public class AddCourse
    {
        public static string Insert(string courseName, string schoolYear, string semester)
        {
            DSXmlHelper helper = new DSXmlHelper("InsertRequest");
            helper.AddElement("Course");
            helper.AddElement("Course", "CourseName", courseName);
            helper.AddElement("Course", "SchoolYear", schoolYear);
            helper.AddElement("Course", "Semester", semester);

            //helper.AddElement("Course", "Subject", subject);
            //helper.AddElement("Course", "SubjectLevel", level);

            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.Insert", dsreq);

            //之前回傳 int
            //return int.Parse(rsp.GetContent().GetElement("NewID").InnerText);

            return rsp.GetContent().GetElement("NewID").InnerText;
        }

        /// <summary>
        /// 開課時用到。
        /// </summary>
        public static void Insert(List<InsertCourse > courses)
        {
            if ( courses.Count == 0 ) return;
            DSXmlHelper helper = new DSXmlHelper("InsertRequest");
            foreach ( InsertCourse course in courses )
            {
                DSXmlHelper helper2 = new DSXmlHelper("Course");
                helper2.AddElement(".", "CourseName", course.CourseName);
                helper2.AddElement(".", "Subject", course.Subject);
                helper2.AddElement(".", "SubjectLevel", course.Level);
                helper2.AddElement(".", "RefClassID", course.RefClassID);
                helper2.AddElement(".", "SchoolYear", course.SchoolYear);
                helper2.AddElement(".", "Semester", course.Semester);
                helper2.AddElement(".", "Credit", course.Credit);
                helper2.AddElement(".", "NotIncludedInCredit", course.NotIncludedInCredit);
                helper2.AddElement(".", "NotIncludedInCalc", course.NotIncludedInCalc);
                helper2.AddElement(".", "ScoreType", course.Entry);
                helper2.AddElement(".", "IsRequired", course.Required);
                helper2.AddElement(".", "RequiredBy", course.RequiredBy);

                helper.AddElement(".", helper2.BaseElement);
            }
            //helper.AddElement("Course");
            //helper.AddElement("Course", "CourseName", courseName);
            //helper.AddElement("Course", "Subject", subject);
            //helper.AddElement("Course", "SubjectLevel", level);
            //helper.AddElement("Course", "RefClassID", refClassID);
            //helper.AddElement("Course", "SchoolYear", schoolYear);
            //helper.AddElement("Course", "Semester", semester);
            //helper.AddElement("Course", "Credit", credit);
            //helper.AddElement("Course", "NotIncludedInCredit", notIncludedInCredit);
            //helper.AddElement("Course", "NotIncludedInCalc", notIncludedInCalc);
            //helper.AddElement("Course", "ScoreType", scoreType);
            //helper.AddElement("Course", "IsRequired", required);
            //helper.AddElement("Course", "RequiredBy", requiredby);

            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.Insert", dsreq);

            //return int.Parse(rsp.GetContent().GetElement("NewID").InnerText);
        }

        public class InsertCourse
        {
            public string CourseName;
            public string Subject;
            public string Level;
            public string RefClassID;
            public string SchoolYear;
            public string Semester;
            public string Credit;
            public string NotIncludedInCredit;
            public string NotIncludedInCalc;
            public string Entry;
            public string Required;
            public string RequiredBy;
            public InsertCourse(string courseName, string subject, string level, string refClassID, string schoolYear,
            string semester, string credit, string notIncludedInCredit, string notIncludedInCalc, string entry, string required, string requiredby)
            {
                CourseName = courseName;
                Subject = subject;
                Level = level;
                RefClassID = refClassID;
                SchoolYear = schoolYear;
                Semester = semester;
                Credit = credit;
                NotIncludedInCredit = notIncludedInCredit;
                NotIncludedInCalc = notIncludedInCalc;
                Entry = entry;
                Required = required;
                RequiredBy = requiredby;
            }
        }

        public static void AttendCourse(DSXmlHelper request)
        {
            FeatureBase.CallService("SmartSchool.Course.InsertSCAttend", new DSRequest(request));
        }
    }

}
