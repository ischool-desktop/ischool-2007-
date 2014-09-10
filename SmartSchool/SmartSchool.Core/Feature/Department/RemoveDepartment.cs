using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Department
{
    [QueryRequest()]
    public class RemoveDepartment
    {
        public static void Delete(DSRequest request)
        {
            FeatureBase.CallService("SmartSchool.Department.Delete", request);
        }
    }
}
