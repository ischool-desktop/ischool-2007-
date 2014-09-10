using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.Configure
{
    public static class SystemConfiguration
    {
        private static IConfigurationManager _Manager;
        private static List<IConfigurationItem> catchItems = new List<IConfigurationItem>();
        public static void SetManager(IConfigurationManager manager)
        {
            _Manager = manager;
            foreach (IConfigurationItem var in catchItems)
            {
                _Manager.AddConfigurationItem(var);
            }
            catchItems.Clear();
        }

        /// <summary>
        /// 新增系統設定項目
        /// </summary>
        public static void AddConfigurationItem(IConfigurationItem report)
        {
            if ( _Manager == null )
            {
                catchItems.Add(report);
            }
            else
                _Manager.AddConfigurationItem(report);
        }
    }
}
