using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;

namespace SmartSchool.Payment.GT
{
    internal class PaymentButtonItem : ButtonItem
    {
        public PaymentButtonItem(GT.Payment payment)
        {
            Payment = payment;
            OptionGroup = "Payment";
            Text = GetDisplayName();
        }

        private Payment _payment;
        public Payment Payment
        {
            get { return _payment; }
            private set { _payment = value; }
        }

        public void SyncDisplayText()
        {
            Text = GetDisplayName();
            Refresh();
        }

        private string GetDisplayName()
        {
            return Payment.Name;
        }
    }
}
