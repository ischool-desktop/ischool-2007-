using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.ApplicationLog;
using SmartSchool.Common;
using SmartSchool.ExceptionHandler;

namespace SmartSchool.StudentRelated.Palmerworm
{
    internal class LogUtility
    {
        public static void LogChange(DataValueManager valueManager, string runningID,string name)
        {
            try
            {
                UpdateStudentLog log = new UpdateStudentLog(runningID.ToString());
                log.StudentName = name;
                foreach (KeyValuePair<string, string> each in valueManager.GetDirtyItems())
                {
                    string displayName = valueManager.GetDisplayText(each.Key);
                    string oldValue = valueManager.GetOldValue(each.Key);
                    log.AddChangeField(displayName, oldValue, each.Value);
                }

                CurrentUser.Instance.AppLog.Write(log);
            }
            catch (Exception ex)
            {
                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
            }
        }
    }
}
