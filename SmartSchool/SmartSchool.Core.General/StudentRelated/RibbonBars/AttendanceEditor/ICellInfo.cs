using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.AttendanceEditor
{
    public interface ICellInfo<T>
    {
        T OriginValue { get;}
        void SetValue(T value);
        bool IsDirty { get;}
    } 
}
