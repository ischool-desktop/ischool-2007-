using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// �ǥͭ׽Ҭ���
    /// </summary>
    public interface StudentAttendCourseRecord : StudentRecord, CourseRecord
    {
        /// <summary>
        /// �׽��`���Z
        /// </summary>
        decimal FinalScore
        {
            get;
        }

        /// <summary>
        /// �w���׽��`���Z
        /// </summary>
        bool HasFinalScore
        {
            get;
        }

        /// <summary>
        /// �ճ��q
        /// </summary>
        string RequiredBy
        {
            get;
        }

        /// <summary>
        /// �����
        /// </summary>
        bool Required
        {
            get;
        }
    }


    /// <summary>
    /// ��ئҸզ��Z��T
    /// </summary>
    public interface ExamScoreInfo : SmartSchool.Customization.Data.StudentAttendCourseRecord
    {

        /// <summary>
        /// �ҸզW��
        /// </summary>
        string ExamName
        {
            get;
        }

        /// <summary>
        /// �Ҹզ��Z
        /// </summary>
        decimal ExamScore
        {
            get;
        }

        /// <summary>
        /// �S���p
        /// </summary>
        string SpecialCase
        {
            get;
        }
    }
}
