using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.API.StudentExtension
{
    class SemesterHistory:Customization.Data.StudentExtension.SemesterHistory
    {
        private string _ClassName = "";
        private XmlElement _Detail = null;
        private int _GradeYear = 0;
        private int _SchoolYear = 0;
        private string _SeatNo = "";
        private int _Semester = 0;

        public SemesterHistory(XmlElement detail)
        {
            _Detail = detail;
            int.TryParse(detail.GetAttribute("SchoolYear"), out _SchoolYear);
            int.TryParse(detail.GetAttribute("Semester"), out _Semester);
            int.TryParse(detail.GetAttribute("GradeYear"), out _GradeYear);
        }

        #region SemesterHistory 成員

        public string ClassName
        {
            get { return _ClassName; }
        }

        public System.Xml.XmlElement Detail
        {
            get { return _Detail; }
        }

        public int GradeYear
        {
            get { return _GradeYear; }
        }

        public int SchoolYear
        {
            get { return _SchoolYear; }
        }

        public string SeatNo
        {
            get { return _SeatNo; }
        }

        public int Semester
        {
            get { return _Semester; }
        }

        #endregion
    }
}
