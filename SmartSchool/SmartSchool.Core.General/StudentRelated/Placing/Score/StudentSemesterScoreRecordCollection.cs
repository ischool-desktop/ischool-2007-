using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Sorter;

namespace SmartSchool.StudentRelated.Placing.Score
{
    public class StudentSemesterScoreRecordCollection : IEnumerable<StudentSemesterScoreRecord>
    {        
        private List<StudentSemesterScoreRecord> _records;

        public StudentSemesterScoreRecordCollection()
        {
            _records = new List<StudentSemesterScoreRecord>();
        }

        public void Add(StudentSemesterScoreRecord record)
        {
            _records.Add(record);
        }

        public void Sort(ScoreComparer comparer)
        {
            _records.Sort(comparer);
        }

        #region IEnumerable<StudentSemesterScoreRecord> ����

        public IEnumerator<StudentSemesterScoreRecord> GetEnumerator()
        {
            return _records.GetEnumerator();
        }

        #endregion

        #region IEnumerable ����

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _records.GetEnumerator();
        }

        #endregion
    }
}
