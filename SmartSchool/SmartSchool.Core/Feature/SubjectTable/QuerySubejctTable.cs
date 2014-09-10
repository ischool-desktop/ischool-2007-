using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.SubjectTable
{
    [QueryRequest()]
    public class QuerySubejctTable
    {
        public static DSResponse GetSubejctTableList(string catalog)
        {
            return FeatureBase.CallService("SmartSchool.SubjectTable.GetDetailList", new DSRequest("<Request><Condition><Catalog>" + catalog + "</Catalog></Condition></Request>"));
        }
    }
}
