using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Customization.PlugIn.ContextMenu
{
    /// <summary>
    /// 班級右鍵選單控制項
    /// </summary>
    public static class ClassMenuControl
    {
        private static IManager<ControlContainer> _Manager = null;
        private static List<ControlContainer> catchItems = new List<ControlContainer>();
        public static void SetManager(IManager<ControlContainer> manager)
        {
            _Manager = manager;
            if ( catchItems != null )
            {
                foreach ( ControlContainer var in catchItems )
                {
                    _Manager.Add(var);
                }
            }
            catchItems.Clear();
        }
        /// <summary>
        /// 新增班級相關右鍵選單控制項
        /// </summary>
        public static void AddItem(ControlContainer item)
        {
            if ( _Manager == null )
            {
                if ( catchItems == null )
                    catchItems = new List<ControlContainer>();
                catchItems.Add(item);
            }
            else
                _Manager.Add(item);
        }
    }
}
