using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public interface TeacherRecord
    {
        /// <summary>
        /// �t�νs��
        /// </summary>
         string TeacherID { get;}
        /// <summary>
        /// �Юv�m�W
        /// </summary>
         string TeacherName { get;}
        /// <summary>
        /// ���A
        /// </summary>
         string Status { get; }
        /// <summary>
        /// �ʧO
        /// </summary>
         string Gender { get; }
        /// <summary>
        /// �Юv���O
        /// </summary>
        CategoryCollection TeacherCategory { get;}

         /// <summary>
         /// ��L������(�ݥ��I�sFillField��J�ݭn�����)
         /// </summary>
        System.Collections.Generic.Dictionary<string, object> Fields
        {
            get;
        }

        /// <summary>
        /// �ߤ@�ѧO�W��
        /// </summary>
        /// <remarks>
        /// �L�ʺٮɬ� �m�W
        /// ���ʺٮɬ� �m�W(�ʺ�)
        /// �m�W�[�ʺ٬��ߤ@�ѧO��ơA�P�@�թm�W�[�ʺ٦b�t�Τ��u���\�@��Юv�֦��C
        /// PS. �ʺ٥i�H�è��A���y��B�ŶǡA���O
        /// </remarks>
        string IdentifiableName
        {
            get;
        }
    }
}
