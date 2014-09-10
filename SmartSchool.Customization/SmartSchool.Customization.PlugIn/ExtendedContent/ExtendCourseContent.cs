using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ExtendedContent
{
    /// <summary>
    /// 課程的延伸資料管理畫面
    /// </summary>
    public static class ExtendCourseContent
    {
        private static IManager<IContentItem> _Manager = null;
        private static List<IContentItem> catchItems = new List<IContentItem>();
        public static void SetManager(IManager<IContentItem> manager)
        {
            _Manager = manager;
            if ( catchItems != null )
            {
                foreach ( IContentItem var in catchItems )
                {
                    _Manager.Add(var);
                }
            }
            catchItems.Clear();
        }
        /// <summary>
        /// 新增課程的延伸資料管理
        /// </summary>
        public static void AddItem(IContentItem item)
        {
            if ( _Manager == null )
            {
                if ( catchItems == null )
                    catchItems = new List<IContentItem>();
                catchItems.Add(item);
            }
            else
                _Manager.Add(item);
        }
    }
}
