using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 學期歷程，紀錄學生在學期間各別學期的相關訊息
    /// </summary>
    public interface SemesterHistory
    {
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
        /// 座號
        /// </summary>
        string SeatNo
        {
            get;
        }

        /// <summary>
        /// 班級名稱
        /// </summary>
        string ClassName { get; }
    }
}
