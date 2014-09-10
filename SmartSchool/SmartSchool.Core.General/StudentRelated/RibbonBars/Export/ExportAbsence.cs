using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.RibbonBars.Export
{
    [FeatureCode("Button0180")]
    class ExportAbsence : ExportProcess
    {
        private AccessHelper _access_helper;

        public ExportAbsence()
        {
            this.Title = "匯出缺曠紀錄";
            this.Group = "缺曠獎懲";
            foreach (string var in new string[] { "學年度", "學期", "日期", "缺曠假別", "缺曠節次", "節次類別" })
            {
                this.ExportableFields.Add(var);
            }
            this.ExportPackage += new EventHandler<ExportPackageEventArgs>(ExportAbsence_ExportPackage);
            _access_helper = new AccessHelper();
        }

        private void ExportAbsence_ExportPackage(object sender, ExportPackageEventArgs e)
        {
            List<SmartSchool.Customization.Data.StudentRecord> students = _access_helper.StudentHelper.GetStudents(e.List);
            _access_helper.StudentHelper.FillAttendance(students);

            foreach (SmartSchool.Customization.Data.StudentRecord stu in students)
            {
                foreach (AttendanceInfo var in stu.AttendanceList)
                {
                    RowData row = new RowData();
                    row.ID = stu.StudentID;
                    foreach (string field in e.ExportFields)
                    {
                        if (ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "學年度": row.Add(field, var.SchoolYear.ToString()); break;
                                case "學期": row.Add(field, var.Semester.ToString()); break;
                                case "日期": row.Add(field, var.OccurDate.ToShortDateString()); break;
                                case "缺曠假別": row.Add(field, var.Absence); break;
                                case "缺曠節次": row.Add(field, var.Period); break;
                                case "節次類別": row.Add(field, var.PeriodType); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
            }
        }
    }
}
