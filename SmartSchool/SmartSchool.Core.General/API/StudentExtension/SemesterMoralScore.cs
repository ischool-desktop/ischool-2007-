using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.API.StudentExtension
{
    internal class SemesterMoralScore:Customization.Data.StudentExtension.SemesterMoralScoreInfo
    {
        private int _schoolyear;
        private int _semester;
        private int _gradeyear;
        private string _supervisedbycomment;
        private decimal _supervisedbydiff;
        private Dictionary<string, decimal> _otherdiff = new Dictionary<string, decimal>();
        private readonly XmlElement _Detail;

        public SemesterMoralScore(int schoolyear, int semester, int gradeyear, string supervisedbycomment, decimal supervisedbydiff, Dictionary<string, decimal> otherdiff, XmlElement detail)
        {
            _schoolyear = schoolyear;
            _semester = semester;
            _gradeyear = gradeyear;
            _supervisedbycomment = supervisedbycomment;
            _supervisedbydiff = supervisedbydiff;
            _otherdiff = otherdiff;
            _Detail = detail;
        }

        #region SemesterMoralScoreInfo ����

        public int GradeYear
        {
            get { return _gradeyear; }
        }

        public Dictionary<string, decimal> OtherDiff
        {
            get { return _otherdiff; }
        }

        public int SchoolYear
        {
            get { return _schoolyear; }
        }

        public int Semester
        {
            get { return _semester; }
        }

        public string SupervisedByComment
        {
            get { return _supervisedbycomment; }
        }

        public decimal SupervisedByDiff
        {
            get { return _supervisedbydiff; }
        }

        /// <summary>
        /// �ԲӸ��
        /// </summary>
        public System.Xml.XmlElement Detail
        {
            get { return _Detail; }
        }
        #endregion
    }
}
