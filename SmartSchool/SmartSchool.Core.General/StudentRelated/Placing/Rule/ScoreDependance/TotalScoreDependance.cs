using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;

namespace SmartSchool.StudentRelated.Placing.Rule.ScoreDependance
{
    public class TotalScoreDependance : IScoreDependance
    {
        private IList<string> _subjects;

        public TotalScoreDependance(IList<string> subjects)
        {
            _subjects = subjects;
        }

        #region IScoreDependance жин√

        public decimal GetScore(StudentSemesterScoreRecord record)
        {
            decimal score = 0;
            foreach (ISubjectScore ss in record.SubjectScoreCollection)
            {
                if (!_subjects.Contains(ss.SubjectName)) continue;
                score += ss.Score;
            }
            return score;
        }

        #endregion
    }
}
