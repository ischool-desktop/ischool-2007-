using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Feature.Survey;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Survey
{
    public class SurveyData
    {
        private string _identity;
        public string Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _school_year;
        public string SchoolYear
        {
            get { return _school_year; }
            set { _school_year = value; }
        }

        private string _semester;
        public string Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }

        private string _expireation;
        public string Expireation
        {
            get { return _expireation; }
            set { _expireation = value; }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        private SurveyeeType _surveyeeType;
        public SurveyeeType SurveyeeType
        {
            get { return _surveyeeType; }
            set { _surveyeeType = value; }
        }

        private XmlElement _questions;
        public XmlElement Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }

        public static SurveyData LoadData(string identity)
        {
            DSXmlHelper survey = QuerySurvey.GetDetailList(identity);
            SurveyData sd = new SurveyData();

            sd.Identity = survey.GetText("Survey/@ID");
            sd.Name = survey.GetText("Survey/Name");
            sd.SchoolYear = survey.GetText("Survey/SchoolYear");
            sd.Semester = survey.GetText("Survey/Semester");
            sd.Comment = survey.GetText("Survey/Comment");
            sd.Questions = survey.GetElement("Survey/Content/Questions");
            sd.Expireation = ToDisplayDate(survey.GetText("Survey/Expireation"));
            sd.SurveyeeType = ParseSurveyeeType(survey.GetText("Survey/SurveyeeType"));

            return sd;
        }

        public override string ToString()
        {
            return Name;
        }

        public static string GetSurveyeeTypeName(SurveyeeType surveyeeType)
        {
            if (surveyeeType == SurveyeeType.Class)
                return "班級";
            else if (surveyeeType == SurveyeeType.ClassStudent)
                return "班級學生";
            else if (surveyeeType == SurveyeeType.Course)
                return "課程";
            else if (surveyeeType == SurveyeeType.CourseStudent)
                return "課程學生";
            else if (surveyeeType == SurveyeeType.Student)
                return "學生";
            else if (surveyeeType == SurveyeeType.Teacher)
                return "教師";
            else
                return "未定議";
        }

        public static SurveyeeType ParseSurveyeeType(string surveyeeTypeString)
        {
            if (surveyeeTypeString == "Class")
                return SurveyeeType.Class;
            else if (surveyeeTypeString == "ClassStudent")
                return SurveyeeType.ClassStudent;
            else if (surveyeeTypeString == "Course")
                return SurveyeeType.Course;
            else if (surveyeeTypeString == "CourseStudent")
                return SurveyeeType.CourseStudent;
            else if (surveyeeTypeString == "Student")
                return SurveyeeType.Student;
            else if (surveyeeTypeString == "Teacher")
                return SurveyeeType.Teacher;
            else
                return SurveyeeType.None;
        }

        public static string ToDisplayDate(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
                return string.Empty;

            return dateTimeString.Split(' ')[0].Replace("-", "/");
        }
    }
}
