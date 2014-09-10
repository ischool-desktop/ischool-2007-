using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.ClassRelated.SourceProvider;
using SmartSchool.Common;
using System.Windows.Forms;

namespace SmartSchool.ClassRelated.Divider
{
    interface IClassDivider : IDenominated
    {
        TempClassSourceProvider TempProvider { get; set; }
        TreeView TargetTreeView { get; set; }
        void Divide(Dictionary<string, ClassInfo> source);
    }
}
