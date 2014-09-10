using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using IntelliSchool.DSA30.Util;
using System.Diagnostics;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Threading;
using SmartSchool.Common;

namespace SmartSchool.ExceptionHandler
{
    public partial class BugReporter : Form
    {
        private static string _System="", _Version = "";

        public static void SetSystem(string name) { _System = name; }

        public static void SetVersion(string version) { _Version = version; }

        public static void SetRunTimeMessage(string field, string msg)
        {
            if ( _RunTimeMessages.ContainsKey(field) )
                _RunTimeMessages[field] = msg;
            else
                _RunTimeMessages.Add(field, msg);
        }

        public static void ReportException(string system, string version, Exception exception, bool show)
        {
            if (!FilterThreadAbortException(exception))
            {
                BugReporter exceptionForm = null;
                try
                {
                    exceptionForm = new BugReporter(exception, system, version,show);
                }
                catch
                { }
                try
                {
                    if (show)
                    {
                        if (exceptionForm != null)
                            exceptionForm.ShowDialog();
                        else
                            MsgBox.Show("系統發生錯誤，嘗試自動回報失敗。");
                    }
                }
                catch
                {
                    MsgBox.Show("系統發生錯誤，嘗試自動回報失敗。");
                }
            }
        }

        public static void ReportException(Exception exception, bool show)
        {
            ReportException(_System, _Version, exception, show);
        }

        private static bool FilterThreadAbortException(Exception exception)
        {
            if (exception == null)
                return false;
            if (exception is ThreadAbortException)
                return true;
            else
                return FilterThreadAbortException(exception.InnerException);
        }

        private static Dictionary<string, string> _RunTimeMessages = new Dictionary<string, string>();

        private XmlDocument doc;

        private string fileName = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Month + "_" + DateTime.Now.Second + ".xml";

        private void SaveExceptionOnLocal()
        {
            try
            {
                DirectoryInfo dic = new DirectoryInfo(Application.StartupPath + "Exception");
                if ( !dic.Exists )
                {
                    ( new DirectoryInfo(Application.StartupPath) ).CreateSubdirectory("Exception");
                }
                doc.Save(Application.StartupPath + "\\Exception\\" + fileName);
            }
            catch { }
        }

        private BugReporter(Exception e, string system, string version, bool sorry)
        {
            InitializeComponent();
            doc = new XmlDocument();
            doc.LoadXml("<Exception></Exception>");
            //儲存系統名稱資訊
            try { doc.DocumentElement.SetAttribute("有沒有道歉", sorry?"ㄨ":"哞"); }
            catch { }
            try { doc.DocumentElement.SetAttribute("SystemName", system); }
            catch { }
            try { doc.DocumentElement.SetAttribute("SystemVersion", version); }
            catch { }
            try { doc.DocumentElement.SetAttribute("AssemblyName", e.TargetSite.Module.Assembly.GetName().Name); }
            catch { }
            try { doc.DocumentElement.SetAttribute("AssemblyVersion", e.TargetSite.Module.Assembly.GetName().Version.ToString()); }
            catch { }
            try { doc.DocumentElement.SetAttribute("AssemblyImageRuntimeVersion", e.TargetSite.Module.Assembly.ImageRuntimeVersion); }
            catch { }
            try { doc.DocumentElement.SetAttribute("EnvironmentUserName", Environment.UserName); }
            catch { }
            try { doc.DocumentElement.SetAttribute("EnvironmentMachineName", Environment.MachineName); }
            catch { }
            try { doc.DocumentElement.SetAttribute("EnvironmentOSVersion", Environment.OSVersion.VersionString); }
            catch { }
            try { doc.DocumentElement.SetAttribute("EnvironmentServicePack", Environment.OSVersion.ServicePack); }
            catch { }
            try { doc.DocumentElement.SetAttribute("EnvironmentPlatform", Environment.OSVersion.Platform.ToString()); }
            catch { }

            foreach ( string field in _RunTimeMessages.Keys )
            {
                doc.DocumentElement.SetAttribute(field, _RunTimeMessages[field]);
            }
            BulideExceptionXml(doc.DocumentElement, e);
            SaveExceptionOnLocal();
            if ( !File.Exists(System.IO.Path.Combine(Application.StartupPath, "噓~不要告訴johnny5")) )
            {
                #region 傳送回SERVER
                try
                {
                    DSConnection conn = GetDSConnection();

                    if ( conn == null )
                        throw new Exception("無法連線到錯誤回報系統。");

                    conn.SendRequest("QualitySystem.ReportBug", new DSRequest("<ReportBugRequest>" +
                        "<SystemName>" + system + "</SystemName>" +
                        "<SystemVersion>" + version + "</SystemVersion>" +
                        "<Content>" + doc.DocumentElement.OuterXml + "</Content>" +
                        "</ReportBugRequest>"));
                }
                catch
                {
                    this.richTextBox1.Text = "系統發生錯誤，進行中的動作將停止執行。\r\n\r\n嘗試自動回報失敗，錯誤資訊儲存於\"" + Application.StartupPath + "\\Exception\\" + fileName + "\"。\r\n\r\n造成您的不便，本公司深感抱歉。\r\n";
                }
                #endregion
            }
        }

        private static DSConnection _dscon;
        private static DSConnection GetDSConnection()
        {
            if (_dscon == null)
            {
                Ping p = new Ping();
                //string host = "beta.smartschool.com.tw";
                try
                {
                    //PingReply pr = p.Send(host, 2000);
                    //if (pr.Status == IPStatus.Success)
                    //    _dscon = new DSConnection("http://beta.smartschool.com.tw/commonserver/quality", "anonymous", "");
                    //else
                        _dscon = new DSConnection("http://beta.smartschool.com.tw/commonserver/quality", "anonymous", "");
                }
                catch (Exception)
                {
                    //MsgBox.Show("找不到更新主機 :" + host);
                    return null;
                }
                if (!_dscon.IsConnected)
                {
                    try
                    {
                        _dscon.Connect();
                    }
                    catch (Exception)
                    {
                        _dscon = null;
                    }
                }
            }
            return _dscon;
        }

        private void BulideExceptionXml(XmlElement element, Exception ex)
        {
            //儲存Exception的真正型別
            element.SetAttribute("Type", ex.GetType().Name);
            XmlElement ele;
            #region 用反映把所有可以XML序列化的屬性轉換成XML，不能序列化就算了
            foreach (PropertyInfo var in ex.GetType().GetProperties())
            {
                if (var.Name != "InnerException" && var.CanRead)
                {
                    try
                    {
                        StringWriter sw = new StringWriter();
                        XmlSerializer xs = new XmlSerializer(var.PropertyType);
                        xs.Serialize(sw, var.GetValue(ex, null));
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(sw.ToString());

                        ele = element.OwnerDocument.CreateElement(var.Name);

                        if (var.Name == "StackTrace")
                            ele.AppendChild(element.OwnerDocument.CreateCDataSection(doc.DocumentElement.InnerText));
                        else if (var.PropertyType == typeof(DSRequest))
                        {//發生錯誤就不管...
                            try
                            {
                                ele.InnerXml = (var.GetValue(ex, null) as DSRequest).GetRawXml();
                            }
                            catch (Exception) { }
                        }
                        else
                            ele.InnerText = doc.DocumentElement.InnerText;

                        element.AppendChild(ele);
                    }
                    catch
                    { }
                }
                //if(var.Name=="Data"
            }
            #endregion
            //如果有InnerException則遞迴呼叫
            ele = element.OwnerDocument.CreateElement("InnerException");
            element.AppendChild(ele);
            if (ex.InnerException != null)
            {
                BulideExceptionXml(ele, ex.InnerException);
            }
            foreach ( object var in ex.Data.Keys )
            {
                ele = element.OwnerDocument.CreateElement("Data");
                element.AppendChild(ele);
                try
                {
                    StringWriter sw = new StringWriter();
                    XmlSerializer xs = new XmlSerializer(var.GetType());
                    xs.Serialize(sw, var);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(sw.ToString());
                    XmlElement  keyElement = element.OwnerDocument.CreateElement("Key");
                    keyElement.InnerText = doc.DocumentElement.InnerText;
                    ele.AppendChild(keyElement);
                }
                catch
                { }
                try
                {
                    StringWriter sw = new StringWriter();
                    XmlSerializer xs = new XmlSerializer(ex.Data[var].GetType());
                    xs.Serialize(sw, ex.Data[var]);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(sw.ToString());
                    XmlElement valueElement = element.OwnerDocument.CreateElement("Value");
                    valueElement.InnerText = doc.DocumentElement.InnerText;
                    ele.AppendChild(valueElement);
                }
                catch
                { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Application.StartupPath + "\\Exception\\" + fileName);
        }
    }
}