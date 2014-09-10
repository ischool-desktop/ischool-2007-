using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data.StudentExtension;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// �ǥͬ���
    /// </summary>
    public interface StudentRecord
    {
        /// <summary>
        /// �ʧO
        /// </summary>
        string Gender
        {
            get;
        }

        /// <summary>
        /// �t�νs��
        /// </summary>
        string StudentID
        {
            get;
        }

        /// <summary>
        /// �O�_���b�վǥ�
        /// </summary>
        bool IsNormalStudent
        {
            get;
        }

        /// <summary>
        /// �m�W
        /// </summary>
        string StudentName
        {
            get;
        }

        /// <summary>
        /// �y��
        /// </summary>
        string SeatNo
        {
            get;
        }

        /// <summary>
        /// ���A
        /// </summary>
        string Status
        {
            get;
        }

        /// <summary>
        /// �Ǹ�
        /// </summary>
        string StudentNumber
        {
            get;
        }

        /// <summary>
        /// �ͤ�
        /// </summary>
        string Birthday
        {
            get;
        }

        /// <summary>
        /// ��O
        /// </summary>
        string Department { get;}

        /// <summary>
        /// �����Ҹ�
        /// </summary>
        string IDNumber
        {
            get;
        }

        /// <summary>
        /// �ǥ����O
        /// </summary>
        CategoryCollection StudentCategorys
        {
            get;
        }

        /// <summary>
        /// �ǥͯ��m(�������I�sFillAttendance)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.AttendanceInfo> AttendanceList
        {
            get;
        }

        /// <summary>
        /// �ǥͭ׽Ҧ��Z(�������I�sFillExamScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.ExamScoreInfo> ExamScoreList
        {
            get;
        }

        /// <summary>
        /// �ǥͼ��g(�������I�sFillReward)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.RewardInfo> RewardList
        {
            get;
        }

        /// <summary>
        /// �ǥ;Ǧ~�������Z(�������I�sFillSchoolYearEntryScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.SchoolYearEntryScoreInfo> SchoolYearEntryScoreList
        {
            get;
        }

        /// <summary>
        /// �ǥ;Ǧ~��ئ��Z(�������I�sFillSchoolYearSubjectScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo> SchoolYearSubjectScoreList
        {
            get;
        }

        /// <summary>
        /// �ǥ;Ǵ��������Z(�������I�sFillSemesterEntryScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.SemesterEntryScoreInfo> SemesterEntryScoreList
        {
            get;
        }

        /// <summary>
        /// �ǥ;Ǵ���ئ��Z(�������I�sFillSemesterSubjectScore)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.SemesterSubjectScoreInfo> SemesterSubjectScoreList
        {
            get;
        }

        /// <summary>
        /// �ǥͲ��ʸ��(�������I�sFillUpdateRecord)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo> UpdateRecordList
        {
            get;
        }

        /// <summary>
        /// �׽Ҹ��(�������I�sFillAttendCourse)
        /// </summary>
        List<StudentAttendCourseRecord> AttendCourseList
        {
            get;
        }

        /// <summary>
        /// ���ݯZ��
        /// </summary>
        ClassRecord RefClass
        {
            get;
        }

        /// <summary>
        /// �p����T(�������I�sFillContactInfo)
        /// </summary>
        ContactInfo ContactInfo
        {
            get;
            set;
        }

        /// <summary>
        /// �a����T(�������I�sFillParentInfo)
        /// </summary>
        ParentInfo ParentInfo
        {
            get;
            set;
        }

        /// <summary>
        /// ��L������(�ݥ��I�sFillField��J�ݭn�����)
        /// </summary>
        System.Collections.Generic.Dictionary<string, object> Fields
        {
            get;
        }
        /// <summary>
        /// �Ǵ��w�榨�Z(�������I�sFillSemsesterMoralScore)
        /// </summary>
       List< SemesterMoralScoreInfo> SemesterMoralScoreList
        {
            get;
        }

        /// <summary>
        /// �Ǵ��w�榨�Z(�������I�sFillSemesterHistory)
        /// </summary>
        List<SmartSchool.Customization.Data.StudentExtension.SemesterHistory> SemesterHistoryList
        {
            get;
        }

        /// <summary>
        /// �ǥͩ��������
        /// </summary>
        ExtensionFieldCollection ExtensionFields
        {
            get;
        }
    }
}
