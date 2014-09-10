using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data.StudentExtension
{
        public interface AddressInfo
        {

            /// <summary>
            /// 縣市
            /// </summary>
            string County
            {
                get;
            }

            /// <summary>
            /// 鄉鎮
            /// </summary>
            string Town
            {
                get;
            }

            /// <summary>
            /// 村里街號
            /// </summary>
            string DetailAddress
            {
                get;
            }

            /// <summary>
            /// 郵遞區號
            /// </summary>
            string ZipCode
            {
                get;
            }

            /// <summary>
            /// 完整地址
            /// </summary>
            string FullAddress
            {
                get;
            }
        }
    public  interface ContactInfo
    {
        /// <summary>
        /// 戶籍電話
        /// </summary>
        string PermenantPhone
        {
            get;
        }

        /// <summary>
        /// 連絡電話
        /// </summary>
        string ContactPhone
        {
            get;
        }

        /// <summary>
        /// 戶籍地址
        /// </summary>
        AddressInfo PermanentAddress
        {
            get;
        }

        /// <summary>
        /// 聯絡地址
        /// </summary>
        SmartSchool.Customization.Data.StudentExtension.AddressInfo MailingAddress
        {
            get;
        }
    }
}
