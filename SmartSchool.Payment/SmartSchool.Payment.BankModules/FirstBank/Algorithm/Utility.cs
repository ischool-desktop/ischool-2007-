using System;
using System.Collections.Generic;
using System.Text;

namespace FirstBankPayment.FirstBank.Algorithm
{
    public class Utility
    {
        /// <summary>
        /// 盢块﹃綼オ干箂﹚计
        /// </summary>
        /// <param name="str">块﹃</param>
        /// <param name="power">璶恶骸计</param>
        /// <param name="exceptionMessage">ㄒ癟</param>
        /// <returns>干箂Ч﹃</returns>
        public static string GetWellFormedString(string strOrig, int power, string exceptionMessage)
        {
            string str = strOrig;

            if (str.Length > power)
                throw new Exception(exceptionMessage);

            if (str.Length < power)
            {
                int size = power - str.Length;
                for (int i = 0; i < size; i++)
                {
                    str = "0" + str;
                }
            }
            return str;
        }
    }
}
