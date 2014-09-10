using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.StudentRelated.Placing.Control
{
    public interface ISubjectStatistic
    {
        SubjectInfoCollection Statistic();
    }

    public class SubjectInfoCollection : IEnumerable<SubjectInfo>
    {
        private List<SubjectInfo> _list;

        public SubjectInfoCollection()
        {
            _list = new List<SubjectInfo>();
        }

        /// <summary>
        /// �O�_�]�t���w���
        /// </summary>
        /// <param name="subjectName">��ئW��</param>
        /// <returns>�O�_�]�t�����</returns>
        public bool Contains(string subjectName)
        {
            foreach (SubjectInfo info in _list)
            {
                if (info.SubjectName == subjectName)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// ���o���w��ظ�T
        /// </summary>
        /// <param name="subjectName">��ئW��</param>
        /// <returns>SubjectInfo ����, �Y������h�Ǧ^null</returns>
        public SubjectInfo GetSubjectInfo(string subjectName)
        {
            foreach (SubjectInfo info in _list)
            {
                if (info.SubjectName == subjectName)
                    return info;
            }
            return null;
        }

        /// <summary>
        /// ��J����ءC�Y����ؤw�s�b�h�W�[�׽ҤH�Ƣ����A�Y���s�b�h�s�W�ܶ��X���C
        /// </summary>
        /// <param name="subjectName"></param>
        public void Put(string subjectName)
        {
            SubjectInfo info;
            if (Contains(subjectName))
                info = GetSubjectInfo(subjectName);
            else
            {
                info = new SubjectInfo(subjectName, 0);
                _list.Add(info);
            }
            info.AddStudentCount();            
        }

        #region IEnumerable<SubjectInfo> ����

        public IEnumerator<SubjectInfo> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable ����

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion
    }

    public class SubjectInfo
    {
        private string _subjectName;
        private int _studentCount;

        /// <summary>
        /// ��ظ�T
        /// </summary>
        /// <param name="subjectName">��ئW��</param>
        /// <param name="studentCount">�׽ҤH��</param>
        public SubjectInfo(string subjectName, int studentCount)
        {
            _subjectName = subjectName;
            _studentCount = studentCount;
        }

        /// <summary>
        /// ���o��ئW��
        /// </summary>
        public string SubjectName
        {
            get { return _subjectName; }
        }

        /// <summary>
        /// ���o�׽ҤH��
        /// </summary>
        public int StudentCount
        {
            get { return _studentCount; }
        }

        /// <summary>
        /// �W�[�׽ҤH��
        /// </summary>
        public void AddStudentCount()
        {
            _studentCount++;
        }
    }
}
