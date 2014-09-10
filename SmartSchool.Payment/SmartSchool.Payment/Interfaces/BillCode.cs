using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.Interfaces
{
    public class BillCode
    {
        public BillCode()
        {
            SupplyChains = new SupplyChainCodeCollection();
        }

        private int _sequence;
        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        private string _virtual_account;
        public string VirtualAccount
        {
            get { return _virtual_account; }
            set { _virtual_account = value; }
        }

        private SupplyChainCodeCollection _supply_chains;
        public SupplyChainCodeCollection SupplyChains
        {
            get { return _supply_chains; }
            private set { _supply_chains = value; }
        }
    }
}
