using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SmartSchool.Payment.AccountStatedModules
{
    class LogDailyCollection : List<LogDaily>
    {
        public LogDailyCollection()
        {
        }

        public static LogDailyCollection GetFTPDailies(FTPConnection connection, string[] enterpriseCodes)
        {
            //81850970718-daily-v40.txt
            Regex rx = new Regex(@"^\d{11}-daily-v40\.\w*$", RegexOptions.IgnoreCase);

            LogDailyCollection dailies = new LogDailyCollection();
            foreach (string each in connection.ListDirectory())
            {
                //只處理符合特定企業代碼的檔案。
                bool validfile = false;
                foreach (string eachCode in enterpriseCodes)
                    validfile |= each.StartsWith(eachCode);

                if (rx.IsMatch(each) && validfile)
                {
                    Stream stream = connection.DownloadFile(each);
                    LogDaily daily = new LogDaily(each, stream);
                    dailies.Add(daily);
                    stream.Close();
                }
            }

            return dailies;
        }

        public static void DeleteFTPDailies(FTPConnection connection, LogDailyCollection dailies)
        {
            foreach (LogDaily each in dailies)
                connection.DeleteFile(each.FileName);
        }
    }
}
