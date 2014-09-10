using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.Report
{
    public  interface IReportManager
    {
        void AddButton(ButtonAdapter button);
        void RemoveButton(ButtonAdapter button);
    }
}
