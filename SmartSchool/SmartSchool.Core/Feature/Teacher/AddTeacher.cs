using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Teacher
{
    public class AddTeacher
    {
        public static string InsertTeacher(string name)
        {
            DSRequest dsreq = new DSRequest(
                "<InsertRequest><Teacher><Field>" +
                "<TeacherName>" + name + "</TeacherName>" +
                "</Field></Teacher></InsertRequest>");
            DSResponse dsrsp = FeatureBase.CallService("SmartSchool.Teacher.Insert", dsreq);
            return dsrsp.GetContent().GetElement("NewID").InnerText;
        }

        public static string InsertTeacher(string name, string nick)
        {
            DSRequest dsreq = new DSRequest(
                "<InsertRequest><Teacher><Field>" +
                "<TeacherName>" + name + "</TeacherName>" +
                "<Nickname>" + nick + "</Nickname>" +
                "</Field></Teacher></InsertRequest>");
            DSResponse dsrsp = FeatureBase.CallService("SmartSchool.Teacher.Insert", dsreq);
            return dsrsp.GetContent().GetElement("NewID").InnerText;
        }
    }
}
