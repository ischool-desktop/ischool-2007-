using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class ClassHelper
    {
        private ClassInformationProvider _Provider;

        private AccessHelper _AccessHelper;

        internal ClassHelper(ClassInformationProvider provider, AccessHelper accesshelper)
        {
            _Provider = provider;
            _AccessHelper = accesshelper;
        }

        /// <summary>
        /// 用班級編號清單取得班級資料
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.ClassRecord> GetClass(IEnumerable<string> identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetClass(identities);
        }

        /// <summary>
        /// 用班級編號清單取得班級資料
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.ClassRecord> GetClass(params string[] identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetClass(identities);
        }

        /// <summary>
        /// 取得畫面上選取的班級資料
        /// </summary>
        public List<ClassRecord> GetSelectedClass()
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetSelectedClass();
        }

        /// <summary>
        /// 取得所有成績
        /// </summary>
        public List<ClassRecord> GetAllClass()
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetAllClass();
        }

        /// <summary>
        /// 呼叫FillField方法時
        /// </summary>
        public static event EventHandler<FillFieldEventArgs<ClassRecord>> FillingField;

        /// <summary>
        /// 填入班級其他資料
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="classes">要填入資料的班級</param>
        public void FillField(string fieldName, System.Collections.Generic.IEnumerable<ClassRecord> classes)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, classes);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<ClassRecord>(_AccessHelper, fieldName, classes));
        }

        /// <summary>
        /// 填入班級其他資料
        /// </summary>
        /// <param name="fieldName">要填入的欄位名稱</param>
        /// <param name="classes">要填入資料的班級</param>
        public void FillField(string fieldName, params ClassRecord[] classes)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, classes);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<ClassRecord>(_AccessHelper, fieldName, classes));
        }

        /// <summary>
        /// 依班級名稱取得班級
        /// </summary>
        /// <param name="className">班級名稱</param>
        /// <returns>如果查無班級名稱則傳回null</returns>
        public ClassRecord GetClassByClassName(string className)
        {
            if ( _Provider == null )
                throw new Exception("Provider尚未設定");
            return _Provider.GetClassByClassName(className);
        }
    }
}
