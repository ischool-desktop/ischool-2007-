using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SmartSchool.StudentRelated.Placing.Score
{
    public class SubjectScoreCollection:IEnumerable<ISubjectScore>
    {
        private IList<ISubjectScore> _scoreList;

        public SubjectScoreCollection()
        {
            _scoreList = new List<ISubjectScore>();
        }

        public void Add(ISubjectScore score)
        {
            _scoreList.Add(score);
        }

        public int Count
        {
            get { return _scoreList.Count; }
        }

        #region IEnumerable<ISubjectScore> ����

        public IEnumerator<ISubjectScore> GetEnumerator()
        {
            return _scoreList.GetEnumerator();
        }

        #endregion

        #region IEnumerable ����

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _scoreList.GetEnumerator();
        }

        #endregion
    }
}
