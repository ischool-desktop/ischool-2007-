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
        /// 是否包含指定科目
        /// </summary>
        /// <param name="subjectName">科目名稱</param>
        /// <returns>是否包含此科目</returns>
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
        /// 取得指定科目資訊
        /// </summary>
        /// <param name="subjectName">科目名稱</param>
        /// <returns>SubjectInfo 物件, 若取不到則傳回null</returns>
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
        /// 塞入此科目。若此科目已存在則增加修課人數１次，若不存在則新增至集合中。
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

        #region IEnumerable<SubjectInfo> 成員

        public IEnumerator<SubjectInfo> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成員

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
        /// 科目資訊
        /// </summary>
        /// <param name="subjectName">科目名稱</param>
        /// <param name="studentCount">修課人數</param>
        public SubjectInfo(string subjectName, int studentCount)
        {
            _subjectName = subjectName;
            _studentCount = studentCount;
        }

        /// <summary>
        /// 取得科目名稱
        /// </summary>
        public string SubjectName
        {
            get { return _subjectName; }
        }

        /// <summary>
        /// 取得修課人數
        /// </summary>
        public int StudentCount
        {
            get { return _studentCount; }
        }

        /// <summary>
        /// 增加修課人數
        /// </summary>
        public void AddStudentCount()
        {
            _studentCount++;
        }
    }
}
