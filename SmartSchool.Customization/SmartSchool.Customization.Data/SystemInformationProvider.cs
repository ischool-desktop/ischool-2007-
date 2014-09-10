using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
     public interface SystemInformationProvider
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
        /// 中文校名
        /// </summary>
         string SchoolChineseName
        {
            get;
        }
        /// <summary>
        /// 英文校名
        /// </summary>
         string SchoolEnglishName
        {
            get;
        }
        /// <summary>
        /// 學校代碼
        /// </summary>
         string SchoolCode
        {
            get;
        }
        /// <summary>
        /// 學校電話。
        /// </summary>
         string Telephone
        {
            get;
        }
        /// <summary>
        /// 學校傳真。
        /// </summary>
         string Fax
        {
            get;
        }
        /// <summary>
        /// 學校地址。
        /// </summary>
         string Address
        {
            get;
        }
         /// <summary>
         /// 登入帳號相關設定資料
         /// </summary>
         PreferenceCollection Preference{get;}
         /// <summary>
         /// 全校通用設定資料
         /// </summary>
         ConfigurationCollection Configuration { get;}
         object GetField(string fieldName);
    }
}
