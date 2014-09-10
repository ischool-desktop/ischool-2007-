using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;


namespace SmartSchool.StudentRelated.Placing.Rule
{
    public class SubjectScoreDependance : IScoreDependance
    {
        private string _subjectName;
        public SubjectScoreDependance(string subjectName)
        {
            _subjectName = subjectName;
        }

        #region IScoreDependance жин√

        public decimal GetScore(StudentSemesterScoreRecord record)
        {
            foreach (ISubjectScore ss in record.SubjectScoreCollection)
            {
                if (_subjectName.Equals(ss.SubjectName))
                    return ss.Score;
            }
            return 0;
        }

        #endregion
    }
}
