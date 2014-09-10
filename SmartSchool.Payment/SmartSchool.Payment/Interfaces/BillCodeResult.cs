using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.Interfaces
{
    public class BillCodeResult
    {
        public BillCodeResult(string identity)
        {
            _identity = identity;
        }

        private string _identity;
        public string Identity
        {
            get { return _identity; }
        }

        private BillCode _bill_code;
        public BillCode BillCode
        {
            get { return _bill_code; }
            set { _bill_code = value; }
        }

    }
}
