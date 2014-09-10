using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.Placing
{
    public class PlacingArg
    {
        private PlaceType _type;

        public PlaceType Type
        {
            get { return _type; }
            set { _type = value; }
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

        private PlacingRule _placingRule;

        public PlacingRule PlacingRule
        {
            get { return _placingRule; }
            set { _placingRule = value; }
        }
        
    }

    public enum PlaceType
    {
        /// <summary>
        /// 依學年度
        /// </summary>
        SchoolYear,

        /// <summary>
        /// 依學年度學期
        /// </summary>
        Semester,

        /// <summary>
        /// 畢業成績
        /// </summary>
        Graduate,

        /// <summary>
        /// 其它(分項成績)
        /// </summary>
        Other
    }

    public enum PlacingRule
    {
        /// <summary>
        /// 可重複名次
        /// </summary>
        Repeatable,

        /// <summary>
        /// 不可重複名次
        /// </summary>
        Unrepeatable
    }
}
