using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ContextMenu
{
    /// <summary>
    /// 課程右鍵選單按鈕
    /// </summary>
    public static class CourseMenuButton
    {
        private static IManager<ButtonAdapter> _Manager = null;
        private static List<ButtonAdapter> catchItems = new List<ButtonAdapter>();
        public static void SetManager(IManager<ButtonAdapter> manager)
        {
            _Manager = manager;
            if ( catchItems != null )
            {
                foreach ( ButtonAdapter var in catchItems )
                {
                    _Manager.Add(var);
                }
            }
            catchItems.Clear();
        }
        /// <summary>
        /// 新增課程相關右鍵選單按鈕
        /// </summary>
        public static void AddItem(ButtonAdapter item)
        {
            if ( _Manager == null )
            {
                if ( catchItems == null )
                    catchItems = new List<ButtonAdapter>();
                catchItems.Add(item);
            }
            else
                _Manager.Add(item);
        }
    }
}
