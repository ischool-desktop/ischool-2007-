using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;
using System.Xml;

namespace SmartSchool.Payment.BankModules.Chinatrust
{
    public class CTBankService : IBankService
    {
        public const string PreferenceIdentity = "SmartSchool.Payment.BankModules.Chinatrust";

        public const int MaxSequence = 99999;

        #region IBankService 成員

        public string Name
        {
            get { return "中國信託"; }
        }

        public string ModuleCode
        {
            get { return "Chinatrust"; }
        }

        public BankConfigPane CreateBankConfigPane(BankConfig preConf)
        {
            BankConfigPane pane = new CTBankConfigPane();
            pane.SetConfig(preConf);
            return pane;
        }

        public IBillCodeGenerator CreateGenerator(BankConfig bankConf, DateTime expireation)
        {
            return new CTGenerator(bankConf, expireation);
        }

        #endregion

        #region IBankService 成員

        public int GetAmountLimit(BankConfig bankConf)
        {
            ConfigParser cparser = new ConfigParser(bankConf);
            int maxAmount = 1;

            foreach (AmountLevel each in cparser.Levels)
                maxAmount = Math.Max(maxAmount, each.UpperLimit);

            return maxAmount;
        }

        #endregion
    }
}
