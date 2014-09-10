using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Feature
{
    public class QueryRequestAttribute : Attribute
    {
        private int _ReTryTimes = 3;
        public int ReTryTimes
        {
            get { return _ReTryTimes; }
        }
        public QueryRequestAttribute() { }
        public QueryRequestAttribute(int retryTimes) 
        {
            _ReTryTimes = retryTimes;
        }
    }
}
