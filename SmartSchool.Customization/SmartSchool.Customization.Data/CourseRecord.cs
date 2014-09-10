using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// 課程紀錄
    /// </summary>
    public interface CourseRecord
    {

        /// <summary>
        /// 系統編號
        /// </summary>
        int CourseID
        {
            get;
        }

        /// <summary>
        /// 課程名稱
        /// </summary>
        string CourseName
        {
            get;
        }

        /// <summary>
        /// 科目
        /// </summary>
        string Subject
        {
            get;
        }

        /// <summary>
        /// 科目級別
        /// </summary>
        string SubjectLevel
        {
            get;
        }

        /// <summary>
        /// 學年度
        /// </summary>
        int SchoolYear
        {
            get;
        }

        /// <summary>
        /// 學期
        /// </summary>
        int Semester
        {
            get;
        }

        /// <summary>
        /// 學分數
        /// </summary>
        int Credit
        {
            get;
        }

        /// <summary>
        /// 分項類別
        /// </summary>
        string Entry
        {
            get;
        }

        /// <summary>
        /// 不計學分
        /// </summary>
        bool NotIncludedInCredit
        {
            get;
        }

        /// <summary>
        /// 不需評量
        /// </summary>
        bool NotIncludedInCalc
        {
            get;
        }

        /// <summary>
        /// 課程類別
        /// </summary>
        CategoryCollection CourseCategorys
        {
            get;
        }

        /// <summary>
        /// 修課學生資料(必須先呼叫FillStudentAttend)
        /// </summary>
        List<StudentAttendCourseRecord> StudentAttendList
        {
            get;
        }

        /// <summary>
        /// 其他資料欄位(需先呼叫FillField填入需要的欄位)
        /// </summary>
        System.Collections.Generic.Dictionary<string, object> Fields
        {
            get;
        }

        /// <summary>
        /// 修課學生成績(必須先呼叫FillExamScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.ExamScoreInfo> ExamScoreList
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

        /// <summary>
        /// 考試清單(必須先呼叫FillExam)
        /// </summary>
        List<string> ExamList
        {
            get;
        }
    }
}
