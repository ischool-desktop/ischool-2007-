using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class TeacherHelper
    {
        private TeacherInformationProvider _Provider;

        private AccessHelper _AccessHelper;

        internal TeacherHelper(TeacherInformationProvider provider, AccessHelper accesshelper)
        {
            _Provider = provider;
            _AccessHelper = accesshelper;
        }

        /// <summary>
        /// ���o�Юv���
        /// </summary>
        public List<TeacherRecord> GetTeacher(System.Collections.Generic.IEnumerable<string> identities)
        {
            if (_Provider == null)
                throw new Exception("Provider�|���]�w");
            return _Provider.GetTeacher(identities);
        }


        /// <summary>
        /// ���o�Юv���
        /// </summary>
        public List<TeacherRecord> GetTeacher(params string[] identities)
        {
            if (_Provider == null)
                throw new Exception("Provider�|���]�w");
            return _Provider.GetTeacher(identities);
        }

        /// <summary>
        /// ���o�Ҧ��Юv���
        /// </summary>
        public List<TeacherRecord> GetAllTeacher()
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetAllTeacher();
        }

        /// <summary>
        /// ���o�e���W������Юv���
        /// </summary>
        public List<TeacherRecord> GetSelectedTeacher()
        {
            if (_Provider == null)
                throw new Exception("Provider�|���]�w");
            return _Provider.GetSelectedTeacher();
        }

        public static event EventHandler<GettingLectureTeacherEventArgs> GettingLectureTeacher;
        /// <summary>
        /// ���o�½ұЮv
        /// </summary>
        public List<TeacherRecord> GetLectureTeacher(CourseRecord course)
        {
            if ( GettingLectureTeacher != null )
            {
                GettingLectureTeacherEventArgs args = new GettingLectureTeacherEventArgs(_AccessHelper, course);
                GettingLectureTeacher.Invoke(this, args);
                return args.Teachers;
            }
            else
            {
                if ( _Provider == null )
                    throw new Exception("Provider�|���]�w");
                return _Provider.GetLectureTeacher(course);
            }
        }

        /// <summary>
        /// �I�sFillField��k��
        /// </summary>
        public static event EventHandler<FillFieldEventArgs<TeacherRecord>> FillingField;

        /// <summary>
        /// ��J�Юv��L���
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="teachers">�n��J��ƪ��Юv</param>
        public void FillField(string fieldName, System.Collections.Generic.IEnumerable<TeacherRecord> teachers)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, teachers);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<TeacherRecord>(_AccessHelper, fieldName, teachers));
        }

        /// <summary>
        /// ��J�Юv��L���
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="teachers">�n��J��ƪ��Юv</param>
        public void FillField(string fieldName, params TeacherRecord[] teachers)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, teachers);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<TeacherRecord>(_AccessHelper, fieldName, teachers));
        }

        /// <summary>
        /// ���ѧO�W�٨��o�Юv
        /// </summary>
        /// <param name="identifiableName">�Юv�ѧO�W��</param>
        /// <returns>�d�L���Юv�h�Ǧ^null</returns>
        public TeacherRecord GetTeacherByIdentifiableName(string identifiableName)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetTeacherByIdentifiableName(identifiableName);
        }
    }
}
