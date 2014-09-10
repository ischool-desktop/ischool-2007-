using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.StudentRelated.Placing.Score;

namespace SmartSchool.StudentRelated.Placing.DataSource
{
    public class SemesterDataFormater : IDataFormater
    {
        #region IDataSource 成員

        public StudentSemesterScoreRecordCollection Format(object source)
        {
            XmlElement element = source as XmlElement;
            if (element == null)
                throw new Exception("資料來源必須為XmlElement");

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
                    string subjectName = se.GetAttribute("科目") + DogmaticBill.GetRomanNumber(se.GetAttribute("科目級別"));
                    string ds = se.GetAttribute("原始成績");
                    decimal score = 0;
                    if (!decimal.TryParse(ds, out score))
                        score = 0;
                    ISubjectScore ss = new SimpleSubjectScore(subjectName, score);
                    r.SubjectScoreCollection.Add(ss);
                }
                collection.Add(r);
            }
            return collection;
        }

        #endregion
    }
}
