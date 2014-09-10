using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.CourseRelated.RibbonBars.ScoresCalc
{
    interface IProgressUI
    {
        void ReportProgress(string message, int progress);

        void Cancel();

        bool Cancellation { get;}
    }
}
