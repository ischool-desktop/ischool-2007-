using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.RomanNumeral.IntToRomanConverter
{
    public class SingleConverter:IIRConverter
    {
        #region IIRConverter жин√

        public string Convert(int singleInt)
        {
            return Util.GetSymbol(singleInt, Symbol.ROMAN_NUMERAL_1, Symbol.ROMAN_NUMERAL_5, Symbol.ROMAN_NUMERAL_10);
        }

        #endregion
    }
}
