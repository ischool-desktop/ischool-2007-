using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.Configure;

namespace SmartSchool.Configure
{
    public class ConfigurationManager : IConfigurationManager
    {
        private static ConfigurationManager _Instance = null;

        public static ConfigurationManager Instance 
        {
            get
            {
                if ( _Instance == null )
                    _Instance = new ConfigurationManager();
                return _Instance;
            }
        }

        private Dictionary<string, Configuration> _Configurations = new Dictionary<string, Configuration>();

        private ConfigurationManager() { }

        #region IConfigurationManager 成員

        public void AddConfigurationItem(IConfigurationItem configurationItem)
        {
            string groupName = configurationItem.TabGroup == "" ? "設定" : configurationItem.TabGroup;
            if ( !_Configurations.ContainsKey(groupName) )
            {
                Configuration newConf = new Configuration(groupName);
                _Configurations.Add(groupName, newConf);
                MotherForm.Instance.AddEntity(newConf);
            }
            _Configurations[groupName].AddConfigurationItem(configurationItem);
        }

        #endregion
    }
}
