using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SmartSchool.Common
{

    public class ReadOnlyCollection<KeyType, ListType> : IEnumerable<ListType>
    {
        protected Dictionary<KeyType, ListType> _Items;
        public ReadOnlyCollection(Dictionary<KeyType, ListType> items)
        {
            if ( items == null )
                throw new Exception("不得傳入null");
            _Items = items;
        }
        public virtual ListType this[KeyType ID]
        {
            get
            {
                if ( _Items.ContainsKey(ID) )
                    return _Items[ID];
                else
                    return default(ListType);
            }
        }

        public virtual bool ContainsKey(KeyType key)
        {
            return _Items.ContainsKey(key);
        }

        public virtual bool ContainsValue(ListType value)
        {
            return _Items.ContainsValue(value);
        }

        public virtual int Count
        {
            get { return _Items.Count; }
        }

        #region IEnumerable<GraduationPlanInfo> 成員

        public IEnumerator<ListType> GetEnumerator()
        {
            return ( (IEnumerable<ListType>)_Items.Values ).GetEnumerator();
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
