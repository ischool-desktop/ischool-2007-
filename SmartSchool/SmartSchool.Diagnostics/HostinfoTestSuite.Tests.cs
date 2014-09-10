using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;
using System.Net;

namespace SmartSchool.Diagnostics
{
    public partial class HostinfoTestSuite
    {
        IPGlobalProperties hostprop = IPGlobalProperties.GetIPGlobalProperties();
        TestResult hostbasicTest()
        {
            log("取得主機及使用者資訊");
            insertValue("Hostname", hostprop.HostName);
            insertValue("Domainname", hostprop.DomainName);
            insertValue("OS", Environment.OSVersion + "");
            return TestResult.Pass;
        }

        string defaultGateway = "";
        bool hasGateway = false;
        bool canGatewayAccess = false;

        string defaultDns = "";
        bool hasDns = false;
        bool canDnsAccess = false;

        public static bool NetworkCanConnect = false;
        
        TestResult nicTest()
        {
            log("取得網路介面資訊");
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string id = nic.Name.Replace(' ', '_');
                insertGroup(id);
                insertValue("Name", nic.Name);
                insertValue("Description", nic.Description);
                insertValue("Type", nic.NetworkInterfaceType + "");
                insertValue("MAC", nic.GetPhysicalAddress() + "");
                insertValue("Status", nic.OperationalStatus + "");
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
                insertValue("Speed", nic.Speed + "");
                if (nic.Supports(NetworkInterfaceComponent.IPv4) == true)
                {
                    IPInterfaceProperties info = nic.GetIPProperties();
                    insertValue("DnsSuffix", info.DnsSuffix);
                    insertValue("DnsEnabled", info.IsDnsEnabled + "");
                    insertValue("DnsDynamic", info.IsDynamicDnsEnabled + "");

                    for (int i = 0; i < info.DnsAddresses.Count; i++)
                    {
                        insertValue("Dns_" + i, info.DnsAddresses[i].ToString());
                        if (i == 0)
                        {
                            defaultDns = info.DnsAddresses[i].ToString();
                            hasDns = true;
                        }
                    }
                    for (int i = 0; i < info.GatewayAddresses.Count; i++)
                    {
                        insertValue("Dateway_" + i, info.GatewayAddresses[i].Address.ToString());
                        if (i == 0)
                        {
                            defaultGateway = info.GatewayAddresses[i].Address.ToString();
                            hasGateway = true;
                        }
                    }
                }
                insertEnd();
            }
            return TestResult.Pass;
        }

        TestResult hardwareTest()
        {
            ManagementObjectSearcher searcher;
            //cpu
            searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                string id = obj["DeviceID"].ToString();
                insertGroup(id);
                insertValue("Name", obj["Name"].ToString());
                insertValue("LogicalProcessors", obj["NumberOfLogicalProcessors"].ToString());
                insertValue("Clock", obj["CurrentClockSpeed"].ToString());
                insertEnd();
            }

            //ram
            searcher = new ManagementObjectSearcher("select * from Win32_LogicalMemoryConfiguration");
            foreach (ManagementObject obj in searcher.Get())
            {
                string id = obj["Name"].ToString();
                insertGroup(id);
                insertValue( "TotalPage", obj["TotalPageFileSpace"].ToString());
                insertValue( "TotalPhy", obj["TotalPhysicalMemory"].ToString());
                insertValue( "TotalVirt", obj["TotalVirtualMemory"].ToString());
                insertEnd();
            }
            //TODO: minimal system requirement

            return TestResult.Pass;
        }

        TestResult processTest()
        {
            log("取得程序資訊");
            foreach (Process p in Process.GetProcesses())
            {
                insertGroup("Process");
                insertValue("Name", p.ProcessName);
                insertValue("WorkingSet", p.WorkingSet64+"");
                insertEnd();
            }
            return TestResult.Pass;
        }

        TestResult listenerTest()
        {
            log("取得TCP及UDP監聽狀態");
            IPEndPoint[] tcpips = hostprop.GetActiveTcpListeners();
            IPEndPoint[] udpips = hostprop.GetActiveUdpListeners();
            insertGroup("TCP");
            foreach (IPEndPoint p in tcpips)
            {
                insertGroup("Listener");
                insertValue("IP", p.Address + "");
                insertValue("Port", p.Port + "");
                insertEnd();
            }
            insertEnd();
            insertGroup("UDP");
            foreach (IPEndPoint p in udpips)
            {
                insertGroup("Listener");
                insertValue("IP", p.Address + "");
                insertValue("Port", p.Port + "");
                insertEnd();
            }
            insertEnd();
            return TestResult.Pass;
        }

        TestResult pingGatewayTest()
        {
            if (hasGateway)
            {
                doPing(defaultGateway);

                //進行ICMP測試
                bool pingfail = false;
                if (doPing("168.95.1.1") == TestResult.Fail)
                    pingfail = true;

                //進行TCP(HTTP)測試
                int httpfail = 0;
                foreach (string url in new string[] { "http://209.85.175.99", "http://61.219.38.89", "http://119.160.246.241" })
                {
                    if (doHTTP(url) == TestResult.Fail) httpfail++;
                }
                if (pingfail && httpfail >= 3)
                {
                    log("Gateway存在但很有可能因為防火牆或內部網路問題而無法通過，請洽網管人員");
                    return TestResult.Fail;
                }
                canGatewayAccess = true;
            }
            else
            {
                log("無法取得Default Gateway，網路卡或網路連線設定不正常！");
                return TestResult.Fail;
            }
            
            return TestResult.Pass;
        }

        TestResult dnsTest()
        {
            if (hasDns)
            {
                doPing(defaultDns);
                int fail = 0;

                if (doDns("www.hinet.net") == TestResult.Fail) fail++;
                if (doDns("beta.smartschool.com.tw") == TestResult.Fail) fail++;
                if (fail == 0)
                {
                    hasDns = true;
                    canDnsAccess = true;
                }
            }
            else
            {
                log("無法取得DNS Server，網路卡或網路連線設定不正常！");
                return TestResult.Fail;
            }
            return TestResult.Pass;
        }

    }
}
