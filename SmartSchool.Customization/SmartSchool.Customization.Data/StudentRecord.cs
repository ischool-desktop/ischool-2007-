using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data.StudentExtension;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// 學生紀錄
    /// </summary>
    public interface StudentRecord
    {
        /// <summary>
        /// 性別
        /// </summary>
        string Gender
        {
            get;
        }

        /// <summary>
        /// 系統編號
        /// </summary>
        string StudentID
        {
            get;
        }

        /// <summary>
        /// 是否為在校學生
        /// </summary>
        bool IsNormalStudent
        {
            get;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        string StudentName
        {
            get;
        }

        /// <summary>
        /// 座號
        /// </summary>
        string SeatNo
        {
            get;
        }

        /// <summary>
        /// 狀態
        /// </summary>
        string Status
        {
            get;
        }

        /// <summary>
        /// 學號
        /// </summary>
        string StudentNumber
        {
            get;
        }

        /// <summary>
        /// 生日
        /// </summary>
        string Birthday
        {
            get;
        }

        /// <summary>
        /// 科別
        /// </summary>
        string Department { get;}

        /// <summary>
        /// 身分證號
        /// </summary>
        string IDNumber
        {
            get;
        }

        /// <summary>
        /// 學生類別
        /// </summary>
        CategoryCollection StudentCategorys
        {
            get;
        }

        /// <summary>
        /// 學生缺曠(必須先呼叫FillAttendance)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.AttendanceInfo> AttendanceList
        {
            get;
        }

        /// <summary>
        /// 學生修課成績(必須先呼叫FillExamScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.ExamScoreInfo> ExamScoreList
        {
            get;
        }

        /// <summary>
        /// 學生獎懲(必須先呼叫FillReward)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.RewardInfo> RewardList
        {
            get;
        }

        /// <summary>
        /// 學生學年分項成績(必須先呼叫FillSchoolYearEntryScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.SchoolYearEntryScoreInfo> SchoolYearEntryScoreList
        {
            get;
        }

        /// <summary>
        /// 學生學年科目成績(必須先呼叫FillSchoolYearSubjectScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo> SchoolYearSubjectScoreList
        {
            get;
        }

        /// <summary>
        /// 學生學期分項成績(必須先呼叫FillSemesterEntryScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.SemesterEntryScoreInfo> SemesterEntryScoreList
        {
            get;
        }

        /// <summary>
        /// 學生學期科目成績(必須先呼叫FillSemesterSubjectScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.SemesterSubjectScoreInfo> SemesterSubjectScoreList
        {
            get;
        }

        /// <summary>
        /// 學生異動資料(必須先呼叫FillUpdateRecord)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo> UpdateRecordList
        {
            get;
        }

        /// <summary>
        /// 修課資料(必須先呼叫FillAttendCourse)
        /// </summary>
        List<StudentAttendCourseRecord> AttendCourseList
        {
            get;
        }

        /// <summary>
        /// 所屬班級
        /// </summary>
        ClassRecord RefClass
        {
            get;
        }

        /// <summary>
        /// 聯絡資訊(必須先呼叫FillContactInfo)
        /// </summary>
        ContactInfo ContactInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 家長資訊(必須先呼叫FillParentInfo)
        /// </summary>
        ParentInfo ParentInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 其他資料欄位(需先呼叫FillField填入需要的欄位)
        /// </summary>
        System.Collections.Generic.Dictionary<string, object> Fields
        {
            get;
        }
        /// <summary>
        /// 學期德行成績(必須先呼叫FillSemsesterMoralScore)
        /// </summary>
       List< SemesterMoralScoreInfo> SemesterMoralScoreList
        {
            get;
        }

        /// <summary>
        /// 學期德行成績(必須先呼叫FillSemesterHistory)
        /// </summary>
        List<SmartSchool.Customization.Data.StudentExtension.SemesterHistory> SemesterHistoryList
        {
            get;
        }

        /// <summary>
        /// 學生延伸欄位資料
        /// </summary>
        ExtensionFieldCollection ExtensionFields
        {
            get;
        }
    }
}
