using System;
using System.Collections.Generic;
using System.Text;

namespace FirstBankPayment.FirstBank.Algorithm
{
    /// <summary>
    /// 此類別負責產生 一銀 虛擬帳號。也包含產生驗證碼的規則。
    /// </summary>
    public class VirtualAccount
    {
        private string _origin_unique_id;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cropCode">企業代碼</param>
        /// <param name="bill">繳款金額</param>
        /// <param name="expriedDate">截止日期</param>
        /// <param name="uniqueId">唯一識別碼。目前採用 小於 99999的流水號。</param>
        public VirtualAccount(string cropCode, int bill, DateTime expriedDate, string uniqueId)
        {
            _corpCode = cropCode;
            _bill = bill;
            _expiredDate = expriedDate;
            _origin_unique_id = uniqueId;
            _uniqueId = Utility.GetWellFormedString(_origin_unique_id, 6, "");
        }

        /// <summary>
        /// 如果有學校代碼時，那麼產生ATM虛擬帳號的第10碼為學校代碼。
        /// </summary>
        /// <param name="schoolCode"></param>
        public void SetSchoolCode(string schoolCode)
        {
            _school_code = schoolCode;
            _uniqueId = SchoolCode + Utility.GetWellFormedString(_origin_unique_id, 5, "");
        }


        private string _school_code;
        /// <summary>
        /// 只有當一個學校的日夜間部都透過相同的企業代碼收費時才會用到SchoolCode。若學校只有日間部時，就不需要設定SchoolCode。SchoolCode由使用者在畫面上設定，長度為一個字元。
        /// </summary>
        public string SchoolCode
        {
            get { return _school_code; }
        }

        private string _corpCode;
        /// <summary>
        /// 企業代碼。
        /// </summary>
        public string CorporationCode
        {
            get { return _corpCode; }
        }

        private int _bill;
        /// <summary>
        /// 繳費金額
        /// </summary>
        private int Bill
        {
            get { return _bill; }
        }

        private DateTime _expiredDate;
        /// <summary>
        /// 繳費截止日
        /// </summary>
        private DateTime ExpiredDate
        {
            get { return _expiredDate; }
        }

        private string _uniqueId = "";
        public string UniqueID
        {
            get { return _uniqueId; }
        }

        /// <summary>
        /// 產生第一銀行 虛擬代碼，共16碼。
        /// 企網代碼(5) + 期限年(1) + 期限日(3) + 編號(6) + 檢核碼(1) = 16碼
        /// </summary>
        /// <returns></returns>
        public string GetVirtualAccount()
        {
            //虛擬帳號規則為：
            //企網代碼(5) + 期限年(1) + 期限日(3) + 編號(6) + 檢核碼(1) = 16碼
            return CorporationCode + GetExprYearString() + GetExprDayString() + UniqueID + GetValidationCode();
        }

        /// <summary>
        /// 取得第一銀行 虛擬代碼，共16碼。
        /// 企網代碼(5) + 期限年(1) + 期限日(3) + 編號(6) + 檢核碼(1) = 16碼
        /// </summary>
        /// <returns></returns>
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
            string billString = Utility.GetWellFormedString(Bill.ToString(), 8, "金額不能超過8位數");
            int[] ary2 = { 8, 7, 6, 5, 4, 3, 2, 1 };
            int x2 = 0;
            for (int i = 0; i < billString.Length; i++)
            {
                x2 += ary2[i] * int.Parse(billString.Substring(i, 1));
            }

            int result = 10 - ((x1 + x2) % 10);

            result = (result == 10) ? 0 : result;

            return result.ToString();
        }

        //ATM轉帳時的銀行代碼，第一銀行為 007
        private static string BankCode
        {
            get { return "007"; }
        }

    }
}
