using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Broadcaster
{
    public class Events
    {
        private static EventCollection _Items;
        public static EventCollection Items
        {
            get
            {
                if ( _Items == null )
                    _Items = new EventCollection();
                return _Items;
            }
        }

        private Events()
        {

        }


    }
}
