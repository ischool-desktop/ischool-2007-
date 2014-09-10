using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;
using SmartSchool.StudentRelated.Placing.Score.SubjectScore;

namespace SmartSchool.StudentRelated.Placing.Rule.ScoreDependance
{
    public class WeightTotalScoreDependance : IScoreDependance
    {
        private IList<SubjectWeight> _subjects;

        public WeightTotalScoreDependance(IList<SubjectWeight> subjects)
        {
            _subjects = subjects;
        }

        #region IScoreDependance жин√

        public decimal GetScore(StudentSemesterScoreRecord record)
        {
            if (_subjects.Count == 0) return 0;

            decimal score = 0;
            foreach (SubjectWeight sw in _subjects)
            {
                foreach (ISubjectScore ss in record.SubjectScoreCollection)
                {
                    if (sw.SubjectName != ss.SubjectName) continue;

                    score += sw.Weight * ss.Score;
                }
            }
            return score;
        }

        #endregion
    }
}
