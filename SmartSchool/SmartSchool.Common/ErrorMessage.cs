using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;

namespace SmartSchool.Common
{
    internal class Replace
    {
        public readonly string Name;
        public readonly string Value;

        public Replace(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    internal static class ErrorMessage
    {
        private static ResourceManager rm;

        static ErrorMessage()
        {
            rm = MessageResource.ResourceManager;
        }

        public static string Get(string msgName)
        {
            return rm.GetString(msgName);
        }

        public static string Get(string msgName, params Replace[] replaces)
        {
            string msg = Get(msgName);

            foreach (Replace rep in replaces)
                msg = msg.Replace("<%" + rep.Name + "%>", rep.Value);

            return msg;
        }
    }
}
