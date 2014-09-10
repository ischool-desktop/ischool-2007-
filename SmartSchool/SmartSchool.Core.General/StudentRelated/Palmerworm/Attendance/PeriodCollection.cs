using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SmartSchool.StudentRelated.Palmerworm.Attendance
{
    public class PeriodCollection
    {
        public PeriodCollection()
        {
            _periodList = new List<PeriodInfo>();
        }

        private List<PeriodInfo> _periodList;

        public List<PeriodInfo> Items
        {
            get { return _periodList; }        
        }

        public List<PeriodInfo> GetSortedList()
        {
            _periodList.Sort(new PeriodComparer());
            return _periodList;
        }
    }

    public class PeriodComparer : IComparer<PeriodInfo>
    {
        #region IComparer<PeriodInfo> жин√

        public int Compare(PeriodInfo x, PeriodInfo y)
        {
            if (x.Sort == y.Sort) return 0;
            else if (x.Sort > y.Sort) return 1;
            else return -1;
        }

        #endregion
    }
}
