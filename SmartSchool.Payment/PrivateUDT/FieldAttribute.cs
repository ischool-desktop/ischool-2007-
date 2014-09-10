using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT
{
    /// <summary>
    /// 貼在屬性上，指定為資料庫欄位
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public FieldAttribute() { Field = null; Indexed = false; }
        /// <summary>
        /// 取得或設定，資料庫內欄位名稱
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 取得或設定，此欄位是否建立索引
        /// </summary>
        public bool Indexed { get; set; }
    }
}
