using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using SmartSchool.Payment.Interfaces;

namespace SmartSchool.Payment.BankManagement
{
    internal static class BankConfigManager
    {
        private static string Preference_Configs = "BankConfigs";

        #region Method GetConfigList
        public static List<BankConfig> GetConfigList()
        {
            try
            {
                ModulePreference preference = PreferenceManager.GetPreference(ModuleMain.PreferenceIdentity);

                //從 Module Preference 中讀取銀行的組態設定。
                List<BankConfig> configs = new List<BankConfig>();
                XmlElement banks = preference.GetXml(Preference_Configs);
                if (banks != null)
                {
                    DSXmlHelper hlpconfigs = new DSXmlHelper(banks);
                    foreach (XmlElement each in hlpconfigs.GetElements("BankConfig"))
                    {
                        BankConfig objConf = new BankConfig(each);
                        string configId = objConf.ConfigID;

                        if (string.IsNullOrEmpty(configId))
                        {
                            configId = Guid.NewGuid().ToString();
                            objConf.ConfigID = configId;
                        }

                        configs.Add(objConf);
                    }
                }

                return configs;
            }
            catch (Exception ex)
            {
                throw new PaymentModuleException("讀取銀行組態資料錯誤。", ex);
            }
        }
        #endregion

        #region Method SaveConfig
        public static void SaveConfig(BankConfig config)
        {
            Dictionary<string, BankConfig> bcs = GetBankDictionary();

            if (bcs.ContainsKey(config.ConfigID))
                bcs[config.ConfigID] = config;
            else
                bcs.Add(config.ConfigID, config);

            SaveBankConfigsToServer(new List<BankConfig>(bcs.Values));
        }
        #endregion

        #region Method DeleteConfig
        public static void DeleteConfig(BankConfig config)
        {
            Dictionary<string, BankConfig> bcs = GetBankDictionary();

            if (bcs.ContainsKey(config.ConfigID))
                bcs.Remove(config.ConfigID);

            SaveBankConfigsToServer(new List<BankConfig>(bcs.Values));
        }
        #endregion

        #region Method SaveBankConfigsToServer
        private static void SaveBankConfigsToServer(List<BankConfig> configs)
        {
            DSXmlHelper xmlconfig = new DSXmlHelper("BankConfigs");
            foreach (BankConfig each in configs)
                xmlconfig.AddElement(".", each.BaseXml);

            ModulePreference preference = PreferenceManager.GetPreference(ModuleMain.PreferenceIdentity);
            preference.SetXml(Preference_Configs, xmlconfig.BaseElement);
            PreferenceManager.SavePreference(preference);
        }
        #endregion

        internal static Dictionary<string, BankConfig> GetBankDictionary()
        {
            Dictionary<string, BankConfig> bcs = new Dictionary<string, BankConfig>();
            foreach (BankConfig each in GetConfigList())
                bcs.Add(each.ConfigID, each);
            return bcs;
        }

        internal static BankConfig GetConfig(GT.Payment payment)
        {
            foreach (BankConfig each in BankConfigManager.GetConfigList())
            {
                if (each.ConfigID == payment.Config.BankConfigID)
                    return each;
            }

            return null;
        }
    }
}
