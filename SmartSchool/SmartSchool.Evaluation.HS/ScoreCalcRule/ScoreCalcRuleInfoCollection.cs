using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SmartSchool.Evaluation.ScoreCalcRule
{
    public class ScoreCalcRuleInfoCollection : IEnumerable<ScoreCalcRuleInfo>
    {
        private Dictionary<string, ScoreCalcRuleInfo> _Items;
        internal ScoreCalcRuleInfoCollection(Dictionary<string, ScoreCalcRuleInfo> items)
        {
            _Items = items;
        }

        public ScoreCalcRuleInfo this[string ID]
        {
            get
            {
                if (_Items.ContainsKey(ID))
                    return _Items[ID];
                else
                    return null;
            }
        }

        #region IEnumerable<ScoreCalcRuleInfo> ����

        public IEnumerator<ScoreCalcRuleInfo> GetEnumerator()
        {
            return ((IEnumerable<ScoreCalcRuleInfo>)_Items.Values).GetEnumerator();
        }

        #endregion

        #region IEnumerable ����

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.Values.GetEnumerator();
        }

        #endregion
    }
}
