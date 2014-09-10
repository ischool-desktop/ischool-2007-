using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.Interfaces
{
    public class SupplyChainCode
    {
        public SupplyChainCode(SupplyChains chainType)
        {
            _chain_type = chainType;
        }

        private SupplyChains _chain_type = SupplyChains.None;
        /// <summary>
        /// 取得條碼的通路別。
        /// </summary>
        public SupplyChains ChainType
        {
            get { return _chain_type; }
        }

        private List<string> _codes = new List<string>();
        /// <summary>
        /// 通路的條碼清單。
        /// </summary>
        public IList<string> Codes
        {
            get { return _codes.AsReadOnly(); }
        }

        public void Add(string code)
        {
            _codes.Add(code);
        }

        public void Clear()
        {
            _codes.Clear();
        }

        public int Count
        {
            get { return _codes.Count; }
        }
    }

    public class SupplyChainCodeCollection
    {
        private Dictionary<SupplyChains, SupplyChainCode> _chains;

        public SupplyChainCodeCollection()
        {
            _chains = new Dictionary<SupplyChains, SupplyChainCode>();
        }

        public void Add(SupplyChainCode chainCode)
        {
            _chains.Add(chainCode.ChainType, chainCode);
        }

        public void Clear()
        {
            _chains.Clear();
        }

        public int Count
        {
            get { return _chains.Count; }
        }

        public bool Contains(SupplyChains chainType)
        {
            return _chains.ContainsKey(chainType);
        }

        public SupplyChainCode GetCode(SupplyChains supplyChain)
        {
            return _chains[supplyChain];
        }
    }
}
