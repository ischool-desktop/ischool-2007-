using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 父母監護人資料
    /// </summary>
    public interface ParentInfo
    {
        /// <summary>
        /// 父親姓名
        /// </summary>
        string FatherName
        {
            get;
        }

        /// <summary>
        /// 父親存歿
        /// </summary>
        bool FatherLiving
        {
            get;
        }

        /// <summary>
        /// 母親姓名
        /// </summary>
        string MotherName
        {
            get;
        }

        /// <summary>
        /// 母親存歿
        /// </summary>
        bool MotherLiving
        {
            get;
        }

        /// <summary>
        /// 監護人姓名
        /// </summary>
        string CustodianName
        {
            get;
        }

        /// <summary>
        /// 監護人稱謂
        /// </summary>
        string CustodianRelationship
        {
            get;
        }
    }
}
