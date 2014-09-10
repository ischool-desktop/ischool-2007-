using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public interface TeacherRecord
    {
        /// <summary>
        /// 系統編號
        /// </summary>
         string TeacherID { get;}
        /// <summary>
        /// 教師姓名
        /// </summary>
         string TeacherName { get;}
        /// <summary>
        /// 狀態
        /// </summary>
         string Status { get; }
        /// <summary>
        /// 性別
        /// </summary>
         string Gender { get; }
        /// <summary>
        /// 教師類別
        /// </summary>
        CategoryCollection TeacherCategory { get;}

         /// <summary>
         /// 其他資料欄位(需先呼叫FillField填入需要的欄位)
         /// </summary>
        System.Collections.Generic.Dictionary<string, object> Fields
        {
            get;
        }

        /// <summary>
        /// 唯一識別名稱
        /// </summary>
        /// <remarks>
        /// 無暱稱時為 姓名
        /// 有暱稱時為 姓名(暱稱)
        /// 姓名加暱稱為唯一識別資料，同一組姓名加暱稱在系統內只允許一位教師擁有。
        /// PS. 暱稱可以亂取，不宜到處宣傳，切記
        /// </remarks>
        string IdentifiableName
        {
            get;
        }
    }
}
