using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.RomanNumeral.IntToRomanConverter
{
    public class Less5000Converter
    {

        #region IIRConverter жин√

        public static string Convert(int integer)
        {
            int i = integer % 5000;
            int thousand = i / 1000;
            int hundred = (i / 100) % 10;
            int ten = (i % 100) / 10;
            int single = (i % 10);

            ThousandConverter tc = new ThousandConverter();
            HundredConverter hc = new HundredConverter();
            TenConverter tenc = new TenConverter();
            SingleConverter sc = new SingleConverter();

            return tc.Convert(thousand) + hc.Convert(hundred) + tenc.Convert(ten) + sc.Convert(single);
        }

        #endregion
    }
}
