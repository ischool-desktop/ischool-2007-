using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API.Provider
{
    public class GetSystemFieldEventArgs : EventArgs
    {
        internal GetSystemFieldEventArgs(string fieldName)
        {
            _FieldName = fieldName;
        }

        internal GetSystemFieldEventArgs(string fieldName, object result)
            : this(fieldName)
        {
            _Result = result;
        }

        private string _FieldName = "";
        public string FieldName { get { return _FieldName; } }

        private object _Result = null;
        public object Result { get { return _Result; } set { _Result = value; } }
    }

    public class FillDataEventArgs<T> : EventArgs
    {
        internal FillDataEventArgs(System.Collections.Hashtable cashePool, string fillMethod, IEnumerable<T> items, params object[] args)
        {
            _CashePool = cashePool;
            _FillMethod = fillMethod;
            _Items = new List<T>(items);
            _Args = args;
        }
        private List<T> _Items;
        public List<T> Items { get { return _Items; } }

        private System.Collections.Hashtable _CashePool;
        public System.Collections.Hashtable CashePool { get { return _CashePool; } }

        private string _FillMethod = "";
        public string FillMethod { get { return _FillMethod; } }

        private object[] _Args;
        public object[] Args { get { return _Args; } }
    }

    public class FillStudentEventArgs : FillDataEventArgs<Customization.Data.StudentRecord>
    {
        internal FillStudentEventArgs(System.Collections.Hashtable cashePool, string fillMethod, IEnumerable<Customization.Data.StudentRecord> items, params object[] args) : base(cashePool, fillMethod, items, args) { }
    }

    public class FillClassEventArgs : FillDataEventArgs<Customization.Data.ClassRecord>
    {

        internal FillClassEventArgs(System.Collections.Hashtable cashePool, string fillMethod, IEnumerable<Customization.Data.ClassRecord> items, params object[] args) : base(cashePool, fillMethod, items, args) { }
    }

    public class FillTeacherEventArgs : FillDataEventArgs<Customization.Data.TeacherRecord>
    {

        internal FillTeacherEventArgs(System.Collections.Hashtable cashePool, string fillMethod, IEnumerable<Customization.Data.TeacherRecord> items, params object[] args) : base(cashePool, fillMethod, items, args) { }
    }

    public class FillCourseEventArgs : FillDataEventArgs<Customization.Data.CourseRecord>
    {

        internal FillCourseEventArgs(System.Collections.Hashtable cashePool, string fillMethod, IEnumerable<Customization.Data.CourseRecord> items, params object[] args) : base(cashePool, fillMethod, items, args) { }
    }
}
