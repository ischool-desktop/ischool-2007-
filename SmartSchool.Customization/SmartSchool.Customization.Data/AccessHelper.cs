using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// 提供各種資料取得服務
    /// </summary>
    public class AccessHelper
    {
        #region Static
        private static List<AccessHelper> _NonStudentProviderHelperList = new List<AccessHelper>();
        private static StudentInformationProvider _StudentProvider;
        public static void SetStudentProvider(StudentInformationProvider provider)
        {
            _StudentProvider = provider;
            foreach ( AccessHelper var in _NonStudentProviderHelperList )
            {
                StudentInformationProvider studentProvider = (StudentInformationProvider)_StudentProvider.Clone();
                studentProvider.AccessHelper = var;
                studentProvider.CachePool = var._CachePool;
                var._StudentHelper = new StudentHelper(studentProvider, var);
            }
            _NonStudentProviderHelperList.Clear();
        }

        private static List<AccessHelper> _NonClassProviderHelperList = new List<AccessHelper>();
        private static ClassInformationProvider _ClassProvider;
        public static void SetClassProvider(ClassInformationProvider provider)
        {
            _ClassProvider = provider;
            foreach ( AccessHelper var in _NonClassProviderHelperList )
            {
                ClassInformationProvider classProvider = (ClassInformationProvider)_ClassProvider.Clone();
                classProvider.AccessHelper = var;
                classProvider.CachePool = var._CachePool;
                var._ClassHelper = new ClassHelper(classProvider, var);
            }
            _NonClassProviderHelperList.Clear();
        }

        private static List<AccessHelper> _NonCourseProviderHelperList = new List<AccessHelper>();
        private static CourseInformationProvider _CourseProvider;
        public static void SetCourseProvider(CourseInformationProvider provider)
        {
            _CourseProvider = provider;
            foreach ( AccessHelper var in _NonCourseProviderHelperList )
            {
                CourseInformationProvider courseProvider = (CourseInformationProvider)_CourseProvider.Clone();
                courseProvider.AccessHelper = var;
                courseProvider.CachePool = var._CachePool;
                var._CourseHelper = new CourseHelper(courseProvider, var);
            }
            _NonCourseProviderHelperList.Clear();
        }

        private static List<AccessHelper> _NonTeacherProviderHelperList = new List<AccessHelper>();
        private static TeacherInformationProvider _TeacherProvider;
        public static void SetTeacherProvider(TeacherInformationProvider provider)
        {
            _TeacherProvider = provider;
            foreach ( AccessHelper var in _NonTeacherProviderHelperList )
            {
                TeacherInformationProvider teacherProvider = (TeacherInformationProvider)_TeacherProvider.Clone();
                teacherProvider.AccessHelper = var;
                teacherProvider.CachePool = var._CachePool;
                var._TeacherHelper = new TeacherHelper(teacherProvider, var);
            }
            _NonTeacherProviderHelperList.Clear();
        }

        #endregion
        /// <summary>
        /// 建構子
        /// </summary>
        public AccessHelper()
        {

            _CachePool = new System.Collections.Hashtable();
            if ( _StudentProvider != null )
            {
                StudentInformationProvider studentProvider = (StudentInformationProvider)_StudentProvider.Clone();
                studentProvider.AccessHelper = this;
                studentProvider.CachePool = _CachePool;
                _StudentHelper = new StudentHelper(studentProvider, this);
            }
            else
                _NonStudentProviderHelperList.Add(this);

            if ( _ClassProvider != null )
            {
                ClassInformationProvider classProvider = (ClassInformationProvider)_ClassProvider.Clone();
                classProvider.AccessHelper = this;
                classProvider.CachePool = _CachePool;
                _ClassHelper = new ClassHelper(classProvider, this);
            }
            else
                _NonClassProviderHelperList.Add(this);

            if ( _CourseProvider != null )
            {
                CourseInformationProvider courseProvider = (CourseInformationProvider)_CourseProvider.Clone();
                courseProvider.AccessHelper = this;
                courseProvider.CachePool = _CachePool;
                _CourseHelper = new CourseHelper(courseProvider, this);
            }
            else
                _NonCourseProviderHelperList.Add(this);

            if ( _TeacherProvider != null )
            {
                TeacherInformationProvider teacherProvider = (TeacherInformationProvider)_TeacherProvider.Clone();
                teacherProvider.AccessHelper = this;
                teacherProvider.CachePool = _CachePool;
                _TeacherHelper = new TeacherHelper(teacherProvider, this);
            }
            else
                _NonTeacherProviderHelperList.Add(this);

        }

        private System.Collections.Hashtable _CachePool;
        private StudentHelper _StudentHelper = null;
        private ClassHelper _ClassHelper = null;
        private TeacherHelper _TeacherHelper = null;
        private CourseHelper _CourseHelper = null;

        /// <summary>
        /// 協助處理及取得學生資料
        /// </summary>
        public StudentHelper StudentHelper
        {
            get
            {
                if ( _StudentHelper == null )
                    throw new Exception("StudentProvider尚未設定");
                return _StudentHelper;
            }
        }

        /// <summary>
        /// 協助處理及取得班級資料
        /// </summary>
        public ClassHelper ClassHelper
        {
            get
            {
                if ( _ClassHelper == null )
                    throw new Exception("ClassProvider尚未設定");
                return _ClassHelper;
            }
        }

        /// <summary>
        /// 協助處理及取得教師資料
        /// </summary>
        public TeacherHelper TeacherHelper
        {
            get
            {
                if ( _TeacherHelper == null )
                    throw new Exception("TeacherHelper尚未設定");
                return _TeacherHelper;
            }
        }

        /// <summary>
        /// 協助處理及取得課程資料
        /// </summary>
        public CourseHelper CourseHelper
        {
            get
            {
                if ( _CourseHelper == null )
                    throw new Exception("CourseHelper尚未設定");
                return _CourseHelper;
            }
        }

        /// <summary>
        /// 暫存容器
        /// </summary>
        public System.Collections.Hashtable CachePool
        {
            get { return _CachePool; }
        }

        /// <summary>
        /// 清除暫存資訊
        /// </summary>
        public void Reflash()
        {
            _CachePool.Clear();
        }
    }
}
