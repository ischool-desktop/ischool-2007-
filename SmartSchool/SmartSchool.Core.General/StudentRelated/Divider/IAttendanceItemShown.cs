using System;
namespace SmartSchool.StudentRelated.Divider
{
    interface IAttendanceItemShown
    {
        bool Shown(BriefStudentData student, SmartSchool.API.StudentExtension.Attendance attendance);
    }
}
