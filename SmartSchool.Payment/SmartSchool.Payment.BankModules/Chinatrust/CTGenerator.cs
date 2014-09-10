using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;

namespace SmartSchool.Payment.BankModules.Chinatrust
{
    internal class CTGenerator : IBillCodeGenerator
    {
        public CTGenerator(BankConfig config, DateTime expiration)
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

        #region IBillGenerator 成員

        public List<BillCodeResult> Generate(List<BillCodeParameter> args)
        {
            ModulePreference mp = PreferenceManager.GetPreference(CTBankService.PreferenceIdentity);
            List<BillCodeResult> results = new List<BillCodeResult>();
            int nextSequence = mp.GetInteger("NextSequence", 0);
            int nowSeq1 = nextSequence;
            ConfigParser cparser = new ConfigParser(Config);
            ChainChargeOnus onus = cparser.ChargeOnus;
            string schoolCode = cparser.SchoolCode;

            if (onus == ChainChargeOnus.None)
                throw new PaymentModuleException("銀行組態中未決定手續費由誰負擔，所以無法產生繳費資料。", null);

            foreach (BillCodeParameter each in args)
            {
                BillCode account = new BillCode();

                AmountLevel alevel;
                int amount = each.Amount;
                alevel = cparser.GetAmountLevel(amount); //先依目前金額取得企業代碼。

                if (alevel == null) //判斷是否有此金額區段設定。
                    throw new PaymentModuleException(string.Format("銀行組態中並未設定此金額的企業代碼(金額：{0})。", amount), null);

                //這裡的金額就是原本「應繳金額」。
                VirtualAccount va = new VirtualAccount(alevel.EnterpriseCode, each.Amount, Expiration, nextSequence.ToString());

                if (!string.IsNullOrEmpty(schoolCode))
                    va.SetSchoolCode(schoolCode);

                //這裡的金額是「應繳金額」加上(可能)「超商」的手續費。
                CTShopRule shop = new CTShopRule(va, Expiration,
                    (onus == ChainChargeOnus.Payer) ? each.Amount + alevel.ShopCharge : each.Amount);

                //這裡的金額是「郵局」的，一般來說是固定的。
                CTPostRule post = new CTPostRule(va, Expiration,
                    (onus == ChainChargeOnus.Payer) ? each.Amount + alevel.PostCharge : each.Amount);

                account.VirtualAccount = va.ToString();
                account.Sequence =int.Parse(va.UniqueID);

                SupplyChainCode shopCode = new SupplyChainCode(SupplyChains.Shop);
                shopCode.Add(shop.GetCode1());
                shopCode.Add(shop.GetCode2());
                shopCode.Add(shop.GetCode3());

                account.SupplyChains.Add(shopCode);

                SupplyChainCode postCode = new SupplyChainCode(SupplyChains.Post);
                postCode.Add(post.GetCode1());
                postCode.Add(post.GetCode2());
                postCode.Add(post.GetCode3());

                account.SupplyChains.Add(postCode);

                BillCodeResult result = new BillCodeResult(each.Identity);
                result.BillCode = account;
                results.Add(result);

                nextSequence++;
                if (nextSequence > CTBankService.MaxSequence)
                    nextSequence = 0;
            }

            ModulePreference nmp = PreferenceManager.GetPreference(CTBankService.PreferenceIdentity);
            int nowSeq2 = nmp.GetInteger("NextSequence", 0);

            if (nowSeq1 == nowSeq2)
            {
                mp.SetInteger("NextSequence", nextSequence);
                PreferenceManager.SavePreference(mp);
            }
            else
                throw new PaymentModuleException("銀行模組設定已被其他使用者變更，請重新產生資料", null);

            return results;
        }

        #endregion
    }
}
