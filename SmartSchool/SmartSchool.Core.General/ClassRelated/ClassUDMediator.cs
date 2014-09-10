//using System;
//using System.Collections.Generic;
//using System.Text;
//using SmartSchool.StudentRelated.Process.StudentIUD;
//using SmartSchool.StudentRelated.Palmerworm;
//using DevComponents.DotNetBar;
//using SmartSchool.StudentRelated;
//using SmartSchool.ClassRelated.RibbonBars;

//namespace SmartSchool.ClassRelated
//{
//    internal class ClassUDMediator
//    {
//        private static Manage process;
//        private static ClassInfoPanel palmerworm;

//        public static Manage Process
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

//        public static ClassInfoPanel Palmerworm
//        {
//            get { return palmerworm; }
//            set 
//            {
//                palmerworm = value;
//                if (value != null)
//                {
//                    palmerworm.SaveableChanged += new EventHandler<SaveableEventArgs>(palmerworm_SaveableChanged);
//                    palmerworm.VisibleChanged += new EventHandler(palmerworm_VisibleChanged);

//                    if (process != null)
//                    {
//                        process.btnSave.Enabled = false;
//                        process.btnDelete.Enabled = true;
//                    }
//                }
//                else
//                {
//                    if (process != null)
//                    {
//                        process.btnSave.Enabled = false;
//                        process.btnDelete.Enabled = false;
//                    }
//                }
//            }
//        }

//        static void palmerworm_SaveableChanged(object sender, SaveableEventArgs e)
//        {
//            process.btnSave.Enabled = e.Saveable;
//        }

//        static void btnSave_Click(object sender, EventArgs e)
//        {
//            if (palmerworm == null) return;
//            palmerworm.Save();
//        }

//        static void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (palmerworm == null) return;       
//            palmerworm.Delete();
//        }

//        static void palmerwormSave_EnabledChanged(object sender, EventArgs e)
//        {
//            if (process == null) return;
//            process.btnSave.Enabled = palmerworm.CheckToSave();
//        }

//        static void palmerworm_VisibleChanged(object sender, EventArgs e)
//        {
//            if (process == null) return;
//            process.btnDelete.Enabled = palmerworm.Visible;
//            process.btnSave.Enabled = palmerworm.CheckToSave();        
//        }

//        public static void OnSelectedClassChange(bool hasSelectedItem)
//        {
//            if(process != null)
//                process.btnDelete.Enabled = hasSelectedItem;
//        }
//    }
//}
