using System;
using System.Collections.Generic;
using SmartSchool;
using SmartSchool.Customization.PlugIn;

namespace StatisticsAndReport
{
    public static class Program
    {
        [MainMethod()]
        public static void Main()
        {
            MotherForm.Instance.AddProcess(new Process(), 4);
        }
    }
}