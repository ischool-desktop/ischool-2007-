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
        /// �ίZ�Žs���M����o�Z�Ÿ��
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.ClassRecord> GetClass(IEnumerable<string> identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetClass(identities);
        }

        /// <summary>
        /// �ίZ�Žs���M����o�Z�Ÿ��
        /// </summary>
        public System.Collections.Generic.List<SmartSchool.Customization.Data.ClassRecord> GetClass(params string[] identities)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetClass(identities);
        }

        /// <summary>
        /// ���o�e���W������Z�Ÿ��
        /// </summary>
        public List<ClassRecord> GetSelectedClass()
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetSelectedClass();
        }

        /// <summary>
        /// ���o�Ҧ����Z
        /// </summary>
        public List<ClassRecord> GetAllClass()
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetAllClass();
        }

        /// <summary>
        /// �I�sFillField��k��
        /// </summary>
        public static event EventHandler<FillFieldEventArgs<ClassRecord>> FillingField;

        /// <summary>
        /// ��J�Z�Ũ�L���
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="classes">�n��J��ƪ��Z��</param>
        public void FillField(string fieldName, System.Collections.Generic.IEnumerable<ClassRecord> classes)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, classes);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<ClassRecord>(_AccessHelper, fieldName, classes));
        }

        /// <summary>
        /// ��J�Z�Ũ�L���
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="classes">�n��J��ƪ��Z��</param>
        public void FillField(string fieldName, params ClassRecord[] classes)
        {
            if ( _Provider != null )
                _Provider.FillField(fieldName, classes);
            if ( FillingField != null )
                FillingField.Invoke(this, new FillFieldEventArgs<ClassRecord>(_AccessHelper, fieldName, classes));
        }

        /// <summary>
        /// �̯Z�ŦW�٨��o�Z��
        /// </summary>
        /// <param name="className">�Z�ŦW��</param>
        /// <returns>�p�G�d�L�Z�ŦW�٫h�Ǧ^null</returns>
        public ClassRecord GetClassByClassName(string className)
        {
            if ( _Provider == null )
                throw new Exception("Provider�|���]�w");
            return _Provider.GetClassByClassName(className);
        }
    }
}
