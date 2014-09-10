using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace AccountsReceivalbe.BuildinBank.Chinatrust98
{
    internal class ConfigParser
    {
        public ConfigParser(XmlElement config)
        {
            DSXmlHelper hlpconfig = new DSXmlHelper(config);

            string chargeOnus = hlpconfig.GetText("@ChainChargeOnus");

            if (string.IsNullOrEmpty(chargeOnus))
                _charge_onus = PostChargeOnus.None;
            else
                _charge_onus = (PostChargeOnus)Enum.Parse(typeof(PostChargeOnus), chargeOnus, true);

            foreach (XmlElement each in hlpconfig.GetElements("AccountConfig/AmountLevel"))
                _levels.Add(new AmountLevel(each));

            _school_code = hlpconfig.GetText("@SchoolCode");
        }

        private List<AmountLevel> _levels = new List<AmountLevel>();
        internal List<AmountLevel> Levels
        {
            get { return _levels; }
        }

        public AmountLevel GetAmountLevel(decimal amount)
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

        private PostChargeOnus _charge_onus;
        public PostChargeOnus ChargeOnus
        {
            get { return _charge_onus; }
        }

    }

    internal enum PostChargeOnus
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
        public AmountLevel(XmlElement config)
        {
            _enter_code = config.GetAttribute("EnterpriseCode");
            _shop_charge = int.Parse(config.GetAttribute("ShopCharge"));
            ShopCode = config.GetAttribute("ShopCode");
            PostCode = config.GetAttribute("PostCode");

            _low_limit = int.Parse(config.GetAttribute("LowerLimit"));
            _up_limit = int.Parse(config.GetAttribute("UpperLimit"));

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
        /// <summary>
        /// 超商手續費。
        /// </summary>
        public int ShopCharge
        {
            get { return _shop_charge; }
        }

        /// <summary>
        /// 超商代號。
        /// </summary>
        public string ShopCode { get; set; }

        /// <summary>
        /// 郵局代號。
        /// </summary>
        public string PostCode { get; set; }

        //private int _post_charge;
        //public int PostCharge
        //{
        //    get { return _post_charge; }
        //}

    }
}
