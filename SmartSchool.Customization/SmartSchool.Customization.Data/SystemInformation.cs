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
        /// ���o�S�w�����
        /// </summary>
        /// <param name="fieldName">���W��</param>
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
        /// ��L���(�ݥ��I�sgetField)
        /// </summary>
        static public Dictionary<string, object> Fields
        {
            get { return _Fields; }
        }

        /// <summary>
        /// �Ǧ~��
        /// </summary>
        static public int SchoolYear
        {
            get 
            { 
                if (_Provider == null)throw new Exception("Provider�|���]�w");
                return _Provider.SchoolYear;
            }
        }
        /// <summary>
        /// �Ǵ�
        /// </summary>
        static public int Semester
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider�|���]�w");
                return _Provider.Semester;
            }
        }
        /// <summary>
        /// ����զW
        /// </summary>
        static public string SchoolChineseName
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider�|���]�w");
                return _Provider.SchoolChineseName;
            }
        }
        /// <summary>
        /// �^��զW
        /// </summary>
        static public string SchoolEnglishName
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider�|���]�w");
                return _Provider.SchoolEnglishName;
            }
        }
        /// <summary>
        /// �ǮեN�X
        /// </summary>
        static public string SchoolCode
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider�|���]�w");
                return _Provider.SchoolCode;
            }
        }
        /// <summary>
        /// �Ǯչq�ܡC
        /// </summary>
        static public string Telephone
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider�|���]�w");
                return _Provider.Telephone;
            }
        }
        /// <summary>
        /// �Ǯնǯu�C
        /// </summary>
        static public string Fax
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider�|���]�w");
                return _Provider.Fax;
            }
        }
        /// <summary>
        /// �Ǯզa�}�C
        /// </summary>
        static public string Address
        {
            get
            {
                if (_Provider == null) throw new Exception("Provider�|���]�w");
                return _Provider.Address;
            }
        }
        /// <summary>
        /// �ӤH�Ȧs���
        /// </summary>
        static public PreferenceCollection Preference
        {
            get
            {
                if ( _Provider == null ) throw new Exception("Provider�|���]�w");
                return _Provider.Preference;
            }
        }
        /// <summary>
        /// ���ճq�γ]�w���
        /// </summary>
        static public ConfigurationCollection Configuration
        {
            get
            {
                if ( _Provider == null ) throw new Exception("Provider�|���]�w");
                return _Provider.Configuration;
            }
        }
    }
}
