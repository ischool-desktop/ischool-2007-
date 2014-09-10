using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// 學生修課紀錄
    /// </summary>
    public interface StudentAttendCourseRecord : StudentRecord, CourseRecord
    {
        /// <summary>
        /// 修課總成績
        /// </summary>
        decimal FinalScore
        {
            get;
        }

        /// <summary>
        /// 已有修課總成績
        /// </summary>
        bool HasFinalScore
        {
            get;
        }

        /// <summary>
        /// 校部訂
        /// </summary>
        string RequiredBy
        {
            get;
        }

        /// <summary>
        /// 必選修
        /// </summary>
        bool Required
        {
            get;
        }
    }


    /// <summary>
    /// 科目考試成績資訊
    /// </summary>
    public interface ExamScoreInfo : SmartSchool.Customization.Data.StudentAttendCourseRecord
    {

        /// <summary>
        /// 考試名稱
        /// </summary>
        string ExamName
        {
            get;
        }

        /// <summary>
        /// 考試成績
        /// </summary>
        decimal ExamScore
        {
            get;
        }

        /// <summary>
        /// 特殊情況
        /// </summary>
        string SpecialCase
        {
            get;
        }
    }
}
