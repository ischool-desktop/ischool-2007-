using System;
namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 學生學年分項成績資訊
    /// </summary>
    public interface SchoolYearEntryScoreInfo
    {
        /// <summary>
        /// 分項
        /// </summary>
        string Entry
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
        /// 學年度
        /// </summary>
        int SchoolYear
        {
            get;
        }

        /// <summary>
        /// 成績
        /// </summary>
        decimal Score
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
