using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.Customization.PlugIn;

namespace SmartSchool.ArchivalImage
{
    public static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [MainMethod()]
        static public void Main()
        {
            if ( System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "阿寶萬歲萬歲萬萬歲")) ) return;
            OfficialStudentRecordReport report = new OfficialStudentRecordReport();
        }

    }
}