using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SmartSchool.Payment.BankModules.Chinatrust
{
    public class Utility
    {
        /// <summary>
        /// 將輸入的字串，靠右左補零，直到指定的位數。
        /// </summary>
        /// <param name="str">輸入字串</param>
        /// <param name="power">要填滿的位數</param>
        /// <param name="exceptionMessage">例外時的訊息</param>
        /// <returns>補零完後的字串</returns>
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
