using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Department
{
    [QueryRequest()]
    public class QueryDepartment
    {
        public static DSResponse GetAbstractList()
        {
            return FeatureBase.CallService("SmartSchool.Department.GetAbstractList", new DSRequest());
        }

        public static DSResponse GetUsedDepartment()
        {
            return FeatureBase.CallService("SmartSchool.Department.GetUsedDepartment", new DSRequest());
        }
    }
}
