using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public interface ClassInformationProvider : ICloneable
    {
        /// <summary>
        /// 資料存取器
        /// </summary>
        AccessHelper AccessHelper
        {
            get;
            set;
        }
        /// <summary>
        /// 快取暫存區
        /// </summary>
        System.Collections.Hashtable CachePool
        {
            get;
            set;
        }

        /// <summary>
        /// 用班級編號取得班級資料
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.ClassRecord> GetClass(IEnumerable<string> identities);

        /// <summary>
        /// 取得在系統畫面中選取的班級
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.ClassRecord> GetSelectedClass();

        /// <summary>
        /// 取得所有班級
        /// </summary>
        List<ClassRecord> GetAllClass();

        /// <summary>
        /// 填入班級相關欄位
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="classes">要填入資料的班級</param>
        void FillField(string fieldName, System.Collections.Generic.IEnumerable<ClassRecord> classes);

        /// <summary>
        /// 依班級名稱取得班級
        /// </summary>
        /// <param name="className">班級名稱</param>
        /// <returns>如果查無班級名稱則傳回null</returns>
        ClassRecord GetClassByClassName(string className);

    }
}
