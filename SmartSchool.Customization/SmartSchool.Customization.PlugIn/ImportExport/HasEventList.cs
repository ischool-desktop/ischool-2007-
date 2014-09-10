using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SmartSchool.Customization.PlugIn.ImportExport
{
    internal class HasEventList<T> : Collection<T>
    {
        public event EventHandler ItemChanged;

        protected override void ClearItems()
        {
            base.ClearItems();
            if ( ItemChanged != null )
                ItemChanged.Invoke(this, new EventArgs());
        }
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            if ( ItemChanged != null )
                ItemChanged.Invoke(this, new EventArgs());
        }
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            if ( ItemChanged != null )
                ItemChanged.Invoke(this, new EventArgs());
        }
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            if ( ItemChanged != null )
                ItemChanged.Invoke(this, new EventArgs());
        }
    }
}
