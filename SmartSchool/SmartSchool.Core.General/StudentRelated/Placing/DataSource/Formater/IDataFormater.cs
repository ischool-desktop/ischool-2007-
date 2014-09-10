using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;

namespace SmartSchool.StudentRelated.Placing.DataSource
{
    public interface IDataFormater
    {
        StudentSemesterScoreRecordCollection Format(object source);
    }
}
