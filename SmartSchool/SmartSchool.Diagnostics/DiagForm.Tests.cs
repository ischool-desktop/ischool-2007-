using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace SmartSchool.Diagnostics
{
    partial class DiagForm
    {

/*

        void networkTest()
        {
            log("進行網路測試...", true);
            performTest(pingGatewayTest);
            performTest(dnsTest);
            if (hasDns && hasGateway)
                networkCanConnect = true;
            if (!networkCanConnect)
            {
                log("網路無法連接，將產生文字報表...", true);
                return;
            }
            performTest(pingHinetTest);
            performTest(pingYahooTest);
            performTest(pingBlazerTest);
            performTest(pingSiteTest);
        }

        void connectionTest() {
            log("進行伺服器診斷...", true);
        }
        string filename = "";
        void genXml()
        {
            log("產生XML報表...",true);
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            XmlTextWriter xml= XmlWriter.Create(filename + ".xml",setting);
            xml.WriteStartElement("result");
            //hostinfo
            xml.WriteStartElement("hostinfo");
            foreach (string key in new string[] { "hostname","domainname"})
            {
                xml.WriteElementString(key,data[key]);
            }
            xml.WriteEndElement();
            //hardware

            xml.WriteEndElement();
        }



        
        Dictionary<string, string> data = new Dictionary<string, string>();

        

        
        

        //tests


        TestResult pingHinetTest()
        {
            return doPing("www.hinet.net");
        }

        TestResult pingYahooTest()
        {
            return doPing("tw.yahoo.com");
        }

        TestResult pingBlazerTest()
        {
            try
            {
                if (Dns.GetHostEntry("dsns.blazer.org.tw").AddressList.Length <= 0)
                {
                    log("無法解析dsns.blazer.org.tw");
                    return TestResult.Fail;
                }
            }
            catch (Exception)
            {
                log("無法解析dsns.blazer.org.tw");
                return TestResult.Fail;
            }
            return doPing("dsns.blazer.org.tw");
        }

        TestResult pingSiteTest()
        {
            try
            {
                if (Dns.GetHostEntry("beta.smartschool.com.tw").AddressList.Length <= 0)
                {
                    log("無法解析beta.smartschool.com.tw");
                    return TestResult.Fail;
                }
            }
            catch (Exception)
            {
                log("無法解析beta.smartschool.com.tw");
                return TestResult.Fail;
            }
            return doPing("beta.smartschool.com.tw");
        }


        TestResult windowsSecurityTest()
        {
            return TestResult.Pass;
        }


*/
    }
}
