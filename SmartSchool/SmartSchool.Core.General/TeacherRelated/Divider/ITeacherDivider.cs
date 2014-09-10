using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.TeacherRelated.SourceProvider;
using SmartSchool.Common;
using System.Windows.Forms;

namespace SmartSchool.TeacherRelated.Divider
{
    interface ITeacherDivider : IDenominated
    {
        TempTeacherSourceProvider TempProvider { get; set; }
        DragDropTreeView TargetTreeView { get; set; }
        void Divide(Dictionary<string, BriefTeacherData> source);
    }
}
