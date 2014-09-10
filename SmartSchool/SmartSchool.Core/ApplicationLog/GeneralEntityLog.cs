using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.ApplicationLog
{
    public class GeneralEntityLog : LogInfoBase
    {
        public GeneralEntityLog()
        {
        }

        public string ActionName
        {
            get { return log_record.Action; }
            set { log_record.Action = value; }
        }

        public string EntityName
        {
            get { return log_record.Entity; }
            set { log_record.Entity = value; }
        }

        public string EntityID
        {
            get { return log_record.EntityID; }
            set { log_record.EntityID = value; }
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
