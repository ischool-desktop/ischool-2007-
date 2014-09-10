using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using SmartSchool.AccessControl;

namespace SmartSchool.Feature
{
    [FeatureCode("F009")]
    public class AddStudent : FeatureBase
    {
        public static string InsertStudent(string name)
        {
            DSRequest dsreq = new DSRequest("<InsertRequest><Student><Field> " +
                "<Name>" + name + "</Name> "+
                "</Field></Student></InsertRequest>");
            DSResponse dsrsp = CallService("SmartSchool.Student.Insert", dsreq);
            return dsrsp.GetContent().GetElement("NewID").InnerText;
        }
    }
}
