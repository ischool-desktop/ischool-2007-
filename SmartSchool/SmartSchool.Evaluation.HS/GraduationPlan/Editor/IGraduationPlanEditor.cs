﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.Evaluation.GraduationPlan.Editor
{
   public   interface  IGraduationPlanEditor
    {
        void SetSource(XmlElement source);
        XmlElement GetSource();
        bool IsDirty { get;}
        event EventHandler IsDirtyChanged;
        bool IsValidated
        {
            get;
        }
    }
}
