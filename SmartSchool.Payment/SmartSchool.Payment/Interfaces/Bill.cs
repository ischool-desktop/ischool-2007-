using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.Interfaces
{
    public class Bill
    {
        private BillCode _account;
        public BillCode Account
        {
            get { return _account; }
            set { _account = value; }
        }

        private DateTime _expiration;
        public DateTime Expiration
        {
            get { return _expiration; }
            set { _expiration = value; }
        }

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private Dictionary<string, int> _amount_details = new Dictionary<string, int>();
        public Dictionary<string, int> AmountDetails
        {
            get { return _amount_details; }
        }

        private Dictionary<string, string> _merge_fields = new Dictionary<string, string>();
        public Dictionary<string, string> MergeFields
        {
            get { return _merge_fields; }
        }

    }
}
