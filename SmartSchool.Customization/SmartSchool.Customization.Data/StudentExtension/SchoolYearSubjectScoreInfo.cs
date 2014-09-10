using System;
namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 學生學年科目成績資訊
    /// </summary>
    public interface SchoolYearSubjectScoreInfo
    {
        /// <summary>
        /// 成績年級
        /// </summary>
        int GradeYear
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
        /// 科目
        /// </summary>
        string Subject
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
    }
}
