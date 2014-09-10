using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 缺曠資訊
    /// </summary>
    public interface  AttendanceInfo
    {
        /// <summary>
        /// 缺曠假別
        /// </summary>
        string Absence
        {
            get;
        }

        /// <summary>
        /// 缺曠日期
        /// </summary>
        System.DateTime OccurDate
        {
            get;
        }

        /// <summary>
        /// 缺曠節次
        /// </summary>
        string Period
        {
            get;
        }

        /// <summary>
        /// 節次的類別
        /// </summary>
        string PeriodType
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
        /// 詳細資料
        /// </summary>
        System.Xml.XmlElement Detail
        {
            get;
        }
    }
}
