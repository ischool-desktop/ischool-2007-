using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SmartSchool.Diagnostics
{
    public partial class HostinfoTestSuite: TestSuite
    {
        public HostinfoTestSuite(StreamWriter txtfile,XmlWriter xmlfile): base("Hostinfo",txtfile,xmlfile){ }
        public override void RunTests()
        {
            log("蒐集使用者及主機資訊...");
            performTest(hostbasicTest);
            performTest(hardwareTest);
            performTest(processTest);
            performTest(listenerTest);    
            performTest(nicTest);
            performTest(pingGatewayTest);
            performTest(dnsTest);
            if (canDnsAccess && canGatewayAccess)
                NetworkCanConnect = true;
        }


    }
}
