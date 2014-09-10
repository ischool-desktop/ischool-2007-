using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public interface CourseInformationProvider : ICloneable
    {
        /// <summary>
        /// 資料存取器
        /// </summary>
        AccessHelper AccessHelper
        {
            get;
            set;
        }
        /// <summary>
        /// 快取暫存區
        /// </summary>
        System.Collections.Hashtable CachePool
        {
            get;
            set;
        }
        /// <summary>
        /// 取得課程資料
        /// </summary>
        /// <param name="identities"></param>
        /// <returns></returns>
        List<CourseRecord> GetCourse(System.Collections.Generic.IEnumerable<string> identities);

        /// <summary>
        /// 取得所有課程資料
        /// </summary>
        /// <param name="schoolyear">學年度</param>
        /// <param name="semester">學期</param>
        List<CourseRecord> GetAllCourse(int schoolyear, int semester);

        /// <summary>
        /// 取得畫面上選取的課程資料
        /// </summary>
        List<CourseRecord> GetSelectedCourse();

        /// <summary>
        /// 取得班級開課課程資料
        /// </summary>
        /// <param name="schoolyear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="classrecord">班級</param>
        List<CourseRecord> GetClassCourse(int schoolyear, int semester, ClassRecord classrecord);

        /// <summary>
        /// 填入學生修課資料
        /// </summary>
        /// <param name="courses">課程</param>
        void FillStudentAttend(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses);

        /// <summary>
        /// 取得教師授課課程
        /// </summary>
        /// <param name="schoolyear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="teacher">教師</param>
        List<CourseRecord> GetTeacherCourse(int schoolyear, int semester, TeacherRecord teacher);

        /// <summary>
        /// 填入課程相關欄位
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="courses">要填入資料的課程</param>
        void FillField(string fieldName, System.Collections.Generic.IEnumerable<CourseRecord> courses);

        /// <summary>
        /// 填入修課學生評量成績
        /// </summary>
        /// <param name="courses">課程</param>
        void FillExamScore(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses);

        /// <summary>
        /// 填入考試資訊
        /// </summary>
        /// <param name="courses">課程</param>
        void FillExam(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses);

        /// <summary>
        /// 依課程名稱取得課程
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="courseName">課程名稱</param>
        /// <returns>如果查無課程名稱則傳回null</returns>
        CourseRecord GetCourseByCourseName(int schoolYear, int semester, string courseName);
    }
}
