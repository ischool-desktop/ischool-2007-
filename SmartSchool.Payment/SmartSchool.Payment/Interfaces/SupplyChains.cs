using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.Interfaces
{
    /// <summary>
    /// 通路，目前可選擇 7-11、郵局。
    /// </summary>
    [Flags()]
    public enum SupplyChains
    {
        /// <summary>
        /// 未決定
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 超商。
        /// </summary>
        Shop = 0x01,
        /// <summary>
        /// 郵局。
        /// </summary>
        Post = 0x02
    }
}