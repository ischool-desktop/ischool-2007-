using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public interface TeacherInformationProvider : ICloneable
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
        /// 取得教師資料
        /// </summary>
        List<TeacherRecord> GetTeacher(System.Collections.Generic.IEnumerable<string> identities);

        /// <summary>
        /// 取得所有教師資料
        /// </summary>
        List<TeacherRecord> GetAllTeacher();

        /// <summary>
        /// 取得畫面上選取的教師資料
        /// </summary>
        List<TeacherRecord> GetSelectedTeacher();

        /// <summary>
        /// 取得授課教師
        /// </summary>
        List<TeacherRecord> GetLectureTeacher(CourseRecord course);

        /// <summary>
        /// 填入教師相關欄位
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="teachers">要填入資料的教師</param>
        void FillField(string fieldName, System.Collections.Generic.IEnumerable<TeacherRecord> teachers);

        /// <summary>
        /// 依識別名稱取得教師
        /// </summary>
        /// <param name="identifiableName">教師識別名稱</param>
        /// <returns>如查無此教師則傳回null</returns>
        TeacherRecord GetTeacherByIdentifiableName(string identifiableName);
    }
}
