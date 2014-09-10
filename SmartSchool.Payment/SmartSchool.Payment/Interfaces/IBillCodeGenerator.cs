using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.Payment.Interfaces
{
    /// <summary>
    /// 用於繳費單產生所需資料。
    /// </summary>
    public interface IBillCodeGenerator
    {
        /// <summary>
        /// 產生繳費單所需資料。
        /// </summary>
        /// <param name="amount">金額。</param>
        /// <returns>繳費單所需資料。</returns>
        List<BillCodeResult> Generate(List<BillCodeParameter> args);
    }
}
