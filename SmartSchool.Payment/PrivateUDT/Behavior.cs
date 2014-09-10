using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace Private.UDT
{
    public delegate DSResponse CallServiceDelegate(string service,DSRequest req);

    public class Behavior
    {
        private static Behavior _Instance = null;
        public static Behavior Instance { get; set; }

        private CallServiceDelegate _call_service;
        public Behavior(CallServiceDelegate callService)
        {
            _call_service = callService;
        }

        public DSResponse CallService(string service, DSRequest req)
        {
            return _call_service.Invoke(service, req);
        }
    }
}
