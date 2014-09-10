using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.RomanNumeral
{
    public class Util
    {
        public static string GetSymbol(int singleInt, string s1, string s5, string s10)
        {
            if (singleInt == 9)
                return s1 + s10;

            if (singleInt == 4)
                return s1 + s5;

            string s = "";
            int i5 = singleInt / 5;
            int im = singleInt % 5;           

            if (i5 == 1)
                s = s5;
            for (int i = 0; i < im; i++)
                s += s1;
            return s;
        }
    }
}
