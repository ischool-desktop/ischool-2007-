using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Broadcaster
{
    public class EventCollection
    {

        private Dictionary<string, Event> _EventMapping = new Dictionary<string, Event>();

        public Event this[string path]
        {
            get
            {
                lock ( _EventMapping )
                {
                    if ( !_EventMapping.ContainsKey(path) )
                        _EventMapping.Add(path, new Event());
                    return _EventMapping[path];
                }
            }
        }
    }
}
