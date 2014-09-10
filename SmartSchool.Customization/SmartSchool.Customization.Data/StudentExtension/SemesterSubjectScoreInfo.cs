using System;
namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 學生學期科目成績資訊
    /// </summary>
    public interface SemesterSubjectScoreInfo
    {
        /// <summary>
        /// 學分數
        /// </summary>
        int Credit
        {
            get;
        }

        /// <summary>
        /// 詳細資料
        /// </summary>
        System.Xml.XmlElement Detail
        {
            get;
        }

        /// <summary>
        /// 成績年級
        /// </summary>
        int GradeYear
        {
            get;
        }

        /// <summary>
        /// 級別
        /// </summary>
        string Level
        {
            get;
        }

        /// <summary>
        /// 必修
        /// </summary>
        bool Require
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
        /// 分數
        /// </summary>
        decimal Score
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
        /// 科目
        /// </summary>
        string Subject
        {
            get;
        }

        /// <summary>
        /// 取得學分
        /// </summary>
        bool Pass
        {
            get;
        }
    }
}
