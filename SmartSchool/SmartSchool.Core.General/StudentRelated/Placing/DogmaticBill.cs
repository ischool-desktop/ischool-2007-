using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.StudentRelated.Placing.Rule;
using SmartSchool.StudentRelated.Placing.DataSource;
using SmartSchool.StudentRelated.Placing.Score;
using SmartSchool.RomanNumeral.IntToRomanConverter;

namespace SmartSchool.StudentRelated.Placing
{
    public class DogmaticBill
    {
        public static string GetRomanNumber(string integer)
        {
            int i;
            if (!int.TryParse(integer, out i))
                return integer;
            return Less5000Converter.Convert(i);
            //string[] r = new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
            //int i;
            //if (!int.TryParse(integer, out i))
            //    return integer;
            //if (i > 12)
            //    return integer;
            //return r[i];
        }
    }
}
