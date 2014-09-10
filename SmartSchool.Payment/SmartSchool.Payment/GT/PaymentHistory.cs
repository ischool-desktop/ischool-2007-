using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.GT
{
    internal class PaymentHistory
    {
        private string _identity;
        public string Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        private string _ref_payment_detail_id;

        public string RefPaymentDetailID
        {
            get { return _ref_payment_detail_id; }
            set { _ref_payment_detail_id = value; }
        }

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private int _paid_amount;
        public int PaidAmount
        {
            get { return _paid_amount; }
            set { _paid_amount = value; }
        }

        private bool _paid;
        public bool Paid
        {
            get { return _paid; }
            set { _paid = value; }
        }

        private bool _cancelled;

        public bool Cancelled
        {
            get { return _cancelled; }
            set { _cancelled = value; }
        }

        private TransactionCollection _trans = new TransactionCollection();
        public TransactionCollection Transactions
        {
            get { return _trans; }
        }
    }
}
