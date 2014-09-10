using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class CourseHelper
    {
        private CourseInformationProvider _Provider;

        private AccessHelper _AccessHelper;

        internal CourseHelper(CourseInformationProvider provider, AccessHelper accesshelper)
        {
            _Provider = provider;
            _AccessHelper = accesshelper;
        }

        /// <summary>
        /// ���o�ҵ{���
        /// </summary>
        /// <param name="identities"></param>
        public List<CourseRecord> GetCourse(System.Collections.Generic.IEnumerable<string> identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetCourse(identities);
        }

        /// <summary>
        /// ���o�ҵ{���
        /// </summary>
        /// <param name="identities"></param>
        public List<CourseRecord> GetCourse(params  string[] identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetCourse(identities);
        }

        /// <summary>
        /// ���o�e���W������ҵ{���
        /// </summary>
        public List<CourseRecord> GetSelectedCourse()
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetSelectedCourse();
        }

        /// <summary>
        /// ���o�Z�Ŷ}�ҽҵ{���
        /// </summary>
        /// <param name="schoolyear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="classrecord">�Z��</param>
        public List<CourseRecord> GetClassCourse(int schoolyear, int semester, ClassRecord classrecord)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetClassCourse(schoolyear, semester, classrecord);
        }

        /// <summary>
        /// ���o�Ҧ��ҵ{���
        /// </summary>
        /// <param name="schoolyear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        public List<CourseRecord> GetAllCourse(int schoolyear, int semester)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetAllCourse(schoolyear, semester);
        }

        /// <summary>
        /// ���o�Юv�½ҽҵ{
        /// </summary>
        /// <param name="schoolyear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="teacher">�Юv</param>
        public List<CourseRecord> GetTeacherCourse(int schoolyear, int semester, TeacherRecord teacher)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetTeacherCourse(schoolyear, semester, teacher);
        }

        /// <summary>
        /// �I�sFillExamScore��k��
        /// </summary>
        public static event EventHandler<FillEventArgs<CourseRecord>> FillingStudentAttend;

        /// <summary>
        /// ��J�׽Ҿǥ͸��
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        public void FillStudentAttend(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            if ( _Provider != null )
                _Provider.FillStudentAttend(courses);
            if ( FillingStudentAttend != null )
                FillingStudentAttend.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// ��J�׽Ҿǥ͸��
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        public void FillStudentAttend(params SmartSchool.Customization.Data.CourseRecord[] courses)
        {
            if ( _Provider != null )
                _Provider.FillStudentAttend(courses);
            if ( FillingStudentAttend != null )
                FillingStudentAttend.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// �I�sFillField��k��
        /// </summary>
        public static event EventHandler<FillFieldEventArgs<CourseRecord>> FillingField;

        /// <summary>
        /// ��J�ҵ{��L���
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="courses">�n��J��ƪ��ҵ{</param>
        public void FillField(string fieldName, System.Collections.Generic.IEnumerable<CourseRecord> courses)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, courses);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<CourseRecord>(_AccessHelper, fieldName, courses));
        }

        /// <summary>
        /// ��J�ҵ{��L���
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="courses">�n��J��ƪ��ҵ{</param>
        public void FillField(string fieldName, params CourseRecord[] courses)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, courses);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<CourseRecord>(_AccessHelper, fieldName, courses));
        }

        /// <summary>
        /// �I�sFillExamScore��k��
        /// </summary>
        public static event EventHandler<FillEventArgs<CourseRecord>> FillingExamScore;

        /// <summary>
        /// ��J�ǥͭ׽ҵ��q���Z
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        public void FillExamScore(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            if ( _Provider != null )
                _Provider.FillExamScore(courses);
            if ( FillingExamScore != null )
                FillingExamScore.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// ��J�ǥͭ׽ҵ��q���Z
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        public void FillExamScore(params  CourseRecord[] courses)
        {
            if ( _Provider != null )
                _Provider.FillExamScore(courses);
            if ( FillingExamScore != null )
                FillingExamScore.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// �I�sFillExam��k��
        /// </summary>
        public static event EventHandler<FillEventArgs<CourseRecord>> FillingExam;

        /// <summary>
        /// ��J�Ҹո�T
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        public void FillExam(params  CourseRecord[] courses)
        {
            if ( _Provider != null )
                _Provider.FillExam(courses);
            if ( FillingExam != null )
                FillingExam.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// ��J�Ҹո�T
        /// </summary>
        /// <param name="courses">�ҵ{</param>
        public void FillExam(System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            if ( _Provider != null )
                _Provider.FillExam(courses);
            if ( FillingExam != null )
                FillingExam.Invoke(this, new FillEventArgs<CourseRecord>(_AccessHelper, courses));
        }

        /// <summary>
        /// �̽ҵ{�W�٨��o�ҵ{
        /// </summary>
        /// <param name="schoolYear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="courseName">�ҵ{�W��</param>
        /// <returns>�p�G�d�L�ҵ{�W�٫h�Ǧ^null</returns>
        public CourseRecord GetCourseByCourseName(int schoolYear, int semester, string courseName)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetCourseByCourseName(schoolYear, semester, courseName);
        }
    }
}
