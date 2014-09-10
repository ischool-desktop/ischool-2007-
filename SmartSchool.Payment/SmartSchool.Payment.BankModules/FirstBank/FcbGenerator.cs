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
        /// Constructor。
        /// </summary>
        /// <param name="config">代表某銀行的組態值。(組態值格式可參考 Config_Sample.xml ) </param>
        /// <param name="expiration">繳費截止日</param>
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

        #region IBillGenerator 成員

        /// <summary>
        /// 根據傳入的 BillCodeParameter 物件集合
        /// </summary>
        /// <param name="args">BillCodeParameter 物件集合。BillCodeParameter物件只記錄了學生</param>
        /// <returns>BillCodeResult 物件的集合</returns>
        public List<BillCodeResult> Generate(List<BillCodeParameter> args)
        {            
            ModulePreference mp = PreferenceManager.GetPreference(FcbBankService.PreferenceIdentity);    //取得某銀行的偏好設定值。
            List<BillCodeResult> results = new List<BillCodeResult>();
            int nextSequence = mp.GetInteger("NextSequence", 0);    //從偏好設定中取得目前的流水號。
            int nowSeq1 = nextSequence;
            ConfigParser cparser = new ConfigParser(Config);        //解析某銀行的組態值。
            ChainChargeOnus onus = cparser.ChargeOnus;              //從組態值中找出是由學生或學校負擔手續費。
            string schoolCode = cparser.SchoolCode;                 //取得學校代碼。

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

                account.VirtualAccount = va.ToString();
                account.Sequence = int.Parse(va.UniqueID);

                FcbShopRule shop = new FcbShopRule(va, Expiration, each.Amount, cparser.ChargeOnus.ToString());

                //產生超商所需要的三個代碼
                SupplyChainCode shopCode = new SupplyChainCode(SupplyChains.Shop);
                shopCode.Add(shop.GetCode1());
                shopCode.Add(shop.GetCode2());
                shopCode.Add(shop.GetCode3());

                account.SupplyChains.Add(shopCode);


                //這裡的金額是「郵局」的，一般來說是固定的。
                //FcbPostRule post = new CTPostRule(va, Expiration,
                //    (onus == ChainChargeOnus.Payer) ? each.Amount + alevel.PostCharge : each.Amount);

                //產生郵局通路所需的三個代碼。若無郵局通路則此段省略。
                //SupplyChainCode postCode = new SupplyChainCode(SupplyChains.Post);
                //postCode.Add(post.GetCode1());
                //postCode.Add(post.GetCode2());
                //postCode.Add(post.GetCode3());

                //account.SupplyChains.Add(postCode);

                //儲存代碼結果的物件
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
                throw new PaymentModuleException("銀行模組設定已被其他使用者變更，請重新產生資料", null);

            return results;
        }

        #endregion
    }
}
