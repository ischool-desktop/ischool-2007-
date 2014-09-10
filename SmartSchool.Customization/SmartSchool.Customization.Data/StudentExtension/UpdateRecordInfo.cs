using System;
namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 學生異動資訊
    /// </summary>
    public interface UpdateRecordInfo
    {
        /// <summary>
        /// 核準日期(回填)
        /// </summary>
        string ADDate
        {
            get;
        }

        /// <summary>
        /// 核準文號(回填)
        /// </summary>
        string ADNumber
        {
            get;
        }

        /// <summary>
        /// 生日
        /// </summary>
        string BirthDate
        {
            get;
        }

        /// <summary>
        /// 備註
        /// </summary>
        string Comment
        {
            get;
        }

        /// <summary>
        /// 科別
        /// </summary>
        string Department
        {
            get;
        }

        /// <summary>
        /// 性別
        /// </summary>
        string Gender
        {
            get;
        }

        /// <summary>
        /// 年級
        /// </summary>
        string GradeYear
        {
            get;
        }

        /// <summary>
        /// 畢(結)業證書字號
        /// </summary>
        string GraduateCertificateNumber
        {
            get;
        }

        /// <summary>
        /// 入學資格-畢業國中
        /// </summary>
        string GraduateSchool
        {
            get;
        }

        /// <summary>
        /// 入學資格-畢業國中所在地代碼
        /// </summary>
        string GraduateSchoolLocationCode
        {
            get;
        }

        /// <summary>
        /// 身份證號
        /// </summary>
        string IDNumber
        {
            get;
        }

        /// <summary>
        /// 最後核準日期
        /// </summary>
        string LastADDate
        {
            get;
        }

        /// <summary>
        /// 最後核準文號
        /// </summary>
        string LastADNumber
        {
            get;
        }

        /// <summary>
        /// 最後核準異動代碼
        /// </summary>
        string LastUpdateCode
        {
            get;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// 新學號
        /// </summary>
        string NewStudentNumber
        {
            get;
        }

        /// <summary>
        /// 轉入前學生資料-科別
        /// </summary>
        string PreviousDepartment
        {
            get;
        }

        /// <summary>
        /// 轉入前學生資料-年級
        /// </summary>
        string PreviousGradeYear
        {
            get;
        }

        /// <summary>
        /// 轉入前學生資料-學校
        /// </summary>
        string PreviousSchool
        {
            get;
        }

        /// <summary>
        /// 轉入前學生資料-(最後核準日期)
        /// </summary>
        string PreviousSchoolLastADDate
        {
            get;
        }

        /// <summary>
        /// 轉入前學生資料-(最後核準文號)
        /// </summary>
        string PreviousSchoolLastADNumber
        {
            get;
        }

        /// <summary>
        /// 轉入前學生資料-學號
        /// </summary>
        string PreviousStudentNumber
        {
            get;
        }

        /// <summary>
        /// 學號
        /// </summary>
        string StudentNumber
        {
            get;
        }

        /// <summary>
        /// 異動代碼
        /// </summary>
        string UpdateCode
        {
            get;
        }

        /// <summary>
        /// 異動日期
        /// </summary>
        string UpdateDate
        {
            get;
        }

        /// <summary>
        /// 原因及事項
        /// </summary>
        string UpdateDescription
        {
            get;
        }

        /// <summary>
        /// 異動種類(新生異動、轉入異動、...)
        /// </summary>
        string UpdateRecordType
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
