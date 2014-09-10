using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System;
using System.Reflection;
using System.IO;

namespace SmartSchool.Payment.AccountStatedService
{
    static class ServiceProgram
    {
        internal const string ServiceName = "SmartSchool Payment Transaction Service";

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            // 在同一處理序中可以執行一個以上的使用者服務。若要在這個處理序中
            // 加入另一項服務，請修改下行程式碼，
            // 以建立第二個服務物件。例如，
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            ServicesToRun = new ServiceBase[] { new ASService() };

            ServiceBase.Run(ServicesToRun);
        }

        public static string WorkingFolder
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                FileInfo file = new FileInfo(asm.Location);
                return file.DirectoryName;
            }
        }
    }
}