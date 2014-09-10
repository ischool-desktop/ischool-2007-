using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.ComponentModel;

namespace SmartSchool.StudentRelated.Placing.DataSource
{
    public class SchoolYearDataProvider : IDataProvider
    {
        private IList<string> _studentidList;
        private string _schoolYear;
        public event EventHandler<DataLoadEventHandler> DataLoaded;
        private XmlElement _source;
        private int _returnCount;
        private int _expectCount;

        public SchoolYearDataProvider(string schoolYear, IList<string> studentidList)
        {
            _schoolYear = schoolYear;
            _studentidList = studentidList;
        }

        #region IDataProvider 成員

        public void GetData()
        {
            int size = 50;
            _expectCount = _studentidList.Count % size == 0 ? _studentidList.Count / size : (_studentidList.Count / size) + 1;
            _returnCount = 0;

            for (int page = 0; page < _expectCount; page++)
            {
                List<string> list = new List<string>();
                int sindex = page * size;
                int eindex = (page + 1) * size > _studentidList.Count ? _studentidList.Count : (page + 1) * size;

                DSXmlHelper h = new DSXmlHelper("Request");
                h.AddElement("Field");
                h.AddElement("Field", "All");
                h.AddElement("Condition");
                h.AddElement("Condition", "SchoolYear", _schoolYear);
                for (int i = sindex; i < eindex; i++)
                {
                    h.AddElement("Condition", "ID", _studentidList[i]);
                }
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync(new DSRequest(h));
            }
        }
        #endregion

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw e.Error;

            XmlElement dsrsp = e.Result as XmlElement;
            if (_source == null)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(dsrsp.OuterXml);
                _source = doc.DocumentElement;
            }
            else
            {
                foreach (XmlElement element in dsrsp.SelectNodes("Score"))
                {
                    XmlNode node = element.Clone();
                    node = _source.OwnerDocument.ImportNode(node, true);
                    _source.AppendChild(node);
                }
            }
            _returnCount++;
            if (_returnCount == _expectCount && DataLoaded != null)
            {
                DataLoadEventHandler result = new DataLoadEventHandler(_source);
                DataLoaded.Invoke(this, result);
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DSRequest req = e.Argument as DSRequest;
            DSResponse dsrsp = SmartSchool.Feature.Score.QueryScore.GetSchoolYearScore(req);

            if (dsrsp.HasContent)
                e.Result = dsrsp.GetContent().BaseElement;
            else
                throw new Exception("取得回覆文件錯誤:" + dsrsp.GetFault().Message);
        }
    }
}
