using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Feature.Class;
using SmartSchool.Feature.Teacher;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.ExamTemplate;
using SmartSchool.ImportSupport;

namespace SmartSchool.CourseRelated.RibbonBars.Import
{
    internal class ImportDataAccess : IDataAccess
    {
        public XmlElement GetImportFieldList()
        {
            return SmartSchool.Feature.Course.CourseBulkProcess.GetImportDescription();
        }

        public XmlElement GetValidateFieldRule()
        {
            return SmartSchool.Feature.Course.CourseBulkProcess.GetFieldValidationRule();
        }

        public XmlElement GetUniqueFieldData()
        {
            return SmartSchool.Feature.Course.CourseBulkProcess.GetPrimaryKeyList();
        }

        public XmlElement GetShiftCheckList(params string[] fieldNameList)
        {
            return SmartSchool.Feature.Course.CourseBulkProcess.GetShiftCheckList(fieldNameList);
        }

        public void InsertImportData(XmlElement data)
        {
            SmartSchool.Feature.Course.CourseBulkProcess.InsertImportCourse(data);
        }

        public void UpdateImportData(XmlElement data)
        {
            SmartSchool.Feature.Course.CourseBulkProcess.UpdateImportCourse(data);
        }

        public void AddCourseTeachers(XmlElement request)
        {
            SmartSchool.Feature.Course.EditCourse.AddCourseTeacher(new DSXmlHelper(request));
        }

        public void RemoveCourseTeachers(XmlElement request)
        {
            SmartSchool.Feature.Course.EditCourse.RemoveCourseTeachers(new DSXmlHelper(request));
        }

        public XmlElement GetCourseTeachers(IEnumerable<string> fieldNameList)
        {
            return SmartSchool.Feature.Course.CourseBulkProcess.GetCourseTeachers(fieldNameList);
        }
    }
}
