using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;

namespace SmartSchool.StudentRelated.Placing.Rule.ScoreDependance
{
    public class TotalAvgScoreDependance:IScoreDependance
    {
        private IList<string> _subjects;

        public TotalAvgScoreDependance(IList<string> subjects)
        {
            _subjects = subjects;
        }

        #region IScoreDependance жин√

        public decimal GetScore(StudentSemesterScoreRecord record)
        {
            if (_subjects.Count == 0) return 0;

            decimal total = 0;            
            foreach (ISubjectScore ss in record.SubjectScoreCollection)
            {
                if (!_subjects.Contains(ss.SubjectName)) continue;
                total += ss.Score;
            }
            return total / record.SubjectScoreCollection.Count;
        }

        #endregion
    }
}
