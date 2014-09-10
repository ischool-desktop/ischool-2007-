using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;

namespace FirstBankPayment.FirstBank
{
    /// <summary>
    /// 此類別實作IBankService，提供相關的類別及資訊給主程式。
    /// </summary>
    /// <remarks>
    /// 此類別是Abstract Factory Pattern 中的 Concrete Factory，負責產生相關物件，並提供某一銀行的特定資訊。
    /// </remarks>
    public class FcbBankService : IBankService
    {
        /// <summary>
        /// 此屬性作為儲存偏好設定時的 key 。不同銀行應有不同的 Key 值。
        /// </summary>
        public const string PreferenceIdentity = "SmartSchool.Payment.BankModules.FirstBank";

        /// <summary>
        /// 流水號的最大值，請勿修改。
        /// </summary>
        public const int MaxSequence = 99999;

        #region IBankService 成員

        /// <summary>
        /// 取得模組名稱，建議以銀行的中文名稱。
        /// </summary>
        public string Name
        {
            get { return "第一銀行(未開放)"; }
        }

        /// <summary>
        /// 取得此模組的代碼。不同銀行應定義不同的代碼，建議以銀行的英文名稱。
        /// </summary>
        public string ModuleCode
        {
            get { return "FirstBank"; }
        }

        /// <summary>
        /// 建立有繼承 BankConfigPane 類別，且有中國信託專屬邏輯的物件。
        /// 此物件是個User Control，用來管理某一銀行在某金額級距對應的手續費及企業代碼。
        /// </summary>
        /// <param name="preConf">BankConfig物件，代表某一銀行在某金額級距對應的手續費及企業代碼的組態值。(組態值格式可參考 Config_Sample.xml ) </param>
        /// <returns></returns>
        public BankConfigPane CreateBankConfigPane(BankConfig preConf)
        {
            BankConfigPane pane = new FcbBankConfigPane();
            pane.SetConfig(preConf);
            return pane;
        }

        /// <summary>
        /// 建立有實作 IBillCodeGenerator 介面的物件。
        /// </summary>
        /// <param name="bankConf">某銀行的組態值。(組態值格式可參考 Config_Sample.xml ) </param>
        /// <param name="expireation">繳費截止日</param>
        /// <returns></returns>
        public IBillCodeGenerator CreateGenerator(BankConfig bankConf, DateTime expireation)
        {
            return new FcbGenerator(bankConf, expireation);
        }

        /// <summary>
        /// 取得組態值中的金額上限。
        /// </summary>
        /// <param name="bankConf">某銀行的組態值。(組態值格式可參考 Config_Sample.xml )</param>
        /// <returns></returns>
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
