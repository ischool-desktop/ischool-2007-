using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 比較欄位是否小於或等於某個值
    /// </summary>
    public class LessEqualsCondition: ICondition
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public LessEqualsCondition()
        {
            Field = "";
            Value = "";
        }
        /// <summary>
        /// 取得或設定，欄位名稱
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 取得或設定，要比較的值
        /// </summary>
        public string Value { get; set; }

        #region ICondtion 成員

        /// <summary>
        /// 傳回以Xml描述的條件式
        /// </summary>
        /// <returns>代表此條件式的XmlElement</returns>
        public System.Xml.XmlElement GetCondtionElement()
        {
            System.Xml.XmlElement element = QueryCompiler.Doc.CreateElement("LessEquals");
            element.SetAttribute("FieldName", Field);
            element.InnerText = Value;
            return element;
        }

        #endregion
    }
}
