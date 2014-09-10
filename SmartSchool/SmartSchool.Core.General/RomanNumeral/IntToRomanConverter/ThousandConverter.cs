using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.RomanNumeral.IntToRomanConverter
{
    public class ThousandConverter : IIRConverter
    {
        #region IIRConverter жин√

        public string Convert(int singleInt)
        {
            string s = "";
            for (int i = 0; i < singleInt; i++)
            {
                s += Symbol.ROMAN_NUMERAL_1000;
            }
            return s;
        }

        #endregion
    }
}
