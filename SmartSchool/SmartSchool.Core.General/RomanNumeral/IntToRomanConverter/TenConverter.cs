using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.RomanNumeral.IntToRomanConverter
{
    public class TenConverter:IIRConverter
    {
        #region IIRConverter жин√

        public string Convert(int singleInt)
        {
            return Util.GetSymbol(singleInt, Symbol.ROMAN_NUMERAL_10, Symbol.ROMAN_NUMERAL_50, Symbol.ROMAN_NUMERAL_100);
        }

        #endregion
    }
}
