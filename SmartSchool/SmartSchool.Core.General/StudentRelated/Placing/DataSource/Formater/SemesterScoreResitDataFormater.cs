using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.StudentRelated.Placing.Score;
using SmartSchool.StudentRelated.Placing.Score.SubjectScore;

namespace SmartSchool.StudentRelated.Placing.DataSource.Formater
{
    public class SemesterScoreResitDataFormater : IDataFormater
    {
        #region IDataFormater ����

        public StudentSemesterScoreRecordCollection Format(object source)
        {
            XmlElement element = source as XmlElement;
            if (element == null)
                throw new Exception("��ƨӷ�������XmlElement");

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
                    string subjectName = se.GetAttribute("���") + " " + DogmaticBill.GetRomanNumber(se.GetAttribute("��دŧO"));
                    string ds = se.GetAttribute("�ɦҦ��Z");
                    string credit = se.GetAttribute("�}�ҾǤ���");

                    decimal score = 0;
                    if (!decimal.TryParse(ds, out score))
                        score = 0;

                    int cd;
                    if (!int.TryParse(credit, out cd))
                        cd = 0;

                    ISubjectScore ss = new SuperSubjectScore(subjectName, score, cd);
                    r.SubjectScoreCollection.Add(ss);
                }
                collection.Add(r);
            }
            return collection;
        }

        #endregion
    }
}
