using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.Placing.Score
{
    public abstract class AbstractSubjectScore:ISubjectScore
    {
        private string _subjectName;
        private decimal _score;
        private object _tag;

        protected virtual void SetSubjectName(string subjectName)
        {
            _subjectName = subjectName;
        }

        protected virtual void SetScore(decimal score)
        {
            _score = score;
        }        

        #region ISubjectScore жин√

        public string SubjectName
        {
            get { return _subjectName; }
        }

        public decimal Score
        {
            get { return _score; }
        }

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        #endregion
    }
}
