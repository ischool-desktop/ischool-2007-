using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.Placing.Score
{
    public interface ISubjectScore
    {
        string SubjectName { get;}
        decimal Score { get;}
        object Tag { get;set;}
    }
}
