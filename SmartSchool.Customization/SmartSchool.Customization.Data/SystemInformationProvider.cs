using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
     public interface SystemInformationProvider
    {
        /// <summary>
        /// �Ǧ~��
        /// </summary>
         int SchoolYear
        {
            get;
        }
        /// <summary>
        /// �Ǵ�
        /// </summary>
         int Semester
        {
            get;
        }
        /// <summary>
        /// ����զW
        /// </summary>
         string SchoolChineseName
        {
            get;
        }
        /// <summary>
        /// �^��զW
        /// </summary>
         string SchoolEnglishName
        {
            get;
        }
        /// <summary>
        /// �ǮեN�X
        /// </summary>
         string SchoolCode
        {
            get;
        }
        /// <summary>
        /// �Ǯչq�ܡC
        /// </summary>
         string Telephone
        {
            get;
        }
        /// <summary>
        /// �Ǯնǯu�C
        /// </summary>
         string Fax
        {
            get;
        }
        /// <summary>
        /// �Ǯզa�}�C
        /// </summary>
         string Address
        {
            get;
        }
         /// <summary>
         /// �n�J�b�������]�w���
         /// </summary>
         PreferenceCollection Preference{get;}
         /// <summary>
         /// ���ճq�γ]�w���
         /// </summary>
         ConfigurationCollection Configuration { get;}
         object GetField(string fieldName);
    }
}
