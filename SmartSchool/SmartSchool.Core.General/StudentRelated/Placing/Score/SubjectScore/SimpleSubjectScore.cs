using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.Placing.Score
{
    public class SimpleSubjectScore : AbstractSubjectScore
    {
        public SimpleSubjectScore(string subjectName, decimal score)
        {
            this.SetSubjectName(subjectName);
            this.SetScore(score);            
        }
    }
}
