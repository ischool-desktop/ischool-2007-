using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.StudentRelated.Placing.Score;
using SmartSchool.StudentRelated.Placing.Score.SubjectScore;

namespace SmartSchool.StudentRelated.Placing.DataSource.Formater
{
    public class SemesterScoreChoiceDataFormater:IDataFormater
    {
        private ChoiceScoreTypeArg _arg;

        public SemesterScoreChoiceDataFormater(ChoiceScoreTypeArg arg)
        {
            _arg = arg;
        }

        public StudentSemesterScoreRecordCollection Format(object source)
        {
            XmlElement element = source as XmlElement;
            if (element == null)
                throw new Exception("戈ㄓ方ゲ斗XmlElement");

            StudentSemesterScoreRecordCollection collection = new StudentSemesterScoreRecordCollection();
            foreach (XmlNode node in element.SelectNodes("Score"))
            {
                XmlElement e = node as XmlElement;
                StudentSemesterScoreRecord r = new StudentSemesterScoreRecord();
                r.StudentID = e.GetAttribute("ID");
                r.StudentName = e.SelectSingleNode("Name").InnerText;
                r.Semester = e.SelectSingleNode("Semester").InnerText;
                r.SchoolYear = e.SelectSingleNode("SchoolYear").InnerText;
                r.GradeYear = e.SelectSingleNode("GradeYear").InnerText;
                r.ClassName = e.SelectSingleNode("ClassName").InnerText;
                r.StudentNumber = e.SelectSingleNode("StudentNumber").InnerText;
                r.SeatNo = e.SelectSingleNode("SeatNo").InnerText;

                foreach (XmlNode n in e.SelectNodes("ScoreInfo/Subject"))
                {
                    XmlElement se = n as XmlElement;
                    string subjectName = se.GetAttribute("ヘ") + " " + DogmaticBill.GetRomanNumber(se.GetAttribute("ヘ"));                    
                    string credit = se.GetAttribute("秨揭厩だ计");

                    string s1 = se.GetAttribute("﹍Θ罿");
                    string s2 = se.GetAttribute("干σΘ罿");
                    string s3 = se.GetAttribute("Θ罿");

                    decimal score1 = 0;
                    if (!decimal.TryParse(s1, out score1))
                        score1 = 0;

                    decimal score2 = 0;
                    if (!decimal.TryParse(s2, out score2))
                        score2 = 0;
                    
                    decimal score3 = 0;
                    if (!decimal.TryParse(s3, out score3))
                        score3 = 0;
                    
                    decimal bestScore = 0;
                    if (_arg.HasType(ChoiceScoreType.﹍Θ罿))
                        bestScore = score1;

                    if (_arg.HasType(ChoiceScoreType.干σΘ罿))
                        bestScore = Math.Max(bestScore, score2);

                    if(_arg.HasType(ChoiceScoreType.Θ罿))
                        bestScore = Math.Max(bestScore,score3);

                    int cd;
                    if (!int.TryParse(credit, out cd))
                        cd = 0;

                    ISubjectScore ss = new SuperSubjectScore(subjectName, bestScore, cd);
                    r.SubjectScoreCollection.Add(ss);
                }
                collection.Add(r);
            }
            return collection;
        }
    }

    public enum ChoiceScoreType
    {
        ﹍Θ罿,Θ罿,干σΘ罿
    }

    public class ChoiceScoreTypeArg:IEnumerable<ChoiceScoreType>
    {
        private IList<ChoiceScoreType> _types;

        public ChoiceScoreTypeArg()
        {
            _types = new List<ChoiceScoreType>();
        }

        public void Join(ChoiceScoreType type)
        {
            _types.Add(type);
        }

        public bool HasType(ChoiceScoreType type)
        {
            foreach (ChoiceScoreType t in _types)
            {
                if (t == type)
                    return true;
            }
            return false;
        }

        #region IEnumerable<ChoiceScoreType> Θ

        public IEnumerator<ChoiceScoreType> GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        #endregion

        #region IEnumerable Θ

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        #endregion
    }
}
