using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ApplicationLog
{
    public class UpdateStudentLog : LogInfoBase
    {
        private Dictionary<string, FieldChange> _changes;
        private string _name = "";

        public UpdateStudentLog(string entityId)
        {
            log_record.Entity = EntityTypeName.Get(EntityType.Student);
            log_record.Action = EntityActionName.Get(EntityAction.Update);
            log_record.EntityID = entityId;

            _changes = new Dictionary<string, FieldChange>();
        }

        public void AddChangeField(string name, string originValue, string newValue)
        {
            _changes.Add(name, new FieldChange(originValue, newValue));
        }

        public string StudentName
        {
            get { return _name; }
            set { _name = value; }
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
            StringBuilder loginfo = new StringBuilder();
            loginfo.AppendLine("學生姓名：" + StudentName+" ");
            foreach (KeyValuePair<string, FieldChange> each in _changes)
            {
                FieldChange value = each.Value;
                loginfo.AppendLine("欄位「" + each.Key + "」由「" + value.OriginValue + "」變更為「" + value.NewValue + "」");
            }

            log_record.Description = loginfo.ToString();
        }

        private class FieldChange
        {
            public FieldChange(string originValue, string newValue)
            {
                OriginValue = originValue;
                NewValue = newValue;
            }

            public string OriginValue;

            public string NewValue;
        }
    }
}
