using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool
{
    class ExamScoreRec:StudentAttendCourseRec, Customization.Data.ExamScoreInfo
    {
        private string _ExamName;
        private decimal _ExamScore;
        private string _SpecialCase;

        public ExamScoreRec(
            Customization.Data.StudentRecord studentRecord, 
            Customization.Data.CourseRecord courseRecord,
            decimal? finalScore,
            bool required,
            string requiredBy,
            string examName,
            decimal examScore,
            string specialCase
            )
            : base(studentRecord, courseRecord, finalScore, required, requiredBy)
        {
            _ExamName = examName;
            _ExamScore = examScore;
            _SpecialCase = specialCase;
        }

        #region ExamScoreInfo 成員

        public string ExamName
        {
            get { return _ExamName; }
        }

        decimal SmartSchool.Customization.Data.ExamScoreInfo.ExamScore
        {
            get { return _ExamScore; }
        }

        public string SpecialCase
        {
            get { return _SpecialCase; }
        }
        #endregion
    }
}
