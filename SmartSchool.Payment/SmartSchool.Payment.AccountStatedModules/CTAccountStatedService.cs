using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.AccountStatedService.Interfaces;

namespace SmartSchool.Payment.AccountStatedModules
{
    public class CTAccountStatedService : IAccountStatedService
    {
        #region IAccountStatedService 成員

        public string Name
        {
            get { return "中國信託"; }
        }

        public string ModuleCode
        {
            get { return "Chinatrust"; }
        }

        public ASServiceConfigPanel CreateASServiceConfigPanel(ASServiceConfig previousConf)
        {
            CTASServiceConfigPanel config = new CTASServiceConfigPanel();
            config.SetConfig(previousConf);
            return config;
        }

        public ITransactionBridge CreateBridge(ASServiceConfig bridgeConf)
        {
            return new CTTransactionBridge(bridgeConf);
        }

        #endregion
    }
}
