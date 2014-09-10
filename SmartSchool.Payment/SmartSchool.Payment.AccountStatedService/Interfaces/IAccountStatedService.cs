using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.Payment.AccountStatedService.Interfaces
{
    public interface IAccountStatedService
    {
        /// <summary>
        /// 取得名稱，用於顯示於畫面上。
        /// </summary>
        string Name { get;}

        /// <summary>
        /// 模組代碼，用於識別銀行模組。
        /// </summary>
        string ModuleCode { get;}

        ASServiceConfigPanel CreateASServiceConfigPanel(ASServiceConfig previousConf);

        ITransactionBridge CreateBridge(ASServiceConfig bridgeConf);
    }
}
