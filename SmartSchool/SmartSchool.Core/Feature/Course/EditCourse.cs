using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Course
{
    public class EditCourse
    {
        public static void InsertSCEScore(DSRequest request)
        {
            FeatureBase.CallService("SmartSchool.Course.InsertSECScore", request);
        }

        [QueryRequest()]
        public static void UpdateSCEScore(DSRequest request)
        {
            FeatureBase.CallService("SmartSchool.Course.UpdateSCEScore", request);
        }

        [QueryRequest()]
        public static void DeleteSCEScore(DSRequest request)
        {
            FeatureBase.CallService("SmartSchool.Course.DeleteSCEScore", request);
        }

        [QueryRequest()]
        public static void UpdateCourse(DSRequest request)
        {
            FeatureBase.CallService("SmartSchool.Course.Update", request);
        }

        [QueryRequest()]
        public static void UpdateAttend(DSXmlHelper request)
        {
            FeatureBase.CallService("SmartSchool.Course.UpdateSCAttend", new DSRequest(request));
        }

        [QueryRequest()]
        public static void DeleteAttend(DSXmlHelper request)
        {
            FeatureBase.CallService("SmartSchool.Course.DeleteSCAttend", new DSRequest(request));
        }

        [QueryRequest()]
        public static void RemoveCourseTeachers(string courseId)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Course");
            helper.AddElement("Course", "CourseID", courseId);

            FeatureBase.CallService("SmartSchool.Course.RemoveCourseTeacher", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void RemoveCourseTeachers(string courseId,string teacherId)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Course");
            helper.AddElement("Course", "CourseID", courseId);
            helper.AddElement("Course", "RefTeacherID", teacherId);

            FeatureBase.CallService("SmartSchool.Course.RemoveCourseTeacher", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void RemoveCourseTeachers(DSXmlHelper request)
        {
            FeatureBase.CallService("SmartSchool.Course.RemoveCourseTeacher", new DSRequest(request));
        }

        public static void AddCourseTeacher(DSXmlHelper request)
        {
            FeatureBase.CallService("SmartSchool.Course.AddCourseTeacher", new DSRequest(request));
        }
    }
}
