//using System;
//using System.Collections.Generic;
//using System.Text;
//using SmartSchool.StudentRelated.Process.StudentIUD;
//using SmartSchool.StudentRelated.Palmerworm;
//using DevComponents.DotNetBar;
//using SmartSchool.StudentRelated;

//namespace SmartSchool.StudentRelated
//{
//    internal class StudentUDMediator
//    {
//        private static StudentIDUProcess process;
//        private static PalmerwormStudent palmerworm;
//        public static StudentIDUProcess Process
//        {
//            get { return process; }
//            set
//            {
//                if (process != null)
//                {
//                    process.btnSave.Click -= new EventHandler(btnSave_Click);
//                    process.btnDelete.Click -= new EventHandler(btnDelete_Click);
//                }
//                process = value;
//                process.btnSave.Click += new EventHandler(btnSave_Click);
//                process.btnDelete.Click += new EventHandler(btnDelete_Click);
//            }
//        }

//        public static PalmerwormStudent Palmerworm
//        {
//            get { return palmerworm; }
//            set 
//            {
//                if (value != null)
//                {
//                    value.VisibleChanged -= new EventHandler(palmerworm_VisibleChanged);
//                    value.palmerwormSave.EnabledChanged -= new EventHandler(palmerwormSave_EnabledChanged);
//                }
//                palmerworm = value;
//                value.VisibleChanged += new EventHandler(palmerworm_VisibleChanged);
//                value.palmerwormSave.EnabledChanged += new EventHandler(palmerwormSave_EnabledChanged);
//                if (process == null) return;
//                process.btnDelete.Enabled =  palmerworm.Visible;
//                process.btnSave.Enabled = palmerworm.palmerwormSave.Enabled & palmerworm.Visible;
//                //process.itemContainer6.Visible = palmerworm.Visible;
//                //process.ProcessRibbon.RecalcLayout();
//                //((RibbonControl)((RibbonPanel)process.ProcessRibbon.Parent).Parent).RecalcLayout();
//            }
//        }

//        static void btnSave_Click(object sender, EventArgs e)
//        {
//            if (palmerworm == null) return;
//            palmerworm.palmerwormSave_Click(null, null);
//        }

//        static void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (palmerworm == null) return;
//            palmerworm.palmerwormDelete_Click(null, null);
//        }

//        static void palmerwormSave_EnabledChanged(object sender, EventArgs e)
//        {
//            if (process == null) return;
//            process.btnSave.Enabled = palmerworm.palmerwormSave.Enabled;
//        }

//        static void palmerworm_VisibleChanged(object sender, EventArgs e)
//        {
//            if (process == null) return;
//            process.btnDelete.Enabled = palmerworm.Visible;
//            process.btnSave.Enabled = palmerworm.palmerwormSave.Enabled & palmerworm.Visible;
//            //process.itemContainer6.Visible = palmerworm.Visible;
//            //process.itemContainer6.rec
//            //process.ProcessRibbon.RecalcLayout();
//            //((RibbonControl)((RibbonPanel)process.ProcessRibbon.Parent).Parent).RecalcLayout();
//        }
//    }
//}
