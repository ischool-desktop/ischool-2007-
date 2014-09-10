using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ApplicationLog
{
    public enum EntityAction
    {
        Insert,
        Update,
        Delete,
        Query,
        Undefine
    }

    public static class EntityActionName
    {
        public static string Get(EntityAction action)
        {
            switch (action)
            {
                case EntityAction.Insert:
                    return "新增";
                case EntityAction.Update:
                    return "修改";
                case EntityAction.Delete:
                    return "刪除";
                case EntityAction.Query:
                    return "查詢";
                default:
                    return "未定議";
            }
        }
    }
}
