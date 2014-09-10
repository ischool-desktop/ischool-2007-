using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// �ǥ;Ǵ��w�榨�Z��T
    /// </summary>
    public  interface SemesterMoralScoreInfo
    {
        /// <summary>
        /// ���Z�~��
        /// </summary>
        int GradeYear
        {
            get;
        }

        /// <summary>
        /// �Ǧ~��
        /// </summary>
        int SchoolYear
        {
            get;
        }

        /// <summary>
        /// �ɮv�[���
        /// </summary>
        decimal SupervisedByDiff
        {
            get;
        }
        /// <summary>
        /// �ɮv���y
        /// </summary>
        string SupervisedByComment
        {
            get;
        }

        /// <summary>
        /// �Ǵ�
        /// </summary>
        int Semester
        {
            get;
        }
        /// <summary>
        /// ��L���إ[���(Key:���ئW��,Value:���إ[�����)
        /// </summary>
        Dictionary<string, decimal> OtherDiff
        {
            get;
        }

        /// <summary>
        /// �ԲӸ��
        /// </summary>
        System.Xml.XmlElement Detail
        {
            get;
        }
    }
}
