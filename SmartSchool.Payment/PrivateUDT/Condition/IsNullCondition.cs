using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 判斷某個欄位值是否為null
    /// </summary>
    public class IsNullCondition:ICondition
    {
        /// <summary>
        /// 取得或設定，欄位名稱
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 建構子
        /// </summary>
        public IsNullCondition()
        {
            Field = "";
        }
        #region ICondition 成員

        /// <summary>
        /// 傳回以Xml描述的條件式
        /// </summary>
        /// <returns>代表此條件式的XmlElement</returns>
        public System.Xml.XmlElement GetCondtionElement()
        {
            System.Xml.XmlElement element = QueryCompiler.Doc.CreateElement("IsNull");
            element.SetAttribute("FieldName", Field);
            return element;
        }

        #endregion
    }
}
