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
        /// �����O�ΨӸѪR�Ĥ@�Ȧ檺�պA�ȡC(�պA�Ȯ榡�i�ѷ� Config_Sample.xml)
        /// </summary>
        /// <param name="config"></param>
        public ConfigParser(BankConfig config)
        {
            DSXmlHelper hlpconfig = new DSXmlHelper(config.BaseXml);

            string chargeOnus = hlpconfig.GetText("Content/@ChainChargeOnus");     //�֨ӭt�����O

            if (string.IsNullOrEmpty(chargeOnus))
                _charge_onus = ChainChargeOnus.None;
            else
                _charge_onus = (ChainChargeOnus)Enum.Parse(typeof(ChainChargeOnus), chargeOnus, true);

            foreach (XmlElement each in hlpconfig.GetElements("Content/AccountConfig/AmountLevel"))
                _levels.Add(new AmountLevel(each, ChargeOnus));     //�պA�ɮפ��C�Ӫ��B�ŶZ�A�Ψ����������O�M���~�N�X�A�����ͤ@�ӹ�����AmountLevel ����C

            _school_code = hlpconfig.GetText("Content/@SchoolCode");   //���o�ǮեN�X�C
        }

        
        private List<AmountLevel> _levels = new List<AmountLevel>();
        /// <summary>
        /// ���o AmountLevel ���󪺶��X�C
        /// </summary>
        internal List<AmountLevel> Levels
        {
            get { return _levels; }
        }

        /// <summary>
        /// �ھ�ú�O���B���o�պA�ɤ��w�q�����B�ŶZ�A�Ǧ���X�۹諸����O�M���~�N�X�C
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
        /// �u����@�ӾǮժ���]�������z�L�ۦP�����~�N�X���O�ɤ~�|�Ψ�SchoolCode�C�Y�Ǯեu���鶡���ɡA�N���ݭn�]�wSchoolCode�CSchoolCode�ѨϥΪ̦b�e���W�]�w�A���׬��@�Ӧr���C
        /// </summary>
        public string SchoolCode
        {
            get { return _school_code; }
        }

        private ChainChargeOnus _charge_onus;
        /// <summary>
        /// �֭t�d����O�H
        /// </summary>
        public ChainChargeOnus ChargeOnus
        {
            get { return _charge_onus; }
        }
    }

    internal enum ChainChargeOnus
    {
        /// <summary>
        /// �S���H�Q�t�d�C
        /// </summary>
        None,
        /// <summary>
        /// ���ڤH�A�q�`���ǮաC
        /// </summary>
        Payee,
        /// <summary>
        /// �I�ڤH�A�q�`���ǥ͡C
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
