using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ApplicationLog
{
    public abstract class LogInfoBase : ILogInformation
    {
        protected LogRecord log_record = new LogRecord();
        private bool _is_upload = false;

        public abstract void FlushLog();

        #region ILogInformation 成員

        bool ILogInformation.IsUpload
        {
            get { return _is_upload; }
            set { _is_upload = value; }
        }

        void ILogInformation.Flush()
        {
            FlushLog();
        }

        LogRecord ILogInformation.Content
        {
            get { return log_record; }
        }

        #endregion
    }
}
