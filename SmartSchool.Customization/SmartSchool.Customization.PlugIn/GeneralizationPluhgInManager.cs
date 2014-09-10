using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn
{
    /// <summary>
    /// 通用PlugIn管理
    /// </summary>
    /// <remarks>
    /// 不同的型別會有各自獨立的管理器集合。
    /// 在管理器集合中，以字串的值做為管理器的索引，不同的管理器應會PlugIn至不同地方。
    /// </remarks>
    /// <typeparam name="T">PlugIn內容物的型別</typeparam>
    public class GeneralizationPluhgInManager<T> : Dictionary<string, IManager<T>>
    {
        private GeneralizationPluhgInManager()
        { 
        }

        static private GeneralizationPluhgInManager<T> _Instance = null;
        /// <summary>
        /// GeneralizationPluhgInManager在此泛型的唯一實體
        /// </summary>
        static public GeneralizationPluhgInManager<T> Instance
        {
            get 
            {
                if ( _Instance == null )
                    _Instance = new GeneralizationPluhgInManager<T>();
                return _Instance;
            }
        }
    }
}
