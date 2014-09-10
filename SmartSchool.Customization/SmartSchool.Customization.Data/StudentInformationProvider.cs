using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data.StudentExtension;

namespace SmartSchool.Customization.Data
{
    public interface StudentInformationProvider:ICloneable
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
        /// 用學生編號清單取得學生資料
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetStudents(IEnumerable<string> identities);

        /// <summary>
        /// 取得在系統畫面中選取的學生
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetSelectedStudent();

        /// <summary>
        /// 取得所有的在校學生，在校學生指學生狀態為"一般"、"延修"及"輟學"(國中才會有)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetAllStudent();

        /// <summary>
        /// 填入學生缺曠
        /// </summary>
        void FillAttendance(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 填入學生缺曠
        /// </summary>
        void FillAttendance(int schoolYear, int semester, System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 填入學生獎懲
        /// </summary>
        void FillReward(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 填入學生獎懲
        /// </summary>
        void FillReward(int schoolYear, int semester, System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 填入學生異動資料
        /// </summary>
        void FillUpdateRecord(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 填入學生學期歷程
        /// </summary>
        void FillSemesterHistory(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 填入學生修課評量成績
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="students">學生</param>
        void FillExamScore(int schoolYear, int semester, System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students);

        /// <summary>
        /// 填入學生學期科目成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
        void FillSemesterSubjectScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// 填入學生學期德行成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
        void FillSemesterMoralScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// 填入學生學期分項成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
        void FillSemesterEntryScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// 填入學生學年科目成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級)資料</param>
        void FillSchoolYearSubjectScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// 填入學生學年分項成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級)資料</param>
        void FillSchoolYearEntryScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// 填入學生修課資料
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
        void FillAttendCourse(int schoolYear, int semester, System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students);

        /// <summary>
        /// 填入學生聯絡資料
        /// </summary>
        void FillContactInfo(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 填入學生家長資料
        /// </summary>
        void FillParentInfo(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 填入學生相關欄位
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="students">要填入資料的學生</param>
        void FillField(string fieldName, System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// 依學號取得在校生
        /// </summary>
        /// <param name="studentNumber">學號</param>
        /// <returns>如查無此學生或不是在校生則傳回null</returns>
        StudentRecord GetStudentByStudentNumber(string studentNumber);

        /// <summary>
        /// 填入學生擴充欄位資料
        /// </summary>
        Dictionary<StudentRecord,Dictionary<string,string>> GetExtensionFields(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students, string nameSpace, string[] fields);

        /// <summary>
        /// 寫入延伸欄位資料
        /// </summary>
        void SetExtensionFields(string nameSpace, string field, IDictionary<StudentRecord, string> list);
    }
}
