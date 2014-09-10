using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Common
{
    public class ServerFailException : Exception
    {
        public ServerFailException(string message, Exception InnerException)
            : base(message, InnerException)
        {
        }
    }
}
