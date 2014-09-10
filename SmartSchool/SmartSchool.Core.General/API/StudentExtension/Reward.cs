using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.API.StudentExtension
{
    internal class Reward:Customization.Data.StudentExtension.RewardInfo
    {
        private int _schoolyear;
        private int _semester;
        private DateTime _occurdate;
        private string _occurplace;
        private string _occurreason;
        private int _awardA;
        private int _awardB;
        private int _awardC;
        private int _faultA;
        private int _faultB;
        private int _faultC;
        private bool _cleared;
        private DateTime _cleardate;
        private string _clearreason;
        private bool _UltimateAdmonition;
        private XmlElement _detail;

        public Reward(int schoolyear, int semester, DateTime occurdate, string occurplace, string occurreason, int[] award, int[] fault, bool cleared, DateTime cleardate, string clearreason, bool ultimateAdmonition, XmlElement detail)
        {
            _schoolyear = schoolyear;
            _semester = semester;
            _occurdate = occurdate;
            _occurplace = occurplace;
            _occurreason = occurreason;
            _awardA = award[0];
            _awardB = award[1];
            _awardC = award[2];
            _faultA = fault[0];
            _faultB = fault[1];
            _faultC = fault[2];
            _cleared = cleared;
            _cleardate = cleardate;
            _clearreason = clearreason;
            _UltimateAdmonition = ultimateAdmonition;
            _detail = detail;
        }

        #region RewardInfo жин√

        public int AwardA
        {
            get { return _awardA; }
        }

        public int AwardB
        {
            get { return _awardB; }
        }

        public int AwardC
        {
            get { return _awardC; }
        }

        public DateTime ClearDate
        {
            get { return _cleardate; }
        }

        public bool Cleared
        {
            get { return _cleared; }
        }

        public int FaultA
        {
            get { return _faultA; }
        }

        public int FaultB
        {
            get { return _faultB; }
        }

        public int FaultC
        {
            get { return _faultC; }
        }

        public DateTime OccurDate
        {
            get { return _occurdate; }
        }

        public string OccurPlace
        {
            get { return _occurplace; }
        }

        public string OccurReason
        {
            get { return _occurreason; }
        }

        public int SchoolYear
        {
            get { return _schoolyear; }
        }

        public int Semester
        {
            get { return _semester; }
        }

        public bool UltimateAdmonition
        {
            get
            {
                return _UltimateAdmonition;
            }
        }

        public string ClearReason
        {
            get { return _clearreason; }
        }

        public System.Xml.XmlElement Detail
        {
            get { return _detail; }
        }

        #endregion
    }
}
