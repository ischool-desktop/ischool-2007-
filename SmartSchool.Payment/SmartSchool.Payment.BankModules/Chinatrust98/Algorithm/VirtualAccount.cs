using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsReceivalbe.BuildinBank.Chinatrust98.Algorithm
{
    public class VirtualAccount
    {
        private string _origin_unique_id;

        public VirtualAccount(string cropCode, decimal bill, DateTime expriedDate, string uniqueId)
        {
            _corpCode = cropCode;
            _bill = bill;
            _expiredDate = expriedDate;
            _origin_unique_id = uniqueId;
            _uniqueId = Utility.GetWellFormedString(_origin_unique_id, 6, "");
        }

        public void SetSchoolCode(string schoolCode)
        {
            _school_code = schoolCode;
            _uniqueId = SchoolCode + Utility.GetWellFormedString(_origin_unique_id, 5, "");
        }

        private string _school_code;
        public string SchoolCode
        {
            get { return _school_code; }
        }

        private string _corpCode;
        public string CorporationCode
        {
            get { return _corpCode; }
        }

        private decimal _bill;
        private decimal Bill
        {
            get { return _bill; }
        }

        private DateTime _expiredDate;
        private DateTime ExpiredDate
        {
            get { return _expiredDate; }
        }

        private string _uniqueId = "";
        public string UniqueID
        {
            get { return _uniqueId; }
        }

        public string GetVirtualAccount()
        {
            //ATM 虛擬帳號規則為：
            //企網代碼(5) + 期限年(1) + 期限日(3) + 編號(6) + 檢核碼(1) = 16碼
            return CorporationCode + GetExprYearString() + GetExprDayString() + UniqueID + GetValidationCode();
        }

        public override string ToString()
        {
            return GetVirtualAccount();
        }

        /// <summary>
        /// 取得期限年
        /// </summary>
        /// <returns></returns>
        private string GetExprYearString()
        {
            return (ExpiredDate.Year % 10).ToString();
        }

        /// <summary>
        /// 取得期限日
        /// </summary>
        /// <returns></returns>
        private string GetExprDayString()
        {
            return Utility.GetWellFormedString(ExpiredDate.DayOfYear.ToString(), 3, "");
        }

        /// <summary>
        /// 計算 ATM虛擬帳號的檢核碼
        /// </summary>
        /// <param name="corpCode"></param>
        /// <param name="id"></param>
        /// <param name="bill"></param>
        /// <returns></returns>
        public string GetValidationCode()
        {
            //第一次驗證，將下列各位數依序乘以 ( 3,7,1,3,7,1,3,7,1, ...... )
            string tempCode = CorporationCode + GetExprYearString() + GetExprDayString() + UniqueID;
            int[] ary1 = { 3, 7, 1 };
            int x1 = 0;
            for (int i = 0; i < tempCode.Length; i++)
            {
                x1 += ary1[i % 3] * int.Parse(tempCode.Substring(i, 1));
            }
            x1 = x1 % 10;

            //第二次驗證，取得金額，各個位數依序乘以8的降冪數字(8,7,6,5,4,3,2,1)
            string billString = Utility.GetWellFormedString(Bill.ToString(), 10, "金額不能超過10位數");
            int[] ary2 = { 8, 7, 6, 5, 4, 3, 2, 1, 8, 7 };
            int x2 = 0;
            for (int i = 0; i < billString.Length; i++)
            {
                x2 += ary2[i] * int.Parse(billString.Substring(i, 1));
            }

            int result = 10 - ((x1 + x2) % 10);

            result = (result == 10) ? 0 : result;

            return result.ToString();
        }

        //ATM轉帳時的銀行代碼，中國信託為 822
        private static string BankCode
        {
            get { return "822"; }
        }
    }
}
