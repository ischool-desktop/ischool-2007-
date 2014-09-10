using System;
using System.Collections.Generic;
using System.Text;

namespace FirstBankPayment.FirstBank.Algorithm
{
    public class Utility
    {
        /// <summary>
        /// �N��J���r��A�a�k���ɹs�A������w����ơC
        /// </summary>
        /// <param name="str">��J�r��</param>
        /// <param name="power">�n�񺡪����</param>
        /// <param name="exceptionMessage">�ҥ~�ɪ��T��</param>
        /// <returns>�ɹs���᪺�r��</returns>
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
