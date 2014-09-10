using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 比較欄位是否包含在某些值之中
    /// </summary>
    public class InCondition : ICondition
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public InCondition()
        {
            Field = "";
            Values = new List<string>();
        }
        /// <summary>
        /// 取得或設定，欄位名稱
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 取得，包含的值的集合
        /// </summary>
        public List<string> Values { get; internal set; }

        #region ICondtion 成員

        /// <summary>
        /// 傳回以Xml描述的條件式
        /// </summary>
        /// <returns>代表此條件式的XmlElement</returns>
        public System.Xml.XmlElement GetCondtionElement()
        {
            System.Xml.XmlElement element = QueryCompiler.Doc.CreateElement("In");
            element.SetAttribute("FieldName", Field);
            foreach ( var item in Values )
            {
                var v = QueryCompiler.Doc.CreateElement("Value");
                v.InnerText = item;
                element.AppendChild(v);
            }
            return element;
        }

        #endregion
    }
}
