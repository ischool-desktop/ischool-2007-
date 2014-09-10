using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;


namespace SmartSchool.StudentRelated.Placing.Rule
{
    public interface IScoreDependance
    {
        decimal GetScore(StudentSemesterScoreRecord record);
    }
}
