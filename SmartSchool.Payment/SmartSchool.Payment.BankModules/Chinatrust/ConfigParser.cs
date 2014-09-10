using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Payment.BankModules.Chinatrust
{
    internal class ConfigParser
    {
        public ConfigParser(BankConfig config)
        {
            DSXmlHelper hlpconfig = new DSXmlHelper(config.BaseXml);

            string chargeOnus = hlpconfig.GetText("Content/@ChainChargeOnus");

            if (string.IsNullOrEmpty(chargeOnus))
                _charge_onus = ChainChargeOnus.None;
            else
                _charge_onus = (ChainChargeOnus)Enum.Parse(typeof(ChainChargeOnus), chargeOnus, true);

            foreach (XmlElement each in hlpconfig.GetElements("Content/AccountConfig/AmountLevel"))
                _levels.Add(new AmountLevel(each, ChargeOnus));

            _school_code = hlpconfig.GetText("Content/@SchoolCode");
        }

        private List<AmountLevel> _levels = new List<AmountLevel>();
        internal List<AmountLevel> Levels
        {
            get { return _levels; }
        }

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
        public string SchoolCode
        {
            get { return _school_code; }
        }

        private ChainChargeOnus _charge_onus;
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
        /// 收款人。
        /// </summary>
        Payee,
        /// <summary>
        /// 付款人。
        /// </summary>
        Payer
    }

    internal class AmountLevel
    {
        public AmountLevel(XmlElement config, ChainChargeOnus chargeOnus)
        {
            _enter_code = config.GetAttribute("EnterpriseCode");
            _shop_charge = int.Parse(config.GetAttribute("ShopCharge"));
            _post_charge = int.Parse(config.GetAttribute("PostCharge"));

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
