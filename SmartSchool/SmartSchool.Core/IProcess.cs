using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;

namespace SmartSchool
{
    public  interface IProcess
    {
        double Level { get;set;}
        string ProcessTabName { get;}
        RibbonBar ProcessRibbon { get;}
    }
}
