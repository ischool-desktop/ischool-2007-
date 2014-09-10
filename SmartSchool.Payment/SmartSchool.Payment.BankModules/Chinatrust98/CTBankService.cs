using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AccountsReceivalbe.BuildinBank.ChinatrustCommon;
using SmartSchool.Payment.Interfaces;

namespace AccountsReceivalbe.BuildinBank.Chinatrust98
{
    /// <summary>
    /// 這是 98 年，中國信託變更「超商」條碼邏輯後開發的新版。
    /// </summary>
    public class CTBankService : IBankService
    {
        #region IBankServiceProvider 成員

        public string Name
        {
            get { return "中國信託(98超商新邏輯)"; }
        }

        public string ModuleCode
        {
            get { return "Chinatrust98"; }
        }

        public int SequenceNumber
        {
            get { return SequenceRecord.GetNextSequence(); }
            set { SequenceRecord.SaveSequence(value); }
        }

        public BankConfigPane CreateBankConfigPane(BankConfig previousConf)
        {
            return new CTConfigurationPanel(previousConf);
        }

        public IBillCodeGenerator CreateGenerator(BankConfig bankConf, DateTime expireation)
        {
            return new CTBarcodeGenerator(expireation, bankConf);
        }

        #endregion

        /// <summary>
        /// 流水號的最大值，請勿修改。
        /// </summary>
        public const int MaxSequence = 99999;

        #region IBankService 成員

        public int GetAmountLimit(BankConfig bankConf)
        {
            ConfigParser cparser = new ConfigParser(bankConf.Content);
            int maxAmount = 1;

            foreach (AmountLevel each in cparser.Levels)
                maxAmount = Math.Max(maxAmount, each.UpperLimit);

            return maxAmount;
        }

        #endregion
    }
}
