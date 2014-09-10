using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SmartSchool.Customization.Data;

namespace SmartSchool.CourseRelated
{
    class SearchCourseInMetadata : ISearchCourse
    {
        //bool _SearchInCourseName;//, _SearchInTeacher, _SearchInRefClass;
        List<CourseRec> _Source;
        public List<CourseRec> Source
        {
            get { return _Source; }
            set { _Source = value; }
        }

        #region ISearchStudent 成員

        //public bool SearchInCourseName
        //{
        //    get
        //    {
        //        return _SearchInCourseName;
        //    }
        //    set
        //    {
        //        _SearchInCourseName = value;
        //    }
        //}

        //public bool SearchInTeacher
        //{
        //    get
        //    {
        //        return _SearchInTeacher;
        //    }
        //    set
        //    {
        //        _SearchInTeacher = value;
        //    }
        //}

        //public bool SearchInRefClass
        //{
        //    get
        //    {
        //        return _SearchInRefClass;
        //    }
        //    set
        //    {
        //        _SearchInRefClass = value;
        //    }
        //}

        List<CourseRec> ISearchCourse.Search(string key)
        {
            Regex rx = new Regex(key.Replace("*", ".*").Replace(@"\", @"\\"));
            List<CourseRec> list = new List<CourseRec>();
            foreach ( CourseRec var in _Source )
            {
                //if ( matchCount == maxCount ) break;
                if (// _SearchInCourseName &&
                    rx.IsMatch(((CourseRecord)var).CourseName) )
                {
                    list.Add(var);
                }
                //if ( _SearchInTeacher && rx.IsMatch(var.StudentNumber) )
                //{
                //        list.Add(var);
                //}
                //if ( _SearchInRefClass && rx.IsMatch(var.IDNumber) )
                //{
                //        list.Add(var);
                //}
            }
            return list;
        }

        #endregion
    }
}
