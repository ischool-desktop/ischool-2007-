using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// 班級紀錄
    /// </summary>
    public interface ClassRecord
    {
        /// <summary>
        /// 系統編號
        /// </summary>
         string ClassID { get; }
        /// <summary>
        /// 班級名稱
        /// </summary>
         string ClassName { get; }
        /// <summary>
        /// 年級
        /// </summary>
         string GradeYear { get; }
        /// <summary>
        /// 科別
        /// </summary>
         string Department { get;}
        /// <summary>
        /// 班級類別
        /// </summary>
        CategoryCollection ClassCategorys { get;}

        /// <summary>
        /// 班導師
        /// </summary>
        TeacherRecord RefTeacher
        {
            get;
        }

        /// <summary>
        /// 班級學生
        /// </summary>
        List<StudentRecord> Students
        {
            get;
        }

        /// <summary>
        /// 其他資料欄位(需先呼叫FillField填入需要的欄位)
        /// </summary>
        System.Collections.Generic.Dictionary<string, object> Fields
        {
            get;
        }
    }
}
