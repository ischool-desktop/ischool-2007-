using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// �Z�Ŭ���
    /// </summary>
    public interface ClassRecord
    {
        /// <summary>
        /// �t�νs��
        /// </summary>
         string ClassID { get; }
        /// <summary>
        /// �Z�ŦW��
        /// </summary>
         string ClassName { get; }
        /// <summary>
        /// �~��
        /// </summary>
         string GradeYear { get; }
        /// <summary>
        /// ��O
        /// </summary>
         string Department { get;}
        /// <summary>
        /// �Z�����O
        /// </summary>
        CategoryCollection ClassCategorys { get;}

        /// <summary>
        /// �Z�ɮv
        /// </summary>
        TeacherRecord RefTeacher
        {
            get;
        }

        /// <summary>
        /// �Z�žǥ�
        /// </summary>
        List<StudentRecord> Students
        {
            get;
        }

        /// <summary>
        /// ��L������(�ݥ��I�sFillField��J�ݭn�����)
        /// </summary>
        System.Collections.Generic.Dictionary<string, object> Fields
        {
            get;
        }
    }
}
