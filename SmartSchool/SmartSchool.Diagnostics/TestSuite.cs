using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;

namespace SmartSchool.Diagnostics
{
    abstract public class TestSuite
    {
        public StreamWriter TxtFile;
        public XmlWriter XmlFile;
        public string Name;
        public TestSuite(string name,StreamWriter txtfile,XmlWriter xmlfile)
        {
            this.XmlFile = xmlfile;
            this.TxtFile = txtfile;
            this.Name = name;
            insertGroup(this.Name);
            log("[" + DateTime.Now + "]\t進行TestSuite" );
            this.RunTests();
            insertEnd();
            XmlFile.Flush();
        }
        abstract public void RunTests();

        //protected
        protected  enum TestResult { Pass, Warn, Fail };
        protected  delegate TestResult PerformTestMethod();

        protected void performTest(PerformTestMethod testmethod)
        {
            TestResult result = TestResult.Pass;
            log("[" + DateTime.Now + "]\t進行" + testmethod.Method.Name);
            insertGroup(testmethod.Method.Name);
            insertValue("PerformTime", DateTime.Now.ToString());
            try
            {
                result = testmethod.Invoke();
            }
            catch (Exception e)
            {
                result = TestResult.Fail;
                log(e.ToString());
                insertValue("Result", result.ToString());
            }
            insertValue("Result", result.ToString());
            insertEnd();
        }

        //functions
        protected const int PING_MAX = 20;

        protected TestResult doPing(string hostname)
        {
            Ping ping = new Ping();
            insertGroup("PingTest");
            insertValue("Hostname", hostname);
            log("嘗試ping " + hostname+"...");
            int success = 0, fail = 0;
            List<long> rtt = new List<long>();
            try
            {
                for (int i = 0; i < PING_MAX; i++)
                {
                    PingReply reply = ping.Send(hostname, 120);
                    Thread.Sleep(10);
                    if (reply.Status == IPStatus.Success)
                    {
                        rtt.Add(reply.RoundtripTime);
                        success++;
                    }
                    else
                    {
                        fail++;
                    }
                    insertEnd();
                }
                if (success > fail)
                    if (success == PING_MAX)
                        insertValue("Status", "OK");
                    else
                        insertValue("Status", "Warn");
                else
                    insertValue("Status", "Fail");
                insertValue("Success", success + "");
                log("Success\t" + success + "");
                insertValue("Fail", fail + "");
                log("Fail\t" + fail + "");
                rtt.Sort();
                insertValue("MinRTT", rtt[0] + "");
                insertValue("MaxRTT", rtt[rtt.Count - 1] + "");
                long sum = 0;
                foreach (long value in rtt)
                    sum += value;
                insertValue("AvgRTT", sum / (PING_MAX - fail) + "");
                log("Avg RTT\t" + sum / (PING_MAX - fail));
            }
            catch (Exception)
            {
                log("無法傳送ICMP封包或者是" + hostname + "沒有回應");
                return TestResult.Pass;
            }
            insertEnd();
            if (success == PING_MAX)
                return TestResult.Pass;
            else
            {
                if (fail < PING_MAX)
                    return TestResult.Warn;
                else
                    return TestResult.Fail;
            }
        }

        protected TestResult doDns(string domainname)
        {
            insertGroup("DnsTest");
            insertValue("DomainName", domainname);
            log("解析" + domainname + "...");
            try
            {
                IPAddress[] addresses= Dns.GetHostEntry(domainname).AddressList;
                foreach (IPAddress address in addresses)
                {
                    insertValue("Address", address.ToString());
                    log(address.ToString());
                }
                if (addresses.Length <= 0)
                {
                    log("解析" + domainname + "失敗，沒有清單傳回");
                    insertValue("Status", "NoAddresses");
                    return TestResult.Fail;
                }

            }
            catch (Exception)
            {
                log("解析" + domainname + "失敗");
                insertValue("Status", "Exception");
                return TestResult.Fail;
            }
            insertEnd();
            return TestResult.Pass;
        }

        protected TestResult doHTTP(string url)
        {
            insertGroup("HTTPTest");
            insertValue("URL", url);
            try
            {
                //實際
                log("建立" + url + "的HTTP連線");
                WebClient client = new WebClient();
                string result = client.DownloadString(url);
                log("回應"+result.Length);
                if (result.Length == 0)
                    return TestResult.Fail;
            }
            catch (Exception)
            {
                log("連線至"+ url + "失敗");
                return TestResult.Fail;
            }
            insertEnd();
            return TestResult.Pass;
        }

        //
        protected void insertGroup(string name)
        {
            XmlFile.WriteStartElement(name);
        }

        protected void insertEnd()
        {
            XmlFile.WriteEndElement();
        }

        protected void insertValue(string key, string value)
        {
            XmlFile.WriteElementString(key, value);
        }

        protected void log(string message)
        {
            TxtFile.WriteLine(message);
        }


    }
}
