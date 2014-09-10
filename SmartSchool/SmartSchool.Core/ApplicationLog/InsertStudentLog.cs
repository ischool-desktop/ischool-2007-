using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ApplicationLog
{
    public class InsertStudentLog : LogInfoBase
    {
        private string _name = "";

        public InsertStudentLog(string entityId)
        {
            log_record.Entity = EntityTypeName.Get(EntityType.Student);
            log_record.Action = EntityActionName.Get(EntityAction.Insert);
            log_record.EntityID = entityId;
        }

        public string Source
        {
            get { return log_record.Source; }
            set { log_record.Source = value; }
        }

        public string StudentName
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Diagnostics
        {
            get { return log_record.Diagnostics; }
            set { log_record.Diagnostics = value; }
        }

        public override void FlushLog()
        {
            log_record.Description = "新學生姓名：" + StudentName;
        }
    }
}
