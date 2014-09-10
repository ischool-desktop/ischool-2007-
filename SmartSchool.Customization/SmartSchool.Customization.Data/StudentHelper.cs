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
        /// 用學生編號取得學生資料
        /// </summary>
        public SmartSchool.Customization.Data.StudentRecord GetStudent(string identity)
        {
            System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> list = GetStudents(identity);
            if ( list.Count == 0 )
                throw new Exception("此學生編號不存在");
            else
                return list[0];
        }

        /// <summary>
        /// 用學生編號清單取得學生資料
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetStudents(IEnumerable<string> identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetStudents(identities);
        }

        /// <summary>
        /// 用學生編號清單取得學生資料
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetStudents(params string[] identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetStudents(identities);
        }

        /// <summary>
        /// 依學號取得在校生
        /// </summary>
        /// <param name="studentNumber">學號</param>
        /// <returns>如查無此學生或不是在校生則傳回null</returns>
        public StudentRecord GetStudentByStudentNumber(string studentNumber)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetStudentByStudentNumber(studentNumber);
        }

        /// <summary>
        /// 取得在系統畫面中選取的學生
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetSelectedStudent()
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetSelectedStudent();
        }

        /// <summary>
        /// 取得所有的在校學生，在校學生指學生狀態為"一般"、"延修"及"輟學"(國中才會有)
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.StudentRecord> GetAllStudent()
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetAllStudent();
        }

        #region FillExtensionFields
        /// <summary>
        /// 填入學生延伸欄位資料
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
        /// 呼叫FillAttendance方法時
        /// </summary>
        public static event EventHandler<FillSemesterInfoEventArgs<StudentRecord>> FillingAttendance;

        /// <summary>
        /// 填入學生缺曠
        /// </summary>
        public void FillAttendance(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillAttendance(students);
            if ( FillingAttendance != null )
                FillingAttendance.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// 填入學生缺曠
        /// </summary>
        public void FillAttendance(params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillAttendance(students);
            if ( FillingAttendance != null )
                FillingAttendance.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// 填入學生缺曠
        /// </summary>
        public void FillAttendance(int schoolYear, int semester, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillAttendance(schoolYear, semester, students);
            if ( FillingAttendance != null )
                FillingAttendance.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }

        /// <summary>
        /// 填入學生缺曠
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
        /// 呼叫FillReward方法時
        /// </summary>
        public static event EventHandler<FillSemesterInfoEventArgs<StudentRecord>> FillingReward;

        /// <summary>
        /// 填入學生獎懲
        /// </summary>
        public void FillReward(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillReward(students);
            if ( FillingReward != null )
                FillingReward.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// 填入學生獎懲
        /// </summary>
        public void FillReward(params  StudentRecord[] students)
        {
            if ( _Provider != null )
                _Provider.FillReward(students);
            if ( FillingReward != null )
                FillingReward.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// 填入學生獎懲
        /// </summary>
        public void FillReward(int schoolYear, int semester, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillReward(schoolYear, semester, students);
            if ( FillingReward != null )
                FillingReward.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }

        /// <summary>
        /// 填入學生獎懲
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
        /// 呼叫FillUpdateRecord方法時
        /// </summary>
        public static event EventHandler<FillEventArgs<StudentRecord>> FillingUpdateRecord;
        /// <summary>
        /// 填入學生異動資料
        /// </summary>
        public void FillUpdateRecord(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillUpdateRecord(students);
            if ( FillingUpdateRecord != null )
                FillingUpdateRecord.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// 填入學生異動資料
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
        /// 呼叫FillSemesterHistory方法時
        /// </summary>
        public static event EventHandler<FillEventArgs<StudentRecord>> FillingSemesterHistory;

        /// <summary>
        /// 填入學生學期歷程
        /// </summary>
        public void FillSemesterHistory(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterHistory(students);
            if ( FillingSemesterHistory != null )
                FillingSemesterHistory.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// 填入學生學期歷程
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
        /// 呼叫FillExamScore方法時
        /// </summary>
        public static event EventHandler<FillSemesterInfoEventArgs<StudentRecord>> FillingExamScore;
        /// <summary>
        /// 填入學生修課評量成績
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="students">學生</param>
        public void FillExamScore(int schoolYear, int semester, System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillExamScore(schoolYear, semester, students);
            if ( FillingExamScore != null )
                FillingExamScore.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }

        /// <summary>
        /// 填入學生修課評量成績
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
        /// <param name="students">學生</param>
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
        /// 呼叫FillSemesterSubjectScore方法時
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSemesterSubjectScore;

        /// <summary>
        /// 填入學生學期科目成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
        public void FillSemesterSubjectScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterSubjectScore(students, filterRepeat);
            if ( FillingSemesterSubjectScore != null )
                FillingSemesterSubjectScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// 填入學生學期科目成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
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
        /// 呼叫FillSemesterMoralScore方法時
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSemesterMoralScore;
        /// <summary>
        /// 填入學生學期德行成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
        public void FillSemesterMoralScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterMoralScore(students, filterRepeat);
            if ( FillingSemesterMoralScore != null )
                FillingSemesterMoralScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// 填入學生學期德行成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
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
        /// 呼叫FillSemesterEntryScore方法時
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSemesterEntryScore;

        /// <summary>
        /// 填入學生學期分項成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
        public void FillSemesterEntryScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSemesterEntryScore(students, filterRepeat);
            if ( FillingSemesterEntryScore != null )
                FillingSemesterEntryScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// 填入學生學期分項成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級學期)資料</param>
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
        /// 呼叫FillingSchoolYearSubjectScore方法時
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSchoolYearSubjectScore;

        /// <summary>
        /// 填入學生學年科目成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級)資料</param>
        public void FillSchoolYearSubjectScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSchoolYearSubjectScore(students, filterRepeat);
            if ( FillingSchoolYearSubjectScore != null )
                FillingSchoolYearSubjectScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// 填入學生學年科目成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級)資料</param>
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
        /// 呼叫FillSchoolYearEntryScore方法時
        /// </summary>
        public static event EventHandler<FillScoreInfoEventArgs<StudentRecord>> FillingSchoolYearEntryScore;

        /// <summary>
        /// 填入學生學年分項成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級)資料</param>
        public void FillSchoolYearEntryScore(bool filterRepeat, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillSchoolYearEntryScore(students, filterRepeat);
            if ( FillingSchoolYearEntryScore != null )
                FillingSchoolYearEntryScore.Invoke(this, new FillScoreInfoEventArgs<StudentRecord>(_AccessHelper, filterRepeat, students));
        }

        /// <summary>
        /// 填入學生學年分項成績
        /// </summary>
        /// <param name="filterRepeat">過濾重讀(重複年級)資料</param>
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
        /// 呼叫FillAttendCourse方法時
        /// </summary>
        public static event EventHandler<FillSemesterInfoEventArgs<StudentRecord>> FillingAttendCourse;

        /// <summary>
        /// 填入學生修課資料
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
        public void FillAttendCourse(int schoolYear, int semester, System.Collections.Generic.IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillAttendCourse(schoolYear, semester, students);
            if ( FillingAttendCourse != null )
                FillingAttendCourse.Invoke(this, new FillSemesterInfoEventArgs<StudentRecord>(_AccessHelper, schoolYear, semester, students));
        }

        /// <summary>
        /// 填入學生修課資料
        /// </summary>
        /// <param name="schoolYear">學年度</param>
        /// <param name="semester">學期</param>
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
        /// 呼叫FillContactInfo方法時
        /// </summary>
        public static event EventHandler<FillEventArgs<StudentRecord>> FillingContactInfo;

        /// <summary>
        /// 填入學生聯絡資料
        /// </summary>
        public void FillContactInfo(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillContactInfo(students);
            if ( FillingContactInfo != null )
                FillingContactInfo.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// 填入學生聯絡資料
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
        /// 呼叫FillParentInfo方法時
        /// </summary>
        public static event EventHandler<FillEventArgs<StudentRecord>> FillingParentInfo;

        /// <summary>
        /// 填入學生聯絡資料
        /// </summary>
        public void FillParentInfo(System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillParentInfo(students);
            if ( FillingParentInfo != null )
                FillingParentInfo.Invoke(this, new FillEventArgs<StudentRecord>(_AccessHelper, students));
        }

        /// <summary>
        /// 填入學生聯絡資料
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
        /// 呼叫FillField方法時
        /// </summary>
        public static event EventHandler<FillFieldEventArgs<StudentRecord>> FillingField;

        /// <summary>
        /// 填入學生其他資料
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="students">要填入資料的學生</param>
        public void FillField(string fieldName, System.Collections.Generic.IEnumerable<StudentRecord> students)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, students);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<StudentRecord>(_AccessHelper, fieldName, students));
        }

        /// <summary>
        /// 填入學生其他資料
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="students">要填入資料的學生</param>
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
        /// 寫入延伸欄位資料
        /// </summary>
        /// <param name="nameSpace">延伸欄位的命名空間</param>
        /// <param name="field">欄位名稱</param>
        /// <param name="studentRecord">學生</param>
        /// <param name="value">值</param>
        public void SetExtensionFields(string nameSpace, string field, StudentRecord studentRecord,string value)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            SortedList<StudentRecord, string> list = new SortedList<StudentRecord, string>(1);
            list.Add(studentRecord, value);
            _Provider.SetExtensionFields(nameSpace, field, list);
        } 
        /// <summary>
        /// 寫入延伸欄位資料
        /// </summary>
        /// <param name="nameSpace">延伸欄位的命名空間</param>
        /// <param name="field">欄位名稱</param>
        /// <param name="list">"學生"與"值"的對照表，Dictionary或SortedList等皆可傳入</param>
        public void SetExtensionFields(string nameSpace, string field, IDictionary<StudentRecord, string> list)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            _Provider.SetExtensionFields(nameSpace, field, list);
        }
        #endregion
    }
}
