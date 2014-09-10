using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;

namespace SmartSchool.StudentRelated.Placing.Sorter
{
    public abstract class ScoreComparer : IComparer<StudentSemesterScoreRecord>
    {
        #region IComparer<StudentSemesterScoreRecord> жин√

        public virtual int Compare(StudentSemesterScoreRecord x, StudentSemesterScoreRecord y)
        {
            return 0;
        }

        #endregion
    }
}
