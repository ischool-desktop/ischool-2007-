using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public interface CourseInformationProvider : ICloneable
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
        /// ���o�ҵ{���
        /// </summary>
        /// <param name="identities"></param>
        /// <returns></returns>
        List<CourseRecord> GetCourse(System.Collections.Generic.IEnumerable<string> identities);

        /// <summary>
        /// ���o�Ҧ��ҵ{���
        /// </summary>
        /// <param name="schoolyear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        List<CourseRecord> GetAllCourse(int schoolyear, int semester);

        /// <summary>
        /// ���o�e���W������ҵ{���
        /// </summary>
        List<CourseRecord> GetSelectedCourse();

        /// <summary>
        /// ���o�Z�Ŷ}�ҽҵ{���
        /// </summary>
        /// <param name="schoolyear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="classrecord">�Z��</param>
        List<CourseRecord> GetClassCourse(int schoolyear, int semester, ClassRecord classrecord);

        /// <summary>
        /// ��J�ǥͭ׽Ҹ��
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        void FillStudentAttend(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses);

        /// <summary>
        /// ���o�Юv�½ҽҵ{
        /// </summary>
        /// <param name="schoolyear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="teacher">�Юv</param>
        List<CourseRecord> GetTeacherCourse(int schoolyear, int semester, TeacherRecord teacher);

        /// <summary>
        /// ��J�ҵ{�������
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="courses">�n��J��ƪ��ҵ{</param>
        void FillField(string fieldName, System.Collections.Generic.IEnumerable<CourseRecord> courses);

        /// <summary>
        /// ��J�׽Ҿǥ͵��q���Z
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        void FillExamScore(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses);

        /// <summary>
        /// ��J�Ҹո�T
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        void FillExam(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses);

        /// <summary>
        /// �̽ҵ{�W�٨��o�ҵ{
        /// </summary>
        /// <param name="schoolYear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="courseName">�ҵ{�W��</param>
        /// <returns>�p�G�d�L�ҵ{�W�٫h�Ǧ^null</returns>
        CourseRecord GetCourseByCourseName(int schoolYear, int semester, string courseName);
    }
}
