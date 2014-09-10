using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.StudentRelated.Placing.Control
{
    public class SemesterSubjectStatistic : ISubjectStatistic
    {
        private XmlElement _source;

        public SemesterSubjectStatistic(XmlElement source)
        {
            _source = source;
        }

        #region ISubjectStatistic ����

        public SubjectInfoCollection Statistic()
        {
            SubjectInfoCollection collection = new SubjectInfoCollection();
            foreach (XmlNode node in _source.SelectNodes("Score"))
            {
                foreach (XmlNode cnode in node.SelectNodes("ScoreInfo/Subject"))
                {
                    XmlElement e = cnode as XmlElement;
                    collection.Put(e.GetAttribute("���") + " " + DogmaticBill.GetRomanNumber(e.GetAttribute("��دŧO")));
                }
            }
            return collection;
        }

        #endregion
    }
}
