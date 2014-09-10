using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ExtendedColumn
{
    /// <summary>
    /// 班級的延伸資料行管理
    /// </summary>
    public static class ExtendClassColumn
    {
        private static IManager<IColumnItem> _Manager = null;
        private static List<IColumnItem> catchItems = new List<IColumnItem>();
        public static void SetManager(IManager<IColumnItem> manager)
        {
            _Manager = manager;
            if ( catchItems != null )
            {
                foreach ( IColumnItem var in catchItems )
                {
                    _Manager.Add(var);
                }
            }
            catchItems.Clear();
        }
        /// <summary>
        /// 新增班級的延伸資料行
        /// </summary>
        public static void AddItem(IColumnItem item)
        {
            if ( _Manager == null )
            {
                if ( catchItems == null )
                    catchItems = new List<IColumnItem>();
                catchItems.Add(item);
            }
            else
                _Manager.Add(item);
        }
    }
}
