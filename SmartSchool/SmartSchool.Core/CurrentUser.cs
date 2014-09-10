using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
using SmartSchool.Feature;
using DevComponents.DotNetBar;
using System.IO;
using SmartSchool.ExceptionHandler;
using System.Resources;
using System.Reflection;
using Aspose.Cells;
using SmartSchool.ApplicationLog;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using OnlineUpdateClient;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using SmartSchool.AccessControl;
using SmartSchool.SysAdmin;
using SmartSchool.Common;

namespace SmartSchool
{
    public class CurrentUser
    {
        #region 獨體模式
        private static CurrentUser _Instance;
        public static CurrentUser Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CurrentUser();
                return _Instance;
            }
        }
        private CurrentUser()
        {
            _CrossThreadControl = new Control();
            IntPtr p = _CrossThreadControl.Handle;
        }
        #endregion

        private Control _CrossThreadControl;
        private DSConnection _Conn;
        private bool _IsLogined;
        private PreferenceCollection _Preference;
        private ConfigurationCollection _Configuration;
        //private FeatureCollection _allowFeatures;
        private SchoolInfo _schoolInfo = new SchoolInfo();
        private SchoolConfig _schoolConfig = new SchoolConfig();
        private SystemConfig _systemConfig = new SystemConfig();
        private VersionManifest _manifest;
        private LogProvider _log;
        private LogSender _log_sender;
        private string _user_name;
        private bool _debug_mode;
        private string _currentAccessPoint;
        private static FeatureAcl _acl = new FeatureAcl();
        private List<string> _roles = new List<string>();

        public event EventHandler LoginSuccess;

        public int SchoolYear
        {
            get
            {
                return _systemConfig.DefaultSchoolYear;
            }
        }

        public int Semester
        {
            get
            {
                return _systemConfig.DefaultSemester;
            }
        }

        public string SchoolChineseName
        {
            get { return _schoolInfo.ChineseName; }
        }

        public string SchoolEnglishName
        {
            get { return _schoolInfo.EnglishName; }
        }

        public string SchoolCode
        {
            get { return _schoolInfo.Code; }
        }

        public string SystemVersion
        {
            get
            {
                if (_manifest == null)
                    return "0.0.0.0";
                else
                {
                    if (_manifest.Version == null)
                        return "0.0.0.0";
                    else
                        return _manifest.Version.ToString();
                }
            }
        }

        public string SystemName
        {
            get
            {
                return "Smart School";
            }
        }

        public bool IsDebugMode
        {
            get { return _debug_mode; }
            set { _debug_mode = value; }
        }

        public LogProvider AppLog
        {
            get { return _log; }
        }

        public string UserName
        {
            get { return _user_name; }
        }

        private bool _is_sys_admin;
        public bool IsSysAdmin
        {
            get { return _is_sys_admin; }
        }

        public string AccessPoint
        {
            get { return _currentAccessPoint; }
        }

        public SchoolInfo SchoolInfo
        {
            get { return _schoolInfo; }
        }

        public SchoolConfig SchoolConfig
        {
            get { return _schoolConfig; }
        }

        internal SystemConfig SystemConfig
        {
            get { return _systemConfig; }
        }

        public bool IsLogined
        {
            get { return _IsLogined; }
        }
        public PreferenceCollection Preference
        {
            get { return _Preference; }
        }
        public ConfigurationCollection Configuration
        {
            get { return _Configuration; }
        }
        /// <summary>
        /// 
        /// </summary>
        public static FeatureAcl Acl
        {
            get { return _acl; }
        }

        public void Login()
        {
            _Preference = null;
            _Configuration = null;
            _IsLogined = false;
            LoginForm loginForm = new LoginForm();
            loginForm.FormClosed += new FormClosedEventHandler(loginForm_FormClosed);
            loginForm.Show();
        }

        void loginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginForm loginForm = (LoginForm)sender;
            if (!_IsLogined)
                Application.Exit();
            else
            {
                loginForm.Hide();
                _Preference = new PreferenceCollection();
                _Configuration = new ConfigurationCollection();
                _schoolConfig.Load(); //載入學校設定

                //_allowFeatures = new FeatureCollection();
                _schoolInfo.Load(); //載入學校資訊
                _systemConfig.Load(); //載入系統相關設定
                LoadManifestInfo(); //載入版本相關資訊
                SetAsposeComponentsLicense();
                InitialLogProvider(loginForm.UserName);
                _user_name = loginForm.UserName;
                _currentAccessPoint = loginForm.AccessPoint;

                GetUserRoles();

                string desc = "系統版本：" + SystemVersion;
                SmartSchool.ExceptionHandler.BugReporter.SetSystem("SmartSchool");
                SmartSchool.ExceptionHandler.BugReporter.SetVersion(SystemVersion);
                string diag = string.Format("學校名稱：{0}\n學年度：{1}\n學期：{2}", SchoolChineseName, SchoolYear, Semester);
                AppLog.Write("登入", desc, string.Empty, diag);

                //決定是否為除錯模式。
                IsDebugMode = (Control.ModifierKeys == Keys.Shift);

                if (LoginSuccess != null)
                {
                    LoginSuccess.Invoke(this, new EventArgs());
                }
            }
        }

        private void GetUserRoles()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "UserName", this.UserName.ToUpper());
            DSResponse dsrsp = CallService("SmartSchool.Personal.GetRoles", new DSRequest(helper));
            foreach (XmlElement roleElement in dsrsp.GetContent().GetElements("Role"))
            {
                string role = roleElement.GetAttribute("Description");
                if (!_roles.Contains(role))
                    _roles.Add(role);

                foreach (XmlElement featureElement in roleElement.SelectNodes("Permissions/Feature"))
                {
                    string code = featureElement.GetAttribute("Code");
                    string perm = featureElement.GetAttribute("Permission");
                    FeatureAce ace = new FeatureAce(code, perm);
                    _acl.MergeAce(ace);
                }
            }
        }

        /// <summary>
        /// 回報錯誤訊息到 Server。
        /// </summary>
        public static void ReportError(Exception exception)
        {
            CurrentUser user = CurrentUser.Instance;
            BugReporter.ReportException(user.SystemName, user.SystemVersion, exception, false);
        }

        private void InitialLogProvider(string userName)
        {
            LogStorageQueue logstorage = new LogStorageQueue();
            _log_sender = new LogSender(logstorage);
            _log = new LogProvider(logstorage, userName);
        }

        private void SetAsposeComponentsLicense()
        {
            try
            {
                Type type = this.GetType();
                Stream slic = new MemoryStream(Properties.Resources.aspose_total);//type.Assembly.GetManifestResourceStream("SmartSchool.Aspose.Total.lic");

                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense(slic);

                slic.Seek(0, SeekOrigin.Begin);
                Aspose.Words.License licword = new Aspose.Words.License();
                licword.SetLicense(slic);

                slic.Seek(0, SeekOrigin.Begin);
                Aspose.BarCode.License licbarcode = new Aspose.BarCode.License();
                licbarcode.SetLicense(slic);
            }
            catch (Exception ex)
            {
                BugReporter.ReportException(SystemName, SystemVersion, ex, false);
            }
        }


        private bool SSLCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //只要是「CN=intellischool Root Authority」發的憑證都信任。
            return (certificate.Issuer == "CN=intellischool Root Authority");
        }

        internal bool CheckUserPassword(string loginName, string password)
        {
            try
            {
                DSXmlHelper request = new DSXmlHelper("Request");
                request.AddElement("Condition");
                request.AddElement("Condition", "UserName", loginName.ToUpper());
                request.AddElement("Condition", "Password", PasswordHash.Compute(password));

                DSXmlHelper response = _Conn.SendRequest("SmartSchool.Personal.CheckUserPassword", request);

                if (response.GetElements("User").Length <= 0)
                    throw new Exception("使用者帳號或密碼錯誤。");

                XmlElement user = response.GetElement("User");
                if (user.GetAttribute("IsSysAdmin") == "1")
                    _is_sys_admin = true;
                else
                    _is_sys_admin = false;


                _user_name = loginName;
                SmartSchool.ExceptionHandler.BugReporter.SetRunTimeMessage("LoginUser", loginName);
                _IsLogined = true;

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ArrangeExceptionMessage(ex));
                return false;
            }

        }

        internal bool SetConnection(LicenseInfo license)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(SSLCertificateValidation);

                _Conn = new DSConnection();
                _Conn.UseSession = true;
                _Conn.Connect(license.AccessPoint, license.ApplicationToken);
                SmartSchool.ExceptionHandler.BugReporter.SetRunTimeMessage("DsnsName", license.AccessPoint);
                return true;
            }
            catch (DSAServerException ex)
            {
                switch (ex.ServerStatus)
                {
                    case DSAServerStatus.AccessDeny:
                        MsgBox.Show(DSAServerStatus.AccessDeny.ToString() + " 拒絕存取");
                        break;
                    case DSAServerStatus.ApplicationUnavailable:
                        MsgBox.Show(DSAServerStatus.ApplicationUnavailable + " DSA Application 組態或狀態不正確");
                        break;
                    case DSAServerStatus.CredentialInvalid:
                        MsgBox.Show("登入失敗，請確認帳號密碼");
                        break;
                    case DSAServerStatus.InvalidRequestDocument:
                        MsgBox.Show(DSAServerStatus.InvalidRequestDocument + " 不合法的申請文件");
                        break;
                    case DSAServerStatus.InvalidResponseDocument:
                        MsgBox.Show(DSAServerStatus.InvalidResponseDocument + " 不合法的回覆文件");
                        break;
                    case DSAServerStatus.PassportExpire:
                        MsgBox.Show(DSAServerStatus.PassportExpire + " DSA Passport 過期");
                        break;
                    case DSAServerStatus.ServerUnavailable:
                        MsgBox.Show(DSAServerStatus.ServerUnavailable + " DSA Server 組態或狀態不正確");
                        break;
                    case DSAServerStatus.ServiceActivationError:
                        MsgBox.Show(DSAServerStatus.ServiceActivationError + " 服務啟動錯誤");
                        break;
                    case DSAServerStatus.ServiceBusy:
                        MsgBox.Show(DSAServerStatus.ServiceBusy + " 服務忙碌");
                        break;
                    case DSAServerStatus.ServiceExecutionError:
                        MsgBox.Show(DSAServerStatus.ServiceExecutionError + " 服務內部錯誤");
                        break;
                    case DSAServerStatus.ServiceNotFound:
                        MsgBox.Show(DSAServerStatus.ServiceNotFound + " 服務不存在");
                        break;
                    case DSAServerStatus.SessionExpire:
                        MsgBox.Show(DSAServerStatus.SessionExpire + " Session 過期");
                        break;
                    case DSAServerStatus.Successful:
                        MsgBox.Show("成功");
                        break;
                    case DSAServerStatus.UnhandledException:
                        MsgBox.Show(DSAServerStatus.UnhandledException + " DSA Server 未預期處理的 Exception");
                        break;
                    case DSAServerStatus.Unknow:
                        MsgBox.Show(DSAServerStatus.Unknow + " 未知的狀態");
                        break;
                    default:
                        switch (ex.ServerStatus.ToString())
                        {
                            case "513":
                                MsgBox.Show("連線到 DSNS 主機錯誤");
                                break;
                        }
                        break;
                }

            }
            catch (ConnectException ex)
            {
                MsgBox.Show(ArrangeExceptionMessage(ex));
            }
            catch (Exception ex)
            {
                MsgBox.Show(ArrangeExceptionMessage(ex));
            }

            _IsLogined = false;
            return false;
        }

        private string ArrangeExceptionMessage(Exception ex)
        {
            string msg = string.Empty;
            int level = 0;
            Exception temp = ex;

            while (temp != null)
            {
                if (msg != string.Empty)
                    msg += "\n".PadRight(level * 5, ' ') + temp.Message;
                else
                    msg = temp.Message;

                temp = temp.InnerException;
                level++;
            }

            return msg;
        }

        public DSResponse CallService(string service, DSRequest req)
        {
            bool isQueryRequest = false;
            DSResponse resp = null;
            bool success = false;
            bool leaveOnError = false;
            bool isWebException = false;
            string exceptionMessage = "";
            for (int runTimes = 1; runTimes > 0; runTimes--)
            {
                try
                {
                        DateTime d1 = DateTime.Now;
                        MotherForm.NetWorking();
                        resp = _Conn.SendRequest(service, req, 60000);  //60秒才  Timeout
                        success = true;
                        if ( File.Exists(System.IO.Path.Combine(Application.StartupPath, "恐怖無敵爛網路")) )//開啟暴爛網路模擬
                        {
                            if ( Control.ModifierKeys == Keys.Alt )// & Control.ModifierKeys == Keys.Control & Control.ModifierKeys == Keys.Shift )
                            {
                                success = false; isWebException = true;
                            }
                            else
                                Thread.Sleep(3500);
                        }
                        MotherForm.SetupSpeed(resp.GetRawBinary().Length, ( (TimeSpan)( DateTime.Now - d1 ) ).Milliseconds);
                }
                catch ( Exception e )
                {
                    #region 是網路問題
                    success = false;
                    isWebException = IsWebException(e);
                    if ( isWebException )
                    {
                        exceptionMessage = GetExceptionMessage<WebException>(e);
                        //吵一下佳煜
                        ExceptionHandler.BugReporter.ReportException(e, false);
                        foreach ( StackFrame frame in ( new StackTrace() ).GetFrames() )
                        {
                            #region LeaveOnErrorCheck
                            #region 先掃呼叫函數
                            if ( !leaveOnError )
                            {
                                foreach ( object var in frame.GetMethod().GetCustomAttributes(true) )
                                {
                                    if ( var is LeaveOnErrorAttribute )
                                    {
                                        leaveOnError = true;
                                        break;
                                    }
                                }
                            }
                            #endregion
                            #region 再掃呼叫函數的class
                            if ( !leaveOnError )
                            {
                                Type type = frame.GetMethod().ReflectedType;
                                foreach ( object var in type.GetCustomAttributes(true) )
                                {
                                    if ( var is LeaveOnErrorAttribute )
                                    {
                                        leaveOnError = true;
                                        break;
                                    }
                                }
                            }
                            #endregion
                            if ( leaveOnError ) break;
                            #endregion
                            #region isQueryRequestCheck
                            #region 先掃呼叫函數
                            if ( !isQueryRequest )
                            {
                                foreach ( object var in frame.GetMethod().GetCustomAttributes(true) )
                                {
                                    if ( var is QueryRequestAttribute )
                                    {
                                        isQueryRequest = true;
                                        runTimes += ( var as QueryRequestAttribute ).ReTryTimes;
                                        break;
                                    }
                                }
                            }
                            #endregion
                            #region 再掃呼叫函數的class
                            if ( !isQueryRequest )
                            {
                                Type type = frame.GetMethod().ReflectedType;
                                foreach ( object var in type.GetCustomAttributes(true) )
                                {
                                    if ( var is QueryRequestAttribute )
                                    {
                                        isQueryRequest = true;
                                        runTimes += ( var as QueryRequestAttribute ).ReTryTimes;
                                        break;
                                    }
                                }
                            }
                            #endregion
                            if ( isQueryRequest ) break;
                            #endregion
                        }
                    #endregion
                    }
                    else
                    {
                        throw new SendRequestException(service, req, e);
                    }
                }
                finally
                {
                    if (success)
                        runTimes = 0;
                    //失敗且最後一次迴圈
                    if (isWebException && !success && runTimes <= 1)
                    {
                        if (MotherForm.IsClosed)
                        {
                            if (!Thread.CurrentThread.IsBackground)
                                Thread.CurrentThread.Abort();
                            Application.ExitThread();
                            MotherForm.NetWorkFinished(true);
                        }
                        else
                        {
                            if (isQueryRequest)
                            {
                                MotherForm.NetWorkFinished(false, exceptionMessage);
                                Thread.Sleep(500);
                                runTimes++;
                            }
                            else
                            {
                                MotherForm.SayByeBye(leaveOnError);
                                MotherForm.NetWorkFinished(false, exceptionMessage);
                                while (true)
                                {
                                    Thread.Sleep(1000);
                                    if (MotherForm.IsClosed)
                                    {
                                        if (!Thread.CurrentThread.IsBackground)
                                            Thread.CurrentThread.Abort();
                                        Application.ExitThread();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                        MotherForm.NetWorkFinished(true);
                }
            }
            return resp;
        }

        private bool IsWebException(Exception e)
        {
            return (e is WebException) || (e.InnerException != null && IsWebException(e.InnerException));
        }

        private string GetExceptionMessage<T>(Exception e) where T : Exception
        {
            if (e is T)
                return e.Message;
            else if (e.InnerException != null)
                return GetExceptionMessage<T>(e.InnerException);
            else
                return "";
        }

        private void LoadManifestInfo()
        {
            string manifestDir = Application.StartupPath;
            string mainfestFile = Path.Combine(manifestDir, "version.manifest");

            if (File.Exists(mainfestFile))
            {
                try
                {
                    _manifest = new VersionManifest();
                    _manifest.LoadFromFile(mainfestFile);
                }
                catch (Exception ex)
                {
                    BugReporter.ReportException("未定議", "未定議", ex, false);
                    _manifest = null;
                }
            }
            else
                _manifest = null;
        }
    }
}
