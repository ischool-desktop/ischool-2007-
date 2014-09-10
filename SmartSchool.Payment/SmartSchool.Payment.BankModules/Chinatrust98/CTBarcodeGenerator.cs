using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AccountsReceivalbe.BuildinBank.Chinatrust98.Algorithm;
using AccountsReceivalbe.BuildinBank.ChinatrustCommon;
using SmartSchool.Payment.Interfaces;

namespace AccountsReceivalbe.BuildinBank.Chinatrust98
{
    internal class CTBarcodeGenerator : IBillCodeGenerator
    {
        private DateTime Expiration;
        private ConfigParser Config;

        public CTBarcodeGenerator(DateTime expiration, BankConfig bankConfig)
        {
            Expiration = expiration;
            Config = new ConfigParser(bankConfig.Content);
        }

        #region IBarcodeGenerator 成員

        public List<BillCodeResult> Generate(List<BillCodeParameter> args)
        {
            List<BillCodeResult> results = new List<BillCodeResult>();
            int nextSequence = SequenceRecord.GetNextSequence();
            int originSequence = nextSequence;
            PostChargeOnus onus = Config.ChargeOnus;
            string schoolCode = Config.SchoolCode;

            if (onus == PostChargeOnus.None)
                throw new ArgumentException("銀行組態中未決定手續費由誰負擔，所以無法產生繳費資料。");

            foreach (BillCodeParameter each in args)
            {
                BillCodeResult barcode = new BillCodeResult(each.Identity);
                BillCode code = new BillCode();

                AmountLevel alevel;
                decimal amount = each.Amount;
                alevel = Config.GetAmountLevel(amount); //先依目前金額取得企業代碼。

                if (alevel == null) //判斷是否有此金額區段設定。
                    throw new ArgumentException(string.Format("銀行組態中並未設定此金額的企業代碼(金額：{0})。", amount));

                //這裡的金額就是原本「應繳金額」。
                VirtualAccount va = new VirtualAccount(alevel.EnterpriseCode, each.Amount, Expiration, nextSequence.ToString());

                if (!string.IsNullOrEmpty(schoolCode))
                    va.SetSchoolCode(schoolCode);

                //這裡的金額是「應繳金額」加上(可能)「超商」的手續費。
                //CTShopRule shop = new CTShopRule(va, Expiration,
                //    (onus == ChainChargeOnus.Payer) ? each.Amount + alevel.ShopCharge : each.Amount);
                /**
                 * 98超商新邏輯，在超商的部份，手續費一律外加。
                 * 所以條碼上的金額與應繳金額一定相同，手續費超商會另外收。
                 */
                CTShopRule shop = new CTShopRule(va, Expiration, each.Amount, alevel.ShopCode);

                //這裡的金額是「郵局」的，一般來說是固定的。
                CTPostRule post = new CTPostRule(va, Expiration, CalculatePostAmount(onus, each), alevel.PostCode);

                code.VirtualAccount = va.ToString();
                code.Sequence = int.Parse(va.UniqueID);

                SupplyChainCode shopCode = new SupplyChainCode(SupplyChains.Shop);
                shopCode.Add(shop.GetCode1());
                shopCode.Add(shop.GetCode2());
                shopCode.Add(shop.GetCode3());

                code.SupplyChains.Add(shopCode);

                SupplyChainCode postCode = new SupplyChainCode(SupplyChains.Post);

                postCode.Add(post.GetCode1());
                postCode.Add(post.GetCode2());
                postCode.Add(post.GetCode3());

                code.SupplyChains.Add(postCode);

                barcode.BillCode = code;

                results.Add(barcode);

                nextSequence++;

                if (nextSequence > CTBankService.MaxSequence)
                    nextSequence = 0;
            }

            SequenceRecord.SaveSequence(originSequence, nextSequence);

            return results;
        }

        /// <summary>
        /// 郵局的應繳金額會依據「手續費」由誰負擔而不同。
        /// </summary>
        /// <param name="onus"></param>
        /// <param name="each"></param>
        /// <returns></returns>
        private decimal CalculatePostAmount(PostChargeOnus onus, BillCodeParameter each)
        {
            if (onus == PostChargeOnus.Payer) //由付款人負擔。
                return each.Amount + CalculatePostCharge(each.Amount);
            else //由收款人負擔。
                return each.Amount;
        }

        /// <summary>
        /// 計算郵局手續費。
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private decimal CalculatePostCharge(decimal amount)
        {
            return 15;
            //if (amount <= 1000)
            //    return 10; //1000元以下手續費是10元。
            //else
            //    return 15; //1000元以上手續費是15元。
        }

        #endregion
    }
}
