using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;

namespace SmartSchool.Payment.AccountStatedService.Interfaces
{
    internal static class ASServiceProvider
    {
        private static List<IAccountStatedService> _services;

        public static List<IAccountStatedService> GetServices()
        {
            if (_services == null)
                LoadASServiceModule();

            return _services;
        }

        #region Method LoadASServiceModule
        /// <summary>
        /// 載入銀行模組，並將 Service Instance 加入到 BankService 屬性中。
        /// </summary>
        private static void LoadASServiceModule()
        {
            _services = new List<IAccountStatedService>();
            FileInfo file = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string configFile = Path.Combine(file.DirectoryName, "ASServiceModuleConfig.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            foreach (XmlElement each in doc.DocumentElement.SelectNodes("Module"))
            {
                string assemblyFile = each.GetAttribute("AssemblyFile");

                Assembly asm = Assembly.LoadFile(Path.Combine(file.DirectoryName, assemblyFile));

                foreach (Type eachType in asm.GetTypes())
                {
                    if (eachType.GetInterface(typeof(IAccountStatedService).Name) != null)
                    {
                        ConstructorInfo ctr = eachType.GetConstructor(new Type[] { });
                        Object instance = ctr.Invoke(new object[] { });
                        IAccountStatedService service = instance as IAccountStatedService;
                        _services.Add(service);
                    }
                }
            }
        }
        #endregion

        internal static IAccountStatedService GetService(string moduleCode)
        {
            foreach (IAccountStatedService each in ASServiceProvider.GetServices())
            {
                if (each.ModuleCode == moduleCode)
                    return each;
            }
            return null;
        }
    }
}
