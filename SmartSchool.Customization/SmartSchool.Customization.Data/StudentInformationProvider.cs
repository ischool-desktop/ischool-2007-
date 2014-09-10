using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data.StudentExtension;

namespace SmartSchool.Customization.Data
{
    public interface StudentInformationProvider:ICloneable
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
        /// �ξǥͽs���M����o�ǥ͸��
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetStudents(IEnumerable<string> identities);

        /// <summary>
        /// ���o�b�t�εe����������ǥ�
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetSelectedStudent();

        /// <summary>
        /// ���o�Ҧ����b�վǥ͡A�b�վǥͫ��ǥͪ��A��"�@��"�B"����"��"����"(�ꤤ�~�|��)
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetAllStudent();

        /// <summary>
        /// ��J�ǥͯ��m
        /// </summary>
        void FillAttendance(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// ��J�ǥͯ��m
        /// </summary>
        void FillAttendance(int schoolYear, int semester, System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// ��J�ǥͼ��g
        /// </summary>
        void FillReward(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// ��J�ǥͼ��g
        /// </summary>
        void FillReward(int schoolYear, int semester, System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// ��J�ǥͲ��ʸ��
        /// </summary>
        void FillUpdateRecord(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// ��J�ǥ;Ǵ����{
        /// </summary>
        void FillSemesterHistory(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// ��J�ǥͭ׽ҵ��q���Z
        /// </summary>
        /// <param name="schoolYear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="students">�ǥ�</param>
        void FillExamScore(int schoolYear, int semester, System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students);

        /// <summary>
        /// ��J�ǥ;Ǵ���ئ��Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        void FillSemesterSubjectScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// ��J�ǥ;Ǵ��w�榨�Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        void FillSemesterMoralScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// ��J�ǥ;Ǵ��������Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        void FillSemesterEntryScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// ��J�ǥ;Ǧ~��ئ��Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~��)���</param>
        void FillSchoolYearSubjectScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// ��J�ǥ;Ǧ~�������Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~��)���</param>
        void FillSchoolYearEntryScore(System.Collections.Generic.IEnumerable<StudentRecord> students, bool filterRepeat);

        /// <summary>
        /// ��J�ǥͭ׽Ҹ��
        /// </summary>
        /// <param name="schoolYear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        void FillAttendCourse(int schoolYear, int semester, System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students);

        /// <summary>
        /// ��J�ǥ��p�����
        /// </summary>
        void FillContactInfo(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// ��J�ǥͮa�����
        /// </summary>
        void FillParentInfo(System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// ��J�ǥͬ������
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="students">�n��J��ƪ��ǥ�</param>
        void FillField(string fieldName, System.Collections.Generic.IEnumerable<StudentRecord> students);

        /// <summary>
        /// �̾Ǹ����o�b�ե�
        /// </summary>
        /// <param name="studentNumber">�Ǹ�</param>
        /// <returns>�p�d�L���ǥͩΤ��O�b�եͫh�Ǧ^null</returns>
        StudentRecord GetStudentByStudentNumber(string studentNumber);

        /// <summary>
        /// ��J�ǥ��X�R�����
        /// </summary>
        Dictionary<StudentRecord,Dictionary<string,string>> GetExtensionFields(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students, string nameSpace, string[] fields);

        /// <summary>
        /// �g�J���������
        /// </summary>
        void SetExtensionFields(string nameSpace, string field, IDictionary<StudentRecord, string> list);
    }
}
