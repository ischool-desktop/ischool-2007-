using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.AccessControl
{
    public class FeatureCodeAttribute : Attribute
    {
        public FeatureCodeAttribute(string code)
        {
            _code = code;
        }

        private string _code;
        public string FeatureCode
        {
            get { return _code; }
        }

        public static string GetCode(Type featureType)
        {
            FeatureCodeAttribute fcode = Attribute.GetCustomAttribute(featureType, typeof(FeatureCodeAttribute)) as FeatureCodeAttribute;

            if (fcode == null)
                return string.Empty;
            else
                return fcode.FeatureCode;
        }
    }
}
