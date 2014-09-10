using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Student
{
        [QueryRequest()]
    public class QueryDiscipline : FeatureBase
    {
        public static DSResponse GetDiscipline(DSRequest request)
        {
            return CallService("SmartSchool.Student.Discipline.GetDiscipline", request);
        }

        public static DSResponse GetDemeritStatistic(DSRequest request)
        {
            return CallService("SmartSchool.Student.Discipline.GetDemeritStatistic", request);
        }

        public static DSResponse GetMeritStatistic(DSRequest request)
        {
            return CallService("SmartSchool.Student.Discipline.GetMeritStatistic", request);
        }

        public static DSResponse GetMeritIgnoreDemerit(DSRequest request)
        {
            return CallService("SmartSchool.Student.Discipline.GetMeritIgnoreDemerit", request);
        }

        public static DSResponse GetMeritIgnoreUnclearedDemerit(DSRequest request)
        {
            return CallService("SmartSchool.Student.Discipline.GetMeritIgnoreUnclearedDemerit", request);
        }
    }
}
