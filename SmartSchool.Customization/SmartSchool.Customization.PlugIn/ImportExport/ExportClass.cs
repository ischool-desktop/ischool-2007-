using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ImportExport
{
    /// <summary>
    /// 匯出班級相依資料
    /// </summary>
    public class ExportClass
    {
        private static IManager<ExportProcess> _Manager = null;
        private static List<ExportProcess> catchItems = new List<ExportProcess>();
        public static void SetManager(IManager<ExportProcess> manager)
        {
            _Manager = manager;
            if ( catchItems != null )
            {
                foreach ( ExportProcess var in catchItems )
                {
                    _Manager.Add(var);
                }
            }
            catchItems.Clear();
        }
        /// <summary>
        /// 新增匯出班級相關流程
        /// </summary>
        public static void AddProcess(ExportProcess process)
        {
            if ( _Manager == null )
            {
                if ( catchItems == null )
                {
                    catchItems = new List<ExportProcess>();
                }
                catchItems.Add(process);
            }
            else
                _Manager.Add(process);
        }
    }
}
