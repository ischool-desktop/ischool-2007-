using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// 類別資訊
    /// </summary>
    public interface CategoryInfo
    {
        /// <summary>
        /// 子類別
        /// </summary>
        string SubCategory
        {
            get;
        }
        /// <summary>
        /// 類別
        /// </summary>
        string Name
        {
            get;
        }
        /// <summary>
        /// 完整名稱
        /// </summary>
        string FullName
        {
            get;
        }
    }
}
