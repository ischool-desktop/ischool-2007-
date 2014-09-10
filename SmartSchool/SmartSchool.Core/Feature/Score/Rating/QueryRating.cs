using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.Feature.Score.Rating
{
    [QueryRequest()]
    public class QueryRating
    {
        public static DSXmlHelper GetSemesterEntryRating(bool classRating, bool deptRating, bool yearRating, params string[] studentIDList)
        {
            DSXmlHelper helper = new DSXmlHelper("GetSemesterEntryScore");
            DSRequest dsreq = new DSRequest();
            helper.AddElement("Field");
            helper.AddElement("Field", "ID");
            helper.AddElement("Field", "RefStudentId");
            helper.AddElement("Field", "SchoolYear");
            helper.AddElement("Field", "Semester");
            helper.AddElement("Field", "GradeYear");
            helper.AddElement("Field", "EntryGroup");
            if (classRating) helper.AddElement("Field", "ClassRating");
            if (deptRating) helper.AddElement("Field", "DeptRating");
            if (yearRating) helper.AddElement("Field", "YearRating");

            helper.AddElement("Condition");
            if (studentIDList.Length > 0)
            {
                helper.AddElement("Condition", "StudentIDList");
                foreach (string id in studentIDList)
                {
                    helper.AddElement("Condition/StudentIDList", "ID", id);
                }
            }
            helper.AddElement("Order");
            helper.AddElement("Order", "RefStudentId");
            helper.AddElement("Order", "SchoolYear");
            helper.AddElement("Order", "Semester");
            dsreq.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Score.GetSemesterEntryScore", dsreq);
            return dsrsp.GetContent();
        }

        public static DSXmlHelper GetSchoolYearEntryRating(bool classRating, bool deptRating, bool yearRating, params string[] studentIDList)
        {
            DSXmlHelper helper = new DSXmlHelper("GetSchoolYearEntryScore");
            DSRequest dsreq = new DSRequest();
            helper.AddElement("Field");
            helper.AddElement("Field", "ID");
            helper.AddElement("Field", "RefStudentId");
            helper.AddElement("Field", "SchoolYear");
            helper.AddElement("Field", "GradeYear");
            helper.AddElement("Field", "EntryGroup");
            if (classRating) helper.AddElement("Field", "ClassRating");
            if (deptRating) helper.AddElement("Field", "DeptRating");
            if (yearRating) helper.AddElement("Field", "YearRating");

            helper.AddElement("Condition");
            if (studentIDList.Length > 0)
            {
                helper.AddElement("Condition", "StudentIDList");
                foreach (string id in studentIDList)
                {
                    helper.AddElement("Condition/StudentIDList", "ID", id);
                }
            }
            helper.AddElement("Order");
            helper.AddElement("Order", "RefStudentId");
            helper.AddElement("Order", "SchoolYear");
            helper.AddElement("Order", "Semester");
            dsreq.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Score.GetSchoolYearEntryScore", dsreq);
            return dsrsp.GetContent();
        }
    }
}
