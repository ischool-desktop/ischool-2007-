using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartSchool;
using SmartSchool.Common;
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
using DevComponents.DotNetBar.Rendering;

namespace ischool
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.CurrentCulture = new System.Globalization.CultureInfo("zh-TW", false);
            #region 讀取預設樣式資訊
            string filename = Application.StartupPath + "\\" + "Lucifer.dll";
            bool hasLoginInfo = File.Exists(filename);
            if ( hasLoginInfo )
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlElement ele = (XmlElement)doc.SelectSingleNode("Informations/Style");
                if ( ele != null )
                {
                    switch ( ele.GetAttribute("RibbonColor") )
                    {
                        case "Black":
                            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(eOffice2007ColorScheme.Black);
                            break;
                        case "Blue":
                            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(eOffice2007ColorScheme.Blue);
                            break;
                        case "Silver":
                            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(eOffice2007ColorScheme.Silver);
                            break;
                    }
                }
            }
            ( (Office2007Renderer)GlobalManager.Renderer ).ColorTableChanged += new EventHandler(Program_ColorTableChanged);
            #endregion
            Application.Run(new SmartSchoolLogo());
        }

        static void Program_ColorTableChanged(object sender, EventArgs e)
        {
            #region 寫入預設樣式資訊
            string filename = Application.StartupPath + "\\" + "Lucifer.dll";
            bool hasLoginInfo = System.IO.File.Exists(filename);
            if ( hasLoginInfo )
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlElement inf = (XmlElement)doc.SelectSingleNode("Informations");
                XmlElement ele = (XmlElement)doc.SelectSingleNode("Informations/Style");
                if ( inf != null && ele == null )
                {
                    ele = doc.CreateElement("Style");
                    inf.AppendChild(ele);
                }
                eOffice2007ColorScheme colorScheme = ( (Office2007Renderer)GlobalManager.Renderer ).ColorTable.InitialColorScheme;
                switch ( colorScheme )
                {
                    case eOffice2007ColorScheme.Black:
                        ele.SetAttribute("RibbonColor", "Black");
                        break;
                    case eOffice2007ColorScheme.Blue:
                        ele.SetAttribute("RibbonColor", "Blue");
                        break;
                    case eOffice2007ColorScheme.Silver:
                        ele.SetAttribute("RibbonColor", "Silver");
                        break;
                }
                try
                {
                    doc.Save(filename);
                }
                catch { }
            }
            #endregion
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            SmartSchool.ExceptionHandler.BugReporter.ReportException((Exception)e.ExceptionObject, true);
            Application.Exit();
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            SmartSchool.ExceptionHandler.BugReporter.ReportException(e.Exception, true);
            Application.Exit();
        }

        static void au_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}