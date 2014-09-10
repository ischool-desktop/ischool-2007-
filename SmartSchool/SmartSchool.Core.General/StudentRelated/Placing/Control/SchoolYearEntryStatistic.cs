using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.StudentRelated.Placing.Control
{
    public class SchoolYearEntryStatistic : ISubjectStatistic
    {
        private XmlElement _source;

        public SchoolYearEntryStatistic(XmlElement source)
        {
            _source = source;
        }

        #region ISubjectStatistic 成員

        public SubjectInfoCollection Statistic()
        {
            SubjectInfoCollection collection = new SubjectInfoCollection();
            foreach (XmlNode node in _source.SelectNodes("Score"))
            {
                foreach (XmlNode cnode in node.SelectNodes("ScoreInfo/Entry"))
                {
                    XmlElement e = cnode as XmlElement;
                    collection.Put(e.GetAttribute("分項"));
                }
            }
            return collection;
        }

        #endregion
    }
}
