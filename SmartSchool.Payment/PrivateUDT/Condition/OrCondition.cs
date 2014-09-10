using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 描述兩個條件做or運算
    /// </summary>
    public class OrCondition : ICondition
    {
        /// <summary>
        /// 取得或設定，第一個條件
        /// </summary>
        public ICondition Condtion1 { get; set; }
        /// <summary>
        /// 取得或設定，第二個條件
        /// </summary>
        public ICondition Condtion2 { get; set; }

        #region ICondtion 成員

        /// <summary>
        /// 傳回以Xml描述的條件式
        /// </summary>
        /// <returns>代表此條件式的XmlElement</returns>
        public System.Xml.XmlElement GetCondtionElement()
        {
            System.Xml.XmlElement element = QueryCompiler.Doc.CreateElement("Or");
            element.AppendChild(Condtion1.GetCondtionElement());
            element.AppendChild(Condtion2.GetCondtionElement());
            return element;
        }

        #endregion
    }
}
