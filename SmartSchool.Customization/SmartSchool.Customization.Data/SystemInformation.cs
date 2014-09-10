using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public static class SystemInformation
    {
        private static SystemInformationProvider _Provider;
        private static System.Collections.Generic.Dictionary<string, object> _Fields = new Dictionary<string, object>();

        public static void SetProvider(SystemInformationProvider provider)
        {
            _Provider = provider;
        }

        public static event EventHandler<GetFieldEventArgs> GettingField;
        /// <summary>
        /// 取得特定欄位資料
        /// </summary>
        /// <param name="fieldName">欄位名稱</param>
        public static void getField(string fieldName)
        {
            if ( _Provider != null )
            {
                if ( _Fields.ContainsKey(fieldName) )
                    _Fields[fieldName] = _Provider.GetField(fieldName);
                else
                    _Fields.Add(fieldName, _Provider.GetField(fieldName));
            }
            if ( GettingField != null )
                GettingField.Invoke(null, new GetFieldEventArgs(fieldName));
        }

        /// <summary>
        /// 其他資料(需先呼叫getField)
        /// </summary>
        static public Dictionary<string, object> Fields
        {
            get { return _Fields; }
        }

        /// <summary>
        /// 學年度
        /// </summary>
        static public int SchoolYear
        {
            get 
            { 
                if (_Provider == null)throw new Exception("Provider尚未設定");
                return _Provider.SchoolYear;
            }
        }
        /// <summary>
        /// 學期
        /// </summary>
        static public int Semester
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider尚未設定");
                return _Provider.Semester;
            }
        }
        /// <summary>
        /// 中文校名
        /// </summary>
        static public string SchoolChineseName
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider尚未設定");
                return _Provider.SchoolChineseName;
            }
        }
        /// <summary>
        /// 英文校名
        /// </summary>
        static public string SchoolEnglishName
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider尚未設定");
                return _Provider.SchoolEnglishName;
            }
        }
        /// <summary>
        /// 學校代碼
        /// </summary>
        static public string SchoolCode
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider尚未設定");
                return _Provider.SchoolCode;
            }
        }
        /// <summary>
        /// 學校電話。
        /// </summary>
        static public string Telephone
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider尚未設定");
                return _Provider.Telephone;
            }
        }
        /// <summary>
        /// 學校傳真。
        /// </summary>
        static public string Fax
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider尚未設定");
                return _Provider.Fax;
            }
        }
        /// <summary>
        /// 學校地址。
        /// </summary>
        static public string Address
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider尚未設定");
                return _Provider.Address;
            }
        }
        /// <summary>
        /// 個人暫存資料
        /// </summary>
        static public PreferenceCollection Preference
        {
            get
            {
                if ( _Provider == null ) throw new Exception("Provider尚未設定");
                return _Provider.Preference;
            }
        }
        /// <summary>
        /// 全校通用設定資料
        /// </summary>
        static public ConfigurationCollection Configuration
        {
            get
            {
                if ( _Provider == null ) throw new Exception("Provider尚未設定");
                return _Provider.Configuration;
            }
        }
    }
}
