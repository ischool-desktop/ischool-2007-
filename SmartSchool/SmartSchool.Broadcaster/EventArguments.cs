using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SmartSchool.Broadcaster
{
    public class EventArguments:EventArgs
    {
        private readonly object[] _Items = new object[0];

        public EventArguments()
        { }
        public EventArguments(IEnumerable items)
        {
            List<object> list = new List<object>();
            if ( items is string )
            {
                list.Add(items);
            }
            else
            {
                foreach ( object var in items )
                {
                    list.Add(var);
                }
            }
            _Items = list.ToArray();
        }
        public object[] Items
        {
            get { return _Items; }
        }
    }
}
