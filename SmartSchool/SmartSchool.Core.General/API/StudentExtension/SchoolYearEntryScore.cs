using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.API.StudentExtension
{
    internal class SchoolYearEntryScore:Customization.Data.StudentExtension.SchoolYearEntryScoreInfo
    {
        private readonly string _Entry;
        private readonly int _GradeYear;
        private readonly int _SchoolYear;
        private readonly decimal _Score;
        private readonly XmlElement _Detail;

        /// <summary>
        /// 分項
        /// </summary>
        public string Entry
        {
            get
            {
                return _Entry;
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
        /// 成績
        /// </summary>
        public decimal Score
        {
            get
            {
                return _Score;
            }
        }

        /// <summary>
        /// 詳細資料
        /// </summary>
        public System.Xml.XmlElement Detail
        {
            get { return _Detail; }
        }

        public SchoolYearEntryScore(
            string entry,
            int gradeYear,
            int schoolYear,
            decimal score,
            XmlElement detail
            )
        {
            _Entry = entry;
            _GradeYear = gradeYear;
            _SchoolYear = schoolYear;
            _Score = score;
            _Detail = detail;
        }
    }
}
