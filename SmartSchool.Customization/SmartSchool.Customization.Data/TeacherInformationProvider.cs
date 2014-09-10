using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public interface TeacherInformationProvider : ICloneable
    {
        /// <summary>
        /// ��Ʀs����
        /// </summary>
        AccessHelper AccessHelper
        {
            get;
            set;
        }
        /// <summary>
        /// �֨��Ȧs��
        /// </summary>
        System.Collections.Hashtable CachePool
        {
            get;
            set;
        }
        /// <summary>
        /// ���o�Юv���
        /// </summary>
        List<TeacherRecord> GetTeacher(System.Collections.Generic.IEnumerable<string> identities);

        /// <summary>
        /// ���o�Ҧ��Юv���
        /// </summary>
        List<TeacherRecord> GetAllTeacher();

        /// <summary>
        /// ���o�e���W������Юv���
        /// </summary>
        List<TeacherRecord> GetSelectedTeacher();

        /// <summary>
        /// ���o�½ұЮv
        /// </summary>
        List<TeacherRecord> GetLectureTeacher(CourseRecord course);

        /// <summary>
        /// ��J�Юv�������
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="teachers">�n��J��ƪ��Юv</param>
        void FillField(string fieldName, System.Collections.Generic.IEnumerable<TeacherRecord> teachers);

        /// <summary>
        /// ���ѧO�W�٨��o�Юv
        /// </summary>
        /// <param name="identifiableName">�Юv�ѧO�W��</param>
        /// <returns>�p�d�L���Юv�h�Ǧ^null</returns>
        TeacherRecord GetTeacherByIdentifiableName(string identifiableName);
    }
}
