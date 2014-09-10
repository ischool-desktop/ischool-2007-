using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ApplicationLog
{
    public class GeneralActionLog : LogInfoBase
    {
        public GeneralActionLog()
        {
        }

        public string ActionName
        {
            get { return log_record.Action; }
            set { log_record.Action = value; }
        }

        public string Description
        {
            get { return log_record.Description; }
            set { log_record.Description = value; }
        }

        public string Source
        {
            get { return log_record.Source; }
            set { log_record.Source = value; }
        }

        public string Diagnostics
        {
            get { return log_record.Diagnostics; }
            set { log_record.Diagnostics = value; }
        }

        public override void FlushLog()
        {
        }
    }
}
