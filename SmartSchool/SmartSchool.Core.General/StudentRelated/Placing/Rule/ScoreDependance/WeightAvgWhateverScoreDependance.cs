using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;

namespace SmartSchool.StudentRelated.Placing.Rule.ScoreDependance
{
    public class WeightAvgWhateverScoreDependance : IScoreDependance
    {
        private IList<SubjectWeight> _subjects;
        public WeightAvgWhateverScoreDependance(IList<SubjectWeight> subjects)
        {
            _subjects = subjects;
        }

        #region IScoreDependance жин√

        public decimal GetScore(StudentSemesterScoreRecord record)
        {
            decimal total = 0;
            decimal weights = 0;

            foreach (SubjectWeight sw in _subjects)
            {
                weights += sw.Weight;
                foreach (ISubjectScore ss in record.SubjectScoreCollection)
                {                   
                    if (ss.SubjectName != sw.SubjectName) continue;
                    total += ss.Score * sw.Weight;
                }
            }
            if (weights == 0) return 0;
            return total / weights;
        }

        #endregion
    }
}
