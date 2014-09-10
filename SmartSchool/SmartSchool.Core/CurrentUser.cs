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
        #region �W��Ҧ�
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
                _schoolConfig.Load(); //���J�Ǯճ]�w

                //_allowFeatures = new FeatureCollection();
                _schoolInfo.Load(); //���J�Ǯո�T
                _systemConfig.Load(); //���J�t�ά����]�w
                LoadManifestInfo(); //���J����������T
                SetAsposeComponentsLicense();
                InitialLogProvider(loginForm.UserName);
                _user_name = loginForm.UserName;
                _currentAccessPoint = loginForm.AccessPoint;

                GetUserRoles();

                string desc = "�t�Ϊ����G" + SystemVersion;
                SmartSchool.ExceptionHandler.BugReporter.SetSystem("SmartSchool");
                SmartSchool.ExceptionHandler.BugReporter.SetVersion(SystemVersion);
                string diag = string.Format("�ǮզW�١G{0}\n�Ǧ~�סG{1}\n�Ǵ��G{2}", SchoolChineseName, SchoolYear, Semester);
                AppLog.Write("�n�J", desc, string.Empty, diag);

                //�M�w�O�_�������Ҧ��C
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
        /// �^�����~�T���� Server�C
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
            //�u�n�O�uCN=intellischool Root Authority�v�o�����ҳ��H���C
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
                    throw new Exception("�ϥΪ̱b���αK�X���~�C");

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
                        MsgBox.Show(DSAServerStatus.AccessDeny.ToString() + " �ڵ��s��");
                        break;
                    case DSAServerStatus.ApplicationUnavailable:
                        MsgBox.Show(DSAServerStatus.ApplicationUnavailable + " DSA Application �պA�Ϊ��A�����T");
                        break;
                    case DSAServerStatus.CredentialInvalid:
                        MsgBox.Show("�n�J���ѡA�нT�{�b���K�X");
                        break;
                    case DSAServerStatus.InvalidRequestDocument:
                        MsgBox.Show(DSAServerStatus.InvalidRequestDocument + " ���X�k���ӽФ��");
                        break;
                    case DSAServerStatus.InvalidResponseDocument:
                        MsgBox.Show(DSAServerStatus.InvalidResponseDocument + " ���X�k���^�Ф��");
                        break;
                    case DSAServerStatus.PassportExpire:
                        MsgBox.Show(DSAServerStatus.PassportExpire + " DSA Passport �L��");
                        break;
                    case DSAServerStatus.ServerUnavailable:
                        MsgBox.Show(DSAServerStatus.ServerUnavailable + " DSA Server �պA�Ϊ��A�����T");
                        break;
                    case DSAServerStatus.ServiceActivationError:
                        MsgBox.Show(DSAServerStatus.ServiceActivationError + " �A�ȱҰʿ��~");
                        break;
                    case DSAServerStatus.ServiceBusy:
                        MsgBox.Show(DSAServerStatus.ServiceBusy + " �A�Ȧ��L");
                        break;
                    case DSAServerStatus.ServiceExecutionError:
                        MsgBox.Show(DSAServerStatus.ServiceExecutionError + " �A�Ȥ������~");
                        break;
                    case DSAServerStatus.ServiceNotFound:
                        MsgBox.Show(DSAServerStatus.ServiceNotFound + " �A�Ȥ��s�b");
                        break;
                    case DSAServerStatus.SessionExpire:
                        MsgBox.Show(DSAServerStatus.SessionExpire + " Session �L��");
                        break;
                    case DSAServerStatus.Successful:
                        MsgBox.Show("���\");
                        break;
                    case DSAServerStatus.UnhandledException:
                        MsgBox.Show(DSAServerStatus.UnhandledException + " DSA Server ���w���B�z�� Exception");
                        break;
                    case DSAServerStatus.Unknow:
                        MsgBox.Show(DSAServerStatus.Unknow + " ���������A");
                        break;
                    default:
                        switch (ex.ServerStatus.ToString())
                        {
                            case "513":
                                MsgBox.Show("�s�u�� DSNS �D�����~");
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
                        resp = _Conn.SendRequest(service, req, 60000);  //60��~  Timeout
                        success = true;
                        if ( File.Exists(System.IO.Path.Combine(Application.StartupPath, "���ƵL�������")) )//�}�Ҽ����������
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
                    #region �O�������D
                    success = false;
                    isWebException = IsWebException(e);
                    if ( isWebException )
                    {
                        exceptionMessage = GetExceptionMessage<WebException>(e);
                        //�n�@�U�η�
                        ExceptionHandler.BugReporter.ReportException(e, false);
                        foreach ( StackFrame frame in ( new StackTrace() ).GetFrames() )
                        {
                            #region LeaveOnErrorCheck
                            #region �����I�s���
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
                            #region �A���I�s��ƪ�class
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
                            #region �����I�s���
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
                            #region �A���I�s��ƪ�class
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
                    //���ѥB�̫�@���j��
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
                    BugReporter.ReportException("���wĳ", "���wĳ", ex, false);
                    _manifest = null;
                }
            }
            else
                _manifest = null;
        }
    }
}
