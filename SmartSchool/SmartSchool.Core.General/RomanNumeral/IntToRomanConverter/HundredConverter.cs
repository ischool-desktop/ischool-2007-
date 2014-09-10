using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.RomanNumeral.IntToRomanConverter
{
    public class HundredConverter:IIRConverter
    {
        #region IIRConverter жин√

        public string Convert(int singleInt)
        {
            return Util.GetSymbol(singleInt, Symbol.ROMAN_NUMERAL_100, Symbol.ROMAN_NUMERAL_500, Symbol.ROMAN_NUMERAL_1000);
        }

        #endregion
    }
}
