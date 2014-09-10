using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.StudentRelated.Placing.DataSource
{
    public interface IDataProvider
    {
        void GetData();
        event EventHandler<DataLoadEventHandler> DataLoaded;
    }

    public class DataLoadEventHandler : EventArgs
    {
        private XmlElement _result;
        public DataLoadEventHandler(XmlElement result)
        {
            _result = result;
        }

        public XmlElement Result
        {
            get { return _result; }
        }
    }
}
