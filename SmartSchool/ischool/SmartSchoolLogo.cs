using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartSchool.Common;
//using SmartSchool;
//using SmartSchool.StudentRelated.Process.StudentIUD;
//using SmartSchool.StudentRelated;
//using SmartSchool.ClassRelated;
//using SmartSchool.TeacherRelated;
//using SmartSchool.CouseRelated;
using System.Reflection;
using System.IO;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.Diagnostics;
using DevComponents.DotNetBar;
using System.Net.NetworkInformation;
using System.Threading;
using System.Drawing;
using SmartSchool;
namespace ischool
{
    public partial class SmartSchoolLogo : BaseForm
    {
        //private static SmartSchoolLogo _logo;

        //private void ShowLogo()
        //{
        //    _logo = new SmartSchoolLogo();
        //    _logo.Show();
        //}

        private void CloseLogo()
        {
            ShowMessage("建立 ischool 使用者操作介面 ...");
            Application.DoEvents();

            Thread closeThread = new Thread(new ThreadStart(ThreadCloseLogo));
            closeThread.Start(); //啟動關閉執行緒。
        }

        private void ThreadCloseLogo()
        {
            if ( this.Created&&this.InvokeRequired )
            {
                Thread.Sleep(2000);
                this.Invoke(new ThreadStart(ThreadCloseLogo));
            }
            else
                this.Hide();
        }

        private void ShowMessage(string msg)
        {
            try
            {
                this.reflectionLabel1.Text = "<font color=\"MidnightBlue\" size=\"+6\"><i>" + msg + "</i></font>";
                Application.DoEvents();
            }
            catch ( Exception ex )
            {
                SmartSchool.ExceptionHandler.BugReporter.ReportException(ex, false);
            }
        }

        public SmartSchoolLogo()
        {
            InitializeComponent();
        }

        private void SmartSchoolLogo_Load(object sender, EventArgs ev)
        {
            this.Location = new Point(50, 50);
        }

        private void SmartSchoolLogo_Shown(object sender, EventArgs ea)
        {

            #region 檢查更新
            //bool ignoreUpdate = false;
            this.ShowMessage("檢查 ischool 更新...");
            bool ignoreUpdate = File.Exists(System.IO.Path.Combine(Application.StartupPath, "耀明萬歲萬歲萬萬歲"));// ( AppDomain.CurrentDomain.FriendlyName != "SmartSchool" );
            if ( !ignoreUpdate )
            {
                try
                {
                    OnlineUpdateForm updater = new OnlineUpdateForm();
                    if ( updater.IsUpdateRequired )
                    {
                        updater.UpdateSystem();
                        Application.Restart();
                        return;
                    }
                }
                catch ( Exception ex )
                {
                    MsgBox.Show(ex.Message);
                }
            }
            #endregion
            //使用者登入
            this.ShowMessage("等待使用者登入系統...");
            SmartSchool.CurrentUser.Instance.LoginSuccess += new EventHandler(Instance_LoginSuccess);
            SmartSchool.CurrentUser.Instance.Login();
        }

        void Instance_LoginSuccess(object sender, EventArgs ea)
        {

            try
            {
                SmartSchool.CurrentUser.Instance.LoginSuccess += new EventHandler(Instance_LoginSuccess);
                if ( !SmartSchool.CurrentUser.Instance.IsLogined )
                    return;
                SmartSchool.MotherForm.Instance.FormClosed += new FormClosedEventHandler(Instance_FormClosed);

                this.ShowMessage("啟動核心系統...");
                bool jhSystem = File.Exists(System.IO.Path.Combine(Application.StartupPath, "阿寶萬歲萬歲萬萬歲"));

                SmartSchool.Core_Program.Init_System();
                SmartSchool.Core_General_Program.Init_Student_Class_Teacher();
                if ( !jhSystem )
                    SmartSchool.Core_HS_Program.Init_Course();
                SmartSchool.Core_General_Program.Init_Core_Others();
                if ( !jhSystem )
                    SmartSchool.Core_HS_Program.Init_Course_Others();

                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.SetManager(SmartSchool.Configure.ConfigurationManager.Instance);

                string _ErrorMessage = "";
                this.ShowMessage("載入延伸模組...");
                #region 讀取PlugIn資料夾內的PlugIn
                System.IO.DirectoryInfo dic = new System.IO.DirectoryInfo(Application.StartupPath + "\\kernel\\");
                if ( dic.Exists )
                {
                    //#region 掃描整個資料夾的ＸＭＬ檔
                    //foreach ( FileInfo file in dic.GetFiles("*.xml", SearchOption.TopDirectoryOnly) )
                    //{
                    //    XmlDocument doc = new XmlDocument();
                    //    try
                    //    {
                    //        doc.Load(file.FullName);
                    //    }
                    //    catch ( Exception e )
                    //    {
                    //        _ErrorMessage += "無法載入" + file.Name + "：" + e.Message + "\n";
                    //        break;
                    //    }
                    //    #region 讀取XML黨內所有被記錄的DLL檔
                    //    foreach ( XmlNode dll in doc.SelectNodes("PlugIn/Dll") )
                    //    {
                    //        Assembly assm;
                    //        try
                    //        {
                    //            assm = Assembly.LoadFile(file.DirectoryName + "\\" + dll.SelectSingleNode("@Path").InnerText);
                    //        }
                    //        catch ( Exception e )
                    //        {
                    //            if ( dll.SelectSingleNode("@Path") != null || dll.SelectSingleNode("@Path").InnerText != "" )
                    //            {
                    //                _ErrorMessage += "無法載入函式庫" + dll.SelectSingleNode("@Path").InnerText + "：" + e.Message + "。\n";
                    //            }
                    //            else
                    //            {
                    //                _ErrorMessage += "XML檔案" + file.Name + "內容格式錯誤：PlugIn/Dll節點內沒有\"Path\"屬性或\"Path\"為空值\n";
                    //            }
                    //            continue;
                    //        }
                    //        //把Dll內所有Process加入
                    //        foreach ( XmlNode process in dll.SelectNodes("Process") )
                    //        {
                    //            try
                    //            {
                    //                IProcess iprocess = (IProcess)assm.CreateInstance(process.SelectSingleNode("@FullName").InnerText);
                    //                double tryParseLevel = 0;
                    //                if ( double.TryParse(process.SelectSingleNode("@Level").InnerText, out tryParseLevel) )
                    //                    iprocess.Level = tryParseLevel;
                    //                MotherForm.Instance.AddProcess(iprocess);
                    //            }
                    //            catch ( Exception e )
                    //            {
                    //                if ( process.SelectSingleNode("@FullName") != null || process.SelectSingleNode("@FullName").InnerText != "" )
                    //                {
                    //                    _ErrorMessage += "無法載入Process" + process.SelectSingleNode("@FullName").InnerText + "：" + e.Message + "\n";
                    //                }
                    //                else
                    //                {
                    //                    _ErrorMessage += "XML檔案" + file.Name + "內容格式錯誤：PlugIn/Dll/Process節點內沒有\"FullName\"屬性或\"FullName\"為空值\n";
                    //                }
                    //            }
                    //        }
                    //    }
                    //    #endregion
                    //}
                    //#endregion
                    #region 掃描每一個有MainMethod屬性進入點的模組
                    foreach ( FileInfo file in dic.GetFiles() )
                    {
                        Assembly assm;
                        try
                        {
                            assm = Assembly.LoadFile(file.FullName);
                        }
                        catch ( Exception e )
                        {
                            continue;
                        }
                        try
                        {
                            foreach ( Type type in assm.GetTypes() )
                            {
                                foreach ( MethodInfo method in type.GetMethods() )
                                {
                                    if ( method.IsStatic )
                                    {
                                        foreach ( Attribute att in Attribute.GetCustomAttributes(method, true) )
                                        {
                                            if ( att is SmartSchool.Customization.PlugIn.MainMethodAttribute )
                                            {
                                                try
                                                {
                                                    //this.ShowMessage("載入延伸模組...\n" + assm.FullName);
                                                    lblName.Text = assm.FullName.Split(',')[0];
                                                    Application.DoEvents();
                                                    method.Invoke(null, null);
                                                }
                                                catch ( Exception e )
                                                {
                                                    _ErrorMessage += "系統核心模組：\"" + file.FullName + "\"啟動失敗：" + e.Message + "\n";
                                                    e.HelpLink = "系統核心模組：\"" + file.FullName + "\"啟動失敗：" + e.Message;
                                                    SmartSchool.ExceptionHandler.BugReporter.ReportException(e, false);
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch ( ReflectionTypeLoadException le )
                        {
                            _ErrorMessage += "系統核心模組：\"" + file.FullName + "\"啟動失敗：";
                            le.HelpLink = "file.FullName";
                            foreach ( Exception var in le.LoaderExceptions )
                            {
                                _ErrorMessage += "\n\"" + var.Message + "\"\n";
                            }
                            CurrentUser user = CurrentUser.Instance;
                            SmartSchool.ExceptionHandler.BugReporter.ReportException("SmartSchool", user.SystemVersion, le, false);
                        }
                        catch { }
                    }
                    #endregion
                }
                #endregion
                #region 讀取Customize資料夾內的PlugIn
                dic = new System.IO.DirectoryInfo(Application.StartupPath + "\\Customize\\");
                if ( dic.Exists )
                {
                    #region 掃描每一個有MainMethod屬性進入點的模組
                    foreach ( FileInfo file in dic.GetFiles() )
                    {
                        Assembly assm;
                        try
                        {
                            assm = Assembly.LoadFile(file.FullName);
                        }
                        catch ( Exception e )
                        {
                            continue;
                        }
                        try
                        {
                            foreach ( Type type in assm.GetTypes() )
                            {
                                foreach ( MethodInfo method in type.GetMethods() )
                                {
                                    if ( method.IsStatic )
                                    {
                                        foreach ( Attribute att in Attribute.GetCustomAttributes(method, true) )
                                        {
                                            if ( att is SmartSchool.Customization.PlugIn.MainMethodAttribute )
                                            {
                                                try
                                                {
                                                    //this.ShowMessage("載入延伸模組...\n" + assm.FullName);
                                                    lblName.Text = assm.FullName.Split(',')[0];
                                                    Application.DoEvents();
                                                    method.Invoke(null, null);
                                                }
                                                catch ( Exception e )
                                                {
                                                    _ErrorMessage += "外掛模組" + file.FullName + "啟動失敗：" + e.Message + "。\n";
                                                    e.HelpLink = "外掛模組：\"" + file.FullName + "\"啟動失敗：" + e.Message;
                                                    SmartSchool.ExceptionHandler.BugReporter.ReportException(e, false);
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch ( ReflectionTypeLoadException le )
                        {
                            _ErrorMessage += "外掛模組：\"" + file.FullName + "\"啟動失敗：";
                            le.HelpLink = file.FullName;
                            foreach ( Exception var in le.LoaderExceptions )
                            {
                                _ErrorMessage += "\n\"" + var.Message + "\"\n";
                            }
                            SmartSchool.ExceptionHandler.BugReporter.ReportException(le, false);
                        }
                        catch { }
                    }
                    #endregion

                }
                #endregion
                lblName.Text = string.Empty;

                if ( _ErrorMessage != "" )
                {
                    new ErrorMessage(_ErrorMessage).Show();
                }
                if ( !MotherForm.Instance.IsDisposed )
                {
                    MotherForm.Instance.Show();
                    MotherForm.Instance.Activate();
                }
                this.CloseLogo();
            }
            //catch (ThreadAbortException et)
            catch ( ThreadAbortException )
            {
                Thread.ResetAbort();
                return;
            }
            catch ( Exception e )
            {
                SmartSchool.ExceptionHandler.BugReporter.ReportException(e, true);
                Application.Exit();
            }
        }

        void Instance_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}