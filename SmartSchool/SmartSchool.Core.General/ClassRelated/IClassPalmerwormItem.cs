using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated
{
    interface IClassPalmerwormItem:SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem
    {
        bool IsValid();
    }
}
