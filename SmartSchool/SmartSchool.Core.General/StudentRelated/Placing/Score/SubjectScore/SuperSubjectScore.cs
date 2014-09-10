using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.Placing.Score.SubjectScore
{
    public class SuperSubjectScore : AbstractSubjectScore
    {
        private int _credit;
        public SuperSubjectScore(string subjectName, decimal score, int credit)
        {
            SetSubjectName(subjectName);
            SetScore(score);
            _credit = credit;
        }

        public int Credit
        {
            get { return _credit; }
        }
    }
}
