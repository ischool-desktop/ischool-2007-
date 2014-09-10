using System;
using System.Text;
using System.Collections.Generic;

namespace SmartSchool.API.PlugIn
{

    public class Collection<T> : System.Collections.ObjectModel.Collection<T>
    {
        private bool _StopEvent = false;
        public event EventHandler<ItemEventArgs<T>> ItemAdded;
        public event EventHandler<ItemEventArgs<T>> ItemRemoved;
        /// <summary>
        /// 發生於項目改變時。
        /// </summary>
        public event EventHandler ItemsChanged;

        /// <summary>
        /// 大量新增
        /// </summary>
        /// <param name="collection">新增項目集合</param>
        public new void AddRange(params T[] collection)
        {
            AddRange((IEnumerable<T>)collection);
        }
        public void AddRange(System.Collections.Generic.IEnumerable<T> collection)
        {
            bool hasInsert = false;
            _StopEvent = true;
            foreach ( T s in collection )
            {
                this.Add(s);
                hasInsert = true;
            }
            _StopEvent = false;
            if ( hasInsert && ItemsChanged != null )
                ItemsChanged(this, new EventArgs());
        }
        protected override void ClearItems()
        {
            System.Collections.Generic.List<T> list = new System.Collections.Generic.List<T>(this);
            base.ClearItems();
            if ( ItemRemoved != null )
            {
                foreach ( T var in list )
                {
                    ItemRemoved(this, new ItemEventArgs<T>(var));
                }
            }
            if ( !_StopEvent && ItemsChanged != null )
                ItemsChanged(this, new EventArgs());
        }
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            if ( ItemAdded != null )
                ItemAdded(this, new ItemEventArgs<T>(item));
            if ( !_StopEvent && ItemsChanged != null )
                ItemsChanged(this, new EventArgs());
        }
        protected override void RemoveItem(int index)
        {
            T item = this[index];
            base.RemoveItem(index);
            if ( ItemRemoved != null )
                ItemRemoved(this, new ItemEventArgs<T>(item));
            if ( !_StopEvent && ItemsChanged != null )
                ItemsChanged(this, new EventArgs());
        }
        protected override void SetItem(int index, T item)
        {
            T ritem = this[index];
            base.SetItem(index, item);
            if ( ItemRemoved != null )
                ItemRemoved(this, new ItemEventArgs<T>(ritem));
            if ( ItemAdded != null )
                ItemAdded(this, new ItemEventArgs<T>(item));
            if ( !_StopEvent && ItemsChanged != null )
                ItemsChanged(this, new EventArgs());
        }
    }
    public class ItemEventArgs<T> : EventArgs
    {
        private T _Item;
        public T Item { get { return _Item; } }
        public ItemEventArgs(T item)
        {
            _Item = item;
        }
    }
}
