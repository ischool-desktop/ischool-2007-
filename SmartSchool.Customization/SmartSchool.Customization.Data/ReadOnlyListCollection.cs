using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SmartSchool.Customization.Data
{
    public class ReadOnlyListCollection<KeyType, ListType> : IEnumerable<List<ListType>>
    {

        protected Dictionary<KeyType,List< ListType>> _Items;
        internal ReadOnlyListCollection(Dictionary<KeyType, List<ListType>> items)
        {
            _Items = items;
        }
        public virtual List<ListType> this[KeyType ID]
        {
            get
            {
                if (_Items.ContainsKey(ID))
                {
                    List<ListType> l = new List<ListType>();
                    l.AddRange(_Items[ID]);
                    return l;
                }
                else
                    return default(List<ListType>);
            }
        }

        public virtual bool ContainsKey(KeyType key)
        {
            return _Items.ContainsKey(key);
        }

        #region IEnumerable<GraduationPlanInfo> 成員

        public IEnumerator<List<ListType>> GetEnumerator()
        {
            return ((IEnumerable<List<ListType>>)_Items.Values).GetEnumerator();
        }

        #endregion

        #region IEnumerable 成員

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.Values.GetEnumerator();
        }

        #endregion
    }
}
