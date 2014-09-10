using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartSchool.Customization.PlugIn;
using SmartSchool;

namespace DataManager
{
    public static class ModuleMain
    {
        [MainMethod]
        public static void Main()
        {
            MotherForm.Instance.AddProcess(new StudentAffairsRibbonBar());
            MotherForm.Instance.AddProcess(new AcademicAffairsRibbonBarSecond());
        }
    }
}