using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Common
{
    /// <summary>
    /// 代表 Xml 處理錯誤。
    /// </summary>
    public class XmlProcessException : Exception
    {
        public XmlProcessException(string message)
            : base(message)
        {
        }

        public XmlProcessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
