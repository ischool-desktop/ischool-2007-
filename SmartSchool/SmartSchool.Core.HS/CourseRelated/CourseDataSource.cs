using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Course;
using System.Xml;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.CourseRelated
{
    internal class CourseDataSource
    {
        private Dictionary<string, CourseInformation> _course_dict_base = new Dictionary<string, CourseInformation>();
        private CourseCollection _course_list;
        private CourseDictionary _course_dict;
        private List<string> _LoadedSemester = new List<string>();
        private int _schoolyear, _semester;

        public CourseDataSource(int schoolyear, int semester)
        {
            _schoolyear = schoolyear;
            _semester = semester;
        }

        private void LoadAllCourse()
        {
            int t1 = Environment.TickCount;
            #region 移除本學期的資料
            List<string> removeCourse = new List<string>();
            foreach ( CourseInformation course in _course_dict_base.Values )
            {
                if ( course.SchoolYear == _schoolyear && course.Semester == _semester )
                    removeCourse.Add("" + course.Identity);
            }

            foreach ( string id in removeCourse )
                _course_dict_base.Remove(id);

            #endregion

            #region 重新讀取本學期資料
            DSXmlHelper xml_all_course = QueryCourse.GetCourseBySemester(_schoolyear, _semester);

            _course_list = new CourseCollection();
            //Dictionary<string, CourseInformation> temp = new Dictionary<string, CourseInformation>();
            foreach ( XmlElement each in xml_all_course.GetElements("Course") )
            {
                CourseInformation course = new CourseInformation(each);

                //這招不知道會不會有後遺症...
                if ( _course_dict_base.ContainsKey("" + course.Identity) )
                {
                    _course_dict_base.Remove("" + course.Identity);
                }

                _course_dict_base.Add("" + course.Identity, course);
                _course_list.Add(course);
                //temp.Add(course.Identity + "", course);
            }
            _course_dict = new CourseDictionary(_course_dict_base);
            #endregion

            if ( !_LoadedSemester.Contains("" + _schoolyear + "_" + _semester) )
                _LoadedSemester.Add("" + _schoolyear + "_" + _semester);

            //RefreshCacheList();
            Console.WriteLine("Course Load Time：{0}", Environment.TickCount - t1);
        }

        private void RefreshCacheList()
        {
            lock ( _LoadedSemester )
            {
                _LoadedSemester.Clear();
            }
            EnsureCourse(_schoolyear, _semester);
        }

        public CourseCollection CourseList
        {
            get { return _course_list; }
        }

        public CourseDictionary CourseDictionary
        {
            get { return _course_dict; }
        }

        public void Sync()
        {
            //LoadAllCourse();
            RefreshCacheList();
            if (SourceRefresh != null)
                SourceRefresh(this, EventArgs.Empty);
        }

        public void Sync(int schoolyear, int semester)
        {
            _schoolyear = schoolyear;
            _semester = semester;

            Sync();
        }
        /// <summary>
        /// 保障擁有學年度學期的課程資料
        /// </summary>
        public void EnsureCourse(int schoolyear, int semester)
        {
            lock (_LoadedSemester)
            {
                if (!_LoadedSemester.Contains("" + schoolyear + "_" + semester))
                {
                    int sy = _schoolyear, ss = _semester;
                    _schoolyear = schoolyear;
                    _semester = semester;
                    LoadAllCourse();
                    _schoolyear = sy;
                    _semester = ss;
                }
            }
        }

        public void ReloadCourseInformation(params int[] courseId)
        {
            if (courseId.Length == 0) return;
            DSXmlHelper xmlCourse = QueryCourse.GetCourseDetail(courseId).GetContent();

            List<CourseInformation> changedCourses = new List<CourseInformation>();

            foreach (XmlElement each in xmlCourse.GetElements("Course"))
                changedCourses.Add(new CourseInformation(each));

            foreach (CourseInformation each in changedCourses)
            {
                if (_course_dict_base.ContainsKey(each.Identity.ToString()))
                {
                    _course_dict_base.Remove(each.Identity.ToString());
                    _course_dict_base.Add(each.Identity.ToString(), each);
                }
                else
                    _course_dict_base.Remove(each.Identity.ToString());
            }

            RefreshCacheList();
        }

        public CourseInformation FindByCourseID(int courseId)
        {
            if (_course_dict_base.ContainsKey(courseId.ToString()))
                return _course_dict_base[courseId.ToString()];

            return null;
        }

        public event EventHandler SourceRefresh;
    }

    internal class CourseDictionary : ReadOnlyCollection<string, CourseInformation>
    {
        public CourseDictionary(Dictionary<string, CourseInformation> items)
            : base(items)
        {
        }
        /// <summary>
        /// 用學年度學期抓出在資料集合內的課程
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
        /// <returns>集合</returns>
        public CourseDictionary this[int schoolYear, int semester]
        {
            get
            {
                Dictionary<string, CourseInformation> semesterCourses = new Dictionary<string, CourseInformation>();
                foreach (CourseInformation courseInformation in this._Items.Values)
                {
                    if (courseInformation.SchoolYear == schoolYear && courseInformation.Semester == semester)
                        semesterCourses.Add("" + courseInformation.Identity, courseInformation);
                }
                return new CourseDictionary(semesterCourses);
            }
        }

        public override CourseInformation this[string ID]
        {
            get
            {
                if (!_Items.ContainsKey(ID))
                {
                    DSXmlHelper xml_all_course = QueryCourse.GetCourseById(ID).GetContent();

                    foreach (XmlElement each in xml_all_course.GetElements("Course"))
                    {
                        CourseInformation course = new CourseInformation(each);
                        _Items.Add("" + course.Identity, course);
                    }
                }
                return base[ID];
            }
        }
    }
}
