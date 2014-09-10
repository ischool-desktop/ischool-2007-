using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Rule;
using SmartSchool.StudentRelated.Placing.Score;

namespace SmartSchool.StudentRelated.Placing.Sorter
{
    public class SimpleComparer : ScoreComparer
    {
        private IScoreDependance _dependance;

        public SimpleComparer(IScoreDependance dependance)
        {
            _dependance = dependance;
        }

        #region IComparer<StudentSemesterScoreRecord> жин√

        public override int Compare(StudentSemesterScoreRecord x, StudentSemesterScoreRecord y)
        {
            decimal d1 = x.GetPlacingScore(_dependance);
            decimal d2 = y.GetPlacingScore(_dependance);
            return d1.CompareTo(d2) * -1;
        }

        #endregion
    }
}
