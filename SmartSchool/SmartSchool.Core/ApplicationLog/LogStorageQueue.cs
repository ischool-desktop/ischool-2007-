using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ApplicationLog
{
    internal class LogStorageQueue : Queue<ILogInformation>
    {
        private object _sync = new object();

        public object SyncRoot
        {
            get { return _sync; }
        }
    }
}