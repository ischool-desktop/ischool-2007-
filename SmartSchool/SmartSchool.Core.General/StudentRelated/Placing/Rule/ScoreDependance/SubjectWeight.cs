using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.Placing.Rule.ScoreDependance
{
    public class SubjectWeight
    {
        private string _subjectName;

        public string SubjectName
        {
            get { return _subjectName; }        
        }

        private decimal _weight;

        public decimal Weight
        {
            get { return _weight; }
        }

        public SubjectWeight(string subjectName, decimal weight)
        {
            _subjectName = subjectName;
            _weight = weight;
        }


    }
}
