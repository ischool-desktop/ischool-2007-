using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.Evaluation.Configuration
{
    interface  IMoralConductInstance
    {
        string XPath { get;}

        void SetSource(XmlElement source);

        XmlElement GetSource();

        bool IsValidate{get;}

        event EventHandler IsValidateChanged;

        bool IsDirty { get;}

        event EventHandler IsDirtyChanged;

        void GetDependenceData();

        void SetDependenceData();
    }
}
