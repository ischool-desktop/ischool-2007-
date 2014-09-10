using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using System.Drawing;
using SmartSchool.StudentRelated;
using DevComponents.DotNetBar;

namespace SmartSchool.StudentRelated
{
    public class BriefDataChangedEventArgs : EventArgs
    {
        private List<BriefDataChangedContent> _Items=new List<BriefDataChangedContent>();
        public List<BriefDataChangedContent> Items { get { return _Items; } }
    }

    public class BriefDataChangedContent
    {
        private readonly BriefStudentData _OldData;
        private readonly BriefStudentData _NewData;
        public BriefStudentData OldData { get { return _OldData; } }
        public BriefStudentData NewData { get { return _NewData; } }
        public BriefDataChangedContent(BriefStudentData oldData, BriefStudentData newData)
        {
            _OldData = oldData;
            _NewData = newData;
        }
    }

    public class StudentDeletedEventArgs : EventArgs
    {
        string _id;
        public string ID
        {
            get{return _id;}
            set{_id=value;}
        }
    }

    public class StudentAttendanceChangedEventArgs : EventArgs
    {
        List<BriefStudentData> _Items;
        public StudentAttendanceChangedEventArgs(params BriefStudentData[] items)
        {
            _Items = new List<BriefStudentData>();
            foreach ( BriefStudentData var in items )
            {
                _Items.Add(var);
            }
        }
        public List<BriefStudentData> Students
        { get { return _Items; } }
    }

    public class StudentDisciplineChangedEventArgs : EventArgs
    {
        List<BriefStudentData> _Items;
        public StudentDisciplineChangedEventArgs(params BriefStudentData[] items)
        {
            _Items = new List<BriefStudentData>();
            foreach ( BriefStudentData var in items )
            {
                _Items.Add(var);
            }
        }
        public List<BriefStudentData> Students
        { get { return _Items; } }
    }
}
