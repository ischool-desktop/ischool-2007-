using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 描述將一個條件做Not運算
    /// </summary>
    public class NotCondition : ICondition
    {
        /// <summary>
        /// 條件
        /// </summary>
        public ICondition Condtion { get; set; }

        #region ICondtion 成員

        /// <summary>
        /// 傳回以Xml描述的條件式
        /// </summary>
        /// <returns>代表此條件式的XmlElement</returns>
        public System.Xml.XmlElement GetCondtionElement()
        {
            System.Xml.XmlElement element = QueryCompiler.Doc.CreateElement("Not");
            element.AppendChild(Condtion.GetCondtionElement());
            return element;
        }

        #endregion
    }
}
