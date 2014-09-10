using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.ClassRelated.RibbonBars.DeXing
{
    interface IDeXingExport
    {
        Control MainControl { get;}
        void LoadData();
        void Export();
    }
}
