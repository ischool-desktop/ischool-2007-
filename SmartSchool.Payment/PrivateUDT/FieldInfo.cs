using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Private.UDT
{
    internal class FieldInfo
    {
        public string Name { get; set; }
        public DataType Type { get; set; }
        public PropertyInfo Target { get; set; }
        public bool Indexed { get; set; }
        public object Instance { get; set; }
    }
}
