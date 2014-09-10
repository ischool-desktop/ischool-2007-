using System;
namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 學生奨懲資訊
    /// </summary>
    public interface RewardInfo
    {
        /// <summary>
        /// 大功
        /// </summary>
        int AwardA
        {
            get;
        }

        /// <summary>
        /// 小功
        /// </summary>
        int AwardB
        {
            get;
        }

        /// <summary>
        /// 嘉獎
        /// </summary>
        int AwardC
        {
            get;
        }

        /// <summary>
        /// 已銷過
        /// </summary>
        bool Cleared
        {
            get;
        }

        /// <summary>
        /// 銷過日期
        /// </summary>
        System.DateTime ClearDate
        {
            get;
        }

        /// <summary>
        /// 大過
        /// </summary>
        int FaultA
        {
            get;
        }

        /// <summary>
        /// 小過
        /// </summary>
        int FaultB
        {
            get;
        }

        /// <summary>
        /// 警告
        /// </summary>
        int FaultC
        {
            get;
        }

        /// <summary>
        /// 日期
        /// </summary>
        System.DateTime OccurDate
        {
            get;
        }

        /// <summary>
        /// 地點
        /// </summary>
        string OccurPlace
        {
            get;
        }

        /// <summary>
        /// 原因
        /// </summary>
        string OccurReason
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
        /// 留校查看
        /// </summary>
        bool UltimateAdmonition
        {
            get;
        }

        /// <summary>
        /// 銷過事由
        /// </summary>
        string ClearReason
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
