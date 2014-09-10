using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;


namespace SmartSchool.StudentRelated.Placing.Rule
{
    public class PlacingInfo
    {
        private StudentSemesterScoreRecord _record;

        public StudentSemesterScoreRecord Record
        {
            get { return _record; }
            set { _record = value; }
        }
        private int _place;

        public int Place
        {
            get { return _place; }
            set { _place = value; }
        }
        private decimal _score;

        public decimal Score
        {
            get { return _score; }
            set { _score = value; }
        }
    }
}
