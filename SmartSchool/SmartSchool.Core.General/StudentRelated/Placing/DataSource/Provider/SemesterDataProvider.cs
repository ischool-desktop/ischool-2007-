using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.StudentRelated.Placing.DataSource
{
    public class SemesterDataProvider : IDataProvider
    {
        private IList<string> _studentidList;
        private string _schoolYear;
        private string _semester;
        public event EventHandler<DataLoadEventHandler> DataLoaded;
        //private XmlElement _source;
        private int _returnCount;
        private int _expectCount;
        private List<EachRecord> _records;

        public SemesterDataProvider(string schoolYear, string semester, IList<string> studentidList)
        {
            _schoolYear = schoolYear;
            _semester = semester;
            _studentidList = studentidList;
        }

        #region IDataProvider 成員

        public void GetData()
        {
            int size = 200;
            _expectCount = _studentidList.Count % size == 0 ? _studentidList.Count / size : (_studentidList.Count / size) + 1;
            _returnCount = 0;

            for (int page = 0; page < _expectCount; page++)
            {
                List<string> list = new List<string>();
                int sindex = page * size;
                int eindex = (page + 1) * size > _studentidList.Count ? _studentidList.Count : (page + 1) * size;

                //DSXmlHelper h = new DSXmlHelper("Request");
                //h.AddElement("Field");
                //h.AddElement("Field", "All");
                //h.AddElement("Condition");
                //h.AddElement("Condition","SchoolYear", _schoolYear);
                //h.AddElement("Condition", "Semester", _semester);                
                for (int i = sindex; i < eindex; i++)
                {
                    list.Add(_studentidList[i]);
                    //h.AddElement("Condition", "ID", _studentidList[i]);                    
                }
                SemesterDoWorkArg arg = new SemesterDoWorkArg(_schoolYear, _semester, list);
                _records = new List<EachRecord>();

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync(arg);
            }
        }
        #endregion

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw e.Error;

            //XmlElement dsrsp = e.Result as XmlElement;
            //if (_source == null)
            //{
            //    XmlDocument doc = new XmlDocument();
            //    doc.LoadXml(dsrsp.OuterXml);
            //    _source = doc.DocumentElement;
            //}
            //else
            //{
            //    foreach (XmlElement element in dsrsp.SelectNodes("Score"))
            //    {
            //        XmlNode node = element.Clone();
            //        node = _source.OwnerDocument.ImportNode(node, true);
            //        _source.AppendChild(node);
            //    }
            //}
            List<EachRecord> records = e.Result as List<EachRecord>;
            foreach (EachRecord record in records)
            {
                _records.Add(record);
            }
            _returnCount++;
            if (_returnCount == _expectCount && DataLoaded != null)
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("ScoreList");
                doc.AppendChild(root);

                foreach (EachRecord r in _records)
                {
                    XmlElement score = doc.CreateElement("Score");
                    root.AppendChild(score);
                    score.SetAttribute("ID", r.ID);
                    foreach (string ns in r.Normal.Keys)
                    {
                        XmlElement ne = doc.CreateElement(ns);
                        score.AppendChild(ne);
                        ne.InnerText = r.Normal[ns];
                    }
                    XmlElement scoreInfo = doc.CreateElement("ScoreInfo");
                    score.AppendChild(scoreInfo);
                    foreach (EachAttributeCollection attribute in r.Subject)
                    {
                        XmlElement subject = doc.CreateElement("Subject");
                        scoreInfo.AppendChild(subject);
                        foreach (string name in attribute.Collection.Keys)
                        {
                            subject.SetAttribute(name, attribute.Collection[name]);
                        }
                    }
                }

                DataLoadEventHandler result = new DataLoadEventHandler(doc.DocumentElement);
                DataLoaded.Invoke(this, result);
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //DSRequest req = e.Argument as DSRequest;
            SemesterDoWorkArg arg = e.Argument as SemesterDoWorkArg;
            DSXmlHelper h = new DSXmlHelper("Request");
            h.AddElement("Field");
            h.AddElement("Field", "All");
            h.AddElement("Condition");
            h.AddElement("Condition", "SchoolYear", arg.SchoolYear);
            h.AddElement("Condition", "Semester", arg.Semester);
            foreach (string id in arg.IdList)
            {
                h.AddElement("Condition", "ID", id);
            }

            DSResponse dsrsp = SmartSchool.Feature.Score.QueryScore.GetSemesterScore(new DSRequest(h));

            if (dsrsp.HasContent)
                e.Result = dsrsp.GetContent().BaseElement;
            else
                throw new Exception("取得回覆文件錯誤:" + dsrsp.GetFault().Message);

            List<EachRecord> records = new List<EachRecord>();
            foreach (XmlElement element in dsrsp.GetContent().GetElements("Score"))
            {
                EachRecord r = new EachRecord(element);
                records.Add(r);
            }
            e.Result = records;
        }

        public class EachRecord
        {
            private string _id;

            public string ID
            {
                get { return _id; }             
            }

            private Dictionary<string, string> _normal;

            public Dictionary<string, string> Normal
            {
                get { return _normal; }            
            }

            private List<EachAttributeCollection> _subject;

            public List<EachAttributeCollection> Subject
            {
                get { return _subject; }            
            }
                      
            public EachRecord(XmlElement each)
            {
                _normal = new Dictionary<string, string>();
                _subject = new List<EachAttributeCollection>();
                _id = each.GetAttribute("ID");

                foreach (XmlNode node in each.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element) continue;
                    if (node.Name == "ScoreInfo") continue;
                    _normal.Add(node.Name, node.InnerText);
                }

                foreach (XmlNode node in each.SelectNodes("ScoreInfo/Subject"))
                {
                    EachAttributeCollection a = new EachAttributeCollection();
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        a.Collection.Add(attribute.Name, attribute.Value);
                    }
                    _subject.Add(a);
                }
            }           
        }

        public class EachAttributeCollection
        {
            private Dictionary<string, string> _collection;

            public Dictionary<string, string> Collection
            {
                get { return _collection; }
                set { _collection = value; }
            }

            public EachAttributeCollection()
            {
                _collection = new Dictionary<string, string>();
            }
        }

        public class SemesterDoWorkArg
        {
            private string _schoolYear;
            public string SchoolYear
            {
                get { return _schoolYear; }
            }
            private string _semester;
            public string Semester
            {
                get { return _semester; }
            }
            private IList<string> _idList;
            public IList<string> IdList
            {
                get { return _idList; }
            }

            public SemesterDoWorkArg(string schoolYear, string semester, IList<string> idList)
            {
                _schoolYear = schoolYear;
                _semester = semester;
                _idList = idList;
            }
        }
    }
}
