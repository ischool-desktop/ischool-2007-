using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ArchivalImage
{
    internal class Config
    {
        #region 自訂標示

        /// <summary>
        /// 核心科目標示
        /// </summary>
        private string _sign_core_subject;
        public string SignCoreSubject
        {
            get { return _sign_core_subject; }
            set { _sign_core_subject = value; }
        }

        /// <summary>
        /// 未取得學分標示
        /// </summary>
        private string _sign_failed;
        public string SignFailed
        {
            get { return _sign_failed; }
            set { _sign_failed = value; }
        }

        /// <summary>
        /// 學年調整成績標示
        /// </summary>
        private string _sign_school_year_adjust;
        public string SignSchoolYearAdjust
        {
            get { return _sign_school_year_adjust; }
            set { _sign_school_year_adjust = value; }
        }

        /// <summary>
        /// 手動調整成績標示
        /// </summary>
        private string _sign_manual_adjust;
        public string SignManualAdjust
        {
            get { return _sign_manual_adjust; }
            set { _sign_manual_adjust = value; }
        }

        /// <summary>
        /// 補考成績標示
        /// </summary>
        private string _sign_resit;
        public string SignResit
        {
            get { return _sign_resit; }
            set { _sign_resit = value; }
        }

        /// <summary>
        /// 重修成績標示
        /// </summary>
        private string _sign_retake;
        public string SignRetake
        {
            get { return _sign_retake; }
            set { _sign_retake = value; }
        }

        #endregion

        #region 資訊

        /// <summary>
        /// 家長 (or 監護人)
        /// </summary>
        private string _custodian;
        public string Custodian
        {
            get { return _custodian; }
            set { _custodian = value; }
        }

        /// <summary>
        /// 地址
        /// </summary>
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// 電話
        /// </summary>
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        #endregion

        #region 學生類別

        #endregion
    }
}