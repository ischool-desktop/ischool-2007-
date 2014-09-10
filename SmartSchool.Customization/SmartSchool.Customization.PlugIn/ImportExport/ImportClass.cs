using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ImportExport
{
    /// <summary>
    /// 匯入班級相依資料
    /// </summary>
    public static class ImportClass
    {
        private static IManager<ImportProcess> _Manager = null;
        private static List<ImportProcess> catchItems = new List<ImportProcess>();
        public static void SetManager(IManager<ImportProcess> manager)
        {
            _Manager = manager;
            if ( catchItems != null )
            {
                foreach ( ImportProcess var in catchItems )
                {
                    _Manager.Add(var);
                }
            }
            catchItems.Clear();
        }
        /// <summary>
        /// 新增匯入班級相關流程
        /// </summary>
        public static void AddProcess(ImportProcess process)
        {
            if ( _Manager == null )
            {
                if ( catchItems == null )
                {
                    catchItems = new List<ImportProcess>();
                }
                catchItems.Add(process);
            }
            else
                _Manager.Add(process);
        }
    }
}
