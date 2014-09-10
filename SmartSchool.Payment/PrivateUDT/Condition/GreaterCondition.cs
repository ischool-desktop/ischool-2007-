using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 比較欄位是否大於某個值
    /// </summary>
   public  class GreaterCondition: ICondition
    {
       /// <summary>
       /// 建構子
       /// </summary>
       public GreaterCondition()
        {
            Field = "";
            Value = "";
        }
       /// <summary>
       /// 取得或設定，要比較的欄位
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
            System.Xml.XmlElement element = QueryCompiler.Doc.CreateElement("Greater");
            element.SetAttribute("FieldName", Field);
            element.InnerText = Value;
            return element;
        }

        #endregion
    }
}
