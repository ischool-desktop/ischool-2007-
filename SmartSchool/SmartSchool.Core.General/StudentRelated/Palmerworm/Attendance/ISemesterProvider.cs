using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.Palmerworm.Attendance
{
    internal interface ISemester
    {
        void SetDate(DateTime date);
        int Semester { get;}
        int SchoolYear { get;}
    }

    internal class SemesterProvider
    {
        private static ISemester _provider;
        public static ISemester GetInstance()
        {
            if (_provider == null)
                _provider = new BaseSemesterProvider();
            return _provider;
        }        
    }

    internal class BaseSemesterProvider : ISemester
    {
        private DateTime _date;
        DateTime d1;
        DateTime d2;
        #region ISemesterProvider ����

        public void SetDate(DateTime date)
        {
            _date = date;
            d1 = new DateTime(_date.Year, 1, 31);
            d2 = new DateTime(_date.Year, 8, 1);
        }

        public int Semester
        {
            get
            {
                //if (_date > d1 && _date < d2) return 2;
                //else return 1;
                return CurrentUser.Instance.Semester;
            }
        }

        public int SchoolYear
        {
            get
            {
                //if (_date > d1 && _date < d2)
                //    return _date.Year - 1912;
                //else
                //    return _date.Year - 1911;
                return CurrentUser.Instance.SchoolYear;
            }
        }

        #endregion
    }
}


