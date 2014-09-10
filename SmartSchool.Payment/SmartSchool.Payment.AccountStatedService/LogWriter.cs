using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace SmartSchool.Payment.AccountStatedService
{
    public class LogWriter
    {
        private XPathNavigator _log;
        private XPathNavigator _previous;
        private object _sync_object = new object();

        public LogWriter()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<MsgClub/>");
            doc.DocumentElement.SetAttribute("StartTimestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            _log = doc.DocumentElement.CreateNavigator();
            _previous = null;
        }

        private LogWriter(XPathNavigator target, object sync_object)
        {
            _log = target;
            _sync_object = sync_object;
        }

        public void Write(string message)
        {
            lock (_sync_object)
            {
                if (_previous == null)
                {
                    _log.AppendChildElement("", "Msg", null, message);
                    _log.MoveToChild(XPathNodeType.Element);
                    _log.CreateAttribute("", "Timestamp", null, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    _previous = _log.CreateNavigator();
                }
                else
                {
                    _log.MoveTo(_previous);
                    _log.InsertElementAfter("", "Msg", null, message);
                    _log.MoveToNext(XPathNodeType.Element);
                    _log.CreateAttribute("", "Timestamp", null, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    _previous = _log.CreateNavigator();
                }
            }
        }

        public LogWriter CreateMessageGroup(string name)
        {
            lock (_sync_object)
            {
                if (_previous == null)
                {
                    _log.AppendChildElement("", "MsgGroup", null, string.Empty);
                    _log.MoveToFirstChild();
                    _log.CreateAttribute("", "Name", null, name);
                    _log.CreateAttribute("", "StartTimestamp", null, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    _previous = _log.CreateNavigator();
                }
                else
                {
                    _log.MoveTo(_previous);
                    _log.InsertElementAfter("", "MsgGroup", null, string.Empty);
                    _log.MoveToNext(XPathNodeType.Element);
                    _log.CreateAttribute("", "Name", null, name);
                    _log.CreateAttribute("", "StartTimestamp", null, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    _previous = _log.CreateNavigator();
                }
            }

            return new LogWriter(_log.CreateNavigator(), _sync_object);
        }

        public XmlDocument GetXmlDocument()
        {
            XmlDocument doc = new XmlDocument();

            lock (_sync_object)
            {
                _log.MoveToRoot();
                doc.LoadXml(_log.OuterXml);
            }

            return doc;
        }
    }
}
