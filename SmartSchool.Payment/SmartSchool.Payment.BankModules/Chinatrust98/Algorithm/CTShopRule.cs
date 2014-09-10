using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsReceivalbe.BuildinBank.Chinatrust98.Algorithm
{
    public class CTShopRule
    {
        private string _bankCode = "";  //中國信託在超商的代碼，依金額20000元分為6H1 (<20000) 和 6H2 (>20000)
        //private string _expiredDateStringMMDD = "";   //截止日期的月日，是給人看的，出現再第三個條碼。
        private DateTime _expiredDate;   //截止日期的年月日。
        private decimal _amount;
        private string _shop_code;

        /// <summary>
        /// 建立超商條碼產生物件。
        /// </summary>
        /// <param name="virtualAccount">虛擬帳號。</param>
        /// <param name="expiredDate">繳費過期日。</param>
        /// <param name="amount">應繳金額，包含手續費。</param>
        /// <param name="shopCode">超商代碼。</param>
        public CTShopRule(VirtualAccount virtualAccount, DateTime expiredDate, decimal amount, string shopCode)
        {
            //虛擬帳號資訊。
            _va = virtualAccount;

            //應繳金額。
            _amount = amount;

            //超商的銀行代碼。
            //_bankCode = GetBankCode(_amount);
            _bankCode = shopCode;

            //繳費過期日。
            _expiredDate = expiredDate;

            //超商代碼。
            _shop_code = shopCode;
        }

        private VirtualAccount _va;
        public VirtualAccount VirtualAccount
        {
            get { return _va; }
        }

        public string GetCode1()
        {
            //過期日(6)+超商的銀行代碼(3)
            return GetExpiredDateString() + this._bankCode;
            //return "9912316H1";
        }

        public string GetCode2()
        {
            //ATM跨行匯款之虛擬帳號(16)
            return VirtualAccount.ToString();
        }

        public string GetCode3()
        {
            //列印日期(4) + 檢核碼(2) +  金額(9)
            // return _expiredDateStringMMDD + GetValidationCode() + GetBillString();
            return GetExpiredDateMMDD() + GetValidationCode() + GetBillString();
        }

        /**
         * 超商代碼在 AmountLevel 類別提供。
         */
        //private string GetBankCode(decimal bill)
        //{
        //    string result;

        //    if (bill >= 1 && bill <= 20000)
        //        result = "6H1";
        //    else if (bill >= 20001 && bill <= 40000)
        //        result = "6H2";
        //    else if (bill >= 40001 && bill <= 60000)
        //        result = "6H3";
        //    else throw new ArgumentException("金額不在指定的範圍內。");

        //    return result;
        //}

        /// <summary>
        /// 超商的收費期限字串，yymmdd為民國年月日。
        /// </summary>
        /// <returns></returns>
        private string GetExpiredDateString()
        {
            //DateTime expiredDate = VirtualAccount.ExpiredDate;

            //int year = (expiredDate.Year > 1911) ? (expiredDate.Year - 1911) : (expiredDate.Year);
            //year = year % 100; //取兩位
            //return Utility.GetWellFormedString(year.ToString(), 2, "") + GetExpiredDateMMDD();

            int year = (_expiredDate.Year > 1911) ? (_expiredDate.Year - 1911) : (_expiredDate.Year);
            year = year % 100; //取兩位
            return Utility.GetWellFormedString(year.ToString(), 2, "") + GetExpiredDateMMDD();

        }

        /// <summary>
        /// 截止日(月、日)。
        /// </summary>
        /// <returns></returns>
        private string GetExpiredDateMMDD()
        {
            //DateTime expiredDate = VirtualAccount.ExpiredDate;

            //return Utility.GetWellFormedString(expiredDate.Month.ToString(), 2, "") +
            //            Utility.GetWellFormedString(expiredDate.Day.ToString(), 2, "");

            return Utility.GetWellFormedString(_expiredDate.Month.ToString(), 2, "") +
                        Utility.GetWellFormedString(_expiredDate.Day.ToString(), 2, "");
        }

        /// <summary>
        /// 取得超商的檢查碼，共兩碼。
        /// 第一碼的規則為：各段Barcode’基數值’之加總值 除以11 後之’餘數’ (若餘數為0則放A,若餘數為10則放B)
        /// 第二碼計算公式：各段Barcode’偶數值’之加總值 除以11 後之’餘數’ (若餘數為0 則放X, 若餘數為10則放Y)
        /// </summary>
        /// <returns></returns>
        public string GetValidationCode()
        {
            int oddSum = 0;
            int evenSum = 0;

            //for Shop BarCode 1
            string bar1 = this.GetCode1();
            for (int i = 0; i < bar1.Length; i++)
            {
                int val = this.GetCharValue(bar1.Substring(i, 1));
                if (i % 2 == 0)
                    oddSum += val;
                else
                    evenSum += val;
            }

            //for Shop BarCode 2
            string bar2 = this.GetCode2();
            for (int i = 0; i < bar2.Length; i++)
            {
                int val = this.GetCharValue(bar2.Substring(i, 1));
                if (i % 2 == 0)
                    oddSum += val;
                else
                    evenSum += val;
            }

            //for Shop BarCode 3
            //string bar3 = this._expiredDateStringMMDD + "00" + GetBillString();    //中間加"00"是因為0不影響加總結果
            string bar3 = this.GetExpiredDateMMDD() + "00" + GetBillString();    //中間加"00"是因為0不影響加總結果
            for (int i = 0; i < bar3.Length; i++)
            {
                int val = this.GetCharValue(bar3.Substring(i, 1));
                if (i % 2 == 0)
                    oddSum += val;
                else
                    evenSum += val;
            }

            //計算第一碼，奇數和被11除的餘數，0則為A, 10則為B，其餘為數字。            
            int valCode1 = oddSum % 11;
            string validatationCode1 = (valCode1 == 0) ? "A" : ((valCode1 == 10) ? "B" : valCode1.ToString());

            //計算第二碼，偶數和被11除的餘數，0則為X, 10則為Y，其餘為原數字。     
            int valCode2 = evenSum % 11;
            string validatationCode2 = (valCode2 == 0) ? "X" : ((valCode2 == 10) ? "Y" : valCode2.ToString());

            return validatationCode1 + validatationCode2;
        }

        /// <summary>
        /// 超商的金額為九位數，要靠右左補零。
        /// </summary>
        /// <returns></returns>
        private string GetBillString()
        {
            return Utility.GetWellFormedString(_amount.ToString(), 9, "超商金額不能超過9位數！");
            //return "000007890";
        }

        /// <summary>
        /// 超商條碼中可能有英文字母，在計算檢核碼時須將其轉為數字計算。
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private int GetCharValue(string chr)
        {
            /*
              ’A’:1, ’B’:2, ’C’:3, ’D’:4, ’E’:5, ’F’:6, ’G’:7, ’H’:8, ’I’:9
              ’J’:1, ’K’:2, ’L’:3, ’M’:4, ’N’:5, ’O’:6, ’P’:7, ’Q’:8, ’R’:9, 
              ’S’:2 , ‘T’:3, ’U’:4, ’V’:5, ’W’;6, ’X’:7, ’Y’:8, ’Z’:9
             * */
            int result = -1;
            string flag = chr.ToUpper();

            if ((flag == "A") || (flag == "J") || (flag == "1"))
                result = 1;
            else if ((flag == "B") || (flag == "K") || (flag == "S") || (flag == "2"))
                result = 2;
            else if ((flag == "C") || (flag == "L") || (flag == "T") || (flag == "3"))
                result = 3;
            else if ((flag == "D") || (flag == "M") || (flag == "U") || (flag == "4"))
                result = 4;
            else if ((flag == "E") || (flag == "N") || (flag == "V") || (flag == "5"))
                result = 5;
            else if ((flag == "F") || (flag == "O") || (flag == "W") || (flag == "6"))
                result = 6;
            else if ((flag == "G") || (flag == "P") || (flag == "X") || (flag == "7"))
                result = 7;
            else if ((flag == "H") || (flag == "Q") || (flag == "Y") || (flag == "8"))
                result = 8;
            else if ((flag == "I") || (flag == "R") || (flag == "Z") || (flag == "9"))
                result = 9;
            else if (flag == "0")
                result = 0;

            return result;
        }
    }

}
