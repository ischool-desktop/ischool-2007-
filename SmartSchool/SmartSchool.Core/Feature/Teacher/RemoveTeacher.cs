using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Teacher
{
    [QueryRequest()]
    public class RemoveTeacher
    {
        public static void DeleteTeacher(string id)
        {
            DSRequest dsreq = new DSRequest("<UpdateRequest><Teacher><Field><Status>§R°£</Status></Field><Condition><ID>"+id+"</ID></Condition></Teacher></UpdateRequest>");
            DSResponse dsrsp = FeatureBase.CallService("SmartSchool.Teacher.Update", dsreq);
        }
    }
}
