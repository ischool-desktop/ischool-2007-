using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.RomanNumeral
{
    public class Symbol
    {
        public const string ROMAN_NUMERAL_1000 = "M";
        public const string ROMAN_NUMERAL_500 = "D";
        public const string ROMAN_NUMERAL_100 = "C";
        public const string ROMAN_NUMERAL_50 = "L";
        public const string ROMAN_NUMERAL_10 = "X";
        public const string ROMAN_NUMERAL_5 = "V";
        public const string ROMAN_NUMERAL_1 = "I";
        public const string ROMAN_NUMERAL_0 = "";

        public const int INTEGER_I = 1;
        public const int INTEGER_V = 5;
        public const int INTEGER_X = 10;
        public const int INTEGER_L = 50;
        public const int INTEGER_C = 100;
        public const int INTEGER_D = 500;
        public const int INTEGER_M = 1000;

        public static int GetSymbolInteger(string RomanNumeral)
        {
            switch (RomanNumeral)
            {
                case "I":
                    return INTEGER_I;
                case "V":
                    return INTEGER_V;
                case "X":
                    return INTEGER_X;
                case "L":
                    return INTEGER_L;
                case "C":
                    return INTEGER_C;
                case "D":
                    return INTEGER_D;
                case "M":
                    return INTEGER_M;
                default:
                    return 0;
            }
        }

        public static bool Valid(string romanNumeral)
        {
            //foreach (string s in romanNumeral)
            //{
            //    //if(s.Equals("
            //}
            return true;
        }
    }
}
