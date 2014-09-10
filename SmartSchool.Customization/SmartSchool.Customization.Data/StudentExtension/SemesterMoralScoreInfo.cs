using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 學生學期德行成績資訊
    /// </summary>
    public  interface SemesterMoralScoreInfo
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
        /// 導師加減分
        /// </summary>
        decimal SupervisedByDiff
        {
            get;
        }
        /// <summary>
        /// 導師評語
        /// </summary>
        string SupervisedByComment
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
        /// 其他項目加減分(Key:項目名稱,Value:項目加減分數)
        /// </summary>
        Dictionary<string, decimal> OtherDiff
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
