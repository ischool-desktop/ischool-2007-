using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace FirstBankPayment.FirstBank
{
    internal class ConfigParser
    {
        /// <summary>
        /// 此類別用來解析第一銀行的組態值。(組態值格式可參照 Config_Sample.xml)
        /// </summary>
        /// <param name="config"></param>
        public ConfigParser(BankConfig config)
        {
            DSXmlHelper hlpconfig = new DSXmlHelper(config.BaseXml);

            string chargeOnus = hlpconfig.GetText("Content/@ChainChargeOnus");     //誰來負擔手續費

            if (string.IsNullOrEmpty(chargeOnus))
                _charge_onus = ChainChargeOnus.None;
            else
                _charge_onus = (ChainChargeOnus)Enum.Parse(typeof(ChainChargeOnus), chargeOnus, true);

            foreach (XmlElement each in hlpconfig.GetElements("Content/AccountConfig/AmountLevel"))
                _levels.Add(new AmountLevel(each, ChargeOnus));     //組態檔案中每個金額級距，及其對應的手續費和企業代碼，都產生一個對應的AmountLevel 物件。

            _school_code = hlpconfig.GetText("Content/@SchoolCode");   //取得學校代碼。
        }

        
        private List<AmountLevel> _levels = new List<AmountLevel>();
        /// <summary>
        /// 取得 AmountLevel 物件的集合。
        /// </summary>
        internal List<AmountLevel> Levels
        {
            get { return _levels; }
        }

        /// <summary>
        /// 根據繳費金額取得組態檔中定義的金額級距，藉此找出相對的手續費和企業代碼。
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public AmountLevel GetAmountLevel(int amount)
        {
            foreach (AmountLevel each in _levels)
            {
                if (amount >= each.LowerLimit && amount <= each.UpperLimit)
                    return each;
            }

            return null;
        }

        private string _school_code;
        /// <summary>
        /// 只有當一個學校的日夜間部都透過相同的企業代碼收費時才會用到SchoolCode。若學校只有日間部時，就不需要設定SchoolCode。SchoolCode由使用者在畫面上設定，長度為一個字元。
        /// </summary>
        public string SchoolCode
        {
            get { return _school_code; }
        }

        private ChainChargeOnus _charge_onus;
        /// <summary>
        /// 誰負責手續費？
        /// </summary>
        public ChainChargeOnus ChargeOnus
        {
            get { return _charge_onus; }
        }
    }

    internal enum ChainChargeOnus
    {
        /// <summary>
        /// 沒有人想負責。
        /// </summary>
        None,
        /// <summary>
        /// 收款人，通常指學校。
        /// </summary>
        Payee,
        /// <summary>
        /// 付款人，通常指學生。
        /// </summary>
        Payer
    }

    internal class AmountLevel
    {
        public AmountLevel(XmlElement config, ChainChargeOnus chargeOnus)
        {
            _enter_code = config.GetAttribute("EnterpriseCode");
            _shop_charge = int.Parse(config.GetAttribute("ShopCharge"));
            //_post_charge = int.Parse(config.GetAttribute("PostCharge"));
            _post_charge = 0;

            if (chargeOnus == ChainChargeOnus.Payee)
            {
                _low_limit = int.Parse(config.GetAttribute("LowerLimit"));
                _up_limit = int.Parse(config.GetAttribute("UpperLimit"));
            }
            else
            {
                _low_limit = int.Parse(config.GetAttribute("LowerLimit")) - _shop_charge;
                _up_limit = int.Parse(config.GetAttribute("UpperLimit")) - _shop_charge;
            }

            if (_low_limit < 0) _low_limit = 1;
            if (_up_limit < 0) _up_limit = 1;
        }

        private string _enter_code;
        public string EnterpriseCode
        {
            get { return _enter_code; }
        }

        private int _low_limit;
        public int LowerLimit
        {
            get { return _low_limit; }
        }

        private int _up_limit;
        public int UpperLimit
        {
            get { return _up_limit; }
        }

        private int _shop_charge;
        public int ShopCharge
        {
            get { return _shop_charge; }
        }

        private int _post_charge;
        public int PostCharge
        {
            get { return _post_charge; }
        }
    }
}
