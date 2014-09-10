using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment
{
    public class PaymentModuleException : Exception
    {
        public PaymentModuleException(string message, Exception ex)
            : base(message, ex)
        {
        }

        private string _detail;

        public string DetailMessage
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }
}
