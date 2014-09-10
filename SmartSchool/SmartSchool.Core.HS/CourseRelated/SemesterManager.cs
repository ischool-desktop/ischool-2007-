using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Course;
using SmartSchool.Common;

namespace SmartSchool.CourseRelated
{
    internal class SemesterManager
    {
        private List<SemesterInfo> _semesters;

        public event EventHandler SemesterListChange;

        public SemesterManager()
        {
            InitialSemesterList();
        }

        public void Refresh()
        {
            InitialSemesterList();

            if (SemesterListChange != null)
                SemesterListChange(this, EventArgs.Empty);
        }

        public List<SemesterInfo> Semesters
        {
            get { return _semesters; }
        }

        public List<int> GroupSchoolYear()
        {
            List<int> schoolyears = new List<int>();
            foreach (SemesterInfo each in _semesters)
            {
                if (!schoolyears.Contains(each.SchoolYear) && each.SchoolYear != -1)
                    schoolyears.Add(each.SchoolYear);
            }

            return schoolyears;
        }

        private void InitialSemesterList()
        {
            _semesters = new List<SemesterInfo>();

            DSXmlHelper xml_sems = QueryCourse.GetAllSemesterList();

            _semesters.Add(new SemesterInfo(-1, -1));
            foreach (XmlElement each in xml_sems.GetElements("Semester"))
            {
                string schoolyear, semester;
                schoolyear = each.SelectSingleNode("SchoolYear").InnerText;
                semester = each.SelectSingleNode("Semester").InnerText;

                if (string.IsNullOrEmpty(schoolyear) || string.IsNullOrEmpty(semester))
                    continue;

                _semesters.Add(new SemesterInfo(int.Parse(schoolyear), int.Parse(semester)));
            }

            AddCurrentSemester(); //新增目前學年度學期到集合中。

            _semesters.Sort(new Comparison<SemesterInfo>(CompareSemester));
        }

        /// <summary>
        /// 新增目前學年度學期到集合中，如果集合中沒有包含。
        /// </summary>
        private void AddCurrentSemester()
        {
            bool has_current = false;
            CurrentUser user = CurrentUser.Instance;

            foreach (SemesterInfo each in _semesters)
            {
                if (each.SchoolYear == user.SchoolYear && each.Semester == user.Semester)
                {
                    has_current = true;
                    break;
                }
            }

            if (!has_current)
                _semesters.Add(new SemesterInfo(user.SchoolYear, user.Semester));
        }

        private int CompareSemester(SemesterInfo x, SemesterInfo y)
        {
            if (x.SchoolYear > y.SchoolYear)
                return 1;
            else if (x.SchoolYear == y.SchoolYear)
                return x.Semester - y.Semester;
            else
                return -1;
        }
    }

    /// <summary>
    /// 代表一個學期的資訊，包含學年度與學期。
    /// </summary>
    [Serializable()]
    internal class SemesterInfo
    {
        private int _school_year, _semester;

        public SemesterInfo(XmlElement sems)
        {
            _school_year = GetNodeValue(sems, "SchoolYear");
            _semester = GetNodeValue(sems, "Semester");
        }

        public SemesterInfo(int schoolyear, int semester)
        {
            _school_year = schoolyear;
            _semester = semester;
        }

        public int SchoolYear
        {
            get { return _school_year; }
        }

        public int Semester
        {
            get { return _semester; }
        }

        public string DisplayName
        {
            get
            {
                return GetDisplayName("{0}學年度 第{1}學期");
            }
        }

        public string ShortName
        {
            get
            {
                return GetDisplayName("[{0}][{1}]");
            }
        }

        private string GetDisplayName(string format)
        {
            if (SchoolYear <= 0 || Semester <= 0)
                return "未定學年度學期";
            else
                return string.Format(format, SchoolYear, Semester);
        }

        private int GetNodeValue(XmlElement node, string name)
        {
            XmlNode n = node.SelectSingleNode(name);

            if (n == null)
                return -1;
            else
            {
                int value;

                if (!int.TryParse(n.InnerText, out value))
                    return -1;
                else
                    return value;
            }
        }
    }
}
