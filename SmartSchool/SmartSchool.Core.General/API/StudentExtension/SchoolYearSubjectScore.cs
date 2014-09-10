using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.API.StudentExtension
{
    internal class SchoolYearSubjectScore:Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo
    {
        private readonly int _SchoolYear;
        private readonly string _Subject;
        private readonly decimal _Score;
        private readonly int _GradeYear;
        private readonly XmlElement _Detail;

        /// <summary>
        /// 學年度
        /// </summary>
        public int SchoolYear
        {
            get
            {
                return _SchoolYear;
            }
        }

        /// <summary>
        /// 科目
        /// </summary>
        public string Subject
        {
            get
            {
                return _Subject;
            }
        }

        /// <summary>
        /// 分數
        /// </summary>
        public decimal Score
        {
            get
            {
                return _Score;
            }
        }

        /// <summary>
        /// 成績年級
        /// </summary>
        public int GradeYear
        {
            get
            {
                return _GradeYear;
            }
        }

        /// <summary>
        /// 詳細資料
        /// </summary>
        public System.Xml.XmlElement Detail
        {
            get { return _Detail; }
        }

        public SchoolYearSubjectScore(
            int schoolYear,
            string subject,
            decimal score,
            int gradeYear,
            XmlElement detail
            )
        {
            _SchoolYear = schoolYear;
            _Subject = subject;
            _Score = score;
            _GradeYear = gradeYear;
            _Detail = detail;
        }
    }
}
