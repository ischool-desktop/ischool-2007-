using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT
{
    /// <summary>
    /// 貼在ActiveRecord的實作類別上，效果與override TableName()相同
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
   public  class TableNameAttribute:Attribute
    {
        /// <summary>
        /// 取得資料表名稱
        /// </summary>
       public string TableName { get; private set; }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="tableName">資料表名稱</param>
       public TableNameAttribute(string tableName)
       {
           TableName = tableName;
       }
    }
}
