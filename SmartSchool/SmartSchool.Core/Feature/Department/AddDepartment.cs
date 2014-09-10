using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Department
{
    public class AddDepartment
    {
        public static void Insert(DSRequest request)
        {
            FeatureBase.CallService("SmartSchool.Department.Insert", request);
        }
    }
}
