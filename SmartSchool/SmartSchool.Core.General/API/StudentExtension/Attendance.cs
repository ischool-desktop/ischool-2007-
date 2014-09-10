using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data.StudentExtension;
using System.Xml;

namespace SmartSchool.API.StudentExtension
{
    internal class Attendance:Customization.Data.StudentExtension.AttendanceInfo
    {
        private int _schoolyear;
        private int _semester;
        private DateTime _occurdate;
        private string _period;
        private string _periodtype;
        private string _absence;
        private XmlElement _Detail;

        public Attendance(int schoolyear, int semester, DateTime occurdate, string period, string periodtype, string absence, XmlElement detail)
        {
            _schoolyear = schoolyear;
            _semester = semester;
            _occurdate = occurdate;
            _period = period;
            _periodtype = periodtype;
            _absence = absence;
            _Detail = detail;
        }


        #region AttendanceInfo жин√
        public string Absence
        {
            get { return _absence; }
        }

        public DateTime OccurDate
        {
            get { return _occurdate; }
        }

        public string Period
        {
            get { return _period; }
        }

        public string PeriodType
        {
            get { return _periodtype; }
        }

        public int SchoolYear
        {
            get { return _schoolyear; }
        }

        public int Semester
        {
            get { return _semester; }
        }

        public System.Xml.XmlElement Detail
        {
            get { return _Detail; }
        }

        #endregion
    }
}
