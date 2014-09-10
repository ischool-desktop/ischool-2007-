using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.Feature;

namespace SmartSchool.StudentRelated
{
    public class StudentCollection : ReadOnlyCollection<string, BriefStudentData>
    {
        internal StudentCollection(Dictionary<string, BriefStudentData> items)
            : base(items)
        { }
        public override BriefStudentData this[string ID]
        {
            get
            {
                Student.Instance.EnsureStudent(new string[1] { ID });
                return base[ID];
            }
        }
        public void EnsureStudent(IEnumerable<string> idlist)
        {
            Student.Instance.EnsureStudent(idlist);
        }
    }
}
