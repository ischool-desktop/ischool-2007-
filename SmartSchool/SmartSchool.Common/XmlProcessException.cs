using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Common
{
    /// <summary>
    /// �N�� Xml �B�z���~�C
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
