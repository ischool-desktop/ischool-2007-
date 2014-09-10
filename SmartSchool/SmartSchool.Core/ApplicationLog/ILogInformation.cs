using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.ApplicationLog
{
    public interface ILogInformation
    {
        bool IsUpload { get; set;}

        void Flush();

        LogRecord Content { get;}
    }
}
