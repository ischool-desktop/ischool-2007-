using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Course
{
    [QueryRequest()]
    public class QueryCourse
    {
        //public static DSXmlHelper GetAllCourse()
        //{
        //    DSXmlHelper helper = new DSXmlHelper("GetDetailListRequest");
        //    helper.AddElement("Field");
        //    helper.AddElement("Field", "All");
        //    DSRequest dsreq = new DSRequest(helper);
        //    DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetDetailList", dsreq);

        //    return rsp.GetContent();
        //}

        public static DSXmlHelper GetCourseBySemester(int schoolyear, int semester)
        {
            DSXmlHelper helper = new DSXmlHelper("GetDetailListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");

            if (schoolyear == -1 || semester == -1) //查未定學年度的
            {
                helper.AddElement("Condition", "Or");
                helper.AddElement("Condition/Or", "SchoolYear");
                helper.AddElement("Condition/Or", "Semester");
            }
            else
            {
                helper.AddElement("Condition", "SchoolYear", schoolyear.ToString());
                helper.AddElement("Condition", "Semester", semester.ToString());
            }

            helper.AddElement("Order");
            helper.AddElement("Order", "CourseName");
            helper.AddElement("Order", "ID");
            helper.AddElement("Order", "Sequence");

            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetDetailList", dsreq);

            return rsp.GetContent();
        }

        /// <summary>
        /// 群組資料庫中所有課程的學年度與學期。
        /// </summary>
        public static DSXmlHelper GetAllSemesterList()
        {
            DSXmlHelper helper = new DSXmlHelper("GetAllSemesterListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetAllSemesterList", dsreq);

            return rsp.GetContent();
        }

        public static DSResponse GetCourseDetail(params int[] courseid)
        {
            DSXmlHelper helper = new DSXmlHelper("GetDetailListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");

            foreach (int each in courseid)
                helper.AddElement("Condition", "ID", each.ToString());

            helper.AddElement("Order");
            helper.AddElement("Order", "CourseName");
            helper.AddElement("Order", "ID");
            helper.AddElement("Order", "Sequence");

            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetDetailList", dsreq);
            return rsp;
        }

        public static DSResponse GetClassCourse(int schoolYear, int semester, params string[] classIDList)
        {
            DSXmlHelper helper = new DSXmlHelper("GetDetailListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "RefClassIDList");
            foreach (string id in classIDList)
            {
                helper.AddElement("Condition/RefClassIDList", "RefClassID", id);
            }
            helper.AddElement("Condition", "SchoolYear", "" + schoolYear);
            helper.AddElement("Condition", "Semester", "" + semester);
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetDetailList", dsreq);
            return rsp;
        }

        public static DSResponse GetCourseExam(params string[] courseid)
        {
            if ( courseid.Length == 0 ) throw new Exception("必須傳入至少一筆課程");
            DSXmlHelper helper = new DSXmlHelper("GetScoreTypeRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach ( string id in courseid )
            {
                helper.AddElement("Condition", "ID", id );
            }
            helper.AddElement("Order");
            helper.AddElement("Order", "DisplayOrder");
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetScoreType", dsreq);
            return rsp;
        }

        public static DSResponse GetSCAttend(params string[] courseid)
        {
            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string var in courseid)
            {
                helper.AddElement("Condition", "CourseID", var);
            }
            helper.AddElement("Order");
            helper.AddElement("Order", "ClassName", "ASC");
            helper.AddElement("Order", "SeatNumber", "ASC");
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetSCAttend", dsreq);
            return rsp;
        }

        public static DSResponse GetSCAttendBrief(params string[] courseid)
        {
            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "ID");
            helper.AddElement("Field", "RefStudentID");
            helper.AddElement("Field", "RefCourseID");
            helper.AddElement("Field", "ClassName");
            helper.AddElement("Field", "StudentNumber");
            helper.AddElement("Field", "SeatNumber");
            helper.AddElement("Field", "Name");
            helper.AddElement("Field", "Score");
            helper.AddElement("Condition");
            foreach (string var in courseid)
            {
                helper.AddElement("Condition", "CourseID", var);
            }
            helper.AddElement("Order");
            helper.AddElement("Order", "ClassName", "ASC");
            helper.AddElement("Order", "SeatNumber", "ASC");
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetSCAttend", dsreq);
            return rsp;
        }

        public static DSResponse GetSCAttend(DSRequest dsreq)
        {
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetSCAttend", dsreq);
            return rsp;
        }

        public static DSResponse GetSECTake(params string[] courseid)
        {
            DSXmlHelper helper = new DSXmlHelper("ScoreRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string id in courseid)
            {
                helper.AddElement("Condition", "CourseID", id);
            }
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetSCETake", dsreq);
            return rsp;
        }

        public static DSResponse GetSECTake(List<string> courseid,List<string> studentid)
        {
            DSXmlHelper helper = new DSXmlHelper("ScoreRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach ( string id in courseid )
            {
                helper.AddElement("Condition", "CourseID", id);
            }
            foreach ( string id in studentid )
            {
                helper.AddElement("Condition", "StudentID", id);
            }
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetSCETake", dsreq);
            return rsp;
        }

        public static DSResponse GetExamList()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Exam.GetAbstractList", dsreq);
            return rsp;
        }

        public static DSResponse GetCourseById(params string[] courseId)
        {
            DSXmlHelper helper = new DSXmlHelper("GetDetailListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string id in courseId)
            {
                helper.AddElement("Condition", "ID", id);
            }
            DSRequest dsreq = new DSRequest(helper);
            DSResponse rsp = FeatureBase.CallService("SmartSchool.Course.GetDetailList", dsreq);
            return rsp;
        }

        public static DSResponse GetExamMapping(DSRequest dSRequest)
        {
            return FeatureBase.CallService("SmartSchool.Course.GetExam", dSRequest);
        }
    }
}
