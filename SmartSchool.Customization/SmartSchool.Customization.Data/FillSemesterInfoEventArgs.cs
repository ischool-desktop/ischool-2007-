using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class FillSemesterInfoEventArgs<T> : FillEventArgs<T>
    {
        private int _SchoolYear;
        private int _Semester;
        private bool _AllSemester=false;
        /// <summary>
        /// 建構子 指定學年度學期
        /// </summary>
        internal FillSemesterInfoEventArgs(AccessHelper accessHelper, int schoolyear, int semester, IEnumerable<T> list)
            : base(accessHelper, list)
        {
            _SchoolYear = schoolyear;
            _Semester = semester;
        }
        /// <summary>
        /// 建構子 不指定學年度學期
        /// </summary>
        internal FillSemesterInfoEventArgs(AccessHelper accessHelper, IEnumerable<T> list)
            : base(accessHelper, list)
        {
            _AllSemester = true;
        }

        public int SchoolYear
        {
            get { return _SchoolYear; }
        }
        public int Semester
        {
            get { return _Semester; }
        }
        public bool AllSemester
        {
            get { return _AllSemester; }
        }
    }
}
