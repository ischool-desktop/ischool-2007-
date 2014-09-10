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
        /// 取得教師資料
        /// </summary>
        public List<TeacherRecord> GetTeacher(System.Collections.Generic.IEnumerable<string> identities)
        {
            if (_Provider == null)
                throw new Exception("Provider尚未設定");
            return _Provider.GetTeacher(identities);
        }


        /// <summary>
        /// 取得教師資料
        /// </summary>
        public List<TeacherRecord> GetTeacher(params string[] identities)
        {
            if (_Provider == null)
                throw new Exception("Provider尚未設定");
            return _Provider.GetTeacher(identities);
        }

        /// <summary>
        /// 取得所有教師資料
        /// </summary>
        public List<TeacherRecord> GetAllTeacher()
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetAllTeacher();
        }

        /// <summary>
        /// 取得畫面上選取的教師資料
        /// </summary>
        public List<TeacherRecord> GetSelectedTeacher()
        {
            if (_Provider == null)
                throw new Exception("Provider尚未設定");
            return _Provider.GetSelectedTeacher();
        }

        public static event EventHandler<GettingLectureTeacherEventArgs> GettingLectureTeacher;
        /// <summary>
        /// 取得授課教師
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
                    throw new Exception("Provider尚未設定");
                return _Provider.GetLectureTeacher(course);
            }
        }

        /// <summary>
        /// 呼叫FillField方法時
        /// </summary>
        public static event EventHandler<FillFieldEventArgs<TeacherRecord>> FillingField;

        /// <summary>
        /// 填入教師其他資料
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="teachers">要填入資料的教師</param>
        public void FillField(string fieldName, System.Collections.Generic.IEnumerable<TeacherRecord> teachers)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, teachers);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<TeacherRecord>(_AccessHelper, fieldName, teachers));
        }

        /// <summary>
        /// 填入教師其他資料
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="teachers">要填入資料的教師</param>
        public void FillField(string fieldName, params TeacherRecord[] teachers)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, teachers);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<TeacherRecord>(_AccessHelper, fieldName, teachers));
        }

        /// <summary>
        /// 依識別名稱取得教師
        /// </summary>
        /// <param name="identifiableName">教師識別名稱</param>
        /// <returns>查無此教師則傳回null</returns>
        public TeacherRecord GetTeacherByIdentifiableName(string identifiableName)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetTeacherByIdentifiableName(identifiableName);
        }
    }
}
