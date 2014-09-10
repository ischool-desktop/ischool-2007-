using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;
using SmartSchool.Payment;
using FirstBankPayment.FirstBank.Algorithm;

namespace FirstBankPayment.FirstBank
{
    internal class FcbGenerator : IBillCodeGenerator
    {
       /// <summary>
        /// Constructor�C
        /// </summary>
        /// <param name="config">�N��Y�Ȧ檺�պA�ȡC(�պA�Ȯ榡�i�Ѧ� Config_Sample.xml ) </param>
        /// <param name="expiration">ú�O�I���</param>
        public FcbGenerator(BankConfig config, DateTime expiration)
        {
            _expiration = expiration;
            _config = config;
        }

        private DateTime _expiration;
        public DateTime Expiration
        {
            get { return _expiration; }
        }

        private BankConfig _config;
        public BankConfig Config
        {
            get { return _config; }
        }

        #region IBillGenerator ����

        /// <summary>
        /// �ھڶǤJ�� BillCodeParameter ���󶰦X
        /// </summary>
        /// <param name="args">BillCodeParameter ���󶰦X�CBillCodeParameter����u�O���F�ǥ�</param>
        /// <returns>BillCodeResult ���󪺶��X</returns>
        public List<BillCodeResult> Generate(List<BillCodeParameter> args)
        {            
            ModulePreference mp = PreferenceManager.GetPreference(FcbBankService.PreferenceIdentity);    //���o�Y�Ȧ檺���n�]�w�ȡC
            List<BillCodeResult> results = new List<BillCodeResult>();
            int nextSequence = mp.GetInteger("NextSequence", 0);    //�q���n�]�w�����o�ثe���y�����C
            int nowSeq1 = nextSequence;
            ConfigParser cparser = new ConfigParser(Config);        //�ѪR�Y�Ȧ檺�պA�ȡC
            ChainChargeOnus onus = cparser.ChargeOnus;              //�q�պA�Ȥ���X�O�ѾǥͩξǮխt�����O�C
            string schoolCode = cparser.SchoolCode;                 //���o�ǮեN�X�C

            if (onus == ChainChargeOnus.None)
                throw new PaymentModuleException("�Ȧ�պA�����M�w����O�ѽ֭t��A�ҥH�L�k����ú�O��ơC", null);

            foreach (BillCodeParameter each in args)
            {
                BillCode account = new BillCode();

                AmountLevel alevel;
                int amount = each.Amount;
                alevel = cparser.GetAmountLevel(amount); //���̥ثe���B���o���~�N�X�C

                if (alevel == null) //�P�_�O�_�������B�Ϭq�]�w�C
                    throw new PaymentModuleException(string.Format("�Ȧ�պA���å��]�w�����B�����~�N�X(���B�G{0})�C", amount), null);

                //�o�̪����B�N�O�쥻�u��ú���B�v�C
                VirtualAccount va = new VirtualAccount(alevel.EnterpriseCode, each.Amount, Expiration, nextSequence.ToString());

                if (!string.IsNullOrEmpty(schoolCode))
                    va.SetSchoolCode(schoolCode);

                account.VirtualAccount = va.ToString();
                account.Sequence = int.Parse(va.UniqueID);

                FcbShopRule shop = new FcbShopRule(va, Expiration, each.Amount, cparser.ChargeOnus.ToString());

                //���ͶW�өһݭn���T�ӥN�X
                SupplyChainCode shopCode = new SupplyChainCode(SupplyChains.Shop);
                shopCode.Add(shop.GetCode1());
                shopCode.Add(shop.GetCode2());
                shopCode.Add(shop.GetCode3());

                account.SupplyChains.Add(shopCode);


                //�o�̪����B�O�u�l���v���A�@��ӻ��O�T�w���C
                //FcbPostRule post = new CTPostRule(va, Expiration,
                //    (onus == ChainChargeOnus.Payer) ? each.Amount + alevel.PostCharge : each.Amount);

                //���Ͷl���q���һݪ��T�ӥN�X�C�Y�L�l���q���h���q�ٲ��C
                //SupplyChainCode postCode = new SupplyChainCode(SupplyChains.Post);
                //postCode.Add(post.GetCode1());
                //postCode.Add(post.GetCode2());
                //postCode.Add(post.GetCode3());

                //account.SupplyChains.Add(postCode);

                //�x�s�N�X���G������
                BillCodeResult result = new BillCodeResult(each.Identity);
                result.BillCode = account;
                results.Add(result);

                nextSequence++;
                if (nextSequence > FcbBankService.MaxSequence)
                    nextSequence = 0;
            }

            ModulePreference nmp = PreferenceManager.GetPreference(FcbBankService.PreferenceIdentity);
            int nowSeq2 = nmp.GetInteger("NextSequence", 0);

            if (nowSeq1 == nowSeq2)
            {
                mp.SetInteger("NextSequence", nextSequence);
                PreferenceManager.SavePreference(mp);
            }
            else
                throw new PaymentModuleException("�Ȧ�Ҳճ]�w�w�Q��L�ϥΪ��ܧ�A�Э��s���͸��", null);

            return results;
        }

        #endregion
    }
}
