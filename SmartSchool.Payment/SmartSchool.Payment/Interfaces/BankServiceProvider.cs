using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;
using System.IO;
using System.Xml;
using System.Reflection;

namespace SmartSchool.Payment.Interfaces
{
    internal static class BankServiceProvider
    {
        private static List<IBankService> _services;

        public static List<IBankService> GetServices()
        {
            if (_services == null)
                LoadBankModule();

            return _services;
        }

        #region Method LoadBankModule
        /// <summary>
        /// 載入銀行模組，並將 Service Instance 加入到 BankService 屬性中。
        /// </summary>
        private static void LoadBankModule()
        {
            _services = new List<IBankService>();
            FileInfo file = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string configFile = Path.Combine(file.DirectoryName, "BankModuleConfig.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            foreach (XmlElement each in doc.DocumentElement.SelectNodes("Module"))
            {
                string assemblyFile = each.GetAttribute("AssemblyFile");

                Assembly asm = Assembly.LoadFile(Path.Combine(file.DirectoryName, assemblyFile));

                foreach (Type eachType in asm.GetTypes())
                {
                    if (eachType.GetInterface(typeof(IBankService).Name) != null)
                    {
                        ConstructorInfo ctr = eachType.GetConstructor(new Type[] { });
                        Object instance = ctr.Invoke(new object[] { });
                        IBankService service = instance as IBankService;
                        _services.Add(service);
                    }
                }
            }
        }
        #endregion

        internal static IBankService GetService(BankConfig config)
        {
            if (config == null) return null;

            foreach (IBankService each in BankServiceProvider.GetServices())
            {
                if (each.ModuleCode == config.ModuleCode)
                    return each;
            }
            return null;
        }
    }
}
