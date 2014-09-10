using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool
{
    public class SendRequestException : Exception
    {
        private string _service_name;
        private DSRequest _request;

        public SendRequestException(string serviceName, DSRequest request, Exception innerException)
            : base("©I¥s¡u" + serviceName + "¡vªA°È¿ù»~¡C", innerException)
        {
            _service_name = serviceName;
            _request = request;
        }

        public string ServiceName
        {
            get { return _service_name; }
        }

        public DSRequest Request
        {
            get { return _request; }
        }

    }
}
