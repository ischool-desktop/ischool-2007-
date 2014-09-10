using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public interface ClassInformationProvider : ICloneable
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
        /// �ίZ�Žs�����o�Z�Ÿ��
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.ClassRecord> GetClass(IEnumerable<string> identities);

        /// <summary>
        /// ���o�b�t�εe����������Z��
        /// </summary>
        System.Collections.Generic.List<SmartSchool.Customization.Data.ClassRecord> GetSelectedClass();

        /// <summary>
        /// ���o�Ҧ��Z��
        /// </summary>
        List<ClassRecord> GetAllClass();

        /// <summary>
        /// ��J�Z�Ŭ������
        /// </summary>
        /// <param name="fieldName">�n��J�����W��</param>
        /// <param name="classes">�n��J��ƪ��Z��</param>
        void FillField(string fieldName, System.Collections.Generic.IEnumerable<ClassRecord> classes);

        /// <summary>
        /// �̯Z�ŦW�٨��o�Z��
        /// </summary>
        /// <param name="className">�Z�ŦW��</param>
        /// <returns>�p�G�d�L�Z�ŦW�٫h�Ǧ^null</returns>
        ClassRecord GetClassByClassName(string className);

    }
}
