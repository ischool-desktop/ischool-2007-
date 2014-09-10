using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// 類別清單
    /// </summary>
    public class CategoryCollection : System.Collections.ObjectModel.Collection<SmartSchool.Customization.Data.CategoryInfo>
    {
        /// <summary>
        /// 天下無敵(?)隱含轉型
        /// </summary>
        public static implicit operator CategoryCollection(System.Collections.Generic.List<SmartSchool.Customization.Data.CategoryInfo> list)
        {
            // code to convert from MyType to int
            CategoryCollection cc = new CategoryCollection();
            foreach ( CategoryInfo var in list )
            {
                cc.Add(var);
            }
            return cc;
        }
        /// <summary>
        /// 天下無敵(?)隱含轉型
        /// </summary>
        public static implicit operator System.Collections.Generic.List<SmartSchool.Customization.Data.CategoryInfo>(CategoryCollection list)
        {
            return new List<CategoryInfo>(list);
        }
        /// <summary>
        /// 是否包含特定類別。
        /// </summary>
        /// <param name="name">要搜尋的類別名稱，可輸入完整類別名稱或主要類別名稱。</param>
        public bool Contains(string name)
        {
            foreach ( CategoryInfo var in this )
            {
                if ( var.Name == name || var.FullName == name )
                    return true;
            }
            return false;
        }
    }
}
