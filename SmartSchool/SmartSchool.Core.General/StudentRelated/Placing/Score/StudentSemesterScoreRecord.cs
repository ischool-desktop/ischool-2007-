using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Rule;


namespace SmartSchool.StudentRelated.Placing.Score
{
    public class StudentSemesterScoreRecord
    {
        public StudentSemesterScoreRecord()
        {
            _subjectScoreCollection = new SubjectScoreCollection();
        }

        private string _studentNumber;

        public string StudentNumber
        {
            get { return _studentNumber; }
            set { _studentNumber = value; }
        }

        private string _studentID;

        public string StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }
        private string _studentName;

        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        private string _seatNo;

        public string SeatNo
        {
            get { return _seatNo; }
            set { _seatNo = value; }
        }

        private string _className;

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        private string _deptName;

        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }
        private string _schoolYear;

        public string SchoolYear
        {
            get { return _schoolYear; }
            set { _schoolYear = value; }
        }
        private string _semester;

        public string Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }
        private string _gradeYear;

        public string GradeYear
        {
            get { return _gradeYear; }
            set { _gradeYear = value; }
        }
        private SubjectScoreCollection _subjectScoreCollection;

        public SubjectScoreCollection SubjectScoreCollection
        {
            get { return _subjectScoreCollection; }
            set { _subjectScoreCollection = value; }
        }

        private decimal _placedScore = -1;
        public decimal GetPlacingScore(IScoreDependance dependance)
        {
            _placedScore = dependance.GetScore(this);
            return _placedScore;
        }

        public decimal PlacedScore
        {
            get { return _placedScore; }
        }
    }
}
