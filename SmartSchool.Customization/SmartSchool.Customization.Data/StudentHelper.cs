using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class StudentHelper
    {
        private StudentInformationProvider _Provider;

        private AccessHelper _AccessHelper;

        internal StudentHelper(StudentInformationProvider provider, AccessHelper accesshelper)
        {
            _Provider = provider;
            _AccessHelper = accesshelper;
        }

        /// <summary>
        /// �ξǥͽs�����o�ǥ͸��
        /// </summary>
        public SmartSchool.Customization.Data.StudentRecord GetStudent(string identity)
        {
            System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> list = GetStudents(identity);
            if ( list.Count == 0 )
                throw new Exception("���ǥͽs�����s�b");
            else
                return list[0];
        }

        /// <summary>
        /// �ξǥͽs���M����o�ǥ͸��
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetStudents(IEnumerable<string> identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetStudents(identities);
        }

        /// <summary>
        /// �ξǥͽs���M����o�ǥ͸��
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetStudents(params string[] identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetStudents(identities);
        }

        /// <summary>
        /// �̾Ǹ����o�b�ե�
        /// </summary>
        /// <param name="studentNumber">�Ǹ�</param>
        /// <returns>�p�d�L���ǥͩΤ��O�b�եͫh�Ǧ^null</returns>
        public StudentRecord GetStudentByStudentNumber(string studentNumber)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetStudentByStudentNumber(studentNumber);
        }

        /// <summary>
        /// ���o�b�t�εe����������ǥ�
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetSelectedStudent()
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetSelectedStudent();
        }

        /// <summary>
        /// ���o�Ҧ����b�վǥ͡A�b�վǥͫ��ǥͪ��A��"�@��"�B"����"��"����"(�ꤤ�~�|��)
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetAllStudent()
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetAllStudent();
        }

        #region FillExtensionFields
        /// <summary>
        /// ��J�ǥͩ��������
        /// </summary>
        public void FillExtensionFields(System.Collections.Generic.IEnumerable<StudentRecord> students, string nameSpace, params string[] fields)
        {
            if ( fields.Length == 0 )
            {
                foreach ( StudentRecord stu in students )
                {
                    stu.ExtensionFields.Clear(nameSpace);
                }
            }
            else
            {
                foreach ( string field in fields )
                {
                    foreach ( StudentRecord stu in students )
                    {
                        stu.ExtensionFields[nameSpace, field] = "";
                    }
                }
            }
            if ( _Provider != null )
            {
                Dictionary<StudentRecord, Dictionary<string, string>> result = _Provider.GetExtensionFields(students, nameSpace, fields);
                foreach ( StudentRecord  studentRec in students )
                {
                    if ( result.ContainsKey(studentRec) )
                    {
                        if ( fields.Length > 0 )
                        {
                            foreach ( string field in fields )
                            {
                                if ( result[studentRec].ContainsKey(field) )
                                    studentRec.ExtensionFields[nameSpace, field] = result[studentRec][field];
                            }
                        }
                        else
                        {
                            foreach ( string field in result[studentRec].Keys )
                            {
                                studentRec.ExtensionFields[nameSpace, field] = result[studentRec][field];
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region FillAttendance
        /// <summary>
        /// �I�sFillAttendance��k��
        /// </summary>
        public static event EventHandler<FillSemesterInfoEventArgs<StudentRecord>> FillingAttendance;

        /// <summary>
        /// ��J�ǥͯ��m
        /// </summary>
        public void FillAttendance(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillAttendance(students);
            if ( FillingAttendance != null )
                FillingAttendance.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// ��J�ǥͯ��m
        /// </summary>
        public void FillAttendance(params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillAttendance(students);
            if ( FillingAttendance != null )
                FillingAttendance.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// ��J�ǥͯ��m
        /// </summary>
        public void FillAttendance(int schoolYear, int semester, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillAttendance(schoolYear, semester, students);
            if ( FillingAttendance != null )
                FillingAttendance.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }

        /// <summary>
        /// ��J�ǥͯ��m
        /// </summary>
        public void FillAttendance(int schoolYear, int semester, params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillAttendance(schoolYear, semester, students);
            if ( FillingAttendance != null )
                FillingAttendance.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        } 
        #endregion

        #region FillReward
        /// <summary>
        /// �I�sFillReward��k��
        /// </summary>
        public static event EventHandler<FillSemesterInfoEventArgs<StudentRecord>> FillingReward;

        /// <summary>
        /// ��J�ǥͼ��g
        /// </summary>
        public void FillReward(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillReward(students);
            if ( FillingReward != null )
                FillingReward.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// ��J�ǥͼ��g
        /// </summary>
        public void FillReward(params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillReward(students);
            if ( FillingReward != null )
                FillingReward.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// ��J�ǥͼ��g
        /// </summary>
        public void FillReward(int schoolYear, int semester, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillReward(schoolYear, semester, students);
            if ( FillingReward != null )
                FillingReward.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }

        /// <summary>
        /// ��J�ǥͼ��g
        /// </summary>
        public void FillReward(int schoolYear, int semester, params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillReward(schoolYear, semester, students);
            if ( FillingReward != null )
                FillingReward.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        } 
        #endregion

        #region FillUpdateRecord
        /// <summary>
        /// �I�sFillUpdateRecord��k��
        /// </summary>
        public static event EventHandler<FillEventArgs<StudentRecord>> FillingUpdateRecord;
        /// <summary>
        /// ��J�ǥͲ��ʸ��
        /// </summary>
        public void FillUpdateRecord(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillUpdateRecord(students);
            if ( FillingUpdateRecord != null )
                FillingUpdateRecord.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// ��J�ǥͲ��ʸ��
        /// </summary>
        public void FillUpdateRecord(params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillUpdateRecord(students);
            if ( FillingUpdateRecord != null )
                FillingUpdateRecord.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        } 
        #endregion

        #region FillSemesterHistory
        /// <summary>
        /// �I�sFillSemesterHistory��k��
        /// </summary>
        public static event EventHandler<FillEventArgs<StudentRecord>> FillingSemesterHistory;

        /// <summary>
        /// ��J�ǥ;Ǵ����{
        /// </summary>
        public void FillSemesterHistory(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterHistory(students);
            if ( FillingSemesterHistory != null )
                FillingSemesterHistory.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// ��J�ǥ;Ǵ����{
        /// </summary>
        public void FillSemesterHistory(params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterHistory(students);
            if ( FillingSemesterHistory != null )
                FillingSemesterHistory.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }
        #endregion

        #region FillExamScore
        /// <summary>
        /// �I�sFillExamScore��k��
        /// </summary>
        public static event EventHandler<FillSemesterInfoEventArgs<StudentRecord>> FillingExamScore;
        /// <summary>
        /// ��J�ǥͭ׽ҵ��q���Z
        /// </summary>
        /// <param name="schoolYear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="students">�ǥ�</param>
        public void FillExamScore(int schoolYear, int semester, System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillExamScore(schoolYear, semester, students);
            if ( FillingExamScore != null )
                FillingExamScore.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }

        /// <summary>
        /// ��J�ǥͭ׽ҵ��q���Z
        /// </summary>
        /// <param name="schoolYear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        /// <param name="students">�ǥ�</param>
        public void FillExamScore(int schoolYear, int semester, params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillExamScore(schoolYear, semester, students);
            if ( FillingExamScore != null )
                FillingExamScore.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }
        #endregion

        #region FillSemesterSubjectScore
        /// <summary>
        /// �I�sFillSemesterSubjectScore��k��
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSemesterSubjectScore;

        /// <summary>
        /// ��J�ǥ;Ǵ���ئ��Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        public void FillSemesterSubjectScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterSubjectScore(students, filterRepeat);
            if ( FillingSemesterSubjectScore != null )
                FillingSemesterSubjectScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// ��J�ǥ;Ǵ���ئ��Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        public void FillSemesterSubjectScore(bool filterRepeat, params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterSubjectScore(students, filterRepeat);
            if ( FillingSemesterSubjectScore != null )
                FillingSemesterSubjectScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }
        #endregion

        #region FillSemesterMoralScore
        /// <summary>
        /// �I�sFillSemesterMoralScore��k��
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSemesterMoralScore;
        /// <summary>
        /// ��J�ǥ;Ǵ��w�榨�Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        public void FillSemesterMoralScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterMoralScore(students, filterRepeat);
            if ( FillingSemesterMoralScore != null )
                FillingSemesterMoralScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// ��J�ǥ;Ǵ��w�榨�Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        public void FillSemesterMoralScore(bool filterRepeat, params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterMoralScore(students, filterRepeat);
            if ( FillingSemesterMoralScore != null )
                FillingSemesterMoralScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }
        #endregion

        #region FillSemesterEntryScore
        /// <summary>
        /// �I�sFillSemesterEntryScore��k��
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSemesterEntryScore;

        /// <summary>
        /// ��J�ǥ;Ǵ��������Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        public void FillSemesterEntryScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterEntryScore(students, filterRepeat);
            if ( FillingSemesterEntryScore != null )
                FillingSemesterEntryScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// ��J�ǥ;Ǵ��������Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~�žǴ�)���</param>
        public void FillSemesterEntryScore(bool filterRepeat, params StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterEntryScore(students, filterRepeat);
            if ( FillingSemesterEntryScore != null )
                FillingSemesterEntryScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }
        #endregion

        #region FillSchoolYearSubjectScore
        /// <summary>
        /// �I�sFillingSchoolYearSubjectScore��k��
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSchoolYearSubjectScore;

        /// <summary>
        /// ��J�ǥ;Ǧ~��ئ��Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~��)���</param>
        public void FillSchoolYearSubjectScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSchoolYearSubjectScore(students, filterRepeat);
            if ( FillingSchoolYearSubjectScore != null )
                FillingSchoolYearSubjectScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// ��J�ǥ;Ǧ~��ئ��Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~��)���</param>
        public void FillSchoolYearSubjectScore(bool filterRepeat, params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillSchoolYearSubjectScore(students, filterRepeat);
            if ( FillingSchoolYearSubjectScore != null )
                FillingSchoolYearSubjectScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }
        #endregion

        #region FillSchoolYearEntryScore
        /// <summary>
        /// �I�sFillSchoolYearEntryScore��k��
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSchoolYearEntryScore;

        /// <summary>
        /// ��J�ǥ;Ǧ~�������Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~��)���</param>
        public void FillSchoolYearEntryScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSchoolYearEntryScore(students, filterRepeat);
            if ( FillingSchoolYearEntryScore != null )
                FillingSchoolYearEntryScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// ��J�ǥ;Ǧ~�������Z
        /// </summary>
        /// <param name="filterRepeat">�L�o��Ū(���Ʀ~��)���</param>
        public void FillSchoolYearEntryScore(bool filterRepeat, params StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillSchoolYearEntryScore(students, filterRepeat);
            if ( FillingSchoolYearEntryScore != null )
                FillingSchoolYearEntryScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }
        #endregion

        #region FillAttendCourse
        /// <summary>
        /// �I�sFillAttendCourse��k��
        /// </summary>
        public static event EventHandler<FillSemesterInfoEventArgs<StudentRecord>> FillingAttendCourse;

        /// <summary>
        /// ��J�ǥͭ׽Ҹ��
        /// </summary>
        /// <param name="schoolYear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        public void FillAttendCourse(int schoolYear, int semester, System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillAttendCourse(schoolYear, semester, students);
            if ( FillingAttendCourse != null )
                FillingAttendCourse.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }

        /// <summary>
        /// ��J�ǥͭ׽Ҹ��
        /// </summary>
        /// <param name="schoolYear">�Ǧ~��</param>
        /// <param name="semester">�Ǵ�</param>
        public void FillAttendCourse(int schoolYear, int semester, params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillAttendCourse(schoolYear, semester, students);
            if ( FillingAttendCourse != null )
                FillingAttendCourse.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }
        #endregion

        #region FillContactInfo
        /// <summary>
        /// �I�sFillContactInfo��k��
        /// </summary>
        public static event EventHandler<FillEventArgs<StudentRecord>> FillingContactInfo;

        /// <summary>
        /// ��J�ǥ��p�����
        /// </summary>
        public void FillContactInfo(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillContactInfo(students);
            if ( FillingContactInfo != null )
                FillingContactInfo.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// ��J�ǥ��p�����
        /// </summary>
        public void FillContactInfo(params StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillContactInfo(students);
            if ( FillingContactInfo != null )
                FillingContactInfo.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }
        #endregion

        #region FillParentInfo
        /// <summary>
        /// �I�sFillParentInfo��k��
        /// </summary>
        public static event EventHandler<FillEventArgs<StudentRecord>> FillingParentInfo;

        /// <summary>
        /// ��J�ǥ��p�����
        /// </summary>
        public void FillParentInfo(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillParentInfo(students);
            if ( FillingParentInfo != null )
                FillingParentInfo.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// ��J�ǥ��p�����
        /// </summary>
        public void FillParentInfo(params StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillParentInfo(students);
            if ( FillingParentInfo != null )
                FillingParentInfo.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }
        #endregion

        #region FillField
        /// <summary>
        /// �I�sFillField��k��
        /// </summary>
        public static event EventHandler<FillFieldEventArgs<StudentRecord>> FillingField;

        /// <summary>
        /// ��J�ǥͨ�L���
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="students">�n��J��ƪ��ǥ�</param>
        public void FillField(string fieldName, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, students);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<StudentRecord>(_AccessHelper, fieldName, students));
        }

        /// <summary>
        /// ��J�ǥͨ�L���
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="students">�n��J��ƪ��ǥ�</param>
        public void FillField(string fieldName, params StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, students);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<StudentRecord>(_AccessHelper, fieldName, students));
        }
        #endregion

        #region SetExtensionFields
        /// <summary>
        /// �g�J���������
        /// </summary>
        /// <param name="nameSpace">������쪺�R�W�Ŷ�</param>
        /// <param name="field">���W��</param>
        /// <param name="studentRecord">�ǥ�</param>
        /// <param name="value">��</param>
        public void SetExtensionFields(string nameSpace, string field, StudentRecord studentRecord,string value)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            SortedList<StudentRecord, string> list = new SortedList<StudentRecord, string>(1);
            list.Add(studentRecord, value);
            _Provider.SetExtensionFields(nameSpace, field, list);
        } 
        /// <summary>
        /// �g�J���������
        /// </summary>
        /// <param name="nameSpace">������쪺�R�W�Ŷ�</param>
        /// <param name="field">���W��</param>
        /// <param name="list">"�ǥ�"�P"��"����Ӫ�ADictionary��SortedList���ҥi�ǤJ</param>
        public void SetExtensionFields(string nameSpace, string field, IDictionary<StudentRecord, string> list)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            _Provider.SetExtensionFields(nameSpace, field, list);
        }
        #endregion
    }
}
