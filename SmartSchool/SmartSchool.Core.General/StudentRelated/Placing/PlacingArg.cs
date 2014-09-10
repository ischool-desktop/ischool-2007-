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
        /// �̾Ǧ~��
        /// </summary>
        SchoolYear,

        /// <summary>
        /// �̾Ǧ~�׾Ǵ�
        /// </summary>
        Semester,

        /// <summary>
        /// ���~���Z
        /// </summary>
        Graduate,

        /// <summary>
        /// �䥦(�������Z)
        /// </summary>
        Other
    }

    public enum PlacingRule
    {
        /// <summary>
        /// �i���ƦW��
        /// </summary>
        Repeatable,

        /// <summary>
        /// ���i���ƦW��
        /// </summary>
        Unrepeatable
    }
}
