using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 描述一個條件式
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// 傳回以Xml描述的條件式
        /// </summary>
        /// <returns>代表此條件式的XmlElement</returns>
        XmlElement GetCondtionElement();
    }
}
