using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.Interfaces
{
    public class BillCodeParameter
    {
        public BillCodeParameter(string identity)
        {
            _identity = identity;
        }

        private string _identity;
        public string Identity
        {
            get { return _identity; }
        }

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

    }
}
