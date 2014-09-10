using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.SourceProvider;
using SmartSchool.Common;
using System.Windows.Forms;

namespace SmartSchool.StudentRelated.Divider
{
    interface IStudentDivider : IDenominated
    {
        TempStudentSourceProvider TempProvider { get; set; }
        DragDropTreeView TargetTreeView { get; set; }
        void Divide(Dictionary<string, BriefStudentData> source);
    }
}
