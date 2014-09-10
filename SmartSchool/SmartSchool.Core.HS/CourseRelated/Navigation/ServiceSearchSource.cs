using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.CourseRelated
{
    class ServiceSearchSource : ISearchSource, ICourseInfoContainer
    {
        private CourseDataSource _data_source;

        public ServiceSearchSource(CourseDataSource dataSource)
        {
            _data_source = dataSource;
        }

        #region ISearchSource 成員

        public CourseCollection GetCourses()
        {
            _data_source.Sync();
            return _data_source.CourseList;
        }

        public string Title
        {
            get { return "搜尋所有課程"; }
        }

        #endregion

        #region ICourseInfoContainer 成員

        CourseCollection ICourseInfoContainer.GetCourses()
        {
            return new CourseCollection();
        }

        public void SyncTitleInformation()
        {
        }

        #endregion
    }
}
