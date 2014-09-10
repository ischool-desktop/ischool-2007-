using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class CourseHelper
    {
        private CourseInformationProvider _Provider;

        private AccessHelper _AccessHelper;

        internal CourseHelper(CourseInformationProvider provider, AccessHelper accesshelper)
        {
            _Provider = provider;
            _AccessHelper = accesshelper;
        }

        /// <summary>
        /// 取得課程資料
        /// </summary>
        /// <param name="identities"></param>
        public List<CourseRecord> GetCourse(System.Collections.Generic.IEnumerable<string> identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetCourse(identities);
        }

        /// <summary>
        /// 取得課程資料
        /// </summary>
        /// <param name="identities"></param>
        public List<CourseRecord> GetCourse(params  string[] identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetCourse(identities);
        }

        /// <summary>
        /// 取得畫面上選取的課程資料
        /// </summary>
        public List<CourseRecord> GetSelectedCourse()
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetSelectedCourse();
        }

        /// <summary>
        /// 取得班級開課課程資料
        /// </summary>
        /// <param name="schoolyear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="classrecord">班級</param>
        public List<CourseRecord> GetClassCourse(int schoolyear, int semester, ClassRecord classrecord)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetClassCourse(schoolyear, semester, classrecord);
        }

        /// <summary>
        /// 取得所有課程資料
        /// </summary>
        /// <param name="schoolyear">學年度</param>
        /// <param name="semester">學期</param>
        public List<CourseRecord> GetAllCourse(int schoolyear, int semester)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetAllCourse(schoolyear, semester);
        }

        /// <summary>
        /// 取得教師授課課程
        /// </summary>
        /// <param name="schoolyear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="teacher">教師</param>
        public List<CourseRecord> GetTeacherCourse(int schoolyear, int semester, TeacherRecord teacher)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetTeacherCourse(schoolyear, semester, teacher);
        }

        /// <summary>
        /// 呼叫FillExamScore方法時
        /// </summary>
        public static event EventHandler<FillEventArgs<CourseRecord>> FillingStudentAttend;

        /// <summary>
        /// 填入修課學生資料
        /// </summary>
        /// <param name="courses">課程</param>
        public void FillStudentAttend(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            if ( _Provider != null )
                _Provider.FillStudentAttend(courses);
            if ( FillingStudentAttend != null )
                FillingStudentAttend.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// 填入修課學生資料
        /// </summary>
        /// <param name="courses">課程</param>
        public void FillStudentAttend(params SmartSchool.Customization.Data.CourseRecord[] courses)
        {
            if ( _Provider != null )
                _Provider.FillStudentAttend(courses);
            if ( FillingStudentAttend != null )
                FillingStudentAttend.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// 呼叫FillField方法時
        /// </summary>
        public static event EventHandler<FillFieldEventArgs<CourseRecord>> FillingField;

        /// <summary>
        /// 填入課程其他資料
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="courses">要填入資料的課程</param>
        public void FillField(string fieldName, System.Collections.Generic.IEnumerable<CourseRecord> courses)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, courses);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<CourseRecord>(_AccessHelper, fieldName, courses));
        }

        /// <summary>
        /// 填入課程其他資料
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="courses">要填入資料的課程</param>
        public void FillField(string fieldName, params CourseRecord[] courses)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, courses);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<CourseRecord>(_AccessHelper, fieldName, courses));
        }

        /// <summary>
        /// 呼叫FillExamScore方法時
        /// </summary>
        public static event EventHandler<FillEventArgs<CourseRecord>> FillingExamScore;

        /// <summary>
        /// 填入學生修課評量成績
        /// </summary>
        /// <param name="courses">課程</param>
        public void FillExamScore(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            if ( _Provider != null )
                _Provider.FillExamScore(courses);
            if ( FillingExamScore != null )
                FillingExamScore.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// 填入學生修課評量成績
        /// </summary>
        /// <param name="courses">課程</param>
        public void FillExamScore(params  CourseRecord[] courses)
        {
            if ( _Provider != null )
                _Provider.FillExamScore(courses);
            if ( FillingExamScore != null )
                FillingExamScore.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// 呼叫FillExam方法時
        /// </summary>
        public static event EventHandler<FillEventArgs<CourseRecord>> FillingExam;

        /// <summary>
        /// 填入考試資訊
        /// </summary>
        /// <param name="courses">課程</param>
        public void FillExam(params  CourseRecord[] courses)
        {
            if ( _Provider != null )
                _Provider.FillExam(courses);
            if ( FillingExam != null )
                FillingExam.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// 填入考試資訊
        /// </summary>
        /// <param name="courses">課程</param>
        public void FillExam(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            if ( _Provider != null )
                _Provider.FillExam(courses);
            if ( FillingExam != null )
                FillingExam.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// 依課程名稱取得課程
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="courseName">課程名稱</param>
        /// <returns>如果查無課程名稱則傳回null</returns>
        public CourseRecord GetCourseByCourseName(int schoolYear, int semester, string courseName)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetCourseByCourseName(schoolYear, semester, courseName);
        }
    }
}
