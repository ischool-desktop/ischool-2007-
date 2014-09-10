using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.BankModules.Chinatrust
{
    //參閱文件：郵局劃撥專戶日期檢核邏輯ALL.doc
    public class CTPostRule
    {
        private string bankCode = "";  //中國信託在郵局的代碼，依金額20000元分為50013957 (<20000) 和 50013944 (>20000)
        private DateTime expiredDate;
        private VirtualAccount _vir_account = null;
        private bool isCheckBill = true;
        private int _final_bill;

        /// <param name="virtualAccount">虛擬帳號。</param>
        /// <param name="expiredDate">截止日期</param>
        /// <param name="isCheckBill">是否要檢查金額，預設是要的。</param>
        public CTPostRule(VirtualAccount virtualAccount, DateTime expiredDate, int amount)
        {
            this.expiredDate = expiredDate;
            this._vir_account = virtualAccount;
            this.isCheckBill = true; //一定檢查金額，目前沒有其他選項。
            this._final_bill = amount;
            this.bankCode = GetBankCode(this._final_bill);
        }

        public string GetCode1()
        {
            //郵局劃撥的的銀行代碼(8)
            return this.bankCode;
            //return "19915601";
        }

        public string GetCode2()
        {
            //過期日(7) + 是否檢核金額(1) + ATM轉帳之虛擬帳號(15, 不含檢查碼) + 檢核碼(1)
            return this.GetExpiredDateString() + (this.isCheckBill ? "1" : "0") + _vir_account.ToString().Substring(0, 15) + this.GetValidationCode();
            //return "ABCDEFGHIKLMNPQR";
        }

        public string GetCode3()
        {
            //金額(11)
            return Utility.GetWellFormedString(this._final_bill.ToString(), 11, "郵局金額不能超過11位數！");
        }

        private string GetBankCode(int bill)
        {
            string result;

            if (bill >= 1 && bill <= 20000)
                result = "50013957";
            else if (bill >= 20001 && bill <= 40000)
                result = "50013944";
            else if (bill >= 40001 && bill <= 60000)
                result = "50013960";
            else throw new ArgumentException("金額不在指定的範圍內。");

            return result;
        }

        public string GetValidationCode()
        {
            //第一次驗證
            string tempCode1 = this.bankCode + this.GetExpiredDateString() + (this.isCheckBill ? "1" : "0") + _vir_account.ToString().Substring(0, 15);
            int[] ary1 = { 3, 7, 1 };
            int A = 0;
            for (int i = 0; i < tempCode1.Length; i++)
            {
                A += ary1[i % 3] * int.Parse(tempCode1.Substring(i, 1));
            }
            A = A % 10;

            //第二次驗證
            string billString = Utility.GetWellFormedString(this._final_bill.ToString(), 8, "金額不能超過8位數");
            int[] ary2 = { 8, 7, 6, 5, 4, 3, 2, 1 };
            int B = 0;
            for (int i = 0; i < 8; i++)
            {
                B += ary2[i] * int.Parse(billString.Substring(i, 1));
            }
            int result = 10 - ((A + B) % 10);
            if (result == 10) result = 0;

            return result.ToString();
        }

        /// <summary>
        /// 計算郵局代碼截止日期(7),
        /// </summary>
        /// <returns></returns>
        private string GetExpiredDateString()
        {
            int year = (this.expiredDate.Year >= 1911) ? (this.expiredDate.Year - 1911) : this.expiredDate.Year;
            year = year % 101;
            string month = Utility.GetWellFormedString(this.expiredDate.Month.ToString(), 2, "");
            string day = Utility.GetWellFormedString(this.expiredDate.Day.ToString(), 2, "");

            return Utility.GetWellFormedString(year.ToString(), 3, "") + month + day;
        }
    }
}
