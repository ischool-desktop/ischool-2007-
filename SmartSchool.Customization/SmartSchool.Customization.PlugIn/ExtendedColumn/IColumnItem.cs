using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ExtendedColumn
{
    /// <summary>
    /// 擴充延伸資料行
    /// </summary>
    public  interface IColumnItem
    {
        /// <summary>
        /// 延伸資料行的標題
        /// </summary>
        string ColumnHeader { get;}
        /// <summary>
        /// 延伸資料的值
        /// </summary>
        Dictionary<string, string> ExtendedValues { get;}
        /// <summary>
        /// 填入延伸資料值
        /// </summary>
        /// <param name="identities">相依的索引鍵值</param>
        void FillExtendedValues(List<string> identities);
        /// <summary>
        /// 資料發生變動
        /// </summary>
        event EventHandler VariableChanged;
    }
}
