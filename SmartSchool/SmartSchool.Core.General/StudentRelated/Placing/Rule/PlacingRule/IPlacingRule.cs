using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;

namespace SmartSchool.StudentRelated.Placing.Rule
{
    public interface IPlacingRule
    {
        IList<PlacingInfo> GetPlacingInfo(StudentSemesterScoreRecordCollection collection,IScoreDependance dependance);
    }
}
