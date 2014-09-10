using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.AccessControl;


namespace SmartSchool.Feature.Student
{
    [FeatureCode("F009")]
    [QueryRequest()]
    public class QueryAttendance : FeatureBase
    {
        public static DSResponse GetAttendance(DSRequest request)
        {
            return CallService("SmartSchool.Student.Attendance.GetAttendance", request);
        }

        public static DSResponse GetAttendanceStatistic(DSRequest request)
        {
            return CallService("SmartSchool.Student.Attendance.GetAbsenceStatistic", request);
        }

        public static DSResponse GetNoAbsenceStatistic(DSRequest request)
        {
            return CallService("SmartSchool.Student.Attendance.GetNoAbsenceStatistic", request);
        }
    }
}
