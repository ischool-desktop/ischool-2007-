using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature
{
    [QueryRequest()]
    public class RemoveStudent : FeatureBase
    {
        public static void DeleteStudent(string id)
        {
            DSRequest dsreq = new DSRequest("<ChangeStatusRequest><Student><Field><Status>§R°£</Status></Field><Condition><ID>" + id + "</ID></Condition></Student></ChangeStatusRequest>");
            DSResponse dsrsp = CallService("SmartSchool.Student.ChangeStatus", dsreq);
        }
    }
}
